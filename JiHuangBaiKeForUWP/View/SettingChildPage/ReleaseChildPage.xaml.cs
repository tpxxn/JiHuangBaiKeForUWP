using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Newtonsoft.Json;

namespace JiHuangBaiKeForUWP.View.SettingChildPage
{
    public sealed partial class ReleaseChildPage : Page
    {
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
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
            await Deserialize();
        }

        public ReleaseChildPage()
        {
            this.InitializeComponent();
        }

        public async Task Deserialize()
        {
            var releaseData = new Collection<Release>();
            var uri = new Uri("ms-appx:///Json/Release.json");
            var storageFile = await StorageFile.GetFileFromApplicationUriAsync(uri);
            var str = await FileIO.ReadTextAsync(storageFile);
            var release = JsonConvert.DeserializeObject<ReleaseRootObject>(str);
            foreach (var releaseItems in release.Release)
            {
                releaseData.Add(releaseItems);
            }
            for (var i = 0; i < releaseData.Count; i++)
            {
                //添加版本号和发布时间
                var rootGrid = new Grid();
                rootGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                rootGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                rootGrid.Margin = i == 0 ? new Thickness(0) : new Thickness(0, 50, 0, 0);
                rootGrid.Children.Add(new Border
                {
                    Width = 65,
                    Background = new SolidColorBrush(Color.FromArgb(255, 111, 66, 193)),
                    CornerRadius = new CornerRadius(5),
                    Child = new TextBlock
                    {
                        FontSize = 23,
                        Text = releaseData[i].Version,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                });
                var dataTextBlock = new TextBlock
                {
                    FontSize = 23,
                    Text = releaseData[i].Data,
                    Margin = new Thickness(15, 0, 0, 0)
                };
                rootGrid.Children.Add(dataTextBlock);
                Grid.SetColumn(dataTextBlock, 1);
                ReleaseStackPanel.Children.Add(rootGrid);
                //添加更新内容
                foreach (var updataContent in releaseData[i].UpdataContent)
                {
                    var updateContentRootGrid = new Grid { Margin = new Thickness(75, 15, 0, 0) };
                    updateContentRootGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    updateContentRootGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    var solidColorBrush = updataContent.Label == "新增" ? new SolidColorBrush(Color.FromArgb(255, 40, 167, 69)) : new SolidColorBrush(Color.FromArgb(255, 3, 102, 214));
                    updateContentRootGrid.Children.Add(new Border
                    {
                        Width = 55,
                        Background = solidColorBrush,
                        CornerRadius = new CornerRadius(5),
                        Child = new TextBlock
                        {
                            FontSize = 17,
                            Text = updataContent.Label,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        }
                    });
                    var updataContentDataTextBlock = new TextBlock
                    {
                        FontSize = 17,
                        Text = updataContent.Content,
                        Margin = new Thickness(15, 0, 0, 0)
                    };
                    updateContentRootGrid.Children.Add(updataContentDataTextBlock);
                    Grid.SetColumn(updataContentDataTextBlock, 1);
                    ReleaseStackPanel.Children.Add(updateContentRootGrid);
                }
            }
        }
    }
}
