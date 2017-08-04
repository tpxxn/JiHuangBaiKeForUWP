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
using JiHuangBaiKeForUWP.View.Dialog;

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
            Deserialize();
        }

        public async void Deserialize()
        {
            var character = JsonConvert.DeserializeObject<CharacterRootObject>(await Global.GetJsonString("Characters.json"));
            foreach (var characterItems in character.Character)
            {
                _characterData.Add(characterItems);
            }
            foreach (var characterItems in _characterData)
            {
                characterItems.Picture = Global.GetGameResourcePath(characterItems.Picture);
            }
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
