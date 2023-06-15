using System.Drawing;
using ConsoleDungeonCrawler.Game.Maps;
using ConsoleDungeonCrawler.Game.Screens;

namespace ConsoleDungeonCrawler.Game.Entities.Items
{
  internal class Bandage : Item
  {
    BandageType BandageType = BandageType.Cloth;
    internal int BuffAmount = 1;

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

    private void SetLevelFromType()
    {
      switch (BandageType)
      {
        case BandageType.Cloth:
          Level = 1;
          break;
        case BandageType.Linen:
          Level = 10;
          break;
        case BandageType.Wool:
          Level = 20;
          break;
        case BandageType.Silk:
          Level = 30;
          break;
        case BandageType.RuneCloth:
          Level = 40;
          break;

      }
    }

    internal override bool Use()
    {
      if (Player.Health == Player.MaxHealth)
      {
        GamePlay.Messages.Add(new Message("You are already at full health.", Color.Orange, Color.Black));
        return false;
      }
      else
      {
        Player.Health += BuffAmount;
        GamePlay.Messages.Add(new Message($"You were healed for {BuffAmount} health.", Color.Orange, Color.Black));
        if (Player.Health > Player.MaxHealth)
        {
          Player.Health = Player.MaxHealth;
        }
        Quantity--;
        if (Quantity > 0) return true;
        GamePlay.Messages.Add(new Message($"You used your last {Name}.", Color.Orange, Color.Black));
        Inventory.RemoveItem(this);
        return true;
      }
    }

    internal new static Item GetRandomItem()
    {
      int randomBandage = Dice.Roll(0, Inventory.Bandages.Count - 1);
      return Inventory.Bandages[randomBandage];
    }
  }
}
