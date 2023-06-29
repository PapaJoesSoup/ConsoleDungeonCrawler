using ConsoleDungeonCrawler.Game.Entities.Items;
using ConsoleDungeonCrawler.Game.Screens;
using System.Drawing;

namespace ConsoleDungeonCrawler.Game.Entities;

internal class Player : Tile
{
  internal static int Level = 1;
  private static int experience;
  private static int experienceToLevel = 100;
  internal static readonly PlayerClass Class = PlayerClass.Rogue;
  internal static int Health = 100;
  internal static int MaxHealth = 100;
  internal static int Mana;
  internal static int MaxMana;
  internal static decimal Gold;

  internal static List<Armor> ArmorSet = new();
  internal static Weapon Weapon = new();
  internal static readonly Dictionary<int, Spell> Spells = new();
  internal static bool InCombat = false;

  internal Player()
  {
  }

  internal Player(Tile tile)
  {
    // Set Player's base Tile values to the passed in values
    X = tile.X;
    Y = tile.Y;
    Type = tile.Type;
    ForegroundColor = Type.ForegroundColor;
    BackgroundColor = Type.BackgroundColor;
    IsVisible = tile.IsVisible;

    // Add 5 empty slots to armor set
    ArmorSet = new List<Armor>
    {
      new(ArmorType.Head),
      new(ArmorType.Body),
      new(ArmorType.Hands),
      new(ArmorType.Legs),
      new(ArmorType.Feet)
    };

    // Add a couple of bags and 5 initial items to inventory
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

    if (!CanMoveTo(newPos)) return false;
    X = newPos.X;
    Y = newPos.Y;
    // Overlay section Check needed for level changes.
    if (Map.LevelOverlayObjects[Game.CurrentLevel][Type.Symbol].Count == 0)
      Map.LevelOverlayObjects[Game.CurrentLevel][Type.Symbol].Add(this);
    else
      Map.LevelOverlayObjects[Game.CurrentLevel][Type.Symbol][0] = this;

    // update grids.
    Map.LevelMapGrids[Game.CurrentLevel][oldPos.X][oldPos.Y].Draw();
    Map.LevelMapGrids[Game.CurrentLevel][newPos.X][newPos.Y].Draw();

    int oldlayer = Map.LevelOverlayGrids[Game.CurrentLevel][oldPos.X][oldPos.Y].Count - 1;
    int newlayer = Map.LevelOverlayGrids[Game.CurrentLevel][newPos.X][newPos.Y].Count - 1;
    Map.LevelOverlayGrids[Game.CurrentLevel][oldPos.X][oldPos.Y][oldlayer].Draw();
    Map.LevelOverlayGrids[Game.CurrentLevel][newPos.X][newPos.Y][newlayer].Draw();
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

    if (!CanJumpTo(oldPos, newPos)) return;

    X = newPos.X;
    Y = newPos.Y;
    for (int i = 0; i < Map.LevelOverlayGrids[Game.CurrentLevel][oldPos.X][oldPos.Y].Count; i++)
    {
      Map.LevelOverlayObjects[Game.CurrentLevel][Type.Symbol][0] = this;
      Map.LevelMapGrids[Game.CurrentLevel][oldPos.X][oldPos.Y].Draw();
      Map.LevelMapGrids[Game.CurrentLevel][newPos.X][newPos.Y].Draw();

      Map.LevelOverlayGrids[Game.CurrentLevel][oldPos.X][oldPos.Y][i].Draw();
      Map.LevelOverlayGrids[Game.CurrentLevel][oldPos.X][oldPos.Y][i].Draw();
      Map.LevelOverlayGrids[Game.CurrentLevel][newPos.X][newPos.Y][i].Draw();
    }
    GamePlay.Messages.Add(new Message($"You jumped {Map.GetDirection(key)}..."));
  }

