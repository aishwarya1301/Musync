using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Musync
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Mood : Page
    {
        private StorageFile inputImage;
        public Mood()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var file = e.Parameter as StorageFile;
            if (file != null)
            {
                inputImage =file;
                IRandomAccessStream stream = await inputImage.OpenAsync(FileAccessMode.Read);
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
                SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();

                SoftwareBitmap softwareBitmapBGR8 = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8,BitmapAlphaMode.Premultiplied);

                SoftwareBitmapSource bitmapSource = new SoftwareBitmapSource();
                await bitmapSource.SetBitmapAsync(softwareBitmapBGR8);

                imageControl.Source = bitmapSource;

            }
        }

        private async void MoodFinder_OnClick(object sender, RoutedEventArgs e)
        {
            //
            // Create Project Oxford Emotion API Service client
            //
            EmotionServiceClient emotionServiceClient = new EmotionServiceClient(App.EmotionSubscriptionKey);
            Emotion[] emotionResult;
            await Task.Run(async () =>
            {
                await Task.Yield();
                using (Stream imageFileStream = File.OpenRead(inputImage.Path))
                {
                    emotionResult = await emotionServiceClient.RecognizeAsync(imageFileStream);
                    Debug.WriteLine(emotionResult);
                }
                var detectedEmotion = emotionResult.First().Scores.ToRankedList().First();
                Debug.WriteLine(detectedEmotion.ToString());
                switch (detectedEmotion.Key)
                {
                    case "Happiness":
                        App.mood = "happy";
                        break;
                    case "Sadness":
                        App.mood = "sadness";
                        break;
                    case "Anger":
                        App.mood = "angry";
                        break;
                    case "Fear":
                        App.mood = "fear";
                        break;
                    default:
                        App.mood = "favorites";
                        break;
                }


            });
           
            



        }

        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (Capture));
        }
    }
}
