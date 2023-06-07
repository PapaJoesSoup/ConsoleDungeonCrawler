namespace ConsoleDungeonCrawler.Game.Entities
{
  internal class Item
  {
    internal ItemType Type = ItemType.None;
    internal string Name = "None";
    internal string Description = "None";
    internal ItemRarity Rarity = ItemRarity.Common;
    internal int Quantity = 1;
    internal int StackSize = 1;
    internal decimal BuyCost = 0;
    internal decimal SellCost = 0;

    public Item() { }

    public Item(ItemType type, int qty, decimal cost, decimal value)
    {
      Type = type;
      Name = type.ToString();
      Description = Type == ItemType.Gold ? $"some {type.ToString()}" : $"a {type.ToString()}";
      Quantity = qty;
      BuyCost = cost;
      SellCost = value;

    }

    public void UseItem()
    {
      switch (this)
      {
        default:
          break;
      }
    }
  }
}
