using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using JiHuangBaiKeForUWP.Model;

namespace JiHuangBaiKeForUWP.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SettingPage : Page
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
                RootGrid.Background = dimGrayAcrylicBrush;
                TopListBox.Background = darkSlateGrayAcrylicBrush;
            }
        }

        #region 构造器

        public SettingPage()
        {
            this.InitializeComponent();
            RootFrame.SourcePageType = typeof(SettingChildPage.SettingChildPage);
            Global.SettingPageRootGrid = RootGrid;
        }
        #endregion

        int lastSelectedMenuItem = 0;

        private void ListBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var listBoxItem = (ListBoxItem)((ListBox)sender).SelectedItem;

            var SelectedItemIndex = ((ListBox)sender).SelectedIndex;

            if (SelectedItemIndex < lastSelectedMenuItem)
            {
                ContentThemeTransition.HorizontalOffset = (double)Global.ContentThemeTransitionShift.LeftOrUpShift;
            }
            else if (SelectedItemIndex == lastSelectedMenuItem)
            {
                ContentThemeTransition.HorizontalOffset = (double)Global.ContentThemeTransitionShift.NoneShift;
            }
            else
            {
                ContentThemeTransition.HorizontalOffset = (double)Global.ContentThemeTransitionShift.RightOrDownShift;
            }
            lastSelectedMenuItem = SelectedItemIndex;

            if (listBoxItem != null)
            {
                var listBoxItemName = listBoxItem.Name;

                switch (listBoxItemName)
                {
                    case "SettingBoxItem":
                        RootFrame.Navigate(typeof(SettingChildPage.SettingChildPage));
                        break;
                    case "CortanaBoxItem":
                        RootFrame.Navigate(typeof(SettingChildPage.CortanaChildPage));
                        break;
                    case "FeedbackBoxItem":
                        RootFrame.Navigate(typeof(SettingChildPage.FeedbackChildPage));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
