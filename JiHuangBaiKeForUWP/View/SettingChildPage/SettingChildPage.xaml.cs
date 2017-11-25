using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
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
using JiHuangBaiKeForUWP.UserControls.SettingPage;
using Microsoft.Win32.SafeHandles;

namespace JiHuangBaiKeForUWP.View.SettingChildPage
{
    /// <summary>
    /// 设置子页面
    /// </summary>
    public sealed partial class SettingChildPage : Page
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
                //隐藏主题设置
                ThemeStackPanel.Visibility = Visibility.Collapsed;
                //显示亚克力背景设置
                AcrylicOpacityStackPanel.Visibility = Visibility.Visible;
                AcrylicBackgroundStackPanel.Visibility = Visibility.Visible;
                //添加ColorPicker控件
                var colorPicker = new ColorPicker
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Color = Color.FromArgb(255, 105, 105, 105),
                    RenderTransformOrigin = new Point(0, 0),
                    RenderTransform = new CompositeTransform
                    {
                        ScaleX = 0.7,
                        ScaleY = 0.7
                    }
                };
                colorPicker.ColorChanged += ColorPicker_ColorChanged;
                AcrylicBackgroundStackPanel.Children.Add(colorPicker);
                AcrylicOpacitySlider.Value = SettingSet.AcrylicOpacitySettingRead();
                _acrylicOpacitySetted = true;
                colorPicker.Color = StringProcess.StringToColor(SettingSet.AcrylicColorSettingRead());
            }
        }

        #region 字段

        private readonly int _gameVersionSelectIndex;
        private bool _acrylicOpacitySetted;

        #endregion

        #region 构造器

        public SettingChildPage()
        {
            _gameVersionSelectIndex = Global.GameVersion;
            this.InitializeComponent();
            ThemeToggleSwitch.IsOn = SettingSet.ThemeSettingRead();
        }

        #endregion

        #region 游戏版本

        /// <summary>
        /// 设置游戏版本
        /// </summary>
        private void GameVersionComboBox_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (GameVersionComboBox.Items != null && GameVersionComboBox.Items.Count >= 5)
            {
                GameVersionComboBox.SelectedIndex = _gameVersionSelectIndex;
            }
        }

        /// <summary>
        /// 游戏版本改变时自动保存
        /// </summary>
        private async void GameVersionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SettingSet.GameVersionSettingSet(GameVersionComboBox.SelectedIndex);
            Global.GameVersion = GameVersionComboBox.SelectedIndex;
            await Global.SetAutoSuggestBoxItem();
        }

        #endregion

        #region 主题

        /// <summary>
        /// 设置主题
        /// </summary>
        private void ThemeToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            SettingSet.ThemeSettingSet(ThemeToggleSwitch.IsOn);
            ((Frame) Window.Current.Content).RequestedTheme =
                ThemeToggleSwitch.IsOn ? ElementTheme.Dark : ElementTheme.Light;
        }

        #endregion

        #region 亚克力背景

        private void AcrylicOpacitySlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (!_acrylicOpacitySetted) return;
            SettingSet.AcrylicOpacitySettingSet(e.NewValue);
            Global.TinkOpacity = e.NewValue;
            SetAcrylicBrush();
        }

        private void ColorPicker_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
        {
            SettingSet.AcrylicColorSettingSet(args.NewColor.ToString());
            Global.TinkColor = args.NewColor;
            SetAcrylicBrush();
        }

        private void SetAcrylicBrush()
        {
            var dimGrayAcrylicBrush = new AcrylicBrush
            {
                BackgroundSource = AcrylicBackgroundSource.HostBackdrop,
                FallbackColor = Colors.Transparent,
                TintColor = Global.TinkColor,
                TintOpacity = Global.TinkOpacity
            };
            //框架
            var rootGrid = Global.RootGrid;
            var rootRelativePanelAcrylic = (RelativePanel) rootGrid.Children[0];
            var rootSplit = (SplitView) rootGrid.Children[2];
            var autoSuggestGrid = Global.AutoSuggestGrid;
            var iconsListViewGameData = Global.IconsListViewGameData;
            var iconsListViewSettingAndAbout = Global.IconsListViewSettingAndAbout;
            rootRelativePanelAcrylic.Background = dimGrayAcrylicBrush;
            rootSplit.PaneBackground = dimGrayAcrylicBrush;
            rootSplit.Background = dimGrayAcrylicBrush;
            autoSuggestGrid.Background = dimGrayAcrylicBrush;
            iconsListViewGameData.Background = dimGrayAcrylicBrush;
            iconsListViewSettingAndAbout.Background = dimGrayAcrylicBrush;
            //页面
            if (Global.SettingPageRootGrid != null)
                Global.SettingPageRootGrid.Background = dimGrayAcrylicBrush;
            RootStackPanel.Background = dimGrayAcrylicBrush;
        }

        #endregion
    }
}
