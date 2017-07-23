using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using System.Xml.Linq;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.Storage.Streams;

namespace JiHuangBaiKeForUWP.Model
{
    /// <summary>
    /// 游戏版本Xml序列化/反序列化表
    /// </summary>
    [XmlRoot(ElementName = "GameVersionRootNode")]
    public class Version
    {
        /// <summary>
        /// 游戏版本列表
        /// </summary>
        [XmlElement(ElementName = "GameVersion")]
        public List<string> GameVersion { get; set; }

    }

}
