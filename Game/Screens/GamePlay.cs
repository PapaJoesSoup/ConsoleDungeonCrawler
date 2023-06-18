using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;
using ConsoleDungeonCrawler.Game.Screens.Dialogs;

namespace ConsoleDungeonCrawler.Game.Screens
{
  internal static class GamePlay
  {
    internal static Box StatusBox = new Box(1, 0, 208, 8);
    internal static Box MapBox = new Box(1, 7, 178, 35);
    internal static Box OverlayBox = new Box(178, 7, 31, 27);
    internal static Box MessageBox = new Box(1, 41, 178, 12);
    internal static Box LegendBox = new Box(178, 33, 31, 20);

    internal static List<Message> Messages = new List<Message>();

    internal static BoxCharsEx bCharsEx = new BoxCharsEx("\u2554", "\u2557", "\u255a", "\u255d", "\u2550", "\u2551");
    internal static BoxCharsEx bCharsEx2 = new BoxCharsEx("\u2554", "\u2557", "\u255a", "\u255d", "\u2550", "\u2551", "\u2560", "\u2563", "\u2566", "\u2569", "\u256c");
    internal static char HBorderChar = '=';
    internal static char VBorderChar = '|';

    internal static int currentBag = 1;

    // MessageOffset is a negative number that decrements the index of the first message to display in the message Section
    internal static int MessageOffset = 0;
    internal static ConsoleKeyInfo LastKey;

    internal static void Draw()
    {
      //Borders();
      BordersEx();
      MapSection();
      LegendSection();
      MessageLegend();
      Map.SetVisibleArea(10);
      Map.Player.Draw();
      Map.WhatIsVisible();
      Update();
    }

    internal static void Update()
    {
      Map.SetVisibleArea(10);
      Map.Player.Draw();
      StatusSection();
      OverlaySection();
      if (AcceptableKeys()) Map.WhatIsVisible();
      Actions.MonsterActions();
      MessageSection();
    }

    internal static void Borders()
    {
      ConsoleEx.WriteBorderEx(StatusBox, HBorderChar, VBorderChar, Color.Gold);
      ConsoleEx.WriteAlignedAt($"[{Game.Title} - The {Game.CurrentDungeon}]", HAlign.Center, VAlign.Top, Color.White);
      ConsoleEx.WriteBorderEx(MapBox, HBorderChar, VBorderChar, Color.Gold);
      ConsoleEx.WriteBorderEx(OverlayBox, HBorderChar, VBorderChar, Color.Gold);
      ConsoleEx.WriteBorderEx(MessageBox, HBorderChar, VBorderChar, Color.Gold);
      ConsoleEx.WriteBorderEx(LegendBox, HBorderChar, VBorderChar, Color.Gold);
    }

