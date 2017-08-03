using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace JiHuangBaiKeForUWP.UserControls
{
    public sealed partial class PicButton : UserControl
    {
        #region 属性：Source

        public string Source
        {
            get => (string)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(string), typeof(PicButton), new PropertyMetadata(false, OnSourceChang));

        private static void OnSourceChang(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;
            var picButton = (PicButton)d;
            try
            {
                picButton.PictureButtonImage.Source = new BitmapImage(new Uri((string)e.NewValue));
            }
            catch
            {
                picButton.PictureButtonImage.Source = null;
            }
        }

        #endregion

        #region 属性：Text

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(PicButton), new PropertyMetadata(false, OnTextChang));

        private static void OnTextChang(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;
            var picButton = (PicButton)d;
            if ((string) e.NewValue == null)
            {
                picButton.PictureButtonTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                picButton.PictureButtonTextBlock.Text = (string)e.NewValue;
            }
        }

        #endregion
        public PicButton()
        {
            this.InitializeComponent();
        }
    }
}
