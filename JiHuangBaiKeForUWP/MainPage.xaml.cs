using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

        #region 字段成员

        private static readonly Color AccentColor = (Color)Application.Current.Resources["SystemAccentColor"];
        private readonly List<ListBoxItem> _iconsListBoxGameDataList;
        private readonly List<ListBoxItem> _iconsListBoxSettingAndAboutList;

        #endregion

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
            Global.MainPageListBoxItem.Add(CharacterListBoxItem);
            Global.MainPageListBoxItem.Add(FoodListBoxItem);
            Global.MainPageListBoxItem.Add(CookListBoxItem);
            Global.MainPageListBoxItem.Add(ScienceListBoxItem);
            Global.MainPageListBoxItem.Add(CreatureListBoxItem);
            Global.MainPageListBoxItem.Add(NaturalListBoxItem);
            Global.MainPageListBoxItem.Add(GoodListBoxItem);
            //读取游戏版本
            _iconsListBoxGameDataList = new List<ListBoxItem>(
                new[]
                {
                    CharacterListBoxItem, FoodListBoxItem, ScienceListBoxItem, CreatureListBoxItem,
                    NaturalListBoxItem, GoodListBoxItem, StrategyListBoxItem,
                    SocialIntercourseListBoxItem
                }
            );
            _iconsListBoxSettingAndAboutList = new List<ListBoxItem>(
                new[]
                {
                    SettingListBoxItem, AboutListBoxItem
                }
            );
            // 设置Frame标题Margin属性
            SetFrameTitleMargin();
            // 汉堡菜单边框
            HamburgerGrid.BorderBrush = new SolidColorBrush(AccentColor);
            // 默认页
            RootFrame.SourcePageType = typeof(CharacterPage);
            // 设置SearchAutoSuggestBox的ItemSource属性
            SearchAutoSuggestBox.ItemsSource = Global.AutoSuggestBoxItem;
            // 设置主题
            ((Frame)Window.Current.Content).RequestedTheme =
                SettingSet.ThemeSettingRead() ? ElementTheme.Dark : ElementTheme.Light;
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
            if (RootSplit.DisplayMode == SplitViewDisplayMode.CompactInline)
            {
                FrameTitle.Margin = RootSplit.IsPaneOpen ? new Thickness(180, 0, 0, 0) : new Thickness(10, 0, 0, 0);
            }
            else
            {
                FrameTitle.Margin = new Thickness(10, 0, 0, 0);
            }
        }

        /// <summary>
        /// 汉堡菜单列表按钮触摸事件
        /// </summary>
        private void IconsListBoxGameData_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            if (sender == IconsListBoxGameData)
            {
                IconsListBoxSettingAndAbout.SelectedItem = null;
            }
            else if (sender == IconsListBoxSettingAndAbout)
            {
                IconsListBoxGameData.SelectedItem = null;
            }
            var listBoxItem = (ListBoxItem)((ListBox)sender).SelectedItem;
            if (listBoxItem != null)
            {
                var listBoxItemName = listBoxItem.Name;

                switch (listBoxItemName)
                {
                    case "CharacterListBoxItem":
                        FrameTitle.Text = "人物";
                        RootFrame.Navigate(typeof(CharacterPage));
                        break;
                    case "FoodListBoxItem":
                        FrameTitle.Text = "食物";
                        RootFrame.Navigate(typeof(FoodPage));
                        break;
                    case "CookListBoxItem":
                        FrameTitle.Text = "模拟";
                        RootFrame.Navigate(typeof(CookingSimulatorPage));
                        break;
                    case "ScienceListBoxItem":
                        FrameTitle.Text = "科技";
                        RootFrame.Navigate(typeof(SciencePage));
                        break;
                    case "CreatureListBoxItem":
                        FrameTitle.Text = "生物";
                        RootFrame.Navigate(typeof(CreaturePage));
                        break;
                    case "NaturalListBoxItem":
                        FrameTitle.Text = "自然";
                        RootFrame.Navigate(typeof(NaturalPage));
                        break;
                    case "GoodListBoxItem":
                        FrameTitle.Text = "物品";
                        RootFrame.Navigate(typeof(GoodPage));
                        break;
                    case "StrategyListBoxItem":
                        FrameTitle.Text = "攻略";
                        RootFrame.Navigate(typeof(StrategyPage));
                        break;
                    case "SocialIntercourseListBoxItem":
                        FrameTitle.Text = "社交";
                        RootFrame.Navigate(typeof(SocialIntercoursePage));
                        break;
                    case "SettingListBoxItem":
                        FrameTitle.Text = "设置";
                        RootFrame.Navigate(typeof(SettingPage));
                        break;
                    case "AboutListBoxItem":
                        FrameTitle.Text = "关于";
                        RootFrame.Navigate(typeof(AboutPage));
                        break;
                    default:
                        FrameTitle.Text = "";
                        break;
                }
            }

            if (RootSplit.ActualWidth < 1008)
            {
                RootSplit.IsPaneOpen = false;
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
            var extraData = new[] {  suggestBoxItem.SourcePath, suggestBoxItem.Picture, suggestBoxItem.Category};
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
                    CharacterListBoxItem.IsSelected = true;
                    RootFrame.Navigate(typeof(CharacterPage), extraData);
                    break;
                case "食物":
                    FrameTitle.Text = "食物";
                    FoodListBoxItem.IsSelected = true;
                    RootFrame.Navigate(typeof(FoodPage), extraData);
                    break;
                case "科技":
                    FrameTitle.Text = "科技";
                    ScienceListBoxItem.IsSelected = true;
                    RootFrame.Navigate(typeof(SciencePage), extraData);
                    break;
                case "生物":
                    FrameTitle.Text = "生物";
                    CreatureListBoxItem.IsSelected = true;
                    RootFrame.Navigate(typeof(CreaturePage), extraData);
                    break;
                case "自然":
                    FrameTitle.Text = "自然";
                    NaturalListBoxItem.IsSelected = true;
                    RootFrame.Navigate(typeof(NaturalPage), extraData);
                    break;
                case "物品":
                    FrameTitle.Text = "物品";
                    GoodListBoxItem.IsSelected = true;
                    RootFrame.Navigate(typeof(GoodPage), extraData);
                    break;
            }
        }
        #endregion

    }
}
