

namespace ConsoleDungeonCrawler.GameData
{
  internal static class Input
  {

    internal static void ProcessPlayerInput()
    {
      // Capture and hide the key the user pressed
      ConsoleKeyInfo keyInfo = Console.ReadKey(true);
      switch (keyInfo.Key)
      {
        case ConsoleKey.Escape:
          Game.IsGameOver = true;
          break;
        case ConsoleKey.W:
          Game.Messages.Add($"You pressed W");
          if (Map.CanMoveTo(Map.Player.x, Map.Player.y - 1))
            Map.OverlayObjects['P'][0].y--;
          break;
        case ConsoleKey.A:
          Game.Messages.Add($"You pressed A");
          if (Map.CanMoveTo(Map.Player.x - 1, Map.Player.y))
            Map.OverlayObjects['P'][0].x--;
          break;
        case ConsoleKey.S:
          Game.Messages.Add($"You pressed S");
          if (Map.CanMoveTo(Map.Player.x, Map.Player.y + 1))
            Map.OverlayObjects['P'][0].y++;
          break;
        case ConsoleKey.D:
          Game.Messages.Add($"You pressed D");
          if (Map.CanMoveTo(Map.Player.x + 1, Map.Player.y))
            Map.OverlayObjects['P'][0].x++;
          break;
        case ConsoleKey.PageUp:
          Game.Messages.Add($"You pressed Page Up");
          break;
        case ConsoleKey.PageDown:
          Game.Messages.Add($"You pressed Page Down");
          break;
        case ConsoleKey.O:
          Game.Messages.Add($"You pressed O");
          if (Map.IsPlayerNextTo('+', out MapObject doorC))
          {
            ObjectType? type = Map.MapTypes.Find(t => t.Symbol == '-');
            if (type != null)
            {
              Map.MapGrid[doorC.x][doorC.y].Type = type;
              doorC.Type = type;
            }
          }
          break;
        case ConsoleKey.C:
          Game.Messages.Add($"You pressed C");
          if (Map.IsPlayerNextTo('-', out MapObject doorO))
          {
            ObjectType? type = Map.MapTypes.Find(t => t.Symbol == '+');
            Map.MapGrid[doorO.x][doorO.y].Type = type;
            if (type != null) doorO.Type = type;
          }
          break;
      }
    }


  }
}
