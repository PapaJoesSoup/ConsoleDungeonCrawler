using System.Drawing;

namespace ConsoleDungeonCrawler.Game.Maps
{
  internal class ObjectType
  {
    internal char Symbol = ' ';
    internal string Name = "Empty";
    internal string Singular = "Empty Space";
    internal string Plural = "Empty Spaces";
    internal Color ForegroundColor = Color.Black;
    internal Color BackgroundColor = Color.Black;
    internal bool IsPassable = false;
    internal bool IsVisible = true;
    internal bool IsAttackable = false;
    internal bool IsLootable = false;

    internal ObjectType()
    {
    }

    internal ObjectType(bool isPassable, bool isVisible = true)
    {
      IsPassable = isPassable;
      IsVisible = isVisible;
    }
  }
}
