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
    ConsoleEx.Clear();
    Environment.Exit(0);
  }

  private static void LoadArt()
  {
    StringBuilder sb = new();
    sb.Append(File.ReadAllText($"{Game.ArtPath}/TitleArt2.txt"));
    // write the title art to the console
    string[] lines = sb.ToString().Split('\n');
    for (int y = 1; y < 52; y++)
    {
      string line = lines[y];
      line.WriteAt(1, y, Color.DarkOrange);
    }
    "Courtesy of: https://textart.sh".WriteAlignedAt(HAlign.Right, VAlign.Bottom, Color.Bisque, Color.DarkOrange, 0, 0);
  }
}