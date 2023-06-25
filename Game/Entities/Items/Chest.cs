using System.Drawing;
using ConsoleDungeonCrawler.Game.Screens;

namespace ConsoleDungeonCrawler.Game.Entities.Items;

internal class Chest : Item
{
  private readonly ItemRarity itemRarity;
  private readonly List<Item> items;

  private Chest(int quantity, ItemRarity rarity, decimal buyCost, decimal sellCost)
  {
    Type = ItemType.Chest;
    Name = Type.ToString();
    Description = "A Chest";
    itemRarity = rarity;
    Quantity = quantity;
    StackSize = 1;
    BuyCost = buyCost;
    SellCost = sellCost;

    items = GetChestItems();
  }

  private static List<Item> GetChestItems()
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
    GamePlay.Messages.Add(new Message("You open the chest...", Color.DarkOrange, Color.Black));
    if (items.Count > 0)
    {
      foreach (Item item in items)
      {
        Inventory.AddItem(item);
        GamePlay.Messages.Add(item.Type == ItemType.Gold
          ? new Message($"You have gained {((Gold)item).GetValue()} {item.Name}!", Color.DarkOrange, Color.Black)
          : new Message($"You have gained {item.Quantity} {item.Name}{(item.Quantity > 1 ? "s!" : "!")}",
            Color.DarkOrange, Color.Black));
      }
    }
    foreach (Bag bag in Inventory.Bags)
    {
      if (!bag.Items.Contains(this)) continue;
      bag.RemoveItem(this);
      GamePlay.MessageSection();
      return result;
    }
    return result;
  }
    
  internal new static Item GetRandomItem()
  {
    int randomChest = Dice.Roll(0, 5);
    return randomChest switch
    {
      0 => new Chest(1, ItemRarity.Poor, 0.5M, 0.1M),
      1 => new Chest(1, ItemRarity.Common, 1, 0.5M),
      2 => new Chest(1, ItemRarity.Uncommon, 5, 1M),
      3 => new Chest(1, ItemRarity.Rare, 10, 1.5M),
      4 => new Chest(1, ItemRarity.Epic, 20, 4M),
      5 => new Chest(1, ItemRarity.Legendary, 50, 7.5M),
      _ => new Chest(1, ItemRarity.Common, 1, 0.5M)
    };
  }
}