using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
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
            this.InitializeComponent();
            ThemeToggleSwitch.IsOn = SettingSet.ThemeSettingRead();
            _gameVersionSelectIndex = SettingSet.GameVersionSettingRead();
            GameVersionDeserialize();
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
        public async void GameVersionDeserialize()
        {
            var xmlUri = new Uri("ms-appx:///Xml/GameVersion.xml");
            var xmlFile = await StorageFile.GetFileFromApplicationUriAsync(xmlUri);
            var serializer = new XmlSerializer(typeof(Model.Version));
            var accessStream = await xmlFile.OpenReadAsync();
            using (var stream = accessStream.AsStreamForRead((int)accessStream.Size))
            {
                var xml = (Model.Version)serializer.Deserialize(stream);
                foreach (var str in xml.GameVersion)
                {
                    _versionData.Add(str);
                }
            }
        }

        /// <summary>
        /// 序列化游戏版本
        /// </summary>
        public async void GameVersionSerialize()
        {
            var xmlUri = new Uri("ms-appx:///Xml/GameVersion.xml");
            var xmlFile = await StorageFile.GetFileFromApplicationUriAsync(xmlUri);
            var serializer = new XmlSerializer(typeof(Model.Version));
            var accessStream = await xmlFile.OpenReadAsync();
            using (var stream = accessStream.AsStreamForWrite((int)accessStream.Size))
            {
                var version = new Model.Version();
                //TODO System.NullReferenceException错误 原因未知
                version.GameVersion.AddRange(_versionData);
                serializer.Serialize(stream, version);
            }
        }

        /// <summary>
        /// 设置游戏版本
        /// </summary>
        private void GameVersionComboBox_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            GameVersionComboBox.SelectedIndex = _gameVersionSelectIndex < _versionData.Count
                ? _gameVersionSelectIndex
                : 3;
        }

        /// <summary>
        /// 游戏版本改变时自动保存
        /// </summary>
        private void GameVersionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SettingSet.GameVersionSettingSet(GameVersionComboBox.SelectedIndex);
        }

        /// <summary>
        /// 添加游戏配置文件
        /// </summary>
        private async void GameVersionAddButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var gameVersionString = await DisplayGameVersionAddDialog();
            var fileNameSameFlag = false;
            if (gameVersionString != null)
            {
                if (gameVersionString == "")
                {
                    AddErrorDialog("Null");
                }
                else
                {
                    foreach (var versionData in _versionData)
                    {
                        if (gameVersionString == versionData)
                        {
                            fileNameSameFlag = true;
                            AddErrorDialog("FileNameSame");
                        }
                    }
                    if (fileNameSameFlag == false)//判断配置文件名没问题
                    {
                        _versionData.Add(gameVersionString);
                        //TODO 该函数含有错误
                        GameVersionSerialize();
                    }
                }
            }
        }

        /// <summary>
        /// 显示游戏版本添加对话框
        /// </summary>
        /// <returns>文本框字符串</returns>
        private static async Task<string> DisplayGameVersionAddDialog()
        {
            var getText = "";
            var gameVersionAddDialog = new ContentDialog()
            {
                Title = "添加配置文件",
                Content = new GameVersionAddUserControl(),
                PrimaryButtonText = "确定",
                SecondaryButtonText = "取消"
            };
            var result = await gameVersionAddDialog.ShowAsync();
            getText = result == ContentDialogResult.Primary
                ? ((GameVersionAddUserControl) gameVersionAddDialog.Content).GetText()
                : null;
            return getText;
        }

        /// <summary>
        /// 添加错误对话框
        /// </summary>
        /// <param name="errorType">错误类型</param>
        private static async void AddErrorDialog(string errorType)
        {
            var errorTypeContent = "";
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
        /// 复制游戏配置文件
        /// </summary>
        private void GameVersionCopyButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //TODO 复制配置文件
        }

        /// <summary>
        /// 删除游戏配置文件
        /// </summary>
        private void GameVersionDeleteButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (GameVersionComboBox.SelectedIndex <= 4)
            {
                DisplayDeleteErrorDialog();
            }
            else
            {
                _versionData.RemoveAt(GameVersionComboBox.SelectedIndex);
                GameVersionComboBox.SelectedIndex = 3;
                //TODO 删除配置文件
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
