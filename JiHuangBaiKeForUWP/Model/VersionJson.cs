using System.Collections.Generic;

namespace JiHuangBaiKeForUWP.Model
{
    public class VersionJson
    {
        public class RootObject
        {
            public List<string> GameVersion { get; set; }

            public RootObject()
            {
                GameVersion = new List<string>();
            }
        }
    }
}
