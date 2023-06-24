using ConsoleDungeonCrawler.Extensions;

namespace ConsoleDungeonCrawler;

/// <summary>
/// This is the main type for the game
/// </summary>
public static class Program
{
  /// <summary>
  /// This is the main entry point of the application
  /// </summary>
  /// <param name="args"></param>
  public static void Main(string[] args)
  {
    // Setup console to support unicode characters
    ConsoleEx.InitializeConsole();
    Game.Game.Run();
  }
}