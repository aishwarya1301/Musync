using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using BackgroundAudioShared;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Musync
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Me : Page
    {
        public Me()
        {
            this.InitializeComponent();
            this.Loaded += Me_Loaded;
        }
        

        public  void SetUserFName(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            App.SCUser.first_name = txtFirstname.Text;
          
        }
        public void SetUserLName(FrameworkElement sender, DataContextChangedEventArgs args)
        {

            App.SCUser.last_name = txtlastname.Text;


        }
        public void SetUserWeb(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            
            App.SCUser.website = txtWebsite.Text ;
           

        }
        public void SetUserCity(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            
            App.SCUser.city = txtCity.Text ?? " ";
            

        }
        public void SetUserCountry(FrameworkElement sender, DataContextChangedEventArgs args)
        {
           
            App.SCUser.country = txtCountry.Text ?? " ";

        }
        private void Me_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.SCUser != null)
            {
                txtFirstname.Text = Convert.ToString(App.SCUser.first_name);
                txtlastname.Text = Convert.ToString(App.SCUser.last_name);
                txtWebsite.Text = Convert.ToString(App.SCUser.website);
                txtCity.Text = Convert.ToString(App.SCUser.city);
                txtCountry.Text = Convert.ToString(App.SCUser.country);
               if(!string.IsNullOrEmpty(App.SCUser.avatar_url))
                profilePhoto.ImageSource = new BitmapImage(new Uri(App.SCUser?.avatar_url));

            }
        }
    }
}
