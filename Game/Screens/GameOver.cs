﻿using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Screens.Dialogs;
using System.Drawing;
using System.Text;

namespace ConsoleDungeonCrawler.Game.Screens;

internal static class GameOver
{
  private static readonly Box ScreenBorder = new(0, 0, Console.WindowWidth, Console.WindowHeight);

  // Create a method that displays the game over screen in ascii art
  internal static void Draw()
  {
    ConsoleEx.Clear();
    ScreenBorder.WriteBorder(GamePlay.BChars, Color.DarkOrange);
    LoadArt();
    LoadBannerText();
    ReplayMenu.Draw();
  }

  private static void LoadArt()
  {
    StringBuilder sb = new();
    sb.Append(File.ReadAllText($"{Game.ArtPath}/TitleArt3.txt"));
    // write the title art to the console
    string[] lines = sb.ToString().Split('\n');
    int height = lines.Length > Console.WindowHeight - 2 ? Console.WindowHeight - 2 : lines.Length;
    int width = lines[0].Length > Console.WindowWidth -2 ? Console.WindowWidth - 2 : lines[0].Length;
    int startX = (Console.WindowWidth - width) / 2;
    int startY = (Console.WindowHeight - height) / 2;
    for (int y = 0; y < height; y++)
    {
      string line = lines[y];
      line.WriteAt(startX, startY + y, Color.DarkOrange);
    }
    "Ascii art courtesy of: https://textart.sh".WriteAlignedAt(HAlign.Right, VAlign.Bottom, Color.Bisque, Color.DarkOrange, -2, -1);
  }

  private static void LoadBannerText()
  {
    StringBuilder sb = new();
    int xOffset = 0;
    int yOffset = 7;

    sb.Append(File.ReadAllText($"{Game.ArtPath}/GameOver.txt"));
    // write the title art to the console
    string[] lines = sb.ToString().Split('\n');
    int height = lines.Length;
    int width = lines[0].Length;
    for (int y = 0; y < height; y++)
    {
      string line = lines[y];
      line.WriteAlignedAt(HAlign.Center, VAlign.Top, Color.Black, Color.DarkOrange, xOffset, yOffset);
      yOffset++;
    }
  }
}