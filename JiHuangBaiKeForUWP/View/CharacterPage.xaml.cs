using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
using JiHuangBaiKeForUWP.View.Dialog;

namespace JiHuangBaiKeForUWP.View
{
    /// <summary>
    /// Character页面
    /// </summary>
    public sealed partial class CharacterPage : Page
    {
        private readonly ObservableCollection<Character> _characterData = new ObservableCollection<Character>();

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter = (string[])e.Parameter;
            await Deserialize();
            if (parameter == null) return;
            var _e = parameter[1];
            if (CharacterGridView.Items == null) return;
            foreach (var gridViewItem in CharacterGridView.Items)
            {
                var character = gridViewItem as Character;
                if (character == null || character.Picture != _e) continue;
                var contentDialog = new ContentDialog
                {
                    Content = new CharacterDialog(character),
                    PrimaryButtonText = "确定",
                    FullSizeDesired = false,
                    Style = Global.Transparent
                };
                Global.ShowDialog(contentDialog, CharacterStackPanel);
                break;
            }
        }

        public CharacterPage()
        {
            this.InitializeComponent();
        }

        public async Task<bool> Deserialize()
        {
            _characterData.Clear();
            var character = JsonConvert.DeserializeObject<CharacterRootObject>(await Global.GetJsonString("Characters.json"));
            foreach (var characterItems in character.Character)
            {
                _characterData.Add(characterItems);
            }
            foreach (var characterItems in _characterData)
            {
                characterItems.Picture = Global.GetGameResourcePath(characterItems.Picture);
            }
            return false;
        }

        private void CharacterGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var c = e.ClickedItem as Character;
            var contentDialog = new ContentDialog
            {
                Content = new CharacterDialog(c),
                PrimaryButtonText = "确定",
                FullSizeDesired = false,
                Style = Global.Transparent
            };
            Global.ShowDialog(contentDialog, CharacterStackPanel);
        }
    }
}
