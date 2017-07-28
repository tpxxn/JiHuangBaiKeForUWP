using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using JiHuangBaiKeForUWP.View;
using Microsoft.Toolkit.Uwp.UI.Animations;

namespace JiHuangBaiKeForUWP.Model
{

    public static class Global
    {
        /// <summary>
        /// 应用程序文件夹
        /// </summary>
        public static readonly StorageFolder ApplicationFolder = ApplicationData.Current.LocalFolder;

        #region 游戏版本

        /// <summary>
        /// 游戏版本
        /// </summary>
        public static int GameVersion { get; set; }

        /// <summary>
        /// 游戏版本已改变
        /// </summary>
        public static bool GameVersionChanged { get; set; }

        /// <summary>
        /// 内置游戏版本
        /// </summary>
        public static string[] BuiltInGameVersion =
        {
            "DS(饥荒单机)", "RoG(巨兽统治)", "SW(失落之船)", "DST(饥荒联机)", "TGP及QQGame",
        };

        /// <summary>
        /// 内置游戏版本Json文件夹名
        /// </summary>
        public static string[] BuiltInGameVersionJsonFolder =
        {
            "DS", "ROG", "SW", "DST", "Tencent",
        };

        /// <summary>
        /// 版本数据集合
        /// </summary>
        public static ObservableCollection<string> VersionData = new ObservableCollection<string>();

        #endregion

        /// <summary>
        /// 透明Style
        /// </summary>
        public static readonly Style Transparent = (Style)Application.Current.Resources["TransparentDialog"];

        /// <summary>
        /// 显示对话框
        /// </summary>
        /// <param name="contentDialog">ContentDialog</param>
        /// <param name="stackPanlel">StackPanel</param>
        public static async void ShowDialog(ContentDialog contentDialog, StackPanel stackPanlel)
        {

            contentDialog.Closed += async delegate
            {
                await stackPanlel.Blur(0, 0).StartAsync();
                contentDialog.Hide();
            };

            contentDialog.PrimaryButtonClick += async delegate
            {
                await stackPanlel.Blur(0, 0).StartAsync();
                contentDialog.Hide();
            };
            await stackPanlel.Blur(7, 100).StartAsync();

            await contentDialog.ShowAsync();
        }
    }
}
