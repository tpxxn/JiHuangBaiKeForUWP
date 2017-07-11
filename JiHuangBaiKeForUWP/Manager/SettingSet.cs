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
    }
}
