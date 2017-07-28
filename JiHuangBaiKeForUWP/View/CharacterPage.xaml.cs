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
using Microsoft.Toolkit.Uwp.UI.Animations;
using Newtonsoft.Json;

namespace JiHuangBaiKeForUWP.View
{
    /// <summary>
    /// Character页面
    /// </summary>
    public sealed partial class CharacterPage : Page
    {
        private readonly ObservableCollection<Character> _characterData = new ObservableCollection<Character>();

        public CharacterPage()
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
            const string fileName = "Characters.json";
            if (Global.GameVersion < 5) //内置配置文件
            {
                var folderExists = await Global.ApplicationFolder.TryGetItemAsync(Global.BuiltInGameVersion[Global.GameVersion]);
                if (folderExists == null)
                {
                    uri = new Uri("ms-appx:///Json/" + Global.BuiltInGameVersionXmlFolder[Global.GameVersion] + "/" +
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
            var character = JsonConvert.DeserializeObject<CharacterRootObject>(str);
            foreach (var characterItems in character.Character)
            {
                _characterData.Add(characterItems);
            }
            foreach (var characterItems in _characterData)
            {
                characterItems.Picture = $"ms-appx:///Assets/GameResources/Charcters/{characterItems.Picture}.png";
            }
        }

        private async void CharacterGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var c = e.ClickedItem as Character;

            var contentDialog = new ContentDialog
            {
                Content = new CharacterDialog(c),
                PrimaryButtonText = "确定",
                FullSizeDesired = false,
                Style = Global.Transparent
            };

            contentDialog.Closed += async delegate
            {
                await CharacterStackPanel.Blur(0, 0).StartAsync();
                contentDialog.Hide();
            };

            contentDialog.PrimaryButtonClick += async delegate
            {
                await CharacterStackPanel.Blur(0, 0).StartAsync();
                contentDialog.Hide();
            };
            await CharacterStackPanel.Blur(7, 100).StartAsync();

            await contentDialog.ShowAsync();
        }
    }
}
