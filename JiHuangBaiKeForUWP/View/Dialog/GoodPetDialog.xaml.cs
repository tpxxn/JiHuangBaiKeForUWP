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
    public sealed partial class GoodPetDialog : Page
    {
        public GoodPetDialog(GoodPet c)
        {
            this.InitializeComponent();

            GoodImage.Source = new BitmapImage(new Uri(c.Picture));
            GoodName.Text = c.Name;
            GoodEnName.Text = c.EnName;
            // 宠物死亡
            GoodDeadWrapPicButton.Source = Global.GetGameResourcePath(c.Dead);
            // 跟随宠物
            var thickness = new Thickness(5, 0, 0, 0);
            if (c.Follow == null || c.Follow.Count == 0)
            {
                GoodFollowTextBlock.Visibility = Visibility.Collapsed;
                GoodFollowWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                foreach (var picPath in c.Follow)
                {
                    var picButton = new PicButton
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = thickness,
                        Source = Global.GetGameResourcePath(picPath),
                        PictureSize = 90
                    };
                    picButton.Tapped += Good_Jump_Tapped;
                    GoodFollowWrapPanel.Children.Add(picButton);
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

        private static async void Good_Jump_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var picturePath = ((PicButton)sender).Source;
            var rootFrame = Global.RootFrame;
            var mainPageListBoxItem = Global.MainPageListBoxItem;
            var frameTitle = Global.FrameTitle;
            await Global.SetAutoSuggestBoxItem();
            foreach (var suggestBoxItem in Global.AutoSuggestBoxItemSource)
            {
                if (picturePath != suggestBoxItem.Picture) continue;
                string[] extraData = { suggestBoxItem.SourcePath, suggestBoxItem.Picture }; ;
                frameTitle.Text = "生物";
                mainPageListBoxItem[4].IsSelected = true;
                rootFrame.Navigate(typeof(CreaturePage), extraData);
            }
        }
    }
}
