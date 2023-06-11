using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities.Items;
using ConsoleDungeonCrawler.Game.Maps;
using ConsoleDungeonCrawler.Game.Screens;
using System.Drawing;

namespace ConsoleDungeonCrawler.Game.Entities
{
  internal class Player : MapObject
  {
    internal static int Level = 1;
    internal static int Experience = 0;
    internal static int ExperienceToLevel = 100;
    internal static PlayerClass Class = PlayerClass.Rogue;
    internal static int Health = 100;
    internal static int MaxHealth = 100;
    internal static int Mana = 0;
    internal static int MaxMana = 0;
    internal static decimal Gold = 0;

    internal static List<Armor> ArmorSet = new List<Armor>();
    internal static Weapon Weapon = new Weapon();
    internal static Dictionary<int, Spell> Spells = new Dictionary<int, Spell>();
    internal static bool IsAlive = true;
    internal static bool InCombat = false;

    internal Player()
    {
    }

    internal Player(MapObject mapObject)
    {
      // Set Player's MapObject base to the one passed in
      X = mapObject.X;
      Y = mapObject.Y;
      Type = mapObject.Type;
      ForegroundColor = Type.ForegroundColor;
      BackgroundColor = Type.BackgroundColor;
      IsVisible = mapObject.IsVisible;

      // Add 5 empty slots to armor set
      ArmorSet = new List<Armor>
      {
        new Armor(ArmorType.Head),
        new Armor(ArmorType.Body),
        new Armor(ArmorType.Hands),
        new Armor(ArmorType.Legs),
        new Armor(ArmorType.Feet)
      };

      // Add 5 initial slots to inventory
      Inventory.Bags.Add(new Bag());
      Inventory.AddItem( new Potion(BuffType.Health, 1, 1, 0));
      Inventory.AddItem(new Potion(BuffType.Health, 1, 1, 0));
      Inventory.AddItem( new Food(FoodType.Bread, BuffType.Health, 1, 1, 0));
      Inventory.AddItem( new Food(FoodType.Vegetable, BuffType.Health, 1, 1, 0));
    }

    public void Move(ConsoleKey key)
    {
      int x = 0;
      int y = 0;
      if (key == ConsoleKey.W) { x = 0; y = -1; }
      if (key == ConsoleKey.A) { x = -1; y = 0; }
      if (key == ConsoleKey.S) { x = 0; y = 1; }
      if (key == ConsoleKey.D) { x = 1; y = 0; }

      Position oldPos = new Position(X, Y);
      Position newPos = new Position(X + x, Y + y);

      if (!Map.CanMoveTo(newPos.X, newPos.Y)) return;
      X = newPos.X;
      Y = newPos.Y;
      Map.OverlayObjects['P'][0] = this;
      Map.MapGrid[oldPos.X][oldPos.Y].Draw();
      Map.MapGrid[newPos.X][newPos.Y].Draw();
      Map.OverlayGrid[oldPos.X][oldPos.Y].Draw();
      Map.OverlayGrid[newPos.X][newPos.Y].Draw();
    }

    public void Jump(ConsoleKey key)
    {
      int x = 0;
      int y = 0;
      if (key == ConsoleKey.W) { x = 0; y = -2; }
      if (key == ConsoleKey.A) { x = -2; y = 0; }
      if (key == ConsoleKey.S) { x = 0; y = 2; }
      if (key == ConsoleKey.D) { x = 2; y = 0; }

      Position oldPos = new Position(X, Y);
      Position newPos = new Position(X + x, Y + y);

      if (!Map.CanJumpTo(oldPos.X, oldPos.Y, newPos.X, newPos.Y)) return;
      X = newPos.X;
      Y = newPos.Y;
      Map.OverlayObjects['P'][0] = this;
      Map.MapGrid[oldPos.X][oldPos.Y].Draw();
      Map.MapGrid[newPos.X][newPos.Y].Draw();
      Map.OverlayGrid[oldPos.X][oldPos.Y].Draw();
      Map.OverlayGrid[newPos.X][newPos.Y].Draw();
    }

