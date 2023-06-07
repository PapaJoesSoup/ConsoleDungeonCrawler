namespace ConsoleDungeonCrawler.Game.Entities.Items
{
  internal class Potion : Item
  {
    internal BuffType BuffType = BuffType.Health;
    internal int BuffAmount = 1;


    internal Potion()
    {

    }

    internal Potion(BuffType potionType, int quantity, decimal buyCost, decimal sellCost)
    {
      Type = ItemType.Potion;
      Quantity = quantity;
      StackSize = 20;
      BuyCost = buyCost;
      SellCost = sellCost;

      Name = $"{BuffType} Potion";
      Description = $"A {BuffType} Potion";
      BuffType = potionType;
    }

    internal static Potion GetRandomPotion()
    {
      int randomPotion = Dice.Roll(0, 2);
      switch (randomPotion)
      {
        case 0:
          return new Potion(BuffType.Health, 1, 1, 0.2M);
        case 1:
          return new Potion(BuffType.Mana, 1, 1, 0.3M);
        case 2:
          return new Potion(BuffType.HealthAndMana, 1, 1, 0.5M);
        default:
          return new Potion(BuffType.Health, 1, 1, 0.2M);
      }
    }
  }
}
