using System.Drawing;
using ConsoleDungeonCrawler.Extensions;

namespace ConsoleDungeonCrawler.Game.Screens
{
    internal static class GamePausedScreen
  {
    private static BoxChars b = new BoxChars() {botLeft = '=', botRight = '=', topRight = '=', topLeft = '=', hor = '=', ver = '|' };

    internal static void Draw()
    {
      Box box = new Box(Console.WindowWidth/2 -52, Console.WindowHeight/2 -10, 100, 20);
      box.Draw(b, Color.DarkOrange, Color.Black, Color.Olive);
      ConsoleEx.WriteAlignedAt("Game Paused", HAlign.Center, VAlign.Middle, Color.Bisque, Color.Olive);
      ConsoleEx.WriteAlignedAt("Press any key to continue", HAlign.Center, VAlign.Middle, Color.Bisque, Color.Olive, 0, 2);
      Console.ReadKey(true);
      Game.IsPaused = false;
      ConsoleEx.Clear();
      GamePlayScreen.Draw();
    }
  }
}
