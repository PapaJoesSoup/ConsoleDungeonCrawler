using System.Drawing;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Screens;

namespace ConsoleDungeonCrawler.Game.Entities;

internal class Map
{
  #region Properties
  internal static Map Instance = new();
  internal static int Top { get; set; }
  internal static int Left { get; set; }
  private static int Width { get; set; }
  private static int Height { get; set; }

  internal static List<TileType> MapTypes = new();
  private static List<TileType> overlayTypes = new();

  // Dictionary storage and retrieval is faster than a DataSet/DataTables and not as heavy.
  // expressed as:  LevelMapTileGrids[level][x][y];
  private static Dictionary<int, Dictionary<int, Dictionary<int, Tile>>> levelMapTileGrids = new();
  internal static Dictionary<int,Dictionary<int, Tile>> CurrentMap = new();
  internal static int MinMapLevel;
  internal static int MaxMapLevel;

  // This allows for multiple overlay objects on a single tile.
  private static Dictionary<int, Dictionary<int, Dictionary<int, List<Tile>>>> levelOverlayTileGrids = new();
  internal static Dictionary<int, Dictionary<int, List<Tile>>> CurrentOverlay = new();

  private static Dictionary<int, Dictionary<char, List<Tile>>> levelMapTiles = new();
  internal static Dictionary<int, Dictionary<char, List<Tile>>> LevelOverlayTiles = new();
  private static Dictionary<int, Dictionary<char, Tuple<TileType, int>>> levelVisibleObjects = new();

  private static Dictionary<Direction, Point> lookAt = new ();
  internal static Player Player = new();
  private static char startChar;
  private static char exitChar;
  #endregion Properties

  #region Constructors
  private Map()
  {
  }

  internal Map(Box mapBox)
  {
    Top = mapBox.Top + 1;
    Left = mapBox.Left + 1;
    Width = mapBox.Width - 2;
    Height = mapBox.Height - 2;

    InitTypeLists();
    InitDictionaries();
    LoadGameMaps();
    CurrentMap = levelMapTileGrids[Game.CurrentLevel];
    CurrentOverlay = levelOverlayTileGrids[Game.CurrentLevel];
  }
  #endregion Constructors

