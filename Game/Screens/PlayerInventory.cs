using System.Drawing;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;

namespace ConsoleDungeonCrawler.Game.Screens
{
  internal static class PlayerInventory
  {
    private static BoxChars b = new BoxChars() {botLeft = '=', botRight = '=', topRight = '=', topLeft = '=', hor = '=', ver = '|' };

    internal static void Draw()
    {
      Dialog.Draw("Player Inventory Manager");
      // Create a new box for the player inventory
      int x = 1;
      int y = 1;
      foreach (var bag in Inventory.Bags)
      {
        ConsoleEx.WriteAt("Select Bag: (press number)", Dialog.Box.Left + 2, Dialog.Box.Top + 1, Color.White, Color.Olive);
        ConsoleEx.WriteAt($"[{x}] Bag {x}", Dialog.Box.Left + 2, Dialog.Box.Top + 1 + y, Color.White, Color.Olive);
        x++;
        y++;
      }


      //ConsoleEx.WriteAlignedAt(Dialog.Box, "Press any key to continue", HAlign.Center, VAlign.Middle, Color.Bisque, Color.Olive);
      Console.ReadKey(true);
      Dialog.Close();
    }
  }
}
