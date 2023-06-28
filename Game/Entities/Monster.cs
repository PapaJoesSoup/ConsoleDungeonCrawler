using System.Drawing;
using System.Reflection.Emit;
using ConsoleDungeonCrawler.Game.Entities.Items;
using ConsoleDungeonCrawler.Game.Screens;

namespace ConsoleDungeonCrawler.Game.Entities;

internal class Monster : MapObject
{
  private int health = 10;
  internal int MaxHealth = 10;
  internal int Mana;
  internal int MaxMana;
  private readonly int level = 1;
  internal readonly decimal Gold;
  private readonly Weapon weapon = new();
  internal Spell Spell = new();
  internal bool IsAlive = true;
  internal bool InCombat;

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
    health = level * 2;
    MaxHealth = level * 2;
    Mana = level * 2;
    MaxMana = level * 2;
    this.level = level * 2;
    Gold = Decimal.Round(level * Dice.Roll(.01M, 1.1M), 2);
    weapon = new Weapon();
    this.level = level;
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
    Draw();
    GamePlay.Messages.Add(new Message($"A {Type.Name} spots you!  You are in combat!", Color.Red, Color.Black));
  }

  internal void Attack()
  {
    if (!InCombat) return;

    // Find path to player
    List<Position> path = PathFinding.FindPath(this, Map.Player);
    if (path.Count == 0) return;

    // display path to player (testing)
    //foreach (Position pos in path) Map.LevelMapGrids[Game.CurrentLevel][pos.X][pos.Y].Highlight();
    //this.Draw();
    //Map.Player.Draw();

    // check if player is within radius of Weapon range
    if (GetDistance(Map.Player) <= weapon.Range)
    {
      // roll to hit
      if (Dice.Roll(1, 20) < 10) return;
      // roll for damage
      int damage = Dice.Roll(weapon.Damage);
      if (damage == 0)
        GamePlay.Messages.Add(new Message($"The {Type.Name} attacks and misses you!", Color.Gold, Color.Black));
      else
      {
        GamePlay.Messages.Add(new Message($"The {Type.Name} attacks and hits you for {damage} damage!", Color.DarkOrange, Color.Black));
        Player.TakeDamage(damage);
      }
      return;
    }

    if (GetDistance(Map.Player) <= weapon.Range) return;
    // move towards player  Path is in reverse order, so move to second to last position
    if (path.Count > 2) MoveTo(path[^2]);
  }

  private void MoveTo(Position newPos)
  {
    // Monsters can step on overlay items (items/dead monsters on the ground).  Layers are used to preserve items and dead monsters.
    // This will move a monster to a new position, moving them to another layer if needed to preserve items on the ground.
    // Layer 0 is reserved to the Map overlay object when the overlays are loaded.  The top most layer is the last layer in the list.
    // We also wan to make sure that living monsters are always on the top layer, so we will move them to the top layer if needed.
    if (!CanMoveTo(newPos)) return;

    Position oldPos = new(X, Y);
    X = newPos.X;
    Y = newPos.Y;

    List<MapObject> newList = Map.LevelOverlayGrids[Game.CurrentLevel][newPos.X][newPos.Y];
    List<MapObject> oldList = Map.LevelOverlayGrids[Game.CurrentLevel][oldPos.X][oldPos.Y];

    // there is always at least one item in the list, the Map overlay object
    // add monster to the new cell
    if (newList.Count > 1) 
      newList.Add(this);
    else
    {
      if (newList[0].ContainsItem()) newList.Add(this);
      else newList[0] = this;
    }

    // remove this monster from the old cell
    if (oldList.Count == 1)
    {
      oldList[0] = new MapObject(oldPos.X, oldPos.Y, new ObjectType(true));
      Map.LevelMapGrids[Game.CurrentLevel][oldPos.X][oldPos.Y].Draw();
    }
    else
    {
      for (int layer = 1; layer < oldList.Count; layer++)
      {
        if (oldList[layer] != this) continue;
        oldList.RemoveAt(layer);
        break;
      }
    }
    Map.LevelMapGrids[Game.CurrentLevel][oldPos.X][oldPos.Y].Draw();
    Draw();
  }

  internal void TakeDamage(int damage)
  {
    if (damage <= 0)
    {
      GamePlay.Messages.Add(new Message($"You missed the {Type.Name}!", Color.DarkOrange, Color.Black));
      GamePlay.Messages.Add(new Message($"{Type.Name} has {health} health left!", Color.DarkOrange, Color.Black));
    }
    else
    {
      GamePlay.Messages.Add(new Message($"You hit the {Type.Name} for {damage} damage!", Color.DarkOrange, Color.Black));
      health -= damage;
      if (health <= 0)
      {
        health = 0;
        IsAlive = false;
        GamePlay.Messages.Add(new Message($"You killed the {Type.Name}!", Color.LimeGreen, Color.Black));
        int xp = Dice.Roll(level * 2);
        GamePlay.Messages.Add(new Message($"You gained {xp} experience!", Color.LimeGreen, Color.Black));
        Player.AddExperience(xp);
        BackgroundColor = Type.BackgroundColor;
        ForegroundColor = Color.Gray;
        IsPassable = true;
        InCombat = false;
        Draw();
        Map.RemoveFromOverlayObjects(this);
        Player.InCombat = Player.IsInCombat();
      }
      else
        GamePlay.Messages.Add(new Message($"{Type.Name} has {health} health left!", Color.DarkOrange, Color.Black));
    }
  }

  internal static bool CanMoveTo(Position pos)
  {
    // check to see if there is an object that is not passable or some other immovable object
    foreach (MapObject obj in Map.LevelOverlayGrids[Game.CurrentLevel][pos.X][pos.Y])
      if (!obj.IsPassable) return false;
    return Map.LevelMapGrids[Game.CurrentLevel][pos.X][pos.Y].IsPassable;
  }
  
  private static int SetOdds(char type)
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
    if (Dice.Roll(SetOdds(monster.Type.Symbol)) != 1) return new Item(); //chance of dropping an item other than gold
    monster.IsLootable = false;
    return Inventory.GetRandomItem();
  }
}