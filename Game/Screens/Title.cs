using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;
using System.Drawing;

namespace ConsoleDungeonCrawler.Game.Screens
{
  internal static class Title
  {
    private static int ActiveItem = 0;
    static bool DialogOpen = false;
    private static BoxChars bChars = new BoxChars() { botLeft = '=', botRight = '=', topRight = '=', topLeft = '=', hor = '=', ver = '|' };

    // Create a method that displays the title screen in ascii art
    internal static void Draw()
    {
      ConsoleEx.Clear();

      Dialog.Draw("Player Inventory Manager");

      DialogOpen = true;

      while (DialogOpen)
      {
        int row = 0;
        ConsoleEx.WriteAlignedAt("Welcome to the Dungeon Crawler!", HAlign.Center, VAlign.Middle, Color.Bisque, Color.Olive, 0, -2);
        ConsoleEx.WriteAlignedAt("Please select a Game Map (Up or Down Arrow):", HAlign.Center, VAlign.Middle, Color.Bisque,  Color.Olive, 0, row); row+=2;

        for (int i = 0; i < Game.Dungeons.Keys.Count; i++)
        {
          string dungeon = Game.Dungeons.Keys.ElementAt(i);
          ConsoleEx.WriteAlignedAt(dungeon, HAlign.Center, VAlign.Middle, i == ActiveItem ? Color.DarkOrange : Color.Bisque, Color.Olive, 0, row); row++;
        }

        ConsoleEx.WriteAlignedAt("Press Enter to continue", HAlign.Center, VAlign.Middle, Color.Bisque, Color.Olive, 0, row+1);
        KeyHandler();
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
          Dialog.Close();
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
