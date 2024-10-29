using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;
using System.Drawing;

namespace ConsoleDungeonCrawler.Game.Screens.Dialogs
{
    internal static class GameMenu
  {
    private static bool dialogOpen = false;
    private static int activeItem = 0;
    private static readonly Box Box = new(Dialog.ScreenCenter, 80, 17);

    private static readonly Colors Colors = new()
    {
      FillColor = Color.Black,
      SelectedColor = Color.DarkOrange,
      SelectedBackgroundColor = Color.White,
    };

    internal static void Draw()
    {
      Dialog.Draw(" Welcome to the Dungeon Crawler! ",Colors.Color, Colors.BackgroundColor, Colors.FillColor, Colors.TextColor, Box, BoxChars.Default);
      dialogOpen = true;

      while (dialogOpen)
      {
        int rowCount = 5 + Game.Dungeons.Keys.Count;
        int row = -(rowCount / 2) + 1;
        "Please select a Game Map (Up or Down Arrow):".WriteAlignedAt(HAlign.Center, VAlign.Middle, Color.Bisque, Color.Black, 0, row);
        row += 2;
        for (int i = 0; i < Game.Dungeons.Keys.Count; i++)
        {
          string dungeon = Game.Dungeons.Keys.ElementAt(i);
          if (i == activeItem)
            dungeon.PadCenter(40).WriteAlignedAt(HAlign.Center, VAlign.Middle, Colors.SelectedColor, Colors.SelectedBackgroundColor, 0, row);
          else
            dungeon.PadCenter(40).WriteAlignedAt(HAlign.Center, VAlign.Middle, Colors.TextColor, Colors.FillColor, 0, row);
          row++;
        }
        "Press [Q] to Quit".WriteAlignedAt(HAlign.Center, VAlign.Middle, Colors.TextColor, Colors.FillColor, 0, row + 1);
        "Press Enter to continue".WriteAlignedAt(HAlign.Center, VAlign.Middle, Colors.TextColor, Colors.FillColor, 0, row + 2);
        KeyHandler();
      }
    }

    private static void KeyHandler()
    {
      ConsoleKeyInfo keyInfo = Console.ReadKey(true);
      switch (keyInfo.Key)
      {
        case ConsoleKey.Q:
          ConsoleEx.Clear();
          dialogOpen = false;
          Game.IsQuit = true;
          break;
        case ConsoleKey.Enter:
          Game.CurrentDungeon = Game.Dungeons.Keys.ElementAt(activeItem);
          dialogOpen = false;
          break;
        case ConsoleKey.UpArrow:
          if (activeItem == 0) activeItem = Game.Dungeons.Count - 1;
          else activeItem--;
          break;
        case ConsoleKey.DownArrow:
          if (activeItem == Game.Dungeons.Count - 1) activeItem = 0;
          else activeItem++;
          break;
      }
    }
  }
}
