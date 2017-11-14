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
    public sealed partial class GoodPetDialog : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Global.FrameTitle.Text = "物品详情";
            if (e.Parameter != null)
            {
                LoadData((GoodPet)e.Parameter);
            }
            var imageAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("Image");
            imageAnimation?.TryStart(GoodImage);
        }

        public GoodPetDialog()
        {
            this.InitializeComponent();
        }

        private void LoadData(GoodPet c)
        {
            GoodImage.Source = new BitmapImage(new Uri(c.Picture));
            GoodName.Text = c.Name;
            GoodEnName.Text = c.EnName;
            // 宠物死亡
            GoodDeadWrapPicButton.Source = StringProcess.GetGameResourcePath(c.Dead);
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
                        Source = StringProcess.GetGameResourcePath(picPath),
                        PictureSize = 90
                    };
                    picButton.Tapped += Good_Jump_Tapped;
                    GoodFollowWrapPanel.Children.Add(picButton);
                }
            }
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

        private static async void Good_Jump_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var picturePath = ((PicButton)sender).Source;
            var rootFrame = Global.RootFrame;
            var frameTitle = Global.FrameTitle;
            await Global.SetAutoSuggestBoxItem();
            foreach (var suggestBoxItem in Global.AutoSuggestBoxItemSource)
            {
                if (picturePath != suggestBoxItem.Picture) continue;
                string[] extraData = { suggestBoxItem.SourcePath, suggestBoxItem.Picture }; ;
                frameTitle.Text = "生物";
                Global.PageJump(4);
                rootFrame.Navigate(typeof(CreaturePage), extraData);
                Global.PageStack.Push(new PageStackItem { TypeName = typeof(CreaturePage), Object = extraData });
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
