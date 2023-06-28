using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Screens.Dialogs;
using System.Drawing;
using System.Text;

namespace ConsoleDungeonCrawler.Game.Screens;

internal static class GameTitle
{
  private static int activeItem;
  private static bool dialogOpen;
  private static readonly Box ScreenBorder = new(0, 0, Console.WindowWidth, Console.WindowHeight);
  private static readonly Box Box = new(Console.WindowWidth / 2 - 40, Console.WindowHeight / 2 - 8, 80, 17);

  // Create a method that displays the title screen in ascii art
  internal static void Draw()
  {
    ConsoleEx.Clear();
    ScreenBorder.WriteBorder(GamePlay.BChars, Color.DarkOrange);
    LoadTitleArt();
    Dialog.Draw(" Welcome to the Dungeon Crawler! ", Box, GamePlay.BChars);
    dialogOpen = true;

    while (dialogOpen)
    {
      int rowCount = 4 + Game.Dungeons.Keys.Count;
      int row = -(rowCount / 2) + 1;
      "Please select a Game Map (North or South Arrow):".WriteAlignedAt(HAlign.Center, VAlign.Middle, Color.Bisque, Color.Olive, 0, row);
      row += 2;
      for (int i = 0; i < Game.Dungeons.Keys.Count; i++)
      {
        string dungeon = Game.Dungeons.Keys.ElementAt(i);
        if (i == activeItem)
          dungeon.PadCenter(40).WriteAlignedAt(HAlign.Center, VAlign.Middle, Color.DarkOrange, Color.White, 0, row);
        else
          dungeon.PadCenter(40).WriteAlignedAt(HAlign.Center, VAlign.Middle, Color.Bisque, Color.Olive, 0, row);
        row++;
      }

      "Press Enter to continue".WriteAlignedAt(HAlign.Center, VAlign.Middle, Color.Bisque, Color.Olive, 0, row + 1);
      KeyHandler();
    }
  }

  private static void LoadTitleArt()
  {
    StringBuilder sb = new();
    sb.Append(File.ReadAllText($"{Game.ArtPath}/TitleArt.txt"));
    // write the title art to the console
    string[] lines = sb.ToString().Split('\n');
    for (int y = 1; y < 52; y++)
    {
      string line = lines[y];
      line.WriteAt(1, y, Color.DarkOrange);
    }
    "Courtesy of: https://textart.sh".WriteAlignedAt(HAlign.Right, VAlign.Bottom, Color.Bisque, Color.DarkOrange, 0, 0);
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