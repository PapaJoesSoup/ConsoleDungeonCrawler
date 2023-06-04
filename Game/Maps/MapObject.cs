using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;

namespace ConsoleDungeonCrawler.Game.Maps
{
  internal class MapObject : Position
  {
    internal ObjectType Type = new ObjectType();
    internal bool IsVisible = true;
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

    internal MapObject(int x, int y, ObjectType type, bool isIsVisible)
    {
      X = x;
      Y = y;
      Type = type;
      IsVisible = isIsVisible;
    }

    internal void Draw()
    {
      if (!IsVisible || Type.Symbol == ' ') return;
      ConsoleEx.WriteAt(Type.Symbol, X + Map.Left, Y + Map.Top, Type.ForegroundColor, Type.BackgroundColor);
    }
  }

}
