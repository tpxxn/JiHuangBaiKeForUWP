using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace JiHuangBaiKeForUWP.Model
{
    internal sealed class SettingSet
    {
        #region 主题(黑/白)设置
        /// <summary>
        /// 主题常量
        /// </summary>
        private const string SettingTheme = "Theme";

        /// <summary>
        /// 设置主题
        /// </summary>
        /// <param name="isOn">主题选项</param>
        public  static void ThemeSettingSet(bool isOn)
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

        #region 游戏版本设置
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
    }
}
