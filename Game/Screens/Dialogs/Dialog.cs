﻿using ConsoleDungeonCrawler.Extensions;
using System.Drawing;
using ConsoleDungeonCrawler.Game.Entities;

namespace ConsoleDungeonCrawler.Game.Screens.Dialogs;

internal static class Dialog
{
  // This is the default box for the generic dialog box
  private static readonly Box Box = new(Console.WindowWidth / 2 - 52, Console.WindowHeight / 2 - 12, 100, 25);
  private static readonly Box DialogBox = new(GamePlay.MapBox.Width / 2 - 50, GamePlay.MapBox.Top + GamePlay.MapBox.Height / 2 - 12, 100, 24);

  // These colors are for the default theme of a dialog box
  private static readonly Colors Colors = new Colors();

  /// <summary>
  /// Generic Dialog box with a title everything else is default
  /// </summary>
  /// <param name="title"></param>
  internal static void Draw(string title)
  {
    Box.Draw(BoxChars.Default, Colors.Color, Colors.BackgroundColor, Colors.FillColor);
    $"[ {title} ]".WriteAlignedAt(Box, HAlign.Center, VAlign.Top, Colors.TextColor, Colors.BackgroundColor, 0, -1);
  }

  /// <summary>
  /// Generic Dialog box with a title, and default colors
  /// </summary>
  /// <param name="title"></param>
  /// <param name="box"></param>
  /// <param name="bChars"></param>
  internal static void Draw(string title, Box box, BoxChars? bChars = null)
  {
    bChars ??= BoxChars.Default;
    box.Draw(bChars, Colors.Color, Colors.BackgroundColor, Colors.FillColor);
    $"[ {title} ]".WriteAlignedAt(box, HAlign.Center, VAlign.Top, Color.Bisque, Color.Black, 0, -1);
  }

  /// <summary>
  /// Draw a box with a title, custom box, and custom colors
  /// </summary>
  /// <param name="title"></param>
  /// <param name="color"></param>
  /// <param name="bgColor"></param>
  /// <param name="fillColor"></param>
  /// <param name="textColor"></param>
  /// <param name="box"></param>
  /// <param name="bChars"></param>
  internal static void Draw(string title, Color color, Color bgColor, Color fillColor, Color textColor, Box? box = null, BoxChars? bChars = null)
  {
    box ??= Box;
    bChars ??= BoxChars.Default;
    box.Draw(bChars, color, bgColor, fillColor);
    $"[ {title} ]".WriteAlignedAt(box, HAlign.Center, VAlign.Top, textColor, bgColor, 0, -1);
  }

  internal static void AskForInt(string question, string prompt, out int result)
  {
    int width = question.Length > prompt.Length ? question.Length : prompt.Length;
    Box box = new(Console.WindowWidth / 2 - (width + 8) / 2, Console.WindowHeight / 2 - 3, width + 8, 5);
    Draw(question, Color.DarkOrange, Color.Black, Color.Black, Color.Bisque, box);
    prompt.WriteAlignedAt(box, HAlign.Center, VAlign.Middle, Color.Bisque, Color.Black, -1, 0);
    result = ConsoleEx.ReadInt(Console.GetCursorPosition().Left, Console.GetCursorPosition().Top, Color.White, Color.Black);
  }

  internal static void Confirm(string question, string prompt, out bool result)
  {
    int width = question.Length > prompt.Length ? question.Length : prompt.Length;
    Box box = new(Console.WindowWidth / 2 - (width + 8) / 2, Console.WindowHeight / 2 - 3, width + 8, 5);
    Draw(question, Color.DarkOrange, Color.Black, Color.Black, Color.Bisque, box);
    prompt.WriteAlignedAt(box, HAlign.Center, VAlign.Middle, Color.Bisque, Color.Black, -1, 0);
    result = ConsoleEx.ReadBool(Console.GetCursorPosition().Left, Console.GetCursorPosition().Top, Color.White, Color.Black);
  }

  internal static void Notify(string title, string message)
  {
    Box box = new(Console.WindowWidth / 2 - (message.Length + 6) / 2, Console.WindowHeight / 2 - 4, message.Length + 10, 7);
    Draw(title, Color.DarkOrange, Color.Black, Color.Black, Color.Bisque, box);
    message.WriteAlignedAt(Box, HAlign.Center, VAlign.Middle, Color.Bisque, Color.Black, 0, -1);
    "Press any key to continue".WriteAlignedAt(Box, HAlign.Center, VAlign.Middle, Color.Bisque, Color.Black, 0, 1);
    Console.ReadKey(true);
  }

  internal static void Close(string parent)
  {
    Map.Clear(); 
    if (parent is "PlayerInventory") PlayerInventory.Draw();
    if (parent is "PlayerSpells") PlayerSpells.Draw();
    if (parent is "Vendor") Vendor.Draw();
    if (parent is "GamePlay") GamePlay.Draw();
    if (parent is "GamePaused") GamePaused.Draw();
    if (parent is "GameCredits") GameCredits.Draw();
    if (parent is "GameOver") GameOver.Draw();
    if (parent is "GameWon") GameWon.Draw();
  }
}