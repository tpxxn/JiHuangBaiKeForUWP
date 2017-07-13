using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace JiHuangBaiKeForUWP.Manager
{
    internal class SettingSet
    {
        #region 主题设置New region

        private const string SettingTheme = "Theme";

        public  static void ThemeSettingSet(bool isOn)
        {
            var rootContainer = ApplicationData.Current.LocalSettings;
            rootContainer.Values[SettingTheme] = isOn;
        }

        public static bool ThemeSettingRead()
        {
            var rootContainer = ApplicationData.Current.LocalSettings;

            if (rootContainer.Values.TryGetValue(SettingTheme, out object themeIsOn))
            {
                return (bool)themeIsOn;

            }
            else
            {
                return true;
            }
        }

        #endregion

        #region 游戏版本设置

        private const string SettingGameVersion = "GameVersionSelectedIndex";

        public static void GameVersionSettingSet(int gameVersion)
        {
            var rootContainer = ApplicationData.Current.LocalSettings;
            rootContainer.Values[SettingGameVersion] = gameVersion;
        }

        public static int GameVersionSettingRead()
        {
            var rootContainer = ApplicationData.Current.LocalSettings;

            if (rootContainer.Values.TryGetValue(SettingGameVersion, out object gameVersion))
            {
                return (int)gameVersion;

            }
            else
            {
                return 3;
            }
        }

        #endregion
    }
}
