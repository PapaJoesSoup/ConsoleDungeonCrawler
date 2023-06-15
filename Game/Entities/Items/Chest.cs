namespace ConsoleDungeonCrawler.Game.Entities.Items
{
  internal class Chest : Item
  {
    internal ItemRarity ItemRarity = ItemRarity.Common;
    internal List<Item> Items = new List<Item>();

    internal Chest()
    {
      Type = ItemType.Chest;
      Name = $"{ItemRarity} Chest";
      Description = $"A {ItemRarity} Chest";
      Quantity = 1;
      StackSize = 1;
      BuyCost = 1M;
      SellCost = 0.10M;

      Items = GetChestItems();
    }

    internal Chest(int quantity, ItemRarity rarity, decimal buyCost, decimal sellCost)
    {
      Type = ItemType.Chest;
      Name = "Chest";
      Description = "A Chest";
      ItemRarity = rarity;
      Quantity = quantity;
      StackSize = 1;
      BuyCost = buyCost;
      SellCost = sellCost;

      Items = GetChestItems();
    }

    private List<Item> GetChestItems()
    {
      int randomItems = Dice.Roll(0, 10);
      switch (randomItems)
      {
        case 0:
          return new List<Item>() { Armor.GetRandomItem(), Potion.GetRandomItem(), Gold.GetRandomItem() };
        case 1:
          return new List<Item>() { Bandage.GetRandomItem(), Gold.GetRandomItem() };
        case 2:
          return new List<Item>() { Weapon.GetRandomItem(),Potion.GetRandomItem(), Bandage.GetRandomItem(), Gold.GetRandomItem() };
        case 3:
          return new List<Item>() { Food.GetRandomItem(), Food.GetRandomItem(), Potion.GetRandomItem(), Bandage.GetRandomItem(), Gold.GetRandomItem() };
        case 4:
          return new List<Item>() { Armor.GetRandomItem(), Bandage.GetRandomItem(), Gold.GetRandomItem() };
        case 5:
          return new List<Item>() { Potion.GetRandomItem(), Food.GetRandomItem(), Gold.GetRandomItem() };
        case 6:
          return new List<Item>() { Armor.GetRandomItem(), Weapon.GetRandomItem(), Gold.GetRandomItem() };
        case 7:
          return new List<Item>() { Armor.GetRandomItem(), Weapon.GetRandomItem(), Potion.GetRandomItem(), Bandage.GetRandomItem(), Gold.GetRandomItem() };
        case 8:
          return new List<Item>() { Food.GetRandomItem(), Potion.GetRandomItem(), Potion.GetRandomItem(), Gold.GetRandomItem() };
        case 9:
          return new List<Item>() { Potion.GetRandomItem(), Bandage.GetRandomItem(), Gold.GetRandomItem() };
        case 10:
          return new List<Item>() { Potion.GetRandomItem(), Armor.GetRandomItem(), Gold.GetRandomItem() };
         default:
          return new List<Item>() { Potion.GetRandomItem(), Gold.GetRandomItem() };
      }
    }

    internal override bool Use()
    {
      bool result = true;
      if (Items.Count > 0)
      {
        foreach (Item item in Items)
        {
          Inventory.AddItem(item);
        }
      }
      foreach (Bag bag in Inventory.Bags)
      {
        if (!bag.Items.Contains(this)) continue;
        bag.Items.Remove(this);
        return result;
      }

      return result;
    }
    
    internal new static Item GetRandomItem()
    {
      int min = 0;
      int max = 5;

      int randomChest = Dice.Roll(min, max);
      switch (randomChest)
      {
        case 0:
          return new Chest(1, ItemRarity.Poor, 0.5M, 0.1M);
        case 1:
          return new Chest(1, ItemRarity.Common, 1, 0.5M);
        case 2:
          return new Chest(1, ItemRarity.Uncommon, 5, 1M);
        case 3:
          return new Chest(1, ItemRarity.Rare, 10, 1.5M);
        case 4:
          return new Chest(1, ItemRarity.Epic, 20, 4M);
        case 5:
          return new Chest(1, ItemRarity.Legendary, 50, 7.5M);
        default:
          return new Chest(1, ItemRarity.Common, 1, 0.5M);
      }
    }
  }
}
