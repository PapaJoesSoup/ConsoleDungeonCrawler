using System.Drawing;
using System.Text;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Screens;

namespace ConsoleDungeonCrawler.Game.Entities
{
  internal class Map
  {
    internal static Map Instance = new();
    internal static int Top { get; set; }
    internal static int Left { get; set; }
    internal static int Width { get; set; }
    internal static int Height { get; set; }

    internal static List<ObjectType> MapTypes = new();
    internal static List<ObjectType> OverlayTypes = new();

    internal static readonly Dictionary<int, Dictionary<int, Dictionary<int, MapObject>>> LevelMapGrids = new();
    internal static readonly Dictionary<int, Dictionary<int, Dictionary<int, MapObject>>> LevelOverlayGrids = new();

    internal static Dictionary<int, Dictionary<char, List<MapObject>>> LevelMapObjects = new();
    internal static Dictionary<int, Dictionary<char, List<MapObject>>> LevelOverlayObjects = new();
    internal static Dictionary<int, Dictionary<char, Tuple<ObjectType, int>>> LevelVisibleObjects = new();

    internal static Player Player = new();
    internal static Char StartChar;
    internal static Char ExitChar;

    internal Map()
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
    }

    internal void InitTypeLists()
    {
      // These are for building the map
      MapTypes = new List<ObjectType>
      {
        new('#', "Wall", "a wall", "some walls", Color.FromArgb(255, 40, 40, 40), Color.FromArgb(255, 40, 40, 40), false, false, false),
        new('.', "Floor", "a floor", "some flooring", Color.Gray, Color.DimGray, true, false, false),
        new('+', "DoorC", "a closed door", "some closed doors", Color.Yellow, Color.DimGray, false, false, false),
        new('-', "DoorO", "an open door", "some open doors", Color.Yellow, Color.DimGray, true, false, false),
        new('\u25b2', "UpStairs", "stairs going up", "multiple stairs going up", Color.White, Color.DimGray, true, false, false),
        new('\u25bc', "DownStairs", "stairs going down", "multiple stairs going down", Color.White, Color.DimGray, true, false, false),
        new('!', "Fire", "a fire", "some fire", Color.OrangeRed, Color.DimGray, false, false, false),
        new('~', "Water", "some water", "some patches of water", Color.Aqua, Color.Aqua, false, false, false),
        new('a', "Acid", "some acid", "some patches of acid", Color.SaddleBrown, Color.Chartreuse, false, false, false),
        new('L', "Lava", "some lava", "some patches of lava", Color.PapayaWhip, Color.Goldenrod, false, false, false),
        new('I', "Ice", "some ice", "some patches of ice", Color.Blue, Color.DeepSkyBlue, false, false, false)
      };

      // These are for placing objects on the map.
      OverlayTypes = new List<ObjectType>
      {
        new('S', "Start", "the Entrance", "the Entrance", Color.Black, Color.White, true, false, false),
        new('X', "Exit", "the Exit", "The Exit", Color.MidnightBlue, Color.Gold, true, false, false),
        new('\u25b2', "UpStairs", "stairs going up", "multiple stairs going up", Color.White, Color.DimGray, true, false, false),
        new('\u25bc', "DownStairs", "stairs going down", "multiple stairs going down", Color.White, Color.DimGray, true, false, false),
        new('m', "Chest", "a Chest", "some Chests", Color.Silver, Color.DimGray, true, false, false),
        new('i', "Item", "an Item", "some Items", Color.White, Color.DimGray, true, false, true),
        new('$', "Gold", "some Gold", "some stacks of Gold", Color.Gold, Color.DimGray, true, false, true),
        new('T', "Teleporter", "a Teleporter", "some Teleporters", Color.Gold, Color.DimGray, true, false, false),
        new('x', "Trap", "a Trap", "some Traps", Color.LightSalmon, Color.DimGray, false, false, false),
        new('\u2640', "Player", "me", "am is seeing double?", Color.White, Color.DimGray, true, true, true),
        new('k', "Kobald", "a Kobald", "some Kobalds", Color.BlueViolet, Color.DimGray, false, true, true),
        new('z', "Ooze", "an Ooze", "some Oozes", Color.GreenYellow, Color.DimGray, false, true, true),
        new('g', "Goblin", " a Goblin", "some Goblins", Color.CadetBlue, Color.DimGray, false, true, true),
        new('O', "Ogre", "an Ogre", "Some Ogres", Color.Chocolate, Color.DimGray, false, true, true),
        new('B', "Boss", "a Boss", "some Bosses", Color.Maroon, Color.Yellow, false, true, true)
      };

      // Initialize Player and start char symbols
      Player.Type = OverlayTypes.Find(x => x.Name == "Player");
      StartChar = OverlayTypes.Find(x => x.Name == "Start").Symbol;
      ExitChar = OverlayTypes.Find(x => x.Name == "Exit").Symbol;
    }

