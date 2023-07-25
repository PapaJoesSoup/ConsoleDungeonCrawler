using ConsoleDungeonCrawler.Game.Screens;
using System.Drawing;

namespace ConsoleDungeonCrawler.Game.Entities.Items;

internal class Potion : Item
{
  private readonly BuffType buffType = BuffType.Health;
  private readonly int buffAmount = 1;

  internal Potion()
  {
  }

  internal Potion(BuffType potionType, ItemRarity rarity, int quantity, decimal buyCost, decimal sellCost)
  {
    Type = ItemType.Potion;
    Rarity = rarity;
    Quantity = quantity;
    StackSize = 20;
    BuyCost = buyCost;
    SellCost = sellCost;

    buffType = potionType;
    buffAmount = (int)rarity * 10;
    Name = $"{rarity} {potionType} Potion";
    Description = $"A {rarity} {potionType} Potion";
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
      case BuffType.ManaHeal:
        Player.Heal(buffAmount);
        Player.RestoreMana(buffAmount);
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
        return new Potion(BuffType.Health, ItemRarity.Common, 1, 1, 0.1M);
      case 1:
        return new Potion(BuffType.Mana, ItemRarity.Common, 1, 1, 0.1M);
      case 2:
        return new Potion(BuffType.ManaHeal, ItemRarity.Common, 1, 1, 0.1M);
      default:
        return new Potion(BuffType.Health, ItemRarity.Common, 1, 1, 0.1M);
    }
  }
}