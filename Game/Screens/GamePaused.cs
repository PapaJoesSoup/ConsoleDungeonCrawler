using System.Drawing;
using ConsoleDungeonCrawler.Extensions;

namespace ConsoleDungeonCrawler.Game.Screens
{
  internal static class GamePaused
  {

    internal static void Draw()
    {
      Box box = new Box(Console.WindowWidth/2 -52, Console.WindowHeight/2 -10, 100, 20);
      box.Draw(GamePlay.BChars, Color.DarkOrange, Color.Black, Color.Olive);
      "Game Paused".WriteAlignedAt(HAlign.Center, VAlign.Middle, Color.Bisque, Color.Olive);
      "Press any key to continue".WriteAlignedAt(HAlign.Center, VAlign.Middle, Color.Bisque, Color.Olive, 0, 2);
      Console.ReadKey(true);
      Game.IsPaused = false;
      ConsoleEx.Clear();
      GamePlay.Draw();
    }
  }
}
