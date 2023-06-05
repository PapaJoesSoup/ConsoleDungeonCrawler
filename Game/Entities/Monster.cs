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
    internal decimal Gold = 0;
    internal Weapon Weapon = new Weapon();
    internal Spell Spell = new Spell();
    internal List<Item> Inventory = new List<Item>();
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

    internal void Detect()
    {
      if (InCombat) return;
      // check if player is within radius of 3 tiles
      if (Map.Player.X >= X - 3 && Map.Player.X <= X + 3 &&
          Map.Player.Y >= Y - 3 && Map.Player.Y <= Y + 3)
      {
        InCombat = true;
        BackgroundColor = Color.DarkOrange;
        GamePlayScreen.Messages.Add(new Message("You are in combat!", Color.Red, Color.Black));
      }
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
          int gold = Dice.Roll(Level);
          GamePlayScreen.Messages.Add(new Message($"You killed the {Type.Name}!", Color.DarkOrange, Color.Black));
          GamePlayScreen.Messages.Add(new Message($"You gained {gold} gold!", Color.DarkOrange, Color.Black));
          Player.Gold += gold;
          BackgroundColor = Type.BackgroundColor;
          ForegroundColor = Color.Gray;
          Type.IsPassable = true;
          InCombat = false;
          Map.RemoveFromOverlayObjects(this);
        }
        else
          GamePlayScreen.Messages.Add(
            new Message($"{Type.Name} has {Health} health left!", Color.DarkOrange, Color.Black));
      }
    }
  }
}
