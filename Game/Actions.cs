using System.Drawing;
using ConsoleDungeonCrawler.Game.Entities;
using ConsoleDungeonCrawler.Game.Entities.Items;
using ConsoleDungeonCrawler.Game.Screens;
using ConsoleDungeonCrawler.Game.Screens.Dialogs;

namespace ConsoleDungeonCrawler.Game;

internal static class Actions
{
  /// <summary>
  /// Pick up any items that are on the ground under the player.  Dead monsters are lootable till looted.
  /// </summary>
  internal static void PickupOverlayItem()
  {
    // search Tile location from the top down for items to pick up
    for (int i = Map.LevelOverlayGrids[Game.CurrentLevel][Map.Player.X][Map.Player.Y].Count - 1; i >= 0; i--)
    {
      if (Map.LevelOverlayGrids[Game.CurrentLevel][Map.Player.X][Map.Player.Y][i].Type.Symbol == ' ') continue;
      Tile obj = Map.LevelOverlayGrids[Game.CurrentLevel][Map.Player.X][Map.Player.Y][i];
      Item item = new();
      switch (obj.Type.Symbol)
      {
        case '\u25b2':
          SoundSystem.PlayEffect(SoundSystem.MSounds[Sound.Stairs]);
          Thread.Sleep((int)SoundSystem.MSounds[Sound.Stairs].Duration-200);
          UpStairs();
          break;
        case '\u25bc':
          SoundSystem.PlayEffect(SoundSystem.MSounds[Sound.Stairs]);
          Thread.Sleep((int)SoundSystem.MSounds[Sound.Stairs].Duration - 200);
          DownStairs();
          break;
        case 'V':
          SoundSystem.PlayEffect(SoundSystem.MSounds[Sound.Vendor]);
          Vendor.Draw();
          break;
        case 'E':
          Game.IsWon = true;
          SoundSystem.PlayEffect(SoundSystem.MSounds[Sound.GameWon]);
          GameWon.Draw();
          break;
        case 'O':
        case 'k':
        case 'z':
        case 'g':
        case 'B':
          if (obj.IsLootable)
          {
            GamePlay.Messages.Add(new Message($"You loot {((Monster)obj).Type.Name}...", Color.BurlyWood, Color.Black));
            Player.Gold += ((Monster)obj).Gold;
            GamePlay.Messages.Add(new Message($"You gained {((Monster)obj).Gold} gold!", Color.LimeGreen, Color.Black));
            item = Monster.Loot((Monster)obj);
            obj.IsLootable = false;
          }
          break;
        case 'i':
          item = Inventory.GetRandomItem();
          break;
        case 'm':
          item = Chest.GetRandomItem();
          break;
        case '$':
          item = Gold.GetRandomItem();
          break;
      }

      if (item.Type == ItemType.None) continue;
        SoundSystem.PlayEffect(SoundSystem.MSounds[Sound.Pickup]);
      Inventory.AddItem(item);
      string message = item.Type == ItemType.Gold
        ? $"You Picked up a pouch containing {((Gold)item).GetValue()} gold!"
        : "You Picked up";
      GamePlay.Messages.Add(new Message(message, item.Type != ItemType.Gold? item : null, Color.LimeGreen, Color.Black));
      Map.UpdateOverlayTile(obj);
    }
  }

  internal static void OpenCloseDoor()
  {
    if (Map.Player.IsNextToMapGrid('+', out Tile doorC))
      OpenDoor(doorC);
    if (Map.Player.IsNextToMapGrid('-', out Tile doorO))
      CloseDoor(doorO);
  }

  internal static void MonsterActions()
  {
    for (int charIdx = 0; charIdx < Map.LevelOverlayTiles[Game.CurrentLevel].Count; charIdx++)
    {
      char symbol = Map.LevelOverlayTiles[Game.CurrentLevel].Keys.ElementAt(charIdx);
      for (int index = 0; index < Map.LevelOverlayTiles[Game.CurrentLevel][symbol].Count; index++)
      {
        Tile obj = Map.LevelOverlayTiles[Game.CurrentLevel][symbol][index];
        if (obj is not Monster monster) continue;
        if (!monster.IsVisible || !monster.IsAlive) continue;
        // remember monster state before detecting player so we can delay attack one turn.
        bool inCombat = monster.InCombat;
        monster.DetectPlayer();
        if (!monster.InCombat) continue;
        Player.InCombat = true;
        // if we just detected player, don't attack yet.
        if (inCombat == monster.InCombat) monster.Attack();
      }
    }
  }

  internal static void UseSpell(ConsoleKeyInfo keyInfo)
  {
    int index = (int)char.GetNumericValue(keyInfo.KeyChar);
    if (index > Player.Spells.Count) return;
    if (!Player.Spells.ContainsKey(index)) return;
    Spell spell = Player.Spells[index];
    if (spell.ManaCost > Player.Mana)
    {
      GamePlay.Messages.Add(new Message($"Not enough mana to cast {spell.Name}!", Color.Red, Color.Black));
      return;
    }
    Player.Mana -= spell.ManaCost;
    spell.Cast();
  }

  private static void UpStairs()
  {
    if (Game.CurrentLevel == 2)
    {
      GamePlay.Messages.Add(new Message("You can't go up any further!", Color.Red, Color.Black));
      return;
    }
    GamePlay.Messages.Add(new Message("You have gone up stairs to the floor! above", Color.DarkOrange, Color.Black));
    Game.CurrentLevel++;
    Map.LoadLevel();
  }

  private static void DownStairs()
  {
    if (Game.CurrentLevel == -2)
    {
      GamePlay.Messages.Add(new Message("You can't go down any further!", Color.Red, Color.Black));
      return;
    }
    GamePlay.Messages.Add(new Message("You have gone down stairs to the floor below!", Color.DarkOrange, Color.Black));
    Game.CurrentLevel--;
    Map.LoadLevel();
  }
  
  private static void OpenDoor(Tile door)
  {
    TileType type = Map.MapTypes.Find(t => t.Symbol == '-') ?? new TileType();
    if (type.Symbol == ' ') return;
    SoundSystem.PlayEffect(SoundSystem.MSounds[Sound.Door]);
    GamePlay.Messages.Add(new Message("Opening Door...", Color.Yellow, Color.Black));
    Map.RemoveFromMapTiles(door);
    door.Type = type;
    door.IsPassable = type.IsPassable;
    Map.AddToMapTiles(door);
    door.Draw();
  }

  private static void CloseDoor(Tile door)
  {
    TileType type = Map.MapTypes.Find(t => t.Symbol == '+') ?? new TileType();
    if (type.Symbol == ' ') return;
    SoundSystem.PlayEffect(SoundSystem.MSounds[Sound.Door]);
    GamePlay.Messages.Add(new Message("Closing Door...", Color.Yellow, Color.Black));
    Map.RemoveFromMapTiles(door);
    door.Type = type;
    door.IsPassable = type.IsPassable;
    Map.AddToMapTiles(door);
    door.Draw();
  }

}