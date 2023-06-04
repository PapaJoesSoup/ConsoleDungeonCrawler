using System.Drawing;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Maps;
using ConsoleDungeonCrawler.Game.Screens;

namespace ConsoleDungeonCrawler.Game.Entities
{
  internal class Monster
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
    internal MapObject MapObj = new MapObject();
    internal bool IsAlive = true;
    internal bool InCombat = false;


    internal Monster()
    {

    }

    internal Monster(MapObject obj , int level)
    {
      MapObj = obj;
      Level = level;
    }

    internal void Draw()
    {
      if (!MapObj.Visible || MapObj.Type.Symbol == ' ') return;
      ConsoleEx.WriteAt(MapObj.Type.Symbol, MapObj.X + Map.Left, MapObj.Y + Map.Top, MapObj.Type.ForegroundColor, MapObj.Type.BackgroundColor);
    }

    internal void Attack()
    {
      if (InCombat)
      {
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
}
