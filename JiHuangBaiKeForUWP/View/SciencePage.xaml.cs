using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class SciencePage : Page
    {
        private readonly ObservableCollection<Science> _scienceToolData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceLightData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceNauticalData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceSurvivalData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceFoodData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceTechnologyData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceFightData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceStructureData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceRefineData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceMagicData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceDressData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceAncientData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceBookData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceShadowData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceCritterData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceSculptData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceCartographyData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceOfferingsData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceVolcanoData = new ObservableCollection<Science>();

        public SciencePage()
        {
            this.InitializeComponent();
            if (Global.GameVersion != 4)
            {
                NauticalExpander.Visibility = Visibility.Collapsed;
                VolcanoExpander.Visibility = Visibility.Collapsed;
            }
            else
            {
                AncientExpander.Visibility = Visibility.Collapsed;
            }
            if (Global.GameVersion != 0 && Global.GameVersion != 1)
            {
                ShadowExpander.Visibility = Visibility.Collapsed;
                CritterExpaner.Visibility = Visibility.Collapsed;
                SculptExpander.Visibility = Visibility.Collapsed;
                CartographyExpander.Visibility = Visibility.Collapsed;
                OfferingsExpander.Visibility = Visibility.Collapsed;
            }
            if (Global.GameVersion == 1)
            {
                LightExpanderTextBolck.Text = "点燃";
                DressExpanderTextBolck.Text = "服装";
                ShadowExpanderTextBolck.Text = "影子";
                CritterExpanderTextBolck.Text = "小动物";
                CartographyExpanderTextBolck.Text = "制图学";
                OfferingsExpanderTextBolck.Text = "贡品";
            }
            Deserialize();
        }
        public async void Deserialize()
        {
            Uri uri;
            const string fileName = "Sciences.json";
            var folderExists = await Global.ApplicationFolder.TryGetItemAsync(Global.BuiltInGameVersionJsonFolder[Global.GameVersion]);
            if (folderExists == null)
            {
                uri = new Uri("ms-appx:///Json/" + Global.BuiltInGameVersionJsonFolder[Global.GameVersion] + "/" +
                              fileName);
            }
            else
            {
                uri = new Uri(Global.ApplicationFolder.Path + "/" + Global.BuiltInGameVersionJsonFolder[Global.GameVersion] + "/" + fileName);
            }
            var storageFile = await StorageFile.GetFileFromApplicationUriAsync(uri);
            var str = await FileIO.ReadTextAsync(storageFile);
            var science = JsonConvert.DeserializeObject<ScienceRootObject>(str);
            foreach (var scienceToolItems in science.Tool.Science)
            {
                _scienceToolData.Add(scienceToolItems);
            }
            foreach (var scienceToolItems in _scienceToolData)
            {
                scienceToolItems.Picture = $"ms-appx:///Assets/GameResources/Sciences/{scienceToolItems.Picture}.png";
            }
            foreach (var scienceLightItems in science.Light.Science)
            {
                _scienceLightData.Add(scienceLightItems);
            }
            foreach (var scienceLightItems in _scienceLightData)
            {
                scienceLightItems.Picture = $"ms-appx:///Assets/GameResources/Sciences/{scienceLightItems.Picture}.png";
            }
            foreach (var scienceNauticalItems in science.Nautical.Science)
            {
                _scienceNauticalData.Add(scienceNauticalItems);
            }
            foreach (var scienceNauticalItems in _scienceNauticalData)
            {
                scienceNauticalItems.Picture = $"ms-appx:///Assets/GameResources/Sciences/{scienceNauticalItems.Picture}.png";
            }
            foreach (var scienceSurvivalItems in science.Survival.Science)
            {
                _scienceSurvivalData.Add(scienceSurvivalItems);
            }
            foreach (var scienceSurvivalItems in _scienceSurvivalData)
            {
                scienceSurvivalItems.Picture = $"ms-appx:///Assets/GameResources/Sciences/{scienceSurvivalItems.Picture}.png";
            }
            foreach (var scienceFoodItems in science.Food.Science)
            {
                _scienceFoodData.Add(scienceFoodItems);
            }
            foreach (var scienceFoodItems in _scienceFoodData)
            {
                scienceFoodItems.Picture = $"ms-appx:///Assets/GameResources/Sciences/{scienceFoodItems.Picture}.png";
            }
            foreach (var scienceTechnologyItems in science.Technology.Science)
            {
                _scienceTechnologyData.Add(scienceTechnologyItems);
            }
            foreach (var scienceTechnologyItems in _scienceTechnologyData)
            {
                scienceTechnologyItems.Picture = $"ms-appx:///Assets/GameResources/Sciences/{scienceTechnologyItems.Picture}.png";
            }
            foreach (var scienceFightItems in science.Fight.Science)
            {
                _scienceFightData.Add(scienceFightItems);
            }
            foreach (var scienceFightItems in _scienceFightData)
            {
                scienceFightItems.Picture = $"ms-appx:///Assets/GameResources/Sciences/{scienceFightItems.Picture}.png";
            }
            foreach (var scienceStructureItems in science.Structure.Science)
            {
                _scienceStructureData.Add(scienceStructureItems);
            }
            foreach (var scienceStructureItems in _scienceStructureData)
            {
                scienceStructureItems.Picture = $"ms-appx:///Assets/GameResources/Sciences/{scienceStructureItems.Picture}.png";
            }
            foreach (var scienceRefineItems in science.Refine.Science)
            {
                _scienceRefineData.Add(scienceRefineItems);
            }
            foreach (var scienceRefineItems in _scienceRefineData)
            {
                scienceRefineItems.Picture = $"ms-appx:///Assets/GameResources/Sciences/{scienceRefineItems.Picture}.png";
            }
            foreach (var scienceMagicItems in science.Magic.Science)
            {
                _scienceMagicData.Add(scienceMagicItems);
            }
            foreach (var scienceMagicItems in _scienceMagicData)
            {
                scienceMagicItems.Picture = $"ms-appx:///Assets/GameResources/Sciences/{scienceMagicItems.Picture}.png";
            }
            foreach (var scienceDressItems in science.Dress.Science)
            {
                _scienceDressData.Add(scienceDressItems);
            }
            foreach (var scienceDressItems in _scienceDressData)
            {
                scienceDressItems.Picture = $"ms-appx:///Assets/GameResources/Sciences/{scienceDressItems.Picture}.png";
            }
            foreach (var scienceAncientItems in science.Ancient.Science)
            {
                _scienceAncientData.Add(scienceAncientItems);
            }
            foreach (var scienceAncientItems in _scienceAncientData)
            {
                scienceAncientItems.Picture = $"ms-appx:///Assets/GameResources/Sciences/{scienceAncientItems.Picture}.png";
            }
            foreach (var scienceBookItems in science.Book.Science)
            {
                _scienceBookData.Add(scienceBookItems);
            }
            foreach (var scienceBookItems in _scienceBookData)
            {
                scienceBookItems.Picture = $"ms-appx:///Assets/GameResources/Sciences/{scienceBookItems.Picture}.png";
            }
            foreach (var scienceShadowItems in science.Shadow.Science)
            {
                _scienceShadowData.Add(scienceShadowItems);
            }
            foreach (var scienceShadowItems in _scienceShadowData)
            {
                scienceShadowItems.Picture = $"ms-appx:///Assets/GameResources/Sciences/{scienceShadowItems.Picture}.png";
            }
            foreach (var scienceCritterItems in science.Critter.Science)
            {
                _scienceCritterData.Add(scienceCritterItems);
            }
            foreach (var scienceCritterItems in _scienceCritterData)
            {
                scienceCritterItems.Picture = $"ms-appx:///Assets/GameResources/Sciences/{scienceCritterItems.Picture}.png";
            }
            foreach (var scienceSculptItems in science.Sculpt.Science)
            {
                _scienceSculptData.Add(scienceSculptItems);
            }
            foreach (var scienceSculptItems in _scienceSculptData)
            {
                scienceSculptItems.Picture = $"ms-appx:///Assets/GameResources/Sciences/{scienceSculptItems.Picture}.png";
            }
            foreach (var scienceCartographyItems in science.Cartography.Science)
            {
                _scienceCartographyData.Add(scienceCartographyItems);
            }
            foreach (var scienceCartographyItems in _scienceCartographyData)
            {
                scienceCartographyItems.Picture = $"ms-appx:///Assets/GameResources/Sciences/{scienceCartographyItems.Picture}.png";
            }
            foreach (var scienceOfferingsItems in science.Offerings.Science)
            {
                _scienceOfferingsData.Add(scienceOfferingsItems);
            }
            foreach (var scienceOfferingsItems in _scienceOfferingsData)
            {
                scienceOfferingsItems.Picture = $"ms-appx:///Assets/GameResources/Sciences/{scienceOfferingsItems.Picture}.png";
            }
            foreach (var scienceVolcanoItems in science.Volcano.Science)
            {
                _scienceVolcanoData.Add(scienceVolcanoItems);
            }
            foreach (var scienceVolcanoItems in _scienceVolcanoData)
            {
                scienceVolcanoItems.Picture = $"ms-appx:///Assets/GameResources/Sciences/{scienceVolcanoItems.Picture}.png";
            }
        }

        private void ScienceGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var c = e.ClickedItem as Science;
            var contentDialog = new ContentDialog
            {
                Content = new ScienceDialog(c),
                PrimaryButtonText = "确定",
                FullSizeDesired = false,
                Style = Global.Transparent
            };
            Global.ShowDialog(contentDialog, ScienceStackPanel);
        }
    }
}
