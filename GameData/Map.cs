
using System;
using System.Drawing;
using System.Text;
using ConsoleDungeonCrawler.Extensions;

namespace ConsoleDungeonCrawler.GameData
{
  internal class Map
  {

    internal static Map Instance = new Map();
    internal int Top { get; set; }
    internal int Left { get; set; }
    internal int Width { get; set; }
    internal int Height { get; set; }

    internal static List<ObjectType> MapTypes = new List<ObjectType>();
    internal static List<ObjectType> OverlayTypes = new List<ObjectType>();
    internal static Dictionary<int, Dictionary<int, MapObject>> MapGrid = new Dictionary<int, Dictionary<int, MapObject>>();
    internal static Dictionary<int, Dictionary<int, MapObject>> OverlayGrid = new Dictionary<int, Dictionary<int, MapObject>>();
    internal static Dictionary<char, List<MapObject>> OverlayObjects = new Dictionary<char, List<MapObject>>();
    
    internal static MapObject Player= new MapObject();

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
      LoadMapGridFromFile("MapTemplate.txt");
      LoadOverlayFromFile("MapPlacement.txt");
      Player = OverlayObjects['P'][0];
    }

    internal void InitTypeLists()
    {
      // These are for building the map
      MapTypes = new List<ObjectType>
      {
        //new() { Symbol = '#', Name = "Wall", ForegroundColor = Color.White, BackgroundColor = Color.FromArgb(255,30,30,30), IsPassable = false },
        new() { Symbol = '#', Name = "Wall", ForegroundColor = Color.FromArgb(255,30,30,30), BackgroundColor = Color.FromArgb(255,30,30,30), IsPassable = false },
        new() { Symbol = '.', Name = "Floor", ForegroundColor = Color.Gray, BackgroundColor = Color.DimGray, IsPassable = true },
        new() { Symbol = '+', Name = "DoorC", ForegroundColor = Color.Yellow, BackgroundColor = Color.DimGray, IsPassable = false },
        new() { Symbol = '-', Name = "DoorO", ForegroundColor = Color.Yellow, BackgroundColor = Color.DimGray, IsPassable = true },
        new() { Symbol = '>', Name = "StairsU", ForegroundColor = Color.White, BackgroundColor = Color.DimGray, IsPassable = true },
        new() { Symbol = '<', Name = "StairsD", ForegroundColor = Color.White, BackgroundColor = Color.DimGray, IsPassable = true },
        new() { Symbol = '!', Name = "Fire", ForegroundColor = Color.OrangeRed, BackgroundColor = Color.DimGray, IsPassable = false },
        new() { Symbol = '~', Name = "Water", ForegroundColor = Color.Aqua, BackgroundColor = Color.Aqua, IsPassable = false },
        new() { Symbol = 'a', Name = "Acid", ForegroundColor = Color.SaddleBrown, BackgroundColor = Color.Chartreuse, IsPassable = false },
        new() { Symbol = 'L', Name = "Lava", ForegroundColor = Color.PapayaWhip, BackgroundColor = Color.Goldenrod, IsPassable = false },
        new() { Symbol = 'I', Name = "Ice", ForegroundColor = Color.Blue, BackgroundColor = Color.DeepSkyBlue, IsPassable = false }
      };

      // These are for placing objects on the map.
      OverlayTypes = new List<ObjectType>
      {
        new() { Symbol = 'X', Name = "Exit", ForegroundColor = Color.MidnightBlue, BackgroundColor = Color.Gold, IsPassable = true },
        new() { Symbol = 'S', Name = "Start", ForegroundColor = Color.Black, BackgroundColor = Color.White, IsPassable = true },
        new() { Symbol = 'U', Name = "Chest", ForegroundColor = Color.Silver, BackgroundColor = Color.DimGray, IsPassable = true, IsLootable = true},
        new() { Symbol = 'P', Name = "Player", ForegroundColor = Color.White, BackgroundColor = Color.DimGray, IsPassable = true, IsAttackable = true },
        new() { Symbol = 'O', Name = "Ogre", ForegroundColor = Color.Chocolate, BackgroundColor = Color.DimGray, IsPassable = false, IsAttackable = true },
        new() { Symbol = 'k', Name = "Kobald", ForegroundColor = Color.BlueViolet, BackgroundColor = Color.DimGray, IsPassable = false, IsAttackable = true },
        new() { Symbol = 'z', Name = "Ooze", ForegroundColor = Color.GreenYellow, BackgroundColor = Color.DimGray, IsPassable = false, IsAttackable = true },
        new() { Symbol = 'g', Name = "Goblin", ForegroundColor = Color.CadetBlue, BackgroundColor = Color.DimGray, IsPassable = false, IsAttackable = true },
        new() { Symbol = 'M', Name = "Boss", ForegroundColor = Color.Maroon, BackgroundColor = Color.Yellow, IsPassable = false, IsAttackable = true },
        new() { Symbol = 't', Name = "Item", ForegroundColor = Color.White, BackgroundColor = Color.DimGray, IsPassable = true, IsLootable = true },
        new() { Symbol = '$', Name = "Gold", ForegroundColor = Color.Gold, BackgroundColor = Color.DimGray, IsPassable = true, IsLootable = true },
        new() { Symbol = 'x', Name = "Trap", ForegroundColor = Color.LightSalmon, BackgroundColor = Color.DimGray, IsPassable = false }
      };
    }