    internal void InitDictionaries()
    {
      // load empty map/overlay grid for each level
      foreach (string level in Game.Dungeons[Game.CurrentDungeon].Keys)
      {
        int lvlNumber = int.Parse(level.Split('_')[1].Split('.')[0]);
        if (level.Contains("LevelMap_"))
        {
          // create empty map grid
          Dictionary<int, Dictionary<int, MapObject>> mapGrid = new();
          for (int x = 0; x <= Width; x++)
          {
            mapGrid.Add(x, new Dictionary<int, MapObject>());
            for (int y = 0; y <= Width; y++)
            {
              mapGrid[x].Add(y, new MapObject(x, y));
            }
          }
          // Add MapGrid to LevelMapGrids
          LevelMapGrids.Add(lvlNumber, mapGrid);
        }

        if (level.Contains("LevelOverlay_"))
        {
          // create empty overlay grid
          Dictionary<int, Dictionary<int, MapObject>> overlayGrid = new();
          ObjectType empty = new(true);
          for (int x = 0; x <= Width; x++)
          {
            overlayGrid.Add(x, new Dictionary<int, MapObject>());
            for (int y = 0; y <= Width; y++)
            {
              overlayGrid[x].Add(y, new MapObject(x, y, empty));
            }
          }
          // Add OverlayGrid to LevelOverlayGrids
          LevelOverlayGrids.Add(lvlNumber, overlayGrid);
        }
      }

      // Create empty object lists for each level
      LevelMapObjects = new Dictionary<int, Dictionary<char, List<MapObject>>>();
      foreach (string level in Game.Dungeons[Game.CurrentDungeon].Keys)
      {
        int lvlNumber = int.Parse(level.Split('_')[1].Split('.')[0]);
        if (LevelMapObjects.ContainsKey(lvlNumber)) continue;
        LevelMapObjects.Add(lvlNumber, new Dictionary<char, List<MapObject>>());
        foreach (ObjectType objectType in MapTypes)
          LevelMapObjects[lvlNumber].Add(objectType.Symbol, new List<MapObject>());
      }

      LevelOverlayObjects = new Dictionary<int, Dictionary<char, List<MapObject>>>();
      foreach (string level in Game.Dungeons[Game.CurrentDungeon].Keys)
      {
        int lvlNumber = int.Parse(level.Split('_')[1].Split('.')[0]);
        if (LevelOverlayObjects.ContainsKey(lvlNumber)) continue;
        LevelOverlayObjects.Add(lvlNumber, new Dictionary<char, List<MapObject>>());
        foreach (ObjectType objectType in OverlayTypes)
          LevelOverlayObjects[lvlNumber].Add(objectType.Symbol, new List<MapObject>());
      }

      LevelVisibleObjects = new Dictionary<int, Dictionary<char, Tuple<ObjectType, int>>>();
      foreach (string level in Game.Dungeons[Game.CurrentDungeon].Keys)
      {
        int lvlNumber = int.Parse(level.Split('_')[1].Split('.')[0]);
        if (LevelVisibleObjects.ContainsKey(lvlNumber)) continue;
        LevelVisibleObjects.Add(lvlNumber, new Dictionary<char, Tuple<ObjectType, int>>());
      }
    }

