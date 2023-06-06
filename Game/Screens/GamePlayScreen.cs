using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;
using ConsoleDungeonCrawler.Game.Maps;

namespace ConsoleDungeonCrawler.Game.Screens
{
    internal static class GamePlayScreen
  {
    internal static Box StatusBox = new Box(1, 0, 208, 8);
    internal static Box MapBox = new Box(1, 7, 178, 35);
    internal static Box LegendBox = new Box(178, 7, 31, 35);
    internal static Box MessageBox = new Box(1, 41, 208, 10);
    internal static List<Message> Messages = new List<Message>();

    internal static BoxCharsEx boxCharsEx = new BoxCharsEx("\xe2948d", "\xe29491", "\\xd59f", "\xe29499", "\xe295bc", "\xe29482");
    internal static char HBorderChar = '=';
    internal static char VBorderChar = '|';


    internal static void Draw()
    {
      Borders();
      MapSection();
      Update();
    }

    internal static void Update()
    {
      Map.SetVisibleArea(10);
      Map.Player.Draw();
      StatusSection();
      LegendSection();
      if(!Player.InCombat) Map.WhatIsVisible();
      Actions.MonsterActions();
      MessageSection();
    }

    internal static void Borders()
    {
      ConsoleEx.WriteBorder(StatusBox, HBorderChar, VBorderChar, ConsoleColor.Yellow);
      ConsoleEx.WriteAlignedAt($"[{Game.Title}]", HAlign.Center, VAlign.Top, ConsoleColor.White);
      ConsoleEx.WriteBorder(MapBox, HBorderChar, VBorderChar, ConsoleColor.Yellow);
      ConsoleEx.WriteBorder(LegendBox, HBorderChar, VBorderChar, ConsoleColor.Yellow);
      ConsoleEx.WriteBorder(MessageBox, HBorderChar, VBorderChar, ConsoleColor.Yellow);
    }

    internal static void BordersEx()
    {
      ConsoleEx.WriteBorder(StatusBox, boxCharsEx, ConsoleColor.Yellow);
      ConsoleEx.WriteBorder(MapBox, boxCharsEx, ConsoleColor.Yellow);
      ConsoleEx.WriteBorder(LegendBox, boxCharsEx, ConsoleColor.Yellow);
      ConsoleEx.WriteBorder(MessageBox, boxCharsEx, ConsoleColor.Yellow);
    }

    internal static void StatusSection()
    {
      int row = StatusBox.Top + 1;
      int col = StatusBox.Left + 2;

      //Armor
      ConsoleEx.WriteAt("Armor", col, row, ConsoleColor.Yellow);
      row++;
      foreach (var armor in Player.ArmorSet)
      {
        string armorText = $"{armor.ArmorType}: {armor.Name} ".PadRight(col + 50);
        ConsoleEx.WriteAt(armorText, col, row, ConsoleColor.White);
        row++;
      }

      //Items
      col = StatusBox.Left + 60;
      row = StatusBox.Top + 1;
      int count = 0;
      ConsoleEx.WriteAt("Items", col, row, ConsoleColor.Yellow);
      row++;
      foreach (ItemType type in Inventory.Items.Keys)
      {
        foreach (Item item in Inventory.Items[type])
          count++;
        ConsoleEx.WriteAt($"{type}: {count} ", col, row, ConsoleColor.White);
        count = 0;
        row++;
      }

      //Spells
      col = StatusBox.Left + 120;
      row = StatusBox.Top + 1;
      ConsoleEx.WriteAt("Spells", col, row, ConsoleColor.Yellow);
      row++;
      foreach (var spell in Player.Spells)
      {
        ConsoleEx.WriteAt($"{spell.Value.Name}: {spell.Value.Description} ", col, row, ConsoleColor.White);
        row++;
      }

      //Player Stats
      col = StatusBox.Left + 178;
      row = StatusBox.Top + 1;
      ConsoleEx.WriteAt("Player", col, row, ConsoleColor.Yellow);
      row++;
      ConsoleEx.WriteAt($"Class: {Player.Class}", col, row, ConsoleColor.White);
      row++;
      ConsoleEx.WriteAt($"Level: {Player.Level}", col, row, ConsoleColor.White);
      row++;
      ConsoleEx.WriteAt($"Health: {Player.Health}/{Player.MaxHealth}", col, row, ConsoleColor.White);
      row++;
      ConsoleEx.WriteAt($"Mana: {Player.Mana}/{Player.MaxMana}", col, row, ConsoleColor.White);
      row++;
      ConsoleEx.WriteAt($"Gold: {Player.Gold}g", col, row, ConsoleColor.White);
    }

    internal static void MapSection()
    {
      Map.SetVisibleArea(10);
      Map.Instance.DrawMap();
      Map.Instance.DrawOverlay();

      // we add this last so that the player is always on top
      Map.Player.Draw();
    }

    internal static void LegendSection()
    {
      int col = LegendBox.Left + 2;
      int row = LegendBox.Top + 1;
      foreach (char type in Map.OverlayObjects.Keys)
      {
        foreach (MapObject mapObject in Map.OverlayObjects[type])
        {
          if (!mapObject.IsVisible || mapObject.Type.Symbol == ' ') continue;
          ConsoleEx.WriteLegendItem(mapObject, col, row, LegendBox.Width - 2);
          row++;
        }
      }
      // clear the rest of the legend box
      if (row >= LegendBox.Top + LegendBox.Height - 1) return;
      for (int index = row; index < LegendBox.Top + LegendBox.Height - 1; index++)
        ConsoleEx.WriteAt(" ", col, index, ConsoleColor.Black, ConsoleColor.Black, 0, LegendBox.Width - 3);
    }

    internal static void MessageSection()
    {
      int row = MessageBox.Top + 1;
      int col = MessageBox.Left + 2;
      if (Messages.Count <= 0) return;
      int offset = Messages.Count - 8 > 0 ? Messages.Count - 8 : 0;
      for (int index = 0 + offset; index < Messages.Count; index++)
      {
        Messages[index].WriteAt(col, row);
        row++;
      }
    }
  }
}
