using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using JiHuangBaiKeForUWP.Model;
using JiHuangBaiKeForUWP.View.Dialog;
using Newtonsoft.Json;
using Windows.UI.Xaml.Media.Animation;
using JiHuangBaiKeForUWP.UserControls.Expander;

namespace JiHuangBaiKeForUWP.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class NaturalPage : Page
    {
        private readonly ObservableCollection<Nature> _naturalBiomesData = new ObservableCollection<Nature>();

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Global.FrameTitle.Text = "自然";
            if (e.NavigationMode == NavigationMode.Back)
            {
                BiomesEntranceTransition.FromVerticalOffset = 0;
            }
            if (Global.GetOsVersion() >= 16299)
            {
                var dimGrayAcrylicBrush = new AcrylicBrush
                {
                    BackgroundSource = AcrylicBackgroundSource.HostBackdrop,
                    FallbackColor = Colors.Transparent,
                    TintColor = Global.TinkColor,
                    TintOpacity = Global.TinkOpacity
                };
                RootStackPanel.Background = dimGrayAcrylicBrush;
            }
            var extraData = (ViewExtraData)e.Parameter;
            await Deserialize();
            if (extraData != null)
            {
                if (extraData.ExpandedList != null)
                {
                    //展开之前展开的Expander
                    for (var i = 0; i < extraData.ExpandedList.Count; i++)
                    {
                        ((Expander) RootStackPanel.Children[i]).IsExPanded = extraData.ExpandedList[i] == "True";
                    }
                }
                //ScrollViewer滚动到指定位置
                RootScrollViewer.UpdateLayout();
                RootScrollViewer.ChangeView(null, extraData.ScrollViewerVerticalOffset, null, true);
                //导航到指定页面
                var _e = extraData.Picture;
                switch (extraData.Classify)
                {
                    case "NaturalBiomes":
                        BiomesExpander.IsExPanded = true;
                        OnNavigatedToNaturalBiomesDialog(NaturalBiomesGridView, _naturalBiomesData, _e);
                        break;
                }
            }
        }

        private void OnNavigatedToNaturalBiomesDialog(GridView gridView, ObservableCollection<Nature> naturalCollection, string _e)
        {
            if (gridView.Items == null) return;
            foreach (var gridViewItem in naturalCollection)
            {
                var natural = gridViewItem;
                if (natural == null || natural.Picture != _e) continue;
                Frame.Navigate(typeof(NaturalDialog), natural);
                break;
            }
        }

        public NaturalPage()
        {
            this.InitializeComponent();
        }

        public async Task Deserialize()
        {
            _naturalBiomesData.Clear();
            var natural = JsonConvert.DeserializeObject<NaturalRootObject>(await StringProcess.GetJsonString("Natural.json"));
            foreach (var natureBiomesItems in natural.Biomes.Nature)
            {
                _naturalBiomesData.Add(natureBiomesItems);
            }
            foreach (var natureBiomesItems in _naturalBiomesData)
            {
                natureBiomesItems.Picture = StringProcess.GetGameResourcePath(natureBiomesItems.Picture);
            }
        }

        private void NaturalGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (((GridView)sender).ContainerFromItem(e.ClickedItem) is GridViewItem container)
            {
                var root = (FrameworkElement)container.ContentTemplateRoot;
                var image = (UIElement)root.FindName("Image");
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Image", image);
            }
            var item = (Nature)e.ClickedItem;
            Frame.Navigate(typeof(NaturalDialog), item);
            Global.PageStack.Push(new PageStackItem { SourcePageType = typeof(NaturalDialog), Parameter = item });
        }
    }
}
