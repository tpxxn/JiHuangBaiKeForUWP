using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using JiHuangBaiKeForUWP.Model;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace JiHuangBaiKeForUWP.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class FoodPage : Page
    {
        private readonly ObservableCollection<Character> _foodRecipeData = new ObservableCollection<Character>();
        private readonly ObservableCollection<Character> _foodMeatData = new ObservableCollection<Character>();
        private readonly ObservableCollection<Character> _foodVegetableData = new ObservableCollection<Character>();
        private readonly ObservableCollection<Character> _foodFruitData = new ObservableCollection<Character>();
        private readonly ObservableCollection<Character> _foodEggData = new ObservableCollection<Character>();
        private readonly ObservableCollection<Character> _foodOtherData = new ObservableCollection<Character>();
        private readonly ObservableCollection<Character> _foodNoFcData = new ObservableCollection<Character>();
        public FoodPage()
        {
            this.InitializeComponent();
        }

        private void FoodRecipeGridView_ItemClick(object sender, ItemClickEventArgs e)
        {

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
