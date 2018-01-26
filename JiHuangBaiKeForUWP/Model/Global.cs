using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.System.Profile;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media;
using JiHuangBaiKeForUWP.View;
using Microsoft.Toolkit.Uwp.UI.Animations;
using Newtonsoft.Json;
using System.Reflection;
using Windows.Foundation;

namespace JiHuangBaiKeForUWP.Model
{

    public static class Global
    {
        #region 常量
        /// <summary>
        /// 应用程序文件夹
        /// </summary>
        public static readonly StorageFolder ApplicationFolder = ApplicationData.Current.LocalFolder;

        /// <summary>
        /// 错误堆栈字符串
        /// </summary>
        public static string ErrorStackString { get; set; }

        /// <summary>
        /// MainPage需要保存在Global里额几个控件对象
        /// </summary>
        public static ObservableCollection<HamburgerMenuItem> _gameDataHamburgerMenuItem { get; set; }
        public static HamburgerMenuItem SkinHamburgerMenuItem = new HamburgerMenuItem
        {
            Icon = "\x4E0E",
            Text = "皮肤",
            Color = new SolidColorBrush(Colors.White),
            Selected = Visibility.Collapsed,
            NavigatePage = typeof(SkinsPage)
        };
        public static Grid RootGrid { get; set; }
        public static TextBlock FrameTitle { get; set; }
        public static Grid AutoSuggestGrid { get; set; }
        public static Frame RootFrame { get; set; }
        public static ListView IconsListViewGameData { get; set; }
        public static ListView IconsListViewSettingAndAbout { get; set; }
        public static Grid SettingPageRootGrid { get; set; }
        public static Color TinkColor { get; set; }
        public static double TinkOpacity { get; set; }

        /// <summary>
        /// Content动画位移常量
        /// </summary>
        public enum ContentThemeTransitionShift
        {
            LeftOrUpShift = -28,
            NoneShift = 0,
            RightOrDownShift = 28
        }

        #endregion

        #region 后退按钮相关
        /// <summary>
        /// 页面堆栈
        /// </summary>
        public static Stack<PageStackItem> PageStack = new Stack<PageStackItem>();
        
