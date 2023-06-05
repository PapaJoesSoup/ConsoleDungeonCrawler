using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Game.Entities.Items
{
  internal class Chest : Item
  {

    internal List<Item> Items = new List<Item>();

    internal Chest()
    {

    }

    internal Chest(int quantity, decimal cost, decimal value)
    {
      Type = ItemType.Chest;
      Quantity = quantity;
      Name = "Chest";
      Description = "A Chest";
      Cost = cost;
      Value = value;
    }

    internal static Chest GetRandomChest()
    {
      Random random = new Random();
      int randomChest = random.Next(0, 3);
      switch (randomChest)
      {
        case 0:
          return new Chest(1, 1, 0);
        case 1:
          return new Chest(1, 1, 0);
        case 2:
          return new Chest(1, 1, 0);
        default:
          return new Chest(1, 1, 0);
      }
    }

    private List<Item> GetRandomItems()
    {
      Random random = new Random();
      int randomItems = random.Next(0, 15);
      Item randomGold = new Item(ItemType.Gold, random.Next(0, 10), 0, random.Next(0, 10));
      switch (randomItems)
      {
        case 0:
          return new List<Item>() { Armor.GetRandomArmor(), Potion.GetRandomPotion(), randomGold };
        case 1:
          return new List<Item>() { Bandage.GetRandomBandage(), randomGold };
        case 2:
          return new List<Item>() { Weapon.GetRandomWeapon(),Potion.GetRandomPotion(), Bandage.GetRandomBandage(), randomGold };
        case 3:
          return new List<Item>() { Food.GetRandomFood(), Food.GetRandomFood(), Potion.GetRandomPotion(), Bandage.GetRandomBandage(), randomGold };
        case 4:
          return new List<Item>() { Armor.GetRandomArmor(), Bandage.GetRandomBandage(), randomGold };
        case 5:
          return new List<Item>() { Potion.GetRandomPotion(), Food.GetRandomFood(), randomGold };
        case 6:
          return new List<Item>() { Armor.GetRandomArmor(), Weapon.GetRandomWeapon(), randomGold };
        case 7:
          return new List<Item>() { Armor.GetRandomArmor(), Weapon.GetRandomWeapon(), Potion.GetRandomPotion(), Bandage.GetRandomBandage(), randomGold };
        case 8:
          return new List<Item>() { Food.GetRandomFood(), Potion.GetRandomPotion(), Potion.GetRandomPotion(), randomGold };
        case 9:
          return new List<Item>() { Potion.GetRandomPotion(), Bandage.GetRandomBandage(), randomGold };
        case 10:
          return new List<Item>() { Potion.GetRandomPotion(), Armor.GetRandomArmor(), randomGold };
        case 11:
          return new List<Item>() { Potion.GetRandomPotion(), Bandage.GetRandomBandage(), randomGold };
        case 12:
          return new List<Item>() { Potion.GetRandomPotion(), Bandage.GetRandomBandage(), randomGold };
        case 13:
          return new List<Item>() { Potion.GetRandomPotion(), Bandage.GetRandomBandage(), randomGold };
        case 14:
          return new List<Item>() { Potion.GetRandomPotion(), Bandage.GetRandomBandage(), randomGold };
        case 15:
          return new List<Item>() { Potion.GetRandomPotion(), Bandage.GetRandomBandage(), randomGold };
        default:
          return new List<Item>() { Potion.GetRandomPotion(), randomGold };
      }
    }

  }
}
