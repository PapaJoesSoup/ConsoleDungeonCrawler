using ConsoleDungeonCrawler.Game.Maps;
using ConsoleDungeonCrawler.Game.Screens;

namespace ConsoleDungeonCrawler.Game.Entities
{
  internal class Player : MapObject
  {
    internal static int Level = 1;
    internal static int Experience = 0;
    internal static int ExperienceToNextLevel = 100;
    internal static PlayerClass Class = PlayerClass.Rogue;
    internal static int Health = 100;
    internal static int MaxHealth = 100;
    internal static int Mana = 0;
    internal static int MaxMana = 0;
    internal static decimal Gold = 0;
    internal static List<Armor> ArmorSet = new List<Armor>();
    internal static Weapon Weapon = new Weapon();
    internal static Dictionary<int, Spell> Spells = new Dictionary<int, Spell>();
    internal static Dictionary<int, Item> Inventory = new Dictionary<int, Item>();

    internal Player(MapObject mapObject)
    {
      // Set Player's MapObject base to the one passed in
      X = mapObject.X;
      Y = mapObject.Y;
      Type = mapObject.Type;
      IsVisible = mapObject.IsVisible;

      // Add 5 empty slots to armor set
      ArmorSet = new List<Armor>();
      ArmorSet.Add(new Armor(ArmorType.Head));
      ArmorSet.Add(new Armor(ArmorType.Body));
      ArmorSet.Add(new Armor(ArmorType.Hands));
      ArmorSet.Add(new Armor(ArmorType.Legs));
      ArmorSet.Add(new Armor(ArmorType.Feet));

      // Add 5 empty slots to inventory
      Inventory = new Dictionary<int, Item>();
      for (int i = 1; i < 6; i++)
      {
        Inventory.Add(i, new Item() { name = ItemName.None, rarity = ItemRarity.Common, id = 0 });
      }

      // Add 5 empty slots to spells
      Spells = new Dictionary<int, Spell>();
      for (int i = 1; i < 6; i++)
      {
        Spells.Add(i, new Spell() { Name = SpellName.None, DamageType = DamageType.Magical, DamageAmount = DamageEffectAmount.None });
      }
    }

    public void Move(ConsoleKey key)
    {
      int x = 0;
      int y = 0;
      if (key == ConsoleKey.W) { x = 0; y = -1; }
      if (key == ConsoleKey.A) { x = -1; y = 0; }
      if (key == ConsoleKey.S) { x = 0; y = 1; }
      if (key == ConsoleKey.D) { x = 1; y = 0; }

      Position oldPos = new Position(X, Y);
      Position newPos = new Position(X + x, Y + y);

      if (!Map.CanMoveTo(newPos.X, newPos.Y)) return;
      X = newPos.X;
      Y = newPos.Y;
      Map.OverlayObjects['P'][0] = this;
      Map.MapGrid[oldPos.X][oldPos.Y].Draw();
      Map.MapGrid[newPos.X][newPos.Y].Draw();
      Map.OverlayGrid[oldPos.X][oldPos.Y].Draw();
      Map.OverlayGrid[newPos.X][newPos.Y].Draw();
    }

    public void Jump(ConsoleKey key)
    {
      int x = 0;
      int y = 0;
      if (key == ConsoleKey.W) { x = 0; y = -2; }
      if (key == ConsoleKey.A) { x = -2; y = 0; }
      if (key == ConsoleKey.S) { x = 0; y = 2; }
      if (key == ConsoleKey.D) { x = 2; y = 0; }

      Position oldPos = new Position(X, Y);
      Position newPos = new Position(X + x, Y + y);

      if (!Map.CanJumpTo(oldPos.X, oldPos.Y, newPos.X, newPos.Y)) return;
      X = newPos.X;
      Y = newPos.Y;
      Map.OverlayObjects['P'][0] = this;
      Map.MapGrid[oldPos.X][oldPos.Y].Draw();
      Map.MapGrid[newPos.X][newPos.Y].Draw();
      Map.OverlayGrid[oldPos.X][oldPos.Y].Draw();
      Map.OverlayGrid[newPos.X][newPos.Y].Draw();
    }


    internal void EquipArmor(Armor armor)
    {

    }

    internal void EquipWeapon(Weapon weapon)
    {

    }

    internal void EquipSpell(Spell spell)
    {

    }

    internal void UseItem(Item item)
    {

    }

    internal void UseSpell(Spell spell)
    {

    }

