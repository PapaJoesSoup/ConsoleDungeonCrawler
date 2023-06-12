using ConsoleDungeonCrawler.Game.Entities.Items;

namespace ConsoleDungeonCrawler.Game.Entities
{
  internal class Item
  {
    internal ItemType Type = ItemType.None;
    internal string Name = "None";
    internal string Description = "None";
    internal int Level = 1;
    internal ItemRarity Rarity = ItemRarity.Common;
    internal int Quantity = 1;
    internal int StackSize = 1;
    internal decimal BuyCost = 0;
    internal decimal SellCost = 0;

    public Item() { }

    public Item(ItemType type, int level, int qty, decimal cost, decimal value)
    {
      Type = type;
      Name = type.ToString();
      Level = level;
      Description = Type == ItemType.Gold ? $"some {type.ToString()}" : $"a {type.ToString()}";
      Quantity = qty;
      BuyCost = cost;
      SellCost = value;
    }

    public bool Use()
    {
      bool result = true;
      switch (Type)
      {
        case ItemType.Gold:
          Player.Gold += Level * Quantity * SellCost;
          break;
        case ItemType.Food:
          Player.Heal(((Food)this).BuffAmount); 
          break;
        case ItemType.Potion:
          switch (((Potion)this).BuffType)
          {
            case BuffType.Health:
              Player.Heal(((Potion)this).BuffAmount);
              break;
            case BuffType.Mana:
              Player.RestoreMana(((Potion)this).BuffAmount);
              break;
            case BuffType.HealthAndMana:
              Player.Heal(((Potion)this).BuffAmount);
              Player.RestoreMana(((Potion)this).BuffAmount);
              break;
          }
          break;
        case ItemType.Weapon:
          Player.EquipWeapon((Weapon)this);
          break;
        case ItemType.Armor:
          Player.EquipArmor((Armor)this);
          break;
        case ItemType.Bandage:
          Player.Heal(((Bandage)this).BuffAmount);
          break;
        case ItemType.Chest:
          foreach (Item t in ((Chest)this).Items)
            Inventory.AddItem(t);
          break;
        default:
          result = false;
          break;
      }
      return result;
    }
  }
}
