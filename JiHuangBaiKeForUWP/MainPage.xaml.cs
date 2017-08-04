using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
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
// var dialog = new ContentDialog()
// {
//     Title = "当前目录",
//     Content = ApplicationData.Current.LocalFolder.Path,
//     PrimaryButtonText = "确定",
//     FullSizeDesired = false,
// };
// dialog.PrimaryButtonClick += (s, e) => { };
// await dialog.ShowAsync();

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
            //全局初始化
            GlobalInitializeComponent();
            //读取游戏版本
            _iconsListBoxGameDataList = new List<ListBoxItem>(
                new[]
                {
                    CharacterListBoxItem, FoodListBoxItem, ScienceListBoxItem, CreatureListBoxItem,
                    NaturalListBoxItem, GoodListBoxItem, DedicatedServersListBoxItem, StrategyListBoxItem,
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
            // 设置SearchAutoSuggestBox的数据源
            SearchAutoSuggestBox.ItemsSource = Global.AutoSuggestBoxItem;
        }

        /// <summary>
        /// 全局变量初始化
        /// </summary>
        public void GlobalInitializeComponent()
        {
            // 读取游戏版本
            Global.GameVersion = SettingSet.GameVersionSettingRead();
            // 设置AutoSuggestBox的数据源
            Global.SetAutoSuggestBoxItemSource();
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

        #region 顶部按钮

        #region 汉堡菜单

        /// <summary>
        /// 汉堡菜单按钮触摸事件
        /// </summary>
        private void HamburgerButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            RootSplit.IsPaneOpen = !RootSplit.IsPaneOpen;
            SetFrameTitleMargin();
        }

        /// <summary>
        /// 设置Frame框架标题Margin属性
        /// </summary>
        private void SetFrameTitleMargin()
        {
            if (RootSplit.DisplayMode == SplitViewDisplayMode.CompactInline)
            {
                FrameTitle.Margin = RootSplit.IsPaneOpen ? new Thickness(154, 0, 0, 0) : new Thickness(10, 0, 0, 0);
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
            if (sender == IconsListBoxGameData)
            {
                IconsListBoxSettingAndAbout.SelectedItem = null;
            }
            else if (sender == IconsListBoxSettingAndAbout)
            {
                IconsListBoxGameData.SelectedItem = null;
            }

            var listBoxItem = (ListBoxItem) ((ListBox) sender).SelectedItem;
            if (listBoxItem != null)
            {
                var listBoxItemName = listBoxItem.Name;

                switch (listBoxItemName)
                {
                    case "CharacterListBoxItem":
                        FrameTitle.Text = "人物";
                        RootFrame.Navigate(typeof(CharacterPage), null);
                        break;
                    case "FoodListBoxItem":
                        FrameTitle.Text = "食物";
                        RootFrame.Navigate(typeof(FoodPage), null);
                        break;
                    case "CookListBoxItem":
                        FrameTitle.Text = "模拟";
                        RootFrame.Navigate(typeof(CookingSimulatorPage), null);
                        break;
                    case "ScienceListBoxItem":
                        FrameTitle.Text = "科技";
                        RootFrame.Navigate(typeof(SciencePage), null);
                        break;
                    case "CreatureListBoxItem":
                        FrameTitle.Text = "生物";
                        RootFrame.Navigate(typeof(CreaturePage), null);
                        break;
                    case "NaturalListBoxItem":
                        FrameTitle.Text = "自然";
                        RootFrame.Navigate(typeof(NaturalPage), null);
                        break;
                    case "GoodListBoxItem":
                        FrameTitle.Text = "物品";
                        RootFrame.Navigate(typeof(GoodPage), null);
                        break;
                    case "DedicatedServersListBoxItem":
                        FrameTitle.Text = "服务器";
                        break;
                    case "StrategyListBoxItem":
                        FrameTitle.Text = "攻略";
                        break;
                    case "SocialIntercourseListBoxItem":
                        FrameTitle.Text = "社交";
                        break;
                    case "SettingListBoxItem":
                        FrameTitle.Text = "设置";
                        RootFrame.Navigate(typeof(SettingPage), null);
                        break;
                    case "AboutListBoxItem":
                        FrameTitle.Text = "关于";
                        RootFrame.Navigate(typeof(AboutPage), null);
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
            var str = sender.Text.Trim();
            if (string.IsNullOrEmpty(str)) return;
            for (var i = Global.AutoSuggestBoxItem.Count-1; i >= 0; i--)
            {
                if (Global.AutoSuggestBoxItem[i].Name.IndexOf(str, StringComparison.Ordinal) < 0 && Global.AutoSuggestBoxItem[i].EnName.IndexOf(str, StringComparison.Ordinal) < 0)
                {
                    Global.AutoSuggestBoxItem.Remove(Global.AutoSuggestBoxItem[i]);
                }
            }
        }

        /// <summary>
        /// 搜索框查询提交事件
        /// </summary>
        private async void SearchAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (sender.Items == null) return;
            var dialog = new ContentDialog()
            {
                Title = "当前目录",
                Content = sender.Items[0].ToString(),
                PrimaryButtonText = "确定",
                FullSizeDesired = false,
            };
            dialog.PrimaryButtonClick += (s, e) => { };
            await dialog.ShowAsync();
        }

        /// <summary>
        /// 搜索框选择事件
        /// </summary>
        private void SearchAutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {

        }

        #endregion

        #endregion

    }
}
