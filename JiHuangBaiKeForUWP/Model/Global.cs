﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using JiHuangBaiKeForUWP.View;
using Microsoft.Toolkit.Uwp.UI.Animations;
using Newtonsoft.Json;

namespace JiHuangBaiKeForUWP.Model
{

    public static class Global
    {
        /// <summary>
        /// 应用程序文件夹
        /// </summary>
        public static readonly StorageFolder ApplicationFolder = ApplicationData.Current.LocalFolder;

        public static Grid RootGrid { get; set; }
        public static TextBlock FrameTitle { get; set; }
        public static Frame RootFrame { get; set; }
        public static List<ListBoxItem> MainPageListBoxItem { get; set; } = new List<ListBoxItem>();

        #region 颜色常量

        public static SolidColorBrush ColorGreen = new SolidColorBrush(Color.FromArgb(255, 94, 182, 96));     //绿色
        public static SolidColorBrush ColorKhaki = new SolidColorBrush(Color.FromArgb(255, 237, 182, 96));    //卡其布色/土黄色
        public static SolidColorBrush ColorRed = new SolidColorBrush(Color.FromArgb(255, 216, 82, 79));       //红色
        public static SolidColorBrush ColorBlue = new SolidColorBrush(Color.FromArgb(255, 51, 122, 184));     //蓝色
        public static SolidColorBrush ColorPurple = new SolidColorBrush(Color.FromArgb(255, 162, 133, 240));   //紫色
        public static SolidColorBrush ColorPink = new SolidColorBrush(Color.FromArgb(255, 240, 133, 211));     //粉色
        public static SolidColorBrush ColorCyan = new SolidColorBrush(Color.FromArgb(255, 21, 227, 234));     //青色
        public static SolidColorBrush ColorOrange = new SolidColorBrush(Color.FromArgb(255, 246, 166, 11));     //橙色
        public static SolidColorBrush ColorYellow = new SolidColorBrush(Color.FromArgb(255, 238, 232, 21));     //黄色
        public static SolidColorBrush ColorBorderCyan = new SolidColorBrush(Color.FromArgb(255, 178, 236, 237));     //天蓝色

        #endregion

        #region 游戏版本

        /// <summary>
        /// 游戏版本
        /// </summary>
        public static int GameVersion { get; set; }
        
        /// <summary>
        /// 内置游戏版本Json文件夹名
        /// </summary>
        public static string[] BuiltInGameVersionJsonFolder =
        {
            "DST", "Tencent", "DS", "ROG", "Shipwrecked"
        };

        #endregion

        /// <summary>
        /// 透明Style
        /// </summary>
        public static readonly Style Transparent = (Style)Application.Current.Resources["TransparentDialog"];

        /// <summary>
        /// 删除重复数据
        /// </summary>
        /// <param name="str">字符串数组</param>
        public static string[] StringDelRepeatData(string[] str)
        {
            var b = str.GroupBy(p => p).Select(p => p.Key).ToArray();
            if (b.Length != 1) return b;
            var temp = new List<string>
            {
                b[0],
                ""
            };
            b = temp.ToArray();
            return b;
        }

        /// <summary>
        /// 获取游戏图片位置
        /// </summary>
        /// <param name="str">图片名称</param>
        /// <returns>完整路径</returns>
        public static string GetGameResourcePath(string str)
        {
            var strHead = str.Substring(0, 1);
            switch (strHead)
            {
                case "A":
                    str = $"ms-appx:///Assets/GameResources/Creatures/{str}.png";
                    break;
                case "C":
                    str = $"ms-appx:///Assets/GameResources/Charcters/{str}.png";
                    break;
                case "F":
                    str = $"ms-appx:///Assets/GameResources/Foods/{str}.png";
                    break;
                case "G":
                    str = $"ms-appx:///Assets/GameResources/Goods/{str}.png";
                    break;
                case "N":
                    str = $"ms-appx:///Assets/GameResources/Natures/{str}.png";
                    break;
                case "S":
                    str = $"ms-appx:///Assets/GameResources/Sciences/{str}.png";
                    break;
                case "T":
                    str = $"ms-appx:///Assets/GameResources/Goods/{str}.png";
                    break;
            }
            return str;
        }

        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="path">长字符串</param>
        /// <returns>资源文件路径</returns>
        public static string GetFileName(string path)
        {
            path = path.Substring(path.LastIndexOf('/') + 1, path.Length - path.LastIndexOf('/') - 5);
            return path;
        }

        #region 对话框


        /// <summary>
        /// 显示对话框
        /// </summary>
        /// <param name="contentDialog">ContentDialog</param>
        public static async void ShowDialog(ContentDialog contentDialog)
        {
            ShowedDialog?.Hide();
            ShowedDialog = contentDialog;
            contentDialog.Closed += delegate
            {
//                await RootGrid.Blur(0, 0).StartAsync();
                contentDialog.Hide();
            };

            contentDialog.PrimaryButtonClick += delegate
            {
//                await RootGrid.Blur(0, 0).StartAsync();
                contentDialog.Hide();
            };
            //            await RootGrid.Blur(7, 100).StartAsync();
            await contentDialog.ShowAsync();
        }

