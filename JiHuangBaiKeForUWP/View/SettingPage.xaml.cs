using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml.Dom;
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
using JiHuangBaiKeForUWP.Manager;
using JiHuangBaiKeForUWP.Model;

namespace JiHuangBaiKeForUWP.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        private readonly ObservableCollection<GameVersion> _versionData = new ObservableCollection<GameVersion>();
        private readonly int _gameVersionSelectIndex;
        public SettingPage()
        {
            this.InitializeComponent();
            ThemeToggleSwitch.IsOn = SettingSet.ThemeSettingRead();
            _gameVersionSelectIndex = SettingSet.GameVersionSettingRead();
            Deserialize();
        }
        public async void Deserialize()
        {
            var xmlUri = new Uri("ms-appx:///Xml/GameVersion.xml");
            var xmlFile = await StorageFile.GetFileFromApplicationUriAsync(xmlUri);
            var xml = await XmlDocument.LoadFromFileAsync(xmlFile);
            var versionList = xml.DocumentElement.SelectNodes("GameVersion");
            foreach (var item in versionList)
            {
                var childList = item.InnerText;
                _versionData.Add(
                    new GameVersion
                    {
                        Version = childList
                    });
            }
        }

        private void ThemeToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            SettingSet.ThemeSettingSet(ThemeToggleSwitch.IsOn);

            ((Frame) Window.Current.Content).RequestedTheme =
                ThemeToggleSwitch.IsOn ? ElementTheme.Dark : ElementTheme.Light;
        }

        private void GameVersionComboBox_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            GameVersionComboBox.SelectedIndex = _gameVersionSelectIndex;
        }

        private void GameVersionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SettingSet.GameVersionSettingSet(GameVersionComboBox.SelectedIndex);
        }
    }
}
