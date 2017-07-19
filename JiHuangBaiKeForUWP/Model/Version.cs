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
    [XmlRoot(ElementName = "GameVersionRootNode")]
    public class Version
    {
        [XmlElement(ElementName = "GameVersion")]
        public List<string> GameVersion { get; set; }
    }

}
