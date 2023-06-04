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
      if ((keyInfo.Modifiers & ConsoleModifiers.Shift) != 0)
      {
        switch (keyInfo.Key)
        {
          case ConsoleKey.W:
          case ConsoleKey.A:
          case ConsoleKey.S:
          case ConsoleKey.D:
            GamePlayScreen.Messages.Add(new Message($"You pressed Shift+{keyInfo.Key}"));
            Actions.JumpPlayer(keyInfo.Key);
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
            GamePlayScreen.Messages.Add(new Message($"You pressed {keyInfo.Key}"));
            Actions.MovePlayer(keyInfo.Key);
            break;
          case ConsoleKey.End:
            Game.IsOver = true;
            break;
          case ConsoleKey.Escape:
            Game.IsPaused = true;
            break;
          case ConsoleKey.PageUp:
            GamePlayScreen.Messages.Add(new Message($"You pressed {keyInfo.Key}"));
            break;
          case ConsoleKey.PageDown:
            GamePlayScreen.Messages.Add(new Message($"You pressed {keyInfo.Key}"));
            break;
          case ConsoleKey.O:
            Actions.OpenDoor();
            break;
          case ConsoleKey.C:
            Actions.CloseDoor();
            break;
          case ConsoleKey.Enter:
            Actions.PickupOverlayItem();
            break;
            default:
              GamePlayScreen.Messages.Add(new Message($"You pressed {keyInfo.Key}, which does nothing."));
            break;
        }
      }
    }
  }
}