  #region Initialization
  private void InitTypeLists()
  {
    // These are for building the map
    MapTypes = new List<TileType>
    {
      new('#', "Wall", "a wall", "some walls", Color.FromArgb(255, 40, 40, 40), Color.FromArgb(255, 40, 40, 40), false, false, false, false),
      new('.', "Floor", "a floor", "some flooring", Color.Gray, Color.DimGray, true, false, false, false),
      new('+', "DoorC", "a closed door", "some closed doors", Color.Yellow, Color.DimGray, false, false, false, false),
      new('-', "DoorO", "an open door", "some open doors", Color.Yellow, Color.DimGray, true, false, false, false),
      new('\u25b2', "UpStairs", "stairs going up", "multiple stairs going up", Color.White, Color.DimGray, true, false, false, true),
      new('\u25bc', "DownStairs", "stairs going down", "multiple stairs going down", Color.White, Color.DimGray, true, false, false, true),
      new('\u1f62', "Fire", "a fire", "some fire", Color.Khaki, Color.DimGray, false, false, false, true),
      new('~', "Water", "some water", "some patches of water", Color.Aqua, Color.Aqua, false, false, false, true),
      new('a', "Acid", "some acid", "some patches of acid", Color.SaddleBrown, Color.Chartreuse, false, false, false, true),
      new('L', "Lava", "some lava", "some patches of lava", Color.PapayaWhip, Color.Goldenrod, false, false, false, true),
      new('I', "Ice", "some ice", "some patches of ice", Color.Blue, Color.DeepSkyBlue, false, false, false, true)
    };

    // These are for placing objects on the map.
    overlayTypes = new List<TileType>
    {
      new('S', "Start", "the Entrance", "the Entrance", Color.Black, Color.White, true, false, false),
      new('E', "Exit", "the Exit", "The Exit", Color.MidnightBlue, Color.Gold, true, false, false),
      new('\u2640', "Player", "me", "am is seeing double?", Color.White, Color.DimGray, true, true, true),
      new('V', "Vendor", "A Vendor", "Vendors", Color.MidnightBlue, Color.Lime, true, false, false),
      new('\u25b2', "UpStairs", "stairs going up", "multiple stairs going up", Color.White, Color.DimGray, true, false, false),
      new('\u25bc', "DownStairs", "stairs going down", "multiple stairs going down", Color.White, Color.DimGray, true, false, false),
      new('m', "Chest", "a Chest", "some Chests", Color.Silver, Color.DimGray, true, false, false),
      new('i', "Item", "an Item", "some Items", Color.White, Color.DimGray, true, false, true),
      new('$', "Gold", "some Gold", "some stacks of Gold", Color.Gold, Color.DimGray, true, false, true),
      new('T', "Teleporter", "a Teleporter", "some Teleporters", Color.Gold, Color.DimGray, true, false, false),
      new('x', "Trap", "a Trap", "some Traps", Color.LightSalmon, Color.DimGray, false, false, false),
      new('k', "Kobald", "a Kobald", "some Kobalds", Color.BlueViolet, Color.DimGray, false, true, true),
      new('z', "Ooze", "an Ooze", "some Oozes", Color.GreenYellow, Color.DimGray, false, true, true),
      new('g', "Goblin", " a Goblin", "some Goblins", Color.CadetBlue, Color.DimGray, false, true, true),
      new('O', "Ogre", "an Ogre", "Some Ogres", Color.Chocolate, Color.DimGray, false, true, true),
      new('B', "Boss", "a Boss", "some Bosses", Color.Maroon, Color.Yellow, false, true, true)
    };

    // Initialize Player and start char symbols
    Player.Type = overlayTypes.Find(x => x.Name == "Player") ?? new TileType();
    startChar = overlayTypes.Find(x => x.Name == "Start")!.Symbol;
    exitChar = overlayTypes.Find(x => x.Name == "Exit")!.Symbol;
  }