    public void Attack()
    {
      if (!InCombat) return;
      switch (Weapon.WeaponType)
      {
        case WeaponType.Fists:
        case WeaponType.Sword:
        case WeaponType.Axe:
        case WeaponType.Mace:
        case WeaponType.Dagger:
        case WeaponType.Staff:
          if (IsNextToOverlay(out MapObject objM) == ' ') return;
          Monster meelee = (Monster)objM;
          GamePlay.Messages.Add(new Message($"You swing your {Weapon.WeaponType} at the {meelee.Type.Name}!"));
          meelee.TakeDamage(Weapon.Damage);
          break;
        case WeaponType.Bow:
        case WeaponType.Wand:
          if (IsInRange(Weapon.Range, out MapObject objR)) return;
          Monster ranged = (Monster)objR;
          GamePlay.Messages.Add(new Message($"You shoot your {Weapon.WeaponType} at the {ranged.Type.Name}!"));
          ranged.TakeDamage(Weapon.Damage);
          break;
      }
    }

    internal static void EquipArmor(Armor armor)
    {
      switch (armor.ArmorType)
      {
        case ArmorType.Head:
          if (ArmorSet[0].ArmorType != ArmorType.None)
            Inventory.AddItem(ArmorSet[0]); 
          ArmorSet[0] = armor;
          break;
        case ArmorType.Body:
          if (ArmorSet[1].ArmorName != ArmorName.None)
            Inventory.AddItem(ArmorSet[1]);
          ArmorSet[1] = armor;
          break;
        case ArmorType.Hands:
          if (ArmorSet[2].ArmorName != ArmorName.None)
            Inventory.AddItem(ArmorSet[2]);
          ArmorSet[2] = armor;
          break;
        case ArmorType.Legs:
          if (ArmorSet[3].ArmorName != ArmorName.None)
            Inventory.AddItem(ArmorSet[3]);
          ArmorSet[3] = armor;
          break;
        case ArmorType.Feet:
          if (ArmorSet[4].ArmorName != ArmorName.None)
            Inventory.AddItem(ArmorSet[4]);
          ArmorSet[4] = armor;
          break;
      }
    }

    internal static void EquipWeapon(Weapon weapon)
    {

      Inventory.AddItem(Player.Weapon);
      Weapon = weapon;
    }

    internal static void EquipSpell(Spell spell)
    {

    }

    internal static void UseSpell(Spell spell)
    {

    }

    internal static void TakeDamage(int damage)
    {
      if (damage <= 0)
      {
        GamePlay.Messages.Add(new Message($"Monster Missed you!", Color.DarkOrange, Color.Black));
        GamePlay.Messages.Add(
          new Message($"You have {Player.Health} health left!", Color.DarkOrange, Color.Black));
      }

      ;
      GamePlay.Messages.Add(new Message($"You were hit for {damage} damage!", Color.DarkOrange, Color.Black));
      Player.Health -= damage;
      if (Player.Health <= 0)
      {
        Player.Health = 0;
        GamePlay.Messages.Add(new Message("You died!", Color.Red, Color.Black));
        Player.InCombat = false;
        Player.IsAlive = false;
      }
      else
      {
        GamePlay.Messages.Add(
          new Message($"You have {Player.Health} health left!", Color.DarkOrange, Color.Black));
      }
    }

    internal static void Heal(int amount)
    {
      Health += amount;
      if (Health > MaxHealth) Health = MaxHealth;
    }

    internal static void RestoreMana(int amount)
    {
      Mana += amount;
      if (Mana > MaxMana) Mana = MaxMana;
    }

    internal static bool RemoveGold(decimal amount)
    {
      if (amount > Gold)
      {
        GamePlay.Messages.Add(new Message("You don't have enough gold!", Color.Red, Color.Black));
        return false;
      }
      Gold -= amount;
      return true;
    }

    internal static void LevelUp()
    {
      Level++;
      ExperienceToLevel = (int)(ExperienceToLevel * 1.5);
      MaxHealth += 10;
      if(MaxMana > 0) MaxMana += 5;
      Health = MaxHealth;
      Mana = MaxMana;
      GamePlay.Messages.Add(new Message($"You are now level {Level}!", Color.Green, Color.Black));
    }

    internal static void AddExperience(int amount)
    {
      Experience += amount;
      if (Experience >= ExperienceToLevel)
      {
        Experience -= ExperienceToLevel;
        LevelUp();
      }
    }

    internal static bool IsInCombat()
    {
      // Check if there is a mapObject.InCombat == true in OverlayObjects Except Player
      foreach (char key in Map.OverlayObjects.Keys)
      {
        if (Map.OverlayObjects[key].Count == 0) continue;
        List<MapObject> objs = Map.OverlayObjects[key];
        if (!objs[0].Type.IsAttackable) continue;
        foreach (MapObject obj in objs)
        {
          if (obj is Player) continue;
          if (obj is Monster == false) continue;
          if (((Monster)obj).InCombat) return true;
        }
      }
      return false;
    }

