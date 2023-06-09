using ConsoleDungeonCrawler.Extensions;
using System.Drawing;

namespace ConsoleDungeonCrawler.Game.Screens
{
  internal static class Dialog
  {
    private static BoxChars b = new BoxChars() { botLeft = '=', botRight = '=', topRight = '=', topLeft = '=', hor = '=', ver = '|' };
    internal static Box Box = new Box(Console.WindowWidth / 2 - 52, Console.WindowHeight / 2 - 10, 100, 20);

    internal static void Draw(string title)
    {
      Box = new Box(Console.WindowWidth / 2 - 52, Console.WindowHeight / 2 - 10, 100, 20);
      Box.Draw(b, Color.DarkOrange, Color.Black, Color.Olive);
      ConsoleEx.WriteAlignedAt(Box, $"[{title}]", HAlign.Center, VAlign.Top, Color.Bisque, Color.Black, 0, -1);

    }

    internal static void Close()
    {
      Game.IsPaused = false;
      ConsoleEx.Clear();
      GamePlay.Draw();
    }
  }
}
