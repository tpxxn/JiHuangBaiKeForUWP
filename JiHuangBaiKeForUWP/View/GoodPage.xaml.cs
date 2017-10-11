using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
            var parameter = (string[])e.Parameter;
            await Deserialize();
            if (parameter == null) return;
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
                var contentDialog = new ContentDialog
                {
                    Content = new GoodMaterialDialog(good),
                    PrimaryButtonText = "确定",
                    FullSizeDesired = false,
                    Style = Global.Transparent
                };
                Global.ShowDialog(contentDialog);
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
                var contentDialog = new ContentDialog
                {
                    Content = new GoodEquipmentDialog(good),
                    PrimaryButtonText = "确定",
                    FullSizeDesired = false,
                    Style = Global.Transparent
                };
                Global.ShowDialog(contentDialog);
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
                var contentDialog = new ContentDialog
                {
                    Content = new GoodSaplingDialog(good),
                    PrimaryButtonText = "确定",
                    FullSizeDesired = false,
                    Style = Global.Transparent
                };
                Global.ShowDialog(contentDialog);
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
                var contentDialog = new ContentDialog
                {
                    Content = new GoodCreaturesDialog(good),
                    PrimaryButtonText = "确定",
                    FullSizeDesired = false,
                    Style = Global.Transparent
                };
                Global.ShowDialog(contentDialog);
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
                var contentDialog = new ContentDialog
                {
                    Content = new GoodTurfDialog(good),
                    PrimaryButtonText = "确定",
                    FullSizeDesired = false,
                    Style = Global.Transparent
                };
                Global.ShowDialog(contentDialog);
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
                var contentDialog = new ContentDialog
                {
                    Content = new GoodPetDialog(good),
                    PrimaryButtonText = "确定",
                    FullSizeDesired = false,
                    Style = Global.Transparent
                };
                Global.ShowDialog(contentDialog);
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
                var contentDialog = new ContentDialog
                {
                    Content = new GoodUnlockDialog(good),
                    PrimaryButtonText = "确定",
                    FullSizeDesired = false,
                    Style = Global.Transparent
                };
                Global.ShowDialog(contentDialog);
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
                var contentDialog = new ContentDialog
                {
                    Content = new GoodDialog(good),
                    PrimaryButtonText = "确定",
                    FullSizeDesired = false,
                    Style = Global.Transparent
                };
                Global.ShowDialog(contentDialog);
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
            var c = e.ClickedItem as GoodMaterial;
            var contentDialog = new ContentDialog
            {
                Content = new GoodMaterialDialog(c),
                PrimaryButtonText = "确定",
                FullSizeDesired = false,
                Style = Global.Transparent
            };
            Global.ShowDialog(contentDialog);
        }

        private void GoodEquipmentGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var c = e.ClickedItem as GoodEquipment;
            var contentDialog = new ContentDialog
            {
                Content = new GoodEquipmentDialog(c),
                PrimaryButtonText = "确定",
                FullSizeDesired = false,
                Style = Global.Transparent
            };
            Global.ShowDialog(contentDialog);
        }

        private void GoodSaplingGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var c = e.ClickedItem as GoodSapling;
            var contentDialog = new ContentDialog
            {
                Content = new GoodSaplingDialog(c),
                PrimaryButtonText = "确定",
                FullSizeDesired = false,
                Style = Global.Transparent
            };
            Global.ShowDialog(contentDialog);
        }

        private void GoodCreaturesGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var c = e.ClickedItem as GoodCreatures;
            var contentDialog = new ContentDialog
            {
                Content = new GoodCreaturesDialog(c),
                PrimaryButtonText = "确定",
                FullSizeDesired = false,
                Style = Global.Transparent
            };
            Global.ShowDialog(contentDialog);
        }
        
        private void GoodTurfGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var c = e.ClickedItem as GoodTurf;
            var contentDialog = new ContentDialog
            {
                Content = new GoodTurfDialog(c),
                PrimaryButtonText = "确定",
                FullSizeDesired = false,
                Style = Global.Transparent
            };
            Global.ShowDialog(contentDialog);
        }

        private void GoodPetGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var c = e.ClickedItem as GoodPet;
            var contentDialog = new ContentDialog
            {
                Content = new GoodPetDialog(c),
                PrimaryButtonText = "确定",
                FullSizeDesired = false,
                Style = Global.Transparent
            };
            Global.ShowDialog(contentDialog);
        }

        private void GoodUnlockGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var c = e.ClickedItem as GoodUnlock;
            var contentDialog = new ContentDialog
            {
                Content = new GoodUnlockDialog(c),
                PrimaryButtonText = "确定",
                FullSizeDesired = false,
                Style = Global.Transparent
            };
            Global.ShowDialog(contentDialog);
        }

        private void GoodGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var c = e.ClickedItem as Good;
            var contentDialog = new ContentDialog
            {
                Content = new GoodDialog(c),
                PrimaryButtonText = "确定",
                FullSizeDesired = false,
                Style = Global.Transparent
            };
            Global.ShowDialog(contentDialog);
        }
    }
}
