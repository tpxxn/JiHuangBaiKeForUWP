using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace JiHuangBaiKeForUWP.Model
{
    public static class Fonts
    {
        /// <summary>
        /// 是否加载字体
        /// </summary>
        public static bool LoadFont;

//        /// <summary>
//        /// 获取字体函数
//        /// </summary>
//        /// <returns>返回</returns>
//        private static IEnumerable<string> Rf()
//        {
//            var installedFontCollectionFont = new InstalledFontCollection();
//            var fontFamilys = installedFontCollectionFont.Families;
//            return fontFamilys.Length < 1 ? null : fontFamilys.Select(item => item.Name).ToList();
//        }
//
//        /// <summary>
//        /// 修改字体
//        /// </summary>
//        private void Se_ComboBox_Font_SelectionChanged(Object sender, SelectionChangedEventArgs e)
//        {
//            if (!LoadFont) return;
//            var ls = (from TextBlock tb in SeComboBoxFont.Items select tb.Text).ToList();
//            mainWindow.FontFamily = new FontFamily(ls[SeComboBoxFont.SelectedIndex]);
//            RegeditRw.RegWrite("MainWindowFont", ls[SeComboBoxFont.SelectedIndex]);
//        }
    }
}
