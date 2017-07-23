using System.Collections.Generic;

namespace JiHuangBaiKeForUWP.Model
{
    public class VersionJson
    {
        public class GameVersionRootNode
        {
            public List<string> GameVersion { get; set; }

            public GameVersionRootNode()
            {
                GameVersion = new List<string>();
            }
        }

        public class RootObject
        {
            public GameVersionRootNode GameVersionRootNode { get; set; }

            public RootObject()
            {
                GameVersionRootNode = new GameVersionRootNode();
            }
        }
    }
}