    internal char IsNextToOverlay(out MapObject obj)
    {
      // look left
      if (X > 0 && Map.OverlayGrid[X - 1][Y].Type.Symbol != ' ')
      {
        obj = Map.OverlayGrid[X - 1][Y];
        return obj.Type.Symbol;
      }

      // look right
      if (X < GamePlay.MapBox.Width && Map.OverlayGrid[X + 1][Y].Type.Symbol != ' ')
      {
        obj = Map.OverlayGrid[X + 1][Y];
        return obj.Type.Symbol;
      }

      // look up
      if (Y > 0 && Map.OverlayGrid[X][Y - 1].Type.Symbol != ' ')
      {
        obj = Map.OverlayGrid[X][Y - 1];
        return obj.Type.Symbol;
      }

      // look down
      if (Y >= GamePlay.MapBox.Height || Map.OverlayGrid[X][Y + 1].Type.Symbol != ' ')
      {
        obj = Map.OverlayGrid[X][Y + 1];
        return obj.Type.Symbol;
      }

      // not found
      obj = new MapObject();
      return ' ';
    }

    internal bool IsNextToOverlay(char symbol, out MapObject obj)
    {
      // look left
      if (X > 0 && Map.OverlayGrid[X - 1][Y].Type.Symbol == symbol)
      {
        obj = Map.OverlayGrid[X - 1][Y];
        return true;
      }

      // look right
      if (X < GamePlay.MapBox.Width && Map.OverlayGrid[X + 1][Y].Type.Symbol == symbol)
      {
        obj = Map.OverlayGrid[X + 1][Y];
        return true;
      }

      // look up
      if (Y > 0 && Map.OverlayGrid[X][Y - 1].Type.Symbol == symbol)
      {
        obj = Map.OverlayGrid[X][Y - 1];
        return true;
      }

      // look down
      if (Y >= GamePlay.MapBox.Height || Map.OverlayGrid[X][Y + 1].Type.Symbol == symbol)
      {
        obj = Map.OverlayGrid[X][Y + 1];
        return true;
      }

      // not found
      obj = new MapObject();
      return false;
    }

    internal bool IsNextToMap(char symbol, out MapObject obj)
    {
      // look left
      if (X > 0 && Map.MapGrid[X - 1][Y].Type.Symbol == symbol)
      {
        obj = Map.MapGrid[X - 1][Y];
        return true;
      }

      // look right
      if (X < GamePlay.MapBox.Width && Map.MapGrid[X + 1][Y].Type.Symbol == symbol)
      {
        obj = Map.MapGrid[X + 1][Y];
        return true;
      }

      // look up
      if (Y > 0 && Map.MapGrid[X][Y - 1].Type.Symbol == symbol)
      {
        obj = Map.MapGrid[X][Y - 1];
        return true;
      }

      // look down
      if (Y >= GamePlay.MapBox.Height || Map.MapGrid[X][Y + 1].Type.Symbol == symbol)
      {
        obj = Map.MapGrid[X][Y + 1];
        return true;
      }

      // not found
      obj = new MapObject();
      return false;
    }

    internal bool IsInRange(int radius, out MapObject obj)
    {
      // find the closest object within the radius
      for (int i = 0; i < radius; i++)
      {
        // look left
        if (X > 0 && Map.MapGrid[X - 1][Y].Type.Symbol != ' ')
        {
          obj = Map.MapGrid[X - 1][Y];
          return true;
        }

        // look right
        if (X < GamePlay.MapBox.Width && Map.MapGrid[X + 1][Y].Type.Symbol != ' ')
        {
          obj = Map.MapGrid[X + 1][Y];
          return true;
        }

        // look up
        if (Y > 0 && Map.MapGrid[X][Y - 1].Type.Symbol != ' ')
        {
          obj = Map.MapGrid[X][Y - 1];
          return true;
        }

        // look down
        if (Y >= GamePlay.MapBox.Height || Map.MapGrid[X][Y + 1].Type.Symbol != ' ')
        {
          obj = Map.MapGrid[X][Y + 1];
          return true;
        }
      }

      // not found
      obj = new MapObject();
      return false;
    }
  }
}