    internal void TakeDamage(int damage)
    {
      Health -= damage;
      if (Health <= 0) Game.IsOver = true;
    }

    internal void Heal(int amount)
    {
      Health += amount;
      if (Health > MaxHealth) Health = MaxHealth;
    }

    internal void RestoreMana(int amount)
    {
      Mana += amount;
      if (Mana > MaxMana) Mana = MaxMana;
    }

    internal void AddGold(decimal amount)
    {
      Gold += amount;
    }

    internal void RemoveGold(decimal amount)
    {
      Gold -= amount;
    }

    internal void LevelUp()
    {
      Level++;
    }

    internal void AddExperience(int amount)
    {

    }

    internal void AddToInventory(Item item)
    {
      // Check to see if item is in inventory and add to stack, otherwise add to first empty slot
      if (Inventory.ContainsValue(item))
      {
        foreach (KeyValuePair<int, Item> slot in Inventory)
        {
          if (slot.Value.id == item.id)
          {
            slot.Value.Quantity++;
            break;
          }
        }
      }
      else
      {
        foreach (KeyValuePair<int, Item> slot in Inventory)
        {
          if (slot.Value.id == 0)
          {
            //slot.Value = item;
            break;
          }
        }
      }
    }

    internal char IsNextToOverlay(out MapObject obj)
    {
      // look left
      if (X > 0 && Map.OverlayGrid[X - 1][Y].Type.Symbol != ' ')
      {
        obj = Map.OverlayGrid[X - 1][Y];
        return obj.Type.Symbol;
      }
      // look right
      if (X < GamePlayScreen.MapBox.Width && Map.OverlayGrid[X + 1][Y].Type.Symbol != ' ')
      {
        obj = Map.OverlayGrid[X + 1][Y];
        return obj.Type.Symbol;
      }
      // look up
      if (Y > 0 && Map.OverlayGrid[X][Y - 1].Type.Symbol != ' ')
      {
        obj = Map.OverlayGrid[X][Y - 1];
        return obj.Type.Symbol;
      }
      // look down
      if (Y >= GamePlayScreen.MapBox.Height || Map.OverlayGrid[X][Y + 1].Type.Symbol != ' ')
      {
        obj = Map.OverlayGrid[X][Y + 1];
        return obj.Type.Symbol;
      }
      // not found
      obj = new MapObject();
      return ' ';
    }

    internal bool IsNextToOverlay(char symbol, out MapObject obj)
    {
      // look left
      if (X > 0 && Map.OverlayGrid[X - 1][Y].Type.Symbol == symbol)
      {
        obj = Map.OverlayGrid[X - 1][Y];
        return true;
      }
      // look right
      if (X < GamePlayScreen.MapBox.Width && Map.OverlayGrid[X + 1][Y].Type.Symbol == symbol)
      {
        obj = Map.OverlayGrid[X + 1][Y];
        return true;
      }
      // look up
      if (Y > 0 && Map.OverlayGrid[X][Y - 1].Type.Symbol == symbol)
      {
        obj = Map.OverlayGrid[X][Y - 1];
        return true;
      }
      // look down
      if (Y >= GamePlayScreen.MapBox.Height || Map.OverlayGrid[X][Y + 1].Type.Symbol == symbol)
      {
        obj = Map.OverlayGrid[X][Y + 1];
        return true;
      }
      // not found
      obj = new MapObject();
      return false;
    }

    internal bool IsNextToMap(char symbol, out MapObject obj)
    {
      // look left
      if (X > 0 && Map.MapGrid[X - 1][Y].Type.Symbol == symbol)
      {
        obj = Map.MapGrid[X - 1][Y];
        return true;
      }
      // look right
      if (X < GamePlayScreen.MapBox.Width && Map.MapGrid[X + 1][Y].Type.Symbol == symbol)
      {
        obj = Map.MapGrid[X + 1][Y];
        return true;
      }
      // look up
      if (Y > 0 && Map.MapGrid[X][Y - 1].Type.Symbol == symbol)
      {
        obj = Map.MapGrid[X][Y - 1];
        return true;
      }
      // look down
      if (Y >= GamePlayScreen.MapBox.Height || Map.MapGrid[X][Y + 1].Type.Symbol == symbol)
      {
        obj = Map.MapGrid[X][Y + 1];
        return true;
      }
      // not found
      obj = new MapObject();
      return false;
    }
  }
}
