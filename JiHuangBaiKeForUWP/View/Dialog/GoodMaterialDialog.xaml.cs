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
                        Source = StringProcess.GetGameResourcePath(picPath)
                    };
                    picButton.Tapped += Good_Jump_Tapped;
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
                        Source = StringProcess.GetGameResourcePath(picPath)
                    };
                    picButton.Tapped += Good_Jump_Tapped;
                    GoodSourceCreatureWrapPanel.Children.Add(picButton);
                }
            }
            // 介绍
            GoodIntroduction.Text = c.Introduction;
            // 控制台
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

        private static async void Good_Jump_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var picturePath = ((PicButton)sender).Source;
            var rootFrame = Global.RootFrame;
            var shortName = StringProcess.GetFileName(picturePath);
            var mainPageListBoxItem = Global.MainPageListBoxItem;
            var frameTitle = Global.FrameTitle;
            await Global.SetAutoSuggestBoxItem();
            foreach (var suggestBoxItem in Global.AutoSuggestBoxItemSource)
            {
                if (picturePath != suggestBoxItem.Picture) continue;
                var picHead = shortName.Substring(0, 1);
                string[] extraData = { suggestBoxItem.SourcePath, suggestBoxItem.Picture }; ;
                switch (picHead)
                {
                    case "S":
                        frameTitle.Text = "科技";
                        mainPageListBoxItem[3].IsSelected = true;
                        rootFrame.Navigate(typeof(SciencePage), extraData);
                        break;
                    case "A":
                        frameTitle.Text = "生物";
                        mainPageListBoxItem[4].IsSelected = true;
                        rootFrame.Navigate(typeof(CreaturePage), extraData);
                        break;
                }
            }
        }
    }
}
