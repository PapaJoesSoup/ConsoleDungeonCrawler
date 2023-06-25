using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;
using System.Drawing;

namespace ConsoleDungeonCrawler.Game.Screens.Dialogs;

internal static class PlayerSpells
{
  private static readonly Box Box = new(Console.WindowWidth / 2 - 40, Console.WindowHeight / 2 - 8, 80, 17);

  private static readonly Color Color = Color.DarkOrange;
  private static readonly Color BackgroundColor = Color.Black;
  private static readonly Color FillColor = Color.SaddleBrown;
  private static readonly Color TextColor = Color.Bisque;
  private static readonly Color SelectedColor = Color.Lime;
  private static readonly Color SelectedBackgroundColor = Color.DarkOrange;

  static int activeSpell = 1;
  static bool dialogOpen = false;

  internal static void Draw()
  {
    dialogOpen = true;
    Dialog.Draw("Player Spell Manager", Box);
    while (dialogOpen)
    {
      DrawLegend();
      DrawSpells();
      KeyHandler();
    }
  }

  private static void DrawLegend()
  {
    Box box = new(Box.Left, Box.Top, 22, 10);
    int x = box.Left + 2;
    int y = box.Top + 1;
    "Legend:".WriteAt(x, y, Color.White, Color.Olive); y += 2;

    $"[{ConsoleKey.UpArrow}] Prev Spell".WriteAt(x, y, Color.White, Color.Olive); y++;
    $"[{ConsoleKey.DownArrow}] Next Spell".WriteAt(x, y, Color.White, Color.Olive); y++;
    $"[{ConsoleKey.M}] Move Spell".WriteAt(x, y, Color.White, Color.Olive); y++;
    $"[{ConsoleKey.R}] Remove Spell".WriteAt(x, y, Color.White, Color.Olive); y++;
    $"[{ConsoleKey.Escape}] Close Dialog".WriteAt(x, y, Color.White, Color.Olive);
  }

  internal static void DrawSpells()
  {
    Box box = new(Box.Left, Box.Top, 40, 14);
    int x = box.Left + 30;
    int y = box.Top + 1;

    "Player Spells".WriteAt(x, y, Color.White, Color.Olive); y += 2;
    "Key:  Spell".WriteAt(x, y, Color.White, Color.Olive); y++;
    new string('-', 48).WriteAt(x, y, Color.White, Color.Olive); y++;
    for (int i = 1; i <= 10; i++)
    {
      (!Player.Spells.ContainsKey(i)
        ? $"[{(i == 10 ? 0 : i)}]:  Empty"
        : $"[{(i == 10 ? 0 : i)}]:  {Player.Spells[i].Name} - {Player.Spells[i].Description}").WriteAt(x, y, Color.White, i == activeSpell ? Color.DarkOrange : Color.Olive);
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
      Spell temp2 = Player.Spells[activeSpell];
      Player.Spells[newKey] = temp2;
      Player.Spells[activeSpell] = temp;
      activeSpell = newKey;
      Draw();
      return;
    }
    Spell temp3 = Player.Spells[activeSpell];
    Player.Spells.Remove(activeSpell);
    Player.Spells.Add(newKey, temp3);
    Draw();
  }

  internal static void RemoveSpell()
  {
    Dialog.Confirm("Delete Spell", "This will PERMANENTLY remove this spell.  Are you sure? (Y / N): ", out bool confirm);
    if (confirm) Player.Spells.Remove(activeSpell);
    Draw();
  }

  internal static void KeyHandler()
  {
    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
    switch (keyInfo.Key)
    {
      case ConsoleKey.Escape:
        dialogOpen = false;
        Dialog.Close("GamePlay");
        break;
      case ConsoleKey.UpArrow:
        if (activeSpell - 1 < 1) activeSpell = 10;
        else activeSpell--;
        break;
      case ConsoleKey.DownArrow:
        if (activeSpell + 1 > 10) activeSpell = 1;
        else activeSpell++;
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