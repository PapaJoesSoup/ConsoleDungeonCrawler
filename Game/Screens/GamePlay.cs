using System.Drawing;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;
using ConsoleDungeonCrawler.Game.Screens.Dialogs;

namespace ConsoleDungeonCrawler.Game.Screens;

/// <summary>
/// GamePlay is the main game screen.  It is responsible for drawing the map, player, and all other objects on the screen.
/// </summary>
internal static class GamePlay
{
  #region Properties
  private static readonly Box StatusBox = new(1, 0, 208, 8);
  internal static readonly Box MapBox = new(1, 7, 178, 35);
  private static readonly Box OverlayBox = new(178, 7, 31, 30);
  private static readonly Box MessageBox = new(1, 41, 178, 12);
  private static readonly Box LegendBox = new(178, 36, 31, 17);

  internal static List<Message> Messages = new();
  internal static readonly int MessageWidth = MessageBox.Width - 33;
  private static readonly int MessageHeight = MessageBox.Height - 2;

  // MessageOffset is a negative number that decrements the index of the first message to display in the message Section
  private static int messageOffset;


  // These are unicode values for box drawing characters.   Expects Console.OutputEncoding = Encoding.Unicode and Consolas font selected in Terminal Settings.
  // Note that font settings cannot be changed in code, so the user must do this manually in the terminal app.
  // refer to: https://www.fileformat.info/info/unicode/font/consolas/grid.htm for a grid of all characters
  internal static readonly BoxChars BChars = new("\u2554", "\u2557", "\u255a", "\u255d", "\u2550", "\u2551", "\u2560", "\u2563", "\u2566", "\u2569", "\u256c");

  // This is used to manage display of bags in both the inventory section as well as the PlayerInventory and Vendor screens
  private static int currentBag = 1;

  // LastKey is used to help with player combat state management
  private static ConsoleKeyInfo lastKey;
  #endregion Properties

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

  private static void BordersEx()
  {
    StatusBox.WriteBorder(BChars, Color.Gold);
    $"[ {Game.Title} - The {Game.CurrentDungeon} ]".WriteAlignedAt(HAlign.Center, VAlign.Top, Color.White);
    MapBox.WriteBorder(BChars, Color.Gold);
    OverlayBox.WriteBorder(BChars, Color.Gold);
    MessageBox.WriteBorder(BChars, Color.Gold);
    LegendBox.WriteBorder(BChars, Color.Gold);

    // now to clean up the corners
    BChars.MidLeft.WriteAt(MapBox.Left, MapBox.Top, Color.Gold);
    BChars.MidTop.WriteAt(OverlayBox.Left, MapBox.Top, Color.Gold);
    BChars.MidRight.WriteAt(OverlayBox.Left + OverlayBox.Width - 1 , MapBox.Top, Color.Gold);

    BChars.MidLeft.WriteAt(LegendBox.Left, LegendBox.Top, Color.Gold);
    BChars.MidRight.WriteAt(LegendBox.Left + LegendBox.Width - 1, LegendBox.Top, Color.Gold);

    BChars.MidLeft.WriteAt(MessageBox.Left, MessageBox.Top, Color.Gold);
    BChars.MidRight.WriteAt(MessageBox.Left + MessageBox.Width - 1, MessageBox.Top, Color.Gold);
       
    BChars.MidBottom.WriteAt(LegendBox.Left, LegendBox.Top + LegendBox.Height - 1, Color.Gold);

  }

  internal static void StatusSection()
  {
    ArmorStats();
    InventoryStats();
    SpellStats();
    PlayerStats();
  }

  private static void PlayerStats()
  {
    //Player Stats
    int col = StatusBox.Left + 179;
    int row = StatusBox.Top + 1;
    BChars.MidTop.WriteAt(col - 2, row - 1, Color.Gold);
    BChars.Mid.WriteAt(col - 2, StatusBox.Height - 1, Color.Gold);
    for (int index = row; index < row + 6; index++)
    {
      BChars.Ver.WriteAt(col - 2, index, Color.Gold);
    }
    $"Player - Level: {Player.Level}".WriteAt(col, row, Color.Gold); row++;
    $"Class: {Player.Class}".WriteAt(col, row, ConsoleColor.White); row++;
    "Weapon: ".WriteAt(col, row, ConsoleColor.White);
    $"{Player.Weapon.Name}".WriteAt(col + 8, row, ColorEx.RarityColor(Player.Weapon.Rarity)); row++;
    $"Health: {Player.Health}/{Player.MaxHealth}".WriteAt(col, row, ConsoleColor.White); row++;
    $"Mana: {Player.Mana}/{Player.MaxMana}".WriteAt(col, row, ConsoleColor.White); row++;
    $"Gold: {Player.Gold:C}g".WriteAt(col, row, ConsoleColor.White);
  }

