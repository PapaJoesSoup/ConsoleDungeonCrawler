using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    internal Food(FoodType foodType, BuffType bufftype, int quantity, decimal cost, decimal value)
    {
      Type = ItemType.Food;
      FoodType = foodType;
      BuffType = bufftype;
      Quantity = quantity;
      Name = $"{FoodType} Food";
      Description = $"A {FoodType} Food";
      Cost = cost;
      Value = 0;
    }

    internal static Food GetRandomFood()
    {
      Random random = new Random();
      int randomFood = random.Next(0, Inventory.Foods.Count);
      return Inventory.Foods[randomFood];
    }

  }
}
