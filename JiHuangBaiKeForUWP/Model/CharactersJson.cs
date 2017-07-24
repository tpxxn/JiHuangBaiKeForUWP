using JiHuangBaiKeForUWP.DateType;
using System.Collections.Generic;

namespace JiHuangBaiKeForUWP.Model
{
    public class CharactersJson
    {
        public class RootObject
        {
            public List<Character> Characters { get; set; }

            public RootObject()
            {
                Characters = new List<Character>();
            }
        }
    }
}
