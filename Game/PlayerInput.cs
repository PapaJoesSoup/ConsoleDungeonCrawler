using ConsoleDungeonCrawler.Game.Screens;

namespace ConsoleDungeonCrawler.Game
{
  internal static class PlayerInput
  {
    internal static void Process()
    {
      // Capture and hide the key the user pressed
      ConsoleKeyInfo keyInfo = Console.ReadKey(true);
      switch (keyInfo.Key)
      {
        case ConsoleKey.W:
        case ConsoleKey.A:
        case ConsoleKey.S:
        case ConsoleKey.D:
          GamePlayScreen.Messages.Add($"You pressed {keyInfo.Key}");
          Actions.MovePlayer(keyInfo.Key);
          break;
        case ConsoleKey.Escape:
          Game.IsGameOver = true;
          break;
        case ConsoleKey.PageUp:
          GamePlayScreen.Messages.Add($"You pressed {keyInfo.Key}");
          break;
        case ConsoleKey.PageDown:
          GamePlayScreen.Messages.Add($"You pressed {keyInfo.Key}");
          break;
        case ConsoleKey.O:
          Actions.OpenDoor();
          break;
        case ConsoleKey.C:
          Actions.CloseDoor();
          break;
        case ConsoleKey.P:
          Actions.PickupOverlayItem();
          break;
      }
    }
  }
}
