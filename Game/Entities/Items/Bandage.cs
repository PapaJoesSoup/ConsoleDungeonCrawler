using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Game.Entities.Items
{
  internal class Bandage : Item
  {
    BandageType BandageType = BandageType.Cloth;
    BuffType BuffType = BuffType.Health;
    int BuffAmount = 1;

    internal static List<Bandage> Bandages = new List<Bandage>();

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

    internal static void InitBandages()
    {
      Bandages.Add(new Bandage(BandageType.Cloth, 1, 1, 1, 0));
      Bandages.Add(new Bandage(BandageType.Linen, 2, 1, 1, 0));
      Bandages.Add(new Bandage(BandageType.Wool, 3, 1, 1, 0));
      Bandages.Add(new Bandage(BandageType.Silk, 4, 1, 1, 0));
      Bandages.Add(new Bandage(BandageType.Cotton, 5, 1, 1, 0));
      Bandages.Add(new Bandage(BandageType.RuneCloth, 6, 1, 1, 0));
    }

    internal static Bandage GetRandomBandage()
    {
      Random random = new Random();
      int randomBandage = random.Next(0, Bandages.Count);
      return Bandages[randomBandage];
    }
  }
}
