namespace ConsoleDungeonCrawler.Game.Entities.Items
{
  internal class Bandage : Item
  {
    BandageType BandageType = BandageType.Cloth;
    int BuffAmount = 1;

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

    internal void Use()
    {
      Player.Health += BuffAmount;
      Inventory.RemoveItem(this);
    }
  }
}
