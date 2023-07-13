using System.Drawing;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;

namespace ConsoleDungeonCrawler.Game.Screens.Dialogs;

internal static class GameCredits
{
  private static readonly Colors Colors = new Colors()
  {
    FillColor = Color.DarkSlateGray,
    TextColor = Color.Cyan
  };

  private static readonly Box Box = new(Dialog.ScreenCenter, 82, 17);

  internal static void Draw(bool exitKey = true)
  {
    Dialog.Draw($" {Game.Title} - Credits", Colors.Color, Colors.BackgroundColor, Colors.FillColor, Colors.TextColor, Box);
    $"Thanks for playing {Game.Title}!".WriteAlignedAt(HAlign.Center, VAlign.Middle, Colors.TextColor, Colors.FillColor, 0, -4);
    "Designed and built by Joe Korinek For educational purposes.".WriteAlignedAt(HAlign.Center, VAlign.Middle, Colors.TextColor, Colors.FillColor);
    "Thanks to Indi O. for her inspiration in the creation of this project.".WriteAlignedAt(HAlign.Center, VAlign.Middle, Colors.TextColor, Colors.FillColor, 0, 1);
    "Best wishes to you, Indi, as you go forward into the future.".WriteAlignedAt(HAlign.Center, VAlign.Middle, Colors.TextColor, Colors.FillColor, 0, 2);
    if (!exitKey) return;
    "Press any key to continue...".WriteAlignedAt(Box, HAlign.Center, VAlign.Bottom, Colors.SelectedColor, Colors.FillColor, 0, -3);
    Console.ReadKey(true);
  }
}