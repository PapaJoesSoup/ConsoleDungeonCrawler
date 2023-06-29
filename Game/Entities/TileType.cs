using System.Drawing;

namespace ConsoleDungeonCrawler.Game.Entities;

internal class TileType
{
  internal readonly char Symbol = ' ';
  internal readonly string Name = "Empty";
  internal readonly string Singular = "Empty Space";
  internal readonly string Plural = "Empty Spaces";
  internal Color ForegroundColor = Color.Black;
  internal Color BackgroundColor = Color.Black;
  internal readonly bool IsPassable = false;
  internal readonly bool IsVisible = true;
  internal readonly bool IsAttackable = false;
  internal readonly bool IsLootable = false;
  internal readonly bool IsTransparent = false;

  internal TileType()
  {
  }

  internal TileType(bool isPassable, bool isVisible = true)
  {
    IsPassable = isPassable;
    IsVisible = isVisible;
  }

  public TileType(char symbol, string name, string singular, string plural, Color foregroundColor, Color backgroundColor, bool isPassable, bool isAttackable, bool isLootable, bool isTransparent = false) : this()
  {
    Symbol = symbol;
    Name = name;
    Singular = singular;
    Plural = plural;
    ForegroundColor = foregroundColor;
    BackgroundColor = backgroundColor;
    IsPassable = isPassable;
    IsAttackable = isAttackable;
    IsLootable = isLootable;
    IsTransparent = isTransparent;
  }
}