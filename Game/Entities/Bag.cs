namespace ConsoleDungeonCrawler.Game.Entities
{
  internal class Bag
  {
    internal readonly int Capacity = 20;
    internal readonly List<Item> Items;

    internal Bag()
    {
      Items = new List<Item>();
    }

    internal Bag(int capacity)
    {
      Capacity = capacity;
      Items = new List<Item>();
    }

    internal bool AddItem(Item item)
    {
      foreach (Item bagitem in Items.Where(x => x.Name == item.Name))
      {
        if (bagitem.Quantity >= bagitem.StackSize) continue;
        bagitem.Quantity++;
        return true;
      }

      if (Items.Count >= Capacity) return false;
      Items.Add(item);
      return true;
    }

    internal bool RemoveItem(Item item)
    {
      // lambda expression for all items in the bag that match the item.name
      // if the item is in the bag, decrement the count.  if the count drops to 0, remove the item from the bag
      foreach (Item bagitem in Items.Where(x => x.Name == item.Name))
      {
        if (bagitem.Quantity > 1)
        {
          bagitem.Quantity--;
          return true;
        }
        else
        {
          Items.Remove(bagitem);
          return true;
        }
      }
      return false;
    }

    internal int GetQuantity(Item item)
    {
      int quanity = 0;
      foreach (Item bagitem in Items)
      {
        if (bagitem.Name == item.Name)
        {
          quanity += bagitem.Quantity;
        }
      }
      return quanity;
    }
  }
}