    internal static void BordersEx()
    {
      ConsoleEx.WriteBorderEx(StatusBox, bCharsEx, Color.Gold);
      ConsoleEx.WriteAlignedAt($"[ {Game.Title} - The {Game.CurrentDungeon} ]", HAlign.Center, VAlign.Top, Color.White);
      ConsoleEx.WriteBorderEx(MapBox, bCharsEx, Color.Gold);
      ConsoleEx.WriteBorderEx(OverlayBox, bCharsEx, Color.Gold);
      ConsoleEx.WriteBorderEx(MessageBox, bCharsEx, Color.Gold);
      ConsoleEx.WriteBorderEx(LegendBox, bCharsEx, Color.Gold);

      // now to clean up the corners
       ConsoleEx.WriteAt(bCharsEx2.midLeft, MapBox.Left, MapBox.Top, Color.Gold);
      ConsoleEx.WriteAt(bCharsEx2.midTop, OverlayBox.Left, MapBox.Top, Color.Gold);
      ConsoleEx.WriteAt(bCharsEx2.midRight, OverlayBox.Left + OverlayBox.Width - 1 , MapBox.Top, Color.Gold);

      ConsoleEx.WriteAt(bCharsEx2.midLeft, LegendBox.Left, LegendBox.Top, Color.Gold);
      ConsoleEx.WriteAt(bCharsEx2.midRight, LegendBox.Left + LegendBox.Width - 1, LegendBox.Top, Color.Gold);

      ConsoleEx.WriteAt(bCharsEx2.midLeft, MessageBox.Left, MessageBox.Top, Color.Gold);
      ConsoleEx.WriteAt(bCharsEx2.midRight, MessageBox.Left + MessageBox.Width - 1, MessageBox.Top, Color.Gold);
       
      ConsoleEx.WriteAt(bCharsEx2.midBottom, LegendBox.Left, LegendBox.Top + LegendBox.Height - 1, Color.Gold);

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
      int col = StatusBox.Left + 179;
      int row = StatusBox.Top + 1;
      ConsoleEx.WriteAt(bCharsEx2.midTop, col - 2, row - 1, Color.Gold);
      ConsoleEx.WriteAt(bCharsEx2.mid, col - 2, StatusBox.Height - 1, Color.Gold);
      for (int index = row; index < row + 6; index++)
      {
        ConsoleEx.WriteAt(bCharsEx2.ver, col - 2, index, Color.Gold);
      }
      ConsoleEx.WriteAt($"Player - Level: {Player.Level}", col, row, Color.Gold); row++;
      ConsoleEx.WriteAt($"Class: {Player.Class}", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt($"Weapon: ", col, row, ConsoleColor.White);
      ConsoleEx.WriteAt($"{Player.Weapon.Name}", col + 8, row, ColorEx.RarityColor(Player.Weapon.Rarity)); row++;
      ConsoleEx.WriteAt($"Health: {Player.Health}/{Player.MaxHealth}", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt($"Mana: {Player.Mana}/{Player.MaxMana}", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt($"Gold: {Player.Gold}g", col, row, ConsoleColor.White);
    }

    internal static void SpellStats()
    {
      int col;
      int row;
      int count = 0;
      //Spells
      col = StatusBox.Left + 140;
      row = StatusBox.Top + 1;
      int colWidth = 18;
      ConsoleEx.WriteAt(bCharsEx2.midTop, col - 2, row - 1, Color.Gold);
      ConsoleEx.WriteAt(bCharsEx2.midBottom, col - 2, StatusBox.Height - 1, Color.Gold);
      for (int index = row; index < row + 6; index++)
      {
        ConsoleEx.WriteAt(bCharsEx2.ver, col - 2, index, Color.Gold);
      }
      ConsoleEx.WriteAt("Spells", col, row, Color.Gold);
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
      int colWidth = 25;
      int count = 0;
      int totalBags = Inventory.Bags.Count;
      ConsoleEx.WriteAt(bCharsEx2.midTop, col - 2, row - 1, Color.Gold);
      ConsoleEx.WriteAt(bCharsEx2.midBottom, col - 2, StatusBox.Height - 1, Color.Gold);
      for (int index = row; index < row + 6; index++)
      {
        ConsoleEx.WriteAt(bCharsEx.ver, col - 2, index, Color.Gold);
      }

      Bag bag = Inventory.Bags[currentBag - 1];
      ConsoleEx.WriteAt($"Inventory - Bag: {currentBag} of {totalBags}  (< or > to switch bags)", col, row, Color.Gold);
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
      ConsoleEx.WriteAt("Armor", col, row, Color.Gold);
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
      Map.DrawMap();
      Map.DrawOverlay();

      // we add this last so that the player is always on top
      Map.Player.Draw();
    }

    internal static void OverlaySection()
    {
      int col = OverlayBox.Left + 2;
      int row = OverlayBox.Top + 1;
      ConsoleEx.WriteAt("Map objects: ", col, row, Color.Gold); row += 2;
      foreach (char type in Map.LevelOverlayObjects[Game.CurrentLevel].Keys)
      {
        foreach (MapObject mapObject in Map.LevelOverlayObjects[Game.CurrentLevel][type])
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
      ConsoleEx.WriteAt("Game Play Legend: ", col, row, Color.Gold); row += 2;
      ConsoleEx.WriteAt("[W,A,S,D] - Move", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[Shift+W,A,S,D] - Jump", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[T] - Attack Enemy", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[1-0] - Cast Spell", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[H] - Use Healing Potion", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[M] - Use Mana Potion", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[G] - Use Bandage", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[O,C] - Open/Close Door", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[F7/F8] - Up/Down Stairs", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[< >] - Switch Bag Shown", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[Esc] - Pause Menu", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[Shift+I] - Inventory", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[Shift+S] - Spells", col, row, ConsoleColor.White); row++;
      ConsoleEx.WriteAt("[Shift+Q] - Quit", col, row, ConsoleColor.White);
    }

    internal static void MessageSection()
    {
      // display the last 10 messages or less.  allow scrolling up or down through messages in pages of 8
      int col = MessageBox.Left + 2;
      int row = MessageBox.Top + 1;
      int displaycount = MessageBox.Height - 2;
      if (MessageOffset > 0) MessageOffset = 0;
      if (MessageOffset < -Messages.Count) MessageOffset = -Messages.Count;
      int end = Messages.Count + MessageOffset;
      if (Messages.Count < end) end = Messages.Count;
      int start = 0;
      if (end > displaycount) start = end - displaycount;
      if (end < displaycount && Messages.Count >= displaycount) end = displaycount;
      for (int index = start; index < end; index++)
      {
        Messages[index].WriteAt(col, row);
        row++;
      }
      // clear the rest of the message box
      if (row >= MessageBox.Top + MessageBox.Height - 1) return;
      for (int index = row; index < MessageBox.Top + MessageBox.Height - 1; index++)
        ConsoleEx.WriteAt(" ", col, index, Color.Black, Color.Black, MessageBox.Width - 33, 0);
    }

    internal static void MessageLegend()
    {
      int col = MessageBox.Width - 30;
      int row = MessageBox.Top + 1;
      // draw the message Legend left border
      ConsoleEx.WriteAt(bCharsEx2.midTop, col, row - 1, Color.Gold);
      ConsoleEx.WriteAt(bCharsEx2.midBottom, col, MessageBox.Top + MessageBox.Height - 1, Color.Gold);
      for (int index = row; index < MessageBox.Top + MessageBox.Height - 1; index++)
        ConsoleEx.WriteAt(bCharsEx2.ver, col, index, Color.Gold);
      col += 2;
      ConsoleEx.WriteAt("Messages Legend: ", col, row, Color.Gold); row += 2;
      ConsoleEx.WriteAt("[UpArrow] - Prev Message", col, row, Color.White); row++;
      ConsoleEx.WriteAt("[DownArrow] - Next Message", col, row, Color.White); row++;
      ConsoleEx.WriteAt("[PageUP] - Messages - 10", col, row, Color.White); row++;
      ConsoleEx.WriteAt("[PageDown] - Messages + 10", col, row, Color.White); row++;
      ConsoleEx.WriteAt("[Home] - First Message", col, row, Color.White); row++;
      ConsoleEx.WriteAt("[End] - Last Message", col, row, Color.White);
    }

    internal static bool AcceptableKeys()
    {
      return (!Player.InCombat && LastKey.Key is ConsoleKey.W or ConsoleKey.A or ConsoleKey.S or ConsoleKey.D);
    }

    internal static void KeyHandler()
    {
      // Capture and hide the key the user pressed
      ConsoleKeyInfo keyInfo = Console.ReadKey(true);
      GamePlay.LastKey = keyInfo;
      if ((keyInfo.Modifiers & ConsoleModifiers.Shift) != 0)
      {
        switch (keyInfo.Key)
        {
          case ConsoleKey.W:
          case ConsoleKey.A:
          case ConsoleKey.S:
          case ConsoleKey.D:
            GamePlay.Messages.Add(new Message($"You jumped {Map.GetDirection(keyInfo.Key)}..."));
            Map.Player.Jump(keyInfo.Key);
            break;
          case ConsoleKey.I:
            PlayerInventory.Draw();
            break;
          case ConsoleKey.P:
            PlayerSpells.Draw();
            break;
          case ConsoleKey.Q:
            Game.IsOver = true;
            break;
        }
      }
      else
      {
        switch (keyInfo.Key)
        {
          case ConsoleKey.W:
          case ConsoleKey.A:
          case ConsoleKey.S:
          case ConsoleKey.D:

            if (!Map.Player.Move(keyInfo.Key)) break;
            GamePlay.Messages.Add(new Message($"You moved {Map.GetDirection(keyInfo.Key)}..."));
            Actions.PickupOverlayItem();
            break;
          case ConsoleKey.D0:
          case ConsoleKey.D1:
          case ConsoleKey.D2:
          case ConsoleKey.D3:
          case ConsoleKey.D4:
          case ConsoleKey.D5:
          case ConsoleKey.D6:
          case ConsoleKey.D7:
          case ConsoleKey.D8:
          case ConsoleKey.D9:
            Actions.UseSpell(keyInfo);
            break;
          case ConsoleKey.Escape:
            Game.IsPaused = true;
            break;
          case ConsoleKey.PageUp:
            GamePlay.MessageOffset -= 8;
            break;
          case ConsoleKey.PageDown:
            GamePlay.MessageOffset += 8;
            break;
          case ConsoleKey.UpArrow:
            GamePlay.MessageOffset--;
            break;
          case ConsoleKey.DownArrow:
            GamePlay.MessageOffset++;
            break;
          case ConsoleKey.Home:
            GamePlay.MessageOffset = -GamePlay.Messages.Count;
            break;
          case ConsoleKey.End:
            GamePlay.MessageOffset = 0;
            break;
          case ConsoleKey.OemComma:
            if (Inventory.Bags.Count > 1)
              GamePlay.currentBag--;
            break;
          case ConsoleKey.OemPeriod:
            if (GamePlay.currentBag < Inventory.Bags.Count)
              GamePlay.currentBag++;
            break;
          case ConsoleKey.O:
            Actions.OpenDoor();
            Map.SetVisibleArea(10);
            Map.WhatIsVisible();
            break;
          case ConsoleKey.C:
            Actions.CloseDoor();
            break;
          case ConsoleKey.F7:
            Actions.UpStairs();
            break;
          case ConsoleKey.F8:
            Actions.DownStairs();
            break;
          case ConsoleKey.T:
            Map.Player.Attack();
            break;
          case ConsoleKey.F5:
            Map.ShowFullMap();
            break;
          case ConsoleKey.F6:
            Map.ShowFullOverlay();
            break;
          default:
            break;
        }
      }
    }
  }
}
