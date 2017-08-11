using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
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

namespace JiHuangBaiKeForUWP.View.Dialog
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class GoodMaterialDialog : Page
    {
        public GoodMaterialDialog(GoodMaterial c)
        {
            this.InitializeComponent();
            GoodImage.Source = new BitmapImage(new Uri(c.Picture));
            GoodName.Text = c.Name;
            GoodEnName.Text = c.EnName;
            // 可制作科技/来源于生物
            var thickness = new Thickness(5, 0, 0, 0);
            if (c.Science == null || c.Science.Count == 0)
            {
                GoodScienceTextBlock.Visibility = Visibility.Collapsed;
                GoodScienceWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                foreach (var picPath in c.Science)
                {
                    var picButton = new PicButton
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = thickness,
                        Source = Global.GetGameResourcePath(picPath)
                    };
                    GoodScienceWrapPanel.Children.Add(picButton);
                }
            }
            if (c.SourceCreature == null || c.SourceCreature.Count == 0)
            {
                GoodSourceCreatureTextBlock.Visibility = Visibility.Collapsed;
                GoodSourceCreatureWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                foreach (var picPath in c.SourceCreature)
                {
                    var picButton = new PicButton
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = thickness,
                        Source = Global.GetGameResourcePath(picPath)
                    };
                    GoodSourceCreatureWrapPanel.Children.Add(picButton);
                }
            }
            // 介绍
            GoodIntroduction.Text = c.Introduction;
            // 控制台
            Console.Text = $"c_give(\"{c.Console}\",10)";
        }

        private void Copy_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataPackage = new DataPackage();
            dataPackage.SetText(Console.Text);
            Clipboard.SetContent(dataPackage);
        }
    }
}