  private void InitDictionaries()
  {
    lookAt = new ()
    {
      { Direction.North, new Point(0, -1) },
      { Direction.South, new Point(0, 1) },
      { Direction.East, new Point(1, 0) },
      { Direction.West, new Point(-1, 0) },
      { Direction.NorthEast, new Point(1, -1) },
      { Direction.NorthWest, new Point(-1, -1) },
      { Direction.SouthEast, new Point(1, 1) },
      { Direction.SouthWest, new Point(-1, 1) }
    };

    levelMapTileGrids = new();
    levelOverlayTileGrids = new();
    levelMapTileGrids = new();
    LevelOverlayTiles = new();
    levelMapTiles = new();


    // load empty map/overlay grid for each level
    foreach (string level in Game.Dungeons[Game.CurrentDungeon].Keys)
    {
      int lvlNumber = int.Parse(level.Split('_')[1].Split('.')[0]);
      if (level.Contains("LevelMap_"))
      {
        // create empty map grid
        Dictionary<int, Dictionary<int, Tile>> mapGrid = new();
        for (int x = 0; x <= Width; x++)
        {
          mapGrid.Add(x, new Dictionary<int, Tile>());
          for (int y = 0; y <= Width; y++)
          {
            mapGrid[x].Add(y, new Tile(x, y));
          }
        }
        // Add MapGrid to LevelMapTileGrids
        levelMapTileGrids.Add(lvlNumber, mapGrid);
      }

      if (!level.Contains("LevelOverlay_")) continue;
      // create empty overlay grid
      Dictionary<int, Dictionary<int, List<Tile>>> overlayGrid = new();
      TileType empty = new(true, false);
      for (int x = 0; x <= Width; x++)
      {
        overlayGrid.Add(x, new Dictionary<int, List<Tile>>());
        for (int y = 0; y <= Width; y++)
        {
          overlayGrid[x].Add(y, new List<Tile>());
          overlayGrid[x][y].Add(new Tile(x, y, empty));
        }
      }
      // Add OverlayGrid to LevelOverlayTileGrids
      levelOverlayTileGrids.Add(lvlNumber, overlayGrid);
    }

    // Create empty object lists for each level
    levelMapTiles = new Dictionary<int, Dictionary<char, List<Tile>>>();
    foreach (string level in Game.Dungeons[Game.CurrentDungeon].Keys)
    {
      int lvlNumber = int.Parse(level.Split('_')[1].Split('.')[0]);
      if (levelMapTiles.ContainsKey(lvlNumber)) continue;
      levelMapTiles.Add(lvlNumber, new Dictionary<char, List<Tile>>());
      foreach (TileType objectType in MapTypes)
        levelMapTiles[lvlNumber].Add(objectType.Symbol, new List<Tile>());
    }

    LevelOverlayTiles = new Dictionary<int, Dictionary<char, List<Tile>>>();
    foreach (string level in Game.Dungeons[Game.CurrentDungeon].Keys)
    {
      int lvlNumber = int.Parse(level.Split('_')[1].Split('.')[0]);
      if (LevelOverlayTiles.ContainsKey(lvlNumber)) continue;
      LevelOverlayTiles.Add(lvlNumber, new Dictionary<char, List<Tile>>());
      foreach (TileType objectType in overlayTypes)
        LevelOverlayTiles[lvlNumber].Add(objectType.Symbol, new List<Tile>());
    }

    levelVisibleObjects = new Dictionary<int, Dictionary<char, Tuple<TileType, int>>>();
    foreach (string level in Game.Dungeons[Game.CurrentDungeon].Keys)
    {
      int lvlNumber = int.Parse(level.Split('_')[1].Split('.')[0]);
      if (levelVisibleObjects.ContainsKey(lvlNumber)) continue;
      levelVisibleObjects.Add(lvlNumber, new Dictionary<char, Tuple<TileType, int>>());
    }
  }

  /// <summary>
  /// Loads all game maps into memory.  This will improve performance during game play
  /// </summary>
  private static void LoadGameMaps()
  {
    // Load the map grids from the files
    foreach (string level in Game.Dungeons[Game.CurrentDungeon].Keys)
    {
      int lvlNumber = int.Parse(level.Split('_')[1].Split('.')[0]);
      if (level.Contains("LevelMap_"))
      {
        LoadMapGridFromFile(lvlNumber, Game.Dungeons[Game.CurrentDungeon][level]);
        if (lvlNumber < MinMapLevel) MinMapLevel = lvlNumber;
        if (lvlNumber > MaxMapLevel) MaxMapLevel = lvlNumber;
      }
      if (level.Contains("LevelOverlay_")) LoadOverlayGridFromFile(lvlNumber, Game.Dungeons[Game.CurrentDungeon][level]);
    }
  }

  internal static void LoadLevel()
  {
    ConsoleEx.Clear();
    CurrentMap = levelMapTileGrids[Game.CurrentLevel];
    CurrentOverlay = levelOverlayTileGrids[Game.CurrentLevel];
    GamePlay.Draw();
  }

  /// <summary>
  /// Populates the map grid from the file, updating those cells that contain Map type objects
  /// </summary>
  /// <param name="level"></param>
  /// <param name="filename"></param>
  private static void LoadMapGridFromFile(int level, string filename)
  {
    StringBuilder sb = new();
    sb.Append(File.ReadAllText(filename));
    string[] lines = sb.ToString().Split('\n');
    for (int y = 1; y <= Height; y++)
    {
      string line = lines[y];
      for (int x = 1; x <= Width; x++)
      {
        char c = line[x];
        // find the object type where the symbol matches the string
        TileType? type = MapTypes.Find(t => t.Symbol == c);
        if (type == null) continue;
        Tile obj = new(x - 1, y - 1, type, false);
        levelMapTileGrids[level][x - 1][y - 1] = obj;
      }
    }
  }

