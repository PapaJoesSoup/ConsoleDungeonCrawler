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

    internal Armor()
    {
    }

    internal Armor(ArmorType type)
    {
      ArmorType = type;
    }

    internal Armor(ArmorType type, ArmorName name, ItemRarity rarity, int armorValue, int durability)
    {
      ArmorType = type;
      ArmorName = name;
      Rarity = rarity;
      ArmorValue = armorValue;
      Durability = durability;
      MaxDurability = durability;
    }
  }
}