    internal void InitDictionaries()
    {
      MapGrid = new Dictionary<int, Dictionary<int, MapObject>>();
      for (int x = 0; x < Game.MapBox.Width - 1; x++)
      {
        MapGrid.Add(x, new Dictionary<int, MapObject>());
        for (int y = 0; y < Game.MapBox.Height - 1; y++)
        {
          MapGrid[x].Add(y, new MapObject(x, y));
        }
      }

      OverlayGrid = new Dictionary<int, Dictionary<int, MapObject>>();
      for (int x = 0; x < Game.MapBox.Width - 1; x++)
      {
        OverlayGrid.Add(x, new Dictionary<int, MapObject>());
        for (int y = 0; y < Game.MapBox.Height - 1; y++)
        {
          OverlayGrid[x].Add(y, new MapObject(x, y));
        }
      }

      OverlayObjects = new Dictionary<char, List<MapObject>>();
      foreach (ObjectType objectType in OverlayTypes)
        OverlayObjects.Add(objectType.Symbol, new List<MapObject>());

    }

    internal void DrawMap()
    {
      foreach (int x in MapGrid.Keys)
      {
        foreach (int y in MapGrid[x].Keys)
        {
          MapObject obj = MapGrid[x][y];
          if (!obj.Visible || obj.Type.Symbol == ' ') continue;
          ConsoleEx.WriteAt(obj.Type.Symbol, obj.x + Left, obj.y + Top, obj.Type.ForegroundColor, obj.Type.BackgroundColor);
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
          if (!obj.Visible || obj.Type.Symbol == ' ') continue;
          ConsoleEx.WriteAt(obj.Type.Symbol, obj.x + Left, obj.y + Top, obj.Type.ForegroundColor, obj.Type.BackgroundColor);
        }
      }
    }

    internal static void LoadMapGridFromFile(string filename)
    {
      StringBuilder sb = new StringBuilder();
      sb.Append(File.ReadAllText(filename));
      string[] lines = sb.ToString().Split('\n');
      for (int y = 1; y <= Game.MapBox.Height; y++)
      {
        string line = lines[y];
        for (int x = 1; x <= Game.MapBox.Width; x++)
        {
          char c = line[x];
          // find the object type where the symbol matches the string
          ObjectType? type = MapTypes.Find(t => t.Symbol == c);
          if (type == null) continue;
          MapObject obj = new MapObject(x-1, y-1, type, false);
          MapGrid[x-1][y-1] = obj;
        }
      }
    }

