using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace Musync.Controls
{
    public class EditableTextBox : TextBox
    {
        public EditableTextBox()
        {
            this.BorderBrush = new SolidColorBrush(Colors.Black);
        }
        protected override void OnTapped(TappedRoutedEventArgs e)
        {
            this.IsReadOnly = false;
            SetEditingStyle();
            base.OnTapped(e);
        }

        protected override void OnDoubleTapped(DoubleTappedRoutedEventArgs e)
        {
            this.IsReadOnly = false;
            SetEditingStyle();
            base.OnDoubleTapped(e);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            this.IsReadOnly = true;
            SetReadonlyStyle();
            base.OnLostFocus(e);
        }

       
        public void SetReadonlyStyle()
        {
            this.BorderBrush.Opacity = 0;
            this.Background.Opacity = 0;
        }

        public void SetEditingStyle()
        {
            this.BorderBrush.Opacity = 0.5;
            this.Background.Opacity = 0.5;

        }
    }
}