using Windows.UI.Xaml.Controls;

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
    }
}
