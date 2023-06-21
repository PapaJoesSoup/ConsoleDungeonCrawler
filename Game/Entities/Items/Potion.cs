using ConsoleDungeonCrawler.Game.Screens;
using System.Drawing;

namespace ConsoleDungeonCrawler.Game.Entities.Items
{
  internal class Potion : Item
  {
    internal readonly BuffType BuffType = BuffType.Health;
    internal readonly int BuffAmount = 1;


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
