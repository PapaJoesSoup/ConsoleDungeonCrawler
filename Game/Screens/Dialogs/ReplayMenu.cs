﻿using System.Drawing;
using ConsoleDungeonCrawler.Extensions;

namespace ConsoleDungeonCrawler.Game.Screens.Dialogs;

internal static class ReplayMenu
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
    "Designed and built by Joe Korinek For educational purposes.".WriteAlignedAt(HAlign.Center, VAlign.Middle, TextColor, FillColor, 0, -1);
    "Thanks to Indi O. for her inspiration in the creation of this project.".WriteAlignedAt(HAlign.Center, VAlign.Middle, TextColor, FillColor, 0, 0);
    "Best wishes to you Indi as you go forward into the future.".WriteAlignedAt(HAlign.Center, VAlign.Middle, TextColor, FillColor, 0, 1);

    "Press Q to quit...".WriteAlignedAt(Box, HAlign.Center, VAlign.Bottom, TextColor, FillColor, 0, -5);
    "Press R to restart...".WriteAlignedAt(Box, HAlign.Center, VAlign.Bottom, TextColor, FillColor, 0, -4);
    "Press M to return to the main menu...".WriteAlignedAt(Box, HAlign.Center, VAlign.Bottom, TextColor, FillColor, 0, -3);
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