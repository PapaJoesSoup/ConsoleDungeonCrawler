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

    /// <summary>
    /// the default constructor is an item of type Gold with a quantity of 1 and a value of 0.1
    /// </summary>
    internal Item() { }

    /// <summary>
    /// this constructor is used for creating items of a specific ItemType
    /// </summary>
    /// <param name="type"></param>
    /// <param name="level"></param>
    /// <param name="qty"></param>
    /// <param name="cost"></param>
    /// <param name="value"></param>
    internal Item(ItemType type, int level, int qty, decimal cost, decimal value)
    {
      Type = type;
      Name = type.ToString();
      Level = level;
      Description = Type == ItemType.Gold ? $"some {type.ToString()}" : $"a {type.ToString()}";
      Quantity = qty;
      BuyCost = cost;
      SellCost = value;
    }

    internal virtual bool Use()
    {
      bool result = true;
      Player.Gold += Level * Quantity * SellCost;
      return result;
    }

    /// <summary>
    /// this is the default method for getting a random item of type gold.
    /// It is obfuscated in the subtypes with a method that returns a random item of that type.
    /// </summary>
    /// <returns></returns>
    internal static Item GetRandomItem()
    {
      return new Item(ItemType.Gold, Dice.Roll(1, 10), Dice.Roll(1, 10), 0.1M, 0.1M);
    }
  }
}
