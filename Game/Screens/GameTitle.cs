﻿using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;
using ConsoleDungeonCrawler.Game.Screens.Dialogs;

namespace ConsoleDungeonCrawler.Game.Screens;

internal static class GameTitle
{
  // Game Title name courtesy of:
  // https://patorjk.com/software/taag/#p=display&h=2&v=3&f=Elite&t=Console%20Dungeon%20Crawler
  // background screen Ascii art courtesy of: https://textart.sh
  // Rendering speeds and unicode support are improved significantly by setting the renderer in Terminal to "Use Atlas Engine".
  // This can be accessed by "Ctrl + ," to get to the settings menu in the console window. Ctrl + Shift + W will return you to the game.
  // Refer to the comments here: https://github.com/microsoft/terminal/issues/15625

  private static readonly Box ScreenBorder = new(0, 0, Console.WindowWidth, Console.WindowHeight);
  private static readonly Colors Colors = new();
  // Create a method that displays the title screen in ascii art
  internal static void Draw()
  {
    ConsoleEx.Clear();
    ScreenBorder.WriteBorder(BoxChars.Default, Colors.Color, Colors.BackgroundColor);
    LoadTitleArt();
    LoadBannerText();
    GameMenu.Draw();
    if(Game.IsQuit) return;
    SoundSystem.PlayEnter();
    LoadTitleArt();
    LoadBannerText();
    LoadEnterText();
    Thread.Sleep(6000);

  }

  private static void LoadTitleArt()
  {
    // write the title art to the console
    string[] lines = Game.GameTitleArt.ToString().Split('\n');
    int height = lines.Length > Console.WindowHeight - 2 ? Console.WindowHeight - 2 : lines.Length;
    int width = lines[0].Length > Console.WindowWidth - 2 ? Console.WindowWidth - 2 : lines[0].Length;
    int startX = (Console.WindowWidth - width) / 2;
    int startY = (Console.WindowHeight - height) / 2;
    for (int y = 0; y < height; y++)
    {
      string line = lines[y];
      line.WriteAt(startX, startY + y, Colors.Color);
    }

    "License: MIT (https://opensource.org/license/mit/)".WriteAlignedAt(HAlign.Left, VAlign.Bottom, Colors.TextColor,
      Colors.Color, 2, -1);
    "Ascii art courtesy of: https://textart.sh".WriteAlignedAt(HAlign.Right, VAlign.Bottom, Colors.TextColor,
      Colors.Color, -2, -1);
  }

  private static void LoadBannerText()
  {
    int xOffset = 0;
    int yOffset = 4;

    // write the title art to the console
    string[] lines = Game.GameTitleText.ToString().Split('\n');
    int height = lines.Length;
    for (int y = 0; y < height; y++)
    {
      string line = lines[y];
      line.WriteAlignedAt(HAlign.Center, VAlign.Top, Colors.BackgroundColor, Colors.Color, xOffset, yOffset);
      yOffset++;
    }
  }

  private static void LoadEnterText()
  {
    int xOffset = 0;
    int yOffset = 0;

    // write the title art to the console
    string[] lines = Game.GameEnterText.ToString().Split('\n');
    int height = lines.Length;
    for (int y = 0; y < height; y++)
    {
      string line = lines[y];
      line.WriteAlignedAt(HAlign.Center, VAlign.Middle, Colors.BackgroundColor, Colors.Color, xOffset, yOffset);
      yOffset++;
    }
  }
}