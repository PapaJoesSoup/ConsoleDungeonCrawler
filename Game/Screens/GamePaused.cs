using System.Drawing;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;

namespace ConsoleDungeonCrawler.Game.Screens;

internal static class GamePaused
{
  internal static readonly Colors Colors = new Colors();
  internal static void Draw()
  {
    Box box = new(Console.WindowWidth/2 -52, Console.WindowHeight/2 -10, 100, 20);
    box.Draw(BoxChars.Default, Colors.Color, Colors.BackgroundColor, Colors.FillColor);
    "Game Paused".WriteAlignedAt(HAlign.Center, VAlign.Middle, Colors.TextColor, Colors.FillColor);
    "Press any key to continue".WriteAlignedAt(HAlign.Center, VAlign.Middle, Colors.TextColor, Colors.FillColor, 0, 2);
    Console.ReadKey(true);
    Game.IsPaused = false;
    ConsoleEx.Clear();
    GamePlay.Draw();
  }
}