using ConsoleDungeonCrawler.Extensions;
using System.Drawing;

namespace ConsoleDungeonCrawler.Game.Screens.Dialogs;

internal class GameOptions
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
    Dialog.Draw($" {Game.Title} - Options", Color, BackgroundColor, FillColor, TextColor, Box);



    "Press [S] to Save Changes".WriteAlignedAt(Box, HAlign.Center, VAlign.Bottom, TextColor, FillColor, 0, -5);
    "Press [Esc] to Exit Without Waving".WriteAlignedAt(Box, HAlign.Center, VAlign.Bottom, TextColor, FillColor, 0, -4);

    KeyHandler();
  }

  private static void KeyHandler()
  {
    bool loop = true;
    while (loop)
    {
      ConsoleKeyInfo keyInfo = Console.ReadKey(true);
      loop = false;
      switch (keyInfo.Key)
      {
        case ConsoleKey.Escape:
        case ConsoleKey.Q:
          ConsoleEx.Clear();
          Game.IsQuit = true;
          break;
        case ConsoleKey.R:
          ConsoleEx.Clear();
          Game.IsRestart = true;
          break;
        case ConsoleKey.M:
          ConsoleEx.Clear();
          Game.IsMainMenu = true;
          break;
        default:
          loop = true;
          break;
      }

    }
  }
}