    internal static void LoadOverlayFromFile(string filename)
    {
      StringBuilder sb = new StringBuilder();
      sb.Append(File.ReadAllText(filename));
      string[] lines = sb.ToString().Split('\n');
      for (int y = 1; y <= Game.MapBox.Height; y++)
      {
        string line = lines[y];
        for (int x = 1; x <= Game.MapBox.Width; x++)
        {
          char c = line[x];
          // find the object type where the symbol matches the string
          ObjectType? type = OverlayTypes.Find(t => t.Symbol == c);
          if (type == null) continue;
          MapObject obj = new MapObject(x - 1, y - 1, type, type.Symbol is 'P' or 'S');
          OverlayGrid[x - 1][y - 1] = obj;
          OverlayObjects[type.Symbol].Add(obj);
        }
      }
    }

    // Utilities

    internal static bool CanMoveTo(int x, int y)
    {
      // check to see if there is an object there that is not passable
      return MapGrid[x][y].Type.IsPassable || OverlayGrid[x][y].Type.IsAttackable;
    }

    internal static bool IsPlayerNextTo(char symbol, out MapObject obj)
    {
      // look left
      if (Player.x > 0 && MapGrid[Player.x - 1][Player.y].Type.Symbol == symbol)
      {
        obj = MapGrid[Player.x - 1][Player.y];
        return true;
      }
      // look right
      if(Player.x < Game.MapBox.Width && MapGrid[Player.x + 1][Player.y].Type.Symbol == symbol)
      {
        obj = MapGrid[Player.x + 1][Player.y];
        return true;
      }
      // look up
      if (Player.y > 0 && MapGrid[Player.x][Player.y - 1].Type.Symbol == symbol)
      {
        obj = MapGrid[Player.x][Player.y - 1];
        return true;
      }
      // look down
      if (Player.y >= Game.MapBox.Height || MapGrid[Player.x][Player.y + 1].Type.Symbol == symbol)
      {
        obj = MapGrid[Player.x][Player.y + 1];
        return true;
      }
      // not found
      obj = null;
      return false;
    }

    internal static bool CanAttack(int x, int y)
    {
      // check to see if there is an object there that is not passable
      return OverlayGrid[x][y].Type.IsAttackable;
    }

    internal static bool CanLoot(int x, int y)
    {
      // check to see if there is an object there that is not passable
      return OverlayGrid[x][y].Type.IsLootable;
    }

