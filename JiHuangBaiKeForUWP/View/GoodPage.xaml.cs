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
    public sealed partial class GoodPage : Page
    {
        private readonly ObservableCollection<GoodMaterial> _goodMaterialData = new ObservableCollection<GoodMaterial>();
        private readonly ObservableCollection<GoodEquipment> _goodEquipmentData = new ObservableCollection<GoodEquipment>();
        private readonly ObservableCollection<GoodSapling> _goodSaplingData = new ObservableCollection<GoodSapling>();
        private readonly ObservableCollection<GoodCreatures> _goodCreaturesData = new ObservableCollection<GoodCreatures>();
        private readonly ObservableCollection<Good> _goodTrinketsData = new ObservableCollection<Good>();
        private readonly ObservableCollection<GoodTurf> _goodTurfData = new ObservableCollection<GoodTurf>();
        private readonly ObservableCollection<GoodPet> _goodPetData = new ObservableCollection<GoodPet>();
        private readonly ObservableCollection<GoodUnlock> _goodUnlockData = new ObservableCollection<GoodUnlock>();
        private readonly ObservableCollection<Good> _goodHallowedNightsData = new ObservableCollection<Good>();
        private readonly ObservableCollection<Good> _goodWintersFeastData = new ObservableCollection<Good>();
        private readonly ObservableCollection<Good> _goodYearOfTheGobblerData = new ObservableCollection<Good>();
        private readonly ObservableCollection<Good> _goodComponentData = new ObservableCollection<Good>();
        private readonly ObservableCollection<Good> _goodOthersData = new ObservableCollection<Good>();

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Global.FrameTitle.Text = "物品";
            if (e.NavigationMode == NavigationMode.Back)
            {
                MaterialEntranceTransition.FromVerticalOffset = 0;
				EquipmentEntranceTransition.FromVerticalOffset = 0;
				SaplingEntranceTransition.FromVerticalOffset = 0;
				CreaturesEntranceTransition.FromVerticalOffset = 0;
				TrinketsEntranceTransition.FromVerticalOffset = 0;
				TurfEntranceTransition.FromVerticalOffset = 0;
				PetEntranceTransition.FromVerticalOffset = 0;
				UnlockEntranceTransition.FromVerticalOffset = 0;
				HallowedNightsEntranceTransition.FromVerticalOffset = 0;
				WintersFeastEntranceTransition.FromVerticalOffset = 0;
				YearOfTheGobblerEntranceTransition.FromVerticalOffset = 0;
				ComponentEntranceTransition.FromVerticalOffset = 0;
				OthersEntranceTransition.FromVerticalOffset = 0;
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
            var parameter = (List<string>)e.Parameter;
            await Deserialize();
            if (parameter == null || parameter.Count == 0) return;
            if (parameter.Count > 3)
            {
                //展开之前展开的Expander
                for (var i = 3; i < parameter.Count; i++)
                {
                    ((Expander) RootStackPanel.Children[i - 3]).IsExPanded = parameter[i] == "True";
                }
                //ScrollViewer滚动到指定位置
                if (!string.IsNullOrEmpty(parameter[2]))
                {
                    RootScrollViewer.UpdateLayout();
                    RootScrollViewer.ChangeView(null, double.Parse(parameter[2]), null, true);
                }
            }
            //导航到指定页面
            var _e = parameter[1];
            switch (parameter[0])
            {
                case "GoodMaterial":
                    MaterialExpander.IsExPanded = true;
                    OnNavigatedToGoodMaterialDialog(_e);
                    break;
                case "GoodEquipment":
                    EquipmentExpander.IsExPanded = true;
                    OnNavigatedToGoodEquipmentDialog(_e);
                    break;
                case "GoodSapling":
                    SaplingExpander.IsExPanded = true;
                    OnNavigatedToGoodSaplingDialog(_e);
                    break;
                case "GoodCreatures":
                    CreaturesExpander.IsExPanded = true;
                    OnNavigatedToGoodCreaturesDialog(_e);
                    break;
                case "GoodTrinkets":
                    TrinketsExpander.IsExPanded = true;
                    OnNavigatedToGoodDialog(GoodTrinketsGridView, _goodTrinketsData, _e);
                    break;
                case "GoodTurf":
                    TurfExpander.IsExPanded = true;
                    OnNavigatedToGoodTurfDialog(_e);
                    break;
                case "GoodPet":
                    PetExpander.IsExPanded = true;
                    OnNavigatedToGoodPetDialog(_e);
                    break;
                case "GoodUnlock":
                    UnlockExpander.IsExPanded = true;
                    OnNavigatedToGoodUnlockDialog(_e);
                    break;
                case "GoodHallowedNights":
                    HallowedNightsExpander.IsExPanded = true;
                    OnNavigatedToGoodDialog(GoodHallowedNightsGridView, _goodHallowedNightsData, _e);
                    break;
                case "GoodWintersFeast":
                    WintersFeastExpander.IsExPanded = true;
                    OnNavigatedToGoodDialog(GoodWintersFeastGridView, _goodWintersFeastData, _e);
                    break;
                case "GoodYearOfTheGobbler":
                    YearOfTheGobblerExpander.IsExPanded = true;
                    OnNavigatedToGoodDialog(GoodYearOfTheGobblerGridView, _goodYearOfTheGobblerData, _e);
                    break;
                case "GoodComponent":
                    ComponentExpander.IsExPanded = true;
                    OnNavigatedToGoodDialog(GoodComponentGridView, _goodComponentData, _e);
                    break;
                case "GoodOthers":
                    GoodOthersExpander.IsExPanded = true;
                    OnNavigatedToGoodDialog(GoodOthersGridView, _goodOthersData, _e);
                    break;
            }
        }

        private void OnNavigatedToGoodMaterialDialog(string _e)
        {
            if (GoodMaterialGridView.Items == null) return;
            foreach (var gridViewItem in _goodMaterialData)
            {
                var good = gridViewItem;
                if (good == null || good.Picture != _e) continue;
                Frame.Navigate(typeof(GoodMaterialDialog), good);
                break;
            }
        }

        private void OnNavigatedToGoodEquipmentDialog(string _e)
        {
            if (GoodEquipmentGridView.Items == null) return;
            foreach (var gridViewItem in _goodEquipmentData)
            {
                var good = gridViewItem;
                if (good == null || good.Picture != _e) continue;
                Frame.Navigate(typeof(GoodEquipmentDialog), good);
                break;
            }
        }

        private void OnNavigatedToGoodSaplingDialog(string _e)
        {
            if (GoodSaplingGridView.Items == null) return;
            foreach (var gridViewItem in _goodSaplingData)
            {
                var good = gridViewItem;
                if (good == null || good.Picture != _e) continue;
                Frame.Navigate(typeof(GoodSaplingDialog), good);
                break;
            }
        }

        private void OnNavigatedToGoodCreaturesDialog(string _e)
        {
            if (GoodCreaturesGridView.Items == null) return;
            foreach (var gridViewItem in _goodCreaturesData)
            {
                var good = gridViewItem;
                if (good == null || good.Picture != _e) continue;
                Frame.Navigate(typeof(GoodCreaturesDialog), good);
                break;
            }
        }

        private void OnNavigatedToGoodTurfDialog(string _e)
        {
            if (GoodTurfGridView.Items == null) return;
            foreach (var gridViewItem in _goodTurfData)
            {
                var good = gridViewItem;
                if (good == null || good.Picture != _e) continue;
                Frame.Navigate(typeof(GoodTurfDialog), good);
                break;
            }
        }

        private void OnNavigatedToGoodPetDialog(string _e)
        {
            if (GoodPetGridView.Items == null) return;
            foreach (var gridViewItem in _goodPetData)
            {
                var good = gridViewItem;
                if (good == null || good.Picture != _e) continue;
                Frame.Navigate(typeof(GoodPetDialog), good);
                break;
            }
        }

        private void OnNavigatedToGoodUnlockDialog(string _e)
        {
            if (GoodUnlockGridView.Items == null) return;
            foreach (var gridViewItem in _goodUnlockData)
            {
                var good = gridViewItem;
                if (good == null || good.Picture != _e) continue;
                Frame.Navigate(typeof(GoodUnlockDialog), good);
                break;
            }
        }

        private void OnNavigatedToGoodDialog(GridView gridView, ObservableCollection<Good> goodCollection, string _e)
        {
            if (gridView.Items == null) return;
            foreach (var gridViewItem in goodCollection)
            {
                var good = gridViewItem;
                if (good == null || good.Picture != _e) continue;
                Frame.Navigate(typeof(GoodDialog), good);
                break;
            }
        }

        public GoodPage()
        {
            this.InitializeComponent();
            if (Global.GameVersion == 0 || Global.GameVersion == 1)
            {
                UnlockExpander.Visibility = Visibility.Collapsed;
                ComponentExpander.Visibility = Visibility.Collapsed;
            }
            else
            {
                HallowedNightsExpander.Visibility = Visibility.Collapsed;
                WintersFeastExpander.Visibility = Visibility.Collapsed;
                YearOfTheGobblerExpander.Visibility = Visibility.Collapsed;
            }
        }

        public async Task Deserialize()
        {
            _goodMaterialData.Clear();
            _goodEquipmentData.Clear();
            _goodSaplingData.Clear();
            _goodCreaturesData.Clear();
            _goodTrinketsData.Clear();
            _goodTurfData.Clear();
            _goodPetData.Clear();
            _goodUnlockData.Clear();
            _goodHallowedNightsData.Clear();
            _goodWintersFeastData.Clear();
            _goodYearOfTheGobblerData.Clear();
            _goodComponentData.Clear();
            _goodOthersData.Clear();
            var good = JsonConvert.DeserializeObject<GoodsRootObject>(await StringProcess.GetJsonString("Goods.json"));
            foreach (var goodMaterialItems in good.Material.GoodMaterial)
            {
                _goodMaterialData.Add(goodMaterialItems);
            }
            foreach (var goodMaterialItems in _goodMaterialData)
            {
                goodMaterialItems.Picture = StringProcess.GetGameResourcePath(goodMaterialItems.Picture);
            }
            foreach (var goodEquipmentItems in good.Equipment.GoodEquipment)
            {
                _goodEquipmentData.Add(goodEquipmentItems);
            }
            foreach (var goodEquipmentItems in _goodEquipmentData)
            {
                goodEquipmentItems.Picture = StringProcess.GetGameResourcePath(goodEquipmentItems.Picture);
            }
            foreach (var goodSaplingItems in good.Sapling.GoodSapling)
            {
                _goodSaplingData.Add(goodSaplingItems);
            }
            foreach (var goodSaplingItems in _goodSaplingData)
            {
                goodSaplingItems.Picture = StringProcess.GetGameResourcePath(goodSaplingItems.Picture);
            }
            foreach (var goodCreaturesItems in good.Creatures.GoodCreatures)
            {
                _goodCreaturesData.Add(goodCreaturesItems);
            }
            foreach (var goodCreaturesItems in _goodCreaturesData)
            {
                goodCreaturesItems.Picture = StringProcess.GetGameResourcePath(goodCreaturesItems.Picture);
            }
            foreach (var goodTrinketsItems in good.Trinkets.GoodTrinkets)
            {
                _goodTrinketsData.Add(goodTrinketsItems);
            }
            foreach (var goodTrinketsItems in _goodTrinketsData)
            {
                goodTrinketsItems.Picture = StringProcess.GetGameResourcePath(goodTrinketsItems.Picture);
            }
            foreach (var goodTurfItems in good.Turf.GoodTurf)
            {
                _goodTurfData.Add(goodTurfItems);
            }
            foreach (var goodTurfItems in _goodTurfData)
            {
                goodTurfItems.Picture = StringProcess.GetGameResourcePath(goodTurfItems.Picture);
            }
            foreach (var goodPetItems in good.Pet.GoodPet)
            {
                _goodPetData.Add(goodPetItems);
            }
            foreach (var goodPetItems in _goodPetData)
            {
                goodPetItems.Picture = StringProcess.GetGameResourcePath(goodPetItems.Picture);
            }
            foreach (var goodUnlockItems in good.Unlock.GoodUnlock)
            {
                _goodUnlockData.Add(goodUnlockItems);
            }
            foreach (var goodUnlockItems in _goodUnlockData)
            {
                goodUnlockItems.Picture = StringProcess.GetGameResourcePath(goodUnlockItems.Picture);
            }
            foreach (var goodHallowedNightsItems in good.HallowedNights.Good)
            {
                _goodHallowedNightsData.Add(goodHallowedNightsItems);
            }
            foreach (var goodHallowedNightsItems in _goodHallowedNightsData)
            {
                goodHallowedNightsItems.Picture = StringProcess.GetGameResourcePath(goodHallowedNightsItems.Picture);
            }
            foreach (var goodWintersFeastItems in good.WintersFeast.Good)
            {
                _goodWintersFeastData.Add(goodWintersFeastItems);
            }
            foreach (var goodWintersFeastItems in _goodWintersFeastData)
            {
                goodWintersFeastItems.Picture = StringProcess.GetGameResourcePath(goodWintersFeastItems.Picture);
            }
            foreach (var goodYearOfTheGobblerItems in good.YearOfTheGobbler.Good)
            {
                _goodYearOfTheGobblerData.Add(goodYearOfTheGobblerItems);
            }
            foreach (var goodYearOfTheGobblerItems in _goodYearOfTheGobblerData)
            {
                goodYearOfTheGobblerItems.Picture = StringProcess.GetGameResourcePath(goodYearOfTheGobblerItems.Picture);
            }
            foreach (var goodComponentItems in good.Component.Good)
            {
                _goodComponentData.Add(goodComponentItems);
            }
            foreach (var goodComponentItems in _goodComponentData)
            {
                goodComponentItems.Picture = StringProcess.GetGameResourcePath(goodComponentItems.Picture);
            }
            foreach (var goodGoodOthersItems in good.GoodOthers.Good)
            {
                _goodOthersData.Add(goodGoodOthersItems);
            }
            foreach (var goodGoodOthersItems in _goodOthersData)
            {
                goodGoodOthersItems.Picture = StringProcess.GetGameResourcePath(goodGoodOthersItems.Picture);
            }
        }

        private void GoodMaterialGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (((GridView)sender).ContainerFromItem(e.ClickedItem) is GridViewItem container)
            {
                var root = (FrameworkElement)container.ContentTemplateRoot;
                var image = (UIElement)root.FindName("Image");
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Image", image);
            }
            var item = (GoodMaterial)e.ClickedItem;
            Frame.Navigate(typeof(GoodMaterialDialog), item);
            Global.PageStack.Push(new PageStackItem { TypeName = typeof(GoodMaterialDialog), Object = item });
        }

        private void GoodEquipmentGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
			if (((GridView)sender).ContainerFromItem(e.ClickedItem) is GridViewItem container)
            {
                var root = (FrameworkElement)container.ContentTemplateRoot;
                var image = (UIElement)root.FindName("Image");
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Image", image);
            }
            var item = (GoodEquipment)e.ClickedItem;
            Frame.Navigate(typeof(GoodEquipmentDialog), item);
            Global.PageStack.Push(new PageStackItem { TypeName = typeof(GoodEquipmentDialog), Object = item });
        }

        private void GoodSaplingGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
			if (((GridView)sender).ContainerFromItem(e.ClickedItem) is GridViewItem container)
            {
                var root = (FrameworkElement)container.ContentTemplateRoot;
                var image = (UIElement)root.FindName("Image");
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Image", image);
            }
            var item = (GoodSapling)e.ClickedItem;
            Frame.Navigate(typeof(GoodSaplingDialog), item);
            Global.PageStack.Push(new PageStackItem { TypeName = typeof(GoodSaplingDialog), Object = item });
        }

        private void GoodCreaturesGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
			if (((GridView)sender).ContainerFromItem(e.ClickedItem) is GridViewItem container)
            {
                var root = (FrameworkElement)container.ContentTemplateRoot;
                var image = (UIElement)root.FindName("Image");
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Image", image);
            }
            var item = (GoodCreatures)e.ClickedItem;
            Frame.Navigate(typeof(GoodCreaturesDialog), item);
            Global.PageStack.Push(new PageStackItem { TypeName = typeof(GoodCreaturesDialog), Object = item });
        }
        
        private void GoodTurfGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
			if (((GridView)sender).ContainerFromItem(e.ClickedItem) is GridViewItem container)
            {
                var root = (FrameworkElement)container.ContentTemplateRoot;
                var image = (UIElement)root.FindName("Image");
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Image", image);
            }
            var item = (GoodTurf)e.ClickedItem;
            Frame.Navigate(typeof(GoodTurfDialog), item);
            Global.PageStack.Push(new PageStackItem { TypeName = typeof(GoodTurfDialog), Object = item });
        }

        private void GoodPetGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
			if (((GridView)sender).ContainerFromItem(e.ClickedItem) is GridViewItem container)
            {
                var root = (FrameworkElement)container.ContentTemplateRoot;
                var image = (UIElement)root.FindName("Image");
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Image", image);
            }
            var item = (GoodPet)e.ClickedItem;
            Frame.Navigate(typeof(GoodPetDialog), item);
            Global.PageStack.Push(new PageStackItem { TypeName = typeof(GoodPetDialog), Object = item });
        }

        private void GoodUnlockGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
			if (((GridView)sender).ContainerFromItem(e.ClickedItem) is GridViewItem container)
            {
                var root = (FrameworkElement)container.ContentTemplateRoot;
                var image = (UIElement)root.FindName("Image");
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Image", image);
            }
            var item = (GoodUnlock)e.ClickedItem;
            Frame.Navigate(typeof(GoodUnlockDialog), item);
            Global.PageStack.Push(new PageStackItem { TypeName = typeof(GoodUnlockDialog), Object = item });
        }

        private void GoodGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
			if (((GridView)sender).ContainerFromItem(e.ClickedItem) is GridViewItem container)
            {
                var root = (FrameworkElement)container.ContentTemplateRoot;
                var image = (UIElement)root.FindName("Image");
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Image", image);
            }
            var item = (Good)e.ClickedItem;
            Frame.Navigate(typeof(GoodDialog), item);
            Global.PageStack.Push(new PageStackItem { TypeName = typeof(GoodDialog), Object = item });
        }

        private void Expander_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (e.OriginalSource.ToString() == "Windows.UI.Xaml.Controls.Grid")
            {
                var pageStackItem = Global.PageStack.Pop();
                var pageNavigationInfo = (List<string>)pageStackItem.Object ?? new List<string>();
                if (pageNavigationInfo.Count == 0)
                    for (var i = 0; i < 3; i++)
                    {
                        pageNavigationInfo.Add(string.Empty);
                    }
                else
                    for (var i = pageNavigationInfo.Count; i > 3; i--)
                    {
                        pageNavigationInfo.RemoveAt(i - 1);
                    }
                pageNavigationInfo.AddRange(RootStackPanel.Children.Select(expander => ((Expander)expander).IsExPanded.ToString()));
                Global.PageStack.Push(new PageStackItem { TypeName = pageStackItem.TypeName, Object = pageNavigationInfo });
            }
            else
            {
                var pageStackItemClickItem = Global.PageStack.Pop();
                var pageStackItem = Global.PageStack.Pop();
                var pageNavigationInfo = (List<string>)pageStackItem.Object ?? new List<string>();
                if (pageNavigationInfo.Count > 0)
                    pageNavigationInfo[2] = RootScrollViewer.VerticalOffset.ToString();
                Global.PageStack.Push(new PageStackItem { TypeName = pageStackItem.TypeName, Object = pageNavigationInfo });
                Global.PageStack.Push(pageStackItemClickItem);
            }
        }
    }
}
