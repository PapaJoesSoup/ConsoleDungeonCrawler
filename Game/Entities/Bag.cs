namespace ConsoleDungeonCrawler.Game.Entities;

internal class Bag
{
  internal readonly int Capacity = 20;
  internal List<Item> Items;

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
    foreach (Item bagItem in Items.Where(x => x.Name == item.Name))
    {
      if (bagItem.Quantity >= bagItem.StackSize) continue;
      bagItem.Quantity++;
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
    foreach (Item bagItem in Items.Where(x => x.Name == item.Name))
    {
      if (bagItem.Quantity > 1)
      {
        bagItem.Quantity--;
        return true;
      }
      else
      {
        Items.Remove(bagItem);
        return true;
      }
    }
    return false;
  }

  internal int GetQuantity(Item item)
  {
    int quantity = 0;
    foreach (Item bagItem in Items)
    {
      if (bagItem.Name == item.Name)
      {
        quantity += bagItem.Quantity;
      }
    }
    return quantity;
  }
}