  private static void SpellStats()
  {
    int count = 0;
    //Spells
    int col = StatusBox.Left + 140;
    int row = StatusBox.Top + 1;
    int colWidth = 18;
    BChars.MidTop.WriteAt(col - 2, row - 1, Color.Gold);
    BChars.MidBottom.WriteAt(col - 2, StatusBox.Height - 1, Color.Gold);
    for (int index = row; index < row + 6; index++)
    {
      BChars.Ver.WriteAt(col - 2, index, Color.Gold);
    }
    "Spells".WriteAt(col, row, Color.Gold);
    row++;
    for (int index = 0; index < 10; index++) // 10 spells max
    {
      if (index >= Player.Spells.Count) "None".WriteAt(col, row, Color.DimGray);
      else
      {
        Spell spell = Player.Spells[index];
        $"{spell.Name}: {spell.Description} ".WriteAt(col, row, ConsoleColor.White);
      }
      row++;
      count++;

      if (count < 5) continue;
      count = 0;
      row = StatusBox.Top + 2;
      col += colWidth + 2;
    }
  }

  private static void InventoryStats()
  {
    int col =
      //Inventory
      StatusBox.Left + 31;
    int row = StatusBox.Top + 1;
    int colWidth = 25;
    int count = 0;
    int totalBags = Inventory.Bags.Count;
    BChars.MidTop.WriteAt(col - 2, row - 1, Color.Gold);
    BChars.MidBottom.WriteAt(col - 2, StatusBox.Height - 1, Color.Gold);
    for (int index = row; index < row + 6; index++)
    {
      BChars.Ver.WriteAt(col - 2, index, Color.Gold);
    }

    Bag bag = Inventory.Bags[currentBag - 1];
    $"Inventory - Bag: {currentBag} of {totalBags}  (< or > to switch bags)".WriteAt(col, row, Color.Gold);
    row++;
    for (int index = 0; index < bag.Capacity; index++)
    {
      if (index >= bag.Items.Count) "Empty".PadRight(colWidth).WriteAt(col, row, Color.DimGray);
      else
      {
        Item item = bag.Items[index];
        item.WriteInventoryItem(col, row, colWidth);
      }
      row++;
      count++;
      if (count < 5) continue;
      count = 0;
      row = StatusBox.Top + 2;
      col += colWidth + 2;
    }
  }

  private static void ArmorStats()
  {
    int row = StatusBox.Top + 1;
    int col = StatusBox.Left + 2;

    //Armor
    "Armor".WriteAt(col, row, Color.Gold);
    row++;
    foreach (var armor in Player.ArmorSet)
    {
      string armorText = $"{armor.ArmorType}: ";
      armorText.WriteAt(col, row, ConsoleColor.White);
      armor.Name.PadRight(50 - armorText.Length).WriteAt(col + armorText.Length, row, ColorEx.RarityColor(armor.Rarity));
      row++;
    }
  }

