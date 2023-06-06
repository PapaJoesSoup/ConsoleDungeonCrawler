using ConsoleDungeonCrawler.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Game.Screens
{
  internal static class DialogBox
  {
    private static BoxChars b = new BoxChars() { botLeft = '=', botRight = '=', topRight = '=', topLeft = '=', hor = '=', ver = '|' };
    internal static Box Box = new Box(Console.WindowWidth / 2 - 52, Console.WindowHeight / 2 - 10, 100, 20);

    internal static void Draw()
    {
      Box = new Box(Console.WindowWidth / 2 - 52, Console.WindowHeight / 2 - 10, 100, 20);
      Box.Draw(b, Color.DarkOrange, Color.Black, Color.Olive);
    }

    internal static void Close()
    {
      Game.IsPaused = false;
      ConsoleEx.Clear();
      GamePlayScreen.Draw();
    }
  }
}
