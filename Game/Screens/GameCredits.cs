using System.Drawing;
using ConsoleDungeonCrawler.Extensions;

namespace ConsoleDungeonCrawler.Game.Screens;

internal static class GameCredits
{
  internal static void Draw()
  {
    ConsoleEx.Clear();
    $"Thanks for playing {Game.Title}!".WriteAlignedAt(HAlign.Center, VAlign.Middle, Color.Cyan, Color.Black, 0, -2);
    "Designed and built by Joe Korinek For educational purposes.".WriteAlignedAt(HAlign.Center, VAlign.Middle, Color.Cyan, Color.Black);
    "Thanks go to Indi Odegard for her inspiration in the creation of this project.".WriteAlignedAt(HAlign.Center, VAlign.Middle, Color.Cyan, Color.Black, 0, 1);
    "Best wishes to you, Indi, as you go forward into the future.".WriteAlignedAt(HAlign.Center, VAlign.Middle, Color.Cyan, Color.Black, 0, 2);
    "Press any key to continue.".WriteAlignedAt(HAlign.Center, VAlign.Bottom, Color.Cyan, Color.Black);
    Console.ReadKey();
  }
}