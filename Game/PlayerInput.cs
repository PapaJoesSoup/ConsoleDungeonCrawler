using ConsoleDungeonCrawler.Game.Entities;
using ConsoleDungeonCrawler.Game.Maps;
using ConsoleDungeonCrawler.Game.Screens;

namespace ConsoleDungeonCrawler.Game
{
  internal static class PlayerInput
  {
    internal static void Process()
    {
      // Capture and hide the key the user pressed
      ConsoleKeyInfo keyInfo = Console.ReadKey(true);
      if ((keyInfo.Modifiers & ConsoleModifiers.Shift) != 0)
      {
        switch (keyInfo.Key)
        {
          case ConsoleKey.W:
          case ConsoleKey.A:
          case ConsoleKey.S:
          case ConsoleKey.D:
            GamePlay.Messages.Add(new Message($"You pressed Shift+{keyInfo.Key}"));
            Map.Player.Jump(keyInfo.Key);
            break;
          case ConsoleKey.I:
            GamePlay.Messages.Add(new Message($"You pressed Shift+{keyInfo.Key}"));
            PlayerInventory.Draw();
            break;
          case ConsoleKey.Q:
            GamePlay.Messages.Add(new Message($"You pressed Shift+{keyInfo.Key}"));
            Game.IsOver = true;
            break;
        }
      }
      else
      {
        GamePlay.LastKey = keyInfo;
        switch (keyInfo.Key)
        {
          case ConsoleKey.W:
          case ConsoleKey.A:
          case ConsoleKey.S:
          case ConsoleKey.D:
            GamePlay.Messages.Add(new Message($"You pressed {keyInfo.Key}"));
            Map.Player.Move(keyInfo.Key);
            Actions.PickupOverlayItem();
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
            GamePlay.Messages.Add(new Message($"You pressed <"));
            if (Inventory.Bags.Count > 1)
              GamePlay.currentBag--;
            break;
          case ConsoleKey.OemPeriod:
            GamePlay.Messages.Add(new Message($"You pressed >"));
            if (GamePlay.currentBag < Inventory.Bags.Count)
              GamePlay.currentBag++;
            break;
          case ConsoleKey.O:
            Actions.OpenDoor();
            break;
          case ConsoleKey.C:
            Actions.CloseDoor();
            break;
          case ConsoleKey.T:
            Map.Player.Attack();
            break;
            default:
              GamePlay.Messages.Add(new Message($"You pressed {keyInfo.Key}, which does nothing."));
            break;
        }
      }
    }
  }
}