  public void Attack()
  {
    //if (!InCombat) return;
    switch (Weapon.WeaponType)
    {
      case WeaponType.Fists:
      case WeaponType.Sword:
      case WeaponType.Axe:
      case WeaponType.Mace:
      case WeaponType.Dagger:
      case WeaponType.Staff:
        if (IsNextToOverlayGrid(out Tile objM) == ' ') return;
        if (objM is not Monster melee) return;
        GamePlay.Messages.Add(new Message($"You swing your {Weapon.WeaponType} at the {melee.Type.Name}!"));
        melee.TakeDamage(Weapon.Damage);
        break;
      case WeaponType.Bow:
      case WeaponType.Wand:
        if (IsInRange(Weapon.Range, out Tile objR)) return;
        if (objR is not Monster ranged) return;
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
    Health -= damage;
    if (Health <= 0)
    {
      Health = 0;
      GamePlay.Messages.Add(new Message("You died!", Color.Red, Color.Black));
      InCombat = false;
    }
    else
    {
      GamePlay.Messages.Add(
        new Message($"You have {Health} health left!", Color.DarkOrange, Color.Black));
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

  private static void LevelUp()
  {
    Level++;
    experienceToLevel = (int)(experienceToLevel * 1.5);
    MaxHealth += 10;
    if (MaxMana > 0) MaxMana += 5;
    Health = MaxHealth;
    Mana = MaxMana;
    GamePlay.Messages.Add(new Message($"You are now level {Level}!", Color.Green, Color.Black));
  }

  internal static void AddExperience(int amount)
  {
    experience += amount;
    if (experience >= experienceToLevel)
    {
      experience -= experienceToLevel;
      LevelUp();
    }
  }

  internal static bool IsInCombat()
  {
    // Check if there is a tile.InCombat == true in OverlayObjects Except Player
    foreach (char key in Map.LevelOverlayObjects[Game.CurrentLevel].Keys)
    {
      if (Map.LevelOverlayObjects[Game.CurrentLevel][key].Count == 0) continue;
      List<Tile> objs = Map.LevelOverlayObjects[Game.CurrentLevel][key];
      if (!objs[0].Type.IsAttackable) continue;
      foreach (Tile obj in objs)
      {
        if (obj is Player || obj is Monster == false) continue;
        if (((Monster)obj).InCombat) return true;
      }
    }
    return false;
  }

  private char IsNextToOverlayGrid(out Tile obj)
  {
    // we need to account for monsters on a different overlay level
    if (IsNextToOverlay(West, out obj)) return obj.Type.Symbol;
    if (IsNextToOverlay(East, out obj)) return obj.Type.Symbol;
    if (IsNextToOverlay(North, out obj)) return obj.Type.Symbol;
    if (IsNextToOverlay(South, out obj)) return obj.Type.Symbol;

    // not found
    obj = new Tile();
    return ' ';
  }

  internal bool IsNextToOverlayGrid(char symbol, out Tile obj)
  {
    // we need to account for monsters on a different overlay level
    if (IsNextToOverlay(West, out obj, symbol)) return true;
    if (IsNextToOverlay(East, out obj, symbol)) return true;
    if (IsNextToOverlay(North, out obj, symbol)) return true;
    if (IsNextToOverlay(South, out obj, symbol)) return true;

    // not found
    obj = new Tile();
    return false;
  }

  private bool IsNextToOverlay(Position pos, out Tile obj, char symbol = char.MinValue)
  {
    obj = new Tile();
    if (pos is not { X: > 0, Y: > 0 } || pos.X > GamePlay.MapBox.Width || pos.Y > GamePlay.MapBox.Height) return false;
    // if we have any live monsters, we want to get them first, so we work our way down from the top layer.
    for (int layer = Map.LevelOverlayGrids[Game.CurrentLevel][pos.X][pos.Y].Count - 1; layer >= 0; layer--)
    {
      if (symbol != char.MinValue)
      {
        if (Map.LevelOverlayGrids[Game.CurrentLevel][pos.X][pos.Y][layer].Type.Symbol != symbol) continue;
      }
      else if (Map.LevelOverlayGrids[Game.CurrentLevel][pos.X][pos.Y][layer].Type.Symbol == ' ') continue;

      obj = Map.LevelOverlayGrids[Game.CurrentLevel][pos.X][pos.Y][layer];
      if (obj is Monster { IsAlive: false }) continue;
      return true;
    }

    // not found
    return false;
  }

  internal bool IsNextToMapGrid(char symbol, out Tile obj)
  {
    if (IsNextToMap(symbol, West,out obj)) return true;
    if (IsNextToMap(symbol, East, out obj)) return true;
    if (IsNextToMap(symbol, North, out obj)) return true;
    if (IsNextToMap(symbol, South, out obj)) return true;

    // not found
    obj = new Tile();
    return false;
  }

  private bool IsNextToMap(char symbol, Position pos, out Tile obj)
  {
    if (pos is { X: > 0, Y: > 0 } && pos.X <= GamePlay.MapBox.Width && pos.Y <= GamePlay.MapBox.Height)
    {
      if (Map.LevelMapGrids[Game.CurrentLevel][pos.X][pos.Y].Type.Symbol == symbol)
      {
        obj = Map.LevelMapGrids[Game.CurrentLevel][pos.X][pos.Y];
        return true;
      }
    }

    obj = new Tile();
    return false;
  }

  private bool IsNextToMap(Position pos, out Tile obj)
  {
    if (pos is { X: > 0, Y: > 0 } && pos.X <= GamePlay.MapBox.Width && pos.Y <= GamePlay.MapBox.Height)
    {
      if (Map.LevelMapGrids[Game.CurrentLevel][pos.X][pos.Y].Type.Symbol != ' ')
      {
        obj = Map.LevelMapGrids[Game.CurrentLevel][pos.X][pos.Y];
        return true;
      }
    }

    obj = new Tile();
    return false;
  }

  private bool IsInRange(int radius, out Tile obj)
  {
    // find the closest object within the radius
    for (int i = 1; i <= radius; i++)
    {
      Position left = new(X - i, Y);
      Position right = new(X + i, Y);
      Position up = new(X, Y - i);
      Position down = new(X, Y + i);

      if (IsNextToMap(left, out obj)) return true;
      if (IsNextToMap(right, out obj)) return true;
      if (IsNextToMap(up, out obj)) return true;
      if (IsNextToMap(down, out obj)) return true;
    }

    // not found
    obj = new Tile();
    return false;
  }

  private static bool CanMoveTo(Position pos)
  {
    // check to see if there is an object there that is not passable
    for (int layer = Map.LevelOverlayGrids[Game.CurrentLevel][pos.X][pos.Y].Count - 1; layer >= 0; layer--)
      if (!Map.LevelOverlayGrids[Game.CurrentLevel][pos.X][pos.Y][layer].IsPassable) return false;
    return Map.LevelMapGrids[Game.CurrentLevel][pos.X][pos.Y].IsPassable;
  }

  private static bool CanJumpTo(Position oldPos, Position newPos)
  {
    // check to see if there is an object in between old and new location that is not passable and not transparent
    Direction dir = Map.GetDirection(oldPos, newPos);
    Position curPos = oldPos;
    if (dir == Direction.West) curPos = oldPos.West;
    if (dir == Direction.East) curPos = oldPos.East;
    if (dir == Direction.North) curPos = oldPos.North;
    if (dir == Direction.South) curPos = oldPos.South;
    if (curPos == oldPos) return false;

    List<Tile> layers = Map.LevelOverlayGrids[Game.CurrentLevel][curPos.X][curPos.Y];
    for (int i = layers.Count - 1; i >= 0; i--)
      if (!layers[i].IsPassable) return false;

    Tile mapObj = Map.LevelMapGrids[Game.CurrentLevel][curPos.X][curPos.Y];
    return mapObj is not { IsPassable: false, Type.IsTransparent: false } && CanMoveTo(newPos);
  }
}