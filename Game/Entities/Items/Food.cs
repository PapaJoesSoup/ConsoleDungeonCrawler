﻿using System.Drawing;
using ConsoleDungeonCrawler.Game.Screens;

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

    internal override bool Use()
    {
      if (Player.Health == Player.MaxHealth)
      {
        GamePlay.Messages.Add(new Message("You are already at full health.", Color.Orange, Color.Black));
        return false;
      }
      switch (BuffType)
      {
        case BuffType.Health:
          Player.Heal(BuffAmount);
          break;
        case BuffType.Mana:
          Player.RestoreMana(BuffAmount);
          break;
        case BuffType.HealthAndMana:
          Player.Heal(BuffAmount);
          Player.RestoreMana(BuffAmount);
          break;
      }
      Quantity--;
      if (Quantity > 0) return true;
      GamePlay.Messages.Add(new Message($"You used your last {Name}.", Color.Orange, Color.Black));
      Inventory.RemoveItem(this);

      return true;
    }

    internal new static Item GetRandomItem()
    {
      BuffType randomBuff = (BuffType)Dice.Roll(1, Inventory.Foods.Count - 1); // 0 is None
      int randomFood = Dice.Roll(0, Inventory.Foods[randomBuff].Count - 1);
      return Inventory.Foods[randomBuff][randomFood];
    }
  }
}
