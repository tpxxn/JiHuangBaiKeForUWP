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
using JiHuangBaiKeForUWP.DateType;
using JiHuangBaiKeForUWP.Model;
using Newtonsoft.Json;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace JiHuangBaiKeForUWP.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class CharacterPage : Page
    {
        private readonly ObservableCollection<Character> _characterData = new ObservableCollection<Character>();
//        private readonly Style _transparent = (Style)Application.Current.Resources["TransparentDialog"];

        public CharacterPage()
        {
            this.InitializeComponent();
            Deserialize();
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
                    uri = new Uri("ms-appx:///Xml/" + Global.BuiltInGameVersionXmlFolder[Global.GameVersion] + "/" +
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
            var character = JsonConvert.DeserializeObject<CharactersJson.RootObject>(str);
            foreach (var characterItems in character.Characters)
            {
                _characterData.Add(characterItems);
            }
        }

        private async void CharacterGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
//            var c = e.ClickedItem as CharactersJson.Character;
//
//            var contentDialog = new ContentDialog()
//            {
//                Content = new CharacterDialog(c),
//                PrimaryButtonText = "确定",
//                FullSizeDesired = false
//            };
//
//            contentDialog.Style = _transparent;
//
//            contentDialog.Closed += async (_s, _e) =>
//            {
//                await CharacterGrid.Blur(value: 0, duration: 0, delay: 0).StartAsync();
//                contentDialog.Hide();
//            };
//
//            contentDialog.PrimaryButtonClick += async (_s, _e) =>
//            {
//                await CharacterGrid.Blur(value: 0, duration: 0, delay: 0).StartAsync();
//                contentDialog.Hide();
//            };
//            await CharacterGrid.Blur(value: 7, duration: 100, delay: 0).StartAsync();
//
//            await contentDialog.ShowAsync();
        }
    }
}
