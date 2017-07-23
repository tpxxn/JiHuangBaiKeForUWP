using System.Collections.Generic;

namespace JiHuangBaiKeForUWP.Model
{
    public class CharactersJson
    {
        public class Character
        {
            public string Picture { get; set; }
            public string Name { get; set; }
            public string EnName { get; set; }
            public string Motto { get; set; }
            public List<string> Descriptions { get; set; }
            public object Health { get; set; }
            public object Hunger { get; set; }
            public object Sanity { get; set; }
            public object Damage { get; set; }
            public string Introduce { get; set; }
            public string Console { get; set; }
            public int? LogMeter { get; set; }
            public int? DamageDay { get; set; }
            public int? DamageDusk { get; set; }
            public int? DamageNight { get; set; }
        }

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
