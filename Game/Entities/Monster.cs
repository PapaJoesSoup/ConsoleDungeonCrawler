using System.Drawing;
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
      IsPassable = Type.IsPassable;
      IsAttackable = Type.IsAttackable;
      IsLootable = Type.IsLootable;

      // set Monster properties
      Health = level * 2;
      MaxHealth = level * 2;
      Mana = level * 2;
      MaxMana = level * 2;
      Level = level * 2;
      Gold = Decimal.Round(level * Dice.Roll(.01M, 1.1M), 2);
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
      Gold = (decimal)Dice.Roll(1, Level * 2);
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
      this.Draw();
      GamePlay.Messages.Add(new Message($"A {Type.Name} spots you!  You are in combat!", Color.Red, Color.Black));
    }

    internal void Attack()
    {
      if (!InCombat) return;
      // check if player is within radius of Weapon range
      if (Map.Player.X < X - Weapon.Range || Map.Player.X > X + Weapon.Range ||
          Map.Player.Y < Y - Weapon.Range || Map.Player.Y > Y + Weapon.Range) return;
      // roll to hit
      if (Dice.Roll(1, 20) < 10) return;
      // roll for damage
      int damage = Dice.Roll(Weapon.Damage);
      if (damage == 0)
        GamePlay.Messages.Add(new Message($"The {Type.Name} attacks and misses you!", Color.DarkOrange, Color.Black));
      else
      {
        GamePlay.Messages.Add(new Message($"The {Type.Name} attacks and hits you for {damage} damage!", Color.DarkOrange, Color.Black));
        Player.TakeDamage(damage);
      }
    }

    internal void TakeDamage(int damage)
    {
      if (damage <= 0)
      {
        GamePlay.Messages.Add(new Message($"You missed the {Type.Name}!", Color.DarkOrange, Color.Black));
        GamePlay.Messages.Add(new Message($"{Type.Name} has {Health} health left!", Color.DarkOrange, Color.Black));
      }
      else
      {
        GamePlay.Messages.Add(new Message($"You hit the {Type.Name} for {damage} damage!", Color.DarkOrange, Color.Black));
        Health -= damage;
        if (Health <= 0)
        {
          Health = 0;
          IsAlive = false;
          GamePlay.Messages.Add(new Message($"You killed the {Type.Name}!", Color.DarkOrange, Color.Black));
          int xp = Dice.Roll(Level *2 );
          GamePlay.Messages.Add(new Message($"You gained {xp} experience!", Color.DarkOrange, Color.Black));
          Player.AddExperience(xp);
          BackgroundColor = Type.BackgroundColor;
          ForegroundColor = Color.Gray;
          IsPassable = true;
          InCombat = false;
          this.Draw();
          Map.RemoveFromOverlayObjects(this);
          Player.InCombat = Player.IsInCombat();
        }
        else
          GamePlay.Messages.Add(new Message($"{Type.Name} has {Health} health left!", Color.DarkOrange, Color.Black));
      }
    }

    internal static int SetOdds(Char type)
    {
      switch (type)
      {
        case 'g':
        case 'z':
          return 5;
        case 'O':
          return 3;
        case 'B':
          return 1;
        default:
          return 5;
      }
    }

    internal static Item Loot(Monster monster)
    {
      if (Dice.Roll(SetOdds(monster.Type.Symbol)) != 1) return new Item(); //chance of dropping an item
      Item item = Inventory.GetRandomItem();
      Inventory.AddItem(item);
      GamePlay.Messages.Add(new Message($"You gained {item.Description}!", Color.DarkOrange, Color.Black));
      monster.IsLootable = false;
      return item;
    }
  }
}
