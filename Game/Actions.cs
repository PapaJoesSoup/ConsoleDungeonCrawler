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
      if (Map.Player.IsNextToOverlay(out MapObject obj) == ' ') return;
      switch (obj.Type.Symbol)
      {
        case 'i':
        case 'm':
        case 'g':
          GamePlayScreen.Messages.Add(new Message($"You Picked up {obj.Type.Singular}!", Color.DarkGoldenrod, Color.Black));
          Map.Player.AddToInventory(obj.Loot);
          Map.UpdateOverlayObject(obj);
          break;
        default:
          break;
      }
    }

    public static void OpenDoor()
    {
      if (!Map.Player.IsNextToMap('+', out MapObject door)) return;
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
      if (!Map.Player.IsNextToMap('-', out MapObject door)) return;
      ObjectType type = Map.MapTypes.Find(t => t.Symbol == '+') ?? new ObjectType();
      if (type.Symbol == ' ') return;
      GamePlayScreen.Messages.Add(new Message("Closing Door...", Color.Yellow,
        Color.Black));
      Map.RemoveFromMapObjects(door);
      door.Type = type;
      Map.AddToMapObjects(door);
      door.Draw();
    }

    public static void MonsterActions()
    {
      foreach (char symbol in Map.OverlayObjects.Keys)
      {
        foreach (MapObject obj in Map.OverlayObjects[symbol])
        {
          if (obj is not Monster monster) continue;
          if (!monster.IsVisible || !monster.IsAlive) continue;
          monster.DetectPlayer();
          if (!monster.InCombat) continue;
          Player.InCombat = true;
          monster.Attack();
        }
      }
    }
  }
}
