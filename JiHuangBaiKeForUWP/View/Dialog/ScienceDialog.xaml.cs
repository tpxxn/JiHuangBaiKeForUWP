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
using Windows.UI.Xaml.Media.Animation;

namespace JiHuangBaiKeForUWP.View.Dialog
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ScienceDialog : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Global.GetOsVersion() >= 16299)
            {
                var dimGrayAcrylicBrush = new AcrylicBrush
                {
                    BackgroundSource = AcrylicBackgroundSource.HostBackdrop,
                    FallbackColor = Colors.Transparent,
                    TintColor = Global.TinkColor,
                    TintOpacity = Global.TinkOpacity
                };
                RootScrollViewer.Background = dimGrayAcrylicBrush;
            }
            base.OnNavigatedTo(e);
            Global.FrameTitle.Text = "科技详情";
            if (e.Parameter != null)
            {
                LoadData((Science)e.Parameter);
            }
            var imageAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("Image");
            imageAnimation?.TryStart(ScienceImage);
        }

        private string _unlockCharcter;
        public ScienceDialog()
        {
            this.InitializeComponent();
        }

        private void LoadData(Science c)
        {
            ScienceImage.Source = new BitmapImage(new Uri(c.Picture));
            ScienceName.Text = c.Name;
            ScienceEnName.Text = c.EnName;
            Need1PicButton.Source = StringProcess.GetGameResourcePath(c.Need1);
            Need1PicButton.Text = $"×{c.Need1Value}";
            if (c.Need2 != null)
            {
                Need2PicButton.Source = StringProcess.GetGameResourcePath(c.Need2);
                Need2PicButton.Text = $"×{c.Need2Value}";
                Need2PicButton.Visibility = Visibility.Visible;
            }
            if (c.Need3 != null)
            {
                Need3PicButton.Source = StringProcess.GetGameResourcePath(c.Need3);
                Need3PicButton.Text = $"×{c.Need3Value}";
                Need3PicButton.Visibility = Visibility.Visible;
            }
            if (c.Unlock == null && c.UnlockCharcter == null && c.UnlockBlueprint == null)
            {
                ScienceUnlockStackPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (c.Unlock != null && c.Unlock.Count > 0)
                {
                    UnlockPicButton.Visibility = Visibility.Visible;
                    UnlockPicButton.Source = StringProcess.GetGameResourcePath(c.Unlock[0]);
                    if (c.Unlock.Count == 2)
                    {
                        Unlock2PicButton.Visibility = Visibility.Visible;
                        Unlock2PicButton.Source = StringProcess.GetGameResourcePath(c.Unlock[1]);
                    }
                }
                if (c.UnlockCharcter != null)
                {
                    UnlockCharcterButton.Visibility = Visibility.Visible;
                    UnlockCharcterImage.Source = new BitmapImage(new Uri(StringProcess.GetGameResourcePath(c.UnlockCharcter)));
                    _unlockCharcter = StringProcess.GetGameResourcePath(c.UnlockCharcter);
                }
                if (c.UnlockBlueprint != null)
                {
                    UnlockBlueprintPicButton.Visibility = Visibility.Visible;
                    UnlockBlueprintPicButton.Source = StringProcess.GetGameResourcePath(c.UnlockBlueprint);
                }
            }
            ScienceIntroduction.Text = c.Introduction;
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
            if (string.IsNullOrEmpty(ConsoleNum.Text) || double.Parse(ConsoleNum.Text) == 0)
            {
                ConsoleNum.Text = "1";
            }
            dataPackage.SetText(ConsolePre.Text + ConsoleNum.Text + ")");
            Clipboard.SetContent(dataPackage);
        }

        private async void Science_Jump_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var picturePath = ((PicButton)sender).Source;
            var rootFrame = Global.RootFrame;
            var shortName = StringProcess.GetFileName(picturePath);
            var frameTitle = Global.FrameTitle;
            await Global.SetAutoSuggestBoxItem();
            foreach (var suggestBoxItem in Global.AutoSuggestBoxItemSource)
            {
                if (picturePath != suggestBoxItem.Picture) continue;
                var picHead = shortName.Substring(0, 1);
                var viewExtraData = new ViewExtraData { Classify = suggestBoxItem.SourcePath, Picture = suggestBoxItem.Picture };
                switch (picHead)
                {
                    case "F":
                        frameTitle.Text = "食物";
                        Global.PageJump(1);
                        rootFrame.Navigate(typeof(FoodPage), viewExtraData);
                        Global.PageStack.Push(new PageStackItem { SourcePageType = typeof(FoodPage), Parameter = viewExtraData });
                        break;
                    case "S":
                        rootFrame.Navigate(typeof(SciencePage), viewExtraData);
                        Global.PageStack.Push(new PageStackItem { SourcePageType = typeof(SciencePage), Parameter = viewExtraData });
                        break;
                    case "G":
                        frameTitle.Text = "物品";
                        Global.PageJump(6);
                        rootFrame.Navigate(typeof(GoodPage), viewExtraData);
                        Global.PageStack.Push(new PageStackItem { SourcePageType = typeof(GoodPage), Parameter = viewExtraData });
                        break;
                }
            }
        }

        private async void Science_CharacterJump_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var picturePath = _unlockCharcter;
            var rootFrame = Global.RootFrame;
            var frameTitle = Global.FrameTitle;
            await Global.SetAutoSuggestBoxItem();
            foreach (var suggestBoxItem in Global.AutoSuggestBoxItemSource)
            {
                if (picturePath != suggestBoxItem.Picture) continue;
                var viewExtraData = new ViewExtraData { Classify = suggestBoxItem.SourcePath, Picture = suggestBoxItem.Picture };
                frameTitle.Text = "人物";
                Global.PageJump(0);
                rootFrame.Navigate(typeof(CharacterPage), viewExtraData);
                Global.PageStack.Push(new PageStackItem { SourcePageType = typeof(CharacterPage), Parameter = viewExtraData });
            }
        }

        private void ScrollViewer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var list = new List<DependencyObject>();
            Global.FindChildren(list, (ScrollViewer)sender);
            var scrollViewerGrid = (from dependencyObject in list where dependencyObject.ToString() == "Windows.UI.Xaml.Controls.Grid" select dependencyObject.GetHashCode()).FirstOrDefault();
            if (e.OriginalSource.GetHashCode() == scrollViewerGrid)
            {
                Global.App_BackRequested();
            }
        }
    }
}