        /// <summary>
        /// 对话框是否已显示
        /// </summary>
        public static ContentDialog ShowedDialog { get; set; }

        #endregion

        #region 自动搜索
        /// <summary>
        /// 自动建议框Item集合
        /// </summary>
        public static ObservableCollection<SuggestBoxItem> AutoSuggestBoxItem { get; set; } = new ObservableCollection<SuggestBoxItem>();
        /// <summary>
        /// 自动建议框Item集合数据源
        /// </summary>
        public static List<SuggestBoxItem> AutoSuggestBoxItemSource { get; set; } = new List<SuggestBoxItem>();
        #region 自动搜索List
        private static readonly List<Character> CharacterData = new List<Character>();
        private static readonly List<FoodRecipe2> FoodRecipeData = new List<FoodRecipe2>();
        private static readonly List<Food> FoodMeatData = new List<Food>();
        private static readonly List<Food> FoodVegetableData = new List<Food>();
        private static readonly List<Food> FoodFruitData = new List<Food>();
        private static readonly List<Food> FoodEggData = new List<Food>();
        private static readonly List<Food> FoodOtherData = new List<Food>();
        private static readonly List<Food> FoodNoFcData = new List<Food>();
        private static readonly List<Science> ScienceToolData = new List<Science>();
        private static readonly List<Science> ScienceLightData = new List<Science>();
        private static readonly List<Science> ScienceNauticalData = new List<Science>();
        private static readonly List<Science> ScienceSurvivalData = new List<Science>();
        private static readonly List<Science> ScienceFoodData = new List<Science>();
        private static readonly List<Science> ScienceTechnologyData = new List<Science>();
        private static readonly List<Science> ScienceFightData = new List<Science>();
        private static readonly List<Science> ScienceStructureData = new List<Science>();
        private static readonly List<Science> ScienceRefineData = new List<Science>();
        private static readonly List<Science> ScienceMagicData = new List<Science>();
        private static readonly List<Science> ScienceDressData = new List<Science>();
        private static readonly List<Science> ScienceAncientData = new List<Science>();
        private static readonly List<Science> ScienceBookData = new List<Science>();
        private static readonly List<Science> ScienceShadowData = new List<Science>();
        private static readonly List<Science> ScienceCritterData = new List<Science>();
        private static readonly List<Science> ScienceSculptData = new List<Science>();
        private static readonly List<Science> ScienceCartographyData = new List<Science>();
        private static readonly List<Science> ScienceOfferingsData = new List<Science>();
        private static readonly List<Science> ScienceVolcanoData = new List<Science>();
        private static readonly List<Creature> CreatureLandData = new List<Creature>();
        private static readonly List<Creature> CreatureOceanData = new List<Creature>();
        private static readonly List<Creature> CreatureFlyData = new List<Creature>();
        private static readonly List<Creature> CreatureCaveData = new List<Creature>();
        private static readonly List<Creature> CreatureEvilData = new List<Creature>();
        private static readonly List<Creature> CreatureOthersData = new List<Creature>();
        private static readonly List<Creature> CreatureBossData = new List<Creature>();
        private static readonly List<Nature> NaturalBiomesData = new List<Nature>();
        private static readonly List<GoodMaterial> GoodMaterialData = new List<GoodMaterial>();
        private static readonly List<GoodEquipment> GoodEquipmentData = new List<GoodEquipment>();
        private static readonly List<GoodSapling> GoodSaplingData = new List<GoodSapling>();
        private static readonly List<GoodCreatures> GoodCreaturesData = new List<GoodCreatures>();
        private static readonly List<Good> GoodTrinketsData = new List<Good>();
        private static readonly List<GoodTurf> GoodTurfData = new List<GoodTurf>();
        private static readonly List<GoodPet> GoodPetData = new List<GoodPet>();
        private static readonly List<GoodUnlock> GoodUnlockData = new List<GoodUnlock>();
        private static readonly List<Good> GoodHallowedNightsData = new List<Good>();
        private static readonly List<Good> GoodWintersFeastData = new List<Good>();
        private static readonly List<Good> GoodYearOfTheGobblerData = new List<Good>();
        private static readonly List<Good> GoodComponentData = new List<Good>();
        private static readonly List<Good> GoodOthersData = new List<Good>();
        #endregion

