using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;

namespace ConsoleDungeonCrawler.Game.Screens.Dialogs;

internal class TerminalConfiguration
{
  private static readonly Color Color = Color.DarkOrange;
  private static readonly Color BackgroundColor = Color.Black;
  private static readonly Color FillColor = Color.DarkBlue;
  private static readonly Color TextColor = Color.Bisque;
  private static readonly Color SelectedColor = Color.Lime;
  private static readonly Color SelectedBackgroundColor = Color.DarkOrange;

  private static readonly Box DialogBox = new(Console.WindowWidth / 2 - 58, Console.WindowHeight / 2 - 13, 116, 25);
  private static readonly Box LegendBox = new(DialogBox.Left, DialogBox.Top + 11, 22, 10);

  private static bool dialogOpen;

  internal static void Draw()
  {
    ConsoleEx.Clear();
    dialogOpen = true;
    Dialog.Draw(" Terminal Configuration Needed ", Color, BackgroundColor, FillColor, TextColor, DialogBox);

    int y = 3;
    int x = 5;
    while (dialogOpen)
    {
      "The terminal window needs to be configured to allow this game to run properly)".WriteAlignedAt(DialogBox, HAlign.Center, VAlign.Top, Color, FillColor, 0, y); y+=2;
      "The Game requires a minimum Terminal screen width (measured in characters) of 209 and a height of 53.".WriteAlignedAt(DialogBox, HAlign.Left, VAlign.Top, TextColor, FillColor, x, y); y++;
      $"Your current Width / Height is: {Console.WindowWidth} x {Console.WindowHeight}.".WriteAlignedAt(DialogBox, HAlign.Left, VAlign.Top, Color, FillColor, x, y); y+=2;
      "So, for best results, a minimum monitor resolution of 1920px x 1080px is recommended".WriteAlignedAt(DialogBox, HAlign.Left, VAlign.Top, TextColor, FillColor, x, y); y++;
      "This assumes Windows font scaling of 100%.  Use a higher resolution if the font scaling is larger.".WriteAlignedAt(DialogBox, HAlign.Left, VAlign.Top, TextColor, FillColor, x, y); y += 2;
      "Additionally, there are some settings in Windows Terminal we should set:".WriteAlignedAt(DialogBox, HAlign.Left, VAlign.Top, TextColor, FillColor, x, y); y++;
      "To access settings from here, press 'Ctrl + ,' to open.  To close, press 'Ctrl+Shift+W'".WriteAlignedAt(DialogBox, HAlign.Left, VAlign.Top, TextColor, FillColor, x, y); y++;
      "To switch between Settings Tab and the game Tab, press 'Ctrl+Tab'.".WriteAlignedAt(DialogBox, HAlign.Left, VAlign.Top, TextColor, FillColor, x, y); y += 2;
      "1. In Settings, Startup, under Launch Parameters, Set Launch Mode to Maximized focus.".WriteAlignedAt(DialogBox, HAlign.Left, VAlign.Top, TextColor, FillColor, x, y); y++;
      "2. In Settings, Rendering, Set Use AtlasEngine ON.".WriteAlignedAt(DialogBox, HAlign.Left, VAlign.Top, TextColor, FillColor, x, y); y++;
      "3. In Settings, Defaults, Appearance, Set Font face to Consolas.".WriteAlignedAt(DialogBox, HAlign.Left, VAlign.Top, TextColor, FillColor, x, y); y++;
      "4. Save your changes and close all windows.  Restart your game.".WriteAlignedAt(DialogBox, HAlign.Left, VAlign.Top, TextColor, FillColor, x, y); y++;

      "Press any key to quit.".WriteAlignedAt(DialogBox, HAlign.Left, VAlign.Top, TextColor, FillColor, x, y); y++;

      Console.ReadKey(true);
    }
  }
}