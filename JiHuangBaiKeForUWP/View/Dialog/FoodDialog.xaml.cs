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
    public sealed partial class FoodDialog : Page
    {
        public FoodDialog(FoodMeat c)
        {
            this.InitializeComponent();

            FoodImage.Source = new BitmapImage(new Uri(c.Picture));
            FoodName.Text = c.Name;
            FoodEnName.Text = c.EnName;
            FoodHealth.Value = c.Health;
            FoodHealth.BarColor = Global.ColorGreen;
            FoodHunger.Value = c.Hunger;
            FoodHunger.BarColor = Global.ColorKhaki;
            FoodSanity.Value = c.Sanity;
            FoodSanity.BarColor = Global.ColorRed;
            FoodPerish.Value = c.Perish;
            FoodPerish.BarColor = Global.ColorBlue;
            Attribute1PicButton.Source = $"ms-appx:///Assets/GameResources/Foods/{c.Attribute}.png";
            Attribute1PicButton.Text = c.AttributeValue;
            if (c.AttributeValue2 != null)
            {
                Attribute2PicButton.Source = $"ms-appx:///Assets/GameResources/Foods/{c.Attribute2}.png";
                Attribute2PicButton.Text = c.AttributeValue2;
            }
            FoodIntroduction.Text = c.Introduce;
            Console.Text = $"c_give(\"{c.Console}\",10)";
        }

        public FoodDialog(FoodVegetable c)
        {
            this.InitializeComponent();

            FoodImage.Source = new BitmapImage(new Uri(c.Picture));
            FoodName.Text = c.Name;
            FoodEnName.Text = c.EnName;
            FoodHealth.Value = c.Health;
            FoodHealth.BarColor = Global.ColorGreen;
            FoodHunger.Value = c.Hunger;
            FoodHunger.BarColor = Global.ColorKhaki;
            FoodSanity.Value = c.Sanity;
            FoodSanity.BarColor = Global.ColorRed;
            FoodPerish.Value = c.Perish;
            FoodPerish.BarColor = Global.ColorBlue;
            Attribute1PicButton.Source = $"ms-appx:///Assets/GameResources/Foods/{c.Attribute}.png";
            Attribute1PicButton.Text = c.AttributeValue;
            FoodIntroduction.Text = c.Introduce;
            Console.Text = $"c_give(\"{c.Console}\",10)";
        }

        public FoodDialog(FoodFruit c)
        {
            this.InitializeComponent();

            FoodImage.Source = new BitmapImage(new Uri(c.Picture));
            FoodName.Text = c.Name;
            FoodEnName.Text = c.EnName;
            FoodHealth.Value = c.Health;
            FoodHealth.BarColor = Global.ColorGreen;
            FoodHunger.Value = c.Hunger;
            FoodHunger.BarColor = Global.ColorKhaki;
            FoodSanity.Value = c.Sanity;
            FoodSanity.BarColor = Global.ColorRed;
            FoodPerish.Value = c.Perish;
            FoodPerish.BarColor = Global.ColorBlue;
            Attribute1PicButton.Source = $"ms-appx:///Assets/GameResources/Foods/{c.Attribute}.png";
            Attribute1PicButton.Text = c.AttributeValue;
            if (c.AttributeValue2 != null)
            {
                Attribute2PicButton.Source = $"ms-appx:///Assets/GameResources/Foods/{c.Attribute2}.png";
                Attribute2PicButton.Text = c.AttributeValue2;
            }
            FoodIntroduction.Text = c.Introduce;
            Console.Text = $"c_give(\"{c.Console}\",10)";
        }

        public FoodDialog(FoodEgg c)
        {
            this.InitializeComponent();

            FoodImage.Source = new BitmapImage(new Uri(c.Picture));
            FoodName.Text = c.Name;
            FoodEnName.Text = c.EnName;
            FoodHealth.Value = c.Health;
            FoodHealth.BarColor = Global.ColorGreen;
            FoodHunger.Value = c.Hunger;
            FoodHunger.BarColor = Global.ColorKhaki;
            FoodSanity.Value = c.Sanity;
            FoodSanity.BarColor = Global.ColorRed;
            FoodPerish.Value = c.Perish;
            FoodPerish.BarColor = Global.ColorBlue;
            Attribute1PicButton.Source = $"ms-appx:///Assets/GameResources/Foods/{c.Attribute}.png";
            Attribute1PicButton.Text = c.AttributeValue;
            FoodIntroduction.Text = c.Introduce;
            Console.Text = $"c_give(\"{c.Console}\",10)";
        }

        public FoodDialog(FoodOther c)
        {
            this.InitializeComponent();

            FoodImage.Source = new BitmapImage(new Uri(c.Picture));
            FoodName.Text = c.Name;
            FoodEnName.Text = c.EnName;
            FoodHealth.Value = c.Health;
            FoodHealth.BarColor = Global.ColorGreen;
            FoodHunger.Value = c.Hunger;
            FoodHunger.BarColor = Global.ColorKhaki;
            FoodSanity.Value = c.Sanity;
            FoodSanity.BarColor = Global.ColorRed;
            FoodPerish.Value = c.Perish;
            FoodPerish.BarColor = Global.ColorBlue;
            Attribute1PicButton.Source = $"ms-appx:///Assets/GameResources/Foods/{c.Attribute}.png";
            Attribute1PicButton.Text = c.AttributeValue;
            FoodIntroduction.Text = c.Introduce;
            Console.Text = $"c_give(\"{c.Console}\",10)";
        }


        public FoodDialog(FoodNoFc c)
        {
            this.InitializeComponent();

            FoodImage.Source = new BitmapImage(new Uri(c.Picture));
            FoodName.Text = c.Name;
            FoodEnName.Text = c.EnName;
            FoodHealth.Value = c.Health;
            FoodHealth.BarColor = Global.ColorGreen;
            FoodHunger.Value = c.Hunger;
            FoodHunger.BarColor = Global.ColorKhaki;
            FoodSanity.Value = c.Sanity;
            FoodSanity.BarColor = Global.ColorRed;
            FoodPerish.Value = c.Perish;
            FoodPerish.BarColor = Global.ColorBlue;
            if (c.Attribute != null)
            {
                Attribute1PicButton.Source = null;
                Attribute1PicButton.Text = c.Attribute;
            }
            else
            {
                FoodAttributeStackPanel.Visibility = Visibility.Collapsed;
            }
            FoodIntroduction.Text = c.Introduce;
            Console.Text = $"c_give(\"{c.Console}\",10)";
        }

        private void Copy_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataPackage = new DataPackage();
            dataPackage.SetText(Console.Text);
            Clipboard.SetContent(dataPackage);
        }

        private void Food_Jump_Tapped(object sender, TappedRoutedEventArgs e)
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
