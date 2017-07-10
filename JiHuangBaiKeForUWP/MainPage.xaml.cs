using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace JiHuangBaiKeForUWP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            InitializeComponent();
        }

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
                    var brush = RootGrid.Background as SolidColorBrush;
                    if (brush == null)
                    {
                        brush = new SolidColorBrush();
                    }
                    brush.Color = bg;
                });
        }

        #endregion
        
        #region 汉堡菜单

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            RootSplit.IsPaneOpen = !RootSplit.IsPaneOpen;
        }

        //列表按钮
        private void IconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CharacterListBoxItem.IsSelected)
            {

            }
            else if ((FoodListBoxItem.IsSelected))
            {

            }

        }

        #endregion
    }
}
