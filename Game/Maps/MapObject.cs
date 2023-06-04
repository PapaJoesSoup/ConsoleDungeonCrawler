using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;

namespace ConsoleDungeonCrawler.Game.Maps
{
  internal class MapObject : Position
  {
    internal ObjectType Type = new ObjectType();
    internal bool Visible = true;
    internal Item Loot = new Item();

    internal MapObject()
    {
    }

    internal MapObject(int x, int y)
    {
      X = x;
      Y = y;
    }

    internal MapObject(int x, int y, ObjectType type)
    {
      X = x;
      Y = y;
      Type = type;
    }

    internal MapObject(int x, int y, ObjectType type, bool isVisible)
    {
      X = x;
      Y = y;
      Type = type;
      Visible = isVisible;
    }

    internal void Draw()
    {
      if (!Visible || Type.Symbol == ' ') return;
      ConsoleEx.WriteAt(Type.Symbol, X + Map.Left, Y + Map.Top, Type.ForegroundColor, Type.BackgroundColor);
    }
  }

}