        /// <summary>
        /// 设置自动搜索框数据源
        /// </summary>
        public static async Task SetAutoSuggestBoxItem()
        {
            #region 清空列表
            AutoSuggestBoxItem.Clear();
            AutoSuggestBoxItemSource.Clear();
            CharacterData.Clear();
            FoodRecipeData.Clear();
            FoodMeatData.Clear();
            FoodVegetableData.Clear();
            FoodFruitData.Clear();
            FoodEggData.Clear();
            FoodOtherData.Clear();
            FoodNoFcData.Clear();
            ScienceToolData.Clear();
            ScienceLightData.Clear();
            ScienceNauticalData.Clear();
            ScienceSurvivalData.Clear();
            ScienceFoodData.Clear();
            ScienceTechnologyData.Clear();
            ScienceFightData.Clear();
            ScienceStructureData.Clear();
            ScienceRefineData.Clear();
            ScienceMagicData.Clear();
            ScienceDressData.Clear();
            ScienceAncientData.Clear();
            ScienceBookData.Clear();
            ScienceShadowData.Clear();
            ScienceCritterData.Clear();
            ScienceSculptData.Clear();
            ScienceCartographyData.Clear();
            ScienceOfferingsData.Clear();
            ScienceVolcanoData.Clear();
            CreatureLandData.Clear();
            CreatureOceanData.Clear();
            CreatureFlyData.Clear();
            CreatureCaveData.Clear();
            CreatureEvilData.Clear();
            CreatureOthersData.Clear();
            CreatureBossData.Clear();
            NaturalBiomesData.Clear();
            GoodMaterialData.Clear();
            GoodEquipmentData.Clear();
            GoodSaplingData.Clear();
            GoodCreaturesData.Clear();
            GoodTrinketsData.Clear();
            GoodTurfData.Clear();
            GoodPetData.Clear();
            GoodUnlockData.Clear();
            GoodHallowedNightsData.Clear();
            GoodWintersFeastData.Clear();
            GoodYearOfTheGobblerData.Clear();
            GoodComponentData.Clear();
            GoodOthersData.Clear();
            #endregion
            #region 人物
            var character = JsonConvert.DeserializeObject<CharacterRootObject>(await GetJsonString("Characters.json"));
            foreach (var characterItems in character.Character)
            {
                CharacterData.Add(characterItems);
            }
            foreach (var characterItems in CharacterData)
            {
                characterItems.Picture = GetGameResourcePath(characterItems.Picture);
            }
            foreach (var characterItems in CharacterData)
            {
                AutoSuggestBoxItemSourceAdd(characterItems, "Character");
            }
            #endregion
            #region 食物
            var food = JsonConvert.DeserializeObject<FoodRootObject>(await GetJsonString("Foods.json"));
            foreach (var foodRecipeItems in food.FoodRecipe.FoodRecipes)
            {
                FoodRecipeData.Add(foodRecipeItems);
            }
            foreach (var foodRecipeItems in FoodRecipeData)
            {
                foodRecipeItems.Picture = GetGameResourcePath(foodRecipeItems.Picture);
            }
            foreach (var foodMeatsItems in food.FoodMeats.Foods)
            {
                FoodMeatData.Add(foodMeatsItems);
            }
            foreach (var foodMeatsItems in FoodMeatData)
            {
                foodMeatsItems.Picture = GetGameResourcePath(foodMeatsItems.Picture);
            }
            foreach (var foodVegetablesItems in food.FoodVegetables.Foods)
            {
                FoodVegetableData.Add(foodVegetablesItems);
            }
            foreach (var foodVegetablesItems in FoodVegetableData)
            {
                foodVegetablesItems.Picture = GetGameResourcePath(foodVegetablesItems.Picture);
            }
            foreach (var foodFruitItems in food.FoodFruit.Foods)
            {
                FoodFruitData.Add(foodFruitItems);
            }
            foreach (var foodFruitItems in FoodFruitData)
            {
                foodFruitItems.Picture = GetGameResourcePath(foodFruitItems.Picture);
            }
            foreach (var foodEggsItems in food.FoodEggs.Foods)
            {
                FoodEggData.Add(foodEggsItems);
            }
            foreach (var foodEggsItems in FoodEggData)
            {
                foodEggsItems.Picture = GetGameResourcePath(foodEggsItems.Picture);
            }
            foreach (var foodOthersItems in food.FoodOthers.Foods)
            {
                FoodOtherData.Add(foodOthersItems);
            }
            foreach (var foodOthersItems in FoodOtherData)
            {
                foodOthersItems.Picture = GetGameResourcePath(foodOthersItems.Picture);
            }
            foreach (var foodNoFcItems in food.FoodNoFc.Foods)
            {
                FoodNoFcData.Add(foodNoFcItems);
            }
            foreach (var foodNoFcItems in FoodNoFcData)
            {
                foodNoFcItems.Picture = GetGameResourcePath(foodNoFcItems.Picture);
            }
            foreach (var foodRecipeItems in FoodRecipeData)
            {
                AutoSuggestBoxItemSourceAdd(foodRecipeItems,"FoodRecipe");
            }
            foreach (var foodItems in FoodMeatData)
            {
                AutoSuggestBoxItemSourceAdd(foodItems, "FoodMeats");
            }
            foreach (var foodItems in FoodVegetableData)
            {
                AutoSuggestBoxItemSourceAdd(foodItems, "FoodVegetables");
            }
            foreach (var foodItems in FoodFruitData)
            {
                AutoSuggestBoxItemSourceAdd(foodItems, "FoodFruits");
            }
            foreach (var foodItems in FoodEggData)
            {
                AutoSuggestBoxItemSourceAdd(foodItems, "FoodEggs");
            }
            foreach (var foodItems in FoodOtherData)
            {
                AutoSuggestBoxItemSourceAdd(foodItems, "FoodOthers");
            }
            foreach (var foodItems in FoodNoFcData)
            {
                AutoSuggestBoxItemSourceAdd(foodItems, "FoodNoFc");
            }
            #endregion
            #region 科技
            var science = JsonConvert.DeserializeObject<ScienceRootObject>(await GetJsonString("Sciences.json"));
            foreach (var scienceToolItems in science.Tool.Science)
            {
                ScienceToolData.Add(scienceToolItems);
            }
            foreach (var scienceToolItems in ScienceToolData)
            {
                scienceToolItems.Picture = GetGameResourcePath(scienceToolItems.Picture);
            }
            foreach (var scienceLightItems in science.Light.Science)
            {
                ScienceLightData.Add(scienceLightItems);
            }
            foreach (var scienceLightItems in ScienceLightData)
            {
                scienceLightItems.Picture = GetGameResourcePath(scienceLightItems.Picture);
            }
            foreach (var scienceNauticalItems in science.Nautical.Science)
            {
                ScienceNauticalData.Add(scienceNauticalItems);
            }
            foreach (var scienceNauticalItems in ScienceNauticalData)
            {
                scienceNauticalItems.Picture = GetGameResourcePath(scienceNauticalItems.Picture);
            }
            foreach (var scienceSurvivalItems in science.Survival.Science)
            {
                ScienceSurvivalData.Add(scienceSurvivalItems);
            }
            foreach (var scienceSurvivalItems in ScienceSurvivalData)
            {
                scienceSurvivalItems.Picture = GetGameResourcePath(scienceSurvivalItems.Picture);
            }
            foreach (var scienceFoodItems in science.Foods.Science)
            {
                ScienceFoodData.Add(scienceFoodItems);
            }
            foreach (var scienceFoodItems in ScienceFoodData)
            {
                scienceFoodItems.Picture = GetGameResourcePath(scienceFoodItems.Picture);
            }
            foreach (var scienceTechnologyItems in science.Technology.Science)
            {
                ScienceTechnologyData.Add(scienceTechnologyItems);
            }
            foreach (var scienceTechnologyItems in ScienceTechnologyData)
            {
                scienceTechnologyItems.Picture = GetGameResourcePath(scienceTechnologyItems.Picture);
            }
            foreach (var scienceFightItems in science.Fight.Science)
            {
                ScienceFightData.Add(scienceFightItems);
            }
            foreach (var scienceFightItems in ScienceFightData)
            {
                scienceFightItems.Picture = GetGameResourcePath(scienceFightItems.Picture);
            }
            foreach (var scienceStructureItems in science.Structure.Science)
            {
                ScienceStructureData.Add(scienceStructureItems);
            }
            foreach (var scienceStructureItems in ScienceStructureData)
            {
                scienceStructureItems.Picture = GetGameResourcePath(scienceStructureItems.Picture);
            }
            foreach (var scienceRefineItems in science.Refine.Science)
            {
                ScienceRefineData.Add(scienceRefineItems);
            }
            foreach (var scienceRefineItems in ScienceRefineData)
            {
                scienceRefineItems.Picture = GetGameResourcePath(scienceRefineItems.Picture);
            }
            foreach (var scienceMagicItems in science.Magic.Science)
            {
                ScienceMagicData.Add(scienceMagicItems);
            }
            foreach (var scienceMagicItems in ScienceMagicData)
            {
                scienceMagicItems.Picture = GetGameResourcePath(scienceMagicItems.Picture);
            }
            foreach (var scienceDressItems in science.Dress.Science)
            {
                ScienceDressData.Add(scienceDressItems);
            }
            foreach (var scienceDressItems in ScienceDressData)
            {
                scienceDressItems.Picture = GetGameResourcePath(scienceDressItems.Picture);
            }
            foreach (var scienceAncientItems in science.Ancient.Science)
            {
                ScienceAncientData.Add(scienceAncientItems);
            }
            foreach (var scienceAncientItems in ScienceAncientData)
            {
                scienceAncientItems.Picture = GetGameResourcePath(scienceAncientItems.Picture);
            }
            foreach (var scienceBookItems in science.Book.Science)
            {
                ScienceBookData.Add(scienceBookItems);
            }
            foreach (var scienceBookItems in ScienceBookData)
            {
                scienceBookItems.Picture = GetGameResourcePath(scienceBookItems.Picture);
            }
            foreach (var scienceShadowItems in science.Shadow.Science)
            {
                ScienceShadowData.Add(scienceShadowItems);
            }
            foreach (var scienceShadowItems in ScienceShadowData)
            {
                scienceShadowItems.Picture = GetGameResourcePath(scienceShadowItems.Picture);
            }
            foreach (var scienceCritterItems in science.Critter.Science)
            {
                ScienceCritterData.Add(scienceCritterItems);
            }
            foreach (var scienceCritterItems in ScienceCritterData)
            {
                scienceCritterItems.Picture = GetGameResourcePath(scienceCritterItems.Picture);
            }
            foreach (var scienceSculptItems in science.Sculpt.Science)
            {
                ScienceSculptData.Add(scienceSculptItems);
            }
            foreach (var scienceSculptItems in ScienceSculptData)
            {
                scienceSculptItems.Picture = GetGameResourcePath(scienceSculptItems.Picture);
            }
            foreach (var scienceCartographyItems in science.Cartography.Science)
            {
                ScienceCartographyData.Add(scienceCartographyItems);
            }
            foreach (var scienceCartographyItems in ScienceCartographyData)
            {
                scienceCartographyItems.Picture = GetGameResourcePath(scienceCartographyItems.Picture);
            }
            foreach (var scienceOfferingsItems in science.Offerings.Science)
            {
                ScienceOfferingsData.Add(scienceOfferingsItems);
            }
            foreach (var scienceOfferingsItems in ScienceOfferingsData)
            {
                scienceOfferingsItems.Picture = GetGameResourcePath(scienceOfferingsItems.Picture);
            }
            foreach (var scienceVolcanoItems in science.Volcano.Science)
            {
                ScienceVolcanoData.Add(scienceVolcanoItems);
            }
            foreach (var scienceVolcanoItems in ScienceVolcanoData)
            {
                scienceVolcanoItems.Picture = GetGameResourcePath(scienceVolcanoItems.Picture);
            }
            foreach (var scienceItems in ScienceToolData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceTool");
            }
            foreach (var scienceItems in ScienceLightData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceLight");
            }
            foreach (var scienceItems in ScienceNauticalData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceNautical");
            }
            foreach (var scienceItems in ScienceSurvivalData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceSurvival");
            }
            foreach (var scienceItems in ScienceFoodData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceFood");
            }
            foreach (var scienceItems in ScienceTechnologyData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceTechnology");
            }
            foreach (var scienceItems in ScienceFightData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceFight");
            }
            foreach (var scienceItems in ScienceStructureData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceStructure");
            }
            foreach (var scienceItems in ScienceRefineData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceRefine");
            }
            foreach (var scienceItems in ScienceMagicData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceMagic");
            }
            foreach (var scienceItems in ScienceDressData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceDress");
            }
            foreach (var scienceItems in ScienceAncientData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceAncient");
            }
            foreach (var scienceItems in ScienceBookData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceBook");
            }
            foreach (var scienceItems in ScienceShadowData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceShadow");
            }
            foreach (var scienceItems in ScienceCritterData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceCritter");
            }
            foreach (var scienceItems in ScienceSculptData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceSculpt");
            }
            foreach (var scienceItems in ScienceCartographyData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceCartography");
            }
            foreach (var scienceItems in ScienceOfferingsData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceOfferings");
            }
            foreach (var scienceItems in ScienceVolcanoData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceVolcano");
            }
            #endregion
            #region 生物
            var creature = JsonConvert.DeserializeObject<CreaturesRootObject>(await GetJsonString("Creatures.json"));
            foreach (var creatureLandItems in creature.Land.Creature)
            {
                CreatureLandData.Add(creatureLandItems);
            }
            foreach (var creatureLandItems in CreatureLandData)
            {
                creatureLandItems.Picture = GetGameResourcePath(creatureLandItems.Picture);
            }
            foreach (var creatureOceanItems in creature.Ocean.Creature)
            {
                CreatureOceanData.Add(creatureOceanItems);
            }
            foreach (var creatureOceanItems in CreatureOceanData)
            {
                creatureOceanItems.Picture = GetGameResourcePath(creatureOceanItems.Picture);
            }
            foreach (var creatureFlyItems in creature.Fly.Creature)
            {
                CreatureFlyData.Add(creatureFlyItems);
            }
            foreach (var creatureFlyItems in CreatureFlyData)
            {
                creatureFlyItems.Picture = GetGameResourcePath(creatureFlyItems.Picture);
            }
            foreach (var creatureCaveItems in creature.Cave.Creature)
            {
                CreatureCaveData.Add(creatureCaveItems);
            }
            foreach (var creatureCaveItems in CreatureCaveData)
            {
                creatureCaveItems.Picture = GetGameResourcePath(creatureCaveItems.Picture);
            }
            foreach (var creatureEvilItems in creature.Evil.Creature)
            {
                CreatureEvilData.Add(creatureEvilItems);
            }
            foreach (var creatureEvilItems in CreatureEvilData)
            {
                creatureEvilItems.Picture = GetGameResourcePath(creatureEvilItems.Picture);
            }
            foreach (var creatureOthersItems in creature.Others.Creature)
            {
                CreatureOthersData.Add(creatureOthersItems);
            }
            foreach (var creatureOthersItems in CreatureOthersData)
            {
                creatureOthersItems.Picture = GetGameResourcePath(creatureOthersItems.Picture);
            }
            foreach (var creatureBossItems in creature.Boss.Creature)
            {
                CreatureBossData.Add(creatureBossItems);
            }
            foreach (var creatureBossItems in CreatureBossData)
            {
                creatureBossItems.Picture = GetGameResourcePath(creatureBossItems.Picture);
            }
            foreach (var creatureItems in CreatureLandData)
            {
                AutoSuggestBoxItemSourceAdd(creatureItems, "CreatureLand");
            }
            foreach (var creatureItems in CreatureOceanData)
            {
                AutoSuggestBoxItemSourceAdd(creatureItems, "CreatureOcean");
            }
            foreach (var creatureItems in CreatureFlyData)
            {
                AutoSuggestBoxItemSourceAdd(creatureItems, "CreatureFly");
            }
            foreach (var creatureItems in CreatureCaveData)
            {
                AutoSuggestBoxItemSourceAdd(creatureItems, "CreatureCave");
            }
            foreach (var creatureItems in CreatureEvilData)
            {
                AutoSuggestBoxItemSourceAdd(creatureItems, "CreatureEvil");
            }
            foreach (var creatureItems in CreatureOthersData)
            {
                AutoSuggestBoxItemSourceAdd(creatureItems, "CreatureOther");
            }
            foreach (var creatureItems in CreatureBossData)
            {
                AutoSuggestBoxItemSourceAdd(creatureItems, "CreatureBoss");
            }
            #endregion
            #region 自然
            var natural = JsonConvert.DeserializeObject<NaturalRootObject>(await GetJsonString("Natural.json"));
            foreach (var naturalLandItems in natural.Biomes.Nature)
            {
                NaturalBiomesData.Add(naturalLandItems);
            }
            foreach (var naturalLandItems in NaturalBiomesData)
            {
                naturalLandItems.Picture = GetGameResourcePath(naturalLandItems.Picture);
            }
            foreach (var naturalItems in NaturalBiomesData)
            {
                AutoSuggestBoxItemSourceAdd(naturalItems, "NaturalBiomes");
            }
            #endregion
            #region 物品
            var good = JsonConvert.DeserializeObject<GoodsRootObject>(await GetJsonString("Goods.json"));
            foreach (var goodMaterialItems in good.Material.GoodMaterial)
            {
                GoodMaterialData.Add(goodMaterialItems);
            }
            foreach (var goodMaterialItems in GoodMaterialData)
            {
                goodMaterialItems.Picture = GetGameResourcePath(goodMaterialItems.Picture);
            }
            foreach (var goodEquipmentItems in good.Equipment.GoodEquipment)
            {
                GoodEquipmentData.Add(goodEquipmentItems);
            }
            foreach (var goodEquipmentItems in GoodEquipmentData)
            {
                goodEquipmentItems.Picture = GetGameResourcePath(goodEquipmentItems.Picture);
            }
            foreach (var goodSaplingItems in good.Sapling.GoodSapling)
            {
                GoodSaplingData.Add(goodSaplingItems);
            }
            foreach (var goodSaplingItems in GoodSaplingData)
            {
                goodSaplingItems.Picture = GetGameResourcePath(goodSaplingItems.Picture);
            }
            foreach (var goodCreaturesItems in good.Creatures.GoodCreatures)
            {
                GoodCreaturesData.Add(goodCreaturesItems);
            }
            foreach (var goodCreaturesItems in GoodCreaturesData)
            {
                goodCreaturesItems.Picture = GetGameResourcePath(goodCreaturesItems.Picture);
            }
            foreach (var goodTrinketsItems in good.Trinkets.GoodTrinkets)
            {
                GoodTrinketsData.Add(goodTrinketsItems);
            }
            foreach (var goodTrinketsItems in GoodTrinketsData)
            {
                goodTrinketsItems.Picture = GetGameResourcePath(goodTrinketsItems.Picture);
            }
            foreach (var goodTurfItems in good.Turf.GoodTurf)
            {
                GoodTurfData.Add(goodTurfItems);
            }
            foreach (var goodTurfItems in GoodTurfData)
            {
                goodTurfItems.Picture = GetGameResourcePath(goodTurfItems.Picture);
            }
            foreach (var goodPetItems in good.Pet.GoodPet)
            {
                GoodPetData.Add(goodPetItems);
            }
            foreach (var goodPetItems in GoodPetData)
            {
                goodPetItems.Picture = GetGameResourcePath(goodPetItems.Picture);
            }
            foreach (var goodUnlockItems in good.Unlock.GoodUnlock)
            {
                GoodUnlockData.Add(goodUnlockItems);
            }
            foreach (var goodUnlockItems in GoodUnlockData)
            {
                goodUnlockItems.Picture = GetGameResourcePath(goodUnlockItems.Picture);
            }
            foreach (var goodHallowedNightsItems in good.HallowedNights.Good)
            {
                GoodHallowedNightsData.Add(goodHallowedNightsItems);
            }
            foreach (var goodHallowedNightsItems in GoodHallowedNightsData)
            {
                goodHallowedNightsItems.Picture = GetGameResourcePath(goodHallowedNightsItems.Picture);
            }
            foreach (var goodWinterwsFeastItems in good.WintersFeast.Good)
            {
                GoodWintersFeastData.Add(goodWinterwsFeastItems);
            }
            foreach (var goodWinterwsFeastItems in GoodWintersFeastData)
            {
                goodWinterwsFeastItems.Picture = GetGameResourcePath(goodWinterwsFeastItems.Picture);
            }
            foreach (var goodYearOfTheGobblerItems in good.YearOfTheGobbler.Good)
            {
                GoodYearOfTheGobblerData.Add(goodYearOfTheGobblerItems);
            }
            foreach (var goodYearOfTheGobblerItems in GoodYearOfTheGobblerData)
            {
                goodYearOfTheGobblerItems.Picture = GetGameResourcePath(goodYearOfTheGobblerItems.Picture);
            }
            foreach (var goodComponentItems in good.Component.Good)
            {
                GoodComponentData.Add(goodComponentItems);
            }
            foreach (var goodComponentItems in GoodComponentData)
            {
                goodComponentItems.Picture = GetGameResourcePath(goodComponentItems.Picture);
            }
            foreach (var goodOthersItems in good.GoodOthers.Good)
            {
                GoodOthersData.Add(goodOthersItems);
            }
            foreach (var goodOthersItems in GoodOthersData)
            {
                goodOthersItems.Picture = GetGameResourcePath(goodOthersItems.Picture);
            }
            foreach (var goodMaterialItems in GoodMaterialData)
            {
                AutoSuggestBoxItemSourceAdd(goodMaterialItems, "GoodMaterial");
            }
            foreach (var goodEquipmentItems in GoodEquipmentData)
            {
                AutoSuggestBoxItemSourceAdd(goodEquipmentItems, "GoodEquipment");
            }
            foreach (var goodSaplingItems in GoodSaplingData)
            {
                AutoSuggestBoxItemSourceAdd(goodSaplingItems, "GoodSapling");
            }
            foreach (var goodCreaturesItems in GoodCreaturesData)
            {
                AutoSuggestBoxItemSourceAdd(goodCreaturesItems, "GoodCreatures");
            }
            foreach (var goodTrinketsItems in GoodTrinketsData)
            {
                AutoSuggestBoxItemSourceAdd(goodTrinketsItems, "GoodTrinkets");
            }
            foreach (var goodTurfItems in GoodTurfData)
            {
                AutoSuggestBoxItemSourceAdd(goodTurfItems, "GoodTurf");
            }
            foreach (var goodPetItems in GoodPetData)
            {
                AutoSuggestBoxItemSourceAdd(goodPetItems, "GoodPet");
            }
            foreach (var goodUnlockItems in GoodUnlockData)
            {
                AutoSuggestBoxItemSourceAdd(goodUnlockItems, "GoodUnlock");
            }
            foreach (var goodHallowedNightsItems in GoodHallowedNightsData)
            {
                AutoSuggestBoxItemSourceAdd(goodHallowedNightsItems, "GoodHallowedNights");
            }
            foreach (var goodWintersFeastItems in GoodWintersFeastData)
            {
                AutoSuggestBoxItemSourceAdd(goodWintersFeastItems, "GoodWintersFeast");
            }
            foreach (var goodYearOfTheGobblerItems in GoodYearOfTheGobblerData)
            {
                AutoSuggestBoxItemSourceAdd(goodYearOfTheGobblerItems, "GoodYearOfTheGobbler");
            }
            foreach (var goodComponentItems in GoodComponentData)
            {
                AutoSuggestBoxItemSourceAdd(goodComponentItems, "GoodComponent");
            }
            foreach (var goodOthersItems in GoodOthersData)
            {
                AutoSuggestBoxItemSourceAdd(goodOthersItems, "GoodOthers");
            }
            #endregion
            #region 把AutoSuggestBoxItemSource数据源加入到AutoSuggestBoxItem
            foreach (var item in AutoSuggestBoxItemSource)
            {
                AutoSuggestBoxItem.Add(item);
            }
            #endregion
        }