  /// <summary>
  /// Populates the overlay tile grid from the file, updating only those cells that contain an overlay object type.
  /// </summary>
  /// <param name="level"></param>
  /// <param name="filename"></param>
  private static void LoadOverlayGridFromFile(int level, string filename)
  {
    StringBuilder sb = new();
    sb.Append(File.ReadAllText(filename));
    string[] lines = sb.ToString().Split('\n');
    for (int y = 1; y <= Height; y++)
    {
      string line = lines[y];
      for (int x = 1; x <= Width; x++)
      {
        char c = line[x];
        // find the object type where the symbol matches the string
        TileType? type = overlayTypes.Find(t => t.Symbol == c);
        if (type == null) continue;
        Tile obj = new(x - 1, y - 1, type, type.Symbol == Player.Type.Symbol || type.Symbol == startChar);
        if (type.Symbol == Player.Type.Symbol)
        {
          Player = new Player(obj);
          LevelOverlayTiles[level][type.Symbol].Add(Player);
        }
        else if (obj.IsAttackable)
        {
          Monster monster = new(obj, 1);
          levelOverlayTileGrids[level][x - 1][y - 1][0] = monster;
          LevelOverlayTiles[level][type.Symbol].Add(monster);
        }
        else
        {
          levelOverlayTileGrids[level][x - 1][y - 1][0] = obj;
          LevelOverlayTiles[level][type.Symbol].Add(obj);
        }
      }
    }
  }
  #endregion Initialization

  #region Drawing
  internal static void DrawMap()
  {
    foreach (int x in CurrentMap.Keys)
    {
      foreach (int y in CurrentMap[x].Keys)
      {
        Tile obj = CurrentMap[x][y];
        if (!obj.IsVisible || obj.Type.Symbol == ' ') continue;
        obj.Draw();
      }
    }
  }

  internal static void DrawOverlay()
  {
    foreach (int x in CurrentOverlay.Keys)
    {
      foreach (int y in CurrentOverlay[x].Keys)
      {
        int layer = CurrentOverlay[x][y].Count - 1;
        Tile obj = CurrentOverlay[x][y][layer];
        if (!obj.IsVisible || obj.Type.Symbol == ' ') continue;
        obj.Draw();
      }
    }
  }

  internal static void ShowAllMapTiles()
  {
    foreach (int x in CurrentMap.Keys)
    {
      foreach (int y in CurrentMap[x].Keys)
      {
        Tile obj = CurrentMap[x][y];
        if (obj.Type.Symbol == ' ') continue;
        obj.Draw(true);
      }
    }
    Console.ReadKey(true);
    Clear();
    DrawMap();
    DrawOverlay();
  }

  internal static void ShowAllOverlayTiles()
  {
    foreach (int x in CurrentOverlay.Keys)
    {
      foreach (int y in CurrentOverlay[x].Keys)
      {
        int layer = CurrentOverlay[x][y].Count - 1;
        Tile obj = CurrentOverlay[x][y][layer];
        if (obj.Type.Symbol == ' ') continue;
        obj.Draw(true);
      }
    }
    Console.ReadKey(true);
    Clear();
    DrawMap();
    DrawOverlay();
  }
  #endregion Drawing

  #region Utilities
  internal static void Clear()
  {
    for (int y = Top + 1; y < (Top + Height); y++)
      new string(' ', Width - 2).WriteAt(Left + 1, y, ConsoleColor.Black, ConsoleColor.Black);
  }

  internal static void SetVisibleArea(int range)
  {
    // y then x
    Point dir = lookAt[Direction.NorthWest];
    SetVisibleTiles(range, dir);
    dir = lookAt[Direction.NorthEast];
    SetVisibleTiles(range, dir);
    dir = lookAt[Direction.SouthWest];
    SetVisibleTiles(range, dir);
    dir = lookAt[Direction.SouthEast];
    SetVisibleTiles(range, dir);

  }

