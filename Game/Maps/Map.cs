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
        internal static Dictionary<char, List<MapObject>> OverlayObjects = new Dictionary<char, List<MapObject>>();

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
            Player.OnMap = OverlayObjects['P'][0];
        }

        internal void InitTypeLists()
        {
            // These are for building the map
            MapTypes = new List<ObjectType>
      {
        new() { Symbol = '#', Name = "Wall", ForegroundColor = Color.FromArgb(255,40,40,40), BackgroundColor = Color.FromArgb(255,40,40,40), IsPassable = false },
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
        new() { Symbol = 'S', Name = "Start", ForegroundColor = Color.Black, BackgroundColor = Color.White, IsPassable = true },
        new() { Symbol = 'X', Name = "Exit", ForegroundColor = Color.MidnightBlue, BackgroundColor = Color.Gold, IsPassable = true },
        new() { Symbol = 'P', Name = "Player", ForegroundColor = Color.White, BackgroundColor = Color.DimGray, IsPassable = true, IsAttackable = true },
        new() { Symbol = 'O', Name = "Ogre", ForegroundColor = Color.Chocolate, BackgroundColor = Color.DimGray, IsPassable = false, IsAttackable = true },
        new() { Symbol = 'k', Name = "Kobald", ForegroundColor = Color.BlueViolet, BackgroundColor = Color.DimGray, IsPassable = false, IsAttackable = true },
        new() { Symbol = 'z', Name = "Ooze", ForegroundColor = Color.GreenYellow, BackgroundColor = Color.DimGray, IsPassable = false, IsAttackable = true },
        new() { Symbol = 'g', Name = "Goblin", ForegroundColor = Color.CadetBlue, BackgroundColor = Color.DimGray, IsPassable = false, IsAttackable = true },
        new() { Symbol = 'B', Name = "Boss", ForegroundColor = Color.Maroon, BackgroundColor = Color.Yellow, IsPassable = false, IsAttackable = true },
        new() { Symbol = 'm', Name = "Chest", ForegroundColor = Color.Silver, BackgroundColor = Color.DimGray, IsPassable = true, IsLootable = true},
        new() { Symbol = 'i', Name = "Item", ForegroundColor = Color.White, BackgroundColor = Color.DimGray, IsPassable = false, IsLootable = true },
        new() { Symbol = '$', Name = "Gold", ForegroundColor = Color.Gold, BackgroundColor = Color.DimGray, IsPassable = false, IsLootable = true },
        new() { Symbol = 'T', Name = "Teleporter", ForegroundColor = Color.Gold, BackgroundColor = Color.DimGray, IsPassable = true, IsLootable = true },
        new() { Symbol = 'x', Name = "Trap", ForegroundColor = Color.LightSalmon, BackgroundColor = Color.DimGray, IsPassable = false }
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
            ObjectType empty = new ObjectType();
            empty.IsPassable = true;
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
                    OverlayGrid[x - 1][y - 1] = obj;
                    OverlayObjects[type.Symbol].Add(obj);
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
                    if (!obj.Visible || obj.Type.Symbol == ' ') continue;
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
                    if (!obj.Visible || obj.Type.Symbol == ' ') continue;
                    obj.Draw();
                }
            }
        }


        // Utilities
        internal static bool CanMoveTo(int x, int y)
        {
            // check to see if there is an object there that is not passable
            return MapGrid[x][y].Type.IsPassable && OverlayGrid[x][y].Type.IsPassable;
        }

        internal static bool IsPlayerNextToMap(char symbol, out MapObject obj)
        {
            // look left
            if (Player.OnMap.X > 0 && MapGrid[Player.OnMap.X - 1][Player.OnMap.Y].Type.Symbol == symbol)
            {
                obj = MapGrid[Player.OnMap.X - 1][Player.OnMap.Y];
                return true;
            }
            // look right
            if (Player.OnMap.X < GamePlayScreen.MapBox.Width && MapGrid[Player.OnMap.X + 1][Player.OnMap.Y].Type.Symbol == symbol)
            {
                obj = MapGrid[Player.OnMap.X + 1][Player.OnMap.Y];
                return true;
            }
            // look up
            if (Player.OnMap.Y > 0 && MapGrid[Player.OnMap.X][Player.OnMap.Y - 1].Type.Symbol == symbol)
            {
                obj = MapGrid[Player.OnMap.X][Player.OnMap.Y - 1];
                return true;
            }
            // look down
            if (Player.OnMap.Y >= GamePlayScreen.MapBox.Height || MapGrid[Player.OnMap.X][Player.OnMap.Y + 1].Type.Symbol == symbol)
            {
                obj = MapGrid[Player.OnMap.X][Player.OnMap.Y + 1];
                return true;
            }
            // not found
            obj = new MapObject();
            return false;
        }

        internal static char IsPlayerNextToOverlay(out MapObject obj)
        {
            // look left
            if (Player.OnMap.X > 0 && OverlayGrid[Player.OnMap.X - 1][Player.OnMap.Y].Type.Symbol != ' ')
            {
                obj = OverlayGrid[Player.OnMap.X - 1][Player.OnMap.Y];
                return obj.Type.Symbol;
            }
            // look right
            if (Player.OnMap.X < GamePlayScreen.MapBox.Width && OverlayGrid[Player.OnMap.X + 1][Player.OnMap.Y].Type.Symbol != ' ')
            {
                obj = OverlayGrid[Player.OnMap.X + 1][Player.OnMap.Y];
                return obj.Type.Symbol;
            }
            // look up
            if (Player.OnMap.Y > 0 && OverlayGrid[Player.OnMap.X][Player.OnMap.Y - 1].Type.Symbol != ' ')
            {
                obj = OverlayGrid[Player.OnMap.X][Player.OnMap.Y - 1];
                return obj.Type.Symbol;
            }
            // look down
            if (Player.OnMap.Y >= GamePlayScreen.MapBox.Height || OverlayGrid[Player.OnMap.X][Player.OnMap.Y + 1].Type.Symbol != ' ')
            {
                obj = OverlayGrid[Player.OnMap.X][Player.OnMap.Y + 1];
                return obj.Type.Symbol;
            }
            // not found
            obj = new MapObject();
            return ' ';
        }

        internal static bool IsPlayerNextToOverlay(char symbol, out MapObject obj)
        {
            // look left
            if (Player.OnMap.X > 0 && OverlayGrid[Player.OnMap.X - 1][Player.OnMap.Y].Type.Symbol == symbol)
            {
                obj = OverlayGrid[Player.OnMap.X - 1][Player.OnMap.Y];
                return true;
            }
            // look right
            if (Player.OnMap.X < GamePlayScreen.MapBox.Width && OverlayGrid[Player.OnMap.X + 1][Player.OnMap.Y].Type.Symbol == symbol)
            {
                obj = OverlayGrid[Player.OnMap.X + 1][Player.OnMap.Y];
                return true;
            }
            // look up
            if (Player.OnMap.Y > 0 && OverlayGrid[Player.OnMap.X][Player.OnMap.Y - 1].Type.Symbol == symbol)
            {
                obj = OverlayGrid[Player.OnMap.X][Player.OnMap.Y - 1];
                return true;
            }
            // look down
            if (Player.OnMap.Y >= GamePlayScreen.MapBox.Height || OverlayGrid[Player.OnMap.X][Player.OnMap.Y + 1].Type.Symbol == symbol)
            {
                obj = OverlayGrid[Player.OnMap.X][Player.OnMap.Y + 1];
                return true;
            }
            // not found
            obj = new MapObject();
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

        private static void SetVisibleYObjects(int x, int y, ref int yLimit)
        {
            if (MapGrid[x][y].Type.IsPassable)
            {
                MapGrid[x][y].Visible = true;
                MapGrid[x][y].Draw();
            }
            else
            {
                MapGrid[x][y].Visible = true;
                MapGrid[x][y].Draw();
                yLimit = y;
            }

            if (OverlayGrid[x][y].Type.Symbol == ' ') return;
            OverlayGrid[x][y].Visible = true;
            OverlayGrid[x][y].Draw();
        }

        private static void SetVisibleXObjects(int x, int y, ref int xLimit)
        {
            if (MapGrid[x][y].Type.IsPassable)
            {
                MapGrid[x][y].Visible = true;
                MapGrid[x][y].Draw();
            }
            else
            {
                MapGrid[x][y].Visible = true;
                MapGrid[x][y].Draw();
                xLimit = x;
            }

            if (OverlayGrid[x][y].Type.Symbol == ' ') return;
            OverlayGrid[x][y].Visible = true;
            OverlayGrid[x][y].Draw();
        }

        internal static void SetVisibleArea(int range)
        {
            // up and left
            int xLimit = Player.OnMap.X - range;
            int yLimit = Player.OnMap.Y - range;
            for (int y = Player.OnMap.Y; y > Player.OnMap.Y - range; y--)
            {
                if (y < yLimit) break;
                SetVisibleYObjects(Player.OnMap.X, y, ref yLimit);
                for (int x = Player.OnMap.X - 1; x > Player.OnMap.X - range; x--)
                {
                    if (x < xLimit) break;
                    SetVisibleXObjects(x, y, ref xLimit);
                }
            }
            // left and up
            xLimit = Player.OnMap.X - range;
            yLimit = Player.OnMap.Y - range;
            for (int x = Player.OnMap.X; x > Player.OnMap.X - range; x--)
            {
                if (x < xLimit) break;
                SetVisibleXObjects(x, Player.OnMap.Y, ref xLimit);
                for (int y = Player.OnMap.Y - 1; y > Player.OnMap.Y - range; y--)
                {
                    if (y < yLimit) break;
                    SetVisibleYObjects(x, y, ref yLimit);
                }
            }
            // up and right
            xLimit = Player.OnMap.X + range;
            yLimit = Player.OnMap.Y - range;
            for (int y = Player.OnMap.Y; y > Player.OnMap.Y - range; y--)
            {
                if (y < yLimit) break;
                SetVisibleYObjects(Player.OnMap.X, y, ref yLimit);
                for (int x = Player.OnMap.X + 1; x < Player.OnMap.X + range; x++)
                {
                    if (x > xLimit) break;
                    SetVisibleXObjects(x, y, ref xLimit);
                }
            }
            // right and up
            xLimit = Player.OnMap.X + range;
            yLimit = Player.OnMap.Y - range;
            for (int x = Player.OnMap.X; x < Player.OnMap.X + range; x++)
            {
                if (x > xLimit) break;
                SetVisibleXObjects(x, Player.OnMap.Y, ref xLimit);
                for (int y = Player.OnMap.Y - 1; y > Player.OnMap.Y - range; y--)
                {
                    if (y < yLimit) break;
                    SetVisibleYObjects(x, y, ref yLimit);
                }
            }
            // down and left
            xLimit = Player.OnMap.X - range;
            yLimit = Player.OnMap.Y + range;
            for (int y = Player.OnMap.Y; y < Player.OnMap.Y + range; y++)
            {
                if (y > yLimit) break;
                SetVisibleYObjects(Player.OnMap.X, y, ref yLimit);
                for (int x = Player.OnMap.X - 1; x > Player.OnMap.X - range; x--)
                {
                    if (x < xLimit) break;
                    SetVisibleXObjects(x, y, ref xLimit);
                }
            }
            // left and down
            xLimit = Player.OnMap.X - range;
            yLimit = Player.OnMap.Y + range;
            for (int x = Player.OnMap.X; x > Player.OnMap.X - range; x--)
            {
                if (x < xLimit) break;
                SetVisibleXObjects(x, Player.OnMap.Y, ref xLimit);
                for (int y = Player.OnMap.Y + 1; y < Player.OnMap.Y + range; y++)
                {
                    if (y > yLimit) break;
                    SetVisibleYObjects(x, y, ref yLimit);
                }
            }
            // down and right
            xLimit = Player.OnMap.X + range;
            yLimit = Player.OnMap.Y + range;
            for (int y = Player.OnMap.Y; y < Player.OnMap.Y + range; y++)
            {
                if (y > yLimit) break;
                SetVisibleYObjects(Player.OnMap.X, y, ref yLimit);
                for (int x = Player.OnMap.X + 1; x < Player.OnMap.X + range; x++)
                {
                    if (x > xLimit) break;
                    SetVisibleXObjects(x, y, ref xLimit);
                }
            }
            // right and down
            xLimit = Player.OnMap.X + range;
            yLimit = Player.OnMap.Y + range;
            for (int x = Player.OnMap.X; x < Player.OnMap.X + range; x++)
            {
                if (x > xLimit) break;
                SetVisibleXObjects(x, Player.OnMap.Y, ref xLimit);
                for (int y = Player.OnMap.Y + 1; y < Player.OnMap.Y + range; y++)
                {
                    if (y > yLimit) break;
                    SetVisibleYObjects(x, y, ref yLimit);
                }
            }
        }
    }
}
