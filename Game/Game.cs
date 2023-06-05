using System.Drawing;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;
using ConsoleDungeonCrawler.Game.Maps;
using ConsoleDungeonCrawler.Game.Screens;

namespace ConsoleDungeonCrawler.Game
{
  internal static class Game
  {
    internal static string Title = "Console Dungeon Crawler";
    internal static bool IsOver { get; set; }
    internal static bool IsWon { get; set; }
    internal static bool IsPaused { get; set; }

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
      GamePlayScreen.Messages.Add(new Message("You have entered the Dungeon!", Color.Chartreuse, Color.Black));
      GamePlayScreen.Messages.Add(new Message("You look around...",Color.White, Color.Black));
      GamePlayScreen.Draw();
      while (!IsOver && !IsWon)
      {
        if (IsPaused)
          GamePausedScreen.Draw();
        else
        {
          PlayerInput.Process();
          GamePlayScreen.Update();
        }
      }
      if (IsOver)
        GameOverScreen.Draw();
      else if (IsWon)
        GameWonScreen.Draw();

    }
  }
}
