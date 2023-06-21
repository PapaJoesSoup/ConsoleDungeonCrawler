using ConsoleDungeonCrawler.Extensions;

namespace ConsoleDungeonCrawler.Game.Screens
{
    internal static class GameWon
  {
    internal static void Draw()
    {
      ConsoleEx.Clear();
      "You won the game!".WriteAlignedAt(HAlign.Center);
      "Press any key to exit the game.".WriteAlignedAt(HAlign.Center, VAlign.Bottom);
      Console.ReadKey();
      Environment.Exit(0);
    }
  }
}
