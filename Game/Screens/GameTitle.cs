using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Screens.Dialogs;
using System.Drawing;
using System.Text;

namespace ConsoleDungeonCrawler.Game.Screens;

internal static class GameTitle
{
  // Game Title name courtesy of:
  // https://patorjk.com/software/taag/#p=display&h=2&v=3&f=Elite&t=Console%20Dungeon%20Crawler
  // background screen Ascii art courtesy of: https://textart.sh

  private static readonly Box ScreenBorder = new(0, 0, Console.WindowWidth, Console.WindowHeight);

  // Create a method that displays the title screen in ascii art
  internal static void Draw()
  {
    ConsoleEx.Clear();
    ScreenBorder.WriteBorder(GamePlay.BChars, Color.DarkOrange);
    LoadTitleArt();
    LoadGameTitle();
    GameMenu.Draw();
  }

  private static void LoadTitleArt()
  {
    StringBuilder sb = new();
    sb.Append(File.ReadAllText($"{Game.ArtPath}/TitleArt.txt"));
    // write the title art to the console
    string[] lines = sb.ToString().Split('\n');
    int height = lines.Length > Console.WindowHeight - 2 ? Console.WindowHeight - 2 : lines.Length;
    int width = lines[0].Length > Console.WindowWidth - 2 ? Console.WindowWidth - 2 : lines[0].Length;
    int x = (Console.WindowWidth - width) / 2;
    int Y = (Console.WindowHeight - height) / 2;
    for (int y = 0; y < height; y++)
    {
      string line = lines[y];
      line.WriteAt(x, Y + y, Color.DarkOrange);
    }
    "License: MIT (https://opensource.org/license/mit/)".WriteAlignedAt(HAlign.Left, VAlign.Bottom, Color.Bisque, Color.DarkOrange, 2, -1);
    "Ascii art courtesy of: https://textart.sh".WriteAlignedAt(HAlign.Right, VAlign.Bottom, Color.Bisque, Color.DarkOrange, -2, -1);
  }

  private static void LoadGameTitle()
  {
    StringBuilder sb = new();
    int xOffset = 0;
    int yOffset = 4;

    sb.Append(File.ReadAllText($"{Game.ArtPath}/GameTitle.txt"));
    // write the title art to the console
    string[] lines = sb.ToString().Split('\n');
    int height = lines.Length;
    int width = lines[0].Length;
    for (int y = 0; y < height; y++)
    {
      string line = lines[y];
      line.WriteAlignedAt(HAlign.Center, VAlign.Top, Color.Black, Color.DarkOrange,xOffset, yOffset);
      yOffset++;
    }
  }
}