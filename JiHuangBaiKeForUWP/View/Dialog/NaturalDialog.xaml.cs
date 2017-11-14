using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class NaturalDialog : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Global.FrameTitle.Text = "自然详情";
            if (e.Parameter != null)
            {
                LoadData((Nature)e.Parameter);
            }
            var imageAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("Image");
            imageAnimation?.TryStart(NatureImage);
        }

        public NaturalDialog()
        {
            this.InitializeComponent();
        }

        private void LoadData(Nature c)
        {
            NatureImage.Source = new BitmapImage(new Uri(c.Picture));
            NatureName.Text = c.Name;
            NatureEnName.Text = c.EnName;
            // 富含/偶尔/稀有
            var thickness = new Thickness(5, 0, 0, 0);
            if (c.Abundant == null || c.Abundant.Count == 0)
            {
                NaturalAbundantTextBlock.Visibility = Visibility.Collapsed;
                NaturalAbundantWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                foreach (var picPath in c.Abundant)
                {
                    var picButton = new PicButton
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = thickness,
                        Source = StringProcess.GetGameResourcePath(picPath)
                    };
                    NaturalAbundantWrapPanel.Children.Add(picButton);
                }
            }
            if (c.Occasional == null || c.Occasional.Count == 0)
            {
                NaturalOccasionalTextBlock.Visibility = Visibility.Collapsed;
                NaturalOccasionalWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                foreach (var picPath in c.Occasional)
                {
                    var picButton = new PicButton
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = thickness,
                        Source = StringProcess.GetGameResourcePath(picPath)
                    };
                    NaturalOccasionalWrapPanel.Children.Add(picButton);
                }
            }
            if (c.Rare == null || c.Rare.Count == 0)
            {
                NaturalRareTextBlock.Visibility = Visibility.Collapsed;
                NaturalRareWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                foreach (var picPath in c.Rare)
                {
                    var picButton = new PicButton
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = thickness,
                        Source = StringProcess.GetGameResourcePath(picPath)
                    };
                    NaturalRareWrapPanel.Children.Add(picButton);
                }
            }
            // 介绍
            NatureIntroduction.Text = c.Introduction;
        }

        private void ScrollViewer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            List<DependencyObject> list = new List<DependencyObject>();
            Global.FindChildren(list, (ScrollViewer)sender);
            int scrollViewerGrid = 0;
            foreach (var dependencyObject in list)
            {
                if (dependencyObject.ToString() == "Windows.UI.Xaml.Controls.Grid")
                {
                    scrollViewerGrid = dependencyObject.GetHashCode();
                    break;
                }
            }
            if (e.OriginalSource.GetHashCode() == scrollViewerGrid)
            {
                Global.App_BackRequested();
            }
        }
    }
}
