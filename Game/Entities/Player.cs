using ConsoleDungeonCrawler.Game.Entities.Items;
using ConsoleDungeonCrawler.Game.Screens;
using System.Drawing;
using LibVLCSharp.Shared;

namespace ConsoleDungeonCrawler.Game.Entities;

internal class Player : Tile
{
  #region Properties
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
  internal static bool InCombat;

  private static MediaPlayer? effectPlayer;
  #endregion Properties

  internal Player()
  {
    effectPlayer = SoundSystem.GetPlayer();
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
    effectPlayer = SoundSystem.GetPlayer();

    // Add 5 empty slots to armor set
    ArmorSet = new List<Armor>
    {
      new(ArmorType.Head),
      new(ArmorType.Body),
      new(ArmorType.Hand),
      new(ArmorType.Leg),
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
    effectPlayer?.Play(SoundSystem.MSounds[Sound.FootSteps]);
    X = newPos.X;
    Y = newPos.Y;
    // Overlay section Check needed for level changes.
    if (Map.LevelOverlayTiles[Game.CurrentLevel][Type.Symbol].Count == 0)
      Map.LevelOverlayTiles[Game.CurrentLevel][Type.Symbol].Add(this);
    else
      Map.LevelOverlayTiles[Game.CurrentLevel][Type.Symbol][0] = this;

    // update grids.
    Map.CurrentMap[oldPos.X, oldPos.Y].Draw();
    Map.CurrentMap[newPos.X, newPos.Y].Draw();

    int oldLayer = Map.CurrentOverlay[oldPos.X, oldPos.Y].Count - 1;
    int newLayer = Map.CurrentOverlay[newPos.X, newPos.Y].Count - 1;
    Map.CurrentOverlay[oldPos.X, oldPos.Y][oldLayer].Draw();
    Map.CurrentOverlay[newPos.X, newPos.Y][newLayer].Draw();
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
    effectPlayer?.Play(SoundSystem.MSounds[Sound.FootSteps]);

    X = newPos.X;
    Y = newPos.Y;
    for (int i = 0; i < Map.CurrentOverlay[oldPos.X, oldPos.Y].Count; i++)
    {
      Map.LevelOverlayTiles[Game.CurrentLevel][Type.Symbol][0] = this;
      Map.CurrentMap[oldPos.X, oldPos.Y].Draw();
      Map.CurrentMap[newPos.X, newPos.Y].Draw();

      Map.CurrentOverlay[oldPos.X, oldPos.Y][i].Draw();
      Map.CurrentOverlay[oldPos.X, oldPos.Y][i].Draw();
      Map.CurrentOverlay[newPos.X, newPos.Y][i].Draw();
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
        effectPlayer?.Play(SoundSystem.MSounds[Sound.SwordSwing]);
        melee.TakeDamage(Weapon.Damage);
        break;
      case WeaponType.Bow:
      case WeaponType.Wand:
        if (IsInRange(Weapon.Range, out Tile objR)) return;
        if (objR is not Monster ranged) return;
        effectPlayer?.Play(SoundSystem.MSounds[Sound.RangedAttack]);
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
        if (ArmorSet[0].ArmorName != ArmorName.None)
          Inventory.AddItem(ArmorSet[0]);
        ArmorSet[0] = armor;
        break;
      case ArmorType.Body:
        if (ArmorSet[1].ArmorName != ArmorName.None)
          Inventory.AddItem(ArmorSet[1]);
        ArmorSet[1] = armor;
        break;
      case ArmorType.Hand:
        if (ArmorSet[2].ArmorName != ArmorName.None)
          Inventory.AddItem(ArmorSet[2]);
        ArmorSet[2] = armor;
        break;
      case ArmorType.Leg:
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
    effectPlayer?.Play(SoundSystem.MSounds[Sound.LevelUp]);
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
    foreach (char key in Map.LevelOverlayTiles[Game.CurrentLevel].Keys)
    {
      if (Map.LevelOverlayTiles[Game.CurrentLevel][key].Count == 0) continue;
      List<Tile> objs = Map.LevelOverlayTiles[Game.CurrentLevel][key];
      if (!objs[0].Type.IsAttackable) continue;
      foreach (Tile obj in objs)
      {
        if (obj is Player || obj is Monster == false) continue;
        if (((Monster)obj).InCombat) return true;
      }
    }
    return false;
  }

  // Option value Change event handler
  internal static void SetEffectVolume(object? sender, EventArgs e)
  {
    if (sender is not GameOption<int> option) return;
    if (effectPlayer != null) effectPlayer.Volume = option.Value;
  }
}