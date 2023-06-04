using System.Drawing;
using ConsoleDungeonCrawler.Extensions;
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
    internal Color OriginalBackgroundColor = Color.Black;

    internal Monster()
    {

    }

    internal Monster(MapObject obj , int level)
    {
      // set Base MapObject properties
      X = obj.X;
      Y = obj.Y;
      Type = obj.Type;
      IsVisible = obj.IsVisible;
      OriginalBackgroundColor = obj.Type.BackgroundColor;

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
        this.Type.BackgroundColor = Color.HotPink;
        GamePlayScreen.Messages.Add(new Message("You are in combat!", Color.Red, Color.Black));
      }
    }

    internal void Attack()
    {
      if (!InCombat) return;
      if (Player.Health > 0)
      {
        Player.Health -= Weapon.Damage;
        GamePlayScreen.Messages.Add(new Message($"You were hit for {Weapon.Damage} damage!",
          Color.DarkOrange, Color.Black));
        GamePlayScreen.Messages.Add(new Message($"You have {Player.Health} health left!",
          Color.DarkOrange, Color.Black));
      }
      else
      {
        GamePlayScreen.Messages.Add(new Message("You died!", Color.Red, Color.Black));
        Game.IsOver = true;
      }
    }
  }
}
