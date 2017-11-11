using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using JiHuangBaiKeForUWP.Model;
using JiHuangBaiKeForUWP.View;
using LogicalTree = Microsoft.Toolkit.Uwp.UI.Extensions.LogicalTree;

// 对话框示例
// var contentDialog = new ContentDialog()
// {
//     Title = "测试",
//     Content = "加载当前页面错误，可能需要翻墙(～￣▽￣)～",
//     PrimaryButtonText = "确定",
//     FullSizeDesired = false,
// };
// Global.ShowDialog(contentDialog);

namespace JiHuangBaiKeForUWP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region 构造事件

        public MainPage()
        {
            InitializeComponent();
            if (Width <= 641)
            {
                AutoSuggestButton.Visibility = Visibility.Visible;
                SearchAutoSuggestBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                AutoSuggestButton.Visibility = Visibility.Collapsed;
                SearchAutoSuggestBox.Visibility = Visibility.Visible;
            }
            Global.RootGrid = RootGrid;
            Global.FrameTitle = FrameTitle;
            Global.RootFrame = RootFrame;
            Global.IconsListViewGameData = IconsListViewGameData;
            Global.IconsListViewSettingAndAbout = IconsListViewSettingAndAbout;
            // 设置Frame标题Margin属性
            SetFrameTitleMargin();
            // 汉堡菜单边框
            HamburgerGrid.BorderBrush = new SolidColorBrush(Global.AccentColor);
            // 默认页
            RootFrame.SourcePageType = typeof(CharacterPage);
            // 设置SearchAutoSuggestBox的ItemSource属性
            SearchAutoSuggestBox.ItemsSource = Global.AutoSuggestBoxItem;
            // 设置主题
            if (Global.GetOsVersion() >= 16299)
            {
                ((Frame)Window.Current.Content).RequestedTheme = ElementTheme.Dark;
            }
            else
            {
                ((Frame)Window.Current.Content).RequestedTheme = SettingSet.ThemeSettingRead() ? ElementTheme.Dark : ElementTheme.Light;
            }
        }

        #endregion

        #region 设置背景色跟随系统背景色

        private UISettings _uisetting;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _uisetting = new UISettings();
            _uisetting.ColorValuesChanged += OnColorValuesChanged;
            var bgcolor = _uisetting.GetColorValue(UIColorType.Accent);
            RootGrid.Background = new SolidColorBrush(bgcolor);

            var searchItem = (string)e.Parameter;
            if (string.IsNullOrEmpty(searchItem) == false && searchItem != "...")
                VoiceSearch(searchItem);

            //使用Fluent Design System
            if (Global.GetOsVersion() >= 16299)
            {
                //标题栏
                CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                titleBar.ButtonBackgroundColor = Colors.Transparent;
                titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
                titleBar.ButtonHoverBackgroundColor = Colors.Gray;

                RootRelativePanelAcrylic.Visibility = Visibility.Visible;
                RootRelativePanel.Visibility = Visibility.Collapsed;

                if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.XamlCompositionBrushBase"))
                {
                    var dimGrayAcrylicBrush = new AcrylicBrush
                    {
                        BackgroundSource = AcrylicBackgroundSource.HostBackdrop,
                        FallbackColor = Colors.Transparent,
                        TintColor = Color.FromArgb(255, 105, 105, 105),
                        TintOpacity = 0.3
                    };
                    RootRelativePanelAcrylic.Background = dimGrayAcrylicBrush;
                    RootSplit.PaneBackground = dimGrayAcrylicBrush;
                    RootSplit.Background = dimGrayAcrylicBrush;
                    IconsListViewGameData.Background = dimGrayAcrylicBrush;
                    IconsListViewGameData.BorderThickness = new Thickness(0);
                    IconsListViewSettingAndAbout.Background = dimGrayAcrylicBrush;
                    AutoSuggestGrid.Background = null;

                    //汉堡菜单Reveal
                    var buttonRevealStyle = (Style)Application.Current.Resources["ButtonRevealStyle"];
                    HamburgerButtonAcrylic.Style = buttonRevealStyle;

                    var listViewItemRevealStyle = (Style)Application.Current.Resources["ListViewItemRevealStyle"];
                    IconsListViewGameData.ItemContainerStyle = listViewItemRevealStyle;
                    IconsListViewSettingAndAbout.ItemContainerStyle = listViewItemRevealStyle;
                }
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (_uisetting != null)
            {
                _uisetting.ColorValuesChanged -= OnColorValuesChanged;
            }
        }

        private async void OnColorValuesChanged(UISettings sender, object args)
        {
            Color bg;
            bg = sender.GetColorValue(UIColorType.Accent);
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    var brush = RootGrid.Background as SolidColorBrush ?? new SolidColorBrush();
                    brush.Color = bg;
                });
        }

        #endregion

        #region 汉堡菜单
        private readonly ObservableCollection<HamburgerMenuItem> _gameDataHamburgerMenuItem = new ObservableCollection<HamburgerMenuItem>(
            new[]
            {
                new HamburgerMenuItem()
                {
                    Icon = "\x4E0C",
                    Text = "人物",
                    Color = new SolidColorBrush(Global.AccentColor),
                    Selected = Visibility.Visible,
                    NavigatePage = typeof(CharacterPage)
                },
                new HamburgerMenuItem()
                {
                    Icon = "\x4E01",
                    Text = "食物",
                    Color = new SolidColorBrush(Colors.White),
                    Selected = Visibility.Collapsed,
                    NavigatePage = typeof(FoodPage)
                },
                new HamburgerMenuItem()
                {
                    Icon = "\x4E02",
                    Text = "模拟",
                    Color = new SolidColorBrush(Colors.White),
                    Selected = Visibility.Collapsed,
                    NavigatePage = typeof(CookingSimulatorPage)
                },
                new HamburgerMenuItem()
                {
                    Icon = "\x4E03",
                    Text = "科技",
                    Color = new SolidColorBrush(Colors.White),
                    Selected = Visibility.Collapsed,
                    NavigatePage = typeof(SciencePage)
                },
                new HamburgerMenuItem()
                {
                    Icon = "\x4E04",
                    Text = "生物",
                    Color = new SolidColorBrush(Colors.White),
                    Selected = Visibility.Collapsed,
                    NavigatePage = typeof(CreaturePage)
                },
                new HamburgerMenuItem()
                {
                    Icon = "\x4E05",
                    Text = "自然",
                    Color = new SolidColorBrush(Colors.White),
                    Selected = Visibility.Collapsed,
                    NavigatePage = typeof(NaturalPage)
                },
                new HamburgerMenuItem()
                {
                    Icon = "\x4E06",
                    Text = "物品",
                    Color = new SolidColorBrush(Colors.White),
                    Selected = Visibility.Collapsed,
                    NavigatePage = typeof(GoodPage)
                }
            });

        private readonly ObservableCollection<HamburgerMenuItem> _settingAndAboutHamburgerMenuItem = new ObservableCollection<HamburgerMenuItem>(
            new[]
            {
                new HamburgerMenuItem()
                {
                    Icon = "\x4E0A",
                    Text = "设置",
                    Color = new SolidColorBrush(Colors.White),
                    Selected = Visibility.Collapsed,
                    NavigatePage = typeof(SettingPage)
                },
                new HamburgerMenuItem()
                {
                    Icon = "\x4E0B",
                    Text = "关于",
                    Color = new SolidColorBrush(Colors.White),
                    Selected = Visibility.Collapsed,
                    NavigatePage = typeof(AboutPage)
                }
            });

        /// <summary>
        /// 汉堡菜单点击
        /// </summary>
        private void HamburgerMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Rectangle显示并导航
            if (e.ClickedItem is HamburgerMenuItem hamburgerMenuItem)
            {
                HamburgerMenu_ItemSelect(hamburgerMenuItem);
            }

            if (Window.Current.Bounds.Width < 1008)
            {
                RootSplit.IsPaneOpen = false;
            }
        }

        /// <summary>
        /// hamburgerMenuItem选择事件
        /// </summary>
        /// <param name="hamburgerMenuItem">HamburgerMenuItem</param>
        public void HamburgerMenu_ItemSelect(HamburgerMenuItem hamburgerMenuItem)
        {
            // 遍历，将选中Rectangle隐藏
            foreach (var gameDataHamburgerMenuItem in _gameDataHamburgerMenuItem)
            {
                gameDataHamburgerMenuItem.Color = new SolidColorBrush(Colors.White);
                gameDataHamburgerMenuItem.Selected = Visibility.Collapsed;
            }
            foreach (var settingAndAboutHamburgerMenuItem in _settingAndAboutHamburgerMenuItem)
            {
                settingAndAboutHamburgerMenuItem.Color = new SolidColorBrush(Colors.White);
                settingAndAboutHamburgerMenuItem.Selected = Visibility.Collapsed;
            }

            hamburgerMenuItem.Selected = Visibility.Visible;
            hamburgerMenuItem.Color = new SolidColorBrush(Global.AccentColor);

            if (hamburgerMenuItem.NavigatePage != null)
            {
                RootFrame.Navigate(hamburgerMenuItem.NavigatePage);
            }
        }

        /// <summary>
        /// 汉堡菜单按钮触摸事件
        /// </summary>
        private void HamburgerButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            RootSplit.IsPaneOpen = !RootSplit.IsPaneOpen;
            if (RootSplit.IsPaneOpen == false)
            {
                AutoSuggestButton.Visibility = Visibility.Visible;
                SearchAutoSuggestBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                AutoSuggestButton.Visibility = Visibility.Collapsed;
                SearchAutoSuggestBox.Visibility = Visibility.Visible;
            }
            SetFrameTitleMargin();
        }

        /// <summary>
        /// 触摸搜索按钮展开Pane并显示搜索框
        /// </summary>
        private void AutoSuggestButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            RootSplit.IsPaneOpen = !RootSplit.IsPaneOpen;
            AutoSuggestButton.Visibility = Visibility.Collapsed;
            SearchAutoSuggestBox.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Pane关闭时设置隐藏搜索框显示搜索按钮
        /// </summary>
        private void RootSplit_PaneClosing(SplitView sender, object args)
        {
            AutoSuggestButton.Visibility = Visibility.Visible;
            SearchAutoSuggestBox.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// 设置Frame框架标题Margin属性
        /// </summary>
        private void SetFrameTitleMargin()
        {
            if (Global.GetOsVersion() >= 16299)
            {
                if (RootSplit.DisplayMode == SplitViewDisplayMode.CompactInline)
                {
                    FrameTitleAcrylic.Margin = RootSplit.IsPaneOpen ? new Thickness(180, 0, 0, 0) : new Thickness(10, 0, 0, 0);
                }
                else
                {
                    FrameTitleAcrylic.Margin = new Thickness(10, 0, 0, 0);
                }
            }
            else
            {
                if (RootSplit.DisplayMode == SplitViewDisplayMode.CompactInline)
                {
                    FrameTitle.Margin = RootSplit.IsPaneOpen ? new Thickness(180, 0, 0, 0) : new Thickness(10, 0, 0, 0);
                }
                else
                {
                    FrameTitle.Margin = new Thickness(10, 0, 0, 0);
                }
            }
        }

        #endregion

        #region 搜索框

        /// <summary>
        /// 搜索框内容改变事件
        /// </summary>
        private void SearchAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            Global.AutoSuggestBoxItem.Clear();
            foreach (var item in Global.AutoSuggestBoxItemSource)
            {
                Global.AutoSuggestBoxItem.Add(item);
            }
            var str = sender.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(str)) return;
            for (var i = Global.AutoSuggestBoxItem.Count - 1; i >= 0; i--)
            {
                if (Global.AutoSuggestBoxItem[i].Name.ToLower().IndexOf(str, StringComparison.Ordinal) < 0 && Global.AutoSuggestBoxItem[i].EnName.ToLower().IndexOf(str, StringComparison.Ordinal) < 0)
                {
                    Global.AutoSuggestBoxItem.Remove(Global.AutoSuggestBoxItem[i]);
                }
            }
        }

        /// <summary>
        /// 搜索框查询提交事件
        /// </summary>
        private void SearchAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            Global.AutoSuggestBoxItem.Clear();
            foreach (var item in Global.AutoSuggestBoxItemSource)
            {
                Global.AutoSuggestBoxItem.Add(item);
            }
            var str = sender.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(str)) return;
            for (var i = Global.AutoSuggestBoxItem.Count - 1; i >= 0; i--)
            {
                if (Global.AutoSuggestBoxItem[i].Name.ToLower().IndexOf(str, StringComparison.Ordinal) < 0 && Global.AutoSuggestBoxItem[i].EnName.ToLower().IndexOf(str, StringComparison.Ordinal) < 0)
                {
                    Global.AutoSuggestBoxItem.Remove(Global.AutoSuggestBoxItem[i]);
                }
            }
            if (sender.Items != null && sender.Items.Count != 0 && args.ChosenSuggestion == null)
            {
                if (Global.AutoSuggestBoxItem.Count > 1)
                {
                    SearchAutoSuggestBox.Focus(FocusState.Programmatic);
                }
                else
                {
                    var suggestBoxItem = sender.Items[0] as SuggestBoxItem;
                    if (suggestBoxItem == null) return;
                    var extraData = new[] { suggestBoxItem.SourcePath, suggestBoxItem.Picture, suggestBoxItem.Category };
                    AutoSuggestNavigate(extraData);
                    SearchAutoSuggestBox.Text = suggestBoxItem.Name;
                }
            }
        }

        /// <summary>
        /// 搜索框选择事件
        /// </summary>
        private void SearchAutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var suggestBoxItem = args.SelectedItem as SuggestBoxItem;
            if (suggestBoxItem == null) return;
            var extraData = new[] { suggestBoxItem.SourcePath, suggestBoxItem.Picture, suggestBoxItem.Category };
            AutoSuggestNavigate(extraData);
            SearchAutoSuggestBox.Text = suggestBoxItem.Name;
        }

        /// <summary>
        /// 语音搜索
        /// </summary>
        /// <param name="searchItem">搜索内容</param>
        public async void VoiceSearch(string searchItem)
        {
            SuggestBoxItem suggestBoxItem = null;
            await Global.SetAutoSuggestBoxItem();
            var searchItemToLower = searchItem.Trim().ToLower();
            if (string.IsNullOrEmpty(searchItemToLower)) return;
            for (var i = Global.AutoSuggestBoxItem.Count - 1; i >= 0; i--)
            {
                if (Global.AutoSuggestBoxItem[i].Name.ToLower().IndexOf(searchItemToLower, StringComparison.Ordinal) < 0 && Global.AutoSuggestBoxItem[i].EnName.ToLower().IndexOf(searchItemToLower, StringComparison.Ordinal) < 0)
                {
                    Global.AutoSuggestBoxItem.Remove(Global.AutoSuggestBoxItem[i]);
                }
            }
            if (Global.AutoSuggestBoxItem.Count == 0)
            {
                SearchAutoSuggestBox.Text = "";
                var dialog = new ContentDialog()
                {
                    Title = "搜索错误！",
                    Content = "未找到“" + searchItem + "”",
                    PrimaryButtonText = "确定",
                    FullSizeDesired = false,
                };
                Global.ShowDialog(dialog);
            }
            else if (Global.AutoSuggestBoxItem.Count == 1)
            {
                suggestBoxItem = Global.AutoSuggestBoxItem[0];
                SearchAutoSuggestBox.Text = searchItem;
            }
            else if (Global.AutoSuggestBoxItem.Count > 1)
            {
                SearchAutoSuggestBox.Text = searchItem;
                SearchAutoSuggestBox.Focus(FocusState.Programmatic);
            }
            if (suggestBoxItem == null) return;
            var extraData = new[] { suggestBoxItem.SourcePath, suggestBoxItem.Picture, suggestBoxItem.Category };
            AutoSuggestNavigate(extraData);
        }

        /// <summary>
        /// 自动搜索框导航
        /// </summary>
        /// <param name="extraData">额外数据</param>
        private void AutoSuggestNavigate(string[] extraData)
        {
            switch (extraData[2])
            {
                case "人物":
                    FrameTitle.Text = "人物";
                    HamburgerMenu_ItemSelect(_gameDataHamburgerMenuItem[0]);
                    RootFrame.Navigate(typeof(CharacterPage), extraData);
                    break;
                case "食物":
                    FrameTitle.Text = "食物";
                    HamburgerMenu_ItemSelect(_gameDataHamburgerMenuItem[1]);
                    RootFrame.Navigate(typeof(FoodPage), extraData);
                    break;
                case "科技":
                    FrameTitle.Text = "科技";
                    HamburgerMenu_ItemSelect(_gameDataHamburgerMenuItem[3]);
                    RootFrame.Navigate(typeof(SciencePage), extraData);
                    break;
                case "生物":
                    FrameTitle.Text = "生物";
                    HamburgerMenu_ItemSelect(_gameDataHamburgerMenuItem[4]);
                    RootFrame.Navigate(typeof(CreaturePage), extraData);
                    break;
                case "自然":
                    FrameTitle.Text = "自然";
                    HamburgerMenu_ItemSelect(_gameDataHamburgerMenuItem[5]);
                    RootFrame.Navigate(typeof(NaturalPage), extraData);
                    break;
                case "物品":
                    FrameTitle.Text = "物品";
                    HamburgerMenu_ItemSelect(_gameDataHamburgerMenuItem[6]);
                    RootFrame.Navigate(typeof(GoodPage), extraData);
                    break;
            }
        }
        #endregion

    }
}
