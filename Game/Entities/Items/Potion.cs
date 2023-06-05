using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Game.Entities.Items
{
  internal class Potion : Item
  {
    internal BuffType BuffType = BuffType.Health;
    internal int BuffAmount = 1;


    internal Potion()
    {

    }

    internal Potion(BuffType potionType, int quantity, decimal cost, decimal value)
    {
      Type = ItemType.Potion;
      BuffType = potionType;
      Quantity = quantity;
      Name = $"{BuffType} Potion";
      Description = $"A {BuffType} Potion";
      Cost = cost;
      Value = value;
    }

    internal static Potion GetRandomPotion()
    {
      Random random = new Random();
      int randomPotion = random.Next(0, 3);
      switch (randomPotion)
      {
        case 0:
          return new Potion(BuffType.Health, 1, 1, 0);
        case 1:
          return new Potion(BuffType.Mana, 1, 1, 0);
        case 2:
          return new Potion(BuffType.HealthAndMana, 1, 1, 0);
        default:
          return new Potion(BuffType.Health, 1, 1, 0);
      }
    }
  }
}
