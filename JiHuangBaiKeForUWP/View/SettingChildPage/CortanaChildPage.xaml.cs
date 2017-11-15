using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using JiHuangBaiKeForUWP.Model;

namespace JiHuangBaiKeForUWP.View.SettingChildPage
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class CortanaChildPage : Page
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
                RootStackPanel.Background = dimGrayAcrylicBrush;
            }
        }

        public CortanaChildPage()
        {
            this.InitializeComponent();
        }
    }
}
