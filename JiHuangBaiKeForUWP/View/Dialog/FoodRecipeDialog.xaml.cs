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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace JiHuangBaiKeForUWP.View.Dialog
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class FoodRecipeDialog : Page
    {
        public FoodRecipeDialog(FoodRecipe2 c)
        {
            this.InitializeComponent();

            FoodRecipeImage.Source = new BitmapImage(new Uri(c.Picture));
            FoodRecipeName.Text = c.Name;
            FoodRecipeEnName.Text = c.EnName;
            if (c.PortableCrockPot)
            {
                FoodRecipePortableCrockPot.Visibility = Visibility.Visible;
                FoodRecipePortableCrockPot.Source = new BitmapImage(new Uri("ms-appx:///Assets/GameResources/CP_PortableCrockPot.png"));
            }
            FoodRecipeHealth.Value = c.Health;
            FoodRecipeHealth.BarColor = Global.ColorGreen;
            FoodRecipeHunger.Value = c.Hunger;
            FoodRecipeHunger.BarColor = Global.ColorKhaki;
            FoodRecipeSanity.Value = c.Sanity;
            FoodRecipeSanity.BarColor = Global.ColorRed;
            FoodRecipePerish.Value = c.Perish;
            FoodRecipePerish.MaxValue = c.Perish > 20 ? 18750 : 20;
            FoodRecipePerish.BarColor = Global.ColorBlue;
            FoodRecipeCooktime.Value = c.Cooktime;
            FoodRecipeCooktime.BarColor = Global.ColorPurple;
            FoodRecipePriority.ShowIfZero = true;
            FoodRecipePriority.Value = c.Priority;
            FoodRecipePriority.BarColor = Global.ColorPink;
            Need1Button.Source = $"ms-appx:///Assets/GameResources/Foods/{c.NeedPicture1}.png";
            Need1Button.Text = c.Need1;
            if (c.NeedOr != null)
            {
                NeedOrButton.Visibility = Visibility.Visible;
                NeedOrButton.Source = $"ms-appx:///Assets/GameResources/Foods/{c.NeedPictureOr}.png";
                NeedOrButton.Text = c.NeedOr;
            }
            if (c.Need2 != null)
            {
                Need2Button.Visibility = Visibility.Visible;
                Need2Button.Source = $"ms-appx:///Assets/GameResources/Foods/{c.NeedPicture2}.png";
                Need2Button.Text = c.Need2;
            }
            if (c.Need3 != null)
            {
                Need3Button.Visibility = Visibility.Visible;
                Need3Button.Source = $"ms-appx:///Assets/GameResources/Foods/{c.NeedPicture3}.png";
                Need3Button.Text = c.Need3;
            }
            #region restrictions
            var restrictions1 = new List<string>();
            var restrictions2 = new List<string>();
            var prePicture = new[] { c.Restrictions1.Text, c.Restrictions2.Text, c.Restrictions3.Text, c.Restrictions4.Text, c.Restrictions5.Text };
            var pre = new[] { c.Restrictions1.Pre, c.Restrictions2.Pre, c.Restrictions3.Pre, c.Restrictions4.Pre, c.Restrictions5.Pre };
            var restrictionsAttributes = Global.StringDelRepeatData(pre);
            if (pre[0] == restrictionsAttributes[0] && prePicture[0] != null)
            {
                restrictions1.Add(prePicture[0]);
            }
            if (pre[1] == restrictionsAttributes[0] && prePicture[1] != null)
            {
                restrictions1.Add(prePicture[1]);
            }
            if (pre[1] == restrictionsAttributes[1] && prePicture[1] != null)
            {
                restrictions2.Add(prePicture[1]);
            }
            if (pre[2] == restrictionsAttributes[0] && prePicture[2] != null)
            {
                restrictions1.Add(prePicture[2]);
            }
            if (pre[2] == restrictionsAttributes[1] && prePicture[2] != null)
            {
                restrictions2.Add(prePicture[2]);
            }
            if (pre[3] == restrictionsAttributes[0] && prePicture[3] != null)
            {
                restrictions1.Add(prePicture[3]);
            }
            if (pre[3] == restrictionsAttributes[1] && prePicture[3] != null)
            {
                restrictions2.Add(prePicture[3]);
            }
            if (pre[4] == restrictionsAttributes[0] && prePicture[4] != null)
            {
                restrictions1.Add(prePicture[4]);
            }
            if (pre[4] == restrictionsAttributes[1] && prePicture[4] != null)
            {
                restrictions2.Add(prePicture[4]);
            }
            if (c.Restrictions1.Pre != null)
            {
                var stackPanel1 = new StackPanel { Orientation = Orientation.Horizontal };
                var stackPanel2 = new StackPanel { Orientation = Orientation.Horizontal };
                if (restrictions1.Count != 0)
                {
                    var textBlock = new TextBlock
                    {
                        Text = restrictionsAttributes[0],
                        Padding = new Thickness(0, 8, 5, 0)
                    };
                    stackPanel1.Children.Add(textBlock);
                    foreach (var str in restrictions1)
                    {
                        var picButton = new PicButton()
                        {
                            Source = $"ms-appx:///Assets/GameResources/Foods/{str}.png",
                        };
                        picButton.Tapped += Food_Jump_Tapped;
                        stackPanel1.Children.Add(picButton);
                    }
                    FoodRecipeRestrictionsStackPanel.Children.Add(stackPanel1);
                }
                if (restrictions2.Count != 0)
                {
                    var textBlock = new TextBlock
                    {
                        Text = restrictionsAttributes[1],
                        Padding = new Thickness(0, 8, 5, 0)
                    };
                    stackPanel2.Children.Add(textBlock);
                    foreach (var str in restrictions2)
                    {
                        var picButton = new PicButton()
                        {
                            Source = $"ms-appx:///Assets/GameResources/Foods/{str}.png",
                        };
                        picButton.Tapped += Food_Jump_Tapped;
                        stackPanel2.Children.Add(picButton);
                    }
                    FoodRecipeRestrictionsStackPanel.Children.Add(stackPanel2);
                }
            }
            else
            {
                FoodRecipeRestrictionsTextBlock.Visibility = Visibility.Collapsed;
            }
            #endregion
            Recommend1Button.Source = $"ms-appx:///Assets/GameResources/Foods/{c.Recommend1}.png";
            Recommend2Button.Source = $"ms-appx:///Assets/GameResources/Foods/{c.Recommend2}.png";
            Recommend3Button.Source = $"ms-appx:///Assets/GameResources/Foods/{c.Recommend3}.png";
            Recommend4Button.Source = $"ms-appx:///Assets/GameResources/Foods/{c.Recommend4}.png";
            FoodRecipeIntroduction.Text = c.Introduce;
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
