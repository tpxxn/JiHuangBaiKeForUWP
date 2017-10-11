using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace JiHuangBaiKeForUWP.Model
{
    internal static class StringProcess
    {
        /// <summary>
        /// 删除重复数据
        /// </summary>
        /// <param name="str">字符串数组</param>
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

        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="path">长字符串</param>
        /// <returns>资源文件路径</returns>
        public static string GetFileName(string path)
        {
            path = path.Substring(path.LastIndexOf('/') + 1, path.Length - path.LastIndexOf('/') - 5);
            return path;
        }

        /// <summary>
        /// 返回Json文本
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>string类型文本</returns>
        public static async Task<string> GetJsonString(string fileName)
        {
            //            var folderExists = await ApplicationFolder.TryGetItemAsync(BuiltInGameVersionJsonFolder[GameVersion]);
            //            var uri = folderExists == null ? new Uri("ms-appx:///Json/" + BuiltInGameVersionJsonFolder[GameVersion] + "/" + fileName) : new Uri(ApplicationFolder.Path + "/" + BuiltInGameVersionJsonFolder[GameVersion] + "/" + fileName);
            var uri = new Uri("ms-appx:///Json/" + Global.BuiltInGameVersionJsonFolder[Global.GameVersion] + "/" + fileName);
            var storageFile = await StorageFile.GetFileFromApplicationUriAsync(uri);
            var str = await FileIO.ReadTextAsync(storageFile);
            return str;
        }

        /// <summary>
        /// 获取游戏图片位置
        /// </summary>
        /// <param name="str">图片名称</param>
        /// <returns>完整路径</returns>
        public static string GetGameResourcePath(string str)
        {
            var strHead = str.Substring(0, 1);
            switch (strHead)
            {
                case "A":
                    str = $"ms-appx:///Assets/GameResources/Creatures/{str}.png";
                    break;
                case "C":
                    str = $"ms-appx:///Assets/GameResources/Charcters/{str}.png";
                    break;
                case "F":
                    str = $"ms-appx:///Assets/GameResources/Foods/{str}.png";
                    break;
                case "G":
                    str = $"ms-appx:///Assets/GameResources/Goods/{str}.png";
                    break;
                case "N":
                    str = $"ms-appx:///Assets/GameResources/Natures/{str}.png";
                    break;
                case "S":
                    str = $"ms-appx:///Assets/GameResources/Sciences/{str}.png";
                    break;
                case "T":
                    str = $"ms-appx:///Assets/GameResources/Goods/{str}.png";
                    break;
            }
            return str;
        }

        /// <summary>
        /// 检查控制台数字文本框
        /// </summary>
        /// <param name="textbox">文本框对象</param>
        public static void ConsoleNumTextCheck(TextBox textbox)
        {
            if (!Regex.IsMatch(textbox.Text, "^\\d*\\.?\\d*$") && textbox.Text != "")
            {
                int pos = textbox.SelectionStart - 1;
                textbox.Text = textbox.Text.Remove(pos, 1);
                textbox.SelectionStart = pos;
            }
            if (textbox.Text != "")
            {
                if (int.Parse(textbox.Text) > 1000)
                {
                    textbox.Text = "1000";
                }
            }
        }
    }
}
