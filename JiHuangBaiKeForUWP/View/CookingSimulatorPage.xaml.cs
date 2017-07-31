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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using JiHuangBaiKeForUWP.Model;
using Newtonsoft.Json;

namespace JiHuangBaiKeForUWP.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class CookingSimulatorPage : Page
    {
        private readonly ObservableCollection<FoodMeat> _foodMeatData = new ObservableCollection<FoodMeat>();
        private readonly ObservableCollection<FoodVegetable> _foodVegetableData = new ObservableCollection<FoodVegetable>();
        private readonly ObservableCollection<FoodFruit> _foodFruitData = new ObservableCollection<FoodFruit>();
        private readonly ObservableCollection<FoodEgg> _foodEggData = new ObservableCollection<FoodEgg>();
        private readonly ObservableCollection<FoodOther> _foodOtherData = new ObservableCollection<FoodOther>();

        public CookingSimulatorPage()
        {
            this.InitializeComponent();
            Deserialize();
        }

        public async void Deserialize()
        {
            Uri uri;
            const string fileName = "Foods.json";
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
            var food = JsonConvert.DeserializeObject<FoodRootObject>(str);
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
        }

        private void FoodMeatGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is FoodMeat food) CS_Add(food.Picture);
        }

        private void FoodVegetableGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is FoodVegetable food) CS_Add(food.Picture);
        }

        private void FoodFruitGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is FoodFruit food) CS_Add(food.Picture);
        }

        private void FoodEggGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is FoodEgg food) CS_Add(food.Picture);
        }

        private void FoodOtherGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is FoodOther food) CS_Add(food.Picture);
        }
        
        #region 变量初始化
        //重置
        private void ResetButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Food1Button_Tapped(null, null);
            Food2Button_Tapped(null, null);
            Food3Button_Tapped(null, null);
            Food4Button_Tapped(null, null);
            FoodResultImage.Source = null;
            FoodResultTextBlock.Text = "";
            CrockPotList.Clear();
            CrockPotListIndex = -1;
            CrockPotMaxPriority = -128;
        }
        //四个位置
        public string CsRecipe1 = "";
        public string CsRecipe2 = "";
        public string CsRecipe3 = "";
        public string CsRecipe4 = "";
        //38种食材
        public double CsFtEggs = 0;
        public double CsFtVegetables = 0;
        public double CsFtFruit = 0;
        public double CsFtBanana = 0;
        public double CsFtBerries = 0;
        public double CsFtButter = 0;
        public double CsFtButterflyWings = 0;
        public double CsFtCactusFlesh = 0;
        public double CsFtCactusFlower = 0;
        public double CsFtCorn = 0;
        public double CsFtDairyProduct = 0;
        public double CsFtDragonFruit = 0;
        public double CsFtDrumstick = 0;
        public double CsFtEel = 0;
        public double CsFtEggplant = 0;
        public double CsFtFishes = 0;
        public double CsFtFrogLegs = 0;
        public double CsFtHoney = 0;
        public double CsFtIce = 0;
        public double CsFtJellyfish = 0;
        public double CsFtLichen = 0;
        public double CsFtLimpets = 0;
        public double CsFtMandrake = 0;
        public double CsFtMeats = 0;
        public double CsFtMoleworm = 0;
        public double CsFtMonsterFoods = 0;
        public double CsFtMussel = 0;
        public double CsFtPumpkin = 0;
        public double CsFtRoastedBirchnut = 0;
        public double CsFtRoastedCoffeeBeans = 0;
        public double CsFtSeaweed = 0;
        public double CsFtSharkFin = 0;
        public double CsFtSweetener = 0;
        public double CsFtSweetPotato = 0;
        public double CsFtTwigs = 0;
        public double CsFtWatermelon = 0;
        public double CsFtWobster = 0;
        public double CsFtRoyalJelly = 0;

        public byte FoodIndex = 0;
        public string CsFoodName = "";

        public List<string> CrockPotList = new List<string>(); //食物列表
        public sbyte CrockPotListIndex = -1;//食物列表下标
        public sbyte CrockPotMaxPriority = -128; //优先度最大值
        #endregion

        public static string GetFileName(string shortName)
        {
            shortName = shortName.Substring(shortName.LastIndexOf('/') + 1, shortName.Length - shortName.LastIndexOf('/') - 5);
            return shortName;
        }

        //添加食材
        private void CS_Add(string foodName)
        {
            if (CsRecipe1 == "")
            {
                CsRecipe1 = GetFileName(foodName);
                Food1Image.Source = new BitmapImage(new Uri(foodName));
            }
            else if (CsRecipe2 == "")
            {
                CsRecipe2 = GetFileName(foodName);
                Food2Image.Source = new BitmapImage(new Uri(foodName));
            }
            else if (CsRecipe3 == "")
            {
                CsRecipe3 = GetFileName(foodName);
                Food3Image.Source = new BitmapImage(new Uri(foodName));
            }
            else if (CsRecipe4 == "")
            {
                CsRecipe4 = GetFileName(foodName);
                Food4Image.Source = new BitmapImage(new Uri(foodName));
            }
            if (CsRecipe1 != "" && CsRecipe2 != "" && CsRecipe3 != "" && CsRecipe4 != "")
            {
                CS_CrockPotCalculation();
            }
        }
        //删除食材
        private void Food1Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CsRecipe1 = "";
            Food1Image.Source = null;
        }

        private void Food2Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CsRecipe2 = "";
            Food2Image.Source = null;
        }

        private void Food3Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CsRecipe3 = "";
            Food3Image.Source = null;
        }

        private void Food4Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CsRecipe4 = "";
            Food4Image.Source = null;
        }
        //食材属性统计
        private void CS_RecipeStatistics(string name)
        {
            switch (name)
            {
                #region 肉类
                case "F_meat":
                    CsFtMeats += 1;
                    break;
                case "F_cooked_meat":
                    CsFtMeats += 1;
                    break;
                case "F_jerky":
                    CsFtMeats += 1;
                    break;
                case "F_monster_meat":
                    CsFtMeats += 1;
                    CsFtMonsterFoods += 1;
                    break;
                case "F_cooked_monster_meat":
                    CsFtMeats += 1;
                    CsFtMonsterFoods += 1;
                    break;
                case "F_monster_jerky":
                    CsFtMeats += 1;
                    CsFtMonsterFoods += 1;
                    break;
                case "F_morsel":
                    CsFtMeats += 0.5;
                    break;
                case "F_cooked_morsel":
                    CsFtMeats += 0.5;
                    break;
                case "F_small_jerky":
                    CsFtMeats += 0.5;
                    break;
                case "F_drumstick":
                    CsFtMeats += 0.5;
                    CsFtDrumstick += 1;
                    break;
                case "F_fried_drumstick":
                    CsFtMeats += 0.5;
                    CsFtDrumstick += 1;
                    break;
                case "F_frog_legs":
                    CsFtMeats += 0.5;
                    CsFtFrogLegs += 1;
                    break;
                case "F_cooked_frog_legs":
                    CsFtMeats += 0.5;
                    CsFtFrogLegs += 1;
                    break;
                case "F_fish":
                    CsFtFishes += 1;
                    CsFtMeats += 0.5;
                    break;
                case "F_cooked_fish":
                    CsFtFishes += 1;
                    CsFtMeats += 0.5;
                    break;
                case "F_eel":
                    CsFtFishes += 1;
                    CsFtEel += 1;
                    CsFtMeats += 0.5;
                    break;
                case "F_cooked_eel":
                    CsFtFishes += 1;
                    CsFtEel += 1;
                    CsFtMeats += 0.5;
                    break;
                case "F_moleworm":
                    CsFtMoleworm += 1;
                    break;
                case "F_limpets":
                    CsFtFishes += 0.5;
                    CsFtLimpets += 1;
                    break;
                case "F_cooked_limpets":
                    CsFtFishes += 0.5;
                    break;
                case "F_tropical_fish":
                    CsFtMeats += 0.5;
                    CsFtFishes += 1;
                    break;
                case "F_fish_morsel":
                    CsFtFishes += 0.5;
                    break;
                case "F_cooked_fish_morsel":
                    CsFtFishes += 0.5;
                    break;
                case "F_jellyfish":
                    CsFtFishes += 1;
                    CsFtMonsterFoods += 1;
                    CsFtJellyfish += 1;
                    break;
                case "F_dead_jellyfish":
                    CsFtFishes += 1;
                    CsFtMonsterFoods += 1;
                    break;
                case "F_cooked_jellyfish":
                    CsFtFishes += 1;
                    CsFtMonsterFoods += 1;
                    break;
                case "F_dried_jellyfish":
                    CsFtFishes += 1;
                    CsFtMonsterFoods += 1;
                    break;
                case "F_mussel":
                    CsFtFishes += 0.5;
                    CsFtMussel += 1;
                    break;
                case "F_cooked_mussel":
                    CsFtFishes += 0.5;
                    CsFtMussel += 1;
                    break;
                case "F_dead_dogfish":
                    CsFtFishes += 1;
                    CsFtMeats += 0.5;
                    break;
                case "F_wobster":
                    CsFtFishes += 2;
                    CsFtWobster += 1;
                    break;
                case "F_raw_fish":
                    CsFtFishes += 1;
                    CsFtMeats += 0.5;
                    break;
                case "F_fish_steak":
                    CsFtFishes += 1;
                    CsFtMeats += 0.5;
                    break;
                case "F_shark_fin":
                    CsFtFishes += 1;
                    CsFtMeats += 0.5;
                    CsFtSharkFin += 1;
                    break;
                #endregion
                #region 蔬菜
                case "F_blue_cap":
                    CsFtVegetables += 0.5;
                    break;
                case "F_cooked_blue_cap":
                    CsFtVegetables += 0.5;
                    break;
                case "F_green_cap":
                    CsFtVegetables += 0.5;
                    break;
                case "F_cooked_green_cap":
                    CsFtVegetables += 0.5;
                    break;
                case "F_red_cap":
                    CsFtVegetables += 0.5;
                    break;
                case "F_cooked_red_cap":
                    CsFtVegetables += 0.5;
                    break;
                case "F_eggplant":
                    CsFtVegetables += 1;
                    CsFtEggplant += 1;
                    break;
                case "F_braised_eggplant":
                    CsFtVegetables += 1;
                    CsFtEggplant += 1;
                    break;
                case "F_carrot":
                    CsFtVegetables += 1;
                    break;
                case "F_roasted_carrot":
                    CsFtVegetables += 1;
                    break;
                case "F_corn":
                    CsFtVegetables += 1;
                    CsFtCorn += 1;
                    break;
                case "F_popcorn":
                    CsFtVegetables += 1;
                    CsFtCorn += 1;
                    break;
                case "F_pumpkin":
                    CsFtVegetables += 1;
                    CsFtPumpkin += 1;
                    break;
                case "F_hot_pumpkin":
                    CsFtVegetables += 1;
                    CsFtPumpkin += 1;
                    break;
                case "F_cactus_flesh":
                    CsFtVegetables += 1;
                    CsFtCactusFlesh += 1;
                    break;
                case "F_cooked_cactus_flesh":
                    CsFtVegetables += 1;
                    break;
                case "F_cactus_flower":
                    CsFtVegetables += 0.5;
                    CsFtCactusFlower += 1;
                    break;
                case "F_sweet_potato":
                    CsFtVegetables += 1;
                    CsFtSweetPotato += 1;
                    break;
                case "F_cooked_sweet_potato":
                    CsFtVegetables += 1;
                    break;
                case "F_seaweed":
                    CsFtVegetables += 0.5;
                    CsFtSeaweed += 1;
                    break;
                case "F_roasted_seaweed":
                    CsFtVegetables += 0.5;
                    break;
                case "F_dried_seaweed":
                    CsFtVegetables += 0.5;
                    break;
                #endregion
                #region 水果
                case "F_juicy_berries":
                    CsFtFruit += 0.5;
                    break;
                case "F_roasted_juicy_berries":
                    CsFtFruit += 0.5;
                    break;
                case "F_berries":
                    CsFtFruit += 0.5;
                    CsFtBerries += 1;
                    break;
                case "F_roasted_berrie":
                    CsFtFruit += 0.5;
                    CsFtBerries += 1;
                    break;
                case "F_banana":
                    CsFtFruit += 1;
                    CsFtBanana += 1;
                    break;
                case "F_cooked_banana":
                    CsFtFruit += 1;
                    break;
                case "F_dragon_fruit":
                    CsFtFruit += 1;
                    CsFtDragonFruit += 1;
                    break;
                case "F_prepared_dragon_fruit":
                    CsFtFruit += 1;
                    CsFtDragonFruit += 1;
                    break;
                case "F_durian":
                    CsFtFruit += 1;
                    CsFtMonsterFoods += 1;
                    break;
                case "F_extra_smelly_durian":
                    CsFtFruit += 1;
                    CsFtMonsterFoods += 1;
                    break;
                case "F_pomegranate":
                    CsFtFruit += 1;
                    break;
                case "F_sliced_pomegranate":
                    CsFtFruit += 1;
                    break;
                case "F_watermelon":
                    CsFtFruit += 1;
                    CsFtWatermelon += 1;
                    break;
                case "F_grilled_watermelon":
                    CsFtFruit += 1;
                    break;
                case "F_halved_coconut":
                    CsFtFruit += 1;
                    break;
                case "F_roasted_coconut":
                    CsFtFruit += 1;
                    break;
                case "F_coffee_beans":
                    CsFtFruit += 0.5;
                    break;
                case "F_roasted_coffee_beans":
                    CsFtFruit += 1;
                    CsFtRoastedCoffeeBeans += 1;
                    break;
                #endregion
                #region 蛋类
                case "F_egg":
                    CsFtEggs += 1;
                    break;
                case "F_cooked_egg":
                    CsFtEggs += 1;
                    break;
                case "F_tallbird_egg":
                    CsFtEggs += 4;
                    break;
                case "F_fried_tallbird_egg":
                    CsFtEggs += 4;
                    break;
                case "F_doydoy_egg":
                    CsFtEggs += 1;
                    break;
                case "F_fried_doydoy_egg":
                    CsFtEggs += 1;
                    break;
                #endregion
                #region 其他
                case "F_butterfly_wing":
                    CsFtButterflyWings += 1;
                    break;
                case "F_butterfly_wing_sw":
                    CsFtButterflyWings += 1;
                    break;
                case "F_butter":
                    CsFtDairyProduct += 1;
                    CsFtButter += 1;
                    break;
                case "F_honey":
                    CsFtSweetener += 1;
                    CsFtHoney += 1;
                    break;
                case "F_honeycomb":
                    CsFtSweetener += 1;
                    break;
                case "F_lichen":
                    CsFtVegetables += 1;
                    CsFtLichen += 1;
                    break;
                case "F_mandrake":
                    CsFtVegetables += 1;
                    CsFtMandrake += 1;
                    break;
                case "F_electric_milk":
                    CsFtDairyProduct += 1;
                    break;
                case "F_ice":
                    CsFtIce += 1;
                    break;
                case "F_roasted_birchnut":
                    CsFtRoastedBirchnut += 1;
                    break;
                case "F_royal_jelly":
                    CsFtRoyalJelly += 1;
                    break;
                case "F_twigs":
                    CsFtTwigs += 1;
                    break;
                #endregion
            }
        }
        //烹饪计算
        private void CS_CrockPotCalculation()
        {
            FoodIndex = 0;
            #region 食物列表初始化
            CrockPotList.Clear();
            CrockPotListIndex = -1;
            CrockPotMaxPriority = -128;
            #endregion
            #region 食材属性初始化
            CsFtBanana = 0;
            CsFtBerries = 0;
            CsFtButter = 0;
            CsFtButterflyWings = 0;
            CsFtCactusFlesh = 0;
            CsFtCactusFlower = 0;
            CsFtCorn = 0;
            CsFtDairyProduct = 0;
            CsFtDragonFruit = 0;
            CsFtDrumstick = 0;
            CsFtEel = 0;
            CsFtEggplant = 0;
            CsFtEggs = 0;
            CsFtFishes = 0;
            CsFtFrogLegs = 0;
            CsFtFruit = 0;
            CsFtHoney = 0;
            CsFtIce = 0;
            CsFtJellyfish = 0;
            CsFtLichen = 0;
            CsFtLimpets = 0;
            CsFtMandrake = 0;
            CsFtMeats = 0;
            CsFtMoleworm = 0;
            CsFtMonsterFoods = 0;
            CsFtMussel = 0;
            CsFtPumpkin = 0;
            CsFtRoastedBirchnut = 0;
            CsFtRoastedCoffeeBeans = 0;
            CsFtSeaweed = 0;
            CsFtSharkFin = 0;
            CsFtSweetener = 0;
            CsFtSweetPotato = 0;
            CsFtTwigs = 0;
            CsFtVegetables = 0;
            CsFtWatermelon = 0;
            CsFtWobster = 0;
            CsFtRoyalJelly = 0;
            #endregion
            #region 属性统计
            CS_RecipeStatistics(CsRecipe1);
            CS_RecipeStatistics(CsRecipe2);
            CS_RecipeStatistics(CsRecipe3);
            CS_RecipeStatistics(CsRecipe4);
            #endregion
            #region 烹饪
            //------------------------SW------------------------
                //便携式烹饪锅的四种食物
                if (ToggleSwitch.IsOn)
                {
                    if (CsFtFruit >= 2 && CsFtButter >= 1 && CsFtHoney >= 1)
                        CS_CrockPotListAddFood("F_fresh_fruit_crepes", 30);
                    if (CsFtMonsterFoods >= 2 && CsFtEggs >= 1 && CsFtVegetables >= 0.5)
                        CS_CrockPotListAddFood("F_monster_tartare", 30);
                    if (CsFtMussel >= 2 && CsFtVegetables >= 2)
                        CS_CrockPotListAddFood("F_mussel_bouillabaise", 30);
                    if (CsFtSweetPotato >= 2 && CsFtEggs >= 2)
                        CS_CrockPotListAddFood("F_sweet_potato_souffle", 30);
                }
                if (CsFtWobster >= 1 && CsFtIce >= 1)
                    CS_CrockPotListAddFood("F_lobster_bisque", 30);
                if (CsFtLimpets >= 3 && CsFtIce >= 1)
                    CS_CrockPotListAddFood("F_bisque", 30);
                if (CsFtRoastedCoffeeBeans >= 3 && (CsFtRoastedCoffeeBeans == 4 || CsFtSweetener == 1 || CsFtDairyProduct == 1))
                    CS_CrockPotListAddFood("F_coffee", 30);
                if (CsFtMeats >= 2.5 && CsFtFishes >= 1.5 && CsFtIce == 0)
                    CS_CrockPotListAddFood("F_surf_'n'_turf", 30);
                if (CsFtWobster >= 1 && CsFtButter >= 1 && CsFtMeats == 0 && CsFtIce == 0)
                    CS_CrockPotListAddFood("F_lobster_dinner", 25);
                if (CsFtBanana >= 1 && CsFtIce >= 1 && CsFtTwigs >= 1 && CsFtMeats == 0 && CsFtFishes == 0)
                    CS_CrockPotListAddFood("F_banana_pop", 20);
                if (CsFtFishes >= 1 && CsFtSeaweed == 2)
                    CS_CrockPotListAddFood("F_california_roll", 20);
                if (CsFtJellyfish >= 1 && CsFtIce >= 1 && CsFtTwigs >= 1)
                    CS_CrockPotListAddFood("F_jelly-O_pop", 20);
                if (CsFtFishes >= 2 && CsFtIce >= 1)
                    CS_CrockPotListAddFood("F_ceviche", 20);
                if (CsFtSharkFin >= 1)
                    CS_CrockPotListAddFood("F_shark_fin_soup", 20);
                if (CsFtFishes >= 2.5)
                    CS_CrockPotListAddFood("F_seafood_gumbo", 10);
            //------------------------其他------------------------
            if (CsFtRoyalJelly >= 1 && CsFtTwigs == 0 && CsFtMonsterFoods == 0)
                CS_CrockPotListAddFood("F_jellybeans", 12);
            if (CsFtCactusFlesh >= 1 && CsFtMoleworm >= 1 && CsFtFruit == 0)
                CS_CrockPotListAddFood("F_guacamole", 10);
            if (CsFtCactusFlower >= 1 && CsFtVegetables >= 2 && CsFtFruit == 0 && CsFtMeats == 0 && CsFtEggs == 0 && CsFtSweetener == 0 && CsFtTwigs == 0)
                CS_CrockPotListAddFood("F_flower_salad", 10);
            if (CsFtDairyProduct >= 1 && CsFtIce >= 1 && CsFtSweetener >= 1 && CsFtMeats == 0 && CsFtEggs == 0 && CsFtVegetables == 0 && CsFtTwigs == 0)
                CS_CrockPotListAddFood("F_ice_cream", 10);
            if (CsFtWatermelon >= 1 && CsFtIce >= 1 && CsFtTwigs >= 1 && CsFtMeats == 0 && CsFtEggs == 0 && CsFtVegetables == 0)
                CS_CrockPotListAddFood("F_melonsicle", 10);
            if (CsFtRoastedBirchnut >= 1 && CsFtBerries >= 1 && CsFtFruit >= 1 && CsFtMeats == 0 && CsFtEggs == 0 && CsFtVegetables == 0 && CsFtSweetener == 0)
                CS_CrockPotListAddFood("F_trail_mix", 10);
            if (CsFtVegetables >= 1.5 && CsFtMeats >= 1.5)
                CS_CrockPotListAddFood("F_spicy_chili", 10);
            if (CsFtEel >= 1 && CsFtLichen >= 1)
                CS_CrockPotListAddFood("F_unagi", 20);
            if (CsFtPumpkin >= 1 && CsFtSweetener >= 2)
                CS_CrockPotListAddFood("F_pumpkin_cookie", 10);
            if (CsFtCorn >= 1 && CsFtHoney >= 1 && CsFtTwigs >= 1)
                CS_CrockPotListAddFood("F_powdercake", 10);
            if (CsFtMandrake >= 1)
                CS_CrockPotListAddFood("F_mandrake_soup", 10);
            if (CsFtFishes >= 0.5 && CsFtTwigs == 1)
                CS_CrockPotListAddFood("F_fishsticks", 10);
            if (CsFtFishes >= 0.5 && CsFtCorn >= 1)
                CS_CrockPotListAddFood("F_fish_tacos", 10);
            if (CsFtMeats >= 1.5 && CsFtEggs >= 2 && CsFtVegetables == 0)
                CS_CrockPotListAddFood("F_bacon_and_eggs", 10);
            if (CsFtDrumstick >= 2 && CsFtMeats >= 1.5 && (CsFtVegetables >= 0.5 || CsFtFruit >= 0.5))
                CS_CrockPotListAddFood("F_turkey_dinner", 10);
            if (CsFtSweetener >= 3 && CsFtMeats == 0)
                CS_CrockPotListAddFood("F_taffy", 10);
            if (CsFtButter >= 1 && CsFtEggs >= 1 && CsFtBerries >= 1)
                CS_CrockPotListAddFood("F_waffles", 10);
            if (CsFtMonsterFoods >= 2 && CsFtTwigs == 0)
                CS_CrockPotListAddFood("F_monster_lasagna", 10);
            if (CsFtEggs >= 1 && CsFtMeats >= 0.5 && CsFtVegetables >= 0.5 && CsFtTwigs == 0)
                CS_CrockPotListAddFood("F_pierogi", 5);
            if (CsFtMeats >= 0.5 && CsFtTwigs == 1 && CsFtMonsterFoods <= 1)
                CS_CrockPotListAddFood("F_kabobs", 5);
            if (CsFtMeats >= 2 && CsFtHoney >= 1 && CsFtTwigs == 0)
                CS_CrockPotListAddFood("F_honey_ham", 2);
            if (CsFtMeats >= 0.5 && CsFtMeats < 2 && CsFtHoney >= 1 && CsFtTwigs == 0)
                CS_CrockPotListAddFood("F_honey_nuggets", 2);
            if (CsFtButterflyWings >= 1 && CsFtVegetables >= 0.5 && CsFtMeats == 0)
                CS_CrockPotListAddFood("F_butter_muffin", 1);
            if (CsFtFrogLegs >= 1 && CsFtVegetables >= 0.5)
                CS_CrockPotListAddFood("F_froggle_bunwich", 1);
            if (CsFtDragonFruit >= 1 && CsFtMeats == 0)
                CS_CrockPotListAddFood("F_dragonpie", 1);
            if (CsFtEggplant >= 1 && CsFtVegetables >= 0.5)
                CS_CrockPotListAddFood("F_stuffed_eggplant", 1);
            if (CsFtVegetables >= 0.5 && CsFtMeats == 0 && CsFtTwigs == 0)
                CS_CrockPotListAddFood("F_ratatouille", 0);
            if (CsFtFruit >= 0.5 && CsFtMeats == 0 && CsFtVegetables == 0)
            {
                if (CsFtFruit < 3)
                {
                    CS_CrockPotListAddFood("F_fist_full_of_jam", 0);
                }
                else
                {
                    if (CsFtTwigs == 0)
                    {
                        CS_CrockPotListAddFood("F_fist_full_of_jam", 0);
                        CS_CrockPotListAddFood("F_fruit_medley", 0);
                    }
                    else
                    {
                        CS_CrockPotListAddFood("F_fruit_medley", 0);
                    }
                }
            }
            if (CsFtMeats >= 3 && CsFtTwigs == 0)
            {
                CS_CrockPotListAddFood("F_meaty_stew", 0);
            }

            if (CsFtMeats >= 0.5 && CsFtMeats < 3 && CsFtTwigs == 0)
                CS_CrockPotListAddFood("F_meatballs", -1);
            #endregion
            #region 食物判断
            if (CrockPotListIndex == -1)
            {
                CS_CrockPotListAddFood("F_wet_goop", -2);
            }
            CsFoodName = CrockPotList[0];
            CS_image_Food_Result_Source(CrockPotList[0]);
            #endregion
            #region 选择按钮显示判断
            if (CrockPotListIndex < 1)
            {
                SwitchLeftButton.Visibility = Visibility.Collapsed;
                SwitchRightButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                SwitchLeftButton.Visibility = Visibility.Visible;
                SwitchRightButton.Visibility = Visibility.Visible;
                SwitchLeftButton.IsEnabled = false;
                SwitchRightButton.IsEnabled = true;
            }
            #endregion
            //显示食物名称
            FoodResultTextBlock.Text = CS_Food_Text(CsFoodName);
            #region 自动清空材料
            if (AutoCleanToggleSwitch.IsOn)
            {
                CsRecipe1 = "";
                CsRecipe2 = "";
                CsRecipe3 = "";
                CsRecipe4 = "";
                Food1Image.Source = null;
                Food2Image.Source = null;
                Food3Image.Source = null;
                Food4Image.Source = null;
            }
            #endregion
        }
        //向食物列表添加食物
        private void CS_CrockPotListAddFood(string foodName, sbyte foodPriority)
        {
            if (foodPriority >= CrockPotMaxPriority)
            {
                CrockPotMaxPriority = foodPriority;
                CrockPotListIndex += 1;
                CrockPotList.Add(foodName);
            }
        }
        //烹饪结果图片
        private void CS_image_Food_Result_Source(string source)
        {
            FoodResultImage.Source = new BitmapImage(new Uri($"ms-appx:///Assets/GameResources/Foods/{source}.png"));
        }
        //烹饪结果文字
        private string CS_Food_Text(string source)
        {
            switch (source)
            {
                case "F_fresh_fruit_crepes":
                    return "新鲜水果薄饼";
                case "F_monster_tartare":
                    return "怪物鞑靼";
                case "F_mussel_bouillabaise":
                    return "贝类淡菜汤";
                case "F_sweet_potato_souffle":
                    return "薯蛋奶酥";
                case "F_lobster_bisque":
                    return "龙虾浓汤";
                case "F_bisque":
                    return "汤";
                case "F_coffee":
                    return "咖啡";
                case "F_surf_'n'_turf":
                    return "海鲜牛排";
                case "F_lobster_dinner":
                    return "龙虾正餐";
                case "F_banana_pop":
                    return "香蕉冰淇淋";
                case "F_california_roll":
                    return "加州卷";
                case "F_jelly-O_pop":
                    return "果冻冰淇淋";
                case "F_ceviche":
                    return "橘汁腌鱼";
                case "F_shark_fin_soup":
                    return "鱼翅汤";
                case "F_seafood_gumbo":
                    return "海鲜汤";
                case "F_jellybeans":
                    return "糖豆";
                case "F_guacamole":
                    return Global.GameVersion == 4 ? "鼹梨沙拉酱" : "鼹鼠鳄梨酱";
                case "F_flower_salad":
                    return Global.GameVersion == 4 ? "花沙拉" : "花瓣沙拉";
                case "F_ice_cream":
                    return "冰淇淋";
                case "F_melonsicle":
                    return Global.GameVersion == 4 ? "西瓜冰棍" : "西瓜冰";
                case "F_trail_mix":
                    return Global.GameVersion == 4 ? "什锦干果" : "水果杂烩";
                case "F_spicy_chili":
                    return Global.GameVersion == 4 ? "辣椒炖肉" : "辣椒酱";
                case "F_unagi":
                    return Global.GameVersion == 4 ? "鳗鱼料理" : "鳗鱼";
                case "F_pumpkin_cookie":
                    return "南瓜饼";
                case "F_powdercake":
                    return "芝士蛋糕";
                case "F_mandrake_soup":
                    return Global.GameVersion == 4 ? "曼德拉草汤" : "曼德拉汤";
                case "F_fishsticks":
                    return Global.GameVersion == 4 ? "炸鱼排" : "炸鱼条";
                case "F_fish_tacos":
                    return Global.GameVersion == 4 ? "鱼肉玉米卷" : "玉米饼包炸鱼";
                case "F_bacon_and_eggs":
                    return "培根煎蛋";
                case "F_turkey_dinner":
                    return Global.GameVersion == 4 ? "火鸡大餐" : "火鸡正餐";
                case "F_taffy":
                    return "太妃糖";
                case "F_waffles":
                    return "华夫饼";
                case "F_monster_lasagna":
                    return "怪物千层饼";
                case "F_pierogi":
                    return Global.GameVersion == 4 ? "波兰水饺" : "饺子";
                case "F_kabobs":
                    return "肉串";
                case "F_honey_ham":
                    return "蜜汁火腿";
                case "F_honey_nuggets":
                    return Global.GameVersion == 4 ? "蜜汁卤肉" : "甜蜜金砖";
                case "F_butter_muffin":
                    return Global.GameVersion == 4 ? "奶油玛芬" : "奶油松饼";
                case "F_froggle_bunwich":
                    return Global.GameVersion == 4 ? "蛙腿三明治" : "青蛙圆面包三明治";
                case "F_dragonpie":
                    return "火龙果派";
                case "F_stuffed_eggplant":
                    return Global.GameVersion == 4 ? "酿茄子" : "香酥茄盒";
                case "F_ratatouille":
                    return Global.GameVersion == 4 ? "蔬菜大杂烩" : "蔬菜杂烩";
                case "F_fist_full_of_jam":
                    return Global.GameVersion == 4 ? "满满的果酱" : "果酱蜜饯";
                case "F_fruit_medley":
                    return Global.GameVersion == 4 ? "水果圣代" : "水果沙拉";
                case "F_meaty_stew":
                    return "肉汤";
                case "F_meatballs":
                    return "肉丸";
                case "F_wet_goop":
                    return Global.GameVersion == 4 ? "失败料理" : "湿腻焦糊";
                default:
                    return null;
            }
        }
        //左右切换按钮
        private void SwitchLeftButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SwitchRightButton.IsEnabled = true;
            if (FoodIndex != 0)
            {
                FoodIndex -= 1;
                if (FoodIndex == 0)
                {
                    SwitchLeftButton.IsEnabled = false;
                }
                CsFoodName = CrockPotList[FoodIndex];
                CS_image_Food_Result_Source(CrockPotList[FoodIndex]);
            }
            FoodResultTextBlock.Text = CS_Food_Text(CsFoodName);
        }

        private void SwitchRightButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SwitchLeftButton.IsEnabled = true;
            if (FoodIndex != CrockPotListIndex)
            {
                FoodIndex += 1;
                if (FoodIndex == CrockPotListIndex)
                {
                    SwitchRightButton.IsEnabled = false;
                }
                CsFoodName = CrockPotList[FoodIndex];
                CS_image_Food_Result_Source(CrockPotList[FoodIndex]);
            }
            FoodResultTextBlock.Text = CS_Food_Text(CsFoodName);
        }
        //烹饪结果跳转
        private void ResultButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //TODO
        }

