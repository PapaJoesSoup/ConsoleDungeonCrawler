using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;
using System.Drawing;

namespace ConsoleDungeonCrawler.Game.Screens.Dialogs
{
  internal static class PlayerSpells
  {
    internal static Box Box = new Box(Console.WindowWidth / 2 - 40, Console.WindowHeight / 2 - 8, 80, 17);

    static int ActiveSpell = 1;
    static bool DialogOpen = false;

    internal static void Draw()
    {
      DialogOpen = true;
      Dialog.Draw("Player Spell Manager", Box);
      while (DialogOpen)
      {
        DrawLegend();
        DrawSpells();
        KeyHandler();
      }
    }

    private static void DrawLegend()
    {
      Box box = new Box(Box.Left, Box.Top, 22, 10);
      int x = box.Left + 2;
      int y = box.Top + 1;
      $"Legend:".WriteAt(x, y, Color.White, Color.Olive); y += 2;

      $"[{ConsoleKey.UpArrow}] Prev Spell".WriteAt(x, y, Color.White, Color.Olive); y++;
      $"[{ConsoleKey.DownArrow}] Next Spell".WriteAt(x, y, Color.White, Color.Olive); y++;
      $"[{ConsoleKey.M}] Move Spell".WriteAt(x, y, Color.White, Color.Olive); y++;
      $"[{ConsoleKey.R}] Remove Spell".WriteAt(x, y, Color.White, Color.Olive); y++;
      $"[{ConsoleKey.Escape}] Close Dialog".WriteAt(x, y, Color.White, Color.Olive); y++;
    }

    internal static void DrawSpells()
    {
      Box box = new Box(Box.Left, Box.Top, 40, 14);
      int x = box.Left + 30;
      int y = box.Top + 1;

      $"Player Spells".WriteAt(x, y, Color.White, Color.Olive); y += 2;
      $"Key:  Spell".WriteAt(x, y, Color.White, Color.Olive); y++;
      new string('-', 48).WriteAt(x, y, Color.White, Color.Olive); y++;
      for (int i = 1; i <= 10; i++)
      {
        (!Player.Spells.ContainsKey(i)
          ? $"[{(i == 10 ? 0 : i)}]:  Empty"
          : $"[{(i == 10 ? 0 : i)}]:  {Player.Spells[i].Name} - {Player.Spells[i].Description}").WriteAt(x, y, Color.White, i == ActiveSpell ? Color.DarkOrange : Color.Olive);
        y++;
      }
    }

    internal static void MoveSpell()
    {
      Dialog.AskForInt("Move Item To Key", "Enter a Key number (1-0): ", out int newKey);
      if (Player.Spells.ContainsKey(newKey))
      {
        Draw();
        Dialog.Notify("Destination Full", "THe destination key contains a spell.  Spells will be swapped.");
        Spell temp = Player.Spells[newKey];
        Spell temp2 = Player.Spells[ActiveSpell];
        Player.Spells[newKey] = temp2;
        Player.Spells[ActiveSpell] = temp;
        ActiveSpell = newKey;
        Draw();
        return;
      }
      Spell temp3 = Player.Spells[ActiveSpell];
      Player.Spells.Remove(ActiveSpell);
      Player.Spells.Add(newKey, temp3);
      Draw();
    }

    internal static void RemoveSpell()
    {
      Dialog.Confirm("Delete Spell", "This will PERMANENTLY remove this spell.  Are you sure? (Y / N): ", out bool confirm);
      if (confirm) Player.Spells.Remove(ActiveSpell);
      Draw();
    }

    internal static void KeyHandler()
    {
      ConsoleKeyInfo keyInfo = Console.ReadKey(true);
      switch (keyInfo.Key)
      {
        case ConsoleKey.Escape:
          DialogOpen = false;
          Dialog.Close();
          break;
        case ConsoleKey.UpArrow:
          if (ActiveSpell - 1 < 1) ActiveSpell = 10;
          else ActiveSpell--;
          break;
        case ConsoleKey.DownArrow:
          if (ActiveSpell + 1 > 10) ActiveSpell = 1;
          else ActiveSpell++;
          break;
        case ConsoleKey.M:
          MoveSpell();
          break;
        case ConsoleKey.R:
          RemoveSpell();
          break;
      }
    }
  }
}
