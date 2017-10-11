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
            var parameter = (string[])e.Parameter;
            await Deserialize();
            if (parameter == null) return;
            var _e = parameter[1];
            switch (parameter[0])
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

        private void OnNavigatedToCreatureDialog(GridView gridView, ObservableCollection<Creature> creatureCollection, string _e)
        {
            if (gridView.Items == null) return;
            foreach (var gridViewItem in creatureCollection)
            {
                var creature = gridViewItem;
                if (creature == null || creature.Picture != _e) continue;
                var contentDialog = new ContentDialog
                {
                    Content = new CreaturesDialog(creature),
                    PrimaryButtonText = "确定",
                    FullSizeDesired = false,
                    Style = Global.Transparent
                };
                Global.ShowDialog(contentDialog);
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
            var c = e.ClickedItem as Creature;
            var contentDialog = new ContentDialog
            {
                Content = new CreaturesDialog(c),
                PrimaryButtonText = "确定",
                FullSizeDesired = false,
                Style = Global.Transparent
            };
            Global.ShowDialog(contentDialog);
        }

    }
}
