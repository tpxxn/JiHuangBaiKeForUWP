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
        private readonly ObservableCollection<FoodRecipe> _foodRecipeData = new ObservableCollection<FoodRecipe>();
        private readonly ObservableCollection<FoodMeat> _foodMeatData = new ObservableCollection<FoodMeat>();
        private readonly ObservableCollection<FoodVegetable> _foodVegetableData = new ObservableCollection<FoodVegetable>();
        private readonly ObservableCollection<FoodFruit> _foodFruitData = new ObservableCollection<FoodFruit>();
        private readonly ObservableCollection<FoodEgg> _foodEggData = new ObservableCollection<FoodEgg>();
        private readonly ObservableCollection<FoodOther> _foodOtherData = new ObservableCollection<FoodOther>();
        private readonly ObservableCollection<FoodNoFc> _foodNoFcData = new ObservableCollection<FoodNoFc>();
        public FoodPage()
        {
            this.InitializeComponent();
            if (Global.GameVersionChanged)
            {
                Deserialize();
            }
        }

        public async void Deserialize()
        {
            Uri uri;
            const string fileName = "Foods.json";
            if (Global.GameVersion < 5) //内置配置文件
            {
                var folderExists = await Global.ApplicationFolder.TryGetItemAsync(Global.BuiltInGameVersion[Global.GameVersion]);
                if (folderExists == null)
                {
                    uri = new Uri("ms-appx:///Json/" + Global.BuiltInGameVersionJsonFolder[Global.GameVersion] + "/" +
                                  fileName);
                }
                else
                {
                    uri = new Uri(Global.ApplicationFolder.Path + "/" + Global.BuiltInGameVersion[Global.GameVersion] + "/" + fileName);
                }
            }
            else //用户自建配置文件
            {
                uri = new Uri(Global.ApplicationFolder.Path + "/" + Global.VersionData[Global.GameVersion] + "/" + fileName);
            }
            var storageFile = await StorageFile.GetFileFromApplicationUriAsync(uri);
            var str = await FileIO.ReadTextAsync(storageFile);
            var food = JsonConvert.DeserializeObject<FoodRootObject>(str);
            foreach (var foodRecipeItems in food.FoodRecipe)
            {
                _foodRecipeData.Add(foodRecipeItems);
            }
            foreach (var foodRecipeItems in _foodRecipeData)
            {
                foodRecipeItems.Picture = $"ms-appx:///Assets/GameResources/Foods/{foodRecipeItems.Picture}.png";
            }
            foreach (var foodMeatsItems in food.FoodMeats)
            {
                _foodMeatData.Add(foodMeatsItems);
            }
            foreach (var foodMeatsItems in _foodMeatData)
            {
                foodMeatsItems.Picture = $"ms-appx:///Assets/GameResources/Foods/{foodMeatsItems.Picture}.png";
            }
            foreach (var foodVegetablesItems in food.FoodVegetables)
            {
                _foodVegetableData.Add(foodVegetablesItems);
            }
            foreach (var foodVegetablesItems in _foodVegetableData)
            {
                foodVegetablesItems.Picture = $"ms-appx:///Assets/GameResources/Foods/{foodVegetablesItems.Picture}.png";
            }
            foreach (var foodFruitItems in food.FoodFruit)
            {
                _foodFruitData.Add(foodFruitItems);
            }
            foreach (var foodFruitItems in _foodFruitData)
            {
                foodFruitItems.Picture = $"ms-appx:///Assets/GameResources/Foods/{foodFruitItems.Picture}.png";
            }
            foreach (var foodEggsItems in food.FoodEggs)
            {
                _foodEggData.Add(foodEggsItems);
            }
            foreach (var foodEggsItems in _foodEggData)
            {
                foodEggsItems.Picture = $"ms-appx:///Assets/GameResources/Foods/{foodEggsItems.Picture}.png";
            }
            foreach (var foodOthersItems in food.FoodOthers)
            {
                _foodOtherData.Add(foodOthersItems);
            }
            foreach (var foodOthersItems in _foodOtherData)
            {
                foodOthersItems.Picture = $"ms-appx:///Assets/GameResources/Foods/{foodOthersItems.Picture}.png";
            }
            foreach (var foodNoFcItems in food.FoodNoFc)
            {
                _foodNoFcData.Add(foodNoFcItems);
            }
            foreach (var foodNoFcItems in _foodNoFcData)
            {
                foodNoFcItems.Picture = $"ms-appx:///Assets/GameResources/Foods/{foodNoFcItems.Picture}.png";
            }
        }

        private void FoodRecipeGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var c = e.ClickedItem as FoodRecipe;
            var contentDialog = new ContentDialog
            {
                Content = new FoodRecipeDialog(c),
                PrimaryButtonText = "确定",
                FullSizeDesired = false,
                Style = Global.Transparent
            };
            Global.ShowDialog(contentDialog, FoodStackPanel);
        }

        private void FoodMeatGridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void FoodVegetableGridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void FoodFruitGridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void FoodEggGridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void FoodOtherGridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void FoodNoFcGridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
