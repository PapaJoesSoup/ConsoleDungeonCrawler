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


    internal Armor()
    {
    }

    internal Armor(ArmorType type)
    {
      ArmorType = type;
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

    }

    internal static Armor GetRandomArmor()
    {
      Random random = new Random();
      int armorType = random.Next(0, 4);
      int armorName = random.Next(0, 5);
      int armorRarity = random.Next(0, 5);
      return Inventory.ArmorDictionary[(ArmorType)armorType][(ArmorName)armorName][armorRarity];
    }
  }
}
