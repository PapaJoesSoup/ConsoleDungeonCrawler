using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;
using ConsoleDungeonCrawler.Game.Screens.Dialogs;

namespace ConsoleDungeonCrawler.Game.Screens;

internal static class GameWon
{
  private static readonly Box ScreenBorder = new(0, 0, Console.WindowWidth, Console.WindowHeight);
  private static readonly Colors Colors = new();

  internal static void Draw()
  {
    ConsoleEx.Clear();
    ScreenBorder.WriteBorder(BoxChars.Default, Colors.Color, Colors.BackgroundColor);
    LoadArt();
    LoadBannerText();
    ReplayMenu.Draw();
  }

  private static void LoadArt()
  {
    // write the title art to the console
    string[] lines = Game.GameWonArt.ToString().Split('\n');
    int height = lines.Length > Console.WindowHeight - 2 ? Console.WindowHeight - 2 : lines.Length;
    int width = lines[0].Length > Console.WindowWidth - 2 ? Console.WindowWidth - 2 : lines[0].Length;
    int startX = (Console.WindowWidth - width) / 2;
    int startY = (Console.WindowHeight - height) / 2;
    for (int y = 0; y < height; y++)
    {
      string line = lines[y];
      line.WriteAt(startX, startY + y, Colors.Color);
    }
    "Ascii art courtesy of: https://textart.sh".WriteAlignedAt(HAlign.Right, VAlign.Bottom, Colors.TextColor, Colors.Color, -2, -1);
  }

  private static void LoadBannerText()
  {
    int xOffset = 0;
    int yOffset = 4;

    string[] lines = Game.GameWonText.ToString().Split('\n');
    int height = lines.Length;
    for (int y = 0; y < height; y++)
    {
      string line = lines[y];
      line.WriteAlignedAt(HAlign.Center, VAlign.Top, Colors.Color, Colors.BackgroundColor, xOffset, yOffset);
      yOffset++;
    }
  }
}