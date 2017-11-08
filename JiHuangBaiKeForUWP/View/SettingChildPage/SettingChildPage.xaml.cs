using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
using JiHuangBaiKeForUWP.UserControls.SettingPage;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace JiHuangBaiKeForUWP.View.SettingChildPage
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SettingChildPage : Page
    {
        #region 字段

        private readonly int _gameVersionSelectIndex;

        #endregion

        #region 构造器

        public SettingChildPage()
        {
            _gameVersionSelectIndex = Global.GameVersion;
            this.InitializeComponent();
            ThemeToggleSwitch.IsOn = SettingSet.ThemeSettingRead();
        }

        #endregion

        #region 主题

        private void ThemeToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            SettingSet.ThemeSettingSet(ThemeToggleSwitch.IsOn);
            ((Frame)Window.Current.Content).RequestedTheme =
                ThemeToggleSwitch.IsOn ? ElementTheme.Dark : ElementTheme.Light;
        }

        #endregion

        #region 游戏版本

        /// <summary>
        /// 设置游戏版本
        /// </summary>
        private void GameVersionComboBox_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (GameVersionComboBox.Items != null && GameVersionComboBox.Items.Count >= 5)
            {
                GameVersionComboBox.SelectedIndex = _gameVersionSelectIndex;
            }
        }

        /// <summary>
        /// 游戏版本改变时自动保存
        /// </summary>
        private async void GameVersionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SettingSet.GameVersionSettingSet(GameVersionComboBox.SelectedIndex);
            Global.GameVersion = GameVersionComboBox.SelectedIndex;
            await Global.SetAutoSuggestBoxItem();
        }

        #endregion
    }
}
