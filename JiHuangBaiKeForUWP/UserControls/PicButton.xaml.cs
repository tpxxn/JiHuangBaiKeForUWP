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

        #region 属性：PictureSize

        public double PictureSize
        {
            get => (double)GetValue(PictureSizeProperty);
            set => SetValue(PictureSizeProperty, value);
        }

        public static readonly DependencyProperty PictureSizeProperty =
            DependencyProperty.Register("PictureSize", typeof(double), typeof(PicButton), new PropertyMetadata(false, OnPictureSizeChang));

        private static void OnPictureSizeChang(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;
            var picButton = (PicButton)d;
            if ((double) e.NewValue > 0)
            {
                picButton.PictureButton.Height = (double) e.NewValue;
                picButton.PictureButton.Width = (double) e.NewValue;
                picButton.PictureButtonImage.Height = (double) e.NewValue - 3;
                picButton.PictureButtonImage.Width = (double)e.NewValue - 3;

            }
            else
            {
                picButton.PictureButton.Height = 45;
                picButton.PictureButton.Width = 45;
                picButton.PictureButtonImage.Height = 42;
                picButton.PictureButtonImage.Width = 42;
            }
        }

        #endregion

        public PicButton()
        {
            this.InitializeComponent();
        }
    }
}