        /// <summary>
        /// 后退按钮请求处理
        /// </summary>
        public static void App_BackRequested()
        {
            if (PageStack.Count > 1)
            {
                PageStack.Pop();
                var pageStackItem = PageStack.Peek();
                RootFrame.Navigate(pageStackItem.SourcePageType, pageStackItem.Parameter);
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// ScrollViewer滚动到指定位置
        /// </summary>
        /// <param name="scrollViewer">ScrollViewer的Name</param>
        /// <param name="uiElement">UIElement的Name</param>
        public static void ScrollToElement(ScrollViewer scrollViewer, UIElement uiElement)
        {
            var transform = uiElement.TransformToVisual(scrollViewer);
            var point = transform.TransformPoint(new Point(0, 0));
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (point.Y != 0)
            {
                var y = point.Y + scrollViewer.VerticalOffset;
                scrollViewer.ChangeView(null, y, null, true);
            }
        }

        /// <summary>
        /// 遍历视觉树
        /// </summary>
        /// <typeparam name="T">泛型T</typeparam>
        /// <param name="results">结果List</param>
        /// <param name="startNode">开始节点</param>
        public static void FindChildren<T>(List<T> results, DependencyObject startNode) where T : DependencyObject
        {
            var count = VisualTreeHelper.GetChildrenCount(startNode);
            for (var i = 0; i < count; i++)
            {
                var current = VisualTreeHelper.GetChild(startNode, i);
                if (current.GetType() == typeof(T) || current.GetType().GetTypeInfo().IsSubclassOf(typeof(T)))
                {
                    var asType = (T)current;
                    results.Add(asType);
                }
                FindChildren(results, current);
            }
        }

        /// <summary>
        /// 页面跳转(处理汉堡菜单)
        /// </summary>
        /// <param name="index">页面序号</param>
        public static void PageJump(int index)
        {
            // ReSharper disable once PossibleNullReferenceException
            foreach (var hamburgerMenuItem in IconsListViewGameData.Items)
            {
                ((HamburgerMenuItem)hamburgerMenuItem).Color = new SolidColorBrush(Colors.White);
                ((HamburgerMenuItem)hamburgerMenuItem).Selected = Visibility.Collapsed;
            }
            // ReSharper disable once PossibleNullReferenceException
            foreach (var hamburgerMenuItem in IconsListViewSettingAndAbout.Items)
            {
                ((HamburgerMenuItem)hamburgerMenuItem).Color = new SolidColorBrush(Colors.White);
                ((HamburgerMenuItem)hamburgerMenuItem).Selected = Visibility.Collapsed;
            }
            ((HamburgerMenuItem)IconsListViewGameData.Items[index]).Selected = Visibility.Visible;
            ((HamburgerMenuItem)IconsListViewGameData.Items[index]).Color = new SolidColorBrush(AccentColor);
            RootFrame.Navigate(((HamburgerMenuItem)IconsListViewGameData.Items[index]).NavigatePage);
            FrameTitle.Text = ((HamburgerMenuItem)IconsListViewGameData.Items[index]).Text;
        }

        /// <summary>
        /// 获取系统版本
        /// </summary>
        /// <returns>系统版本号</returns>
        public static ulong GetOsVersion()
        {
            var version = Convert.ToUInt64(AnalyticsInfo.VersionInfo.DeviceFamilyVersion);
            return version >> 16 & 0xFFFF;
        }
        #endregion

        #region 颜色常量
        public static Color AccentColor = (Color)Application.Current.Resources["SystemAccentColor"];

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
        public static SolidColorBrush ColorGray = new SolidColorBrush(Color.FromArgb(255, 244, 244, 245));     //灰色
        public static SolidColorBrush ColorWhite = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));     //白色
        public static SolidColorBrush ColorBlack = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));     //黑色
        public static SolidColorBrush SplitViewPaneBackgroundLight = new SolidColorBrush(Color.FromArgb(255, 48, 48, 48));     //黑色
        public static SolidColorBrush SplitViewPaneBackgroundDark = new SolidColorBrush(Color.FromArgb(255, 5, 5, 5));     //黑色
        public static SolidColorBrush ApplicationPageBackgroundThemeBrushLight = new SolidColorBrush(Color.FromArgb(255, 204, 204, 204));     //黑色
        
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

        #region 对话框

        /// <summary>
        /// 透明Style
        /// </summary>
        public static readonly Style Transparent = (Style)Application.Current.Resources["TransparentDialog"];

        /// <summary>
        /// 显示对话框
        /// </summary>
        /// <param name="contentDialog">ContentDialog</param>
        public static async void ShowDialog(ContentDialog contentDialog)
        {
            ShowedDialog?.Hide();
            ShowedDialog = contentDialog;
            contentDialog.Closed += async delegate
            {
                await RootGrid.Blur(0, 0).StartAsync();
                contentDialog.Hide();
            };

            contentDialog.PrimaryButtonClick += async delegate
            {
                await RootGrid.Blur(0, 0).StartAsync();
                contentDialog.Hide();
            };
            await RootGrid.Blur(7, 100).StartAsync();
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
        public static readonly List<FoodRecipe2> FoodRecipeData = new List<FoodRecipe2>();
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
        private static readonly List<NatureBiomes> NaturalBiomesData = new List<NatureBiomes>();
        private static readonly List<NatureSmallPlant> NaturalSmallPlantsData = new List<NatureSmallPlant>();
        private static readonly List<NatureTree> NaturalTreesData = new List<NatureTree>();
        private static readonly List<NatureCreatureNest> NaturalCreatureNestData = new List<NatureCreatureNest>();
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
        private static readonly List<Skin> SkinsBodyData = new List<Skin>();
        private static readonly List<Skin> SkinsHandsData = new List<Skin>();
        private static readonly List<Skin> SkinsLegsData = new List<Skin>();
        private static readonly List<Skin> SkinsFeetData = new List<Skin>();
        private static readonly List<Skin> SkinsCharactersData = new List<Skin>();
        private static readonly List<Skin> SkinsItemsData = new List<Skin>();
        private static readonly List<Skin> SkinsStructuresData = new List<Skin>();
        private static readonly List<Skin> SkinsCrittersData = new List<Skin>();
        private static readonly List<Skin> SkinsSpecialData = new List<Skin>();
        private static readonly List<Skin> SkinsHallowedNightsSkinsData = new List<Skin>();
        private static readonly List<Skin> SkinsWintersFeastSkinsData = new List<Skin>();
        private static readonly List<Skin> SkinsYearOfTheGobblerSkinsData = new List<Skin>();
        private static readonly List<Skin> SkinsTheForgeData = new List<Skin>();
        private static readonly List<Skin> SkinsEmotesData = new List<Skin>();
        private static readonly List<Skin> SkinsOutfitSetsData = new List<Skin>();
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
            NaturalSmallPlantsData.Clear();
            NaturalTreesData.Clear();
            NaturalCreatureNestData.Clear();
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
            SkinsBodyData.Clear();
            SkinsHandsData.Clear();
            SkinsLegsData.Clear();
            SkinsFeetData.Clear();
            SkinsCharactersData.Clear();
            SkinsItemsData.Clear();
            SkinsStructuresData.Clear();
            SkinsCrittersData.Clear();
            SkinsSpecialData.Clear();
            SkinsHallowedNightsSkinsData.Clear();
            SkinsWintersFeastSkinsData.Clear();
            SkinsYearOfTheGobblerSkinsData.Clear();
            SkinsTheForgeData.Clear();
            SkinsEmotesData.Clear();
            SkinsOutfitSetsData.Clear();
            #endregion

            #region 人物
            var character = JsonConvert.DeserializeObject<CharacterRootObject>(await StringProcess.GetJsonString("Characters.json"));
            foreach (var characterItems in character.Character)
            {
                characterItems.Picture = StringProcess.GetGameResourcePath(characterItems.Picture);
                CharacterData.Add(characterItems);
                AutoSuggestBoxItemSourceAdd(characterItems, "Character");
            }
            #endregion
            #region 食物
            var food = JsonConvert.DeserializeObject<FoodRootObject>(await StringProcess.GetJsonString("Foods.json"));
            foreach (var foodRecipeItems in food.FoodRecipe.FoodRecipes)
            {
                foodRecipeItems.Picture = StringProcess.GetGameResourcePath(foodRecipeItems.Picture);
                FoodRecipeData.Add(foodRecipeItems);
                AutoSuggestBoxItemSourceAdd(foodRecipeItems, "FoodRecipe");
            }
            foreach (var foodMeatsItems in food.FoodMeats.Foods)
            {
                foodMeatsItems.Picture = StringProcess.GetGameResourcePath(foodMeatsItems.Picture);
                FoodMeatData.Add(foodMeatsItems);
                AutoSuggestBoxItemSourceAdd(foodMeatsItems, "FoodMeats");
            }
            foreach (var foodVegetablesItems in food.FoodVegetables.Foods)
            {
                foodVegetablesItems.Picture = StringProcess.GetGameResourcePath(foodVegetablesItems.Picture);
                FoodVegetableData.Add(foodVegetablesItems);
                AutoSuggestBoxItemSourceAdd(foodVegetablesItems, "FoodVegetables");
            }
            foreach (var foodFruitItems in food.FoodFruit.Foods)
            {
                foodFruitItems.Picture = StringProcess.GetGameResourcePath(foodFruitItems.Picture);
                FoodFruitData.Add(foodFruitItems);
                AutoSuggestBoxItemSourceAdd(foodFruitItems, "FoodFruits");
            }
            foreach (var foodEggsItems in food.FoodEggs.Foods)
            {
                foodEggsItems.Picture = StringProcess.GetGameResourcePath(foodEggsItems.Picture);
                FoodEggData.Add(foodEggsItems);
                AutoSuggestBoxItemSourceAdd(foodEggsItems, "FoodEggs");
            }
            foreach (var foodOthersItems in food.FoodOthers.Foods)
            {
                foodOthersItems.Picture = StringProcess.GetGameResourcePath(foodOthersItems.Picture);
                FoodOtherData.Add(foodOthersItems);
                AutoSuggestBoxItemSourceAdd(foodOthersItems, "FoodOthers");
            }
            foreach (var foodNoFcItems in food.FoodNoFc.Foods)
            {
                foodNoFcItems.Picture = StringProcess.GetGameResourcePath(foodNoFcItems.Picture);
                FoodNoFcData.Add(foodNoFcItems);
                AutoSuggestBoxItemSourceAdd(foodNoFcItems, "FoodNoFc");
            }
            #endregion
            #region 科技
            var science = JsonConvert.DeserializeObject<ScienceRootObject>(await StringProcess.GetJsonString("Sciences.json"));
            foreach (var scienceToolItems in science.Tool.Science)
            {
                scienceToolItems.Picture = StringProcess.GetGameResourcePath(scienceToolItems.Picture);
                ScienceToolData.Add(scienceToolItems);
                AutoSuggestBoxItemSourceAdd(scienceToolItems, "ScienceTool");
            }
            foreach (var scienceLightItems in science.Light.Science)
            {
                scienceLightItems.Picture = StringProcess.GetGameResourcePath(scienceLightItems.Picture);
                ScienceLightData.Add(scienceLightItems);
                AutoSuggestBoxItemSourceAdd(scienceLightItems, "ScienceLight");

            }
            foreach (var scienceNauticalItems in science.Nautical.Science)
            {
                scienceNauticalItems.Picture = StringProcess.GetGameResourcePath(scienceNauticalItems.Picture);
                ScienceNauticalData.Add(scienceNauticalItems);
                AutoSuggestBoxItemSourceAdd(scienceNauticalItems, "ScienceNautical");

            }
            foreach (var scienceSurvivalItems in science.Survival.Science)
            {
                scienceSurvivalItems.Picture = StringProcess.GetGameResourcePath(scienceSurvivalItems.Picture);
                ScienceSurvivalData.Add(scienceSurvivalItems);
                AutoSuggestBoxItemSourceAdd(scienceSurvivalItems, "ScienceSurvival");

            }
            foreach (var scienceFoodItems in science.Foods.Science)
            {
                scienceFoodItems.Picture = StringProcess.GetGameResourcePath(scienceFoodItems.Picture);
                ScienceFoodData.Add(scienceFoodItems);
                AutoSuggestBoxItemSourceAdd(scienceFoodItems, "ScienceFood");

            }
            foreach (var scienceTechnologyItems in science.Technology.Science)
            {
                scienceTechnologyItems.Picture = StringProcess.GetGameResourcePath(scienceTechnologyItems.Picture);
                ScienceTechnologyData.Add(scienceTechnologyItems);
                AutoSuggestBoxItemSourceAdd(scienceTechnologyItems, "ScienceTechnology");

            }
            foreach (var scienceFightItems in science.Fight.Science)
            {
                scienceFightItems.Picture = StringProcess.GetGameResourcePath(scienceFightItems.Picture);
                ScienceFightData.Add(scienceFightItems);
                AutoSuggestBoxItemSourceAdd(scienceFightItems, "ScienceFight");

            }
            foreach (var scienceStructureItems in science.Structure.Science)
            {
                scienceStructureItems.Picture = StringProcess.GetGameResourcePath(scienceStructureItems.Picture);
                ScienceStructureData.Add(scienceStructureItems);
                AutoSuggestBoxItemSourceAdd(scienceStructureItems, "ScienceStructure");

            }
            foreach (var scienceRefineItems in science.Refine.Science)
            {
                scienceRefineItems.Picture = StringProcess.GetGameResourcePath(scienceRefineItems.Picture);
                ScienceRefineData.Add(scienceRefineItems);
                AutoSuggestBoxItemSourceAdd(scienceRefineItems, "ScienceRefine");

            }
            foreach (var scienceMagicItems in science.Magic.Science)
            {
                scienceMagicItems.Picture = StringProcess.GetGameResourcePath(scienceMagicItems.Picture);
                ScienceMagicData.Add(scienceMagicItems);
                AutoSuggestBoxItemSourceAdd(scienceMagicItems, "ScienceMagic");

            }
            foreach (var scienceDressItems in science.Dress.Science)
            {
                scienceDressItems.Picture = StringProcess.GetGameResourcePath(scienceDressItems.Picture);
                ScienceDressData.Add(scienceDressItems);
                AutoSuggestBoxItemSourceAdd(scienceDressItems, "ScienceDress");

            }
            foreach (var scienceAncientItems in science.Ancient.Science)
            {
                scienceAncientItems.Picture = StringProcess.GetGameResourcePath(scienceAncientItems.Picture);
                ScienceAncientData.Add(scienceAncientItems);
                AutoSuggestBoxItemSourceAdd(scienceAncientItems, "ScienceAncient");

            }
            foreach (var scienceBookItems in science.Book.Science)
            {
                scienceBookItems.Picture = StringProcess.GetGameResourcePath(scienceBookItems.Picture);
                ScienceBookData.Add(scienceBookItems);
                AutoSuggestBoxItemSourceAdd(scienceBookItems, "ScienceBook");

            }
            foreach (var scienceShadowItems in science.Shadow.Science)
            {
                scienceShadowItems.Picture = StringProcess.GetGameResourcePath(scienceShadowItems.Picture);
                ScienceShadowData.Add(scienceShadowItems);
                AutoSuggestBoxItemSourceAdd(scienceShadowItems, "ScienceShadow");

            }
            foreach (var scienceCritterItems in science.Critter.Science)
            {
                scienceCritterItems.Picture = StringProcess.GetGameResourcePath(scienceCritterItems.Picture);
                ScienceCritterData.Add(scienceCritterItems);
                AutoSuggestBoxItemSourceAdd(scienceCritterItems, "ScienceCritter");

            }
            foreach (var scienceSculptItems in science.Sculpt.Science)
            {
                scienceSculptItems.Picture = StringProcess.GetGameResourcePath(scienceSculptItems.Picture);
                ScienceSculptData.Add(scienceSculptItems);
                AutoSuggestBoxItemSourceAdd(scienceSculptItems, "ScienceSculpt");

            }
            foreach (var scienceCartographyItems in science.Cartography.Science)
            {
                scienceCartographyItems.Picture = StringProcess.GetGameResourcePath(scienceCartographyItems.Picture);
                ScienceCartographyData.Add(scienceCartographyItems);
                AutoSuggestBoxItemSourceAdd(scienceCartographyItems, "ScienceCartography");

            }
            foreach (var scienceOfferingsItems in science.Offerings.Science)
            {
                scienceOfferingsItems.Picture = StringProcess.GetGameResourcePath(scienceOfferingsItems.Picture);
                ScienceOfferingsData.Add(scienceOfferingsItems);
                AutoSuggestBoxItemSourceAdd(scienceOfferingsItems, "ScienceOfferings");

            }
            foreach (var scienceVolcanoItems in science.Volcano.Science)
            {
                scienceVolcanoItems.Picture = StringProcess.GetGameResourcePath(scienceVolcanoItems.Picture);
                ScienceVolcanoData.Add(scienceVolcanoItems);
                AutoSuggestBoxItemSourceAdd(scienceVolcanoItems, "ScienceVolcano");

            }
            #endregion
            #region 生物
            var creature = JsonConvert.DeserializeObject<CreaturesRootObject>(await StringProcess.GetJsonString("Creatures.json"));
            foreach (var creatureLandItems in creature.Land.Creature)
            {
                creatureLandItems.Picture = StringProcess.GetGameResourcePath(creatureLandItems.Picture);
                CreatureLandData.Add(creatureLandItems);
                AutoSuggestBoxItemSourceAdd(creatureLandItems, "CreatureLand");
            }
            foreach (var creatureOceanItems in creature.Ocean.Creature)
            {
                creatureOceanItems.Picture = StringProcess.GetGameResourcePath(creatureOceanItems.Picture);
                CreatureOceanData.Add(creatureOceanItems);
                AutoSuggestBoxItemSourceAdd(creatureOceanItems, "CreatureOcean");
            }
            foreach (var creatureFlyItems in creature.Fly.Creature)
            {
                creatureFlyItems.Picture = StringProcess.GetGameResourcePath(creatureFlyItems.Picture);
                CreatureFlyData.Add(creatureFlyItems);
                AutoSuggestBoxItemSourceAdd(creatureFlyItems, "CreatureFly");
            }
            foreach (var creatureCaveItems in creature.Cave.Creature)
            {
                creatureCaveItems.Picture = StringProcess.GetGameResourcePath(creatureCaveItems.Picture);
                CreatureCaveData.Add(creatureCaveItems);
                AutoSuggestBoxItemSourceAdd(creatureCaveItems, "CreatureCave");
            }
            foreach (var creatureEvilItems in creature.Evil.Creature)
            {
                creatureEvilItems.Picture = StringProcess.GetGameResourcePath(creatureEvilItems.Picture);
                CreatureEvilData.Add(creatureEvilItems);
                AutoSuggestBoxItemSourceAdd(creatureEvilItems, "CreatureEvil");
            }
            foreach (var creatureOthersItems in creature.Others.Creature)
            {
                creatureOthersItems.Picture = StringProcess.GetGameResourcePath(creatureOthersItems.Picture);
                CreatureOthersData.Add(creatureOthersItems);
                AutoSuggestBoxItemSourceAdd(creatureOthersItems, "CreatureOther");
            }
            foreach (var creatureBossItems in creature.Boss.Creature)
            {
                creatureBossItems.Picture = StringProcess.GetGameResourcePath(creatureBossItems.Picture);
                CreatureBossData.Add(creatureBossItems);
                AutoSuggestBoxItemSourceAdd(creatureBossItems, "CreatureBoss");
            }
            #endregion
            #region 自然
            var natural = JsonConvert.DeserializeObject<NaturalRootObject>(await StringProcess.GetJsonString("Natural.json"));
            foreach (var naturalBiomesItems in natural.Biomes.NatureBiomes)
            {
                naturalBiomesItems.Picture = StringProcess.GetGameResourcePath(naturalBiomesItems.Picture);
                NaturalBiomesData.Add(naturalBiomesItems);
                AutoSuggestBoxItemSourceAdd(naturalBiomesItems, "NaturalBiomes");
            }
            foreach (var naturalSmallPlantItems in natural.SmallPlants.NatureSmallPlant)
            {
                naturalSmallPlantItems.Picture = StringProcess.GetGameResourcePath(naturalSmallPlantItems.Picture);
                NaturalSmallPlantsData.Add(naturalSmallPlantItems);
                AutoSuggestBoxItemSourceAdd(naturalSmallPlantItems, "NaturalSmallPlants");
            }
            foreach (var naturalTreeItems in natural.Trees.NatureTree)
            {
                naturalTreeItems.Picture = StringProcess.GetGameResourcePath(naturalTreeItems.Picture);
                NaturalTreesData.Add(naturalTreeItems);
                AutoSuggestBoxItemSourceAdd(naturalTreeItems, "NaturalTrees");
            }
            foreach (var naturalCreatureNestItems in natural.CreatureNests.NatureCreatureNest)
            {
                naturalCreatureNestItems.Picture = StringProcess.GetGameResourcePath(naturalCreatureNestItems.Picture);
                NaturalCreatureNestData.Add(naturalCreatureNestItems);
                AutoSuggestBoxItemSourceAdd(naturalCreatureNestItems, "NaturalCreatureNests");
            }
            #endregion
            #region 物品
            var good = JsonConvert.DeserializeObject<GoodsRootObject>(await StringProcess.GetJsonString("Goods.json"));
            foreach (var goodMaterialItems in good.Material.GoodMaterial)
            {
                goodMaterialItems.Picture = StringProcess.GetGameResourcePath(goodMaterialItems.Picture);
                GoodMaterialData.Add(goodMaterialItems);
                AutoSuggestBoxItemSourceAdd(goodMaterialItems, "GoodMaterial");
            }
            foreach (var goodEquipmentItems in good.Equipment.GoodEquipment)
            {
                goodEquipmentItems.Picture = StringProcess.GetGameResourcePath(goodEquipmentItems.Picture);
                GoodEquipmentData.Add(goodEquipmentItems);
                AutoSuggestBoxItemSourceAdd(goodEquipmentItems, "GoodEquipment");
            }
            foreach (var goodSaplingItems in good.Sapling.GoodSapling)
            {
                goodSaplingItems.Picture = StringProcess.GetGameResourcePath(goodSaplingItems.Picture);
                GoodSaplingData.Add(goodSaplingItems);
                AutoSuggestBoxItemSourceAdd(goodSaplingItems, "GoodSapling");
            }
            foreach (var goodCreaturesItems in good.Creatures.GoodCreatures)
            {
                goodCreaturesItems.Picture = StringProcess.GetGameResourcePath(goodCreaturesItems.Picture);
                GoodCreaturesData.Add(goodCreaturesItems);
                AutoSuggestBoxItemSourceAdd(goodCreaturesItems, "GoodCreatures");
            }
            foreach (var goodTrinketsItems in good.Trinkets.GoodTrinkets)
            {
                goodTrinketsItems.Picture = StringProcess.GetGameResourcePath(goodTrinketsItems.Picture);
                GoodTrinketsData.Add(goodTrinketsItems);
                AutoSuggestBoxItemSourceAdd(goodTrinketsItems, "GoodTrinkets");
            }
            foreach (var goodTurfItems in good.Turf.GoodTurf)
            {
                goodTurfItems.Picture = StringProcess.GetGameResourcePath(goodTurfItems.Picture);
                GoodTurfData.Add(goodTurfItems);
                AutoSuggestBoxItemSourceAdd(goodTurfItems, "GoodTurf");
            }
            foreach (var goodPetItems in good.Pet.GoodPet)
            {
                goodPetItems.Picture = StringProcess.GetGameResourcePath(goodPetItems.Picture);
                GoodPetData.Add(goodPetItems);
                AutoSuggestBoxItemSourceAdd(goodPetItems, "GoodPet");
            }
            foreach (var goodUnlockItems in good.Unlock.GoodUnlock)
            {
                goodUnlockItems.Picture = StringProcess.GetGameResourcePath(goodUnlockItems.Picture);
                GoodUnlockData.Add(goodUnlockItems);
                AutoSuggestBoxItemSourceAdd(goodUnlockItems, "GoodUnlock");
            }
            foreach (var goodHallowedNightsItems in good.HallowedNights.Good)
            {
                goodHallowedNightsItems.Picture = StringProcess.GetGameResourcePath(goodHallowedNightsItems.Picture);
                GoodHallowedNightsData.Add(goodHallowedNightsItems);
                AutoSuggestBoxItemSourceAdd(goodHallowedNightsItems, "GoodHallowedNights");
            }
            foreach (var goodWintersFeastItems in good.WintersFeast.Good)
            {
                goodWintersFeastItems.Picture = StringProcess.GetGameResourcePath(goodWintersFeastItems.Picture);
                GoodWintersFeastData.Add(goodWintersFeastItems);
                AutoSuggestBoxItemSourceAdd(goodWintersFeastItems, "GoodWintersFeast");
            }
            foreach (var goodYearOfTheGobblerItems in good.YearOfTheGobbler.Good)
            {
                goodYearOfTheGobblerItems.Picture = StringProcess.GetGameResourcePath(goodYearOfTheGobblerItems.Picture);
                GoodYearOfTheGobblerData.Add(goodYearOfTheGobblerItems);
                AutoSuggestBoxItemSourceAdd(goodYearOfTheGobblerItems, "GoodYearOfTheGobbler");
            }
            foreach (var goodComponentItems in good.Component.Good)
            {
                goodComponentItems.Picture = StringProcess.GetGameResourcePath(goodComponentItems.Picture);
                GoodComponentData.Add(goodComponentItems);
                AutoSuggestBoxItemSourceAdd(goodComponentItems, "GoodComponent");
            }
            foreach (var goodOthersItems in good.GoodOthers.Good)
            {
                goodOthersItems.Picture = StringProcess.GetGameResourcePath(goodOthersItems.Picture);
                GoodOthersData.Add(goodOthersItems);
                AutoSuggestBoxItemSourceAdd(goodOthersItems, "GoodOthers");
            }
            #endregion
            #region 皮肤
            if (GameVersion == 0 || GameVersion == 1)
            {
                var skins = JsonConvert.DeserializeObject<SkinsRootObject>(await StringProcess.GetJsonStringSkins());
                foreach (var skinsBodyItem in skins.Body.Skin)
                {
                    skinsBodyItem.Picture = StringProcess.GetGameResourcePath(skinsBodyItem.Picture);
                    SkinsBodyData.Add(skinsBodyItem);
                    AutoSuggestBoxItemSourceAdd(skinsBodyItem, "SkinsBody");
                }
                foreach (var skinsHandsItem in skins.Hands.Skin)
                {
                    skinsHandsItem.Picture = StringProcess.GetGameResourcePath(skinsHandsItem.Picture);
                    SkinsHandsData.Add(skinsHandsItem);
                    AutoSuggestBoxItemSourceAdd(skinsHandsItem, "SkinsHands");
                }
                foreach (var skinsLegsItem in skins.Legs.Skin)
                {
                    skinsLegsItem.Picture = StringProcess.GetGameResourcePath(skinsLegsItem.Picture);
                    SkinsLegsData.Add(skinsLegsItem);
                    AutoSuggestBoxItemSourceAdd(skinsLegsItem, "SkinsLegs");
                }
                foreach (var skinsFeetItem in skins.Feet.Skin)
                {
                    skinsFeetItem.Picture = StringProcess.GetGameResourcePath(skinsFeetItem.Picture);
                    SkinsFeetData.Add(skinsFeetItem);
                    AutoSuggestBoxItemSourceAdd(skinsFeetItem, "SkinsFeet");
                }
                foreach (var skinsCharactersItem in skins.Characters.Skin)
                {
                    skinsCharactersItem.Picture = StringProcess.GetGameResourcePath(skinsCharactersItem.Picture);
                    SkinsCharactersData.Add(skinsCharactersItem);
                    AutoSuggestBoxItemSourceAdd(skinsCharactersItem, "SkinsCharacters");
                }
                foreach (var skinsItemsItem in skins.Items.Skin)
                {
                    skinsItemsItem.Picture = StringProcess.GetGameResourcePath(skinsItemsItem.Picture);
                    SkinsItemsData.Add(skinsItemsItem);
                    AutoSuggestBoxItemSourceAdd(skinsItemsItem, "SkinsItems");
                }
                foreach (var skinsStructuresItem in skins.Structures.Skin)
                {
                    skinsStructuresItem.Picture = StringProcess.GetGameResourcePath(skinsStructuresItem.Picture);
                    SkinsStructuresData.Add(skinsStructuresItem);
                    AutoSuggestBoxItemSourceAdd(skinsStructuresItem, "SkinsStructures");
                }
                foreach (var skinsCrittersItem in skins.Critters.Skin)
                {
                    skinsCrittersItem.Picture = StringProcess.GetGameResourcePath(skinsCrittersItem.Picture);
                    SkinsCrittersData.Add(skinsCrittersItem);
                    AutoSuggestBoxItemSourceAdd(skinsCrittersItem, "SkinsCritters");
                }
                foreach (var skinsSpecialItem in skins.Special.Skin)
                {
                    skinsSpecialItem.Picture = StringProcess.GetGameResourcePath(skinsSpecialItem.Picture);
                    SkinsSpecialData.Add(skinsSpecialItem);
                    AutoSuggestBoxItemSourceAdd(skinsSpecialItem, "SkinsSpecial");
                }
                foreach (var skinsHallowedNightsSkinsItem in skins.HallowedNightsSkins.Skin)
                {
                    skinsHallowedNightsSkinsItem.Picture = StringProcess.GetGameResourcePath(skinsHallowedNightsSkinsItem.Picture);
                    SkinsHallowedNightsSkinsData.Add(skinsHallowedNightsSkinsItem);
                    AutoSuggestBoxItemSourceAdd(skinsHallowedNightsSkinsItem, "SkinsHallowedNightsSkins");
                }
                foreach (var skinsWintersFeastSkinsItem in skins.WintersFeastSkins.Skin)
                {
                    skinsWintersFeastSkinsItem.Picture = StringProcess.GetGameResourcePath(skinsWintersFeastSkinsItem.Picture);
                    SkinsWintersFeastSkinsData.Add(skinsWintersFeastSkinsItem);
                    AutoSuggestBoxItemSourceAdd(skinsWintersFeastSkinsItem, "SkinsWintersFeastSkins");
                }
                foreach (var skinsYearOfTheGobblerSkinsItem in skins.YearOfTheGobblerSkins.Skin)
                {
                    skinsYearOfTheGobblerSkinsItem.Picture = StringProcess.GetGameResourcePath(skinsYearOfTheGobblerSkinsItem.Picture);
                    SkinsYearOfTheGobblerSkinsData.Add(skinsYearOfTheGobblerSkinsItem);
                    AutoSuggestBoxItemSourceAdd(skinsYearOfTheGobblerSkinsItem, "SkinsYearOfTheGobblerSkins");
                }
                foreach (var skinsTheForgeItem in skins.TheForge.Skin)
                {
                    skinsTheForgeItem.Picture = StringProcess.GetGameResourcePath(skinsTheForgeItem.Picture);
                    SkinsTheForgeData.Add(skinsTheForgeItem);
                    AutoSuggestBoxItemSourceAdd(skinsTheForgeItem, "SkinsTheForge");
                }
                foreach (var skinsEmotesItem in skins.Emotes.Skin)
                {
                    skinsEmotesItem.Picture = StringProcess.GetGameResourcePath(skinsEmotesItem.Picture);
                    SkinsEmotesData.Add(skinsEmotesItem);
                    AutoSuggestBoxItemSourceAdd(skinsEmotesItem, "SkinsEmotes");
                }
                foreach (var skinsOutfitSetsItem in skins.OutfitSets.Skin)
                {
                    skinsOutfitSetsItem.Picture = StringProcess.GetGameResourcePath(skinsOutfitSetsItem.Picture);
                    SkinsOutfitSetsData.Add(skinsOutfitSetsItem);
                    AutoSuggestBoxItemSourceAdd(skinsOutfitSetsItem, "SkinsOutfitSets");
                }
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
            else if (type == typeof(NatureBiomes))
            {
                suggestBoxItem.Picture = ((NatureBiomes)obj).Picture;
                suggestBoxItem.Name = ((NatureBiomes)obj).Name;
                suggestBoxItem.EnName = ((NatureBiomes)obj).EnName;
                suggestBoxItem.Category = "自然";
            }
            else if (type == typeof(NatureSmallPlant))
            {
                suggestBoxItem.Picture = ((NatureSmallPlant)obj).Picture;
                suggestBoxItem.Name = ((NatureSmallPlant)obj).Name;
                suggestBoxItem.EnName = ((NatureSmallPlant)obj).EnName;
                suggestBoxItem.Category = "自然";
            }
            else if (type == typeof(NatureTree))
            {
                suggestBoxItem.Picture = ((NatureTree)obj).Picture;
                suggestBoxItem.Name = ((NatureTree)obj).Name;
                suggestBoxItem.EnName = ((NatureTree)obj).EnName;
                suggestBoxItem.Category = "自然";
            }
            else if (type == typeof(NatureCreatureNest))
            {
                suggestBoxItem.Picture = ((NatureCreatureNest)obj).Picture;
                suggestBoxItem.Name = ((NatureCreatureNest)obj).Name;
                suggestBoxItem.EnName = ((NatureCreatureNest)obj).EnName;
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
            else if (type == typeof(Skin))
            {
                suggestBoxItem.Picture = ((Skin)obj).Picture;
                suggestBoxItem.Name = ((Skin)obj).Name;
                suggestBoxItem.EnName = ((Skin)obj).EnName;
                suggestBoxItem.Category = "皮肤";
            }
            suggestBoxItem.SourcePath = sourcePath;
            AutoSuggestBoxItemSource.Add(suggestBoxItem);
        }

        #endregion
    }
}
