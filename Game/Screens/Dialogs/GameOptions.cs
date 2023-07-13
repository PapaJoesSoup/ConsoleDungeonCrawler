using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;
using System.Drawing;

namespace ConsoleDungeonCrawler.Game.Screens.Dialogs;

internal static class GameOptions
{
  #region Properties
  private static bool dialogOpen;
  private static readonly Box Box = new(Console.WindowWidth / 2 - 41, Console.WindowHeight / 2 - 8, 82, 17);

  private static readonly Colors Colors = new Colors()
  {
    FillColor = Color.DarkSlateGray,
    TextColor = Color.Cyan
  };

  private static readonly Dictionary<string, List<object?>> Options = new();
  private static int activeOption;
  private static int optionCount;
  #endregion Properties

  static GameOptions()
  {
    BuildOptionList();
  }

  internal static void Draw()
  {
    dialogOpen = true;
    while (dialogOpen)
    {
      Dialog.Draw($" {Game.Title} - Options", Colors.Color, Colors.BackgroundColor, Colors.FillColor, Colors.TextColor, Box);

      int i = 0;
      int y = Box.Top + 2;
      int x = Box.Left + 5;
      "Use the Up and Down Arrow Keys to Select an option".WriteAt(x, y, Colors.TextColor, Colors.FillColor); y++;
      "Use Left / Right Arrow Keys to select value of option.  Enter to Change".WriteAt(x, y, Colors.TextColor, Colors.FillColor); y += 2;

      foreach (string header in Options.Keys)
      {
        header.WriteAt(x, y, Colors.TextColor, Colors.FillColor);
        y++;
        foreach (object? option in Options[header])
        {
          switch (option)
          {
            case GameOption<bool> bOption:
              bOption.Name.WriteAt(x + 2, y, activeOption == i ? Colors.SelectedColor : Colors.TextColor, activeOption == i ? Colors.SelectedBackgroundColor : Colors.FillColor);
              "True".WriteAt(x + 30, y, bOption.ActionIdx == 1 ? Colors.SelectedColor : Colors.TextColor, bOption.ActionIdx == 1 ? Colors.SelectedBackgroundColor : Colors.FillColor);
              "False".WriteAt(x + 36, y, bOption.ActionIdx == 0 ? Colors.SelectedColor : Colors.TextColor, bOption.ActionIdx == 0 ? Colors.SelectedBackgroundColor : Colors.FillColor);
              y++;
              i++;
              break;
            case GameOption<int> iOption:
              iOption.Name.WriteAt(x + 2, y, activeOption == i ? Colors.SelectedColor : Colors.TextColor, activeOption == i ? Colors.SelectedBackgroundColor : Colors.FillColor);
              iOption.TempValue.ToString().WriteAt(x + 30, y, Colors.TextColor, Colors.FillColor);
              "<<".WriteAt(x + 36, y, iOption.ActionIdx == 0? Colors.SelectedColor : Colors.TextColor, iOption.ActionIdx == 0 ? Colors.SelectedBackgroundColor : Colors.FillColor);
              "<".WriteAt(x + 40, y, iOption.ActionIdx == 1 ? Colors.SelectedColor : Colors.TextColor, iOption.ActionIdx == 1 ? Colors.SelectedBackgroundColor : Colors.FillColor);
              ">".WriteAt(x + 43, y, iOption.ActionIdx == 2 ? Colors.SelectedColor : Colors.TextColor, iOption.ActionIdx == 2 ? Colors.SelectedBackgroundColor : Colors.FillColor);
              ">>".WriteAt(x + 46, y, iOption.ActionIdx == 3 ? Colors.SelectedColor : Colors.TextColor, iOption.ActionIdx == 3 ? Colors.SelectedBackgroundColor : Colors.FillColor);
              y++;
              i++;
              break;
            case GameOption<string> sOption:
              sOption.Name.WriteAt(x + 2, y, activeOption == i ? Colors.SelectedColor : Colors.TextColor, activeOption == i ? Colors.SelectedBackgroundColor : Colors.FillColor);
              y++;
              i++;
              break;
          }
        }
        y ++;
      }

      "Press [S] to Save Changes".WriteAt(x, y, Colors.TextColor, Colors.FillColor); y++;
      "Press [Esc] to Exit Without Waving".WriteAt(x, y, Colors.TextColor, Colors.FillColor);
      KeyHandler();
    }
  }

  private static object? GetOption(int index)
  {
    int i = 0;
    foreach (string header in Options.Keys)
    {
      foreach (object? option in Options[header])
      {
        if (i == index)
          return option;
        i++;
      }
    }
    return null;
  }

