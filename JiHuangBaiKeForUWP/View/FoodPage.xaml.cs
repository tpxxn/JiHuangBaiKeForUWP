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
    public sealed partial class FoodPage : Page
    {
        private readonly ObservableCollection<FoodRecipe2> _foodRecipeData = new ObservableCollection<FoodRecipe2>();
        private readonly ObservableCollection<Food> _foodMeatData = new ObservableCollection<Food>();
        private readonly ObservableCollection<Food> _foodVegetableData = new ObservableCollection<Food>();
        private readonly ObservableCollection<Food> _foodFruitData = new ObservableCollection<Food>();
        private readonly ObservableCollection<Food> _foodEggData = new ObservableCollection<Food>();
        private readonly ObservableCollection<Food> _foodOtherData = new ObservableCollection<Food>();
        private readonly ObservableCollection<Food> _foodNoFcData = new ObservableCollection<Food>();
        public FoodPage()
        {
            this.InitializeComponent();
            Deserialize();
        }

        public async void Deserialize()
        {
            var food = JsonConvert.DeserializeObject<FoodRootObject>(await Global.GetJsonString("Foods.json"));
            foreach (var foodRecipeItems in food.FoodRecipe.FoodRecipes)
            {
                _foodRecipeData.Add(foodRecipeItems);
            }
            foreach (var foodRecipeItems in _foodRecipeData)
            {
                foodRecipeItems.Picture = Global.GetGameResourcePath(foodRecipeItems.Picture);
            }
            foreach (var foodMeatsItems in food.FoodMeats.Foods)
            {
                _foodMeatData.Add(foodMeatsItems);
            }
            foreach (var foodMeatsItems in _foodMeatData)
            {
                foodMeatsItems.Picture = Global.GetGameResourcePath(foodMeatsItems.Picture);
            }
            foreach (var foodVegetablesItems in food.FoodVegetables.Foods)
            {
                _foodVegetableData.Add(foodVegetablesItems);
            }
            foreach (var foodVegetablesItems in _foodVegetableData)
            {
                foodVegetablesItems.Picture = Global.GetGameResourcePath(foodVegetablesItems.Picture);
            }
            foreach (var foodFruitItems in food.FoodFruit.Foods)
            {
                _foodFruitData.Add(foodFruitItems);
            }
            foreach (var foodFruitItems in _foodFruitData)
            {
                foodFruitItems.Picture = Global.GetGameResourcePath(foodFruitItems.Picture);
            }
            foreach (var foodEggsItems in food.FoodEggs.Foods)
            {
                _foodEggData.Add(foodEggsItems);
            }
            foreach (var foodEggsItems in _foodEggData)
            {
                foodEggsItems.Picture = Global.GetGameResourcePath(foodEggsItems.Picture);
            }
            foreach (var foodOthersItems in food.FoodOthers.Foods)
            {
                _foodOtherData.Add(foodOthersItems);
            }
            foreach (var foodOthersItems in _foodOtherData)
            {
                foodOthersItems.Picture = Global.GetGameResourcePath(foodOthersItems.Picture);
            }
            foreach (var foodNoFcItems in food.FoodNoFc.Foods)
            {
                _foodNoFcData.Add(foodNoFcItems);
            }
            foreach (var foodNoFcItems in _foodNoFcData)
            {
                foodNoFcItems.Picture = Global.GetGameResourcePath(foodNoFcItems.Picture);
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
            Global.ShowDialog(contentDialog, FoodStackPanel);
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
            Global.ShowDialog(contentDialog, FoodStackPanel);
        }
    }
}
