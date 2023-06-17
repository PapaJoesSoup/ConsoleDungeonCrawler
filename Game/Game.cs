﻿using System.Drawing;
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
    internal static string CurrentDungeon = "";
    internal static string MapPath = "Game/Maps/Data/";

    internal static Dictionary<string, Dictionary<string, string>> Dungeons = new Dictionary<string, Dictionary<string, string>>();

    internal static void LoadDungeons()
    {
      string[] folders = Directory.GetDirectories(MapPath);
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
        Dungeons.Add(dungeon.Split("/").Last(), maps);
      }
      CurrentDungeon = Dungeons.Keys.First();
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
