namespace ConsoleDungeonCrawler.Game.Entities.Items
{
  internal class Food : Item
  {
    internal FoodType FoodType = FoodType.Vegetable;
    internal BuffType BuffType = BuffType.Health;
    internal int BuffAmount = 1;

    internal Food()
    {

    }

    internal Food(FoodType foodType, BuffType bufftype, int quantity, decimal buyCost, decimal value)
    {
      Type = ItemType.Food;
      FoodType = foodType;
      BuffType = bufftype;
      Quantity = quantity;
      StackSize = 20;

      Name = $"{FoodType} Food";
      Description = $"A {FoodType} Food";
      BuyCost = buyCost;
      SellCost = 0;
    }
    
    internal static Food GetRandomFood()
    {
      BuffType randomBuff = (BuffType)Dice.Roll(1, Inventory.Foods.Count); // 0 is None
      int randomFood = Dice.Roll(0, Inventory.Foods[randomBuff].Count);
      return Inventory.Foods[randomBuff][randomFood];
    }

  }
}
