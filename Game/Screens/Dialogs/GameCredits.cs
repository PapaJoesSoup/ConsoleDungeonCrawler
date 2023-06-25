using System.Drawing;
using ConsoleDungeonCrawler.Extensions;

namespace ConsoleDungeonCrawler.Game.Screens.Dialogs;

internal static class GameCredits
{
  private static readonly Color Color = Color.DarkOrange;
  private static readonly Color BackgroundColor = Color.Black;
  private static readonly Color FillColor = Color.DarkSlateGray;
  private static readonly Color TextColor = Color.Cyan;
  private static readonly Color SelectedColor = Color.Lime;
  private static readonly Color SelectedBackgroundColor = Color.DarkOrange;

  private static readonly Box Box = new(Console.WindowWidth / 2 - 41, Console.WindowHeight / 2 - 8, 82, 17);

  internal static void Draw()
  {
    Dialog.Draw($" {Game.Title} - Credits", Color, BackgroundColor, FillColor, TextColor, Box);
    $"Thanks for playing {Game.Title}!".WriteAlignedAt(HAlign.Center, VAlign.Middle, TextColor, FillColor, 0, -4);
    "Designed and built by Joe Korinek For educational purposes.".WriteAlignedAt(HAlign.Center, VAlign.Middle, TextColor, FillColor);
    "Thanks to Indi Odegard for her inspiration in the creation of this project.".WriteAlignedAt(HAlign.Center, VAlign.Middle, TextColor, FillColor, 0, 1);
    "Best wishes to you, Indi, as you go forward into the future.".WriteAlignedAt(HAlign.Center, VAlign.Middle, TextColor, FillColor, 0, 2);
    "Press any key to continue...".WriteAlignedAt(Box, HAlign.Center, VAlign.Bottom, TextColor, FillColor, 0, -3);
    Console.ReadKey();
  }
}