using ConsoleDungeonCrawler.Extensions;

namespace ConsoleDungeonCrawler.Game.Screens
{
    internal static class GameOver
  {
    // Create a method that displays the game over screen in ascii art
    internal static void Draw()
    {
      GameCredits.Draw();
      ConsoleEx.Clear();
      ConsoleEx.WriteAlignedAt("Game Over!", HAlign.Center);
      ConsoleEx.WriteAlignedAt("Press any key to exit the game.", HAlign.Center, VAlign.Bottom);
      Console.ReadKey();
      Environment.Exit(0);

    }


  }
}
