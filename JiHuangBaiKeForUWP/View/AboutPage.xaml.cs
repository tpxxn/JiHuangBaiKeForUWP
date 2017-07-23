using Windows.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace JiHuangBaiKeForUWP.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AboutPage : Page
    {
        public AboutPage()
        {
            this.InitializeComponent();
            var version = Windows.ApplicationModel.Package.Current.Id.Version;
            VersionTextBlock.Text = $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
            PublisherTextBlock.Text = Windows.ApplicationModel.Package.Current.PublisherDisplayName;
        }

    }
}
