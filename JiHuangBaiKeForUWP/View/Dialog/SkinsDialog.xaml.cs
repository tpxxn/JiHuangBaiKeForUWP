using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using JiHuangBaiKeForUWP.Model;
using JiHuangBaiKeForUWP.UserControls;
using Windows.UI.Xaml.Media.Animation;

namespace JiHuangBaiKeForUWP.View.Dialog
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SkinsDialog : Page
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
                RootScrollViewer.Background = dimGrayAcrylicBrush;
            }
            base.OnNavigatedTo(e);
            Global.FrameTitle.Text = "皮肤详情";
            if (e.Parameter != null)
            {
                LoadData((Skin)e.Parameter);
            }
            var imageAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("Image");
            imageAnimation?.TryStart(SkinImage);
        }

        public SkinsDialog()
        {
            this.InitializeComponent();
        }

        private byte _skinIndex;
        private int _skinMaxIndex;
        private readonly List<string> _skinColorList = new List<string>();
        private readonly List<string> _skinList = new List<string>();
        private readonly List<string> _skinIntroductionList = new List<string>();

        private void LoadData(Skin c)
        {
            // 颜色
            switch (c.Colors.Count)
            {
                case 0:
                    ColorTextBlock.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    ColorTextBlock.Text = "颜色：" + c.Colors[0];
                    break;
                default:
                    SwitchLeftButton.Visibility = Visibility.Visible;
                    SwitchRightButton.Visibility = Visibility.Visible;
                    _skinMaxIndex = c.Colors.Count - 1;
                    foreach (var color in c.Colors)
                    {
                        _skinColorList.Add(color);
                    }
                    SwitchLeftButton.IsEnabled = false;
                    ColorTextBlock.Text = "颜色：" + _skinColorList[0];
                    break;
            }
            // 图片
            if (c.Colors.Count == 0)
            {
                // 套装
                if (string.IsNullOrEmpty(c.Introduction) && string.IsNullOrEmpty(c.Rarity))
                {
                    ImageButtonGrid.Width = 258;
                    ImageButtonGrid.Height = 170;
                    SkinImage.Height = 170;
                    ImageColumnDefinition.Width = new GridLength(170);
                }
                SkinImage.Source = new BitmapImage(new Uri(c.Picture));
            }
            else
            {
                foreach (var color in c.Colors)
                {
                    _skinList.Add(StringProcess.GetGameResourcePath("P_" + color.Replace(" ", "_").Replace("-", "_") + "_" + c.EnName.Replace(" ", "_").Replace("-", "_")));
                }
                SkinImage.Source = new BitmapImage(new Uri(_skinList[0]));
            }
            //中英文名
            SkinName.Text = c.Name;
            SkinEnName.Text = c.EnName;
            // 稀有度
            switch (c.Rarity)
            {
                case "Common":
                    RarityTextBlock.Text = "普通 Common";
                    break;
                case "Classy":
                    RarityTextBlock.Text = "上等 Classy";
                    break;
                case "Spiffy":
                    RarityTextBlock.Text = "出色 Spiffy";
                    break;
                case "Distinguished":
                    RarityTextBlock.Text = "卓越 Distinguished";
                    break;
                case "Elegant":
                    RarityTextBlock.Text = "高雅 Elegant";
                    break;
                case "Loyal":
                    RarityTextBlock.Text = "忠诚 Loyal";
                    break;
                case "Timeless":
                    RarityTextBlock.Text = "永恒 Timeless";
                    break;
                case "Event":
                    RarityTextBlock.Text = "事件 Event";
                    break;
                case "Proof of Purchase":
                    RarityTextBlock.Text = "购买证明 Proof of Purchase";
                    break;
                case "Reward":
                    RarityTextBlock.Text = "奖励 Reward";
                    break;
                default:
                    RarityTextBlock.Visibility = Visibility.Collapsed;
                    break;
            }
            RarityTextBlock.Foreground = GetSkinColor(c.Rarity);
            // 介绍
            if (c.Colors.Count == 0)
            {
                if(!string.IsNullOrEmpty(c.Introduction))
                    SkinIntroduction.Text = c.Introduction;
            }
            else
            {
                foreach (var color in c.Colors)
                {
                    _skinIntroductionList.Add(c.Introduction.Replace("{Color}", color));
                }
                SkinIntroduction.Text = _skinIntroductionList[0];
            }
        }

        /// <summary>
        /// 左右切换按钮
        /// </summary>
        private void SwitchLeftButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SwitchRightButton.IsEnabled = true;
            if (_skinIndex != 0)
            {
                _skinIndex -= 1;
                if (_skinIndex == 0)
                {
                    SwitchLeftButton.IsEnabled = false;
                }
                SkinImage.Source = new BitmapImage(new Uri(_skinList[_skinIndex]));
                ColorTextBlock.Text = "颜色：" + _skinColorList[_skinIndex];
                SkinIntroduction.Text = _skinIntroductionList[_skinIndex];
            }
        }

        private void SwitchRightButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SwitchLeftButton.IsEnabled = true;
            if (_skinIndex != _skinMaxIndex)
            {
                _skinIndex += 1;
                if (_skinIndex == _skinMaxIndex)
                {
                    SwitchRightButton.IsEnabled = false;
                }
                SkinImage.Source = new BitmapImage(new Uri(_skinList[_skinIndex]));
                ColorTextBlock.Text = "颜色：" + _skinColorList[_skinIndex];
                SkinIntroduction.Text = _skinIntroductionList[_skinIndex];
            }
        }

        /// <summary>
        /// 获取皮肤Color属性
        /// </summary>
        /// <param name="rarity">稀有度</param>
        /// <returns>Color</returns>
        private static SolidColorBrush GetSkinColor(string rarity)
        {
            switch (rarity)
            {
                case "Common":
                    return SkinsColors.Common;
                case "Classy":
                    return SkinsColors.Classy;
                case "Spiffy":
                    return SkinsColors.Spiffy;
                case "Distinguished":
                    return SkinsColors.Distinguished;
                case "Elegant":
                    return SkinsColors.Elegant;
                case "Loyal":
                    return SkinsColors.Loyal;
                case "Timeless":
                    return SkinsColors.Timeless;
                case "Event":
                    return SkinsColors.Event;
                case "Proof of Purchase":
                    return SkinsColors.ProofOfPurchase;
                case "Reward":
                    return SkinsColors.Reward;
                default:
                    return new SolidColorBrush(Colors.Black);
            }
        }

        private void ScrollViewer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var list = new List<DependencyObject>();
            Global.FindChildren(list, (ScrollViewer)sender);
            var scrollViewerGrid = (from dependencyObject in list where dependencyObject.ToString() == "Windows.UI.Xaml.Controls.Grid" select dependencyObject.GetHashCode()).FirstOrDefault();
            if (e.OriginalSource.GetHashCode() == scrollViewerGrid)
            {
                Global.App_BackRequested();
            }
        }
    }
}