    internal static void LoadGameMaps()
    {
      // Load the map grids from the files
      foreach (string level in Game.Dungeons[Game.CurrentDungeon].Keys)
      {
        int lvlNumber = int.Parse(level.Split('_')[1].Split('.')[0]);
        if (level.Contains("LevelMap_")) LoadMapGridFromFile(lvlNumber, Game.Dungeons[Game.CurrentDungeon][level]);
        if (level.Contains("LevelOverlay_")) LoadOverlayGridFromFile(lvlNumber, Game.Dungeons[Game.CurrentDungeon][level]);
      }
    }

    internal static void LoadLevel()
    {
      ConsoleEx.Clear();
      GamePlay.Draw();
    }

    internal static void LoadMapGridFromFile(int level, string filename)
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
          ObjectType? type = MapTypes.Find(t => t.Symbol == c);
          if (type == null) continue;
          MapObject obj = new(x - 1, y - 1, type, false);
          LevelMapGrids[level][x - 1][y - 1] = obj;
        }
      }
    }

    internal static void LoadOverlayGridFromFile(int level, string filename)
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
          ObjectType? type = OverlayTypes.Find(t => t.Symbol == c);
          if (type == null) continue;
          MapObject obj = new(x - 1, y - 1, type, type.Symbol == Player.Type.Symbol || type.Symbol == StartChar);
          if (type.Symbol == Player.Type.Symbol)
          {
            Player = new Player(obj);
            LevelOverlayObjects[level][type.Symbol].Add(Player);
          }
          else if (obj.IsAttackable)
          {
            Monster monster = new(obj, 1);
            LevelOverlayGrids[level][x - 1][y - 1] = monster;
            LevelOverlayObjects[level][type.Symbol].Add(monster);
          }
          else
          {
            LevelOverlayGrids[level][x - 1][y - 1] = obj;
            LevelOverlayObjects[level][type.Symbol].Add(obj);
          }
        }
      }
    }

    internal static void DrawMap()
    {
      foreach (int x in LevelMapGrids[Game.CurrentLevel].Keys)
      {
        foreach (int y in LevelMapGrids[Game.CurrentLevel][x].Keys)
        {
          MapObject obj = LevelMapGrids[Game.CurrentLevel][x][y];
          if (!obj.IsVisible || obj.Type.Symbol == ' ') continue;
          obj.Draw();
        }
      }
    }

    internal static void DrawOverlay()
    {
      foreach (int x in LevelOverlayGrids[Game.CurrentLevel].Keys)
      {
        foreach (int y in LevelOverlayGrids[Game.CurrentLevel][x].Keys)
        {
          MapObject obj = LevelOverlayGrids[Game.CurrentLevel][x][y];
          if (!obj.IsVisible || obj.Type.Symbol == ' ') continue;
          obj.Draw();
        }
      }
    }

    internal static void ClearMapArea()
    {
      for (int y = Top + 1; y < (Top + Height); y++)
        new string(' ', Width - 2).WriteAt(Left + 1, y, ConsoleColor.Black, ConsoleColor.Black);
    }


    // Utilities
    internal static bool CanMoveTo(int x, int y)
    {
      // check to see if there is an object there that is not passable
      return LevelMapGrids[Game.CurrentLevel][x][y].IsPassable && LevelOverlayGrids[Game.CurrentLevel][x][y].IsPassable;
    }

    internal static bool CanJumpTo(int oldX, int oldY, int x, int y)
    {
      // check to see if there is an object in between old and new location that is not passable
      if (oldX == x)
      {
        // moving up or down
        int minY = Math.Min(oldY, y);
        int maxY = Math.Max(oldY, y);
        for (int i = minY; i <= maxY; i++)
        {
          if (!LevelMapGrids[Game.CurrentLevel][x][i].IsPassable || !LevelOverlayGrids[Game.CurrentLevel][x][i].IsPassable) return false;
        }
      }
      else if (oldY == y)
      {
        // moving left or right
        int minX = Math.Min(oldX, x);
        int maxX = Math.Max(oldX, x);
        for (int i = minX; i <= maxX; i++)
        {
          if (!LevelMapGrids[Game.CurrentLevel][i][y].IsPassable || !LevelOverlayGrids[Game.CurrentLevel][i][y].IsPassable) return false;
        }
      }
      return true;
    }

    internal static bool CanAttack(int x, int y)
    {
      // check to see if there is an object there that is attackable
      return LevelOverlayGrids[Game.CurrentLevel][x][y].Type.IsAttackable;
    }

    internal static bool CanLoot(int x, int y)
    {
      // check to see if there is an object there that is not passable
      return LevelOverlayGrids[Game.CurrentLevel][x][y].Type.IsLootable;
    }

    private static void SetVisibleYObjects(int x, int y, ref int yLimit)
    {
      if (LevelMapGrids[Game.CurrentLevel][x][y].IsPassable)
      {
        LevelMapGrids[Game.CurrentLevel][x][y].IsVisible = true;
        AddToMapObjects(LevelMapGrids[Game.CurrentLevel][x][y]);
        LevelMapGrids[Game.CurrentLevel][x][y].Draw();
      }
      else
      {
        LevelMapGrids[Game.CurrentLevel][x][y].IsVisible = true;
        AddToMapObjects(LevelMapGrids[Game.CurrentLevel][x][y]);
        LevelMapGrids[Game.CurrentLevel][x][y].Draw();
        yLimit = y;
      }

      if (LevelOverlayGrids[Game.CurrentLevel][x][y].Type.Symbol == ' ') return;
      LevelOverlayGrids[Game.CurrentLevel][x][y].IsVisible = true;
      AddToMapObjects(LevelMapGrids[Game.CurrentLevel][x][y]);
      LevelOverlayGrids[Game.CurrentLevel][x][y].Draw();
    }

    private static void SetVisibleXObjects(int x, int y, ref int xLimit)
    {
      if (LevelMapGrids[Game.CurrentLevel][x][y].IsPassable)
      {
        LevelMapGrids[Game.CurrentLevel][x][y].IsVisible = true;
        AddToMapObjects(LevelMapGrids[Game.CurrentLevel][x][y]);
        LevelMapGrids[Game.CurrentLevel][x][y].Draw();
      }
      else
      {
        LevelMapGrids[Game.CurrentLevel][x][y].IsVisible = true;
        AddToMapObjects(LevelMapGrids[Game.CurrentLevel][x][y]);
        LevelMapGrids[Game.CurrentLevel][x][y].Draw();
        xLimit = x;
      }

      if (LevelOverlayGrids[Game.CurrentLevel][x][y].Type.Symbol == ' ') return;
      LevelOverlayGrids[Game.CurrentLevel][x][y].IsVisible = true;
      LevelOverlayGrids[Game.CurrentLevel][x][y].Draw();
    }

    internal static void SetVisibleArea(int range)
    {
      // up and left
      int xLimit = Player.X - range;
      int yLimit = Player.Y - range;
      for (int y = Player.Y; y > Player.Y - range; y--)
      {
        if (y < yLimit) break;
        SetVisibleYObjects(Player.X, y, ref yLimit);
        for (int x = Player.X - 1; x > Player.X - range; x--)
        {
          if (x < xLimit) break;
          SetVisibleXObjects(x, y, ref xLimit);
        }
      }
      // left and up
      xLimit = Player.X - range;
      yLimit = Player.Y - range;
      for (int x = Player.X; x > Player.X - range; x--)
      {
        if (x < xLimit) break;
        SetVisibleXObjects(x, Player.Y, ref xLimit);
        for (int y = Player.Y - 1; y > Player.Y - range; y--)
        {
          if (y < yLimit) break;
          SetVisibleYObjects(x, y, ref yLimit);
        }
      }
      // up and right
      xLimit = Player.X + range;
      yLimit = Player.Y - range;
      for (int y = Player.Y; y > Player.Y - range; y--)
      {
        if (y < yLimit) break;
        SetVisibleYObjects(Player.X, y, ref yLimit);
        for (int x = Player.X + 1; x < Player.X + range; x++)
        {
          if (x > xLimit) break;
          SetVisibleXObjects(x, y, ref xLimit);
        }
      }
      // right and up
      xLimit = Player.X + range;
      yLimit = Player.Y - range;
      for (int x = Player.X; x < Player.X + range; x++)
      {
        if (x > xLimit) break;
        SetVisibleXObjects(x, Player.Y, ref xLimit);
        for (int y = Player.Y - 1; y > Player.Y - range; y--)
        {
          if (y < yLimit) break;
          SetVisibleYObjects(x, y, ref yLimit);
        }
      }
      // down and left
      xLimit = Player.X - range;
      yLimit = Player.Y + range;
      for (int y = Player.Y; y < Player.Y + range; y++)
      {
        if (y > yLimit) break;
        SetVisibleYObjects(Player.X, y, ref yLimit);
        for (int x = Player.X - 1; x > Player.X - range; x--)
        {
          if (x < xLimit) break;
          SetVisibleXObjects(x, y, ref xLimit);
        }
      }
      // left and down
      xLimit = Player.X - range;
      yLimit = Player.Y + range;
      for (int x = Player.X; x > Player.X - range; x--)
      {
        if (x < xLimit) break;
        SetVisibleXObjects(x, Player.Y, ref xLimit);
        for (int y = Player.Y + 1; y < Player.Y + range; y++)
        {
          if (y > yLimit) break;
          SetVisibleYObjects(x, y, ref yLimit);
        }
      }
      // down and right
      xLimit = Player.X + range;
      yLimit = Player.Y + range;
      for (int y = Player.Y; y < Player.Y + range; y++)
      {
        if (y > yLimit) break;
        SetVisibleYObjects(Player.X, y, ref yLimit);
        for (int x = Player.X + 1; x < Player.X + range; x++)
        {
          if (x > xLimit) break;
          SetVisibleXObjects(x, y, ref xLimit);
        }
      }
      // right and down
      xLimit = Player.X + range;
      yLimit = Player.Y + range;
      for (int x = Player.X; x < Player.X + range; x++)
      {
        if (x > xLimit) break;
        SetVisibleXObjects(x, Player.Y, ref xLimit);
        for (int y = Player.Y + 1; y < Player.Y + range; y++)
        {
          if (y > yLimit) break;
          SetVisibleYObjects(x, y, ref yLimit);
        }
      }
    }

    public static void WhatIsVisible()
    {
      bool visibleChanged = false;
      foreach (char symbol in LevelMapObjects[Game.CurrentLevel].Keys)
      {
        if (symbol == ' ') continue;
        ObjectType type = MapTypes.Find(t => t.Symbol == symbol) ?? new ObjectType();
        if (type.Symbol == ' ') continue;
        visibleChanged = GetObjectTypeCount(LevelMapObjects[Game.CurrentLevel][symbol], visibleChanged, type);
      }
      foreach (char symbol in LevelMapObjects[Game.CurrentLevel].Keys)
      {
        if (symbol == Player.Type.Symbol || symbol == ' ') continue;
        ObjectType type = OverlayTypes.Find(t => t.Symbol == symbol) ?? new ObjectType();
        if (type.Symbol == ' ') continue;
        visibleChanged = GetObjectTypeCount(LevelOverlayObjects[Game.CurrentLevel][symbol], visibleChanged, type);
      }
      if (visibleChanged) WriteVisibleObjects();
    }

    private static bool GetObjectTypeCount(List<MapObject> list, bool visibleChanged, ObjectType type)
    {
      int count = 0;
      foreach (MapObject obj in list) if (obj.IsVisible) count++;

      if (count < 1)
      {
        if (LevelVisibleObjects[Game.CurrentLevel].ContainsKey(type.Symbol))
        {
          LevelVisibleObjects[Game.CurrentLevel].Remove(type.Symbol);
          visibleChanged = true;
        }

        return visibleChanged;
      }

      if (LevelVisibleObjects[Game.CurrentLevel].ContainsKey(type.Symbol))
      {
        if (LevelVisibleObjects[Game.CurrentLevel][type.Symbol].Item2 == count) return visibleChanged;
        LevelVisibleObjects[Game.CurrentLevel][type.Symbol] = new Tuple<ObjectType, int>(type, count);
        visibleChanged = true;
      }
      else
      {
        LevelVisibleObjects[Game.CurrentLevel].Add(type.Symbol, new Tuple<ObjectType, int>(type, count));
        visibleChanged = true;
      }
      return visibleChanged;
    }

    internal static void WriteVisibleObjects()
    {
      string message = "";
      foreach (char symbol in LevelVisibleObjects[Game.CurrentLevel].Keys)
      {
        ObjectType type = LevelVisibleObjects[Game.CurrentLevel][symbol].Item1;
        int count = LevelVisibleObjects[Game.CurrentLevel][symbol].Item2;
        if (count < 1) continue;
        if (count == 1) message += $"{type.Singular}, ";
        else message += $"{type.Plural} ({count}), ";
      }
      if (message.Length > 0) message = message.Substring(0, message.Length - 2);
      GamePlay.Messages.Add(new Message($"You see {message}.", Color.White, Color.Black));
    }

    internal static void AddToMapObjects(MapObject obj)
    {
      if (LevelMapObjects[Game.CurrentLevel].ContainsKey(obj.Type.Symbol))
      {
        if (!LevelMapObjects[Game.CurrentLevel][obj.Type.Symbol].Contains(obj))
          LevelMapObjects[Game.CurrentLevel][obj.Type.Symbol].Add(obj);
      }
      else
      {
        LevelMapObjects[Game.CurrentLevel].Add(obj.Type.Symbol, new List<MapObject>());
        LevelMapObjects[Game.CurrentLevel][obj.Type.Symbol].Add(obj);
      }
    }

    internal static void RemoveFromMapObjects(MapObject obj)
    {
      if (!LevelMapObjects[Game.CurrentLevel].ContainsKey(obj.Type.Symbol)) return;
      if (LevelMapObjects[Game.CurrentLevel][obj.Type.Symbol].Contains(obj))
        LevelMapObjects[Game.CurrentLevel][obj.Type.Symbol].Remove(obj);
    }

    internal static void UpdateOverlayObject(MapObject obj)
    {
      RemoveFromOverlayObjects(obj);
      RemoveFromOverlayGrid(obj);
      GamePlay.OverlaySection();
    }

    internal static void RemoveFromOverlayObjects(MapObject obj)
    {
      if (!LevelOverlayObjects[Game.CurrentLevel].ContainsKey(obj.Type.Symbol)) return;
      if (LevelOverlayObjects[Game.CurrentLevel][obj.Type.Symbol].Contains(obj))
        LevelOverlayObjects[Game.CurrentLevel][obj.Type.Symbol].Remove(obj);
    }

    internal static void RemoveFromOverlayGrid(MapObject obj)
    {
      if (obj is Monster) return;
      MapObject newObj = new(obj.X, obj.Y, new ObjectType(true));
      LevelOverlayGrids[Game.CurrentLevel][obj.X][obj.Y] = newObj;
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

    internal static void ShowFullMap()
    {
      foreach (int x in LevelMapGrids[Game.CurrentLevel].Keys)
      {
        foreach (int y in LevelMapGrids[Game.CurrentLevel][x].Keys)
        {
          MapObject obj = LevelMapGrids[Game.CurrentLevel][x][y];
          if (obj.Type.Symbol == ' ') continue;
          obj.Draw(true);
        }
      }
      Console.ReadKey(true);
      ClearMapArea();
      DrawMap();
      DrawOverlay();
    }

    internal static void ShowFullOverlay()
    {
      foreach (int x in LevelOverlayGrids[Game.CurrentLevel].Keys)
      {
        foreach (int y in LevelOverlayGrids[Game.CurrentLevel][x].Keys)
        {
          MapObject obj = LevelOverlayGrids[Game.CurrentLevel][x][y];
          if (obj.Type.Symbol == ' ') continue;
          obj.Draw(true);
        }
      }
      Console.ReadKey(true);
      ClearMapArea();
      DrawMap();
      DrawOverlay();
    }
  }
}
