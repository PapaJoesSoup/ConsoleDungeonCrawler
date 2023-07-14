using System.Drawing;
using System.Text;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;
using ConsoleDungeonCrawler.Game.Screens;
using ConsoleDungeonCrawler.Game.Screens.Dialogs;

namespace ConsoleDungeonCrawler.Game;

internal static class Game
{
  #region Properties
  internal const string Title = "Console Dungeon Crawler";
  private const string DataPath = $"Game/Data/";
  private const string MapPath = $"{DataPath}Maps/";
  private const string ArtPath = $"{DataPath}Art/";
  internal const string SoundPath = $"{DataPath}Sounds/";

  internal static bool IsMainMenu { get; set; }
  internal static bool IsPaused { get; set; }
  internal static bool IsOver { get; set; }
  internal static bool IsWon { get; set; }
  internal static bool IsRestart { get; set; }
  internal static bool IsQuit { get; set; }

  internal static string CurrentDungeon = "";
  internal static int CurrentLevel = 0;

  internal static readonly StringBuilder GameTitleArt;
  internal static readonly StringBuilder GameWonArt;
  internal static readonly StringBuilder GameOverArt;
  internal static readonly StringBuilder CharacterArt;
  internal static readonly StringBuilder GameTitleText;
  internal static readonly StringBuilder GameEnterText;
  internal static readonly StringBuilder GameWonText;
  internal static readonly StringBuilder GameOverText;

  internal static readonly Dictionary<string, Dictionary<string, string>> Dungeons = new();
  #endregion Properties

  static Game()
  {
    // Lets cache the art so we don't have to read it from disk every time we need it
    GameTitleArt = new StringBuilder();
    GameTitleArt.Append(File.ReadAllText($"{Game.ArtPath}/GameTitleArt.txt"));
    GameTitleText = new StringBuilder();
    GameTitleText.Append(File.ReadAllText($"{Game.ArtPath}/GameTitleText.txt"));
    GameEnterText = new StringBuilder();
    GameEnterText.Append(File.ReadAllText($"{Game.ArtPath}/GameEnterText.txt"));

    GameWonArt = new StringBuilder();
    GameWonArt.Append(File.ReadAllText($"{Game.ArtPath}/GameWonArt.txt"));
    GameWonText = new StringBuilder();
    GameWonText.Append(File.ReadAllText($"{Game.ArtPath}/GameWonText.txt"));

    GameOverArt = new StringBuilder();
    GameOverArt.Append(File.ReadAllText($"{Game.ArtPath}/GameOverArt.txt"));
    GameOverText = new StringBuilder();
    GameOverText.Append(File.ReadAllText($"{Game.ArtPath}/GameOverText.txt"));

    CharacterArt = new StringBuilder();
    CharacterArt.Append(File.ReadAllText($"{Game.ArtPath}/CharacterArt.txt"));

    LoadDungeons();
  }

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
        SoundSystem.PlayTitle();
        GameTitle.Draw();
        PlayGame();
      }
      if (IsRestart)
      {
        IsRestart = false;
        SoundSystem.PlayEnter();
        Thread.Sleep(6000);
        PlayGame();
      }
      if (IsOver)
      {
        IsOver = false;
        SoundSystem.PlayTitle();
        GameOver.Draw();
        continue;
      }

      if (IsWon)
      {
        IsWon = false;
        SoundSystem.PlayTitle();
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
    SoundSystem.PlayBackground();
    Map.Instance = new Map(GamePlay.MapBox);
    GamePlay.Messages = new();
    ConsoleEx.Clear();
    GamePlay.Messages.Add(new Message($"You have entered the {CurrentDungeon} Dungeon!", Color.Chartreuse, Color.Black));
    GamePlay.Messages.Add(new Message("You look around...", Color.White, Color.Black));
    GamePlay.Draw();
  }
}