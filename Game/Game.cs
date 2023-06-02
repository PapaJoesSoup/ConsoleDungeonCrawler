using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Maps;
using ConsoleDungeonCrawler.Game.Screens;

namespace ConsoleDungeonCrawler.Game
{
  internal static class Game
  {
    internal static string Title = "Console Dungeon Crawler";
    internal static bool IsGameOver { get; set; }
    internal static bool IsGameWon { get; set; }

    public static void Run()
    {
      ConsoleEx.Clear();
      ConsoleEx.InitializeConsole();
      Map.Instance = new Map(GamePlayScreen.MapBox);

      TitleScreen.Draw();
      PlayGame();
    }

    private static void PlayGame()
    {
      ConsoleEx.Clear();
      //Console.SetCursorPosition(0, 0);
      GamePlayScreen.Draw();
      while (!IsGameOver && !IsGameWon)
      {
        PlayerInput.Process();
        GamePlayScreen.Update();
      }
      if (IsGameOver)
      {
        GameOverScreen.Draw();
      }
      else if (IsGameWon)
      {
        GameWonScreen.Draw();
      }

    }
  }
}