  private static void MapSection()
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
    "Map objects: ".WriteAt(col, row, Color.Gold); row += 2;
    foreach (char type in Map.LevelOverlayTiles[Game.CurrentLevel].Keys)
    {
      foreach (Tile tile in Map.LevelOverlayTiles[Game.CurrentLevel][type])
      {
        if (!tile.IsVisible || tile.Type.Symbol == ' ') continue;
        tile.WriteLegendItem(col, row, OverlayBox.Width - 2);
        row++;
      }
    }
    // clear the rest of the legend box
    if (row >= OverlayBox.Top + OverlayBox.Height - 1) return;
    for (int index = row; index < OverlayBox.Top + OverlayBox.Height - 1; index++)
      " ".WriteAt(col, index, ConsoleColor.Black, ConsoleColor.Black, 0, OverlayBox.Width - 3);
  }

  private static void LegendSection()
  {
    int col = LegendBox.Left + 2;
    int row = LegendBox.Top + 1;
    "Game Play Legend: ".WriteAt(col, row, Color.Gold); row += 2;
    "[W,A,S,D] - Move".WriteAt(col, row, ConsoleColor.White); row++;
    "[Shift+W,A,S,D] - Jump".WriteAt(col, row, ConsoleColor.White); row++;
    "[T] - Attack Enemy".WriteAt(col, row, ConsoleColor.White); row++;
    "[1-0] - Cast Spell".WriteAt(col, row, ConsoleColor.White); row++;
    "[H] - Use Healing Potion".WriteAt(col, row, ConsoleColor.White); row++;
    "[M] - Use Mana Potion".WriteAt(col, row, ConsoleColor.White); row++;
    "[G] - Use Bandage".WriteAt(col, row, ConsoleColor.White); row++;
    "[O,C] - Open/Close Door".WriteAt(col, row, ConsoleColor.White); row++;
    "[< >] - Switch Bag Shown".WriteAt(col, row, ConsoleColor.White); row++;
    "[Esc] - Pause Menu".WriteAt(col, row, ConsoleColor.White); row++;
    "[Shift+I] - Inventory".WriteAt(col, row, ConsoleColor.White); row++;
    "[Shift+S] - Spells".WriteAt(col, row, ConsoleColor.White); row++;
    "[Shift+Q] - Quit".WriteAt(col, row, ConsoleColor.White);
  }

  internal static void MessageSection()
  {
    // display the last 10 messages or less.  allow scrolling up or down through messages in pages of 8
    int col = MessageBox.Left + 2;
    int row = MessageBox.Top + 1;
    if (messageOffset > 0) messageOffset = 0;
    if (messageOffset < -Messages.Count) messageOffset = -Messages.Count;
    int end = Messages.Count + messageOffset;
    if (Messages.Count < end) end = Messages.Count;
    int start = 0;
    if (end > MessageHeight) start = end - MessageHeight;
    if (end < MessageHeight && Messages.Count >= MessageHeight) end = MessageHeight;
    for (int index = start; index < end; index++)
    {
      Messages[index].WriteMessageAt(col, row);
      row++;
    }
    // clear the rest of the message box
    if (row >= MessageBox.Top + MessageBox.Height - 1) return;
    for (int index = row; index < MessageBox.Top + MessageBox.Height - 1; index++)
      " ".WriteAt(col, index, Color.Black, Color.Black, MessageWidth, 0);
  }

  private static void MessageLegend()
  {
    int col = MessageBox.Width - 30;
    int row = MessageBox.Top + 1;
    // draw the message Legend left border
    BChars.MidTop.WriteAt(col, row - 1, Color.Gold);
    BChars.MidBottom.WriteAt(col, MessageBox.Top + MessageBox.Height - 1, Color.Gold);
    for (int index = row; index < MessageBox.Top + MessageBox.Height - 1; index++)
      BChars.Ver.WriteAt(col, index, Color.Gold);
    col += 2;
    "Messages Legend: ".WriteAt(col, row, Color.Gold); row += 2;
    "[UpArrow] - Prev Message".WriteAt(col, row, Color.White); row++;
    "[DownArrow] - Next Message".WriteAt(col, row, Color.White); row++;
    "[PageUP] - Messages - 10".WriteAt(col, row, Color.White); row++;
    "[PageDown] - Messages + 10".WriteAt(col, row, Color.White); row++;
    "[Home] - First Message".WriteAt(col, row, Color.White); row++;
    "[End] - Last Message".WriteAt(col, row, Color.White);
  }

  private static bool AcceptableKeys()
  {
    return (!Player.InCombat && lastKey.Key is ConsoleKey.W or ConsoleKey.A or ConsoleKey.S or ConsoleKey.D);
  }

  internal static void KeyHandler()
  {
    // Capture and hide the key the user pressed
    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
    lastKey = keyInfo;
    if ((keyInfo.Modifiers & ConsoleModifiers.Shift) != 0)
    {
      switch (keyInfo.Key)
      {
        case ConsoleKey.W:
        case ConsoleKey.A:
        case ConsoleKey.S:
        case ConsoleKey.D:
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
        case ConsoleKey.E:
          Game.IsWon = true;
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
          Messages.Add(new Message($"You moved {Map.GetDirection(keyInfo.Key)}..."));
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
          messageOffset -= 8;
          break;
        case ConsoleKey.PageDown:
          messageOffset += 8;
          break;
        case ConsoleKey.UpArrow:
          messageOffset--;
          break;
        case ConsoleKey.DownArrow:
          messageOffset++;
          break;
        case ConsoleKey.Home:
          messageOffset = -Messages.Count;
          break;
        case ConsoleKey.End:
          messageOffset = 0;
          break;
        case ConsoleKey.OemComma:
          if (Inventory.Bags.Count > 1)
            currentBag--;
          break;
        case ConsoleKey.OemPeriod:
          if (currentBag < Inventory.Bags.Count)
            currentBag++;
          break;
        case ConsoleKey.O:
          Actions.OpenDoor();
          Map.SetVisibleArea(10);
          Map.WhatIsVisible();
          break;
        case ConsoleKey.C:
          Actions.CloseDoor();
          break;
        case ConsoleKey.T:
          Map.Player.Attack();
          break;
        case ConsoleKey.F5:
          Map.ShowAllMapTiles();
          break;
        case ConsoleKey.F6:
          Map.ShowAllOverlayTiles();
          break;
      }
    }
    ConsoleEx.FlushInput();
  }
}