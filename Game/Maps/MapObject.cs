using System.Drawing;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;

namespace ConsoleDungeonCrawler.Game.Maps
{
  internal class MapObject : Position
  {
    internal ObjectType Type = new ObjectType();
    internal bool IsVisible = true;
    internal Color ForegroundColor = Color.White;
    internal Color BackgroundColor = Color.Black;

    internal MapObject()
    {
    }

    internal MapObject(int x, int y)
    {
      X = x;
      Y = y;
      ForegroundColor = Type.ForegroundColor;
      BackgroundColor = Type.BackgroundColor;
    }

    internal MapObject(int x, int y, ObjectType type)
    {
      X = x;
      Y = y;
      Type = type;
      ForegroundColor = type.ForegroundColor;
      BackgroundColor = type.BackgroundColor;
    }

    internal MapObject(int x, int y, ObjectType type, bool isIsVisible)
    {
      X = x;
      Y = y;
      Type = type;
      IsVisible = isIsVisible;
      ForegroundColor = type.ForegroundColor;
      BackgroundColor = type.BackgroundColor;
    }

    internal void Draw()
    {
      if (!IsVisible || Type.Symbol == ' ') return;
      ConsoleEx.WriteAt(Type.Symbol, X + Map.Left, Y + Map.Top, ForegroundColor, BackgroundColor);
    }
  }

}
