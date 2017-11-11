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
            if (Global.GetOsVersion() >= 16299)
            {
                var dimGrayAcrylicBrush = new AcrylicBrush
                {
                    BackgroundSource = AcrylicBackgroundSource.HostBackdrop,
                    FallbackColor = Colors.Transparent,
                    TintColor = Color.FromArgb(255, 105, 105, 105),
                    TintOpacity = 0.3
                };
                FoodStackPanel.Background = dimGrayAcrylicBrush;
            }
            var parameter = (string[])e.Parameter;
            await Deserialize();
            if (parameter == null) return;
            var _e = parameter[1];
            switch (parameter[0])
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

        private void OnNavigatedToFoodRecipeDialog(string _e)
        {
            if (FoodRecipeGridView.Items == null) return;
            foreach (var gridViewItem in _foodRecipeData)
            {
                var food = gridViewItem;
                if (food == null || food.Picture != _e) continue;
                var contentDialog = new ContentDialog
                {
                    Content = new FoodRecipeDialog(food),
                    PrimaryButtonText = "确定",
                    FullSizeDesired = false,
                    Style = Global.Transparent
                };
                Global.ShowDialog(contentDialog);
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
                var contentDialog = new ContentDialog
                {
                    Content = new FoodDialog(food),
                    PrimaryButtonText = "确定",
                    FullSizeDesired = false,
                    Style = Global.Transparent
                };
                Global.ShowDialog(contentDialog);
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
            var c = e.ClickedItem as FoodRecipe2;
            var contentDialog = new ContentDialog
            {
                Content = new FoodRecipeDialog(c),
                PrimaryButtonText = "确定",
                FullSizeDesired = false,
                Style = Global.Transparent
            };
            Global.ShowDialog(contentDialog);
        }

        private void FoodGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var c = e.ClickedItem as Food;
            var contentDialog = new ContentDialog
            {
                Content = new FoodDialog(c),
                PrimaryButtonText = "确定",
                FullSizeDesired = false,
                Style = Global.Transparent
            };
            Global.ShowDialog(contentDialog);
        }
    }
}
