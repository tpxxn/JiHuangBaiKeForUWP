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
        private readonly ObservableCollection<NatureBiomes> _naturalBiomesData = new ObservableCollection<NatureBiomes>();
        private readonly ObservableCollection<NatureSmallPlant> _naturalSmallPlantData = new ObservableCollection<NatureSmallPlant>();
        private readonly ObservableCollection<NatureTree> _naturalTreesData = new ObservableCollection<NatureTree>();
        private readonly ObservableCollection<NatureCreatureNest> _naturalCreatureNestData = new ObservableCollection<NatureCreatureNest>();

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Global.FrameTitle.Text = "自然";
            if (e.NavigationMode == NavigationMode.Back)
            {
                BiomesEntranceTransition.FromVerticalOffset = 0;
                SmallPlantEntranceTransition.FromVerticalOffset = 0;
                TreesEntranceTransition.FromVerticalOffset = 0;
                CreatureNestEntranceTransition.FromVerticalOffset = 0;
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
                    case "NaturalSmallPlants":
                        SmallPlantExpander.IsExPanded = true;
                        OnNavigatedToNaturalSmallPlantDialog(NaturalSmallPlantGridView, _naturalSmallPlantData, _e);
                        break;
                    case "NaturalTrees":
                        TreesExpander.IsExPanded = true;
                        OnNavigatedToNaturalTreesDialog(NaturalTreesGridView, _naturalTreesData, _e);
                        break;
                    case "NaturalCreatureNests":
                        CreatureNestExpander.IsExPanded = true;
                        OnNavigatedToNaturalCreatureNestDialog(NaturalCreatureNestGridView, _naturalCreatureNestData, _e);
                        break;
                }
            }
        }

        private void OnNavigatedToNaturalBiomesDialog(GridView gridView, ObservableCollection<NatureBiomes> naturalCollection, string _e)
        {
            if (gridView.Items == null) return;
            foreach (var gridViewItem in naturalCollection)
            {
                var natural = gridViewItem;
                if (natural == null || natural.Picture != _e) continue;
                Frame.Navigate(typeof(NaturalBiomesDialog), natural);
                break;
            }
        }

        private void OnNavigatedToNaturalSmallPlantDialog(GridView gridView, ObservableCollection<NatureSmallPlant> naturalCollection, string _e)
        {
            if (gridView.Items == null) return;
            foreach (var gridViewItem in naturalCollection)
            {
                var natural = gridViewItem;
                if (natural == null || natural.Picture != _e) continue;
                Frame.Navigate(typeof(NaturalSmallPlantDialog), natural);
                break;
            }
        }

        private void OnNavigatedToNaturalTreesDialog(GridView gridView, ObservableCollection<NatureTree> naturalCollection, string _e)
        {
            if (gridView.Items == null) return;
            foreach (var gridViewItem in naturalCollection)
            {
                var natural = gridViewItem;
                if (natural == null || natural.Picture != _e) continue;
                Frame.Navigate(typeof(NaturalTreesDialog), natural);
                break;
            }
        }

        private void OnNavigatedToNaturalCreatureNestDialog(GridView gridView, ObservableCollection<NatureCreatureNest> naturalCollection, string _e)
        {
            if (gridView.Items == null) return;
            foreach (var gridViewItem in naturalCollection)
            {
                var natural = gridViewItem;
                if (natural == null || natural.Picture != _e) continue;
                Frame.Navigate(typeof(NaturalCreatureNestDialog), natural);
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
            foreach (var natureBiomesItems in natural.Biomes.NatureBiomes)
            {
                natureBiomesItems.Picture = StringProcess.GetGameResourcePath(natureBiomesItems.Picture);
                _naturalBiomesData.Add(natureBiomesItems);
            }
            foreach (var natureSmallPlantItems in natural.SmallPlants.NatureSmallPlant)
            {
                natureSmallPlantItems.Picture = StringProcess.GetGameResourcePath(natureSmallPlantItems.Picture);
                _naturalSmallPlantData.Add(natureSmallPlantItems);
            }
            foreach (var natureTreesItems in natural.Trees.NatureTree)
            {
                natureTreesItems.Picture = StringProcess.GetGameResourcePath(natureTreesItems.Picture);
                _naturalTreesData.Add(natureTreesItems);
            }
            foreach (var natureCreatureNestItems in natural.CreatureNests.NatureCreatureNest)
            {
                natureCreatureNestItems.Picture = StringProcess.GetGameResourcePath(natureCreatureNestItems.Picture);
                _naturalCreatureNestData.Add(natureCreatureNestItems);
            }
        }

        private void NaturalBiomesGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (((GridView)sender).ContainerFromItem(e.ClickedItem) is GridViewItem container)
            {
                var root = (FrameworkElement)container.ContentTemplateRoot;
                var image = (UIElement)root.FindName("Image");
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Image", image);
            }
            var item = (NatureBiomes)e.ClickedItem;
            Frame.Navigate(typeof(NaturalBiomesDialog), item);
            Global.PageStack.Push(new PageStackItem { SourcePageType = typeof(NaturalBiomesDialog), Parameter = item });
        }

        private void NaturalSmallPlantGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (((GridView)sender).ContainerFromItem(e.ClickedItem) is GridViewItem container)
            {
                var root = (FrameworkElement)container.ContentTemplateRoot;
                var image = (UIElement)root.FindName("Image");
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Image", image);
            }
            var item = (NatureSmallPlant)e.ClickedItem;
            Frame.Navigate(typeof(NaturalSmallPlantDialog), item);
            Global.PageStack.Push(new PageStackItem { SourcePageType = typeof(NaturalSmallPlantDialog), Parameter = item });
        }

        private void NaturalTreeGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (((GridView)sender).ContainerFromItem(e.ClickedItem) is GridViewItem container)
            {
                var root = (FrameworkElement)container.ContentTemplateRoot;
                var image = (UIElement)root.FindName("Image");
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Image", image);
            }
            var item = (NatureTree)e.ClickedItem;
            Frame.Navigate(typeof(NaturalTreesDialog), item);
            Global.PageStack.Push(new PageStackItem { SourcePageType = typeof(NaturalTreesDialog), Parameter = item });
        }

        private void NaturalCreatureNestGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (((GridView)sender).ContainerFromItem(e.ClickedItem) is GridViewItem container)
            {
                var root = (FrameworkElement)container.ContentTemplateRoot;
                var image = (UIElement)root.FindName("Image");
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Image", image);
            }
            var item = (NatureCreatureNest)e.ClickedItem;
            Frame.Navigate(typeof(NaturalCreatureNestDialog), item);
            Global.PageStack.Push(new PageStackItem { SourcePageType = typeof(NaturalCreatureNestDialog), Parameter = item });
        }

        private void Expander_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (e.OriginalSource.ToString() == "Windows.UI.Xaml.Controls.Grid")
            {
                var pageStackItem = Global.PageStack.Pop();
                var pageNavigationInfo = (ViewExtraData)pageStackItem.Parameter ?? new ViewExtraData();
                if (pageNavigationInfo.ExpandedList == null)
                    pageNavigationInfo.ExpandedList = new List<string>();
                pageNavigationInfo.ExpandedList.Clear();
                foreach (var expander in RootStackPanel.Children)
                {
                    pageNavigationInfo.ExpandedList.Add(((Expander)expander).IsExPanded.ToString());
                }
                pageStackItem.Parameter = pageNavigationInfo;
                Global.PageStack.Push(pageStackItem);
            }
            else
            {
                var pageStackItemClickItem = Global.PageStack.Pop();
                var pageStackItem = Global.PageStack.Pop();
                var pageNavigationInfo = (ViewExtraData)pageStackItem.Parameter ?? new ViewExtraData();
                pageNavigationInfo.ScrollViewerVerticalOffset = RootScrollViewer.VerticalOffset;
                pageStackItem.Parameter = pageNavigationInfo;
                Global.PageStack.Push(pageStackItem);
                Global.PageStack.Push(pageStackItemClickItem);
            }
        }
    }
}
