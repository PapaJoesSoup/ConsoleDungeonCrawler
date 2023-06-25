namespace ConsoleDungeonCrawler.Game.Entities.Items;

internal class Weapon : Item
{
  internal readonly WeaponType WeaponType;
  internal readonly int Damage;
  internal int Durability;
  internal int MaxDurability;
  internal readonly int Range;


  internal Weapon()
  {
    Type = ItemType.Weapon;
    Name = "Fists";
    Description = "Your fists";
    Quantity = 1;
    StackSize = 1;
    BuyCost = 1M;
    SellCost = 0.10M;
    
    WeaponType = WeaponType.Fists;
    Damage = 1;
    Durability = 100;
    MaxDurability = 100;
    Range = 1;

  }

  internal Weapon(string name, string description, WeaponType weaponType, ItemRarity rarity, int damage, int durability,
    int maxDurability, int range)
  {
    Type = ItemType.Weapon;
    Name = name;
    Description = description;
    Quantity = 1;
    StackSize = 1;
    BuyCost = 1M;
    SellCost = 0.10M;


    WeaponType = weaponType;
    Rarity = rarity;
    Damage = damage;
    Durability = durability;
    MaxDurability = maxDurability;
    Range = range;

  }

  internal static Weapon GetWeapon(WeaponType weaponType, int idx)
  {
    Weapon weapon = Inventory.WeaponTypes[weaponType][idx];
    return weapon;
  }

  internal override bool Use()
  {
    bool result = true;
    foreach (Bag bag in Inventory.Bags)
    {
      if (!bag.Items.Contains(this)) continue;
      bag.RemoveItem(this);
      Player.EquipWeapon(this);
      return result;
    }
    return false;
  }

  internal new static Item GetRandomItem()
  {
    WeaponType weaponType = (WeaponType)Dice.Roll(1, Inventory.WeaponTypes.Count - 1);
    Weapon weapon = Inventory.WeaponTypes[weaponType][Dice.Roll(0, Inventory.WeaponTypes[weaponType].Count - 1)];
    return weapon;
  }
}