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
  private static readonly Box ArmorBox = new(1, 0, 30, 8);
  private static readonly Box InventoryBox = new(ArmorBox.Right, 0, 110, 8);
  private static readonly Box SpellBox = new(InventoryBox.Right, 0, 40, 8);
  private static readonly Box PlayerBox = new(SpellBox.Right, 0, 31, 8);
  internal static readonly Box MapBox = new(1, ArmorBox.Bottom, 178, 35);
  internal static readonly Box MessageBox = new(1, MapBox.Bottom, MapBox.Right - 30, 12);
  private static readonly Box MessageLegendBox = new(MessageBox.Right, MessageBox.Top, 31, MessageBox.Height);
  private static readonly Box OverlayBox = new(MapBox.Right, MapBox.Top, 31, 29);
  private static readonly Box LegendBox = new(MapBox.Right, 35, OverlayBox.Width, 18);


  private static readonly Colors Colors = new()
  {
    Color = Color.Gold,
    HeaderColor = Color.Gold,
    TextColor = Color.White,
    FillColor = Color.DimGray,
  };

  internal static List<Message> Messages = new();
  internal static readonly int MessageWidth = MessageBox.Width - 3;
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
    GameBorders();
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

  private static void GameBorders()
  {
    ArmorBox.WriteBorder(BoxChars.Default, Colors.Color, Colors.BackgroundColor);
    InventoryBox.WriteBorder(BoxChars.Default, Colors.Color, Colors.BackgroundColor);
    SpellBox.WriteBorder(BoxChars.Default, Colors.Color, Colors.BackgroundColor);
    PlayerBox.WriteBorder(BoxChars.Default, Colors.Color, Colors.BackgroundColor);
    $"[ {Game.Title} - The {Game.CurrentDungeon} ]".WriteAlignedAt(HAlign.Center, VAlign.Top, Colors.TextColor, Colors.BackgroundColor);
    MapBox.WriteBorder(BoxChars.Default, Colors.Color, Colors.BackgroundColor);
    OverlayBox.WriteBorder(BoxChars.Default, Colors.Color, Colors.BackgroundColor);
    MessageBox.WriteBorder(BoxChars.Default, Colors.Color, Colors.BackgroundColor);
    MessageLegendBox.WriteBorder(BoxChars.Default,Colors.Color, Colors.BackgroundColor);
    LegendBox.WriteBorder(BoxChars.Default, Colors.Color, Colors.BackgroundColor);

    // now to clean up the corners
    // Top section
    BoxChars.Default.TopCtr.WriteAt(ArmorBox.Right, ArmorBox.Top, Colors.Color, Colors.BackgroundColor);
    BoxChars.Default.TopCtr.WriteAt(InventoryBox.Right, InventoryBox.Top, Colors.Color, Colors.BackgroundColor);
    BoxChars.Default.TopCtr.WriteAt(SpellBox.Right, SpellBox.Top, Colors.Color, Colors.BackgroundColor);
    BoxChars.Default.MidLeft.WriteAt(ArmorBox.Left, ArmorBox.Bottom, Colors.Color, Colors.BackgroundColor);
    BoxChars.Default.BotCtr.WriteAt(ArmorBox.Right, ArmorBox.Bottom, Colors.Color, Colors.BackgroundColor);
    BoxChars.Default.BotCtr.WriteAt(InventoryBox.Right, InventoryBox.Bottom, Colors.Color, Colors.BackgroundColor);
    BoxChars.Default.MidCtr.WriteAt(SpellBox.Right, SpellBox.Bottom, Colors.Color, Colors.BackgroundColor);
    BoxChars.Default.MidRight.WriteAt(PlayerBox.Right, PlayerBox.Bottom, Colors.Color, Colors.BackgroundColor);

    BoxChars.Default.MidLeft.WriteAt(LegendBox.Left, LegendBox.Top, Colors.Color, Colors.BackgroundColor);
    BoxChars.Default.MidRight.WriteAt(LegendBox.Right, LegendBox.Top, Colors.Color, Colors.BackgroundColor);

    BoxChars.Default.MidLeft.WriteAt(MessageBox.Left, MessageBox.Top, Colors.Color, Colors.BackgroundColor);

    BoxChars.Default.TopCtr.WriteAt(MessageLegendBox.Left, MessageLegendBox.Top, Colors.Color, Colors.BackgroundColor);
    BoxChars.Default.MidRight.WriteAt(MessageLegendBox.Right, MessageLegendBox.Top, Colors.Color, Colors.BackgroundColor);
    BoxChars.Default.BotCtr.WriteAt(MessageLegendBox.Left, MessageLegendBox.Bottom, Colors.Color, Colors.BackgroundColor);
    BoxChars.Default.BotCtr.WriteAt(LegendBox.Left, LegendBox.Bottom, Colors.Color, Colors.BackgroundColor);

  }

  internal static void StatusSection()
  {
    ArmorStats();
    InventoryStats();
    SpellStats();
    PlayerStats();
  }

  private static void ArmorStats()
  {
    int row = ArmorBox.Top + 1;
    int col = ArmorBox.Left + 2;

    //Armor
    "Armor".WriteAt(col, row, Colors.HeaderColor);
    row++;
    foreach (Armor? armor in Player.ArmorSet)
    {
      string armorText = $"{armor.ArmorType}: ";
      armorText.WriteAt(col, row, Colors.TextColor);
      armor.Name.PadRight(ArmorBox.Width - armorText.Length - 3)
        .WriteAt(col + armorText.Length, row, ColorEx.RarityColor(armor.Rarity));
      row++;
    }
  }

  private static void InventoryStats()
  {
    //Inventory
    int col = InventoryBox.Left + 2;
    int row = InventoryBox.Top + 1;
    int colWidth = 25;
    int count = 0;
    int totalBags = Inventory.Bags.Count;
    Bag bag = Inventory.Bags[currentBag];
    $"Inventory - Bag: {currentBag + 1} of {totalBags}  (\u2190 or \u2192 to switch bags)".WriteAt(col, row, Colors.HeaderColor);
    row++;
    for (int index = 0; index < bag.Capacity; index++)
    {
      if (index >= bag.Items.Count) 
        "Empty".PadRight(colWidth).WriteAt(col, row, Colors.FillColor);
      else
      {
        Item item = bag.Items[index];
        item.WriteInventoryItem(col, row, colWidth);
      }

      row++;
      count++;
      if (count < 5) continue;
      count = 0;
      row = InventoryBox.Top + 2;
      col += colWidth + 2;
    }
  }

  private static void SpellStats()
  {
    int count = 0;
    //Spells
    int col = SpellBox.Left + 2;
    int row = SpellBox.Top + 1;
    int colWidth = 18;
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
      row = SpellBox.Top + 2;
      col += colWidth + 2;
    }
  }

  private static void PlayerStats()
  {
    //Player Stats
    int col = PlayerBox.Left + 2;
    int row = PlayerBox.Top + 1;
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
    // clear the rest of the overlay box
    if (row >= OverlayBox.Bottom) return;
    for (int index = row; index < OverlayBox.Bottom; index++)
      " ".WriteAt(col, index, Colors.BackgroundColor, Colors.BackgroundColor, OverlayBox.Width - 3);
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
    "[F] - Open/Close Door".WriteAt(col, row, Colors.TextColor);
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
      " ".WriteAt(col, index, Colors.BackgroundColor, Colors.BackgroundColor, MessageWidth);
  }

  private static void MessageLegend()
  {
    int col = MessageLegendBox.Left;
    int row = MessageLegendBox.Top + 1;
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
          messageOffset -= Message.PageSize;
          break;
        case ConsoleKey.PageDown:
          messageOffset += Message.PageSize;
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
        case ConsoleKey.F:
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
