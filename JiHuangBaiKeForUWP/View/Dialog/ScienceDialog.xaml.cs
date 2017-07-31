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
    public sealed partial class ScienceDialog : Page
    {
        public ScienceDialog(Science c)
        {
            this.InitializeComponent();

            ScienceImage.Source = new BitmapImage(new Uri(c.Picture));
            ScienceName.Text = c.Name;
            ScienceEnName.Text = c.EnName;
            Need1PicButton.Source = GetPath(c.Need1);
            Need1PicButton.Text = $"×{c.Need1Value}";
            if (c.Need2 != null)
            {
                Need2PicButton.Source = GetPath(c.Need2);
                Need2PicButton.Text = $"×{c.Need2Value}";
                Need2PicButton.Visibility = Visibility.Visible;
            }
            if (c.Need3 != null)
            {
                Need3PicButton.Source = GetPath(c.Need3);
                Need3PicButton.Text = $"×{c.Need3Value}";
                Need3PicButton.Visibility = Visibility.Visible;
            }
            if (c.Unlock == null && c.UnlockCharcter == null && c.UnlockBlueprint == null)
            {
                ScienceUnlockStackPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (c.Unlock != null)
                {
                    UnlockPicButton.Visibility = Visibility.Visible;
                    UnlockPicButton.Source = GetPath(c.Unlock);
                }
                if (c.UnlockCharcter != null)
                {
                    UnlockCharcterButton.Visibility = Visibility.Visible;
                    UnlockCharcterImage.Source = new BitmapImage(new Uri(GetPath(c.UnlockCharcter))); 
                }
                if (c.UnlockBlueprint != null)
                {
                    UnlockBlueprintPicButton.Visibility = Visibility.Visible;
                    UnlockBlueprintPicButton.Source = GetPath(c.UnlockBlueprint);
                }
            }
            ScienceIntroduction.Text = c.Introduction;
            Console.Text = $"c_give(\"{c.Console}\",10)";
        }

        private string GetPath(string str)
        {
            var strHead = str.Substring(0, 1);
            switch (strHead)
            {
                case "A":
                    str = $"ms-appx:///Assets/GameResources/Creatures/{str}.png";
                    break;
                case "C":
                    str = $"ms-appx:///Assets/GameResources/Charcters/{str}.png";
                    break;
                case "F":
                    str = $"ms-appx:///Assets/GameResources/Foods/{str}.png";
                    break;
                case "G":
                    str = $"ms-appx:///Assets/GameResources/Goods/{str}.png";
                    break;
                case "N":
                    str = $"ms-appx:///Assets/GameResources/Natures/{str}.png";
                    break;
                case "S":
                    str = $"ms-appx:///Assets/GameResources/Sciences/{str}.png";
                    break;
                default:
                    str = $"ms-appx:///Assets/GameResources/Goods/{str}.png";
                    break;
            }
            return str;
        }

        private void Copy_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataPackage = new DataPackage();
            dataPackage.SetText(Console.Text);
            Clipboard.SetContent(dataPackage);
        }

        private void Science_Jump_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
            switch (((PicButton)sender).Source)
            {
                //TODO Food跳转按钮跳转事件
                default:
                    break;
            }
        }
    }
}
