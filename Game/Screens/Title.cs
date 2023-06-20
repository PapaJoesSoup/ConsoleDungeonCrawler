using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Screens.Dialogs;
using System.Drawing;
using System.Text;

namespace ConsoleDungeonCrawler.Game.Screens
{
  internal static class Title
  {
    private static int ActiveItem = 0;
    static bool DialogOpen = false;
    internal static Box ScreenBorder = new Box(0, 0, Console.WindowWidth, Console.WindowHeight);
    internal static Box Box = new Box(Console.WindowWidth / 2 - 40, Console.WindowHeight / 2 - 8, 80, 17);

    // Create a method that displays the title screen in ascii art
    internal static void Draw()
    {
      ConsoleEx.Clear();
      ConsoleEx.WriteBorderEx(ScreenBorder, GamePlay.bCharsEx, Color.DarkOrange);
      LoadTitleArt();
      Dialog.Draw("Welcome to the Dungeon Crawler!", Box, GamePlay.bCharsEx);
      DialogOpen = true;

      while (DialogOpen)
      {
        int rowCount = 4 + Game.Dungeons.Keys.Count;
        int row = -(rowCount / 2) + 1;
        ConsoleEx.WriteAlignedAt("Please select a Game Map (Up or Down Arrow):", HAlign.Center, VAlign.Middle, Color.Bisque, Color.Olive, 0, row);
        row += 2;
        for (int i = 0; i < Game.Dungeons.Keys.Count; i++)
        {
          string dungeon = Game.Dungeons.Keys.ElementAt(i);
          if (i == ActiveItem)
            ConsoleEx.WriteAlignedAt(dungeon.PadCenter(40), HAlign.Center, VAlign.Middle, Color.DarkOrange, Color.White, 0, row);
          else
            ConsoleEx.WriteAlignedAt(dungeon.PadCenter(40), HAlign.Center, VAlign.Middle, Color.Bisque, Color.Olive, 0, row);
          row++;
        }

        ConsoleEx.WriteAlignedAt("Press Enter to continue", HAlign.Center, VAlign.Middle, Color.Bisque, Color.Olive, 0, row + 1);
        KeyHandler();
      }
    }

    internal static void LoadTitleArt()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append(File.ReadAllText($"Game/Data/Art/TitleArt.txt"));
      // write the title art to the console
      string[] lines = sb.ToString().Split('\n');
      for (int y = 1; y < 52; y++)
      {
        string line = lines[y];
        ConsoleEx.WriteAt(line, 1, y, Color.DarkOrange);
      }
    }

    internal static void KeyHandler()
    {
      ConsoleKeyInfo keyInfo = Console.ReadKey(true);
      switch (keyInfo.Key)
      {
        case ConsoleKey.Enter:
          Game.CurrentDungeon = Game.Dungeons.Keys.ElementAt(ActiveItem);
          DialogOpen = false;
          break;
        case ConsoleKey.UpArrow:
          if (ActiveItem == 0) ActiveItem = Game.Dungeons.Count - 1;
          else ActiveItem--;
          break;
        case ConsoleKey.DownArrow:
          if (ActiveItem == Game.Dungeons.Count - 1) ActiveItem = 0;
          else ActiveItem++;
          break;
      }
    }
  }
}
