namespace ConsoleDungeonCrawler.Game.Entities
{
  internal class Item
  {
    internal ItemType Type = ItemType.None;
    internal string Name = "None";
    internal string Description = "None";
    internal int Quantity = 1;
    internal int stackSize = 1;
    internal decimal Cost = 0;
    internal decimal Value = 0;

    public Item() { }

    public Item(ItemType type, int qty, decimal cost, decimal value)
    {
      this.Type = type;
      Quantity = qty;
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