  private static void SetVisibleTiles(int range, Point dir)
  {
    // y then x 
    int xLimit = Player.X + dir.X * range;
    int yLimit = Player.Y + dir.Y * range;
    for (int y = Player.Y; y != Player.Y + dir.Y * (range + 1); y += dir.Y)
    {
      if ((dir.Y < 0 && y < yLimit) || (dir.Y > 0 && y > yLimit)) break;
      Tile yObj = UpdateVisibleTiles(Player.X, y);
      if (yObj is { IsPassable: false, Type.IsTransparent: false }) yLimit = y;
      for (int x = Player.X + dir.X; x != Player.X + dir.X * (range + 1); x += dir.X)
      {
        if ((dir.X < 0 && x < xLimit) || (dir.X > 0 && x > xLimit)) break;
       Tile xObj = UpdateVisibleTiles(x, y);
        if (xObj is { IsPassable: false, Type.IsTransparent: false }) xLimit = x;
      }
    }

    // x then y
    xLimit = Player.X + dir.X * range;
    yLimit = Player.Y + dir.Y * range;
    for (int x = Player.X; x != Player.X + dir.X * (range + 1); x += dir.X)
    {
      if ((dir.X < 0 && x < xLimit) || (dir.X > 0 && x > xLimit)) break;
      Tile xObj = UpdateVisibleTiles(x, Player.Y);
      if (xObj is { IsPassable: false, Type.IsTransparent: false }) xLimit = x;
      for (int y = Player.Y + dir.Y; y != Player.Y + dir.Y * (range + 1); y += dir.Y)
      {
        if ((dir.Y < 0 && y < yLimit) || (dir.Y > 0 && y > yLimit)) break;
        Tile yObj = UpdateVisibleTiles(x, y);
        if (yObj is { IsPassable: false, Type.IsTransparent: false }) yLimit = y;
      }
    }
  }

  private static Tile UpdateVisibleTiles(int x, int y)
  {
    Tile obj = CurrentMap[x][y];
    obj.IsVisible = true;
    AddToMapTiles(obj);
    obj.Draw();
    AddToMapTiles(obj);

    int layer = CurrentOverlay[x][y].Count - 1;
    Tile overlay = CurrentOverlay[x][y][layer];
    if (overlay.Type.Symbol == ' ') return obj;
    overlay.IsVisible = true;
    overlay.Draw();
    return obj;
  }

  public static void WhatIsVisible()
  {
    bool visibleChanged = false;
    foreach (char symbol in levelMapTiles[Game.CurrentLevel].Keys)
    {
      if (symbol == ' ') continue;
      TileType type = MapTypes.Find(t => t.Symbol == symbol) ?? new TileType();
      if (type.Symbol == ' ') continue;
      visibleChanged = GetTileTypeCount(levelMapTiles[Game.CurrentLevel][symbol], visibleChanged, type);
    }
    foreach (char symbol in levelMapTiles[Game.CurrentLevel].Keys)
    {
      if (symbol == Player.Type.Symbol || symbol == ' ') continue;
      TileType type = overlayTypes.Find(t => t.Symbol == symbol) ?? new TileType();
      if (type.Symbol == ' ') continue;
      visibleChanged = GetTileTypeCount(LevelOverlayTiles[Game.CurrentLevel][symbol], visibleChanged, type);
    }
    if (visibleChanged) DescribeVisibleTiles();
  }

  private static void DescribeVisibleTiles()
  {
    string message = "";
    foreach (char symbol in levelVisibleObjects[Game.CurrentLevel].Keys)
    {
      TileType type = levelVisibleObjects[Game.CurrentLevel][symbol].Item1;
      int count = levelVisibleObjects[Game.CurrentLevel][symbol].Item2;
      if (count < 1) continue;
      if (count == 1) message += $"{type.Singular}, ";
      else message += $"{type.Plural} ({count}), ";
    }
    if (message.Length > 0) message = message.Substring(0, message.Length - 2);
    GamePlay.Messages.Add(new Message($"You see {message}.", Color.White, Color.Black));
  }

