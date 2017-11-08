using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace JiHuangBaiKeForUWP.View
{
    public sealed partial class AboutPage : Page
    {
        public AboutPage()
        {
            this.InitializeComponent();
            var version = Windows.ApplicationModel.Package.Current.Id.Version;
            VersionTextBlock.Text = $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
            PublisherTextBlock.Text = Windows.ApplicationModel.Package.Current.PublisherDisplayName;
        }

        private void ZfbRadioButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Donation1Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Pic/QRCode_zfb1.png"));
            Donation2Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Pic/QRCode_zfb2.png"));
            Donation5Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Pic/QRCode_zfb5.png"));
        }

        private void WxRadioButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Donation1Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Pic/QRCode_wx1.png"));
            Donation2Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Pic/QRCode_wx2.png"));
            Donation5Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Pic/QRCode_wx5.png"));
        }
    }
}
