using ConsoleDungeonCrawler.Game.Entities.Items;

namespace ConsoleDungeonCrawler.Game.Entities;

internal class Item
{
  internal ItemType Type = ItemType.None;
  internal string Name = "None";
  internal string Description = "None";
  internal int Level = 1;
  internal ItemRarity Rarity = ItemRarity.Common;
  internal int Quantity = 1;
  internal int StackSize = 1;
  internal decimal BuyCost;
  internal decimal SellCost;

  /// <summary>
  /// the default constructor is an item of type Gold with a quantity of 1 and a sell of 0.1
  /// </summary>
  internal Item() { }

  /// <summary>
  /// override method to Use an item.  This is overriden in the subtypes with a method uses the item of that type.
  /// </summary>
  /// <returns></returns>
  internal virtual bool Use()
  {
    return true;
  }

  /// <summary>
  /// this is the default method for getting a random item of type gold.
  /// It is obfuscated in the subtypes with a method that returns a random item of that type.
  /// </summary>
  /// <returns></returns>
  internal virtual Item GetRandomItem()
  {
    return Gold.GetRandomItem();
  }
}