  private static bool GetTileTypeCount(List<Tile> list, bool visibleChanged, TileType type)
  {
    int count = 0;
    foreach (Tile obj in list) if (obj.IsVisible) count++;

    if (count < 1)
    {
      if (levelVisibleObjects[Game.CurrentLevel].ContainsKey(type.Symbol))
      {
        levelVisibleObjects[Game.CurrentLevel].Remove(type.Symbol);
        visibleChanged = true;
      }

      return visibleChanged;
    }

    if (levelVisibleObjects[Game.CurrentLevel].ContainsKey(type.Symbol))
    {
      if (levelVisibleObjects[Game.CurrentLevel][type.Symbol].Item2 == count) return visibleChanged;
      levelVisibleObjects[Game.CurrentLevel][type.Symbol] = new Tuple<TileType, int>(type, count);
      visibleChanged = true;
    }
    else
    {
      levelVisibleObjects[Game.CurrentLevel].Add(type.Symbol, new Tuple<TileType, int>(type, count));
      visibleChanged = true;
    }
    return visibleChanged;
  }

  internal static void AddToMapTiles(Tile obj)
  {
    if (levelMapTiles[Game.CurrentLevel].ContainsKey(obj.Type.Symbol))
    {
      if (!levelMapTiles[Game.CurrentLevel][obj.Type.Symbol].Contains(obj))
        levelMapTiles[Game.CurrentLevel][obj.Type.Symbol].Add(obj);
    }
    else
    {
      levelMapTiles[Game.CurrentLevel].Add(obj.Type.Symbol, new List<Tile>());
      levelMapTiles[Game.CurrentLevel][obj.Type.Symbol].Add(obj);
    }
  }

  internal static void RemoveFromMapTiles(Tile obj)
  {
    if (!levelMapTiles[Game.CurrentLevel].ContainsKey(obj.Type.Symbol)) return;
    if (levelMapTiles[Game.CurrentLevel][obj.Type.Symbol].Contains(obj))
      levelMapTiles[Game.CurrentLevel][obj.Type.Symbol].Remove(obj);
  }

  internal static void UpdateOverlayTile(Tile obj)
  {
    RemoveFromOverlayTiles(obj);
    RemoveFromOverlayGrid(obj);
    GamePlay.OverlaySection();
  }

  internal static void RemoveFromOverlayTiles(Tile obj)
  {
    if (!LevelOverlayTiles[Game.CurrentLevel].ContainsKey(obj.Type.Symbol)) return;
    if (LevelOverlayTiles[Game.CurrentLevel][obj.Type.Symbol].Contains(obj))
      LevelOverlayTiles[Game.CurrentLevel][obj.Type.Symbol].Remove(obj);
  }

  private static void RemoveFromOverlayGrid(Tile obj)
  {
    if (obj is Monster) return;
    Tile newObj = new(obj.X, obj.Y, new TileType(true));
    if (!CurrentOverlay[obj.X][obj.Y].Contains(obj)) return;
    CurrentOverlay[obj.X][obj.Y].Remove(obj);
    if (CurrentOverlay[obj.X][obj.Y].Count < 1)
      CurrentOverlay[obj.X][obj.Y].Add(newObj);
  }

  internal static Direction GetDirection(ConsoleKey key)
  {
    switch (key)
    {
      case ConsoleKey.W:
        return Direction.North;
      case ConsoleKey.S:
        return Direction.South;
      case ConsoleKey.A:
        return Direction.West;
      case ConsoleKey.D:
        return Direction.East;
      default:
        return Direction.None;
    }
  }

  internal static Direction GetDirection(Position from, Position to)
  {
    if (from.X < to.X) return Direction.East;
    if (from.X > to.X) return Direction.West;
    if (from.Y < to.Y) return Direction.South;
    if (from.Y > to.Y) return Direction.North;
    return Direction.None;
  }
  #endregion Utilities
}