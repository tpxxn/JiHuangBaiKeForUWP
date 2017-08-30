using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
using JiHuangBaiKeForUWP.View.Dialog;
using Newtonsoft.Json;

namespace JiHuangBaiKeForUWP.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class NaturalPage : Page
    {
        private readonly ObservableCollection<Nature> _naturalBiomesData = new ObservableCollection<Nature>();

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter = (string[])e.Parameter;
            await Deserialize();
            if (parameter == null) return;
            var _e = parameter[1];
            switch (parameter[0])
            {
                case "NaturalBiomes":
                    BiomesExpander.IsExPanded = true;
                    OnNavigatedToNaturalBiomesDialog(NaturalBiomesGridView, _naturalBiomesData, _e);
                    break;
            }
        }

        private void OnNavigatedToNaturalBiomesDialog(GridView gridView, ObservableCollection<Nature> naturalCollection, string _e)
        {
            if (gridView.Items == null) return;
            foreach (var gridViewItem in naturalCollection)
            {
                var natural = gridViewItem;
                if (natural == null || natural.Picture != _e) continue;
                var contentDialog = new ContentDialog
                {
                    Content = new NaturalDialog(natural),
                    PrimaryButtonText = "确定",
                    FullSizeDesired = false,
                    Style = Global.Transparent
                };
                Global.ShowDialog(contentDialog);
                break;
            }
        }

        public NaturalPage()
        {
            this.InitializeComponent();
        }

        public async Task Deserialize()
        {
            _naturalBiomesData.Clear();
            var natural = JsonConvert.DeserializeObject<NaturalRootObject>(await StringProcess.GetJsonString("Natural.json"));
            foreach (var natureBiomesItems in natural.Biomes.Nature)
            {
                _naturalBiomesData.Add(natureBiomesItems);
            }
            foreach (var natureBiomesItems in _naturalBiomesData)
            {
                natureBiomesItems.Picture = StringProcess.GetGameResourcePath(natureBiomesItems.Picture);
            }
        }

        private void NaturalGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var c = e.ClickedItem as Nature;
            var contentDialog = new ContentDialog
            {
                Content = new NaturalDialog(c),
                PrimaryButtonText = "确定",
                FullSizeDesired = false,
                Style = Global.Transparent
            };
            Global.ShowDialog(contentDialog);
        }
    }
}
