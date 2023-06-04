using System.Drawing;
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
        case 'g':
          GamePlayScreen.Messages.Add(new Message($"You Picked up {obj.Type.Singular}!", Color.DarkGoldenrod, Color.Black));
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
      GamePlayScreen.Messages.Add(new Message("Opening Door...", Color.Yellow, Color.Black));
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
      GamePlayScreen.Messages.Add(new Message("Closing Door...", Color.Yellow,
        Color.Black));
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

      Position oldPos = new Position(Player.MapObj.X, Player.MapObj.Y);
      Position newPos = new Position(Player.MapObj.X + x, Player.MapObj.Y + y);

      if (!Map.CanMoveTo(newPos.X, newPos.Y)) return;
      Map.OverlayObjects['P'][0].X = newPos.X;
      Map.OverlayObjects['P'][0].Y = newPos.Y;
      Map.MapGrid[oldPos.X][oldPos.Y].Draw();
      Map.MapGrid[newPos.X][newPos.Y].Draw();
      Map.OverlayGrid[oldPos.X][oldPos.Y].Draw();
      Map.OverlayGrid[newPos.X][newPos.Y].Draw();
    }

    public static void JumpPlayer(ConsoleKey key)
    {
      int x = 0;
      int y = 0;
      if (key == ConsoleKey.W) { x = 0; y = -2; }
      if (key == ConsoleKey.A) { x = -2; y = 0; }
      if (key == ConsoleKey.S) { x = 0; y = 2; }
      if (key == ConsoleKey.D) { x = 2; y = 0; }

      Position oldPos = new Position(Player.MapObj.X, Player.MapObj.Y);
      Position newPos = new Position(Player.MapObj.X + x, Player.MapObj.Y + y);

      if (!Map.CanJumpTo(oldPos.X, oldPos.Y, newPos.X, newPos.Y)) return;
      Map.OverlayObjects['P'][0].X = newPos.X;
      Map.OverlayObjects['P'][0].Y = newPos.Y;
      Map.MapGrid[oldPos.X][oldPos.Y].Draw();
      Map.MapGrid[newPos.X][newPos.Y].Draw();
      Map.OverlayGrid[oldPos.X][oldPos.Y].Draw();
      Map.OverlayGrid[newPos.X][newPos.Y].Draw();
    }
  }
}
