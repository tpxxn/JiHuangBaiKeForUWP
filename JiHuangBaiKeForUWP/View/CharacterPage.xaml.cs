using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
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
using Windows.UI.Xaml.Media.Animation;
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
            Global.FrameTitle.Text = "人物";
            if (e.NavigationMode == NavigationMode.Back)
            {
                EntranceTransition.FromVerticalOffset = 0;
            }
            if (Global.GetOsVersion() >= 16299)
            {
                var dimGrayAcrylicBrush = new AcrylicBrush
                {
                    BackgroundSource = AcrylicBackgroundSource.HostBackdrop,
                    FallbackColor = Colors.Transparent,
                    TintColor = Global.TinkColor,
                    TintOpacity = Global.TinkOpacity
                };
                CharacterStackPanel.Background = dimGrayAcrylicBrush;
            }
            var parameter = (List<string>)e.Parameter;
            await Deserialize();
            if (parameter == null || parameter.Count == 0) return;
            var _e = parameter[1];
            if (CharacterGridView.Items == null) return;
            foreach (var gridViewItem in _characterData)
            {
                var character = gridViewItem;
                if (character == null || character.Picture != _e) continue;
                Frame.Navigate(typeof(CharacterDialog), character);
                break;
            }
        }

        public CharacterPage()
        {
            this.InitializeComponent();
        }

        public async Task Deserialize()
        {
            _characterData.Clear();
            var character = JsonConvert.DeserializeObject<CharacterRootObject>(await StringProcess.GetJsonString("Characters.json"));
            foreach (var characterItems in character.Character)
            {
                _characterData.Add(characterItems);
            }
            foreach (var characterItems in _characterData)
            {
                characterItems.Picture = StringProcess.GetGameResourcePath(characterItems.Picture);
            }
        }

        private void CharacterGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (((GridView)sender).ContainerFromItem(e.ClickedItem) is GridViewItem container)
            {
                var root = (FrameworkElement)container.ContentTemplateRoot;
                var image = (UIElement)root.FindName("Image");
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Image", image);
            }
            var item = (Character)e.ClickedItem;
            Frame.Navigate(typeof(CharacterDialog), item);
            Global.PageStack.Push(new PageStackItem { TypeName = typeof(CharacterDialog), Object = item });
        }
    }
}
