using System.Drawing;
using ConsoleDungeonCrawler.Extensions;

namespace ConsoleDungeonCrawler.Game.Screens
{
    internal static class GameCreditsScreen
  {
    internal static void Draw()
    {
      ConsoleEx.Clear();
      ConsoleEx.WriteAlignedAt($"Thanks for playing {Game.Title}!", HAlign.Center, VAlign.Middle, Color.Cyan, Color.Black, 0, -2);
      ConsoleEx.WriteAlignedAt("Designed and built by Joe Korinek For educational purposes.", HAlign.Center, VAlign.Middle, Color.Cyan, Color.Black);
      ConsoleEx.WriteAlignedAt("Thanks go to Indi Odegard for her inspiration in the creation of this project.", HAlign.Center, VAlign.Middle, Color.Cyan, Color.Black, 0, 1);
      ConsoleEx.WriteAlignedAt("Best wishes to you, Indi, as you go forward into the future.", HAlign.Center, VAlign.Middle, Color.Cyan, Color.Black, 0, 2);
      ConsoleEx.WriteAlignedAt("Press any key to continue.", HAlign.Center, VAlign.Bottom, Color.Cyan, Color.Black);
      Console.ReadKey();
    }
  }
}
