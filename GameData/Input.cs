

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
        case ConsoleKey.W:
        case ConsoleKey.A:
        case ConsoleKey.S:
        case ConsoleKey.D:
          Game.Messages.Add($"You pressed {keyInfo.Key}");
          MovePlayer(keyInfo.Key);
          break;
        case ConsoleKey.Escape:
          Game.IsGameOver = true;
          break;
        case ConsoleKey.PageUp:
          Game.Messages.Add($"You pressed {keyInfo.Key}");
          break;
        case ConsoleKey.PageDown:
          Game.Messages.Add($"You pressed {keyInfo.Key}");
          break;
        case ConsoleKey.O:
          OpenDoor();
          break;
        case ConsoleKey.C:
          CloseDoor();
          break;
        case ConsoleKey.P:
          PickupItem();
          break;
      }
    }

    private static void PickupOverlayItem()
    {
      // TODO: Add logic to pick up items
      // Items can buff the player, or be added to the inventory
      // Items can be gold, potions, weapons, armor, etc.
      if (Map.IsPlayerNextToOverlay(out MapObject obj) == ' ') return;
      Game.Messages.Add($"Picking up {obj.Type.Name}...");
      switch (obj.Type.Symbol)
      {
        case 'i':
        case 'm':
          Game.Messages.Add($"Picking up {obj.Type.Name}...");
          Player.AddToInventory(obj.Loot);
          UpdateOverlayObject(obj);
          break;
        default:
          break;
      }
    }

    private static void PickupItem()
    {
      if (!Map.IsPlayerNextToOverlay('i', out MapObject obj)) return;
      Game.Messages.Add($"Picking up {obj.Type.Name}...");
      Player.AddToInventory(obj.Loot);
      UpdateOverlayObject(obj);
    }

    private static void OpenDoor()
    {
      if (!Map.IsPlayerNextToMap('+', out MapObject door)) return;
      ObjectType? type = Map.MapTypes.Find(t => t.Symbol == '-');
      if (type == null) return;
      Game.Messages.Add($"Opening Door...");
      Map.MapGrid[door.X][door.Y].Type = type;
      door.Type = type;
    }

    private static void CloseDoor()
    {
      if (!Map.IsPlayerNextToMap('-', out MapObject door)) return;
      ObjectType? type = Map.MapTypes.Find(t => t.Symbol == '+');
      if (type == null) return;
      Game.Messages.Add($"Closing Door...");
      Map.MapGrid[door.X][door.Y].Type = type;
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

      if (!Map.CanMoveTo(Map.Player.X + x, Map.Player.Y + y)) return;
        Map.OverlayObjects['P'][0].X += x;
        Map.OverlayObjects['P'][0].Y += y;
    }

    private static void UpdateOverlayObject(MapObject obj)
    {
      int idx = Map.OverlayObjects[obj.Type.Symbol].IndexOf(obj);
      MapObject newObj = new MapObject(obj.X, obj.Y, new ObjectType(), false);
      Map.OverlayGrid[obj.X][obj.Y] = newObj;
      Map.OverlayObjects[obj.Type.Symbol][idx] = newObj;
      Game.ClearLegendSection();
    }
  }
}