  internal static object? GetOption(string name)
  {
    foreach (string header in Options.Keys)
    {
      foreach (object? option in Options[header])
      {
        switch (option)
        {
          case GameOption<bool> bOption:
            if (bOption.Name == name)
              return bOption;
            break;
          case GameOption<int> iOption:
            if (iOption.Name == name)
              return iOption;
            break;
          case GameOption<string> sOption:
            if (sOption.Name == name)
              return sOption;
            break;
        }
      }
    }
    return null;
  }

  private static void SaveOptions()
  {
    foreach (string header in Options.Keys)
    {
      foreach (object? option in Options[header])
      {
        switch (option)
        {
          case GameOption<bool> bOption:
            bOption.Value = bOption.ActionIdx == 1;
            bOption.RaiseEvent();
            break;
          case GameOption<int> iOption:
            iOption.Value = iOption.TempValue;
            iOption.RaiseEvent();
            break;
        }
      }
    }
  }

  private static void BuildOptionList()
  {
    Options.Add("Sound", new List<object?>());

    GameOption<bool> enableSound = new("EnableSound", true);
    enableSound.OnValueChanged += SoundSystem.OnEnableSoundChanged;
    Options["Sound"].Add(enableSound);

    GameOption<int> backgroundVolume = new("BackgroundVolume", 100);
    backgroundVolume.OnValueChanged += SoundSystem.SetBackgroundVolume;
    Options["Sound"].Add(backgroundVolume);

    GameOption<int> effectVolume = new("EffectVolume", 100);
    effectVolume.OnValueChanged += SoundSystem.SetEffectVolume;
    Options["Sound"].Add(effectVolume);

    GameOption<int> playerVolume = new("PlayerVolume", 100);
    playerVolume.OnValueChanged += Player.SetEffectVolume;
    Options["Sound"].Add(playerVolume);

    // We will handle the sound for the monsters in the monster class
    Options["Sound"].Add(new GameOption<int>("MonsterVolume", 100));
    optionCount += Options["Sound"].Count;

    Options.Add("Display", new List<object?>());
    Options["Display"].Add(new GameOption<bool>("Fullscreen", false));
    optionCount += Options["Display"].Count;
  }

  private static void KeyHandler()
  {
    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
    object? option = GetOption(activeOption);
    GameOption<bool>? bOption = option as GameOption<bool>;
    GameOption<int>? iOption = option as GameOption<int>;
    switch (keyInfo.Key)
    {
      case ConsoleKey.Escape:
      case ConsoleKey.Q:
        ConsoleEx.Clear();
        dialogOpen = false;
        Dialog.Close("GamePlay");
        break;
      case ConsoleKey.S:
        SaveOptions();
        ConsoleEx.Clear();
        dialogOpen = false;
        Dialog.Close("GamePlay");
        break;
      case ConsoleKey.UpArrow:
        activeOption--;
        if (activeOption < 0)
          activeOption = optionCount - 1;
        break;
      case ConsoleKey.DownArrow:
        activeOption++;
        if (activeOption >= optionCount)
          activeOption = 0;
        break;
      case ConsoleKey.LeftArrow:
        if (option is GameOption<bool>)
        {
          bOption!.ActionIdx--;
          if (bOption.ActionIdx < 0)
            bOption.ActionIdx = bOption.ActionCount - 1;
        }
        if (option is GameOption<int>)
        {
          iOption!.ActionIdx--;
          if (iOption.ActionIdx < 0)
            iOption.ActionIdx = iOption.ActionCount - 1;
        }
        break;
      case ConsoleKey.RightArrow:
        if (option is GameOption<bool>)
        {
          bOption!.ActionIdx++;
          if (bOption.ActionIdx >= bOption.ActionCount)
            bOption.ActionIdx = 0;
        }
        if (option is GameOption<int>)
        {
          iOption!.ActionIdx++;
          if (iOption.ActionIdx > iOption.ActionCount)
            iOption.ActionIdx = 0;
        }
        break;
      case ConsoleKey.Enter:
        if (option is GameOption<bool>)
          bOption!.Value = bOption.ActionIdx == 1;
        if (option is GameOption<int>)
        {
          if (iOption!.ActionIdx == 0)
            iOption.TempValue = 0;
          if (iOption.ActionIdx == 1)
            iOption.TempValue--;
          if (iOption.ActionIdx == 2)
            iOption.TempValue++;
          if (iOption.ActionIdx == 3)
            iOption.TempValue = 100;
        }
        break;
    }
  }
}