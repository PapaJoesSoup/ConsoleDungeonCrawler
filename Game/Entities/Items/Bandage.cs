namespace ConsoleDungeonCrawler.Game.Entities.Items
{
  internal class Bandage : Item
  {
    BandageType BandageType = BandageType.Cloth;
    internal int BuffAmount = 1;

    internal Bandage()
    {

    }

    internal Bandage(BandageType bandageType, int buffAmount, int quantity, decimal buyCost, decimal sellCost)
    {
      Type = ItemType.Bandage;
      Quantity = quantity;
      StackSize = 20;
      Name = $"{bandageType} Bandage";
      Description = $"A {bandageType} Bandage";
      BuyCost = buyCost;
      SellCost = sellCost;
      BandageType = bandageType;
      BuffAmount = buffAmount;
    }

    internal static Bandage GetRandomBandage()
    {
      int randomBandage = Dice.Roll(0, Inventory.Bandages.Count);
      return Inventory.Bandages[randomBandage];
    }

    private void SetLevelFromType()
    {
      switch (BandageType)
      {
        case BandageType.Cloth:
          Level = 1;
          break;
        case BandageType.Linen:
          Level = 10;
          break;
        case BandageType.Wool:
          Level = 20;
          break;
        case BandageType.Silk:
          Level = 30;
          break;
        case BandageType.RuneCloth:
          Level = 40;
          break;

      }
    }
  }
}
