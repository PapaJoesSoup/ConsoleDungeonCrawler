using System.Drawing;
using ConsoleDungeonCrawler.Extensions;

namespace ConsoleDungeonCrawler.Game.Entities;

internal class Tile : Position
{
  internal TileType Type = new();
  internal bool IsVisible = false;
  internal bool IsPassable = false;
  internal bool IsLootable = false;
  internal bool IsAttackable = false;
  internal Color ForegroundColor = Color.White;
  internal Color BackgroundColor = Color.Black;

  internal Tile()
  {
  }

  internal Tile(int x, int y)
  {
    X = x;
    Y = y;
    Type = new TileType();
    IsVisible = Type.IsVisible;
    IsPassable = Type.IsPassable;
    IsAttackable = Type.IsAttackable;
    IsLootable = Type.IsLootable;
    ForegroundColor = Type.ForegroundColor;
    BackgroundColor = Type.BackgroundColor;
  }

  internal Tile(int x, int y, TileType type)
  {
    X = x;
    Y = y;
    Type = type;
    IsVisible = type.IsVisible;
    IsPassable = type.IsPassable;
    IsAttackable = type.IsAttackable;
    IsLootable = type.IsLootable;
    ForegroundColor = type.ForegroundColor;
    BackgroundColor = type.BackgroundColor;

  }

  internal Tile(int x, int y, TileType type, bool isVisible)
  {
    X = x;
    Y = y;
    Type = type;
    IsVisible = isVisible;
    IsPassable = type.IsPassable;
    IsAttackable = type.IsAttackable;
    IsLootable = type.IsLootable;
    ForegroundColor = type.ForegroundColor;
    BackgroundColor = type.BackgroundColor;
  }

  internal void Draw()
  {
    if (!IsVisible) return;
    Type.Symbol.WriteAt(X + Map.Left, Y + Map.Top, ForegroundColor, BackgroundColor);
  }

  internal void Draw(bool force)
  {
    if (!force)
    {
      Draw();
      return;
    }
    if (Type.Symbol == ' ') return;
    Type.Symbol.WriteAt(X + Map.Left, Y + Map.Top, ForegroundColor, BackgroundColor);
  }

  internal void Highlight()
  {
    if (!IsVisible || Type.Symbol == ' ') return;
    Type.Symbol.WriteAt(X + Map.Left, Y + Map.Top, Color.HotPink, BackgroundColor);
  }

  internal bool ContainsItem()
  {
    return Type.Symbol != ' ';
  }
}