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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace JiHuangBaiKeForUWP.View.Dialog
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class GoodSaplingDialog : Page
    {
        public GoodSaplingDialog(GoodSapling c)
        {
            this.InitializeComponent();

            GoodImage.Source = new BitmapImage(new Uri(c.Picture));
            GoodName.Text = c.Name;
            GoodEnName.Text = c.EnName;
            // 来源于植物
            var thickness = new Thickness(5, 0, 0, 0);
            if (c.Plants == null || c.Plants.Count == 0)
            {
                GoodSourcePlantsTextBlock.Visibility = Visibility.Collapsed;
                GoodSourcePlantsWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                foreach (var picPath in c.Plants)
                {
                    var picButton = new PicButton
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = thickness,
                        Source = Global.GetGameResourcePath(picPath),
                        PictureSize = 100
                    };
                    GoodSourcePlantsWrapPanel.Children.Add(picButton);
                }
            }
            GoodIntroduction.Text = c.Introduction;
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
