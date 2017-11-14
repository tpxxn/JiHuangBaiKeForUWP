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
    public sealed partial class GoodTurfDialog : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Global.FrameTitle.Text = "物品详情";
            if (e.Parameter != null)
            {
                LoadData((GoodTurf)e.Parameter);
            }
            var imageAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("Image");
            imageAnimation?.TryStart(GoodImage);
        }

        public GoodTurfDialog()
        {
            this.InitializeComponent();
        }

        private void LoadData(GoodTurf c)
        {
            GoodImage.Source = new BitmapImage(new Uri(c.Picture));
            GoodName.Text = c.Name;
            GoodEnName.Text = c.EnName;
            // 制作科技
            if (string.IsNullOrEmpty(c.Make))
            {
                GoodMakeStackPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                GoodMakePicButton.Source = StringProcess.GetGameResourcePath(c.Make);
            }
            //草皮纹理
            GoodSourceTextureWrapPanel.Source = new BitmapImage(new Uri(StringProcess.GetGameResourcePath(c.Texture)));
            GoodIntroduction.Text = c.Introduction;
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

        private async void Good_Jump_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var picturePath = ((PicButton)sender).Source;
            var rootFrame = Global.RootFrame;
            var frameTitle = Global.FrameTitle;
            await Global.SetAutoSuggestBoxItem();
            foreach (var suggestBoxItem in Global.AutoSuggestBoxItemSource)
            {
                if (picturePath != suggestBoxItem.Picture) continue;
                string[] extraData = { suggestBoxItem.SourcePath, suggestBoxItem.Picture }; ;
                frameTitle.Text = "科技";
                Global.PageJump(3);
                rootFrame.Navigate(typeof(SciencePage), extraData);
                Global.PageStack.Push(new PageStackItem { TypeName = typeof(SciencePage), Object = extraData });
            }
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
