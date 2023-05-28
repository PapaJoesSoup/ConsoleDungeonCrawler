

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
        case ConsoleKey.A:
        case ConsoleKey.S:
        case ConsoleKey.D:
          Game.Messages.Add($"You pressed {keyInfo.Key.ToString()}");
          MovePlayer(keyInfo.Key);
          break;
        case ConsoleKey.PageUp:
          Game.Messages.Add($"You pressed Page Up");
          break;
        case ConsoleKey.PageDown:
          Game.Messages.Add($"You pressed Page Down");
          break;
        case ConsoleKey.O:
          if (Map.IsPlayerNextToMap('+', out MapObject doorC)) OpenDoor(doorC);
          break;
        case ConsoleKey.C:
          if (Map.IsPlayerNextToMap('-', out MapObject doorO)) CloseDoor(doorO);
          break;
        case ConsoleKey.Enter:
          Game.Messages.Add($"You pressed Enter");
          PickUpOverlayItem();
          break;
      }
    }

    private static void PickUpOverlayItem()
    {
      // TODO: Add logic to pick up items
      if (Map.IsPlayerNextToOverlay(out MapObject item) == ' ') return;
      Game.Messages.Add($"Picking up {item.Type.Name}...");
      switch (item.Type.Symbol)
      {
        case 'i':
          Player.Inventory.Add(0, item.Loot);
          break;
        case 'm':
          Player.Gold += item.Loot.Value;
          break;
        default:
          break;
      }
      Map.OverlayGrid[item.x][item.y].Type = new ObjectType();
    }

    private static void OpenDoor(MapObject door)
    {
      ObjectType? type = Map.MapTypes.Find(t => t.Symbol == '-');
      if (type == null) return;
      Game.Messages.Add($"Opening Door...");
      Map.MapGrid[door.x][door.y].Type = type;
      door.Type = type;
    }

    private static void CloseDoor(MapObject door)
    {
      ObjectType? type = Map.MapTypes.Find(t => t.Symbol == '+');
      if (type == null) return;
      Game.Messages.Add($"Closing Door...");
      Map.MapGrid[door.x][door.y].Type = type;
      door.Type = type;
    }

    private static void MovePlayer(ConsoleKey key)
    {
      int x = 0;
      int y = 0;
      if (key == ConsoleKey.W) { x = 0; y = -1; }
      if (key == ConsoleKey.A) { x = -1; y = 0; }
      if (key == ConsoleKey.S) { x = 0; y = 1; }
      if (key == ConsoleKey.D) { x = 1; y = 0; }

      if (!Map.CanMoveTo(Map.Player.x + x, Map.Player.y + y)) return;
        Map.OverlayObjects['P'][0].x += x;
        Map.OverlayObjects['P'][0].y += y;
    }

    private static void PickupItem()
    {
      if (Map.IsPlayerNextToMap('i', out MapObject item))
      {
        Game.Messages.Add($"Picking up {item.Type.Name}...");
        Map.OverlayGrid[item.x][item.y].Type = new ObjectType();
      }
    }
  }
}
