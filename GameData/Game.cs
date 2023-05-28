using System.Text.RegularExpressions;
using ConsoleDungeonCrawler.Extensions;

namespace ConsoleDungeonCrawler.GameData
{
  internal static class Game
  {
    internal static string Title = "Console Dungeon Crawler";
    internal static bool IsGameOver { get; set; }
    internal static bool IsGameWon { get; set; }
    internal static Box StatusBox = new Box(0, 0, 200, 8);
    internal static Box MapBox = new Box(0, 7, 170, 35);
    internal static Box LegendBox = new Box(169, 7, 31, 35);
    internal static Box MessageBox = new Box(0, 41, 200, 10);
    internal static List<string> Messages = new List<string>();

    private static BoxCharsEx boxCharsEx = new BoxCharsEx("\xe2948d", "\xe29491", "\\xd59f", "\xe29499", "\xe295bc", "\xe29482");
    private static char HBorderChar = '~';
    private static char VBorderChar = '|';

    public static void Run()
    {
      ConsoleEx.Clear();
      ConsoleEx.InitializeConsole();
      Map.Instance = new Map(MapBox);

        DisplayTitleScreen();
        DisplayGamePlayScreen();
    }
    
    // Create a method that displays the title screen in ascii art
    private static void DisplayTitleScreen()
    {
      Console.WriteLine("Welcome to the Dungeon Crawler!");
      Console.WriteLine("Press any key to start the game.");
      Console.ReadKey();
    }

    // Create a method that displays the game over screen in ascii art
    private static void DisplayGameOverScreen()
    {
      DisplayCreditsScreen();
      Console.WriteLine("Game Over!");
      Console.WriteLine("Press any key to exit the game.");
      Console.ReadKey();
      Console.Clear();
    }

    // Create a method that displays the game won screen in ascii art
    private static void DisplayGameWonScreen()
    {
      Console.WriteLine("You won the game!");
      Console.WriteLine("Press any key to exit the game.");
      Console.ReadKey();
    }

    private static void DisplayCreditsScreen()
    {
      Console.WriteLine("Thanks for playing!");
      Console.WriteLine("Press any key to exit the game.");
      Console.ReadKey();
    }

    private static void DisplayGamePlayScreen()
    {
      Console.Clear();
      Console.SetCursorPosition(0, 0);
      while (!IsGameOver && !IsGameWon)
      {
        DrawGamePlayScreen();
        Input.ProcessPlayerInput();
      }
      if (IsGameOver)
      {
        DisplayGameOverScreen();
      }
      else if (IsGameWon)
      {
        DisplayGameWonScreen();
      }

    }

    private static void DrawGamePlayScreen()
    {
      DrawGameBorders();
      DrawStatusSection();
      DrawMapSection();
      DrawLegendSection();
      DrawMessageSection();
    }

    private static void DrawGameBorders()
    {
      ConsoleEx.WriteBorder(StatusBox, HBorderChar, VBorderChar, ConsoleColor.Yellow);
      ConsoleEx.WriteAlignedAt($"[{Title}]", HAlign.Center, VAlign.Top, ConsoleColor.White);
      ConsoleEx.WriteBorder(MapBox, HBorderChar, VBorderChar, ConsoleColor.Yellow);
      ConsoleEx.WriteBorder(LegendBox, HBorderChar, VBorderChar, ConsoleColor.Yellow);
      ConsoleEx.WriteBorder(MessageBox, HBorderChar, VBorderChar, ConsoleColor.Yellow);
    }

    private static void DrawGameBordersEx()
    {
      ConsoleEx.WriteBorder(StatusBox, boxCharsEx, ConsoleColor.Yellow);
      ConsoleEx.WriteBorder(MapBox, boxCharsEx, ConsoleColor.Yellow);
      ConsoleEx.WriteBorder(LegendBox, boxCharsEx, ConsoleColor.Yellow);
      ConsoleEx.WriteBorder(MessageBox, boxCharsEx, ConsoleColor.Yellow);
    }

    private static void DrawStatusSection()
    {
      int row = StatusBox.Top + 1;
      int col = StatusBox.Left + 2;

      //Armor
      ConsoleEx.WriteAt("Armor", col, row, ConsoleColor.Yellow);
      row++;
      foreach (var armor in Player.ArmorSet)
      {
        ConsoleEx.WriteAt($"{armor.Type.ToString()}: {armor.Name} ", col, row, ConsoleColor.White);
        row++;
      }

      //Items
      col = StatusBox.Left + 60;
      row = StatusBox.Top + 1;
      ConsoleEx.WriteAt("Items", col, row, ConsoleColor.Yellow);
      row++;
      foreach (var item in Player.Inventory)
      {
        ConsoleEx.WriteAt($"{item.Key}: {item.Value.Description} ", col, row, ConsoleColor.White);
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
      col = StatusBox.Left + 180;
      row = StatusBox.Top + 1;
      ConsoleEx.WriteAt("Player", col, row, ConsoleColor.Yellow);
      row++;
      ConsoleEx.WriteAt($"Name: {Player.Name}", col, row, ConsoleColor.White);
      row++;
      ConsoleEx.WriteAt($"Class: {Player.Class}", col, row, ConsoleColor.White);
      row++;
      ConsoleEx.WriteAt($"Level: {Player.Level}", col, row, ConsoleColor.White);
      row++;
      ConsoleEx.WriteAt($"Health: {Player.Health}/{Player.MaxHealth}", col, row, ConsoleColor.White);


    }

    private static void DrawMapSection()
    {
      Map.SetVisibleArea(10);

      Map.Instance.DrawMap();
      Map.Instance.DrawOverlay();
    }

    private static void DrawLegendSection()
    {
      int col = LegendBox.Left + 2;
      int row = LegendBox.Top + 1;
      foreach (char type in Map.OverlayObjects.Keys)
      {
        foreach (MapObject mapObject in Map.OverlayObjects[type])
        {
          if (!mapObject.Visible) continue;
          ConsoleEx.WriteLegendItem(mapObject, col, row, LegendBox.Width - 2);
          row++;
        }
      }
    }

    private static void DrawMessageSection()
    {
      int row = MessageBox.Top + 1;
      int col = MessageBox.Left + 2;
      if (Messages.Count > 0)
      {
        int offset = Messages.Count - 8 > 0 ? Messages.Count - 8 : 0;
        for (int index = 0 + offset; index < Messages.Count; index++)
        {
          string? message = Messages[index];
          ConsoleEx.WriteAt(message, col, row, ConsoleColor.White);
          row++;
        }
      }
    }
  }
}
