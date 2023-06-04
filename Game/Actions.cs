using System.Runtime.Remoting;
using ConsoleDungeonCrawler.Game.Entities;
using ConsoleDungeonCrawler.Game.Maps;
using ConsoleDungeonCrawler.Game.Screens;

namespace ConsoleDungeonCrawler.Game
{
  internal static class Actions
  {
    public static void PickupOverlayItem()
    {
      // TODO: Add logic to pick up items
      // Items can buff the player, or be added to the inventory
      // Items can be gold, potions, weapons, armor, etc.
      if (Map.IsPlayerNextToOverlay(out MapObject obj) == ' ') return;
      switch (obj.Type.Symbol)
      {
        case 'i':
        case 'm':
          GamePlayScreen.Messages.Add($"Picking up {obj.Type.Name}...");
          Player.AddToInventory(obj.Loot);
          Map.UpdateOverlayObject(obj);
          break;
        default:
          break;
      }
    }

    public static void OpenDoor()
    {
      if (!Map.IsPlayerNextToMap('+', out MapObject door)) return;
      ObjectType type = Map.MapTypes.Find(t => t.Symbol == '-') ?? new ObjectType();
      if (type.Symbol == ' ') return;
      GamePlayScreen.Messages.Add($"Opening Door...");
      Map.RemoveFromMapObjects(door);
      door.Type = type;
      Map.AddToMapObjects(door);
      door.Draw();
    }

    public static void CloseDoor()
    {
      if (!Map.IsPlayerNextToMap('-', out MapObject door)) return;
      ObjectType type = Map.MapTypes.Find(t => t.Symbol == '+') ?? new ObjectType();
      if (type.Symbol == ' ') return;
      GamePlayScreen.Messages.Add($"Closing Door...");
      Map.RemoveFromMapObjects(door);
      door.Type = type;
      Map.AddToMapObjects(door);
      door.Draw();
    }

    public static void MovePlayer(ConsoleKey key)
    {
      int x = 0;
      int y = 0;
      if (key == ConsoleKey.W) { x = 0; y = -1; }
      if (key == ConsoleKey.A) { x = -1; y = 0; }
      if (key == ConsoleKey.S) { x = 0; y = 1; }
      if (key == ConsoleKey.D) { x = 1; y = 0; }

      Tuple<int, int> oldPos = new Tuple<int, int>(Player.OnMap.X, Player.OnMap.Y);
      Tuple<int, int> newPos = new Tuple<int, int>(Player.OnMap.X + x, Player.OnMap.Y + y);

      if (!Map.CanMoveTo(newPos.Item1, newPos.Item2)) return;
      Map.OverlayObjects['P'][0].X = newPos.Item1;
      Map.OverlayObjects['P'][0].Y = newPos.Item2;
      Map.MapGrid[oldPos.Item1][oldPos.Item2].Draw();
      Map.MapGrid[newPos.Item1][newPos.Item2].Draw();
      Map.OverlayGrid[oldPos.Item1][oldPos.Item2].Draw();
      Map.OverlayGrid[newPos.Item1][newPos.Item2].Draw();
    }
  }
}
