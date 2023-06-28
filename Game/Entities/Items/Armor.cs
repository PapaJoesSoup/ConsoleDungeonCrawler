namespace ConsoleDungeonCrawler.Game.Entities.Items;

internal class Armor : Item
{
  internal readonly ArmorType ArmorType = ArmorType.None;
  internal readonly ArmorName ArmorName = ArmorName.None;
  internal int ArmorValue;
  internal int ArmorBonus;
  internal int Durability;
  internal int MaxDurability;
  internal BuffType BuffType = BuffType.None;

  internal static float ArmorValueMultiplier = 1.0f;
  internal static float ArmorBonusMultiplier = 1.0f;
  internal static float ArmorDurabilityMultiplier = 1.0f;
    
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
    Level = 1;
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

  internal override bool Use()
  {
    foreach (Bag bag in Inventory.Bags)
    {
      if (!bag.Items.Contains(this)) continue;
      bag.RemoveItem(this);
      Player.EquipArmor(this);
      return true;
    }
    return false;
  }
    
  internal new static Item GetRandomItem()
  {
    int armorType = Dice.Roll(1, 5);
    int armorName = Dice.Roll(1, 4);
    int armorRarity = Dice.Roll(0, 5);
    int level = Dice.Roll(1, Player.Level);

    Armor armor = Inventory.ArmorDictionary[(ArmorType)armorType][(ArmorName)armorName][armorRarity];
    armor.Level = level;
    return armor;
  }
}