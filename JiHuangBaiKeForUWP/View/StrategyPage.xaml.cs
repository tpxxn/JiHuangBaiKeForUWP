using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
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

namespace JiHuangBaiKeForUWP.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class StrategyPage : Page
    {
        private readonly List<ListBox> _listBoxList;
        private readonly Stack<ListBoxItem> _listBoxItemStack = new Stack<ListBoxItem>();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var size = GetScreen();
            if (size.Width <= 800)
            {
                MenuColumn.Width = new GridLength(1, GridUnitType.Star);
                WebViewColumn.Width = new GridLength(0);
            }
            //后退按钮可见
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        #region 处理后退按钮

        /// <summary>
        /// 获取窗口大小(和内置函数数据不一样)
        /// </summary>
        /// <returns>Size类型的窗口尺寸</returns>
        public static Size GetScreen()
        {
            var applicationView = ApplicationView.GetForCurrentView();
            var displayInformation = DisplayInformation.GetForCurrentView();
            var bounds = applicationView.VisibleBounds;
            var scale = displayInformation.RawPixelsPerViewPixel;
            var size = new Size(bounds.Width * scale, bounds.Height * scale);
            return size;
        }

        /// <summary>
        /// ScrollViewer滚动到指定位置
        /// </summary>
        /// <param name="scrollViewer">ScrollViewer的Name</param>
        /// <param name="uiElement">UIElement的Name</param>
        public static void ScrollToElement(ScrollViewer scrollViewer, UIElement uiElement)
        {
            var transform = uiElement.TransformToVisual(scrollViewer);
            var point = transform.TransformPoint(new Point(0, 0));
            if (point.Y != 0)
            {
                var y = point.Y + scrollViewer.VerticalOffset;
                scrollViewer.ChangeView(null, y, null, true);
            }
        }

        /// <summary>
        /// 后退按钮请求处理
        /// </summary>
        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            var size = GetScreen();
            if (size.Width > 801)
            {
                if (_listBoxItemStack.Count <= 1) return;
                _listBoxItemStack.Pop();
                var popListBoxItem = _listBoxItemStack.Peek();
                foreach (var listBox in _listBoxList)
                {
                    if (listBox.Items == null) continue;
                    foreach (ListBoxItem listBoxItem in listBox.Items)
                    {
                        if (listBoxItem.Name != popListBoxItem.Name) continue;
                        popListBoxItem.IsSelected = true;
                        ScrollToElement(StrategyScrollViewer, listBoxItem);
                        ListBox_Tapped(listBox, null);
                    }
                }
            }
            else
            {
                MenuColumn.Width = new GridLength(1, GridUnitType.Star);
                WebViewColumn.Width = new GridLength(0);
            }
        }

        #endregion

        /// <summary>
        /// 构造事件
        /// </summary>
        public StrategyPage()
        {
            this.InitializeComponent();
            _listBoxItemStack.Clear();
            _listBoxList = new List<ListBox>(new[] { GameBaseListBox, BossListBox });
            //TGP隐藏汉化游戏、Mod订阅、推荐Mod
            if (Global.GameVersion == 1)
            {
                ChinesizeGameListBoxItem.Visibility = Visibility.Collapsed;
                ModDownloadListBoxItem.Visibility = Visibility.Collapsed;
                RecommendModListBoxItem.Visibility = Visibility.Collapsed;
            }
            //隐藏BOSS打法
            if (Global.GameVersion == 4)
            {
                HideListBoxItem(new List<ListBoxItem>
                {
                    TreguardBossListBoxItem, PoisonBirchnutBossListBoxItem, SpiderQueenBossListBoxItem, VargQueenBossListBoxItem,
                    EwecusQueenBossListBoxItem, BeeQueenBossListBoxItem, AncientGuardianBossListBoxItem, DeerclopsBossListBoxItem,
                    MooseBossListBoxItem, DragonflyBossListBoxItem, BeargerBossListBoxItem, ReanimatedSkeletonBossListBoxItem,
                    AncientFuelweaverBossListBoxItem, ToadstoolBossListBoxItem, MiseryToadstoolBossListBoxItem, KlausBossListBoxItem,
                    AntlionBossListBoxItem
                });
            }
            else
            {
                HideListBoxItem(new List<ListBoxItem>
                {
                    PlamTreeguardBossListBoxItem, QuackenBossListBoxItem, SeaInadoBossListBoxItem, TigerSharkBossListBoxItem
                });
                if (Global.GameVersion != 0 && Global.GameVersion != 1)
                {
                    HideListBoxItem(new List<ListBoxItem>
                    {
                        EwecusQueenBossListBoxItem, BeeQueenBossListBoxItem, ReanimatedSkeletonBossListBoxItem, AncientFuelweaverBossListBoxItem,
                        ToadstoolBossListBoxItem, MiseryToadstoolBossListBoxItem, KlausBossListBoxItem, AntlionBossListBoxItem
                    });
                    if (Global.GameVersion == 2)
                    {
                        HideListBoxItem(new List<ListBoxItem>
                        {
                            VargQueenBossListBoxItem, PoisonBirchnutBossListBoxItem, MooseBossListBoxItem, DragonflyBossListBoxItem,
                            BeargerBossListBoxItem
                        });
                    }
                }
            }
            ListBox_Tapped(GameBaseListBox, null);
            _listBoxItemStack.Push(BaseOperationListBoxItem);
            SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
        }

        /// <summary>
        /// 隐藏listBoxItemList列表中的ListBoxItem
        /// </summary>
        /// <param name="listBoxItemList">需要隐藏的ListBoxItem列表</param>
        private static void HideListBoxItem(List<ListBoxItem> listBoxItemList)
        {
            foreach (var listBoxItem in listBoxItemList)
            {
                listBoxItem.Visibility = Visibility.Collapsed;
            }
        }

        private void ListBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var senderListBox = (ListBox)sender;
            foreach (var listBox in _listBoxList)
            {
                if (listBox != senderListBox)
                {
                    listBox.SelectedItem = null;
                }
            }
            var listBoxItem = (ListBoxItem)((ListBox)sender).SelectedItem;
            if (e != null && listBoxItem != _listBoxItemStack.Peek())
            {
                _listBoxItemStack.Push(listBoxItem);
            }
            var size = GetScreen();
            if (size.Width > 801)
            {
                MenuColumn.Width = new GridLength(300);
                WebViewColumn.Width = new GridLength(1, GridUnitType.Star);
            }
            else
            {
                MenuColumn.Width = new GridLength(0);
                WebViewColumn.Width = new GridLength(1, GridUnitType.Star);
            }
            var webView = StrategyWebView;
            if (listBoxItem != null)
            {
                var listBoxItemName = listBoxItem.Name;
                switch (listBoxItemName)
                {
                    case "BaseOperationListBoxItem":
                        webView.Navigate(new Uri("https://steamcommunity.com/sharedfiles/filedetails/?id=966448064&searchtext=%E6%93%8D%E4%BD%9C"));
                        break;
                    case "ChinesizeGameListBoxItem":
                        webView.Navigate(new Uri("https://steamcommunity.com/sharedfiles/filedetails/?id=757621274"));
                        break;
                    case "ModDownloadListBoxItem":
                        webView.Navigate(new Uri("https://steamcommunity.com/app/322330/workshop/"));
                        break;
                    case "RecommendModListBoxItem":
                        webView.Navigate(new Uri("https://steamcommunity.com/sharedfiles/filedetails/?id=635215011"));
                        break;
                    case "GameUpdateListBoxItem":
                        webView.Navigate(new Uri("http://store.steampowered.com/news/?appids=322330"));
                        break;
                }
            }
        }

        #region WebView加载
        private void StrategyWebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            LoadingControl.IsLoading = true;
            StrategyWebView.Visibility = Visibility.Collapsed;
        }

        private void StrategyWebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            LoadingControl.IsLoading = false;
            if (args.IsSuccess)
            {
                StrategyWebView.Visibility = Visibility.Visible;
            }
            else
            {
                var contentDialog = new ContentDialog()
                {
                    Title = "加载失败",
                    Content = "电波君可能被墙挡住勒~~o(>_<)o ~~",
                    PrimaryButtonText = "确定",
                    FullSizeDesired = false,
                };
                Global.ShowDialog(contentDialog);
            }
        }
        #endregion
    }
}
