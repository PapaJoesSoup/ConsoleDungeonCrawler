using System.Drawing;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;
using ConsoleDungeonCrawler.Game.Screens;

namespace ConsoleDungeonCrawler.Game;

internal static class Game
{
  internal const string Title = "Console Dungeon Crawler";
  private const string MapPath = "Game/Data/Maps/";
  internal const string ArtPath = "Game/Data/Art/";
  internal static bool IsMainMenu { get; set; }
  internal static bool IsPaused { get; set; }
  internal static bool IsOver { get; set; }
  internal static bool IsWon { get; set; }
  internal static bool IsRestart { get; set; }
  internal static bool IsQuit { get; set; }

  internal static string CurrentDungeon = "";
  internal static int CurrentLevel = 0;

  internal static readonly Dictionary<string, Dictionary<string, string>> Dungeons = new();

  private static void LoadDungeons()
  {
    string[] folders = Directory.GetDirectories(MapPath);
    Dungeons.Clear();
    foreach (string dungeon in folders)
    {
      string[] files = Directory.GetFiles(dungeon);
      Dictionary<string, string> maps = new();
      foreach (string map in files)
      {
        string fileName = Path.GetFileName(map);
        maps.Add(fileName, map);
      }
      Dungeons.Add(dungeon.Split("/").Last(), maps);
    }
    CurrentDungeon = Dungeons.Keys.First();
  }

  internal static void Run()
  {
    IsMainMenu = true;
    while (!IsQuit)
    {
      if (IsMainMenu)
      {
        IsMainMenu = false;
        LoadDungeons();
        GameTitle.Draw();
        PlayGame();
      }
      if (IsRestart)
      {
        IsRestart = false;
        PlayGame();
      }
      if (IsOver)
      {
        IsOver = false;
        GameOver.Draw();
        continue;
      }

      if (IsWon)
      {
        IsWon = false;
        GameWon.Draw();
        continue;
      }
      if (IsPaused) GamePaused.Draw();
      GamePlay.KeyHandler();
      GamePlay.Update();
    }
    Environment.Exit(0);
  }

  private static void PlayGame()
  {
    Map.Instance = new Map(GamePlay.MapBox);
    GamePlay.Messages = new();
    ConsoleEx.Clear();
    GamePlay.Messages.Add(new Message($"You have entered the {CurrentDungeon} Dungeon!", Color.Chartreuse, Color.Black));
    GamePlay.Messages.Add(new Message("You look around...", Color.White, Color.Black));
    GamePlay.Draw();
  }
}