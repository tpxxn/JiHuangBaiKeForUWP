using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using JiHuangBaiKeForUWP.Model;
using JiHuangBaiKeForUWP.UserControls.Expander;
using JiHuangBaiKeForUWP.View.Dialog;
using Newtonsoft.Json;

namespace JiHuangBaiKeForUWP.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class CreaturePage : Page
    {
        private readonly ObservableCollection<Creature> _creatureLandData = new ObservableCollection<Creature>();
        private readonly ObservableCollection<Creature> _creatureOceanData = new ObservableCollection<Creature>();
        private readonly ObservableCollection<Creature> _creatureFlyData = new ObservableCollection<Creature>();
        private readonly ObservableCollection<Creature> _creatureCaveData = new ObservableCollection<Creature>();
        private readonly ObservableCollection<Creature> _creatureEvilData = new ObservableCollection<Creature>();
        private readonly ObservableCollection<Creature> _creatureOthersData = new ObservableCollection<Creature>();
        private readonly ObservableCollection<Creature> _creatureBossData = new ObservableCollection<Creature>();

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Global.FrameTitle.Text = "生物";
            if (e.NavigationMode == NavigationMode.Back)
            {
                LandEntranceTransition.FromVerticalOffset = 0;
                OceanEntranceTransition.FromVerticalOffset = 0;
                FlyEntranceTransition.FromVerticalOffset = 0;
                CaveEntranceTransition.FromVerticalOffset = 0;
                EvilEntranceTransition.FromVerticalOffset = 0;
                OthersEntranceTransition.FromVerticalOffset = 0;
                BossEntranceTransition.FromVerticalOffset = 0;
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
                //ScrollViewer滚动到指定位置
                RootScrollViewer.UpdateLayout();
                RootScrollViewer.ChangeView(null, extraData.ScrollViewerVerticalOffset, null, true);
                if (extraData.ExpandedList != null)
                {
                    //展开之前展开的Expander
                    for (var i = 0; i < extraData.ExpandedList.Count; i++)
                    {
                        ((Expander) RootStackPanel.Children[i]).IsExPanded = extraData.ExpandedList[i] == "True";
                    }

                }
                //导航到指定页面
                var _e = extraData.Picture;
                switch (extraData.Classify)
                {
                    case "CreatureLand":
                        LandExpander.IsExPanded = true;
                        OnNavigatedToCreatureDialog(CreatureLandGridView, _creatureLandData, _e);
                        break;
                    case "CreatureOcean":
                        OceanExpander.IsExPanded = true;
                        OnNavigatedToCreatureDialog(CreatureOceanGridView, _creatureOceanData, _e);
                        break;
                    case "CreatureFly":
                        FlyExpander.IsExPanded = true;
                        OnNavigatedToCreatureDialog(CreatureFlyGridView, _creatureFlyData, _e);
                        break;
                    case "CreatureCave":
                        CaveExpander.IsExPanded = true;
                        OnNavigatedToCreatureDialog(CreatureCaveGridView, _creatureCaveData, _e);
                        break;
                    case "CreatureEvil":
                        EvilExpander.IsExPanded = true;
                        OnNavigatedToCreatureDialog(CreatureEvilGridView, _creatureEvilData, _e);
                        break;
                    case "CreatureOther":
                        OthersExpander.IsExPanded = true;
                        OnNavigatedToCreatureDialog(CreatureOthersGridView, _creatureOthersData, _e);
                        break;
                    case "CreatureBoss":
                        BossExpander.IsExPanded = true;
                        OnNavigatedToCreatureDialog(CreatureBossGridView, _creatureBossData, _e);
                        break;
                }
            }
        }

        private void OnNavigatedToCreatureDialog(GridView gridView, ObservableCollection<Creature> creatureCollection, string _e)
        {
            if (gridView.Items == null) return;
            foreach (var gridViewItem in creatureCollection)
            {
                var creature = gridViewItem;
                if (creature == null || creature.Picture != _e) continue;
                Frame.Navigate(typeof(CreaturesDialog), creature);
                break;
            }
        }

        public CreaturePage()
        {
            this.InitializeComponent();
            if (Global.GameVersion != 4)
            {
                OceanExpander.Visibility = Visibility.Collapsed;
            }
            else
            {
                CaveExpander.Visibility = Visibility.Collapsed;
            }
        }

        public async Task Deserialize()
        {
            _creatureLandData.Clear();
            _creatureOceanData.Clear();
            _creatureFlyData.Clear();
            _creatureCaveData.Clear();
            _creatureEvilData.Clear();
            _creatureOthersData.Clear();
            _creatureBossData.Clear();
            var creature = JsonConvert.DeserializeObject<CreaturesRootObject>(await StringProcess.GetJsonString("Creatures.json"));
            foreach (var creatureLandItems in creature.Land.Creature)
            {
                _creatureLandData.Add(creatureLandItems);
            }
            foreach (var creatureLandItems in _creatureLandData)
            {
                creatureLandItems.Picture = StringProcess.GetGameResourcePath(creatureLandItems.Picture);
            }
            foreach (var creatureOceanItems in creature.Ocean.Creature)
            {
                _creatureOceanData.Add(creatureOceanItems);
            }
            foreach (var creatureOceanItems in _creatureOceanData)
            {
                creatureOceanItems.Picture = StringProcess.GetGameResourcePath(creatureOceanItems.Picture);
            }
            foreach (var creatureFlyItems in creature.Fly.Creature)
            {
                _creatureFlyData.Add(creatureFlyItems);
            }
            foreach (var creatureFlyItems in _creatureFlyData)
            {
                creatureFlyItems.Picture = StringProcess.GetGameResourcePath(creatureFlyItems.Picture);
            }
            foreach (var creatureCaveItems in creature.Cave.Creature)
            {
                _creatureCaveData.Add(creatureCaveItems);
            }
            foreach (var creatureCaveItems in _creatureCaveData)
            {
                creatureCaveItems.Picture = StringProcess.GetGameResourcePath(creatureCaveItems.Picture);
            }
            foreach (var creatureEvilItems in creature.Evil.Creature)
            {
                _creatureEvilData.Add(creatureEvilItems);
            }
            foreach (var creatureEvilItems in _creatureEvilData)
            {
                creatureEvilItems.Picture = StringProcess.GetGameResourcePath(creatureEvilItems.Picture);
            }
            foreach (var creatureOthersItems in creature.Others.Creature)
            {
                _creatureOthersData.Add(creatureOthersItems);
            }
            foreach (var creatureOthersItems in _creatureOthersData)
            {
                creatureOthersItems.Picture = StringProcess.GetGameResourcePath(creatureOthersItems.Picture);
            }
            foreach (var creatureBossItems in creature.Boss.Creature)
            {
                _creatureBossData.Add(creatureBossItems);
            }
            foreach (var creatureBossItems in _creatureBossData)
            {
                creatureBossItems.Picture = StringProcess.GetGameResourcePath(creatureBossItems.Picture);
            }
        }

        private void CreatureGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (((GridView)sender).ContainerFromItem(e.ClickedItem) is GridViewItem container)
            {
                var root = (FrameworkElement)container.ContentTemplateRoot;
                var image = (UIElement)root.FindName("Image");
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Image", image);
            }
            var item = (Creature)e.ClickedItem;
            Frame.Navigate(typeof(CreaturesDialog), item);
            Global.PageStack.Push(new PageStackItem { SourcePageType = typeof(CreaturesDialog), Parameter = item });
        }

        private void Expander_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (e.OriginalSource.ToString() == "Windows.UI.Xaml.Controls.Grid")
            {
                var pageStackItem = Global.PageStack.Pop();
                var pageNavigationInfo = (ViewExtraData)pageStackItem.Parameter ?? new ViewExtraData();
                pageNavigationInfo.ExpandedList?.Clear();
                foreach (var expander in RootStackPanel.Children)
                {
                    pageNavigationInfo.ExpandedList?.Add(((Expander)expander).IsExPanded.ToString());
                }
                Global.PageStack.Push(pageStackItem);
            }
            else
            {
                var pageStackItemClickItem = Global.PageStack.Pop();
                var pageStackItem = Global.PageStack.Pop();
                var pageNavigationInfo = (ViewExtraData)pageStackItem.Parameter ?? new ViewExtraData();
                pageNavigationInfo.ScrollViewerVerticalOffset = RootScrollViewer.VerticalOffset;
                Global.PageStack.Push(pageStackItem);
                Global.PageStack.Push(pageStackItemClickItem);
            }
        }
    }
}
