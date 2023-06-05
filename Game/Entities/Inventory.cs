using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleDungeonCrawler.Game.Entities.Items;

namespace ConsoleDungeonCrawler.Game.Entities
{
  internal class Inventory
  {
    internal static Dictionary<ItemType, int> Items = new Dictionary<ItemType, int>();

    public Inventory() { }
    public Inventory(Inventory inventory) { }

    public Inventory(Inventory inventory, string name)
    {

    }

    public void AddItem(ItemType itemType, int quantity)
    {
      if (Items.ContainsKey(itemType))
      {
        Items[itemType] += quantity;
      }
      else
      {
        Items.Add(itemType, quantity);
      }
    }

    public void RemoveItem(ItemType itemType, int quantity)
    {
      if (Items.ContainsKey(itemType))
      {
        Items[itemType] -= quantity;
        if (Items[itemType] <= 0)
        {
          Items.Remove(itemType);
        }
      }
    }

    public void RemoveAllItems(ItemType itemType)
    {
      if (Items.ContainsKey(itemType))
      {
        Items.Remove(itemType);
      }
    }

    public void RemoveAllItems()
    {
      Items.Clear();
    }

    public int GetQuantity(ItemType itemType)
    {
      if (Items.ContainsKey(itemType))
      {
        return Items[itemType];
      }
      else
      {
        return 0;
      }
    }

    public bool HasItem(ItemType itemType)
    {
      return Items.ContainsKey(itemType);
    }

    public bool HasItem(ItemType itemType, int quantity)
    {
      if (Items.ContainsKey(itemType))
      {
        return Items[itemType] >= quantity;
      }
      else
      {
        return false;
      }
    }

    public bool HasItems(Dictionary<ItemType, int> items)
    {
      foreach (KeyValuePair<ItemType, int> item in items)
      {
        if (!HasItem(item.Key, item.Value))
        {
          return false;
        }
      }
      return true;
    }

    // Create a random item and instantiate the associated class based on the ItemType return the item
    public Item GetRandomItem()
    {
      int itemIdx = Dice.Roll(1, Enum.GetNames(typeof(ItemType)).Length);

      Item item = new Item((ItemType)itemIdx, 1, 0, 0);
      switch (item.Type)
      {
        case ItemType.Weapon:
          item = Weapon.GetRandomWeapon();
          break;
        case ItemType.Potion:
          item = Potion.GetRandomPotion();
          break;
        case ItemType.Food:
          item = Food.GetRandomFood();
          break;
        case ItemType.Gold:
          item.Quantity = new Random().Next(0, 5);
          item.Value = new Random().Next(0, 5);
          break;
        case ItemType.Armor:
          item = Armor.GetRandomArmor();
          break;
        case ItemType.Chest:
          item = Chest.GetRandomChest();
          break;
        case ItemType.Bandage:
          item = Bandage.GetRandomBandage();
          break;
      }
      return item;
    }
  }
}
