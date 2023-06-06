using System.Drawing;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities.Items;
using ConsoleDungeonCrawler.Game.Maps;
using ConsoleDungeonCrawler.Game.Screens;

namespace ConsoleDungeonCrawler.Game.Entities
{
  internal class Monster : MapObject
  {
    internal int Health = 10;
    internal int MaxHealth = 10;
    internal int Mana = 0;
    internal int MaxMana = 0;
    internal int Level = 1;
    internal decimal Gold = (decimal)0.00;
    internal Weapon Weapon = new Weapon();
    internal Spell Spell = new Spell();
    internal bool IsAlive = true;
    internal bool InCombat = false;

    internal Monster()
    {

    }

    internal Monster(MapObject obj, int level)
    {
      // set Base MapObject properties
      X = obj.X;
      Y = obj.Y;
      Type = obj.Type;
      ForegroundColor = Type.ForegroundColor;
      BackgroundColor = Type.BackgroundColor;
      IsVisible = obj.IsVisible;

      Health = level * 2;
      MaxHealth = level * 2;
      Mana = level * 2;
      MaxMana = level * 2;
      Level = level * 2;
      Gold = level * 2;
      Weapon = new Weapon();
      Level = level;
    }

    private void InitInventory()
    {
      // get a random number and check to see if any inventory items should be added
      // 1 in 3 chance of adding an item
      if (Dice.Roll(1, 3) != 1) return;
      // now randomly select an item to add
      int item = Dice.Roll(1, Enum.GetNames<ItemType>().Length);
      Gold = (decimal)Dice.Roll(Level * 200)/100;

    }

    internal void DetectPlayer()
    {
      if (InCombat) return;
      // check if player is within radius of 3 tiles
      if (Map.Player.X < X - 3 || Map.Player.X > X + 3 ||
          Map.Player.Y < Y - 3 || Map.Player.Y > Y + 3) return;
      InCombat = true;
      Player.InCombat = true;
      BackgroundColor = Color.DarkOrange;
      GamePlayScreen.Messages.Add(new Message("You are in combat!", Color.Red, Color.Black));
    }

    internal void Attack()
    {
      if (!InCombat) return;
      // check if player is within radius of Weapon range
      if (Map.Player.X >= X - Weapon.Range && Map.Player.X <= X + Weapon.Range &&
          Map.Player.Y >= Y - Weapon.Range && Map.Player.Y <= Y + Weapon.Range)
      {
        // roll to hit
        if (Dice.Roll(1, 20) >= 10)
        {
          // roll for damage
          int damage = Dice.Roll(Weapon.Damage);
          Map.Player.TakeDamage(damage);
        }
      }
    }

    internal void TakeDamage(int damage)
    {
      if (damage <= 0)
      {
        GamePlayScreen.Messages.Add(new Message($"You missed the {Type.Name}!", Color.DarkOrange, Color.Black));
        GamePlayScreen.Messages.Add(
          new Message($"{Type.Name} has {Health} health left!", Color.DarkOrange, Color.Black));
      }
      else
      {
        GamePlayScreen.Messages.Add(new Message($"You hit the {Type.Name} for {damage} damage!", Color.DarkOrange, Color.Black));
        Health -= damage;
        if (Health <= 0)
        {
          Health = 0;
          IsAlive = false;
          int xp = Dice.Roll(Level);
          GamePlayScreen.Messages.Add(new Message($"You killed the {Type.Name}!", Color.DarkOrange, Color.Black));
          GamePlayScreen.Messages.Add(new Message($"You gained {Gold} gold!", Color.DarkOrange, Color.Black));
          Player.Gold += Gold;
          Random Random = new Random();
          if (Random.Next(1, 5) == 1)
          {
            Item item = Inventory.GetRandomItem();
            Inventory.AddItem(item);
            GamePlayScreen.Messages.Add(new Message($"You gained a {item.Type}!", Color.DarkOrange, Color.Black));
          }
          GamePlayScreen.Messages.Add(new Message($"You gained {xp} experience!", Color.DarkOrange, Color.Black));
          Player.Experience += xp;
          BackgroundColor = Type.BackgroundColor;
          ForegroundColor = Color.Gray;
          Type.IsPassable = true;
          InCombat = false;
          Map.RemoveFromOverlayObjects(this);
          Player.InCombat = Player.IsInCombat();
        }
        else
          GamePlayScreen.Messages.Add(
            new Message($"{Type.Name} has {Health} health left!", Color.DarkOrange, Color.Black));
      }
    }
  }
}
