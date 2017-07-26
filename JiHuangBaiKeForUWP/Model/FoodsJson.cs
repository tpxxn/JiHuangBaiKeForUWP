using System.Collections.Generic;

namespace JiHuangBaiKeForUWP.Model
{
    public class Restrictions1
    {
        public string Pre { get; set; }
        public string Text { get; set; }
    }

    public class Restrictions2
    {
        public string Pre { get; set; }
        public string Text { get; set; }
    }

    public class Restrictions3
    {
        public string Pre { get; set; }
        public string Text { get; set; }
    }

    public class FoodRecipe
    {
        public string Picture { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public bool PortableCrockPot { get; set; }
        public double Health { get; set; }
        public double Hunger { get; set; }
        public double Sanity { get; set; }
        public double Perish { get; set; }
        public double Cooktime { get; set; }
        public double Priority { get; set; }
        public string NeedPicture1 { get; set; }
        public string Need1 { get; set; }
        public string NeedPicture2 { get; set; }
        public string Need2 { get; set; }
        public string NeedPictureOr { get; set; }
        public string NeedOr { get; set; }
        public string NeedPicture3 { get; set; }
        public string Need3 { get; set; }
        public Restrictions1 Restrictions1 { get; set; }
        public Restrictions2 Restrictions2 { get; set; }
        public Restrictions3 Restrictions3 { get; set; }
        public string Recommend1 { get; set; }
        public string Recommend2 { get; set; }
        public string Recommend3 { get; set; }
        public string Recommend4 { get; set; }
        public string Introduce { get; set; }
        public string Console { get; set; }
    }

    public class FoodMeat
    {
        public string Picture { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public double Health { get; set; }
        public double Hunger { get; set; }
        public double Sanity { get; set; }
        public double Perish { get; set; }
        public string Attribute { get; set; }
        public string AttributeValue { get; set; }
        public string Introduce { get; set; }
        public string Console { get; set; }
        public string Attribute2 { get; set; }
        public string AttributeValue2 { get; set; }
    }

    public class FoodVegetable
    {
        public string Picture { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public double Health { get; set; }
        public double Hunger { get; set; }
        public double Sanity { get; set; }
        public double Perish { get; set; }
        public string Attribute { get; set; }
        public string AttributeValue { get; set; }
        public string Introduce { get; set; }
        public string Console { get; set; }
    }

    public class FoodFruit
    {
        public string Picture { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public double Health { get; set; }
        public double Hunger { get; set; }
        public double Sanity { get; set; }
        public double Perish { get; set; }
        public string Attribute { get; set; }
        public string AttributeValue { get; set; }
        public string Introduce { get; set; }
        public string Console { get; set; }
        public string Attribute2 { get; set; }
        public string AttributeValue2 { get; set; }
    }

    public class FoodEgg
    {
        public string Picture { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public double Health { get; set; }
        public double Hunger { get; set; }
        public double Sanity { get; set; }
        public double Perish { get; set; }
        public string Attribute { get; set; }
        public string AttributeValue { get; set; }
        public string Introduce { get; set; }
        public string Console { get; set; }
    }

    public class FoodOther
    {
        public string Picture { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public double Health { get; set; }
        public double Hunger { get; set; }
        public double Sanity { get; set; }
        public double Perish { get; set; }
        public string Attribute { get; set; }
        public string AttributeValue { get; set; }
        public string Introduce { get; set; }
        public string Console { get; set; }
    }

    public class FoodNoFc
    {
        public string Picture { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public double Health { get; set; }
        public double Hunger { get; set; }
        public double Sanity { get; set; }
        public double Perish { get; set; }
        public string Attribute { get; set; }
        public string Introduce { get; set; }
        public string Console { get; set; }
    }

    public class FoodRootObject
    {
        public List<FoodRecipe> FoodRecipe { get; set; }
        public List<FoodMeat> FoodMeats { get; set; }
        public List<FoodVegetable> FoodVegetables { get; set; }
        public List<FoodFruit> FoodFruit { get; set; }
        public List<FoodEgg> FoodEggs { get; set; }
        public List<FoodOther> FoodOthers { get; set; }
        public List<FoodNoFc> FoodNoFc { get; set; }
    }
}
