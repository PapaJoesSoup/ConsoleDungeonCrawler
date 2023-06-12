﻿using System.Drawing;
using ConsoleDungeonCrawler.Game.Entities;
using ConsoleDungeonCrawler.Game.Entities.Items;
using ConsoleDungeonCrawler.Game.Maps;
using ConsoleDungeonCrawler.Game.Screens;

namespace ConsoleDungeonCrawler.Game
{
  internal static class Actions
  {
    /// <summary>
    /// Pick up any items that are on the ground under the player.  Dead monsters are lootable till looted.
    /// </summary>
    public static void PickupOverlayItem()
    {
      if (Map.OverlayGrid[Map.Player.X][Map.Player.Y].Type.Symbol == ' ') return;
      MapObject obj = Map.OverlayGrid[Map.Player.X][Map.Player.Y];
      Item item = new Item();
      switch (obj.Type.Symbol)
      {
        case 'O':
        case 'k':
        case 'z':
        case 'g':
        case 'B':
          if (obj.IsLootable)
          {
            Player.Gold += ((Monster)obj).Gold;
            GamePlay.Messages.Add(new Message($"You gained {((Monster)obj).Gold} gold!", Color.DarkOrange, Color.Black));
            item = Monster.Loot((Monster)obj);
            obj.IsLootable = false;
          }
          break;
        case 'i':
          item = Inventory.GetRandomItem();
          break;
        case 'm':
          item = Chest.GetRandomChest();
          break;
        case '$':
          item = Inventory.GetRandomItem(ItemType.Gold);
          break;
        default:
          return;
      }
      if (item.Type == ItemType.None) return;
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
      door.IsPassable = type.IsPassable;
      Map.AddToMapObjects(door);
      door.Draw();
    }

    public static void CloseDoor()
    {
      if (!Map.Player.IsNextToMap('-', out MapObject door)) return;
      ObjectType type = Map.MapTypes.Find(t => t.Symbol == '+') ?? new ObjectType();
      if (type.Symbol == ' ') return;
      GamePlay.Messages.Add(new Message("Closing Door...", Color.Yellow, Color.Black));
      Map.RemoveFromMapObjects(door);
      door.Type = type;
      door.IsPassable = type.IsPassable;
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

    public static void UseSpell(ConsoleKeyInfo keyInfo)
    {
      int index = (int)char.GetNumericValue(keyInfo.KeyChar);
      if (index > Player.Spells.Count) return;
      Spell spell = Player.Spells[index];
      if (spell == null) return;
      if (spell.ManaCost > Player.Mana)
      {
        GamePlay.Messages.Add(new Message("Not enough mana!", Color.Red, Color.Black));
        return;
      }
      Player.Mana -= spell.ManaCost;
      spell.Cast();
    }
  }
}
