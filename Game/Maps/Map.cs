using System.Drawing;
using System.Text;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;
using ConsoleDungeonCrawler.Game.Screens;

namespace ConsoleDungeonCrawler.Game.Maps
{
  internal class Map
  {
    internal static Map Instance = new Map();
    internal static int Top { get; set; }
    internal static int Left { get; set; }
    internal static int Width { get; set; }
    internal static int Height { get; set; }

    internal static List<ObjectType> MapTypes = new List<ObjectType>();
    internal static List<ObjectType> OverlayTypes = new List<ObjectType>();
    internal static Dictionary<int, Dictionary<int, MapObject>> MapGrid = new Dictionary<int, Dictionary<int, MapObject>>();
    internal static Dictionary<int, Dictionary<int, MapObject>> OverlayGrid = new Dictionary<int, Dictionary<int, MapObject>>();
    internal static Dictionary<char, List<MapObject>> MapObjects = new Dictionary<char, List<MapObject>>();
    internal static Dictionary<char, List<MapObject>> OverlayObjects = new Dictionary<char, List<MapObject>>();

    internal static Player Player = new Player();

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
      LoadMapGridFromFile("Game/Maps/Data/MapGrid1.txt");
      LoadOverlayFromFile("Game/Maps/Data/MapOverlay1.txt");
    }

    internal void InitTypeLists()
    {
      // These are for building the map
      MapTypes = new List<ObjectType>
      {
        new() { Symbol = '#', Name = "Wall", Singular = "a wall", Plural = "some walls", ForegroundColor = Color.FromArgb(255,40,40,40), BackgroundColor = Color.FromArgb(255,40,40,40), IsPassable = false },
        new() { Symbol = '.', Name = "Floor", Singular = "a floor", Plural = "some flooring", ForegroundColor = Color.Gray, BackgroundColor = Color.DimGray, IsPassable = true },
        new() { Symbol = '+', Name = "DoorC", Singular = "a closed door", Plural = "some closed doors", ForegroundColor = Color.Yellow, BackgroundColor = Color.DimGray, IsPassable = false },
        new() { Symbol = '-', Name = "DoorO", Singular = "an open door", Plural = "some open doors", ForegroundColor = Color.Yellow, BackgroundColor = Color.DimGray, IsPassable = true },
        new() { Symbol = '>', Name = "StairsU", Singular = "stairs going up", Plural = "multiple stairs going up", ForegroundColor = Color.White, BackgroundColor = Color.DimGray, IsPassable = true },
        new() { Symbol = '<', Name = "StairsD", Singular = "stairs going down", Plural = "multiple stairs going down", ForegroundColor = Color.White, BackgroundColor = Color.DimGray, IsPassable = true },
        new() { Symbol = '!', Name = "Fire", Singular = "a fire", Plural = "some fire", ForegroundColor = Color.OrangeRed, BackgroundColor = Color.DimGray, IsPassable = false },
        new() { Symbol = '~', Name = "Water", Singular = "some water", Plural = "some patches of water", ForegroundColor = Color.Aqua, BackgroundColor = Color.Aqua, IsPassable = false },
        new() { Symbol = 'a', Name = "Acid", Singular = "some acid", Plural = "some patches of acid", ForegroundColor = Color.SaddleBrown, BackgroundColor = Color.Chartreuse, IsPassable = false },
        new() { Symbol = 'L', Name = "Lava", Singular = "some lava", Plural = "some patches of lava", ForegroundColor = Color.PapayaWhip, BackgroundColor = Color.Goldenrod, IsPassable = false },
        new() { Symbol = 'I', Name = "Ice", Singular = "some ice", Plural = "some patches of ice", ForegroundColor = Color.Blue, BackgroundColor = Color.DeepSkyBlue, IsPassable = false }
      };

      // These are for placing objects on the map.
      OverlayTypes = new List<ObjectType>
      {
        new() { Symbol = 'S', Name = "Start", Singular = "the Entrance", Plural = "the Entrance", ForegroundColor = Color.Black, BackgroundColor = Color.White, IsPassable = true },
        new() { Symbol = 'X', Name = "Exit", Singular = "the Exit", Plural = "The Exit", ForegroundColor = Color.MidnightBlue, BackgroundColor = Color.Gold, IsPassable = true },
        new() { Symbol = 'P', Name = "Player", Singular = "me", Plural = "am is seeing double?", ForegroundColor = Color.White, BackgroundColor = Color.DimGray, IsPassable = true, IsAttackable = true },
        new() { Symbol = 'O', Name = "Ogre", Singular = "an Ogre", Plural = "Some Ogres", ForegroundColor = Color.Chocolate, BackgroundColor = Color.DimGray, IsPassable = false, IsAttackable = true },
        new() { Symbol = 'k', Name = "Kobald", Singular = "a Kobald", Plural = "some Kobalds", ForegroundColor = Color.BlueViolet, BackgroundColor = Color.DimGray, IsPassable = false, IsAttackable = true },
        new() { Symbol = 'z', Name = "Ooze", Singular = "an Ooze", Plural = "some Oozes", ForegroundColor = Color.GreenYellow, BackgroundColor = Color.DimGray, IsPassable = false, IsAttackable = true },
        new() { Symbol = 'g', Name = "Goblin", Singular = " a Goblin", Plural = "some Goblins", ForegroundColor = Color.CadetBlue, BackgroundColor = Color.DimGray, IsPassable = false, IsAttackable = true },
        new() { Symbol = 'B', Name = "Boss", Singular = "a Boss", Plural = "some Bosses", ForegroundColor = Color.Maroon, BackgroundColor = Color.Yellow, IsPassable = false, IsAttackable = true },
        new() { Symbol = 'm', Name = "Chest", Singular = "a Chest", Plural = "some Chests", ForegroundColor = Color.Silver, BackgroundColor = Color.DimGray, IsPassable = false, IsLootable = true},
        new() { Symbol = 'i', Name = "Item", Singular = "an Item", Plural = "some Items", ForegroundColor = Color.White, BackgroundColor = Color.DimGray, IsPassable = false, IsLootable = true },
        new() { Symbol = '$', Name = "Gold", Singular = "some Gold", Plural = "some stacks of Gold", ForegroundColor = Color.Gold, BackgroundColor = Color.DimGray, IsPassable = false, IsLootable = true },
        new() { Symbol = 'T', Name = "Teleporter", Singular = "a Teleporter", Plural = "some Teleporters", ForegroundColor = Color.Gold, BackgroundColor = Color.DimGray, IsPassable = true, IsLootable = true },
        new() { Symbol = 'x', Name = "Trap", Singular = "a Trap", Plural = "some Traps", ForegroundColor = Color.LightSalmon, BackgroundColor = Color.DimGray, IsPassable = false }
      };
    }

