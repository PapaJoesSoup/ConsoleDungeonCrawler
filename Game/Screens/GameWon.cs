using ConsoleDungeonCrawler.Extensions;

namespace ConsoleDungeonCrawler.Game.Screens
{
    internal static class GameWon
  {
    internal static void Draw()
    {
      ConsoleEx.Clear();
      ConsoleEx.WriteAlignedAt("You won the game!", HAlign.Center);
      ConsoleEx.WriteAlignedAt("Press any key to exit the game.", HAlign.Center, VAlign.Bottom);
      Console.ReadKey();
      Environment.Exit(0);
    }
  }
}
