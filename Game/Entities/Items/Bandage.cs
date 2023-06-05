using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleDungeonCrawler.Game.Maps;

namespace ConsoleDungeonCrawler.Game.Entities.Items
{
  internal class Bandage : Item
  {
    BandageType BandageType = BandageType.Cloth;
    int BuffAmount = 1;

    internal Bandage()
    {

    }

    internal Bandage(BandageType bandageType, int buffAmount, int quantity, decimal cost, decimal value)
    {
      Type = ItemType.Bandage;

      Quantity = quantity;
      Name = $"{bandageType} Bandage";
      Description = $"A {bandageType} Bandage";
      Cost = cost;
      Value = value;
      BandageType = bandageType;
      BuffAmount = buffAmount;
    }

    internal static Bandage GetRandomBandage()
    {
      Random random = new Random();
      int randomBandage = random.Next(0, Inventory.Bandages.Count);
      return Inventory.Bandages[randomBandage];
    }

    internal void Use()
    {
      Player.Health += BuffAmount;
      Inventory.RemoveItem(this);
    }
  }
}
