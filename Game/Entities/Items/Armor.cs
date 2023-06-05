namespace ConsoleDungeonCrawler.Game.Entities.Items
{
  internal class Armor : Item
  {
    internal ArmorType ArmorType = ArmorType.None;
    internal ArmorName ArmorName = ArmorName.None;
    internal ItemRarity Rarity = ItemRarity.Common;
    internal int ArmorValue = 0;
    internal int ArmorBonus = 0;
    internal int Durability = 0;
    internal int MaxDurability = 0;
    internal BuffType BuffType = BuffType.None;
    internal static bool initialized = false;

    internal static Dictionary<ArmorType, Dictionary<ArmorName, List<Armor>>> ArmorDictionary = new Dictionary<ArmorType, Dictionary<ArmorName, List<Armor>>>();

    internal Armor()
    {
      InitArmorDictionary();
    }

    internal Armor(ArmorType type)
    {
      ArmorType = type;
      InitArmorDictionary();
    }

    internal Armor(ArmorType type, ArmorName name, BuffType buffType, ItemRarity rarity, int armorValue, int durability, decimal cost, decimal value)
    {
      Name = $"{name} {type} Armor";
      Description = $" {rarity} {name} {type} Armor";
      Cost = cost;
      Value = value;

      ArmorType = type;
      ArmorName = name;
      Rarity = rarity;
      ArmorValue = armorValue;
      BuffType = buffType;
      Durability = durability;
      MaxDurability = durability;

      InitArmorDictionary();
    }

    internal static void InitArmorDictionary()
    {
      if (initialized) return;

      ArmorDictionary.Add(ArmorType.Head, new Dictionary<ArmorName, List<Armor>>());
      ArmorDictionary.Add(ArmorType.Body, new Dictionary<ArmorName, List<Armor>>());
      ArmorDictionary.Add(ArmorType.Legs, new Dictionary<ArmorName, List<Armor>>());
      ArmorDictionary.Add(ArmorType.Feet, new Dictionary<ArmorName, List<Armor>>());
      ArmorDictionary.Add(ArmorType.Hands, new Dictionary<ArmorName, List<Armor>>());

      ArmorDictionary[ArmorType.Head].Add(ArmorName.None, new List<Armor>());
      ArmorDictionary[ArmorType.Head].Add(ArmorName.Cloth, new List<Armor>());
      ArmorDictionary[ArmorType.Head].Add(ArmorName.Leather, new List<Armor>());
      ArmorDictionary[ArmorType.Head].Add(ArmorName.ChainMail, new List<Armor>());
      ArmorDictionary[ArmorType.Head].Add(ArmorName.Plate, new List<Armor>());

      ArmorDictionary[ArmorType.Body].Add(ArmorName.None, new List<Armor>());
      ArmorDictionary[ArmorType.Body].Add(ArmorName.Cloth, new List<Armor>());
      ArmorDictionary[ArmorType.Body].Add(ArmorName.Leather, new List<Armor>());
      ArmorDictionary[ArmorType.Body].Add(ArmorName.ChainMail, new List<Armor>());
      ArmorDictionary[ArmorType.Body].Add(ArmorName.Plate, new List<Armor>());

      ArmorDictionary[ArmorType.Legs].Add(ArmorName.None, new List<Armor>());
      ArmorDictionary[ArmorType.Legs].Add(ArmorName.Cloth, new List<Armor>());
      ArmorDictionary[ArmorType.Legs].Add(ArmorName.Leather, new List<Armor>());
      ArmorDictionary[ArmorType.Legs].Add(ArmorName.ChainMail, new List<Armor>());
      ArmorDictionary[ArmorType.Legs].Add(ArmorName.Plate, new List<Armor>());

      ArmorDictionary[ArmorType.Feet].Add(ArmorName.None, new List<Armor>());
      ArmorDictionary[ArmorType.Feet].Add(ArmorName.Cloth, new List<Armor>());
      ArmorDictionary[ArmorType.Feet].Add(ArmorName.Leather, new List<Armor>());
      ArmorDictionary[ArmorType.Feet].Add(ArmorName.ChainMail, new List<Armor>());
      ArmorDictionary[ArmorType.Feet].Add(ArmorName.Plate, new List<Armor>());

      ArmorDictionary[ArmorType.Hands].Add(ArmorName.None, new List<Armor>());
      ArmorDictionary[ArmorType.Hands].Add(ArmorName.Cloth, new List<Armor>());
      ArmorDictionary[ArmorType.Hands].Add(ArmorName.Leather, new List<Armor>());
      ArmorDictionary[ArmorType.Hands].Add(ArmorName.ChainMail, new List<Armor>());
      ArmorDictionary[ArmorType.Hands].Add(ArmorName.Plate, new List<Armor>());

      ArmorDictionary[ArmorType.Head][ArmorName.None].Add(new Armor(ArmorType.Head, ArmorName.None, BuffType.None, ItemRarity.Common, 0, 0, 0, 0));
      ArmorDictionary[ArmorType.Head][ArmorName.Cloth].Add(new Armor(ArmorType.Head, ArmorName.Cloth, BuffType.None, ItemRarity.Common, 1, 10, 1, 1));
      ArmorDictionary[ArmorType.Head][ArmorName.Leather].Add(new Armor(ArmorType.Head, ArmorName.Leather, BuffType.None, ItemRarity.Common, 2, 20, 2, 2));
      ArmorDictionary[ArmorType.Head][ArmorName.ChainMail].Add(new Armor(ArmorType.Head, ArmorName.ChainMail, BuffType.None, ItemRarity.Common, 3, 30, 3, 3));
      ArmorDictionary[ArmorType.Head][ArmorName.Plate].Add(new Armor(ArmorType.Head, ArmorName.Plate, BuffType.None, ItemRarity.Common, 4, 40, 4, 4));

      ArmorDictionary[ArmorType.Head][ArmorName.Cloth].Add(new Armor(ArmorType.Head, ArmorName.Cloth, BuffType.None, ItemRarity.Uncommon, 2, 10, 1, 1));
      ArmorDictionary[ArmorType.Head][ArmorName.Leather].Add(new Armor(ArmorType.Head, ArmorName.Leather, BuffType.None, ItemRarity.Uncommon, 4, 20, 2, 2));
      ArmorDictionary[ArmorType.Head][ArmorName.ChainMail].Add(new Armor(ArmorType.Head, ArmorName.ChainMail, BuffType.None, ItemRarity.Uncommon, 6, 30, 3, 3));
      ArmorDictionary[ArmorType.Head][ArmorName.Plate].Add(new Armor(ArmorType.Head, ArmorName.Plate, BuffType.None, ItemRarity.Uncommon, 8, 40, 4, 4));

      ArmorDictionary[ArmorType.Head][ArmorName.Cloth].Add(new Armor(ArmorType.Head, ArmorName.Cloth, BuffType.None, ItemRarity.Rare, 3, 10, 1, 1));
      ArmorDictionary[ArmorType.Head][ArmorName.Leather].Add(new Armor(ArmorType.Head, ArmorName.Leather, BuffType.None, ItemRarity.Rare, 6, 20, 2, 2));
      ArmorDictionary[ArmorType.Head][ArmorName.ChainMail].Add(new Armor(ArmorType.Head, ArmorName.ChainMail, BuffType.None, ItemRarity.Rare, 9, 30, 3, 3));
      ArmorDictionary[ArmorType.Head][ArmorName.Plate].Add(new Armor(ArmorType.Head, ArmorName.Plate, BuffType.None, ItemRarity.Rare, 12, 40, 4, 4));

      ArmorDictionary[ArmorType.Head][ArmorName.Cloth].Add(new Armor(ArmorType.Head, ArmorName.Cloth, BuffType.None, ItemRarity.Epic, 4, 10, 1, 1));
      ArmorDictionary[ArmorType.Head][ArmorName.Leather].Add(new Armor(ArmorType.Head, ArmorName.Leather, BuffType.None, ItemRarity.Epic, 8, 20, 2, 2));
      ArmorDictionary[ArmorType.Head][ArmorName.ChainMail].Add(new Armor(ArmorType.Head, ArmorName.ChainMail, BuffType.None, ItemRarity.Epic, 12, 30, 3, 3));
      ArmorDictionary[ArmorType.Head][ArmorName.Plate].Add(new Armor(ArmorType.Head, ArmorName.Plate, BuffType.None, ItemRarity.Epic, 16, 40, 4, 4));

      ArmorDictionary[ArmorType.Head][ArmorName.Cloth].Add(new Armor(ArmorType.Head, ArmorName.Cloth, BuffType.None, ItemRarity.Legendary, 5, 10, 1, 1));
      ArmorDictionary[ArmorType.Head][ArmorName.Leather].Add(new Armor(ArmorType.Head, ArmorName.Leather, BuffType.None, ItemRarity.Legendary, 10, 20, 2, 2));
      ArmorDictionary[ArmorType.Head][ArmorName.ChainMail].Add(new Armor(ArmorType.Head, ArmorName.ChainMail, BuffType.None, ItemRarity.Legendary, 15, 30, 3, 3));
      ArmorDictionary[ArmorType.Head][ArmorName.Plate].Add(new Armor(ArmorType.Head, ArmorName.Plate, BuffType.None, ItemRarity.Legendary, 20, 40, 4, 4));
      
      ArmorDictionary[ArmorType.Body][ArmorName.None].Add(new Armor(ArmorType.Body, ArmorName.None, BuffType.None, ItemRarity.Common, 0, 0, 0, 0));
      ArmorDictionary[ArmorType.Body][ArmorName.Cloth].Add(new Armor(ArmorType.Body, ArmorName.Cloth, BuffType.None, ItemRarity.Common, 1, 10, 1, 1));
      ArmorDictionary[ArmorType.Body][ArmorName.Leather].Add(new Armor(ArmorType.Body, ArmorName.Leather, BuffType.None, ItemRarity.Common, 2, 20, 2, 2));
      ArmorDictionary[ArmorType.Body][ArmorName.ChainMail].Add(new Armor(ArmorType.Body, ArmorName.ChainMail, BuffType.None, ItemRarity.Common, 3, 30, 3, 3));
      ArmorDictionary[ArmorType.Body][ArmorName.Plate].Add(new Armor(ArmorType.Body, ArmorName.Plate, BuffType.None, ItemRarity.Common, 4, 40, 4, 4));

      ArmorDictionary[ArmorType.Body][ArmorName.Cloth].Add(new Armor(ArmorType.Body, ArmorName.Cloth, BuffType.None, ItemRarity.Uncommon, 2, 10, 1, 1));
      ArmorDictionary[ArmorType.Body][ArmorName.Leather].Add(new Armor(ArmorType.Body, ArmorName.Leather, BuffType.None, ItemRarity.Uncommon, 4, 20, 2, 2));
      ArmorDictionary[ArmorType.Body][ArmorName.ChainMail].Add(new Armor(ArmorType.Body, ArmorName.ChainMail, BuffType.None, ItemRarity.Uncommon, 6, 30, 3, 3));
      ArmorDictionary[ArmorType.Body][ArmorName.Plate].Add(new Armor(ArmorType.Body, ArmorName.Plate, BuffType.None, ItemRarity.Uncommon, 8, 40, 4, 4));

      ArmorDictionary[ArmorType.Body][ArmorName.Cloth].Add(new Armor(ArmorType.Body, ArmorName.Cloth, BuffType.None, ItemRarity.Rare, 3, 10, 1, 1));
      ArmorDictionary[ArmorType.Body][ArmorName.Leather].Add(new Armor(ArmorType.Body, ArmorName.Leather, BuffType.None, ItemRarity.Rare, 6, 20, 2, 2));
      ArmorDictionary[ArmorType.Body][ArmorName.ChainMail].Add(new Armor(ArmorType.Body, ArmorName.ChainMail, BuffType.None, ItemRarity.Rare, 9, 30, 3, 3));
      ArmorDictionary[ArmorType.Body][ArmorName.Plate].Add(new Armor(ArmorType.Body, ArmorName.Plate, BuffType.None, ItemRarity.Rare, 12, 40, 4, 4));

      ArmorDictionary[ArmorType.Body][ArmorName.Cloth].Add(new Armor(ArmorType.Body, ArmorName.Cloth, BuffType.None, ItemRarity.Epic, 4, 10, 1, 1));
      ArmorDictionary[ArmorType.Body][ArmorName.Leather].Add(new Armor(ArmorType.Body, ArmorName.Leather, BuffType.None, ItemRarity.Epic, 8, 20, 2, 2));
      ArmorDictionary[ArmorType.Body][ArmorName.ChainMail].Add(new Armor(ArmorType.Body, ArmorName.ChainMail, BuffType.None, ItemRarity.Epic, 12, 30, 3, 3));
      ArmorDictionary[ArmorType.Body][ArmorName.Plate].Add(new Armor(ArmorType.Body, ArmorName.Plate, BuffType.None, ItemRarity.Epic, 16, 40, 4, 4));

      ArmorDictionary[ArmorType.Body][ArmorName.Cloth].Add(new Armor(ArmorType.Body, ArmorName.Cloth, BuffType.None, ItemRarity.Legendary, 5, 10, 1, 1));
      ArmorDictionary[ArmorType.Body][ArmorName.Leather].Add(new Armor(ArmorType.Body, ArmorName.Leather, BuffType.None, ItemRarity.Legendary, 10, 20, 2, 2));
      ArmorDictionary[ArmorType.Body][ArmorName.ChainMail].Add(new Armor(ArmorType.Body, ArmorName.ChainMail, BuffType.None, ItemRarity.Legendary, 15, 30, 3, 3));
      ArmorDictionary[ArmorType.Body][ArmorName.Plate].Add(new Armor(ArmorType.Body, ArmorName.Plate, BuffType.None, ItemRarity.Legendary, 20, 40, 4, 4));
  
      ArmorDictionary[ArmorType.Legs][ArmorName.None].Add(new Armor(ArmorType.Legs, ArmorName.None, BuffType.None, ItemRarity.Common, 0, 0, 0, 0));
      ArmorDictionary[ArmorType.Legs][ArmorName.Cloth].Add(new Armor(ArmorType.Legs, ArmorName.Cloth, BuffType.None, ItemRarity.Common, 1, 10, 1, 1));
      ArmorDictionary[ArmorType.Legs][ArmorName.Leather].Add(new Armor(ArmorType.Legs, ArmorName.Leather, BuffType.None, ItemRarity.Common, 2, 20, 2, 2));
      ArmorDictionary[ArmorType.Legs][ArmorName.ChainMail].Add(new Armor(ArmorType.Legs, ArmorName.ChainMail, BuffType.None, ItemRarity.Common, 3, 30, 3, 3));
      ArmorDictionary[ArmorType.Legs][ArmorName.Plate].Add(new Armor(ArmorType.Legs, ArmorName.Plate, BuffType.None, ItemRarity.Common, 4, 40, 4, 4));

      ArmorDictionary[ArmorType.Legs][ArmorName.Cloth].Add(new Armor(ArmorType.Legs, ArmorName.Cloth, BuffType.None, ItemRarity.Uncommon, 2, 10, 1, 1));
      ArmorDictionary[ArmorType.Legs][ArmorName.Leather].Add(new Armor(ArmorType.Legs, ArmorName.Leather, BuffType.None, ItemRarity.Uncommon, 4, 20, 2, 2));
      ArmorDictionary[ArmorType.Legs][ArmorName.ChainMail].Add(new Armor(ArmorType.Legs, ArmorName.ChainMail, BuffType.None, ItemRarity.Uncommon, 6, 30, 3, 3));
      ArmorDictionary[ArmorType.Legs][ArmorName.Plate].Add(new Armor(ArmorType.Legs, ArmorName.Plate, BuffType.None, ItemRarity.Uncommon, 8, 40, 4, 4));

      ArmorDictionary[ArmorType.Legs][ArmorName.Cloth].Add(new Armor(ArmorType.Legs, ArmorName.Cloth, BuffType.None, ItemRarity.Rare, 3, 10, 1, 1));
      ArmorDictionary[ArmorType.Legs][ArmorName.Leather].Add(new Armor(ArmorType.Legs, ArmorName.Leather, BuffType.None, ItemRarity.Rare, 6, 20, 2, 2));
      ArmorDictionary[ArmorType.Legs][ArmorName.ChainMail].Add(new Armor(ArmorType.Legs, ArmorName.ChainMail, BuffType.None, ItemRarity.Rare, 9, 30, 3, 3));
      ArmorDictionary[ArmorType.Legs][ArmorName.Plate].Add(new Armor(ArmorType.Legs, ArmorName.Plate, BuffType.None, ItemRarity.Rare, 12, 40, 4, 4));

      ArmorDictionary[ArmorType.Legs][ArmorName.Cloth].Add(new Armor(ArmorType.Legs, ArmorName.Cloth, BuffType.None, ItemRarity.Epic, 4, 10, 1, 1));
      ArmorDictionary[ArmorType.Legs][ArmorName.Leather].Add(new Armor(ArmorType.Legs, ArmorName.Leather, BuffType.None, ItemRarity.Epic, 8, 20, 2, 2));
      ArmorDictionary[ArmorType.Legs][ArmorName.ChainMail].Add(new Armor(ArmorType.Legs, ArmorName.ChainMail, BuffType.None, ItemRarity.Epic, 12, 30, 3, 3));
      ArmorDictionary[ArmorType.Legs][ArmorName.Plate].Add(new Armor(ArmorType.Legs, ArmorName.Plate, BuffType.None, ItemRarity.Epic, 16, 40, 4, 4));

      ArmorDictionary[ArmorType.Legs][ArmorName.Cloth].Add(new Armor(ArmorType.Legs, ArmorName.Cloth, BuffType.None, ItemRarity.Legendary, 5, 10, 1, 1));
      ArmorDictionary[ArmorType.Legs][ArmorName.Leather].Add(new Armor(ArmorType.Legs, ArmorName.Leather, BuffType.None, ItemRarity.Legendary, 10, 20, 2, 2));
      ArmorDictionary[ArmorType.Legs][ArmorName.ChainMail].Add(new Armor(ArmorType.Legs, ArmorName.ChainMail, BuffType.None, ItemRarity.Legendary, 15, 30, 3, 3));
      ArmorDictionary[ArmorType.Legs][ArmorName.Plate].Add(new Armor(ArmorType.Legs, ArmorName.Plate, BuffType.None, ItemRarity.Legendary, 20, 40, 4, 4));

      ArmorDictionary[ArmorType.Feet][ArmorName.None].Add(new Armor(ArmorType.Feet, ArmorName.None, BuffType.None, ItemRarity.Common, 0, 0, 0, 0));
      ArmorDictionary[ArmorType.Feet][ArmorName.Cloth].Add(new Armor(ArmorType.Feet, ArmorName.Cloth, BuffType.None, ItemRarity.Common, 1, 10, 1, 1));
      ArmorDictionary[ArmorType.Feet][ArmorName.Leather].Add(new Armor(ArmorType.Feet, ArmorName.Leather, BuffType.None, ItemRarity.Common, 2, 20, 2, 2));
      ArmorDictionary[ArmorType.Feet][ArmorName.ChainMail].Add(new Armor(ArmorType.Feet, ArmorName.ChainMail, BuffType.None, ItemRarity.Common, 3, 30, 3, 3));
      ArmorDictionary[ArmorType.Feet][ArmorName.Plate].Add(new Armor(ArmorType.Feet, ArmorName.Plate, BuffType.None, ItemRarity.Common, 4, 40, 4, 4));

      ArmorDictionary[ArmorType.Feet][ArmorName.Cloth].Add(new Armor(ArmorType.Feet, ArmorName.Cloth, BuffType.None, ItemRarity.Uncommon, 2, 10, 1, 1));
      ArmorDictionary[ArmorType.Feet][ArmorName.Leather].Add(new Armor(ArmorType.Feet, ArmorName.Leather, BuffType.None, ItemRarity.Uncommon, 4, 20, 2, 2));
      ArmorDictionary[ArmorType.Feet][ArmorName.ChainMail].Add(new Armor(ArmorType.Feet, ArmorName.ChainMail, BuffType.None, ItemRarity.Uncommon, 6, 30, 3, 3));
      ArmorDictionary[ArmorType.Feet][ArmorName.Plate].Add(new Armor(ArmorType.Feet, ArmorName.Plate, BuffType.None, ItemRarity.Uncommon, 8, 40, 4, 4));

      ArmorDictionary[ArmorType.Feet][ArmorName.Cloth].Add(new Armor(ArmorType.Feet, ArmorName.Cloth, BuffType.None, ItemRarity.Rare, 3, 10, 1, 1));
      ArmorDictionary[ArmorType.Feet][ArmorName.Leather].Add(new Armor(ArmorType.Feet, ArmorName.Leather, BuffType.None, ItemRarity.Rare, 6, 20, 2, 2));
      ArmorDictionary[ArmorType.Feet][ArmorName.ChainMail].Add(new Armor(ArmorType.Feet, ArmorName.ChainMail, BuffType.None, ItemRarity.Rare, 9, 30, 3, 3));
      ArmorDictionary[ArmorType.Feet][ArmorName.Plate].Add(new Armor(ArmorType.Feet, ArmorName.Plate, BuffType.None, ItemRarity.Rare, 12, 40, 4, 4));

      ArmorDictionary[ArmorType.Feet][ArmorName.Cloth].Add(new Armor(ArmorType.Feet, ArmorName.Cloth, BuffType.None, ItemRarity.Epic, 4, 10, 1, 1));
      ArmorDictionary[ArmorType.Feet][ArmorName.Leather].Add(new Armor(ArmorType.Feet, ArmorName.Leather, BuffType.None, ItemRarity.Epic, 8, 20, 2, 2));
      ArmorDictionary[ArmorType.Feet][ArmorName.ChainMail].Add(new Armor(ArmorType.Feet, ArmorName.ChainMail, BuffType.None, ItemRarity.Epic, 12, 30, 3, 3));
      ArmorDictionary[ArmorType.Feet][ArmorName.Plate].Add(new Armor(ArmorType.Feet, ArmorName.Plate, BuffType.None, ItemRarity.Epic, 16, 40, 4, 4));

      ArmorDictionary[ArmorType.Feet][ArmorName.Cloth].Add(new Armor(ArmorType.Feet, ArmorName.Cloth, BuffType.None, ItemRarity.Legendary, 5, 10, 1, 1));
      ArmorDictionary[ArmorType.Feet][ArmorName.Leather].Add(new Armor(ArmorType.Feet, ArmorName.Leather, BuffType.None, ItemRarity.Legendary, 10, 20, 2, 2));
      ArmorDictionary[ArmorType.Feet][ArmorName.ChainMail].Add(new Armor(ArmorType.Feet, ArmorName.ChainMail, BuffType.None, ItemRarity.Legendary, 15, 30, 3, 3));
      ArmorDictionary[ArmorType.Feet][ArmorName.Plate].Add(new Armor(ArmorType.Feet, ArmorName.Plate, BuffType.None, ItemRarity.Legendary, 20, 40, 4, 4));

      ArmorDictionary[ArmorType.Hands][ArmorName.None].Add(new Armor(ArmorType.Hands, ArmorName.None, BuffType.None, ItemRarity.Common, 0, 0, 0, 0));
      ArmorDictionary[ArmorType.Hands][ArmorName.Cloth].Add(new Armor(ArmorType.Hands, ArmorName.Cloth, BuffType.None, ItemRarity.Common, 1, 10, 1, 1));
      ArmorDictionary[ArmorType.Hands][ArmorName.Leather].Add(new Armor(ArmorType.Hands, ArmorName.Leather, BuffType.None, ItemRarity.Common, 2, 20, 2, 2));
      ArmorDictionary[ArmorType.Hands][ArmorName.ChainMail].Add(new Armor(ArmorType.Hands, ArmorName.ChainMail, BuffType.None, ItemRarity.Common, 3, 30, 3, 3));
      ArmorDictionary[ArmorType.Hands][ArmorName.Plate].Add(new Armor(ArmorType.Hands, ArmorName.Plate, BuffType.None, ItemRarity.Common, 4, 40, 4, 4));

      ArmorDictionary[ArmorType.Hands][ArmorName.Cloth].Add(new Armor(ArmorType.Hands, ArmorName.Cloth, BuffType.None, ItemRarity.Uncommon, 2, 10, 1, 1));
      ArmorDictionary[ArmorType.Hands][ArmorName.Leather].Add(new Armor(ArmorType.Hands, ArmorName.Leather, BuffType.None, ItemRarity.Uncommon, 4, 20, 2, 2));
      ArmorDictionary[ArmorType.Hands][ArmorName.ChainMail].Add(new Armor(ArmorType.Hands, ArmorName.ChainMail, BuffType.None, ItemRarity.Uncommon, 6, 30, 3, 3));
      ArmorDictionary[ArmorType.Hands][ArmorName.Plate].Add(new Armor(ArmorType.Hands, ArmorName.Plate, BuffType.None, ItemRarity.Uncommon, 8, 40, 4, 4));

      ArmorDictionary[ArmorType.Hands][ArmorName.Cloth].Add(new Armor(ArmorType.Hands, ArmorName.Cloth, BuffType.None, ItemRarity.Rare, 3, 10, 1, 1));
      ArmorDictionary[ArmorType.Hands][ArmorName.Leather].Add(new Armor(ArmorType.Hands, ArmorName.Leather, BuffType.None, ItemRarity.Rare, 6, 20, 2, 2));
      ArmorDictionary[ArmorType.Hands][ArmorName.ChainMail].Add(new Armor(ArmorType.Hands, ArmorName.ChainMail, BuffType.None, ItemRarity.Rare, 9, 30, 3, 3));
      ArmorDictionary[ArmorType.Hands][ArmorName.Plate].Add(new Armor(ArmorType.Hands, ArmorName.Plate, BuffType.None, ItemRarity.Rare, 12, 40, 4, 4));

      ArmorDictionary[ArmorType.Hands][ArmorName.Cloth].Add(new Armor(ArmorType.Hands, ArmorName.Cloth, BuffType.None, ItemRarity.Epic, 4, 10, 1, 1));
      ArmorDictionary[ArmorType.Hands][ArmorName.Leather].Add(new Armor(ArmorType.Hands, ArmorName.Leather, BuffType.None, ItemRarity.Epic, 8, 20, 2, 2));
      ArmorDictionary[ArmorType.Hands][ArmorName.ChainMail].Add(new Armor(ArmorType.Hands, ArmorName.ChainMail, BuffType.None, ItemRarity.Epic, 12, 30, 3, 3));
      ArmorDictionary[ArmorType.Hands][ArmorName.Plate].Add(new Armor(ArmorType.Hands, ArmorName.Plate, BuffType.None, ItemRarity.Epic, 16, 40, 4, 4));

      ArmorDictionary[ArmorType.Hands][ArmorName.Cloth].Add(new Armor(ArmorType.Hands, ArmorName.Cloth, BuffType.None, ItemRarity.Legendary, 5, 10, 1, 1));
      ArmorDictionary[ArmorType.Hands][ArmorName.Leather].Add(new Armor(ArmorType.Hands, ArmorName.Leather, BuffType.None, ItemRarity.Legendary, 10, 20, 2, 2));
      ArmorDictionary[ArmorType.Hands][ArmorName.ChainMail].Add(new Armor(ArmorType.Hands, ArmorName.ChainMail, BuffType.None, ItemRarity.Legendary, 15, 30, 3, 3));
      ArmorDictionary[ArmorType.Hands][ArmorName.Plate].Add(new Armor(ArmorType.Hands, ArmorName.Plate, BuffType.None, ItemRarity.Legendary, 20, 40, 4, 4));

      initialized = true;
    }

    internal static Armor GetRandomArmor()
    {
      Random random = new Random();
      int armorType = random.Next(0, 4);
      int armorName = random.Next(0, 5);
      int armorRarity = random.Next(0, 5);
      return ArmorDictionary[(ArmorType)armorType][(ArmorName)armorName][armorRarity];
    }
  }
}