//        private void button_CS_Food_Result_Click(object sender, RoutedEventArgs e)
//        {
//            foreach (UIElement expanderStackpanel in WrapPanel_Right_Food.Children)
//            {
//                foreach (UIElement buttonWithText in ((ExpanderStackpanel)expanderStackpanel).UcWrapPanel.Children)
//                {
//                    string[] RightButtonTag = (string[])(((ButtonWithText)buttonWithText).UCButton.Tag);
//                    string RightButtonTag0 = RightButtonTag[0];
//                    RightButtonTag0 = RSN.GetFileName(RightButtonTag0);
//                    if (CrockPotList[FoodIndex] == RightButtonTag0)
//                    {
//                        Sidebar_Food_Click(null, null);
//                        Sidebar_Food.IsChecked = true;
//                        WrapPanel_Left_Food.UpdateLayout();
//                        Food_Click(((ButtonWithText)buttonWithText).UCButton, null);
//                        WrapPanel_Left_Food.UpdateLayout();
//                        //Point point = ((ButtonWithText)buttonWithText).TransformToVisual(WrapPanel_Right_Food).Transform(new Point(0, 0));
//                        //ScrollViewer_Right_Food.ScrollToVerticalOffset(point.Y);
//                    }
//                }
//            }
//        }
    }
}
