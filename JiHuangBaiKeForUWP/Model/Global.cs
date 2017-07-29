using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
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

        #region "颜色常量"
        public static SolidColorBrush ColorGreen = new SolidColorBrush(Color.FromArgb(255, 94, 182, 96));     //绿色
        public static SolidColorBrush ColorKhaki = new SolidColorBrush(Color.FromArgb(255, 237, 182, 96));    //卡其布色/土黄色
        public static SolidColorBrush ColorRed = new SolidColorBrush(Color.FromArgb(255, 216, 82, 79));       //红色
        public static SolidColorBrush ColorBlue = new SolidColorBrush(Color.FromArgb(255, 51, 122, 184));     //蓝色
        public static SolidColorBrush ColorPurple = new SolidColorBrush(Color.FromArgb(255, 162, 133, 240));   //紫色
        public static SolidColorBrush ColorPink = new SolidColorBrush(Color.FromArgb(255, 240, 133, 211));     //粉色
        public static SolidColorBrush ColorCyan = new SolidColorBrush(Color.FromArgb(255, 21, 227, 234));     //青色
        public static SolidColorBrush ColorOrange = new SolidColorBrush(Color.FromArgb(255, 246, 166, 11));     //橙色
        public static SolidColorBrush ColorYellow = new SolidColorBrush(Color.FromArgb(255, 238, 232, 21));     //黄色
        public static SolidColorBrush ColorBorderCyan = new SolidColorBrush(Color.FromArgb(255, 178, 236, 237));     //紫色
        #endregion

        #region 游戏版本

        /// <summary>
        /// 游戏版本
        /// </summary>
        public static int GameVersion { get; set; }

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

        /// <summary>
        /// 删除重复数据
        /// </summary>
        /// <param name="str">字符串数组</param>
        /// <returns></returns>
        public static string[] StringDelRepeatData(string[] str)
        {
            var b = str.GroupBy(p => p).Select(p => p.Key).ToArray();
            if (b.Length != 1) return b;
            var temp = new List<string>
            {
                b[0],
                ""
            };
            b = temp.ToArray();
            return b;
        }
    }
}
