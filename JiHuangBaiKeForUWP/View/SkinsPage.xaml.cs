using System;
using System.Collections;
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
using JiHuangBaiKeForUWP.UserControls.Expander;

namespace JiHuangBaiKeForUWP.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SkinsPage : Page
    {
        private readonly ObservableCollection<Skin> _SkinsBodyData = new ObservableCollection<Skin>();
        private readonly ObservableCollection<Skin> _SkinsHandsData = new ObservableCollection<Skin>();
        private readonly ObservableCollection<Skin> _SkinsLegsData = new ObservableCollection<Skin>();
        private readonly ObservableCollection<Skin> _SkinsFeetData = new ObservableCollection<Skin>();
        private readonly ObservableCollection<Skin> _SkinsCharactersData = new ObservableCollection<Skin>();
        private readonly ObservableCollection<Skin> _SkinsItemsData = new ObservableCollection<Skin>();
        private readonly ObservableCollection<Skin> _SkinsStructuresData = new ObservableCollection<Skin>();
        private readonly ObservableCollection<Skin> _SkinsCrittersData = new ObservableCollection<Skin>();
        private readonly ObservableCollection<Skin> _SkinsSpecialData = new ObservableCollection<Skin>();
        private readonly ObservableCollection<Skin> _SkinsHallowedNightsSkinsData = new ObservableCollection<Skin>();
        private readonly ObservableCollection<Skin> _SkinsWintersFeastSkinsData = new ObservableCollection<Skin>();
        private readonly ObservableCollection<Skin> _SkinsYearOfTheGobblerSkinsData = new ObservableCollection<Skin>();
        private readonly ObservableCollection<Skin> _SkinsTheForgeData = new ObservableCollection<Skin>();
        private readonly ObservableCollection<Skin> _SkinsEmotesData = new ObservableCollection<Skin>();
        private readonly ObservableCollection<Skin> _SkinsOutfitSetsData = new ObservableCollection<Skin>();

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Global.FrameTitle.Text = "皮肤";
            if (e.NavigationMode == NavigationMode.Back)
            {
                BodyEntranceTransition.FromVerticalOffset = 0;
                HandsEntranceTransition.FromVerticalOffset = 0;
                LegsEntranceTransition.FromVerticalOffset = 0;
                FeetEntranceTransition.FromVerticalOffset = 0;
                CharactersEntranceTransition.FromVerticalOffset = 0;
                ItemsEntranceTransition.FromVerticalOffset = 0;
                StructuresEntranceTransition.FromVerticalOffset = 0;
                CrittersEntranceTransition.FromVerticalOffset = 0;
                SpecialEntranceTransition.FromVerticalOffset = 0;
                HallowedNightsSkinsEntranceTransition.FromVerticalOffset = 0;
                WintersFeastSkinsEntranceTransition.FromVerticalOffset = 0;
                YearOfTheGobblerSkinsEntranceTransition.FromVerticalOffset = 0;
                TheForgeEntranceTransition.FromVerticalOffset = 0;
                EmotesEntranceTransition.FromVerticalOffset = 0;
                OutfitSetsEntranceTransition.FromVerticalOffset = 0;
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
                        ((Expander)RootStackPanel.Children[i]).IsExPanded = extraData.ExpandedList[i] == "True";
                    }
                }
                //ScrollViewer滚动到指定位置
                RootScrollViewer.UpdateLayout();
                RootScrollViewer.ChangeView(null, extraData.ScrollViewerVerticalOffset, null, true);
                //导航到指定页面
                var _e = extraData.Picture;
                switch (extraData.Classify)
                {
                    case "SkinsBody":
                        BodyExpander.IsExPanded = true;
                        OnNavigatedToSkinsDialog(SkinsBodyGridView, _SkinsBodyData, _e);
                        break;
                    case "SkinsHands":
                        HandsExpander.IsExPanded = true;
                        OnNavigatedToSkinsDialog(SkinsHandsGridView, _SkinsHandsData, _e);
                        break;
                    case "SkinsLegs":
                        LegsExpander.IsExPanded = true;
                        OnNavigatedToSkinsDialog(SkinsLegsGridView, _SkinsLegsData, _e);
                        break;
                    case "SkinsFeet":
                        FeetExpander.IsExPanded = true;
                        OnNavigatedToSkinsDialog(SkinsFeetGridView, _SkinsFeetData, _e);
                        break;
                    case "SkinsCharacters":
                        CharactersExpander.IsExPanded = true;
                        OnNavigatedToSkinsDialog(SkinsCharactersGridView, _SkinsCharactersData, _e);
                        break;
                    case "SkinsItems":
                        ItemsExpander.IsExPanded = true;
                        OnNavigatedToSkinsDialog(SkinsItemsGridView, _SkinsItemsData, _e);
                        break;
                    case "SkinsStructures":
                        StructuresExpander.IsExPanded = true;
                        OnNavigatedToSkinsDialog(SkinsStructuresGridView, _SkinsStructuresData, _e);
                        break;
                    case "SkinsCritters":
                        CrittersExpander.IsExPanded = true;
                        OnNavigatedToSkinsDialog(SkinsCrittersGridView, _SkinsCrittersData, _e);
                        break;
                    case "SkinsSpecial":
                        SpecialExpander.IsExPanded = true;
                        OnNavigatedToSkinsDialog(SkinsSpecialGridView, _SkinsSpecialData, _e);
                        break;
                    case "SkinsHallowedNightsSkins":
                        HallowedNightsSkinsExpander.IsExPanded = true;
                        OnNavigatedToSkinsDialog(SkinsHallowedNightsSkinsGridView, _SkinsHallowedNightsSkinsData, _e);
                        break;
                    case "SkinsWintersFeastSkins":
                        WintersFeastSkinsExpander.IsExPanded = true;
                        OnNavigatedToSkinsDialog(SkinsWintersFeastSkinsGridView, _SkinsWintersFeastSkinsData, _e);
                        break;
                    case "SkinsYearOfTheGobblerSkins":
                        YearOfTheGobblerSkinsExpander.IsExPanded = true;
                        OnNavigatedToSkinsDialog(SkinsYearOfTheGobblerSkinsGridView, _SkinsYearOfTheGobblerSkinsData, _e);
                        break;
                    case "SkinsTheForge":
                        TheForgeExpander.IsExPanded = true;
                        OnNavigatedToSkinsDialog(SkinsTheForgeGridView, _SkinsTheForgeData, _e);
                        break;
                    case "SkinsEmotes":
                        EmotesExpander.IsExPanded = true;
                        OnNavigatedToSkinsDialog(SkinsEmotesGridView, _SkinsEmotesData, _e);
                        break;
                    case "SkinsOutfitSets":
                        OutfitSetsExpander.IsExPanded = true;
                        OnNavigatedToSkinsDialog(SkinsOutfitSetsGridView, _SkinsOutfitSetsData, _e);
                        break;
                }
            }
        }

        private void OnNavigatedToSkinsDialog(GridView gridView, ObservableCollection<Skin> SkinsCollection, string _e)
        {
            if (gridView.Items == null) return;
            foreach (var gridViewItem in SkinsCollection)
            {
                var Skins = gridViewItem;
                if (Skins == null || Skins.Picture != _e) continue;
                Frame.Navigate(typeof(SkinsDialog), Skins);
                break;
            }
        }

        public SkinsPage()
        {
            this.InitializeComponent();
        }

        public async Task Deserialize()
        {
            _SkinsBodyData.Clear();
            _SkinsHandsData.Clear();
            _SkinsLegsData.Clear();
            _SkinsFeetData.Clear();
            _SkinsCharactersData.Clear();
            _SkinsItemsData.Clear();
            _SkinsStructuresData.Clear();
            _SkinsCrittersData.Clear();
            _SkinsSpecialData.Clear();
            _SkinsHallowedNightsSkinsData.Clear();
            _SkinsWintersFeastSkinsData.Clear();
            _SkinsYearOfTheGobblerSkinsData.Clear();
            _SkinsTheForgeData.Clear();
            _SkinsEmotesData.Clear();
            _SkinsOutfitSetsData.Clear();
            var Skins = JsonConvert.DeserializeObject<SkinsRootObject>(await StringProcess.GetJsonStringSkins());
            foreach (var SkinsBodyItems in Skins.Body.Skin)
            {
                SkinsBodyItems.Picture = StringProcess.GetGameResourcePath(SkinsBodyItems.Picture);
                SkinsBodyItems.Color = GetSkinColor(SkinsBodyItems.Rarity);
                _SkinsBodyData.Add(SkinsBodyItems);
            }
            foreach (var SkinsHandsData in Skins.Hands.Skin)
            {
                SkinsHandsData.Picture = StringProcess.GetGameResourcePath(SkinsHandsData.Picture);
                SkinsHandsData.Color = GetSkinColor(SkinsHandsData.Rarity);
                _SkinsHandsData.Add(SkinsHandsData);
            }
            foreach (var SkinsLegsItems in Skins.Legs.Skin)
            {
                SkinsLegsItems.Picture = StringProcess.GetGameResourcePath(SkinsLegsItems.Picture);
                SkinsLegsItems.Color = GetSkinColor(SkinsLegsItems.Rarity);
                _SkinsLegsData.Add(SkinsLegsItems);
            }
            foreach (var SkinsFeetItems in Skins.Feet.Skin)
            {
                SkinsFeetItems.Picture = StringProcess.GetGameResourcePath(SkinsFeetItems.Picture);
                SkinsFeetItems.Color = GetSkinColor(SkinsFeetItems.Rarity);
                _SkinsFeetData.Add(SkinsFeetItems);
            }
            foreach (var SkinsCharactersItems in Skins.Characters.Skin)
            {
                SkinsCharactersItems.Picture = StringProcess.GetGameResourcePath(SkinsCharactersItems.Picture);
                SkinsCharactersItems.Color = GetSkinColor(SkinsCharactersItems.Rarity);
                _SkinsCharactersData.Add(SkinsCharactersItems);
            }
            foreach (var SkinsItemsItems in Skins.Items.Skin)
            {
                SkinsItemsItems.Picture = StringProcess.GetGameResourcePath(SkinsItemsItems.Picture);
                SkinsItemsItems.Color = GetSkinColor(SkinsItemsItems.Rarity);
                _SkinsItemsData.Add(SkinsItemsItems);
            }
            foreach (var SkinsStructuresItems in Skins.Structures.Skin)
            {
                SkinsStructuresItems.Picture = StringProcess.GetGameResourcePath(SkinsStructuresItems.Picture);
                SkinsStructuresItems.Color = GetSkinColor(SkinsStructuresItems.Rarity);
                _SkinsStructuresData.Add(SkinsStructuresItems);
            }
            foreach (var SkinsCrittersItems in Skins.Critters.Skin)
            {
                SkinsCrittersItems.Picture = StringProcess.GetGameResourcePath(SkinsCrittersItems.Picture);
                SkinsCrittersItems.Color = GetSkinColor(SkinsCrittersItems.Rarity);
                _SkinsCrittersData.Add(SkinsCrittersItems);
            }
            foreach (var SkinsSpecialItems in Skins.Special.Skin)
            {
                SkinsSpecialItems.Picture = StringProcess.GetGameResourcePath(SkinsSpecialItems.Picture);
                SkinsSpecialItems.Color = GetSkinColor(SkinsSpecialItems.Rarity);
                _SkinsSpecialData.Add(SkinsSpecialItems);
            }
            foreach (var SkinsHallowedNightsSkinsItems in Skins.HallowedNightsSkins.Skin)
            {
                SkinsHallowedNightsSkinsItems.Picture = StringProcess.GetGameResourcePath(SkinsHallowedNightsSkinsItems.Picture);
                SkinsHallowedNightsSkinsItems.Color = GetSkinColor(SkinsHallowedNightsSkinsItems.Rarity);
                _SkinsHallowedNightsSkinsData.Add(SkinsHallowedNightsSkinsItems);
            }
            foreach (var SkinsWintersFeastSkinsItems in Skins.WintersFeastSkins.Skin)
            {
                SkinsWintersFeastSkinsItems.Picture = StringProcess.GetGameResourcePath(SkinsWintersFeastSkinsItems.Picture);
                SkinsWintersFeastSkinsItems.Color = GetSkinColor(SkinsWintersFeastSkinsItems.Rarity);
                _SkinsWintersFeastSkinsData.Add(SkinsWintersFeastSkinsItems);
            }
            foreach (var SkinsYearOfTheGobblerSkinsItems in Skins.YearOfTheGobblerSkins.Skin)
            {
                SkinsYearOfTheGobblerSkinsItems.Picture = StringProcess.GetGameResourcePath(SkinsYearOfTheGobblerSkinsItems.Picture);
                SkinsYearOfTheGobblerSkinsItems.Color = GetSkinColor(SkinsYearOfTheGobblerSkinsItems.Rarity);
                _SkinsYearOfTheGobblerSkinsData.Add(SkinsYearOfTheGobblerSkinsItems);
            }
            foreach (var SkinsTheForgeItems in Skins.TheForge.Skin)
            {
                SkinsTheForgeItems.Picture = StringProcess.GetGameResourcePath(SkinsTheForgeItems.Picture);
                SkinsTheForgeItems.Color = GetSkinColor(SkinsTheForgeItems.Rarity);
                _SkinsTheForgeData.Add(SkinsTheForgeItems);
            }
            foreach (var SkinsEmotesItems in Skins.Emotes.Skin)
            {
                SkinsEmotesItems.Picture = StringProcess.GetGameResourcePath(SkinsEmotesItems.Picture);
                SkinsEmotesItems.Color = GetSkinColor(SkinsEmotesItems.Rarity);
                _SkinsEmotesData.Add(SkinsEmotesItems);
            }
            foreach (var SkinsOutfitSetsItems in Skins.OutfitSets.Skin)
            {
                SkinsOutfitSetsItems.Picture = StringProcess.GetGameResourcePath(SkinsOutfitSetsItems.Picture);
                SkinsOutfitSetsItems.Color = GetSkinColor(SkinsOutfitSetsItems.Rarity);
                _SkinsOutfitSetsData.Add(SkinsOutfitSetsItems);
            }
        }

        /// <summary>
        /// 获取皮肤Color属性
        /// </summary>
        /// <param name="rarity">稀有度</param>
        /// <returns>Color</returns>
        private static SolidColorBrush GetSkinColor(string rarity)
        {
            switch (rarity)
            {
                case "Common":
                    return SkinsColors.Common;
                case "Classy":
                    return SkinsColors.Classy;
                case "Spiffy":
                    return SkinsColors.Spiffy;
                case "Distinguished":
                    return SkinsColors.Distinguished;
                case "Elegant":
                    return SkinsColors.Elegant;
                case "Loyal":
                    return SkinsColors.Loyal;
                case "Timeless":
                    return SkinsColors.Timeless;
                case "Event":
                    return SkinsColors.Event;
                case "Proof of Purchase":
                    return SkinsColors.ProofOfPurchase;
                case "Reward":
                    return SkinsColors.Reward;
                default:
                    return new SolidColorBrush(Colors.Black);
            }
        }

        private void SkinsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (((GridView)sender).ContainerFromItem(e.ClickedItem) is GridViewItem container)
            {
                var root = (FrameworkElement)container.ContentTemplateRoot;
                var image = (UIElement)root.FindName("Image");
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Image", image);
            }
            var item = (Skin)e.ClickedItem;
            Frame.Navigate(typeof(SkinsDialog), item);
            Global.PageStack.Push(new PageStackItem { SourcePageType = typeof(SkinsDialog), Parameter = item });
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
