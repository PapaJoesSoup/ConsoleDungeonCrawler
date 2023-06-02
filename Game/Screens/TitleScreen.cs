using ConsoleDungeonCrawler.Extensions;

namespace ConsoleDungeonCrawler.Game.Screens
{
  internal static class TitleScreen
  {
    // Create a method that displays the title screen in ascii art
    internal static void Draw()
    {
      ConsoleEx.Clear();
      ConsoleEx.WriteAlignedAt("Welcome to the Dungeon Crawler!", HAlign.Center, VAlign.Middle);
      ConsoleEx.WriteAlignedAt("Press any key to start the game.", HAlign.Center, VAlign.Bottom);
      Console.ReadKey();
    }

  }
}