    internal static void SetVisibleArea(int range)
    {
      // up and left
      int xLimit = Player.x - range;
      int yLimit = Player.y - range;
      for (int y = Player.y; y > Player.y - range; y--)
      {
        if (y < yLimit) break;
        if (MapGrid[Player.x][y].Type.IsPassable)
          MapGrid[Player.x][y].Visible = true;
        else
        {
          MapGrid[Player.x][y].Visible = true;
          yLimit = y;
        }
        if (OverlayGrid[Player.x][y].Type.Symbol != ' ')
          OverlayGrid[Player.x][y].Visible = true;

        for (int x = Player.x - 1; x > Player.x - range; x--)
        {
          if (x < xLimit) break;
          if (MapGrid[x][y].Type.IsPassable)
            MapGrid[x][y].Visible = true;
          else
          {
            MapGrid[x][y].Visible = true;
            xLimit = x;
          }
          if (OverlayGrid[x][y].Type.Symbol != ' ')
            OverlayGrid[x][y].Visible = true;
        }
      }
      // left and up
      xLimit = Player.x - range;
      yLimit = Player.y - range;
      for (int x = Player.x; x > Player.x - range; x--)
      {
        if (x < xLimit) break;
        if (MapGrid[x][Player.y].Type.IsPassable)
          MapGrid[x][Player.y].Visible = true;
        else
        {
          MapGrid[x][Player.y].Visible = true;
          xLimit = x;
        }
        if (OverlayGrid[x][Player.y].Type.Symbol != ' ')
          OverlayGrid[x][Player.y].Visible = true;

        for (int y = Player.y - 1; y > Player.y - range; y--)
        {
          if (y < yLimit) break;
          if (MapGrid[x][y].Type.IsPassable)
            MapGrid[x][y].Visible = true;
          else
          {
            MapGrid[x][y].Visible = true;
            yLimit = y;
          }
          if (OverlayGrid[x][y].Type.Symbol != ' ')
            OverlayGrid[x][y].Visible = true;
        }
      }
      // up and right
      xLimit = Player.x + range;
      yLimit = Player.y - range;
      for (int y = Player.y; y > Player.y - range; y--)
      {
        if (y < yLimit) break;
        if (MapGrid[Player.x][y].Type.IsPassable)
          MapGrid[Player.x][y].Visible = true;
        else
        {
          MapGrid[Player.x][y].Visible = true;
          yLimit = y;
        }
        if (OverlayGrid[Player.x][y].Type.Symbol != ' ')
          OverlayGrid[Player.x][y].Visible = true;

        for (int x = Player.x + 1; x < Player.x + range; x++)
        {
          if (x > xLimit) break;
          if (MapGrid[x][y].Type.IsPassable)
            MapGrid[x][y].Visible = true;
          else
          {
            MapGrid[x][y].Visible = true;
            xLimit = x;
          }
          if (OverlayGrid[x][y].Type.Symbol != ' ')
            OverlayGrid[x][y].Visible = true;
        }
      }
      // right and up
      xLimit = Player.x + range;
      yLimit = Player.y - range;
      for (int x = Player.x; x < Player.x + range; x++)
      {
        if (x > xLimit) break;
        if (MapGrid[x][Player.y].Type.IsPassable)
          MapGrid[x][Player.y].Visible = true;
        else
        {
          MapGrid[x][Player.y].Visible = true;
          xLimit = x;
        }
        if (OverlayGrid[x][Player.y].Type.Symbol != ' ')
          OverlayGrid[x][Player.y].Visible = true;

        for (int y = Player.y - 1; y > Player.y - range; y--)
        {
          if (y < yLimit) break;
          if (MapGrid[x][y].Type.IsPassable)
            MapGrid[x][y].Visible = true;
          else
          {
            MapGrid[x][y].Visible = true;
            yLimit = y;
          }
          if (OverlayGrid[x][y].Type.Symbol != ' ')
            OverlayGrid[x][y].Visible = true;
        }
      }
      // down and left
      xLimit = Player.x - range;
      yLimit = Player.y + range;
      for (int y = Player.y; y < Player.y + range; y++)
      {
        if (y > yLimit) break;
        if (MapGrid[Player.x][y].Type.IsPassable)
          MapGrid[Player.x][y].Visible = true;
        else
        {
          MapGrid[Player.x][y].Visible = true;
          yLimit = y;
        }
        if (OverlayGrid[Player.x][y].Type.Symbol != ' ')
          OverlayGrid[Player.x][y].Visible = true;

        for (int x = Player.x - 1; x > Player.x - range; x--)
        {
          if (x < xLimit) break;
          if (MapGrid[x][y].Type.IsPassable)
            MapGrid[x][y].Visible = true;
          else
          {
            MapGrid[x][y].Visible = true;
            xLimit = x;
          }
          if (OverlayGrid[x][y].Type.Symbol != ' ')
            OverlayGrid[x][y].Visible = true;
        }
      }
      // left and down
      xLimit = Player.x - range;
      yLimit = Player.y + range;
      for (int x = Player.x; x > Player.x - range; x--)
      {
        if (x < xLimit) break;
        if (MapGrid[x][Player.y].Type.IsPassable)
          MapGrid[x][Player.y].Visible = true;
        else
        {
          MapGrid[x][Player.y].Visible = true;
          xLimit = x;
        }
        if (OverlayGrid[x][Player.y].Type.Symbol != ' ')
          OverlayGrid[x][Player.y].Visible = true;

        for (int y = Player.y + 1; y < Player.y + range; y++)
        {
          if (y > yLimit) break;
          if (MapGrid[x][y].Type.IsPassable)
            MapGrid[x][y].Visible = true;
          else
          {
            MapGrid[x][y].Visible = true;
            yLimit = y;
          }
          if (OverlayGrid[x][y].Type.Symbol != ' ')
            OverlayGrid[x][y].Visible = true;
        }
      }
      // down and right
      xLimit = Player.x + range;
      yLimit = Player.y + range;
      for (int y = Player.y; y < Player.y + range; y++)
      {
        if (y > yLimit) break;
        if (MapGrid[Player.x][y].Type.IsPassable)
          MapGrid[Player.x][y].Visible = true;
        else
        {
          MapGrid[Player.x][y].Visible = true;
          yLimit = y;
        }
        if (OverlayGrid[Player.x][y].Type.Symbol != ' ')
          OverlayGrid[Player.x][y].Visible = true;

        for (int x = Player.x + 1; x < Player.x + range; x++)
        {
          if (x > xLimit) break;
          if (MapGrid[x][y].Type.IsPassable)
            MapGrid[x][y].Visible = true;
          else
          {
            MapGrid[x][y].Visible = true;
            xLimit = x;
          }
          if (OverlayGrid[x][y].Type.Symbol != ' ')
            OverlayGrid[x][y].Visible = true;
        }
      }
      // right and down
      xLimit = Player.x + range;
      yLimit = Player.y + range;
      for (int x = Player.x; x < Player.x + range; x++)
      {
        if (x > xLimit) break;
        if (MapGrid[x][Player.y].Type.IsPassable)
          MapGrid[x][Player.y].Visible = true;
        else
        {
          MapGrid[x][Player.y].Visible = true;
          xLimit = x;
        }
        if (OverlayGrid[x][Player.y].Type.Symbol != ' ')
          OverlayGrid[x][Player.y].Visible = true;

        for (int y = Player.y + 1; y < Player.y + range; y++)
        {
          if (y > yLimit) break;
          if (MapGrid[x][y].Type.IsPassable)
            MapGrid[x][y].Visible = true;
          else
          {
            MapGrid[x][y].Visible = true;
            yLimit = y;
          }
          if (OverlayGrid[x][y].Type.Symbol != ' ')
            OverlayGrid[x][y].Visible = true;
        }
      }
    }
  }



  internal class MapObject
  {
    // Note:  x and y are in relation to the map area, not the console screen.
    // We will add the x and y position of the map area to these

    /// <summary>
    /// the x coordinate within the map area
    /// </summary>
    internal int x { get; set; }

    /// <summary>
    /// the y coordinate within the map area
    /// </summary>
    internal int y { get; set; }

    internal ObjectType Type = new ObjectType();

    internal bool Visible = true;


    internal MapObject()
    {
    }

    internal MapObject(int x, int y)
    {
      this.x = x;
      this.y = y;
    }

    internal MapObject(int x, int y, ObjectType type)
    {
      this.x = x;
      this.y = y;
      Type = type;
    }

    internal MapObject(int x, int y, ObjectType type, bool isVisible)
    {
      this.x = x;
      this.y = y;
      Type = type;
      Visible = isVisible;
    }
  }

  internal class ObjectType
  {
    internal char Symbol = ' ';
    internal string Name = "Empty";
    internal Color ForegroundColor = Color.Black;
    internal Color BackgroundColor = Color.Black;
    internal bool IsPassable = false;
    internal bool IsVisible = true;
    internal bool IsAttackable = false;
    internal bool IsLootable = false;
  }
}
