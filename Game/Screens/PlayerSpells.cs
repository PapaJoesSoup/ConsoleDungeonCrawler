using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;
using System.Drawing;

namespace ConsoleDungeonCrawler.Game.Screens
{
  internal static class PlayerSpells
  {
    private static BoxChars bChars = new BoxChars() { botLeft = '=', botRight = '=', topRight = '=', topLeft = '=', hor = '=', ver = '|' };
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
      ConsoleEx.WriteAt($"Legend:", x, y, Color.White, Color.Olive); y += 2;

      ConsoleEx.WriteAt($"[{ConsoleKey.UpArrow}] Prev Spell", x, y, Color.White, Color.Olive); y++;
      ConsoleEx.WriteAt($"[{ConsoleKey.DownArrow}] Next Spell", x, y, Color.White, Color.Olive); y++;
      ConsoleEx.WriteAt($"[{ConsoleKey.M}] Move Spell", x, y, Color.White, Color.Olive); y++;
      ConsoleEx.WriteAt($"[{ConsoleKey.R}] Remove Spell", x, y, Color.White, Color.Olive); y++;
      ConsoleEx.WriteAt($"[{ConsoleKey.U}] Use Spell", x, y, Color.White, Color.Olive); y++;
      ConsoleEx.WriteAt($"[{ConsoleKey.Escape}] Close Dialog", x, y, Color.White, Color.Olive); y++;
    }

    internal static void DrawSpells()
    {
      Box box = new Box(Box.Left, Box.Top, 40, 14);
      int x = box.Left + 30;
      int y = box.Top + 1;

      ConsoleEx.WriteAt($"Player Spells", x, y, Color.White, Color.Olive); y += 2;
      ConsoleEx.WriteAt($"Key:  Spell", x, y, Color.White, Color.Olive); y++;
      ConsoleEx.WriteAt(new string('-', 48), x, y, Color.White, Color.Olive); y++;
      for (int i = 1; i <= 10; i++)
      {
        ConsoleEx.WriteAt(!Player.Spells.ContainsKey(i)
            ? $"[{(i == 10 ? 0 : i)}]:  Empty"
            : $"[{(i == 10 ? 0 : i)}]:  {Player.Spells[i].Name} - {Player.Spells[i].Description}"
          , x, y, Color.White, i == ActiveSpell ? Color.DarkOrange : Color.Olive);
        y++;
      }
    }

    internal static void MoveSpell()
    {
      Console.WriteLine("Move Spell");
    }

    internal static void RemoveSpell()
    {
      Console.WriteLine("Remove Spell");
    }

    internal static void UseSpell()
    {
      Console.WriteLine("Use Spell");
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
        case ConsoleKey.D1:
        case ConsoleKey.D2:
        case ConsoleKey.D3:
        case ConsoleKey.D4:
        case ConsoleKey.D5:
          break;
        case ConsoleKey.PageUp:
          break;
        case ConsoleKey.PageDown:
          break;
        case ConsoleKey.UpArrow:
          if (ActiveSpell -1 < 1) ActiveSpell = 10;
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
        case ConsoleKey.U:
          UseSpell();
          break;
      }
    }
  }
}
