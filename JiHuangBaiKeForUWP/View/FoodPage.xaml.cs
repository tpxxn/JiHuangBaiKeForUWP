﻿using System;
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
using AppStudio.Uwp;
using JiHuangBaiKeForUWP.UserControls.Expander;

namespace JiHuangBaiKeForUWP.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class FoodPage : Page
    {
        private readonly ObservableCollection<FoodRecipe2> _foodRecipeData = new ObservableCollection<FoodRecipe2>();
        private readonly ObservableCollection<Food> _foodMeatData = new ObservableCollection<Food>();
        private readonly ObservableCollection<Food> _foodVegetableData = new ObservableCollection<Food>();
        private readonly ObservableCollection<Food> _foodFruitData = new ObservableCollection<Food>();
        private readonly ObservableCollection<Food> _foodEggData = new ObservableCollection<Food>();
        private readonly ObservableCollection<Food> _foodOtherData = new ObservableCollection<Food>();
        private readonly ObservableCollection<Food> _foodNoFcData = new ObservableCollection<Food>();

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Global.FrameTitle.Text = "食物";
            if (e.NavigationMode == NavigationMode.Back)
            {
                RecipeEntranceTransition.FromVerticalOffset = 0;
                MeatEntranceTransition.FromVerticalOffset = 0;
                VegetableEntranceTransition.FromVerticalOffset = 0;
                FruitEntranceTransition.FromVerticalOffset = 0;
                EggEntranceTransition.FromVerticalOffset = 0;
                OtherEntranceTransition.FromVerticalOffset = 0;
                NoFcEntranceTransition.FromVerticalOffset = 0;
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
                    case "FoodRecipe":
                        RecipesExpander.IsExPanded = true;
                        OnNavigatedToFoodRecipeDialog(_e);
                        break;
                    case "FoodMeats":
                        MeatsExpander.IsExPanded = true;
                        OnNavigatedToFoodDialog(FoodMeatGridView, _foodMeatData, _e);
                        break;
                    case "FoodVegetables":
                        VegetablesExpander.IsExPanded = true;
                        OnNavigatedToFoodDialog(FoodVegetableGridView, _foodVegetableData, _e);
                        break;
                    case "FoodFruits":
                        FruitsExpander.IsExPanded = true;
                        OnNavigatedToFoodDialog(FoodFruitGridView, _foodFruitData, _e);
                        break;
                    case "FoodEggs":
                        EggsExpander.IsExPanded = true;
                        OnNavigatedToFoodDialog(FoodEggGridView, _foodEggData, _e);
                        break;
                    case "FoodOthers":
                        OtherExpander.IsExPanded = true;
                        OnNavigatedToFoodDialog(FoodOtherGridView, _foodOtherData, _e);
                        break;
                    case "FoodNoFc":
                        NoFcExpander.IsExPanded = true;
                        OnNavigatedToFoodDialog(FoodNoFcGridView, _foodNoFcData, _e);
                        break;
                }
            }
        }

        private void OnNavigatedToFoodRecipeDialog(string _e)
        {
            if (FoodRecipeGridView.Items == null) return;
            foreach (var gridViewItem in _foodRecipeData)
            {
                var food = gridViewItem;
                if (food == null || food.Picture != _e) continue;
                Frame.Navigate(typeof(FoodRecipeDialog), food);
                break;
            }
        }

        private void OnNavigatedToFoodDialog(GridView gridView, ObservableCollection<Food> foodCollection, string _e)
        {
            if (gridView.Items == null) return;
            foreach (var gridViewItem in foodCollection)
            {
                var food = gridViewItem;
                if (food == null || food.Picture != _e) continue;
                Frame.Navigate(typeof(FoodDialog), food);
                break;
            }
        }

        public FoodPage()
        {
            this.InitializeComponent();
        }

        public async Task Deserialize()
        {
            _foodRecipeData.Clear();
            _foodMeatData.Clear();
            _foodVegetableData.Clear();
            _foodFruitData.Clear();
            _foodEggData.Clear();
            _foodOtherData.Clear();
            _foodNoFcData.Clear();
            var food = JsonConvert.DeserializeObject<FoodRootObject>(await StringProcess.GetJsonString("Foods.json"));
            foreach (var foodRecipeItems in food.FoodRecipe.FoodRecipes)
            {
                _foodRecipeData.Add(foodRecipeItems);
            }
            foreach (var foodRecipeItems in _foodRecipeData)
            {
                foodRecipeItems.Picture = StringProcess.GetGameResourcePath(foodRecipeItems.Picture);
            }
            foreach (var foodMeatsItems in food.FoodMeats.Foods)
            {
                _foodMeatData.Add(foodMeatsItems);
            }
            foreach (var foodMeatsItems in _foodMeatData)
            {
                foodMeatsItems.Picture = StringProcess.GetGameResourcePath(foodMeatsItems.Picture);
            }
            foreach (var foodVegetablesItems in food.FoodVegetables.Foods)
            {
                _foodVegetableData.Add(foodVegetablesItems);
            }
            foreach (var foodVegetablesItems in _foodVegetableData)
            {
                foodVegetablesItems.Picture = StringProcess.GetGameResourcePath(foodVegetablesItems.Picture);
            }
            foreach (var foodFruitItems in food.FoodFruit.Foods)
            {
                _foodFruitData.Add(foodFruitItems);
            }
            foreach (var foodFruitItems in _foodFruitData)
            {
                foodFruitItems.Picture = StringProcess.GetGameResourcePath(foodFruitItems.Picture);
            }
            foreach (var foodEggsItems in food.FoodEggs.Foods)
            {
                _foodEggData.Add(foodEggsItems);
            }
            foreach (var foodEggsItems in _foodEggData)
            {
                foodEggsItems.Picture = StringProcess.GetGameResourcePath(foodEggsItems.Picture);
            }
            foreach (var foodOthersItems in food.FoodOthers.Foods)
            {
                _foodOtherData.Add(foodOthersItems);
            }
            foreach (var foodOthersItems in _foodOtherData)
            {
                foodOthersItems.Picture = StringProcess.GetGameResourcePath(foodOthersItems.Picture);
            }
            foreach (var foodNoFcItems in food.FoodNoFc.Foods)
            {
                _foodNoFcData.Add(foodNoFcItems);
            }
            foreach (var foodNoFcItems in _foodNoFcData)
            {
                foodNoFcItems.Picture = StringProcess.GetGameResourcePath(foodNoFcItems.Picture);
            }
        }

        private void FoodRecipeGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (((GridView)sender).ContainerFromItem(e.ClickedItem) is GridViewItem container)
            {
                var root = (FrameworkElement)container.ContentTemplateRoot;
                var image = (UIElement)root.FindName("Image");
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Image", image);
            }
            var item = (FoodRecipe2)e.ClickedItem;
            Frame.Navigate(typeof(FoodRecipeDialog), item);
            Global.PageStack.Push(new PageStackItem { SourcePageType = typeof(FoodRecipeDialog), Parameter = item });
        }

        private void FoodGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (((GridView)sender).ContainerFromItem(e.ClickedItem) is GridViewItem container)
            {
                var root = (FrameworkElement)container.ContentTemplateRoot;
                var image = (UIElement)root.FindName("Image");
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Image", image);
            }
            var item = (Food)e.ClickedItem;
            Frame.Navigate(typeof(FoodDialog), item);
            Global.PageStack.Push(new PageStackItem { SourcePageType = typeof(FoodDialog), Parameter = item });
        }

        private void Expander_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (e.OriginalSource.ToString() == "Windows.UI.Xaml.Controls.Grid")
            {
                var pageStackItem = Global.PageStack.Pop();
                var pageNavigationInfo = (ViewExtraData)pageStackItem.Parameter ?? new ViewExtraData();
                if(pageNavigationInfo.ExpandedList == null)
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
