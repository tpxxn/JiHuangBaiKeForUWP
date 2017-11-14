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
using Windows.UI.Xaml.Navigation;
using JiHuangBaiKeForUWP.Model;
using JiHuangBaiKeForUWP.View.Dialog;
using Newtonsoft.Json;
using Windows.UI.Xaml.Media.Animation;

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

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Global.FrameTitle.Text = "科技";
			if (e.NavigationMode == NavigationMode.Back)
            {
                ToolEntranceTransition.FromVerticalOffset = 0;
                LightEntranceTransition.FromVerticalOffset = 0;
				NauticalEntranceTransition.FromVerticalOffset = 0;
				SurvivalEntranceTransition.FromVerticalOffset = 0;
				FoodEntranceTransition.FromVerticalOffset = 0;
				TechnologyEntranceTransition.FromVerticalOffset = 0;
				FightEntranceTransition.FromVerticalOffset = 0;
				StructureEntranceTransition.FromVerticalOffset = 0;
				RefineEntranceTransition.FromVerticalOffset = 0;
				MagicEntranceTransition.FromVerticalOffset = 0;
				DressEntranceTransition.FromVerticalOffset = 0;
				AncientEntranceTransition.FromVerticalOffset = 0;
				BookEntranceTransition.FromVerticalOffset = 0;
				ShadowEntranceTransition.FromVerticalOffset = 0;
				CritterEntranceTransition.FromVerticalOffset = 0;
				SculptEntranceTransition.FromVerticalOffset = 0;
				CartographyEntranceTransition.FromVerticalOffset = 0;
				OfferingsEntranceTransition.FromVerticalOffset = 0;
				VolcanoEntranceTransition.FromVerticalOffset = 0;
            }
            if (Global.GetOsVersion() >= 16299)
            {
                var dimGrayAcrylicBrush = new AcrylicBrush
                {
                    BackgroundSource = AcrylicBackgroundSource.HostBackdrop,
                    FallbackColor = Colors.Transparent,
                    TintColor = Color.FromArgb(255, 105, 105, 105),
                    TintOpacity = 0.3
                };
                ScienceStackPanel.Background = dimGrayAcrylicBrush;
            }
            var parameter = (string[])e.Parameter;
            await Deserialize();
            if (parameter == null) return;
            var _e = parameter[1];
            switch (parameter[0])
            {
                case "ScienceTool":
                    ToolExpander.IsExPanded = true;
                    OnNavigatedToScienceDialog(ScienceToolGridView,_scienceToolData,_e);
                    break;
                case "ScienceLight":
                    LightExpander.IsExPanded = true;
                    OnNavigatedToScienceDialog(ScienceLightGridView,_scienceLightData,_e);
                    break;
                case "ScienceNautical":
                    NauticalExpander.IsExPanded = true;
                    OnNavigatedToScienceDialog(ScienceNauticalGridView,_scienceNauticalData,_e);
                    break;
                case "ScienceSurvival":
                    SurvivalExpander.IsExPanded = true;
                    OnNavigatedToScienceDialog(ScienceSurvivalGridView,_scienceSurvivalData,_e);
                    break;
                case "ScienceFood":
                    FoodExpander.IsExPanded = true;
                    OnNavigatedToScienceDialog(ScienceFoodGridView,_scienceFoodData,_e);
                    break;
                case "ScienceTechnology":
                    TechnologyExpander.IsExPanded = true;
                    OnNavigatedToScienceDialog(ScienceTechnologyGridView,_scienceTechnologyData,_e);
                    break;
                case "ScienceFight":
                    FightExpander.IsExPanded = true;
                    OnNavigatedToScienceDialog(ScienceFightGridView,_scienceFightData,_e);
                    break;
                case "ScienceStructure":
                    StructuresExpander.IsExPanded = true;
                    OnNavigatedToScienceDialog(ScienceStructureGridView,_scienceStructureData,_e);
                    break;
                case "ScienceRefine":
                    RefineExpander.IsExPanded = true;
                    OnNavigatedToScienceDialog(ScienceRefineGridView,_scienceRefineData,_e);
                    break;
                case "ScienceMagic":
                    MagicExpander.IsExPanded = true;
                    OnNavigatedToScienceDialog(ScienceMagicGridView,_scienceMagicData,_e);
                    break;
                case "ScienceDress":
                    DressExpander.IsExPanded = true;
                    OnNavigatedToScienceDialog(ScienceDressGridView,_scienceDressData,_e);
                    break;
                case "ScienceAncient":
                    AncientExpander.IsExPanded = true;
                    OnNavigatedToScienceDialog(ScienceAncientGridView,_scienceAncientData,_e);
                    break;
                case "ScienceBook":
                    BooksExpander.IsExPanded = true;
                    OnNavigatedToScienceDialog(ScienceBookGridView,_scienceBookData,_e);
                    break;
                case "ScienceShadow":
                    ShadowExpander.IsExPanded = true;
                    OnNavigatedToScienceDialog(ScienceShadowGridView,_scienceShadowData,_e);
                    break;
                case "ScienceCritter":
                    CritterExpaner.IsExPanded = true;
                    OnNavigatedToScienceDialog(ScienceCritterGridView,_scienceCritterData,_e);
                    break;
                case "ScienceSculpt":
                    SculptExpander.IsExPanded = true;
                    OnNavigatedToScienceDialog(ScienceSculptGridView,_scienceSculptData,_e);
                    break;
                case "ScienceCartography":
                    CartographyExpander.IsExPanded = true;
                    OnNavigatedToScienceDialog(ScienceCartographyGridView,_scienceCartographyData,_e);
                    break;
                case "ScienceOfferings":
                    OfferingsExpander.IsExPanded = true;
                    OnNavigatedToScienceDialog(ScienceOfferingsGridView,_scienceOfferingsData,_e);
                    break;
                case "ScienceVolcano":
                    VolcanoExpander.IsExPanded = true;
                    OnNavigatedToScienceDialog(ScienceVolcanoGridView,_scienceVolcanoData,_e);
                    break;
            }
        }

        private void OnNavigatedToScienceDialog(GridView gridView, ObservableCollection<Science> scienceCollection,string _e)
        {
            if (gridView.Items == null) return;
            foreach (var gridViewItem in scienceCollection)
            {
                var science = gridViewItem;
                if (science == null || science.Picture != _e) continue;
                Frame.Navigate(typeof(ScienceDialog), science);
                break;
            }
        }

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
        }
        public async Task Deserialize()
        {
            _scienceToolData.Clear();
            _scienceLightData.Clear();
            _scienceNauticalData.Clear();
            _scienceSurvivalData.Clear();
            _scienceFoodData.Clear();
            _scienceTechnologyData.Clear();
            _scienceFightData.Clear();
            _scienceStructureData.Clear();
            _scienceRefineData.Clear();
            _scienceMagicData.Clear();
            _scienceDressData.Clear();
            _scienceAncientData.Clear();
            _scienceBookData.Clear();
            _scienceShadowData.Clear();
            _scienceCritterData.Clear();
            _scienceSculptData.Clear();
            _scienceCartographyData.Clear();
            _scienceOfferingsData.Clear();
            _scienceVolcanoData.Clear();
            var science = JsonConvert.DeserializeObject<ScienceRootObject>(await StringProcess.GetJsonString("Sciences.json"));
            foreach (var scienceToolItems in science.Tool.Science)
            {
                _scienceToolData.Add(scienceToolItems);
            }
            foreach (var scienceToolItems in _scienceToolData)
            {
                scienceToolItems.Picture = StringProcess.GetGameResourcePath(scienceToolItems.Picture);
            }
            foreach (var scienceLightItems in science.Light.Science)
            {
                _scienceLightData.Add(scienceLightItems);
            }
            foreach (var scienceLightItems in _scienceLightData)
            {
                scienceLightItems.Picture = StringProcess.GetGameResourcePath(scienceLightItems.Picture);
            }
            foreach (var scienceNauticalItems in science.Nautical.Science)
            {
                _scienceNauticalData.Add(scienceNauticalItems);
            }
            foreach (var scienceNauticalItems in _scienceNauticalData)
            {
                scienceNauticalItems.Picture = StringProcess.GetGameResourcePath(scienceNauticalItems.Picture);
            }
            foreach (var scienceSurvivalItems in science.Survival.Science)
            {
                _scienceSurvivalData.Add(scienceSurvivalItems);
            }
            foreach (var scienceSurvivalItems in _scienceSurvivalData)
            {
                scienceSurvivalItems.Picture = StringProcess.GetGameResourcePath(scienceSurvivalItems.Picture);
            }
            foreach (var scienceFoodItems in science.Foods.Science)
            {
                _scienceFoodData.Add(scienceFoodItems);
            }
            foreach (var scienceFoodItems in _scienceFoodData)
            {
                scienceFoodItems.Picture = StringProcess.GetGameResourcePath(scienceFoodItems.Picture);
            }
            foreach (var scienceTechnologyItems in science.Technology.Science)
            {
                _scienceTechnologyData.Add(scienceTechnologyItems);
            }
            foreach (var scienceTechnologyItems in _scienceTechnologyData)
            {
                scienceTechnologyItems.Picture = StringProcess.GetGameResourcePath(scienceTechnologyItems.Picture);
            }
            foreach (var scienceFightItems in science.Fight.Science)
            {
                _scienceFightData.Add(scienceFightItems);
            }
            foreach (var scienceFightItems in _scienceFightData)
            {
                scienceFightItems.Picture = StringProcess.GetGameResourcePath(scienceFightItems.Picture);
            }
            foreach (var scienceStructureItems in science.Structure.Science)
            {
                _scienceStructureData.Add(scienceStructureItems);
            }
            foreach (var scienceStructureItems in _scienceStructureData)
            {
                scienceStructureItems.Picture = StringProcess.GetGameResourcePath(scienceStructureItems.Picture);
            }
            foreach (var scienceRefineItems in science.Refine.Science)
            {
                _scienceRefineData.Add(scienceRefineItems);
            }
            foreach (var scienceRefineItems in _scienceRefineData)
            {
                scienceRefineItems.Picture = StringProcess.GetGameResourcePath(scienceRefineItems.Picture);
            }
            foreach (var scienceMagicItems in science.Magic.Science)
            {
                _scienceMagicData.Add(scienceMagicItems);
            }
            foreach (var scienceMagicItems in _scienceMagicData)
            {
                scienceMagicItems.Picture = StringProcess.GetGameResourcePath(scienceMagicItems.Picture);
            }
            foreach (var scienceDressItems in science.Dress.Science)
            {
                _scienceDressData.Add(scienceDressItems);
            }
            foreach (var scienceDressItems in _scienceDressData)
            {
                scienceDressItems.Picture = StringProcess.GetGameResourcePath(scienceDressItems.Picture);
            }
            foreach (var scienceAncientItems in science.Ancient.Science)
            {
                _scienceAncientData.Add(scienceAncientItems);
            }
            foreach (var scienceAncientItems in _scienceAncientData)
            {
                scienceAncientItems.Picture = StringProcess.GetGameResourcePath(scienceAncientItems.Picture);
            }
            foreach (var scienceBookItems in science.Book.Science)
            {
                _scienceBookData.Add(scienceBookItems);
            }
            foreach (var scienceBookItems in _scienceBookData)
            {
                scienceBookItems.Picture = StringProcess.GetGameResourcePath(scienceBookItems.Picture);
            }
            foreach (var scienceShadowItems in science.Shadow.Science)
            {
                _scienceShadowData.Add(scienceShadowItems);
            }
            foreach (var scienceShadowItems in _scienceShadowData)
            {
                scienceShadowItems.Picture = StringProcess.GetGameResourcePath(scienceShadowItems.Picture);
            }
            foreach (var scienceCritterItems in science.Critter.Science)
            {
                _scienceCritterData.Add(scienceCritterItems);
            }
            foreach (var scienceCritterItems in _scienceCritterData)
            {
                scienceCritterItems.Picture = StringProcess.GetGameResourcePath(scienceCritterItems.Picture);
            }
            foreach (var scienceSculptItems in science.Sculpt.Science)
            {
                _scienceSculptData.Add(scienceSculptItems);
            }
            foreach (var scienceSculptItems in _scienceSculptData)
            {
                scienceSculptItems.Picture = StringProcess.GetGameResourcePath(scienceSculptItems.Picture);
            }
            foreach (var scienceCartographyItems in science.Cartography.Science)
            {
                _scienceCartographyData.Add(scienceCartographyItems);
            }
            foreach (var scienceCartographyItems in _scienceCartographyData)
            {
                scienceCartographyItems.Picture = StringProcess.GetGameResourcePath(scienceCartographyItems.Picture);
            }
            foreach (var scienceOfferingsItems in science.Offerings.Science)
            {
                _scienceOfferingsData.Add(scienceOfferingsItems);
            }
            foreach (var scienceOfferingsItems in _scienceOfferingsData)
            {
                scienceOfferingsItems.Picture = StringProcess.GetGameResourcePath(scienceOfferingsItems.Picture);
            }
            foreach (var scienceVolcanoItems in science.Volcano.Science)
            {
                _scienceVolcanoData.Add(scienceVolcanoItems);
            }
            foreach (var scienceVolcanoItems in _scienceVolcanoData)
            {
                scienceVolcanoItems.Picture = StringProcess.GetGameResourcePath(scienceVolcanoItems.Picture);
            }
        }

        private void ScienceGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (((GridView)sender).ContainerFromItem(e.ClickedItem) is GridViewItem container)
            {
                var root = (FrameworkElement)container.ContentTemplateRoot;
                var image = (UIElement)root.FindName("Image");
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Image", image);
            }
            var item = (Science)e.ClickedItem;
            Frame.Navigate(typeof(ScienceDialog), item);
            Global.PageStack.Push(new PageStackItem { TypeName = typeof(ScienceDialog), Object = item });
        }
    }
}
