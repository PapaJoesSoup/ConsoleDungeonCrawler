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

    internal static List<Food> Foods = new List<Food>();

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

    internal static void InitFoodTypes()
    {
      Foods.Add(new Food(FoodType.Vegetable, BuffType.Health, 1, 1, 0));
      Foods.Add(new Food(FoodType.Fruit, BuffType.Health, 1, 1, 0));
      Foods.Add(new Food(FoodType.BearSteak, BuffType.Health, 1, 1, 0));
      Foods.Add(new Food(FoodType.Bread, BuffType.Health, 1, 1, 0));
      Foods.Add(new Food(FoodType.WolfSteak, BuffType.Health, 1, 1, 0));
      Foods.Add(new Food(FoodType.DeerSteak, BuffType.Health, 1, 1, 0));
      Foods.Add(new Food(FoodType.BoarChop, BuffType.Health, 1, 1, 0));
      Foods.Add(new Food(FoodType.Salmon, BuffType.HealthAndMana, 1, 1, 0));
      Foods.Add(new Food(FoodType.Trout, BuffType.HealthAndMana, 1, 1, 0));
      Foods.Add(new Food(FoodType.Snapper, BuffType.HealthAndMana, 1, 1, 0));
      Foods.Add(new Food(FoodType.MelonJuice, BuffType.Mana, 1, 1, 0));
      Foods.Add(new Food(FoodType.FruitJuice, BuffType.Mana, 1, 1, 0));
      Foods.Add(new Food(FoodType.Water, BuffType.Mana, 1, 1, 0));
      Foods.Add(new Food(FoodType.Tea, BuffType.Mana, 1, 1, 0));
      Foods.Add(new Food(FoodType.Coffee, BuffType.Mana, 1, 1, 0));
      Foods.Add(new Food(FoodType.Milk, BuffType.Mana, 1, 1, 0));
      Foods.Add(new Food(FoodType.Wine, BuffType.Mana, 1, 1, 0));
      Foods.Add(new Food(FoodType.Beer, BuffType.Mana, 1, 1, 0));
      Foods.Add(new Food(FoodType.Ale, BuffType.Mana, 1, 1, 0));
      Foods.Add(new Food(FoodType.Whiskey, BuffType.Mana, 1, 1, 0));
      Foods.Add(new Food(FoodType.Cider, BuffType.Mana, 1, 1, 0));
    }

    internal static Food GetRandomFood()
    {
      Random random = new Random();
      int randomFood = random.Next(0, Foods.Count);
      return Foods[randomFood];
    }

  }
}
