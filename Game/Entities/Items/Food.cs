using System.Drawing;
using ConsoleDungeonCrawler.Game.Screens;

namespace ConsoleDungeonCrawler.Game.Entities.Items;

internal class Food : Item
{
  private readonly BuffType buffType = BuffType.Health;
  private readonly int buffAmount = 1;

  internal Food()
  {

  }

  internal Food(FoodName foodName, BuffType buffType, int quantity, decimal buyCost, decimal sellCost)
  {
    Type = ItemType.Food;
    this.buffType = buffType;
    Quantity = quantity;
    StackSize = 20;

    Name = $"{foodName}";
    Description = this.buffType == BuffType.Health ? $"a {foodName}" : $"some {foodName}";
    BuyCost = buyCost;
    SellCost = sellCost;
  }

  internal override bool Use()
  {
    if (Player.Health == Player.MaxHealth)
    {
      GamePlay.Messages.Add(new Message("You are already at full health.", Color.Orange, Color.Black));
      return false;
    }
    switch (buffType)
    {
      case BuffType.Health:
        Player.Heal(buffAmount);
        break;
      case BuffType.Mana:
        Player.RestoreMana(buffAmount);
        break;
      case BuffType.HealthAndMana:
        Player.Heal(buffAmount);
        Player.RestoreMana(buffAmount);
        break;
    }
    Quantity--;
    GamePlay.Messages.Add(new Message(this.buffType == BuffType.Mana ? $"You drank {Description}." : $"You ate {Description}.", Color.Orange, Color.Black));
    if (Quantity > 0) return true;
    GamePlay.Messages.Add(new Message(this.buffType == BuffType.Mana ? $"You drank your last {Name}." : $"You ate your last {Name}.", Color.Orange, Color.Black));
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