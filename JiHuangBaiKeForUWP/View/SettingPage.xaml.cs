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
using JiHuangBaiKeForUWP.Manager;
using JiHuangBaiKeForUWP.Model;
using JiHuangBaiKeForUWP.UserControls.SettingPage;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;

namespace JiHuangBaiKeForUWP.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        #region 字段

        private readonly int _gameVersionSelectIndex;
        private readonly ObservableCollection<string> _versionData = new ObservableCollection<string>();

        #endregion

        #region 构造器

        public SettingPage()
        {
            GameVersionDeserialize();
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
        /// 反序列化游戏版本
        /// </summary>
        public void GameVersionDeserialize()
        {
            foreach (var gameVersion in Global.VersionData)
            {
                _versionData.Add(gameVersion);
            }
        }
        
        /// <summary>
        /// 序列化游戏版本
        /// </summary>
        public async void GameVersionSerialize()
        {
            const string filaName = "GameVersion";
            var storageFile = await Global.ApplicationFolder.CreateFileAsync("temp", CreationCollisionOption.ReplaceExisting);
            var version = new VersionJson.RootObject();
            version.GameVersion.AddRange(_versionData);
            var str = JsonConvert.SerializeObject(version);
            await FileIO.WriteTextAsync(storageFile, str);
            await storageFile.MoveAsync(Global.ApplicationFolder, filaName, NameCollisionOption.ReplaceExisting);
        }

        /// <summary>
        /// 设置游戏版本
        /// </summary>
        private void GameVersionComboBox_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (GameVersionComboBox.Items != null && GameVersionComboBox.Items.Count >= 5)
            {
                GameVersionComboBox.SelectedIndex = _gameVersionSelectIndex < _versionData.Count
                    ? _gameVersionSelectIndex
                    : 3;
            }
        }

        /// <summary>
        /// 游戏版本改变时自动保存
        /// </summary>
        private void GameVersionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SettingSet.GameVersionSettingSet(GameVersionComboBox.SelectedIndex);
            Global.GameVersion = GameVersionComboBox.SelectedIndex;
        }

        /// <summary>
        /// 复制游戏配置文件
        /// </summary>
        private async void GameVersionCopyButton_Tapped(object sender, TappedRoutedEventArgs e)
        {

            var gameVersionString = await DisplayGameVersionCopyDialog();
            var fileNameSameFlag = false;
            if (gameVersionString != null)
            {
                if (gameVersionString == "")
                {
                    CopyErrorDialog("Null");
                }
                else
                {
                    foreach (var versionData in _versionData)
                    {
                        if (gameVersionString == versionData)
                        {
                            fileNameSameFlag = true;
                            CopyErrorDialog("FileNameSame");
                        }
                    }
                    if (fileNameSameFlag == false) //判断配置文件名没问题
                    {
                        _versionData.Add(gameVersionString);
                        GameVersionComboBox.SelectedIndex = _versionData.Count - 1;
                        GameVersionSerialize();
                        //TODO 复制配置文件
                    }
                }
            }
        }

        /// <summary>
        /// 显示游戏版本复制对话框
        /// </summary>
        /// <returns>文本框字符串</returns>
        private static async Task<string> DisplayGameVersionCopyDialog()
        {
            string getText;
            var gameVersionAddDialog = new ContentDialog()
            {
                Title = "复制配置文件",
                Content = new GameVersionAddUserControl(),
                PrimaryButtonText = "确定",
                SecondaryButtonText = "取消"
            };
            var result = await gameVersionAddDialog.ShowAsync();
            getText = result == ContentDialogResult.Primary
                ? ((GameVersionAddUserControl)gameVersionAddDialog.Content).GetText()
                : null;
            return getText;
        }

        /// <summary>
        /// 复制错误对话框
        /// </summary>
        /// <param name="errorType">错误类型</param>
        private static async void CopyErrorDialog(string errorType)
        {
            string errorTypeContent;
            switch (errorType)
            {
                case "Null":
                    errorTypeContent = "配置文件名不能为空";
                    break;
                case "FileNameSame":
                    errorTypeContent = "与已存在的配置文件名相同";
                    break;
                default:
                    errorTypeContent = "未知错误";
                    break;
            }
            var errorDialog = new ContentDialog()
            {
                Title = "错误",
                Content = errorTypeContent,
                PrimaryButtonText = "确定",
            };
            await errorDialog.ShowAsync();
        }

        /// <summary>
        /// 删除游戏配置文件
        /// </summary>
        private async void GameVersionDeleteButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (GameVersionComboBox.SelectedIndex <= 4)
            {
                DisplayDeleteErrorDialog();
            }
            else
            {
                var dialog = new ContentDialog()
                {
                    Title = "删除配置文件",
                    Content = "确认删除" + GameVersionComboBox.SelectedItem +"配置文件？",
                    PrimaryButtonText = "确定",
                    SecondaryButtonText = "取消",
                    FullSizeDesired = false,
                };
                dialog.PrimaryButtonClick += (_s, _e) => { };
                var result = await dialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    _versionData.RemoveAt(GameVersionComboBox.SelectedIndex);
                    GameVersionComboBox.SelectedIndex = 3;
                    const string name = "GameVersion";
                    var folder = ApplicationData.Current.LocalFolder;
                    var file = await folder.CreateFileAsync("temp", CreationCollisionOption.ReplaceExisting);
                    var version = new VersionJson.RootObject();
                    version.GameVersion.AddRange(_versionData);
                    var str = JsonConvert.SerializeObject(version);
                    await FileIO.WriteTextAsync(file, str);
                    await file.MoveAsync(folder, name, NameCollisionOption.ReplaceExisting);
                    //TODO 删除配置文件
                }
            }
        }

        /// <summary>
        /// 删除游戏配置文件错误对话框
        /// </summary>
        private static async void DisplayDeleteErrorDialog()
        {
            var deleteErrorDialog = new ContentDialog()
            {
                Title = "删除错误",
                Content = "自带的配置文件无法删除！",
                PrimaryButtonText = "确定"
            };
            await deleteErrorDialog.ShowAsync();
        }

        #endregion

    }
}
