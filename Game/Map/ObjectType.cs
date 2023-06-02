using System.Drawing;

namespace ConsoleDungeonCrawler.Game
{
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