        /// <summary>
        /// 自动搜索框数据添加
        /// </summary>
        /// <param name="obj">Items对象</param>
        /// <param name="sourcePath">源路径</param>
        public static void AutoSuggestBoxItemSourceAdd(object obj, string sourcePath)
        {
            var suggestBoxItem = new SuggestBoxItem();
            var type = obj.GetType();
            if (type == typeof(Character))
            {
                suggestBoxItem.Picture = ((Character)obj).Picture;
                suggestBoxItem.Name = ((Character)obj).Name;
                suggestBoxItem.EnName = ((Character)obj).EnName;
                suggestBoxItem.Category = "人物";
            }
            else if (type == typeof(FoodRecipe2))
            {
                suggestBoxItem.Picture = ((FoodRecipe2)obj).Picture;
                suggestBoxItem.Name = ((FoodRecipe2)obj).Name;
                suggestBoxItem.EnName = ((FoodRecipe2)obj).EnName;
                suggestBoxItem.Category = "食物";
            }
            else if (type == typeof(Food))
            {
                suggestBoxItem.Picture = ((Food)obj).Picture;
                suggestBoxItem.Name = ((Food)obj).Name;
                suggestBoxItem.EnName = ((Food)obj).EnName;
                suggestBoxItem.Category = "食物";
            }
            else if (type == typeof(Science))
            {
                suggestBoxItem.Picture = ((Science)obj).Picture;
                suggestBoxItem.Name = ((Science)obj).Name;
                suggestBoxItem.EnName = ((Science)obj).EnName;
                suggestBoxItem.Category = "科技";
            }
            else if (type == typeof(Creature))
            {
                suggestBoxItem.Picture = ((Creature)obj).Picture;
                suggestBoxItem.Name = ((Creature)obj).Name;
                suggestBoxItem.EnName = ((Creature)obj).EnName;
                suggestBoxItem.Category = "生物";
            }
            else if (type == typeof(Nature))
            {
                suggestBoxItem.Picture = ((Nature)obj).Picture;
                suggestBoxItem.Name = ((Nature)obj).Name;
                suggestBoxItem.EnName = ((Nature)obj).EnName;
                suggestBoxItem.Category = "自然";
            }
            else if (type == typeof(GoodMaterial))
            {
                suggestBoxItem.Picture = ((GoodMaterial)obj).Picture;
                suggestBoxItem.Name = ((GoodMaterial)obj).Name;
                suggestBoxItem.EnName = ((GoodMaterial)obj).EnName;
                suggestBoxItem.Category = "物品";
            }
            else if (type == typeof(GoodEquipment))
            {
                suggestBoxItem.Picture = ((GoodEquipment)obj).Picture;
                suggestBoxItem.Name = ((GoodEquipment)obj).Name;
                suggestBoxItem.EnName = ((GoodEquipment)obj).EnName;
                suggestBoxItem.Category = "物品";
            }
            else if (type == typeof(GoodSapling))
            {
                suggestBoxItem.Picture = ((GoodSapling)obj).Picture;
                suggestBoxItem.Name = ((GoodSapling)obj).Name;
                suggestBoxItem.EnName = ((GoodSapling)obj).EnName;
                suggestBoxItem.Category = "物品";
            }
            else if (type == typeof(GoodCreatures))
            {
                suggestBoxItem.Picture = ((GoodCreatures)obj).Picture;
                suggestBoxItem.Name = ((GoodCreatures)obj).Name;
                suggestBoxItem.EnName = ((GoodCreatures)obj).EnName;
                suggestBoxItem.Category = "物品";
            }
            else if (type == typeof(GoodTurf))
            {
                suggestBoxItem.Picture = ((GoodTurf)obj).Picture;
                suggestBoxItem.Name = ((GoodTurf)obj).Name;
                suggestBoxItem.EnName = ((GoodTurf)obj).EnName;
                suggestBoxItem.Category = "物品";
            }
            else if (type == typeof(GoodPet))
            {
                suggestBoxItem.Picture = ((GoodPet)obj).Picture;
                suggestBoxItem.Name = ((GoodPet)obj).Name;
                suggestBoxItem.EnName = ((GoodPet)obj).EnName;
                suggestBoxItem.Category = "物品";
            }
            else if (type == typeof(GoodUnlock))
            {
                suggestBoxItem.Picture = ((GoodUnlock)obj).Picture;
                suggestBoxItem.Name = ((GoodUnlock)obj).Name;
                suggestBoxItem.EnName = ((GoodUnlock)obj).EnName;
                suggestBoxItem.Category = "物品";
            }
            else if (type == typeof(Good))
            {
                suggestBoxItem.Picture = ((Good)obj).Picture;
                suggestBoxItem.Name = ((Good)obj).Name;
                suggestBoxItem.EnName = ((Good)obj).EnName;
                suggestBoxItem.Category = "物品";
            }
            suggestBoxItem.SourcePath = sourcePath;
            AutoSuggestBoxItemSource.Add(suggestBoxItem);
        }

        #endregion

        /// <summary>
        /// 返回Json文本
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>string类型文本</returns>
        public static async Task<string> GetJsonString(string fileName)
        {
            //            var folderExists = await ApplicationFolder.TryGetItemAsync(BuiltInGameVersionJsonFolder[GameVersion]);
            //            var uri = folderExists == null ? new Uri("ms-appx:///Json/" + BuiltInGameVersionJsonFolder[GameVersion] + "/" + fileName) : new Uri(ApplicationFolder.Path + "/" + BuiltInGameVersionJsonFolder[GameVersion] + "/" + fileName);
            var uri = new Uri("ms-appx:///Json/" + BuiltInGameVersionJsonFolder[GameVersion] + "/" + fileName);
            var storageFile = await StorageFile.GetFileFromApplicationUriAsync(uri);
            var str = await FileIO.ReadTextAsync(storageFile);
            return str;
        }
    }
}
