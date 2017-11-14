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
using Windows.UI.Xaml.Media.Animation;

namespace JiHuangBaiKeForUWP.View.Dialog
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class FoodDialog : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Global.FrameTitle.Text = "食物详情";
            if (e.Parameter != null)
            {
                LoadData((Food)e.Parameter);
            }
            var imageAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("Image");
            imageAnimation?.TryStart(FoodImage);
        }

        public FoodDialog()
        {
            this.InitializeComponent();
        }

        private void LoadData(Food c)
        {
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
            Attribute1PicButton.Text = c.AttributeValue ?? c.Attribute;
            if (c.AttributeValue2 != null)
            {
                Attribute2PicButton.Source = $"ms-appx:///Assets/GameResources/Foods/{c.Attribute2}.png";
                Attribute2PicButton.Text = c.AttributeValue2;
                Attribute2PicButton.Visibility = Visibility.Visible;
            }
            FoodIntroduction.Text = c.Introduce;
            ConsolePre.Text = $"c_give(\"{c.Console}\",";
        }

        private void ConsoleNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)sender;
            StringProcess.ConsoleNumTextCheck(textbox);
        }

        private void Copy_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataPackage = new DataPackage();
            dataPackage.SetText(ConsolePre.Text + ConsoleNum.Text + ")");
            Clipboard.SetContent(dataPackage);
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
