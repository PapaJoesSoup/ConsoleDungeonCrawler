namespace ConsoleDungeonCrawler.Game.Entities.Items
{
  internal class Armor : Item
  {
    internal ArmorType ArmorType = ArmorType.None;
    internal ArmorName ArmorName = ArmorName.None;
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

    internal Armor(ArmorType type, ArmorName name, BuffType buffType, ItemRarity rarity, int armorValue, int durability, decimal buyCost, decimal sellCost)
    {
      Type = ItemType.Armor;
      Name = $"{name} {type} Armor";
      Description = $" {rarity} {name} {type} Armor";
      Rarity = rarity;
      Quantity = 1;
      StackSize = 1;

      BuyCost = buyCost;
      SellCost = sellCost;

      ArmorType = type;
      ArmorName = name;
      ArmorValue = armorValue;
      BuffType = buffType;
      Durability = durability;
      MaxDurability = durability;

    }

    internal static Armor GetRandomArmor()
    {
      int armorType = Dice.Roll(1, 5);
      int armorName = Dice.Roll(1, 4); // 0 is None
      int armorRarity = Dice.Roll(0, 5);
      return Inventory.ArmorDictionary[(ArmorType)armorType][(ArmorName)armorName][armorRarity];
    }
  }
}
