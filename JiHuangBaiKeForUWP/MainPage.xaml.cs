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
            //订阅后退按钮事件
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
            //Global类里的一些设定
            Global._gameDataHamburgerMenuItem = _gameDataHamburgerMenuItem;
            Global.RootGrid = RootGrid;
            Global.FrameTitle = Global.GetOsVersion() >= 16299 ? FrameTitleAcrylic : FrameTitle;
            Global.AutoSuggestGrid = AutoSuggestGrid;
            Global.RootFrame = RootFrame;
            Global.IconsListViewGameData = IconsListViewGameData;
            Global.IconsListViewSettingAndAbout = IconsListViewSettingAndAbout;
            // 设置Frame标题Margin属性
            SetFrameTitleMargin();
            // 汉堡菜单边框
            //HamburgerGrid.BorderBrush = new SolidColorBrush(Global.AccentColor);
            //亚克力背景颜色及透明度设置读取
            Global.TinkOpacity = SettingSet.AcrylicOpacitySettingRead();
            Global.TinkColor = StringProcess.StringToColor(SettingSet.AcrylicColorSettingRead());
            // 默认页
            RootFrame.SourcePageType = typeof(CharacterPage);
            Global.PageStack.Push(new PageStackItem { SourcePageType = typeof(CharacterPage) });
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
                //汉堡菜单按钮和页面标题面板
                RootSplit.OpenPaneLength = 240;
                RootRelativePanelAcrylic.Visibility = Visibility.Visible;
                RootRelativePanel.Visibility = Visibility.Collapsed;

                if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.XamlCompositionBrushBase"))
                {
                    //dimGrayAcrylicBrush笔刷
                    var dimGrayAcrylicBrush = new AcrylicBrush
                    {
                        BackgroundSource = AcrylicBackgroundSource.HostBackdrop,
                        FallbackColor = Colors.Transparent,
                        TintColor = Global.TinkColor,
                        TintOpacity = Global.TinkOpacity
                    };
                    RootRelativePanelAcrylic.Background = dimGrayAcrylicBrush;
                    RootSplit.PaneBackground = dimGrayAcrylicBrush;
                    RootSplit.Background = dimGrayAcrylicBrush;
                    HamburgerGrid.BorderThickness = new Thickness(0);
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
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
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
                new HamburgerMenuItem
                {
                    Icon = "\x4E0C",
                    Text = "人物",
                    Color = new SolidColorBrush(Global.AccentColor),
                    Selected = Visibility.Visible,
                    NavigatePage = typeof(CharacterPage)
                },
                new HamburgerMenuItem
                {
                    Icon = "\x4E01",
                    Text = "食物",
                    Color = new SolidColorBrush(Colors.White),
                    Selected = Visibility.Collapsed,
                    NavigatePage = typeof(FoodPage)
                },
                new HamburgerMenuItem
                {
                    Icon = "\x4E02",
                    Text = "模拟",
                    Color = new SolidColorBrush(Colors.White),
                    Selected = Visibility.Collapsed,
                    NavigatePage = typeof(CookingSimulatorPage)
                },
                new HamburgerMenuItem
                {
                    Icon = "\x4E03",
                    Text = "科技",
                    Color = new SolidColorBrush(Colors.White),
                    Selected = Visibility.Collapsed,
                    NavigatePage = typeof(SciencePage)
                },
                new HamburgerMenuItem
                {
                    Icon = "\x4E04",
                    Text = "生物",
                    Color = new SolidColorBrush(Colors.White),
                    Selected = Visibility.Collapsed,
                    NavigatePage = typeof(CreaturePage)
                },
                new HamburgerMenuItem
                {
                    Icon = "\x4E05",
                    Text = "自然",
                    Color = new SolidColorBrush(Colors.White),
                    Selected = Visibility.Collapsed,
                    NavigatePage = typeof(NaturalPage)
                },
                new HamburgerMenuItem
                {
                    Icon = "\x4E06",
                    Text = "物品",
                    Color = new SolidColorBrush(Colors.White),
                    Selected = Visibility.Collapsed,
                    NavigatePage = typeof(GoodPage)
                },
                new HamburgerMenuItem
                {
                    Icon = "\x4E0E",
                    Text = "皮肤",
                    Color = new SolidColorBrush(Colors.White),
                    Selected = Visibility.Collapsed,
                    NavigatePage = typeof(SkinsPage)
                }
            });

        private readonly ObservableCollection<HamburgerMenuItem> _settingAndAboutHamburgerMenuItem = new ObservableCollection<HamburgerMenuItem>(
            new[]
            {
                new HamburgerMenuItem
                {
                    Icon = "\x4E0A",
                    Text = "设置",
                    Color = new SolidColorBrush(Colors.White),
                    Selected = Visibility.Collapsed,
                    NavigatePage = typeof(SettingPage)
                },
                new HamburgerMenuItem
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
                Global.PageStack.Push(new PageStackItem { SourcePageType = hamburgerMenuItem.NavigatePage });
                Global.FrameTitle.Text = hamburgerMenuItem.Text;
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
                    var suggestBoxItem = (SuggestBoxItem)sender.Items[0];
                    if (suggestBoxItem != null)
                    {
                        AutoSuggestNavigate(new SearchExtraData
                        {
                            SourcePath = suggestBoxItem.SourcePath,
                            Picture = suggestBoxItem.Picture,
                            Category = suggestBoxItem.Category
                        });
                        SearchAutoSuggestBox.Text = suggestBoxItem.Name;
                    }
                }
            }
        }

        /// <summary>
        /// 搜索框选择事件
        /// </summary>
        private void SearchAutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var suggestBoxItem = (SuggestBoxItem)args.SelectedItem;
            if (suggestBoxItem != null)
            {
                AutoSuggestNavigate(new SearchExtraData
                {
                    SourcePath = suggestBoxItem.SourcePath,
                    Picture = suggestBoxItem.Picture,
                    Category = suggestBoxItem.Category
                });
                SearchAutoSuggestBox.Text = "";
            }
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
                var dialog = new ContentDialog
                {
                    Title = "搜索错误！",
                    Content = "未找到“" + searchItem + "”",
                    PrimaryButtonText = "确定",
                    FullSizeDesired = false
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
            // 搜索结果唯一时suggestBoxItem获得数据
            if (suggestBoxItem != null)
            {
                AutoSuggestNavigate(new SearchExtraData
                {
                    SourcePath = suggestBoxItem.SourcePath,
                    Picture = suggestBoxItem.Picture,
                    Category = suggestBoxItem.Category
                });
            }
        }

        /// <summary>
        /// 自动搜索框导航
        /// </summary>
        /// <param name="extraData">额外数据</param>
        private void AutoSuggestNavigate(SearchExtraData extraData)
        {
            var viewExtraData = new ViewExtraData { Classify = extraData.SourcePath, Picture = extraData.Picture };
            switch (extraData.Category)
            {
                case "人物":
                    Global.FrameTitle.Text = "人物";
                    HamburgerMenu_ItemSelect(_gameDataHamburgerMenuItem[0]);
                    RootFrame.Navigate(typeof(CharacterPage), viewExtraData);
                    Global.PageStack.Push(new PageStackItem { SourcePageType = typeof(CharacterPage), Parameter = viewExtraData });
                    break;
                case "食物":
                    Global.FrameTitle.Text = "食物";
                    HamburgerMenu_ItemSelect(_gameDataHamburgerMenuItem[1]);
                    RootFrame.Navigate(typeof(FoodPage), viewExtraData);
                    Global.PageStack.Push(new PageStackItem { SourcePageType = typeof(FoodPage), Parameter = viewExtraData });
                    break;
                case "科技":
                    Global.FrameTitle.Text = "科技";
                    HamburgerMenu_ItemSelect(_gameDataHamburgerMenuItem[3]);
                    RootFrame.Navigate(typeof(SciencePage), viewExtraData);
                    Global.PageStack.Push(new PageStackItem { SourcePageType = typeof(SciencePage), Parameter = viewExtraData });
                    break;
                case "生物":
                    Global.FrameTitle.Text = "生物";
                    HamburgerMenu_ItemSelect(_gameDataHamburgerMenuItem[4]);
                    RootFrame.Navigate(typeof(CreaturePage), viewExtraData);
                    Global.PageStack.Push(new PageStackItem { SourcePageType = typeof(CreaturePage), Parameter = viewExtraData });
                    break;
                case "自然":
                    Global.FrameTitle.Text = "自然";
                    HamburgerMenu_ItemSelect(_gameDataHamburgerMenuItem[5]);
                    RootFrame.Navigate(typeof(NaturalPage), viewExtraData);
                    Global.PageStack.Push(new PageStackItem { SourcePageType = typeof(NaturalPage), Parameter = viewExtraData });
                    break;
                case "物品":
                    Global.FrameTitle.Text = "物品";
                    HamburgerMenu_ItemSelect(_gameDataHamburgerMenuItem[6]);
                    RootFrame.Navigate(typeof(GoodPage), viewExtraData);
                    Global.PageStack.Push(new PageStackItem { SourcePageType = typeof(GoodPage), Parameter = viewExtraData });
                    break;
                case "皮肤":
                    Global.FrameTitle.Text = "皮肤";
                    HamburgerMenu_ItemSelect(_gameDataHamburgerMenuItem[7]);
                    RootFrame.Navigate(typeof(SkinsPage), viewExtraData);
                    Global.PageStack.Push(new PageStackItem { SourcePageType = typeof(SkinsPage), Parameter = viewExtraData });
                    break;
            }
        }
        #endregion

        #region 后退按钮


        /// <summary>
        /// 后退按钮请求处理
        /// </summary>
        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (Global.PageStack.Count > 1)
            {
                var pageStackItemUseless = Global.PageStack.Pop();
                var pageStackItem = Global.PageStack.Peek();
                RootFrame.Navigate(pageStackItem.SourcePageType, pageStackItem.Parameter);
                switch (pageStackItem.SourcePageType.ToString())
                {
                    case "JiHuangBaiKeForUWP.View.CharacterPage":
                        HamburgerMenu_ItemSelect_NoPush(_gameDataHamburgerMenuItem[0]);
                        break;
                    case "JiHuangBaiKeForUWP.View.FoodPage":
                        HamburgerMenu_ItemSelect_NoPush(_gameDataHamburgerMenuItem[1]);
                        break;
                    case "JiHuangBaiKeForUWP.View.CookingSimulatorPage":
                        HamburgerMenu_ItemSelect_NoPush(_gameDataHamburgerMenuItem[2]);
                        break;
                    case "JiHuangBaiKeForUWP.View.SciencePage":
                        HamburgerMenu_ItemSelect_NoPush(_gameDataHamburgerMenuItem[3]);
                        break;
                    case "JiHuangBaiKeForUWP.View.CreaturePage":
                        HamburgerMenu_ItemSelect_NoPush(_gameDataHamburgerMenuItem[4]);
                        break;
                    case "JiHuangBaiKeForUWP.View.NaturalPage":
                        HamburgerMenu_ItemSelect_NoPush(_gameDataHamburgerMenuItem[5]);
                        break;
                    case "JiHuangBaiKeForUWP.View.GoodPage":
                        HamburgerMenu_ItemSelect_NoPush(_gameDataHamburgerMenuItem[6]);
                        break;
                    case "JiHuangBaiKeForUWP.View.SkinsPage":
                        HamburgerMenu_ItemSelect_NoPush(_gameDataHamburgerMenuItem[7]);
                        break;
                    case "JiHuangBaiKeForUWP.View.SettingPage":
                        HamburgerMenu_ItemSelect_NoPush(_settingAndAboutHamburgerMenuItem[0]);
                        break;
                    case "JiHuangBaiKeForUWP.View.AboutPage":
                        HamburgerMenu_ItemSelect_NoPush(_settingAndAboutHamburgerMenuItem[1]);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 返回时修改Hamburger菜单的选中状态
        /// </summary>
        public void HamburgerMenu_ItemSelect_NoPush(HamburgerMenuItem hamburgerMenuItem)
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
                Global.FrameTitle.Text = hamburgerMenuItem.Text;
            }
        }
        #endregion



        #region Frame加载
        /// <summary>
        /// Frame导航结束
        /// </summary>
        private void RootFrame_Navigated(object sender, NavigationEventArgs e)
        {
            //RootFrame.ContentTransitions = null;

            //SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = Global.PageStack.Count > 1 ?
            //    AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
            LoadingControl.IsLoading = false;
            RootFrame.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Frame导航中
        /// </summary>
        private void RootFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            LoadingControl.IsLoading = true;
            RootFrame.Visibility = Visibility.Collapsed;

        }
        #endregion
    }
}
