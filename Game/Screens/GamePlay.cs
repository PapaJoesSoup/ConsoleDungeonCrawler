﻿using System.Drawing;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;
using ConsoleDungeonCrawler.Game.Maps;

namespace ConsoleDungeonCrawler.Game.Screens
{
    internal static class GamePlay
  {
    internal static Box StatusBox = new Box(1, 0, 208, 8);
    internal static Box MapBox = new Box(1, 7, 178, 35);
    internal static Box OverlayBox = new Box(178, 7, 31, 27);
    internal static Box MessageBox = new Box(1, 41, 178, 10);
    internal static Box LegendBox = new Box(178, 33, 31, 18);

    internal static List<Message> Messages = new List<Message>();

    internal static BoxCharsEx boxCharsEx = new BoxCharsEx("\u948d", "\u9491", "\ud59f", "\u9499", "\u95bc", "\u9482");
    internal static char HBorderChar = '=';
    internal static char VBorderChar = '|';

    internal static int currentBag = 1;

    // MessageOffset is a negative number that decrements the index of the first message to display in the message Section
    internal static int MessageOffset = 0;
    internal static ConsoleKeyInfo LastKey;
    

    internal static void Draw()
    {
      Borders();
      //BordersEx();
      MapSection();
      LegendSection();
      MessageLegend();
      Update();
    }

    internal static void Update()
    {
      Map.SetVisibleArea(10);
      Map.Player.Draw();
      StatusSection();
      OverlaySection();
      if(AcceptableKeys()) Map.WhatIsVisible();
      Actions.MonsterActions();
      MessageSection();
    }

    internal static void Borders()
    {
      ConsoleEx.WriteBorder(StatusBox, HBorderChar, VBorderChar, ConsoleColor.Yellow);
      ConsoleEx.WriteAlignedAt($"[{Game.Title}]", HAlign.Center, VAlign.Top, ConsoleColor.White);
      ConsoleEx.WriteBorder(MapBox, HBorderChar, VBorderChar, ConsoleColor.Yellow);
      ConsoleEx.WriteBorder(OverlayBox, HBorderChar, VBorderChar, ConsoleColor.Yellow);
      ConsoleEx.WriteBorder(MessageBox, HBorderChar, VBorderChar, ConsoleColor.Yellow);
      ConsoleEx.WriteBorder(LegendBox, HBorderChar, VBorderChar, ConsoleColor.Yellow);
    }

    internal static void BordersEx()
    {
      ConsoleEx.WriteBorder(StatusBox, boxCharsEx, ConsoleColor.Yellow);
      ConsoleEx.WriteBorder(MapBox, boxCharsEx, ConsoleColor.Yellow);
      ConsoleEx.WriteBorder(OverlayBox, boxCharsEx, ConsoleColor.Yellow);
      ConsoleEx.WriteBorder(MessageBox, boxCharsEx, ConsoleColor.Yellow);
      ConsoleEx.WriteBorder(LegendBox, boxCharsEx, ConsoleColor.Yellow);
    }

    internal static void StatusSection()
    {
      ArmorStats();
      InventoryStats();
      SpellStats();
      PlayerStats();
    }

    internal static void PlayerStats()
    {
      //Player Stats
      int col = StatusBox.Left + 178;
      int row = StatusBox.Top + 1;

      ConsoleEx.WriteAt($"Player - Level: {Player.Level}", col, row, ConsoleColor.Yellow);
      row++;
      ConsoleEx.WriteAt($"Class: {Player.Class}", col, row, ConsoleColor.White);
      row++;
      ConsoleEx.WriteAt($"Weapon: ", col, row, ConsoleColor.White);
      ConsoleEx.WriteAt($"{Player.Weapon.Name}", col + 8, row, ColorEx.RarityColor(Player.Weapon.Rarity));
      row++;
      ConsoleEx.WriteAt($"Health: {Player.Health}/{Player.MaxHealth}", col, row, ConsoleColor.White);
      row++;
      ConsoleEx.WriteAt($"Mana: {Player.Mana}/{Player.MaxMana}", col, row, ConsoleColor.White);
      row++;
      ConsoleEx.WriteAt($"Gold: {Player.Gold}g", col, row, ConsoleColor.White);
    }

    internal static void SpellStats()
    {
      int col;
      int row;
      int count = 0;
      //Spells
      col = StatusBox.Left + 130;
      row = StatusBox.Top + 1;
      int colWidth = 22;
      ConsoleEx.WriteAt("Spells", col, row, ConsoleColor.Yellow);
      row++;
      for (int index = 0; index < 10; index++) // 10 spells max
      {
        if (index >= Player.Spells.Count) ConsoleEx.WriteAt($"None", col, row, Color.DimGray);
        else
        {
          Spell spell = Player.Spells[index];
          ConsoleEx.WriteAt($"{spell.Name}: {spell.Description} ", col, row, ConsoleColor.White);
        }
        row++;
        count++;

        if (count < 5) continue;
        count = 0;
        row = StatusBox.Top + 2;
        col += colWidth + 2;
      }
    }

