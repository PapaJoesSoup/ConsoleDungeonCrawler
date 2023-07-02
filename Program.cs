using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Screens.Dialogs;

namespace ConsoleDungeonCrawler;

/// <summary>
/// This is the main type for the game
/// </summary>
public static class Program
{
  internal static readonly int MinWindowWidth = 209;
  internal static readonly int MinWindowHeight = 53;

  /// <summary>
  /// This is the main entry point of the application
  /// </summary>
  /// <param name="args"></param>
  public static void Main(string[] args)
  {
    // Setup console to support unicode characters
    ConsoleEx.InitializeConsole();
    if (Console.WindowWidth < Program.MinWindowWidth || Console.WindowHeight < Program.MinWindowHeight)
    {
      TerminalConfiguration.Draw();
      Environment.Exit(0);
    }
    Game.Game.Run();
  }
}