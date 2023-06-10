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
        new('#', "Wall", "a wall", "some walls", Color.FromArgb(255, 40, 40, 40), Color.FromArgb(255, 40, 40, 40), false, false, false),
        new('.', "Floor", "a floor", "some flooring", Color.Gray, Color.DimGray, true, false, false),
        new('+', "DoorC", "a closed door", "some closed doors", Color.Yellow, Color.DimGray, false, false, false),
        new('-', "DoorO", "an open door", "some open doors", Color.Yellow, Color.DimGray, true, false, false),
        new('>', "StairsU", "stairs going up", "multiple stairs going up", Color.White, Color.DimGray, true, false, false),
        new('<', "StairsD", "stairs going down", "multiple stairs going down", Color.White, Color.DimGray, true, false, false),
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
        new('m', "Chest", "a Chest", "some Chests", Color.Silver, Color.DimGray, true, false, false),
        new('i', "Item", "an Item", "some Items", Color.White, Color.DimGray, true, false, true),
        new('$', "Gold", "some Gold", "some stacks of Gold", Color.Gold, Color.DimGray, true, false, true),
        new('T', "Teleporter", "a Teleporter", "some Teleporters", Color.Gold, Color.DimGray, true, false, false),
        new('x', "Trap", "a Trap", "some Traps", Color.LightSalmon, Color.DimGray, false, false, false),
        new('P', "Player", "me", "am is seeing double?", Color.White, Color.DimGray, true, true, true),
        new('k', "Kobald", "a Kobald", "some Kobalds", Color.BlueViolet, Color.DimGray, false, true, true),
        new('z', "Ooze", "an Ooze", "some Oozes", Color.GreenYellow, Color.DimGray, false, true, true),
        new('g', "Goblin", " a Goblin", "some Goblins", Color.CadetBlue, Color.DimGray, false, true, true),
        new('O', "Ogre", "an Ogre", "Some Ogres", Color.Chocolate, Color.DimGray, false, true, true),
        new('B', "Boss", "a Boss", "some Bosses", Color.Maroon, Color.Yellow, false, true, true)
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
          else if (obj.IsAttackable)
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
      if (obj is Monster) return;
      MapObject newObj = new MapObject(obj.X, obj.Y, new ObjectType(true));
      Map.OverlayGrid[obj.X][obj.Y] = newObj;
    }
  }
}
