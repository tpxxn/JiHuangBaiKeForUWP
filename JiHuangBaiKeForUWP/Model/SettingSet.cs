using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace JiHuangBaiKeForUWP.Model
{
    internal sealed class SettingSet
    {
        #region 游戏版本
        /// <summary>
        /// 游戏版本序号常量
        /// </summary>
        private const string SettingGameVersion = "GameVersionSelectedIndex";

        /// <summary>
        /// 设置游戏版本
        /// </summary>
        /// <param name="gameVersion">游戏版本序号</param>
        public static void GameVersionSettingSet(int gameVersion)
        {
            var rootContainer = ApplicationData.Current.LocalSettings;
            rootContainer.Values[SettingGameVersion] = gameVersion;
        }

        /// <summary>
        /// 读取游戏版本
        /// </summary>
        /// <returns>游戏版本序号</returns>
        public static int GameVersionSettingRead()
        {
            var rootContainer = ApplicationData.Current.LocalSettings;
            if (rootContainer.Values.TryGetValue(SettingGameVersion, out object gameVersion))
            {
                return (int)gameVersion;
            }
            return 0;
        }

        #endregion

        #region 主题(黑/白)
        /// <summary>
        /// 主题常量
        /// </summary>
        private const string SettingTheme = "Theme";

        /// <summary>
        /// 设置主题
        /// </summary>
        /// <param name="isOn">主题选项</param>
        public static void ThemeSettingSet(bool isOn)
        {
            var rootContainer = ApplicationData.Current.LocalSettings;
            rootContainer.Values[SettingTheme] = isOn;
        }

        /// <summary>
        /// 读取主题
        /// </summary>
        /// <returns>主题选项</returns>
        public static bool ThemeSettingRead()
        {
            var rootContainer = ApplicationData.Current.LocalSettings;
            if (rootContainer.Values.TryGetValue(SettingTheme, out object themeIsOn))
            {
                return (bool)themeIsOn;
            }
            return false;
        }

        #endregion

        #region 使用亚克力背景

        #region 亚克力背景透明度
        /// <summary>
        /// 亚克力背景透明度常量
        /// </summary>
        private const string SettingAcrylicOpacity = "AcrylicOpacity";

        /// <summary>
        /// 设置亚克力背景透明度
        /// </summary>
        /// <param name="opacity">亚克力背景透明度选项</param>
        public static void AcrylicOpacitySettingSet(double opacity)
        {
            var rootContainer = ApplicationData.Current.LocalSettings;
            rootContainer.Values[SettingAcrylicOpacity] = opacity;
        }

        /// <summary>
        /// 读取亚克力背景透明度
        /// </summary>
        /// <returns>亚克力背景透明度选项</returns>
        public static double AcrylicOpacitySettingRead()
        {
            var rootContainer = ApplicationData.Current.LocalSettings;
            if (rootContainer.Values.TryGetValue(SettingAcrylicOpacity, out object acrylicOpacityIsOn))
            {
                return (double)acrylicOpacityIsOn;
            }
            return 0.3;
        }

        #endregion

        #region 亚克力背景颜色
        /// <summary>
        /// 亚克力背景颜色常量
        /// </summary>
        private const string SettingAcrylicColor = "AcrylicColor";

        /// <summary>
        /// 设置亚克力背景颜色
        /// </summary>
        /// <param name="color">亚克力背景颜色选项</param>
        public static void AcrylicColorSettingSet(string color)
        {
            var rootContainer = ApplicationData.Current.LocalSettings;
            rootContainer.Values[SettingAcrylicColor] = color;
        }

        /// <summary>
        /// 读取亚克力背景颜色
        /// </summary>
        /// <returns>亚克力背景颜色选项</returns>
        public static string AcrylicColorSettingRead()
        {
            var rootContainer = ApplicationData.Current.LocalSettings;
            if (rootContainer.Values.TryGetValue(SettingAcrylicColor, out object acrylicColorIsOn))
            {
                return (string)acrylicColorIsOn;
            }
            return "#FF696969";
        }

        #endregion

        #endregion

    }
}
