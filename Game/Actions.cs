using System.Drawing;
using ConsoleDungeonCrawler.Game.Entities;
using ConsoleDungeonCrawler.Game.Entities.Items;
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
      Item item = new Item();
      switch (obj.Type.Symbol)
      {
        case 'i':
          item = Inventory.GetRandomItem();
          break;
        case 'm':
          item = Chest.GetRandomChest();
          break;
        case '$':
          item = Inventory.GetRandomItem(ItemType.Gold);
          break;
      }
      Inventory.AddItem(item);
      string message = item.Type == ItemType.Gold ? "You Picked up a pouch of gold!" : $"You Picked up {item.Description}!";
      GamePlay.Messages.Add(new Message(message, Color.DarkGoldenrod, Color.Black));
      Map.UpdateOverlayObject(obj);

    }

    public static void OpenDoor()
    {
      if (!Map.Player.IsNextToMap('+', out MapObject door)) return;
      ObjectType type = Map.MapTypes.Find(t => t.Symbol == '-') ?? new ObjectType();
      if (type.Symbol == ' ') return;
      GamePlay.Messages.Add(new Message("Opening Door...", Color.Yellow, Color.Black));
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
      GamePlay.Messages.Add(new Message("Closing Door...", Color.Yellow,
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
