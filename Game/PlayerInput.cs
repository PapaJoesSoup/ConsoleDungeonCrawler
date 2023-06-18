using System.Runtime.InteropServices.ObjectiveC;
using ConsoleDungeonCrawler.Game.Entities;
using ConsoleDungeonCrawler.Game.Screens;

namespace ConsoleDungeonCrawler.Game
{
    internal static class PlayerInput
  {
    internal static void Process()
    {
      // Capture and hide the key the user pressed
      ConsoleKeyInfo keyInfo = Console.ReadKey(true);
      GamePlay.LastKey = keyInfo;
      if ((keyInfo.Modifiers & ConsoleModifiers.Shift) != 0)
      {
        switch (keyInfo.Key)
        {
          case ConsoleKey.W:
          case ConsoleKey.A:
          case ConsoleKey.S:
          case ConsoleKey.D:
            GamePlay.Messages.Add(new Message($"You jumped {Map.GetDirection(keyInfo.Key)}..."));
            Map.Player.Jump(keyInfo.Key);
            break;
          case ConsoleKey.I:
            PlayerInventory.Draw();
            break;
          case ConsoleKey.P:
            PlayerSpells.Draw();
            break;
          case ConsoleKey.Q:
            Game.IsOver = true;
            break;
        }
      }
      else
      {
        switch (keyInfo.Key)
        {
          case ConsoleKey.W:
          case ConsoleKey.A:
          case ConsoleKey.S:
          case ConsoleKey.D:

            if (!Map.Player.Move(keyInfo.Key)) break;
            GamePlay.Messages.Add(new Message($"You moved {Map.GetDirection(keyInfo.Key)}..."));
            Actions.PickupOverlayItem();
            break;
          case ConsoleKey.D0:
          case ConsoleKey.D1:
          case ConsoleKey.D2:
          case ConsoleKey.D3:
          case ConsoleKey.D4:
          case ConsoleKey.D5:
          case ConsoleKey.D6:
          case ConsoleKey.D7:
          case ConsoleKey.D8:
          case ConsoleKey.D9:
            Actions.UseSpell(keyInfo);
            break;
          case ConsoleKey.Escape:
            Game.IsPaused = true;
            break;
          case ConsoleKey.PageUp:
            GamePlay.MessageOffset -= 8;
            break;
          case ConsoleKey.PageDown:
            GamePlay.MessageOffset += 8;
            break;
          case ConsoleKey.UpArrow:
            GamePlay.MessageOffset--;
            break;
          case ConsoleKey.DownArrow:
            GamePlay.MessageOffset++;
            break;
          case ConsoleKey.Home:
            GamePlay.MessageOffset = -GamePlay.Messages.Count;
            break;
          case ConsoleKey.End:
            GamePlay.MessageOffset = 0;
            break;
          case ConsoleKey.OemComma:
            if (Inventory.Bags.Count > 1)
              GamePlay.currentBag--;
            break;
          case ConsoleKey.OemPeriod:
            if (GamePlay.currentBag < Inventory.Bags.Count)
              GamePlay.currentBag++;
            break;
          case ConsoleKey.O:
            Actions.OpenDoor();
            Map.SetVisibleArea(10);
            Map.WhatIsVisible();
            break;
          case ConsoleKey.C:
            Actions.CloseDoor();
            break;
          case ConsoleKey.F7:
            Actions.UpStairs();
            break;
          case ConsoleKey.F8:
            Actions.DownStairs();
            break;
          case ConsoleKey.T:
            Map.Player.Attack();
            break;
          case ConsoleKey.F5:
            Map.ShowFullMap();
            break;
          case ConsoleKey.F6:
            Map.ShowFullOverlay();
            break;
          default:
            break;
        }
      }
    }
  }
}