    internal void InitDictionaries()
    {
      MapGrid = new Dictionary<int, Dictionary<int, MapObject>>();
      for (int x = 0; x <= Width; x++)
      {
        MapGrid.Add(x, new Dictionary<int, MapObject>());
        for (int y = 0; y <= Width; y++)
        {
          MapGrid[x].Add(y, new MapObject(x, y));
        }
      }

      OverlayGrid = new Dictionary<int, Dictionary<int, MapObject>>();
      ObjectType empty = new ObjectType(true);
      for (int x = 0; x <= Width; x++)
      {
        OverlayGrid.Add(x, new Dictionary<int, MapObject>());
        for (int y = 0; y <= Width; y++)
        {
          OverlayGrid[x].Add(y, new MapObject(x, y, empty));
        }
      }

      OverlayObjects = new Dictionary<char, List<MapObject>>();
      foreach (ObjectType objectType in OverlayTypes)
        OverlayObjects.Add(objectType.Symbol, new List<MapObject>());
    }

    internal static void LoadMapGridFromFile(string filename)
    {
      StringBuilder sb = new StringBuilder();
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
          MapObject obj = new MapObject(x - 1, y - 1, type, false);
          MapGrid[x - 1][y - 1] = obj;
        }
      }
    }

    internal static void LoadOverlayFromFile(string filename)
    {
      StringBuilder sb = new StringBuilder();
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
          MapObject obj = new MapObject(x - 1, y - 1, type, type.Symbol is 'P' or 'S');
          if (type.Symbol == 'P')
          {
            Map.Player = new Player(obj);
            OverlayObjects[type.Symbol].Add(Map.Player);
          }
          else if (obj.Type.IsAttackable)
          {
            Monster monster = new Monster(obj, 1);
            OverlayObjects[type.Symbol].Add(monster);
            OverlayGrid[x - 1][y - 1] = monster;
          }
          else
          {
            OverlayGrid[x - 1][y - 1] = obj;
            OverlayObjects[type.Symbol].Add(obj);
          }
        }
      }
    }

    internal void DrawMap()
    {
      foreach (int x in MapGrid.Keys)
      {
        foreach (int y in MapGrid[x].Keys)
        {
          MapObject obj = MapGrid[x][y];
          if (!obj.IsVisible || obj.Type.Symbol == ' ') continue;
          obj.Draw();
        }
      }
    }

    internal void DrawOverlay()
    {
      foreach (int x in OverlayGrid.Keys)
      {
        foreach (int y in OverlayGrid[x].Keys)
        {
          MapObject obj = OverlayGrid[x][y];
          if (!obj.IsVisible || obj.Type.Symbol == ' ') continue;
          obj.Draw();
        }
      }
    }


    // Utilities
    internal static bool CanMoveTo(int x, int y)
    {
      // check to see if there is an object there that is not passable
      return MapGrid[x][y].IsPassable && OverlayGrid[x][y].IsPassable;
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
          if (!MapGrid[x][i].IsPassable || !OverlayGrid[x][i].IsPassable) return false;
        }
      }
      else if (oldY == y)
      {
        // moving left or right
        int minX = Math.Min(oldX, x);
        int maxX = Math.Max(oldX, x);
        for (int i = minX; i <= maxX; i++)
        {
          if (!MapGrid[i][y].IsPassable || !OverlayGrid[i][y].IsPassable) return false;
        }
      }
      return true;
    }

    internal static bool CanAttack(int x, int y)
    {
      // check to see if there is an object there that is attackable
      return OverlayGrid[x][y].Type.IsAttackable;
    }

    internal static bool CanLoot(int x, int y)
    {
      // check to see if there is an object there that is not passable
      return OverlayGrid[x][y].Type.IsLootable;
    }

    private static void SetVisibleYObjects(int x, int y, ref int yLimit)
    {
      if (MapGrid[x][y].IsPassable)
      {
        MapGrid[x][y].IsVisible = true;
        AddToMapObjects(MapGrid[x][y]);
        MapGrid[x][y].Draw();
      }
      else
      {
        MapGrid[x][y].IsVisible = true;
        AddToMapObjects(MapGrid[x][y]);
        MapGrid[x][y].Draw();
        yLimit = y;
      }

      if (OverlayGrid[x][y].Type.Symbol == ' ') return;
      OverlayGrid[x][y].IsVisible = true;
      AddToMapObjects(MapGrid[x][y]);
      OverlayGrid[x][y].Draw();
    }

    private static void SetVisibleXObjects(int x, int y, ref int xLimit)
    {
      if (MapGrid[x][y].IsPassable)
      {
        MapGrid[x][y].IsVisible = true;
        AddToMapObjects(MapGrid[x][y]);
        MapGrid[x][y].Draw();
      }
      else
      {
        MapGrid[x][y].IsVisible = true;
        AddToMapObjects(MapGrid[x][y]);
        MapGrid[x][y].Draw();
        xLimit = x;
      }

      if (OverlayGrid[x][y].Type.Symbol == ' ') return;
      OverlayGrid[x][y].IsVisible = true;
      OverlayGrid[x][y].Draw();
    }

    internal static void SetVisibleArea(int range)
    {
      // up and left
      int xLimit = Map.Player.X - range;
      int yLimit = Map.Player.Y - range;
      for (int y = Map.Player.Y; y > Map.Player.Y - range; y--)
      {
        if (y < yLimit) break;
        SetVisibleYObjects(Map.Player.X, y, ref yLimit);
        for (int x = Map.Player.X - 1; x > Map.Player.X - range; x--)
        {
          if (x < xLimit) break;
          SetVisibleXObjects(x, y, ref xLimit);
        }
      }
      // left and up
      xLimit = Map.Player.X - range;
      yLimit = Map.Player.Y - range;
      for (int x = Map.Player.X; x > Map.Player.X - range; x--)
      {
        if (x < xLimit) break;
        SetVisibleXObjects(x, Map.Player.Y, ref xLimit);
        for (int y = Map.Player.Y - 1; y > Map.Player.Y - range; y--)
        {
          if (y < yLimit) break;
          SetVisibleYObjects(x, y, ref yLimit);
        }
      }
      // up and right
      xLimit = Map.Player.X + range;
      yLimit = Map.Player.Y - range;
      for (int y = Map.Player.Y; y > Map.Player.Y - range; y--)
      {
        if (y < yLimit) break;
        SetVisibleYObjects(Map.Player.X, y, ref yLimit);
        for (int x = Map.Player.X + 1; x < Map.Player.X + range; x++)
        {
          if (x > xLimit) break;
          SetVisibleXObjects(x, y, ref xLimit);
        }
      }
      // right and up
      xLimit = Map.Player.X + range;
      yLimit = Map.Player.Y - range;
      for (int x = Map.Player.X; x < Map.Player.X + range; x++)
      {
        if (x > xLimit) break;
        SetVisibleXObjects(x, Map.Player.Y, ref xLimit);
        for (int y = Map.Player.Y - 1; y > Map.Player.Y - range; y--)
        {
          if (y < yLimit) break;
          SetVisibleYObjects(x, y, ref yLimit);
        }
      }
      // down and left
      xLimit = Map.Player.X - range;
      yLimit = Map.Player.Y + range;
      for (int y = Map.Player.Y; y < Map.Player.Y + range; y++)
      {
        if (y > yLimit) break;
        SetVisibleYObjects(Map.Player.X, y, ref yLimit);
        for (int x = Map.Player.X - 1; x > Map.Player.X - range; x--)
        {
          if (x < xLimit) break;
          SetVisibleXObjects(x, y, ref xLimit);
        }
      }
      // left and down
      xLimit = Map.Player.X - range;
      yLimit = Map.Player.Y + range;
      for (int x = Map.Player.X; x > Map.Player.X - range; x--)
      {
        if (x < xLimit) break;
        SetVisibleXObjects(x, Map.Player.Y, ref xLimit);
        for (int y = Map.Player.Y + 1; y < Map.Player.Y + range; y++)
        {
          if (y > yLimit) break;
          SetVisibleYObjects(x, y, ref yLimit);
        }
      }
      // down and right
      xLimit = Map.Player.X + range;
      yLimit = Map.Player.Y + range;
      for (int y = Map.Player.Y; y < Map.Player.Y + range; y++)
      {
        if (y > yLimit) break;
        SetVisibleYObjects(Map.Player.X, y, ref yLimit);
        for (int x = Map.Player.X + 1; x < Map.Player.X + range; x++)
        {
          if (x > xLimit) break;
          SetVisibleXObjects(x, y, ref xLimit);
        }
      }
      // right and down
      xLimit = Map.Player.X + range;
      yLimit = Map.Player.Y + range;
      for (int x = Map.Player.X; x < Map.Player.X + range; x++)
      {
        if (x > xLimit) break;
        SetVisibleXObjects(x, Map.Player.Y, ref xLimit);
        for (int y = Map.Player.Y + 1; y < Map.Player.Y + range; y++)
        {
          if (y > yLimit) break;
          SetVisibleYObjects(x, y, ref yLimit);
        }
      }
    }

    public static void WhatIsVisible()
    {
      foreach (char symbol in Map.MapObjects.Keys)
      {
        if (symbol == ' ') continue;
        int count = 0;
        ObjectType type = Map.MapTypes.Find(t => t.Symbol == symbol) ?? new ObjectType();
        if (type.Symbol == ' ') continue;

        foreach (MapObject obj in Map.MapObjects[symbol])
          if (obj.IsVisible) count++;

        if (count < 1) continue;
        GamePlay.Messages.Add(
          new Message($"You see {(count < 2 ? type.Singular : type.Plural)} ({count})...", type.Symbol == '#'? Color.White : type.ForegroundColor, Color.Black));
      }

      foreach (char symbol in Map.OverlayObjects.Keys)
      {
        if (symbol == 'P' || symbol == ' ') continue;
        int count = 0;
        ObjectType type = Map.OverlayTypes.Find(t => t.Symbol == symbol) ?? new ObjectType();
        if (type.Symbol == ' ') continue;
        foreach (MapObject obj in Map.OverlayObjects[symbol])
          if (obj.IsVisible) count++;

        if (count < 1) continue;
        GamePlay.Messages.Add(new Message($"You see {(count < 2 ? type.Singular : type.Plural)} ({count})...", (type.Symbol == 'S' || type.Symbol == 'E') ? Color.White : type.ForegroundColor, Color.Black));
      }
    }

    internal static void AddToMapObjects(MapObject obj)
    {
      if (MapObjects.ContainsKey(obj.Type.Symbol))
      {
        if (!MapObjects[obj.Type.Symbol].Contains(obj))
          MapObjects[obj.Type.Symbol].Add(obj);
      }
      else
      {
        MapObjects.Add(obj.Type.Symbol, new List<MapObject>());
        MapObjects[obj.Type.Symbol].Add(obj);
      }
    }

    internal static void RemoveFromMapObjects(MapObject obj)
    {
      if (!MapObjects.ContainsKey(obj.Type.Symbol)) return;
      if (MapObjects[obj.Type.Symbol].Contains(obj))
        MapObjects[obj.Type.Symbol].Remove(obj);
    }

    internal static void UpdateOverlayObject(MapObject obj)
    {
      RemoveFromOverlayObjects(obj);
      RemoveFromOverlayGrid(obj);
      GamePlay.LegendSection();
    }

    internal static void RemoveFromOverlayObjects(MapObject obj)
    {
      if (!Map.OverlayObjects.ContainsKey(obj.Type.Symbol)) return;
      if (Map.OverlayObjects[obj.Type.Symbol].Contains(obj))
        Map.OverlayObjects[obj.Type.Symbol].Remove(obj);
    }

    internal static void RemoveFromOverlayGrid(MapObject obj)
    {
      MapObject newObj = new MapObject(obj.X, obj.Y, new ObjectType(true));
      Map.OverlayGrid[obj.X][obj.Y] = newObj;
    }
  }
}
