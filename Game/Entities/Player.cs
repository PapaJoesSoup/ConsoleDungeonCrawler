using ConsoleDungeonCrawler.Game.Entities.Items;
using ConsoleDungeonCrawler.Game.Screens;
using System.Drawing;

namespace ConsoleDungeonCrawler.Game.Entities;

internal class Player : MapObject
{
  internal static int Level = 1;
  internal static int Experience = 0;
  internal static int ExperienceToLevel = 100;
  internal static readonly PlayerClass Class = PlayerClass.Rogue;
  internal static int Health = 100;
  internal static int MaxHealth = 100;
  internal static int Mana = 0;
  internal static int MaxMana = 0;
  internal static decimal Gold = 0;

  internal static List<Armor> ArmorSet = new();
  internal static Weapon Weapon = new();
  internal static readonly Dictionary<int, Spell> Spells = new();
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
      new(ArmorType.Head),
      new(ArmorType.Body),
      new(ArmorType.Hands),
      new(ArmorType.Legs),
      new(ArmorType.Feet)
    };

    // Add a couple of bags and 5 initial slots to inventory
    Inventory.Bags.Add(new Bag());
    Inventory.Bags.Add(new Bag());
    Inventory.AddItem(new Potion(BuffType.Health, ItemRarity.Common, 1, 1, 0.1M));
    Inventory.AddItem(new Potion(BuffType.Health, ItemRarity.Common, 1, 1, 0.1M));
    Inventory.AddItem(new Food(FoodName.Bread, BuffType.Health, 1, 1, 0.1M));
    Inventory.AddItem(new Food(FoodName.Vegetable, BuffType.Health, 1, 1, 0.1M));
  }

  public bool Move(ConsoleKey key)
  {
    int x = 0;
    int y = 0;
    if (key == ConsoleKey.W) { x = 0; y = -1; }
    if (key == ConsoleKey.A) { x = -1; y = 0; }
    if (key == ConsoleKey.S) { x = 0; y = 1; }
    if (key == ConsoleKey.D) { x = 1; y = 0; }

    Position oldPos = new(X, Y);
    Position newPos = new(X + x, Y + y);

    if (!Map.CanMoveTo(newPos.X, newPos.Y)) return false;
    X = newPos.X;
    Y = newPos.Y;
    // Check needed for level changes.
    if (Map.LevelOverlayObjects[Game.CurrentLevel][Type.Symbol].Count == 0)
      Map.LevelOverlayObjects[Game.CurrentLevel][Type.Symbol].Add(this);
    else
      Map.LevelOverlayObjects[Game.CurrentLevel][Type.Symbol][0] = this;

    Map.LevelMapGrids[Game.CurrentLevel][oldPos.X][oldPos.Y].Draw();
    Map.LevelMapGrids[Game.CurrentLevel][newPos.X][newPos.Y].Draw();
    Map.LevelOverlayGrids[Game.CurrentLevel][oldPos.X][oldPos.Y].Draw();
    Map.LevelOverlayGrids[Game.CurrentLevel][newPos.X][newPos.Y].Draw();
    Map.Player.Draw();
    return true;
  }

  public void Jump(ConsoleKey key)
  {
    int x = 0;
    int y = 0;
    if (key == ConsoleKey.W) { x = 0; y = -2; }
    if (key == ConsoleKey.A) { x = -2; y = 0; }
    if (key == ConsoleKey.S) { x = 0; y = 2; }
    if (key == ConsoleKey.D) { x = 2; y = 0; }

    Position oldPos = new(X, Y);
    Position newPos = new(X + x, Y + y);

    if (!Map.CanJumpTo(oldPos.X, oldPos.Y, newPos.X, newPos.Y)) return;

    X = newPos.X;
    Y = newPos.Y;
    Map.LevelOverlayObjects[Game.CurrentLevel][Type.Symbol][0] = this;
    Map.LevelMapGrids[Game.CurrentLevel][oldPos.X][oldPos.Y].Draw();
    Map.LevelMapGrids[Game.CurrentLevel][newPos.X][newPos.Y].Draw();
    Map.LevelOverlayGrids[Game.CurrentLevel][oldPos.X][oldPos.Y].Draw();
    Map.LevelOverlayGrids[Game.CurrentLevel][newPos.X][newPos.Y].Draw();
    GamePlay.Messages.Add(new Message($"You jumped {Map.GetDirection(key)}..."));
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
        Monster melee = (Monster)objM;
        GamePlay.Messages.Add(new Message($"You swing your {Weapon.WeaponType} at the {melee.Type.Name}!"));
        melee.TakeDamage(Weapon.Damage);
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
    // don't add fists to inventory
    if (Weapon.Name != "Fists") Inventory.AddItem(Weapon);
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
    Player.Health -= damage;
    if (Player.Health <= 0)
    {
      Player.Health = 0;
      GamePlay.Messages.Add(new Message("You died!", Color.Red, Color.Black));
      Player.InCombat = false;
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
    if (MaxMana > 0) MaxMana += 5;
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
    foreach (char key in Map.LevelOverlayObjects[Game.CurrentLevel].Keys)
    {
      if (Map.LevelOverlayObjects[Game.CurrentLevel][key].Count == 0) continue;
      List<MapObject> objs = Map.LevelOverlayObjects[Game.CurrentLevel][key];
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
    if (X > 0 && Map.LevelOverlayGrids[Game.CurrentLevel][X - 1][Y].Type.Symbol != ' ')
    {
      obj = Map.LevelOverlayGrids[Game.CurrentLevel][X - 1][Y];
      if (obj is not Monster monster || monster.IsAlive)
        return obj.Type.Symbol;
    }

    // look right
    if (X < GamePlay.MapBox.Width && Map.LevelOverlayGrids[Game.CurrentLevel][X + 1][Y].Type.Symbol != ' ')
    {
      obj = Map.LevelOverlayGrids[Game.CurrentLevel][X + 1][Y];
      if (obj is not Monster monster || monster.IsAlive)
        return obj.Type.Symbol;
    }

    // look up
    if (Y > 0 && Map.LevelOverlayGrids[Game.CurrentLevel][X][Y - 1].Type.Symbol != ' ')
    {
      obj = Map.LevelOverlayGrids[Game.CurrentLevel][X][Y - 1];
      if (obj is not Monster monster || monster.IsAlive)
        return obj.Type.Symbol;
    }

    // look down
    if (Y >= GamePlay.MapBox.Height || Map.LevelOverlayGrids[Game.CurrentLevel][X][Y + 1].Type.Symbol != ' ')
    {
      obj = Map.LevelOverlayGrids[Game.CurrentLevel][X][Y + 1];
      if (obj is not Monster monster || monster.IsAlive)
        return obj.Type.Symbol;
    }

    // not found
    obj = new MapObject();
    return ' ';
  }

  internal bool IsNextToOverlay(char symbol, out MapObject obj)
  {
    // look left
    if (X > 0 && Map.LevelOverlayGrids[Game.CurrentLevel][X - 1][Y].Type.Symbol == symbol)
    {
      obj = Map.LevelOverlayGrids[Game.CurrentLevel][X - 1][Y];
      return true;
    }

    // look right
    if (X < GamePlay.MapBox.Width && Map.LevelOverlayGrids[Game.CurrentLevel][X + 1][Y].Type.Symbol == symbol)
    {
      obj = Map.LevelOverlayGrids[Game.CurrentLevel][X + 1][Y];
      return true;
    }

    // look up
    if (Y > 0 && Map.LevelOverlayGrids[Game.CurrentLevel][X][Y - 1].Type.Symbol == symbol)
    {
      obj = Map.LevelOverlayGrids[Game.CurrentLevel][X][Y - 1];
      return true;
    }

    // look down
    if (Y >= GamePlay.MapBox.Height || Map.LevelOverlayGrids[Game.CurrentLevel][X][Y + 1].Type.Symbol == symbol)
    {
      obj = Map.LevelOverlayGrids[Game.CurrentLevel][X][Y + 1];
      return true;
    }

    // not found
    obj = new MapObject();
    return false;
  }

  internal bool IsNextToMap(char symbol, out MapObject obj)
  {
    // look left
    if (X > 0 && Map.LevelMapGrids[Game.CurrentLevel][X - 1][Y].Type.Symbol == symbol)
    {
      obj = Map.LevelMapGrids[Game.CurrentLevel][X - 1][Y];
      return true;
    }

    // look right
    if (X < GamePlay.MapBox.Width && Map.LevelMapGrids[Game.CurrentLevel][X + 1][Y].Type.Symbol == symbol)
    {
      obj = Map.LevelMapGrids[Game.CurrentLevel][X + 1][Y];
      return true;
    }

    // look up
    if (Y > 0 && Map.LevelMapGrids[Game.CurrentLevel][X][Y - 1].Type.Symbol == symbol)
    {
      obj = Map.LevelMapGrids[Game.CurrentLevel][X][Y - 1];
      return true;
    }

    // look down
    if (Y >= GamePlay.MapBox.Height || Map.LevelMapGrids[Game.CurrentLevel][X][Y + 1].Type.Symbol == symbol)
    {
      obj = Map.LevelMapGrids[Game.CurrentLevel][X][Y + 1];
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
      if (X > 0 && Map.LevelMapGrids[Game.CurrentLevel][X - 1][Y].Type.Symbol != ' ')
      {
        obj = Map.LevelMapGrids[Game.CurrentLevel][X - 1][Y];
        return true;
      }

      // look right
      if (X < GamePlay.MapBox.Width && Map.LevelMapGrids[Game.CurrentLevel][X + 1][Y].Type.Symbol != ' ')
      {
        obj = Map.LevelMapGrids[Game.CurrentLevel][X + 1][Y];
        return true;
      }

      // look up
      if (Y > 0 && Map.LevelMapGrids[Game.CurrentLevel][X][Y - 1].Type.Symbol != ' ')
      {
        obj = Map.LevelMapGrids[Game.CurrentLevel][X][Y - 1];
        return true;
      }

      // look down
      if (Y >= GamePlay.MapBox.Height || Map.LevelMapGrids[Game.CurrentLevel][X][Y + 1].Type.Symbol != ' ')
      {
        obj = Map.LevelMapGrids[Game.CurrentLevel][X][Y + 1];
        return true;
      }
    }

    // not found
    obj = new MapObject();
    return false;
  }
}