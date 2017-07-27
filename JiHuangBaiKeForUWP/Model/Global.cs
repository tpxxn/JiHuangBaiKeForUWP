using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

namespace JiHuangBaiKeForUWP.Model
{

    public static class Global
    {
        public static readonly StorageFolder ApplicationFolder = ApplicationData.Current.LocalFolder;

        #region 游戏版本

        public static int GameVersion { get; set; }
        public static bool GameVersionChanged { get; set; }
        public static string[] BuiltInGameVersion = 
        {
            "DS(饥荒单机)", "RoG(巨兽统治)", "SW(失落之船)", "DST(饥荒联机)", "TGP及QQGame",
        };
        public static string[] BuiltInGameVersionXmlFolder =
        {
            "DS", "ROG", "SW", "DST", "Tencent",
        };
        
        public static ObservableCollection<string> VersionData = new ObservableCollection<string>();

        #endregion

        public static readonly Style Transparent = (Style)Application.Current.Resources["TransparentDialog"];

    }
}
