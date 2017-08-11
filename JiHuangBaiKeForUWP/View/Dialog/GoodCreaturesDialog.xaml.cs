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
    public sealed partial class GoodCreaturesDialog : Page
    {
        public GoodCreaturesDialog(GoodCreatures c)
        {
            this.InitializeComponent();

            GoodImage.Source = new BitmapImage(new Uri(c.Picture));
            GoodName.Text = c.Name;
            GoodEnName.Text = c.EnName;
            // 保鲜
            if (c.Fresh == 0)
            {
                GoodFresh.Visibility = Visibility.Collapsed;
            }
            else
            {
                GoodFresh.Value = c.Fresh;
                GoodFresh.BarColor = Global.ColorBlue;
            }
            // 杀害后获得
            if (c.Goods.Count == 0)
            {
                GoodGoodsTextBlock.Visibility = Visibility.Collapsed;
                GoodGoodsWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                var thickness = new Thickness(20, 0, 0, 0);
                foreach (var str in c.Goods)
                {
                    var breakPosition = str.IndexOf('|');
                    var goodSource = str.Substring(0, breakPosition);
                    var goodText = str.Substring(breakPosition + 1);
                    var picButton = new PicButton
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = thickness,
                        Source = Global.GetGameResourcePath(goodSource),
                        Text = goodText
                    };
                    GoodGoodsWrapPanel.Children.Add(picButton);
                }
            }
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
