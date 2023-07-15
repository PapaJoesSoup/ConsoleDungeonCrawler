using System.Drawing;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;
using ConsoleDungeonCrawler.Game.Entities.Items;
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
  private static readonly Box MessageBox = new(1, 41, 178, 12);
  private static readonly Box OverlayBox = new(178, 7, 31, 29);
  private static readonly Box LegendBox = new(178, 35, 31, 18);
  private static readonly Colors Colors = new()
  {
    Color = Color.Gold,
    HeaderColor = Color.Gold,
    TextColor = Color.White,
    FillColor = Color.DimGray,
  };

  internal static List<Message> Messages = new();
  internal static readonly int MessageWidth = MessageBox.Width - 33;
  private static readonly int MessageHeight = MessageBox.Height - 2;

  // MessageOffset is a negative number that decrements the index of the first message to display in the message Section
  private static int messageOffset;


  // This is used to manage display of bags in the inventory section
  private static int currentBag;

  // LastKey is used to help with player combat state management
  private static ConsoleKeyInfo lastKey;

  #endregion Properties

  internal static void Draw()
  {
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
    StatusBox.WriteBorder(BoxChars.Default, Colors.Color);
    $"[ {Game.Title} - The {Game.CurrentDungeon} ]".WriteAlignedAt(HAlign.Center, VAlign.Top, Colors.TextColor);
    MapBox.WriteBorder(BoxChars.Default, Colors.Color);
    OverlayBox.WriteBorder(BoxChars.Default, Colors.Color);
    MessageBox.WriteBorder(BoxChars.Default, Colors.Color);
    LegendBox.WriteBorder(BoxChars.Default, Colors.Color);

    // now to clean up the corners
    BoxChars.Default.MidLeft.WriteAt(MapBox.Left, MapBox.Top, Colors.Color);
    BoxChars.Default.TopCtr.WriteAt(OverlayBox.Left, MapBox.Top, Colors.Color);
    BoxChars.Default.MidRight.WriteAt(OverlayBox.Left + OverlayBox.Width - 1, MapBox.Top, Colors.Color);

    BoxChars.Default.MidLeft.WriteAt(LegendBox.Left, LegendBox.Top, Colors.Color);
    BoxChars.Default.MidRight.WriteAt(LegendBox.Left + LegendBox.Width - 1, LegendBox.Top, Colors.Color);

    BoxChars.Default.MidLeft.WriteAt(MessageBox.Left, MessageBox.Top, Colors.Color);
    BoxChars.Default.MidRight.WriteAt(MessageBox.Left + MessageBox.Width - 1, MessageBox.Top, Colors.Color);

    BoxChars.Default.BotCtr.WriteAt(LegendBox.Left, LegendBox.Top + LegendBox.Height - 1, Colors.Color);

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
    BoxChars.Default.TopCtr.WriteAt(col - 2, row - 1, Colors.Color);
    BoxChars.Default.MidCtr.WriteAt(col - 2, StatusBox.Height - 1, Colors.Color);
    for (int index = row; index < row + 6; index++)
    {
      BoxChars.Default.Ver.WriteAt(col - 2, index, Colors.Color);
    }

    $"Player - Level: {Player.Level}".WriteAt(col, row, Colors.HeaderColor);
    row++;
    $"Class: {Player.Class}".WriteAt(col, row, Colors.TextColor);
    row++;
    "Weapon: ".WriteAt(col, row, Colors.TextColor);
    $"{Player.Weapon.Name}".WriteAt(col + 8, row, ColorEx.RarityColor(Player.Weapon.Rarity));
    row++;
    $"Health: {Player.Health}/{Player.MaxHealth}".WriteAt(col, row, Colors.TextColor);
    row++;
    $"Mana: {Player.Mana}/{Player.MaxMana}".WriteAt(col, row, Colors.TextColor);
    row++;
    $"Gold: {Player.Gold:C}g".WriteAt(col, row, Colors.TextColor);
  }

  private static void SpellStats()
  {
    int count = 0;
    //Spells
    int col = StatusBox.Left + 140;
    int row = StatusBox.Top + 1;
    int colWidth = 18;
    BoxChars.Default.TopCtr.WriteAt(col - 2, row - 1, Colors.Color);
    BoxChars.Default.BotCtr.WriteAt(col - 2, StatusBox.Height - 1, Colors.Color);
    for (int index = row; index < row + 6; index++)
    {
      BoxChars.Default.Ver.WriteAt(col - 2, index, Colors.Color);
    }

    "Spells".WriteAt(col, row, Colors.Color);
    row++;
    for (int index = 0; index < 10; index++) // 10 spells max
    {
      if (index >= Player.Spells.Count) "None".WriteAt(col, row, Colors.FillColor);
      else
      {
        Spell spell = Player.Spells[index];
        $"{spell.Name}: {spell.Description} ".WriteAt(col, row, Colors.TextColor);
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
    //Inventory
    int col = StatusBox.Left + 31;
    int row = StatusBox.Top + 1;
    int colWidth = 25;
    int count = 0;
    int totalBags = Inventory.Bags.Count;
    BoxChars.Default.TopCtr.WriteAt(col - 2, row - 1, Colors.Color);
    BoxChars.Default.BotCtr.WriteAt(col - 2, StatusBox.Height - 1, Colors.Color);
    for (int index = row; index < row + 6; index++)
    {
      BoxChars.Default.Ver.WriteAt(col - 2, index, Colors.Color);
    }

    Bag bag = Inventory.Bags[currentBag];
    $"Inventory - Bag: {currentBag + 1} of {totalBags}  (\u2190 or \u2192 to switch bags)".WriteAt(col, row, Colors.HeaderColor);
    row++;
    for (int index = 0; index < bag.Capacity; index++)
    {
      if (index >= bag.Items.Count) "Empty".PadRight(colWidth).WriteAt(col, row, Colors.FillColor);
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
    "Armor".WriteAt(col, row, Colors.HeaderColor);
    row++;
    foreach (Armor? armor in Player.ArmorSet)
    {
      string armorText = $"{armor.ArmorType}: ";
      armorText.WriteAt(col, row, Colors.TextColor);
      armor.Name.PadRight(50 - armorText.Length)
        .WriteAt(col + armorText.Length, row, ColorEx.RarityColor(armor.Rarity));
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
    "Map objects: ".WriteAt(col, row, Colors.HeaderColor);
    row += 2;
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
      " ".WriteAt(col, index, Colors.BackgroundColor, Colors.BackgroundColor, 0, OverlayBox.Width - 3);
  }

  private static void LegendSection()
  {
    int col = LegendBox.Left + 2;
    int row = LegendBox.Top + 1;
    "Game Play Legend: ".WriteAt(col, row, Colors.HeaderColor);
    row += 2;
    "[W,A,S,D] - Move".WriteAt(col, row, Colors.TextColor);
    row++;
    "[Shift+W,A,S,D] - Jump".WriteAt(col, row, Colors.TextColor);
    row++;
    "[T] - Attack Enemy".WriteAt(col, row, Colors.TextColor);
    row++;
    "[1-0] - Cast Spell".WriteAt(col, row, Colors.TextColor);
    row++;
    "[H] - Use Healing Potion".WriteAt(col, row, Colors.TextColor);
    row++;
    "[M] - Use Mana Potion".WriteAt(col, row, Colors.TextColor);
    row++;
    "[G] - Use Bandage".WriteAt(col, row, Colors.TextColor);
    row++;
    "[Enter] - Open/Close Door".WriteAt(col, row, Colors.TextColor);
    row++;
    "[\u2190][\u2192] - Switch Bag".WriteAt(col, row, Colors.TextColor);
    row++;
    "[Esc] - Pause Menu".WriteAt(col, row, Colors.TextColor);
    row++;
    "[Shift+G] - Game Options".WriteAt(col, row, Colors.TextColor);
    row++;
    "[Shift+I] - Inventory".WriteAt(col, row, Colors.TextColor);
    row++;
    "[Shift+P] - Spells".WriteAt(col, row, Colors.TextColor);
    row++;
    "[Shift+Q] - Quit".WriteAt(col, row, Colors.TextColor);
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
      " ".WriteAt(col, index, Colors.BackgroundColor, Colors.BackgroundColor, MessageWidth, 0);
  }

  private static void MessageLegend()
  {
    int col = MessageBox.Width - 30;
    int row = MessageBox.Top + 1;
    // draw the message Legend left border
    BoxChars.Default.TopCtr.WriteAt(col, row - 1, Colors.Color);
    BoxChars.Default.BotCtr.WriteAt(col, MessageBox.Top + MessageBox.Height - 1, Colors.Color);
    for (int index = row; index < MessageBox.Top + MessageBox.Height - 1; index++)
      BoxChars.Default.Ver.WriteAt(col, index, Colors.Color);
    col += 2;
    "Messages Legend: ".WriteAt(col, row, Colors.HeaderColor);
    row += 2;
    "[\u2191] - Prev Message".WriteAt(col, row, Colors.TextColor);
    row++;
    "[\u2193] - Next Message".WriteAt(col, row, Colors.TextColor);
    row++;
    "[PageUP] - Messages - 10".WriteAt(col, row, Colors.TextColor);
    row++;
    "[PageDown] - Messages + 10".WriteAt(col, row, Colors.TextColor);
    row++;
    "[Home] - First Message".WriteAt(col, row, Colors.TextColor);
    row++;
    "[End] - Last Message".WriteAt(col, row, Colors.TextColor);
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
          SoundSystem.PlayEffect(SoundSystem.MSounds[Sound.GameOver]);
          break;
        case ConsoleKey.E:
          Game.IsWon = true;
          SoundSystem.PlayEffect(SoundSystem.MSounds[Sound.GameWon]);
          break;
        case ConsoleKey.G:
          GameOptions.Draw();
          break;
        case ConsoleKey.B:
          ThemeConfig.Draw();
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
          Actions.ProcessOverlayItem();
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
        case ConsoleKey.LeftArrow:
            currentBag--;
          if (currentBag<0) currentBag = Inventory.Bags.Count-1;
          break;
        case ConsoleKey.RightArrow:
          currentBag++;
          if (currentBag > Inventory.Bags.Count - 1) currentBag = 0;
          break;
        case ConsoleKey.Enter:
          Actions.OpenCloseDoor();
          Map.SetVisibleArea(10);
          Map.WhatIsVisible();
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
