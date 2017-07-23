using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            public string Descriptions1 { get; set; }
            public string Health { get; set; }
            public string Hunger { get; set; }
            public string Sanity { get; set; }
            public string Damage { get; set; }
            public string Introduce { get; set; }
            public string Console { get; set; }
            public string Descriptions2 { get; set; }
            public string Descriptions3 { get; set; }
            public string LogMeter { get; set; }
            public string DamageDay { get; set; }
            public string DamageDusk { get; set; }
            public string DamageNight { get; set; }
        }

        public class CharacterNode
        {
            public List<Character> Character { get; set; }

            public CharacterNode()
            {
                Character = new List<Character>();
            }
        }

        public class RootObject
        {
            public CharacterNode CharacterNode { get; set; }

            public RootObject()
            {
                CharacterNode = new CharacterNode();
            }
        }
    }
}
