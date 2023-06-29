using ConsoleDungeonCrawler.Extensions;
using System.Drawing;

namespace ConsoleDungeonCrawler.Game.Screens.Dialogs
{
  internal class GameMenu
  {
    private static bool dialogOpen = false;
    private static int activeItem = 0;
    private static readonly Box Box = new(Console.WindowWidth / 2 - 40, Console.WindowHeight / 2 - 8, 80, 17);

    internal static void Draw()
    {
      Dialog.Draw(" Welcome to the Dungeon Crawler! ",Color.DarkOrange, Color.Black, Color.Black, Color.Bisque, Box, GamePlay.BChars);
      dialogOpen = true;

      while (dialogOpen)
      {
        int rowCount = 4 + Game.Dungeons.Keys.Count;
        int row = -(rowCount / 2) + 1;
        "Please select a Game Map (North or South Arrow):".WriteAlignedAt(HAlign.Center, VAlign.Middle, Color.Bisque, Color.Black, 0, row);
        row += 2;
        for (int i = 0; i < Game.Dungeons.Keys.Count; i++)
        {
          string dungeon = Game.Dungeons.Keys.ElementAt(i);
          if (i == activeItem)
            dungeon.PadCenter(40).WriteAlignedAt(HAlign.Center, VAlign.Middle, Color.DarkOrange, Color.White, 0, row);
          else
            dungeon.PadCenter(40).WriteAlignedAt(HAlign.Center, VAlign.Middle, Color.Bisque, Color.Black, 0, row);
          row++;
        }

        "Press Enter to continue".WriteAlignedAt(HAlign.Center, VAlign.Middle, Color.Bisque, Color.Black, 0, row + 1);
        KeyHandler();
      }
    }

    private static void KeyHandler()
    {
      ConsoleKeyInfo keyInfo = Console.ReadKey(true);
      switch (keyInfo.Key)
      {
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
