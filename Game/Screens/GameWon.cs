using System.Drawing;
using System.Text;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Screens.Dialogs;
using System.Drawing;
using System.Text;

namespace ConsoleDungeonCrawler.Game.Screens;

internal static class GameWon
{
  private static readonly Box ScreenBorder = new(0, 0, Console.WindowWidth, Console.WindowHeight);

  internal static void Draw()
  {
    ConsoleEx.Clear();
    ScreenBorder.WriteBorder(GamePlay.BChars, Color.DarkOrange);
    LoadArt();
    GameCredits.Draw();
    KeyHandler();
  }

  private static void LoadArt()
  {
    StringBuilder sb = new();
    sb.Append(File.ReadAllText($"{Game.ArtPath}/TitleArt2.txt"));
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
    "Ascii art courtesy of: https://textart.sh".WriteAlignedAt(HAlign.Right, VAlign.Bottom, Color.Bisque, Color.DarkOrange, -2, -1);
  }
  internal static void KeyHandler()
  {
    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
    switch (keyInfo.Key)
    {
      case ConsoleKey.Escape:
        ConsoleEx.Clear();
        Environment.Exit(0);
        break;
      case ConsoleKey.UpArrow:
        break;
      case ConsoleKey.DownArrow:
        break;
      case ConsoleKey.Q:
        ConsoleEx.Clear();
        Environment.Exit(0);
        break;
      case ConsoleKey.R:
        Game.IsWon = false;
        Game.IsOver = false;
        Game.IsPaused = false;
        ConsoleEx.Clear();
        GamePlay.Draw();
        break;
      case ConsoleKey.M:
        Game.IsWon = false;
        Game.IsOver = false;
        Game.IsPaused = false;
        ConsoleEx.Clear();
        GameTitle.Draw();
        break;
    }
  }

}