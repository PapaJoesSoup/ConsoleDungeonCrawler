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


    internal static Dictionary<string, Dictionary<string, string>> Dungeons = new Dictionary<string, Dictionary<string, string>>()
    {
      {
        "Dungeon 1", new Dictionary<string, string>()
        {
          {"Name", "Dungeon 1"},
          {"Description", "A small dungeon"},
          {"Map", "Dungeon1"},
          {"Start", "1,1"},
          {"End", "10,10"}
        }
      }
    };

    internal static void LoadDungeons()
    {
      string folderPath  = "Game/Maps/Data";
      string[] folders = Directory.GetDirectories(folderPath);
      Dungeons.Clear();
      foreach (string dungeon in folders)
      {
        string[] files = Directory.GetFiles(dungeon); 
        Dictionary<string, string> maps = new Dictionary<string, string>();
        foreach (string map in files)
        {
          string fileName = Path.GetFileName(map);
          maps.Add(fileName, map);
        }
        Dungeons.Add(dungeon.Split("\\")[1], maps);
      }
    }
    public static void Run()
    {
      LoadDungeons();
      Map.Instance = new Map(GamePlay.MapBox);
      Screens.Title.Draw();
      PlayGame();
    }

    private static void PlayGame()
    {
      ConsoleEx.Clear();
      GamePlay.Messages.Add(new Message("You have entered the Dungeon!", Color.Chartreuse, Color.Black));
      GamePlay.Messages.Add(new Message("You look around...",Color.White, Color.Black));
      GamePlay.Draw();
      while (!IsOver && !IsWon)
      {
        if (IsPaused)
          GamePaused.Draw();
        else
        {
          PlayerInput.Process();
          GamePlay.Update();
        }
      }
      if (IsOver)
        GameOver.Draw();
      else if (IsWon)
        GameWon.Draw();

    }
  }
}