    internal static void InventoryStats()
    {
      int col;
      int row;
      //Inventory
      col = StatusBox.Left + 31;
      row = StatusBox.Top + 1;
      int colWidth = 22;
      int count = 0;
      int totalBags = Inventory.Bags.Count;

      Bag bag = Inventory.Bags[currentBag - 1];
      ConsoleEx.WriteAt($"Inventory - Bag: {currentBag} of {totalBags}  (< or > to switch bags)", col, row, ConsoleColor.Yellow);
      row++;
      for (int index = 0; index < bag.Capacity; index++)
      {
        if (index >= bag.Items.Count) ConsoleEx.WriteAt("Empty".PadRight(colWidth), col, row, Color.DimGray);
        else
        {
          Item item = bag.Items[index];
          ConsoleEx.WriteInventoryItem(item, col, row, colWidth);
        }
        
        row++;
        count++;

        if (count < 5) continue;
        count = 0;
        row = StatusBox.Top + 2;
        col += colWidth + 2;
      }
    }

    internal static void ArmorStats()
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
    }

    internal static void MapSection()
    {
      Map.SetVisibleArea(10);
      Map.Instance.DrawMap();
      Map.Instance.DrawOverlay();

      // we add this last so that the player is always on top
      Map.Player.Draw();
    }

    internal static void OverlaySection()
    {
      int col = OverlayBox.Left + 2;
      int row = OverlayBox.Top + 1;
      foreach (char type in Map.OverlayObjects.Keys)
      {
        foreach (MapObject mapObject in Map.OverlayObjects[type])
        {
          if (!mapObject.IsVisible || mapObject.Type.Symbol == ' ') continue;
          ConsoleEx.WriteLegendItem(mapObject, col, row, OverlayBox.Width - 2);
          row++;
        }
      }
      // clear the rest of the legend box
      if (row >= OverlayBox.Top + OverlayBox.Height - 1) return;
      for (int index = row; index < OverlayBox.Top + OverlayBox.Height - 1; index++)
        ConsoleEx.WriteAt(" ", col, index, ConsoleColor.Black, ConsoleColor.Black, 0, OverlayBox.Width - 3);
    }

    internal static void LegendSection()
    {
      int col = LegendBox.Left + 2;
      int row = LegendBox.Top + 1;
      ConsoleEx.WriteAt("Game Play Legend: ", col, row, ConsoleColor.Yellow); row += 2;
      ConsoleEx.WriteAt("[W,A,S,D] - Move", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[Shift+W,A,S,D] - Jump", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[T] - Attack Enemy", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[1-0] - Cast Spell", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[H] - Use Healing Potion", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[M] - Use Mana Potion", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[G] - Use Bandage", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[O,C] - Open/Close Door", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[+,-] - Up/Down Stairs", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[< >] - Switch Bag Shown", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[Esc] - Pause Menu", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[Shift+I] - Inventory", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[Shift+S] - Spells", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[Shift+Q] - Quit", col, row, ConsoleColor.White);
    }

    internal static void MessageSection()
    {
      // display the last 8 messages or less.  allow scrolling up or down through messages in pages of 8
      int col = MessageBox.Left + 2;
      int row = MessageBox.Top + 1;
      if (MessageOffset > 0) MessageOffset = 0;
      if (MessageOffset < -Messages.Count) MessageOffset = -Messages.Count;
      int end = Messages.Count + MessageOffset;
      if (Messages.Count < end) end = Messages.Count;
      int start = 0;
      if (end > 8) start = end - 8;
      if (end < 8 && Messages.Count >= 8) end = 8;
      for (int index = start; index < end; index++)
      {
        Messages[index].WriteAt(col, row);
        row++;
      }
      // clear the rest of the message box
      if (row >= MessageBox.Top + MessageBox.Height - 1) return;
      for (int index = row; index < MessageBox.Top + MessageBox.Height - 1; index++)
        ConsoleEx.WriteAt(" ", col, index, Color.Black, Color.Black, 0, MessageBox.Width - 32);
    }

    internal static void MessageLegend()
    {
      int col = MessageBox.Width - 28;
      int row = MessageBox.Top + 1;
      ConsoleEx.WriteAt("Messages Legend: ", col, row, ConsoleColor.Yellow); row += 2;
      ConsoleEx.WriteAt("[UpArrow] - Prev Message", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[DownArrow] - Next Message", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[PageUP] - Messages - 8", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[PageDown] - Messages + 8", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[Home] - First Message", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[End] - Last Message", col, row, ConsoleColor.White);
    }

    internal static bool AcceptableKeys()
    {
      return (!Player.InCombat && LastKey.Key is ConsoleKey.W or ConsoleKey.A or ConsoleKey.S or ConsoleKey.D);
    }
  }
}