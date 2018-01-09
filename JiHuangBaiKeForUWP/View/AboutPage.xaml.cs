using System;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using JiHuangBaiKeForUWP.Model;

namespace JiHuangBaiKeForUWP.View
{
    public sealed partial class AboutPage : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Global.GetOsVersion() >= 16299)
            {
                var dimGrayAcrylicBrush = new AcrylicBrush
                {
                    BackgroundSource = AcrylicBackgroundSource.HostBackdrop,
                    FallbackColor = Colors.Transparent,
                    TintColor = Global.TinkColor,
                    TintOpacity = Global.TinkOpacity
                };
                var darkSlateGrayAcrylicBrush = new AcrylicBrush
                {
                    BackgroundSource = AcrylicBackgroundSource.HostBackdrop,
                    FallbackColor = Colors.Transparent,
                    TintColor = Color.FromArgb(255, 47, 79, 79),
                    TintOpacity = 0.5
                };
                RootStackPanel.Background = dimGrayAcrylicBrush;
                HeaderStackPanel.Background = darkSlateGrayAcrylicBrush;
                ContentStackPanel.Background = dimGrayAcrylicBrush;
            }
        }

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

        private async void ReviewButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var uri = new Uri("ms-windows-store://review/?productid=9n91997fd3h3");
            await Launcher.LaunchUriAsync(uri);
        }

        private void UIElement_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var finalPageStack = "";
            foreach (var pageStackItem in Global.PageStack)
            {
                if (pageStackItem.Object != null)
                {
                    finalPageStack += pageStackItem.TypeName + " " + pageStackItem.Object + "\r\n";
                }
                else
                {
                    finalPageStack += pageStackItem.TypeName + "\r\n";
                }
            }
            //PageStackLog.Text = "页面堆栈日志：\r\n" + Global.PageStackLog + "\r\n最终页面堆栈：\r\n" + finalPageStack;
        }
    }
}
