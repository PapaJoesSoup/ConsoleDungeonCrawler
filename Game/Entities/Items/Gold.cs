namespace ConsoleDungeonCrawler.Game.Entities.Items
{
  internal class Gold : Item
  {
    internal Gold()
    {
      Type = ItemType.Gold;
      Name = "Gold";
      Description = "some Gold";
      Level = 1;
      Quantity = 1;
      BuyCost = 0.1M;
      SellCost = 0.1M;
    }

    internal Gold(int level, int qty )
    {
      Type = ItemType.Gold;
      Name = "Gold";
      Description = "some Gold";
      Level = level;
      Quantity = qty;
      BuyCost = 0.1M;
      SellCost = 0.1M;
    }

    internal override bool Use()
    {
      bool result = true;
      Player.Gold += GetValue();
      return result;
    }

    internal decimal GetValue()
    {
      return Decimal.Round(Level * Quantity * SellCost, 2);
    }

    internal new static Item GetRandomItem()
    {
      return new Gold(Dice.Roll(1, 10), Dice.Roll(1, 10));
    }
  }
}
