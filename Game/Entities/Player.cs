using ConsoleDungeonCrawler.Game.Maps;

namespace ConsoleDungeonCrawler.Game.Entities
{
  internal static class Player
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
    internal static List<Armor> ArmorSet;
    internal static Weapon Weapon = new Weapon();
    internal static Dictionary<int, Spell> Spells;
    internal static Dictionary<int, Item> Inventory;
    internal static MapObject MapObj = new MapObject();

    static Player()
    {
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

    internal static void EquipArmor(Armor armor)
    {

    }

    internal static void EquipWeapon(Weapon weapon)
    {

    }

    internal static void EquipSpell(Spell spell)
    {

    }

    internal static void UseItem(Item item)
    {

    }

    internal static void UseSpell(Spell spell)
    {

    }

    internal static void TakeDamage(int damage)
    {
      Health -= damage;
      if (Health <= 0) Game.IsOver = true;
    }

    internal static void Heal(int amount)
    {
      Health += amount;
      if (Health > MaxHealth) Health = MaxHealth;
    }

    internal static void RestoreMana(int amount)
    {
      Mana += amount;
      if (Mana > MaxMana) Mana = MaxMana;
    }

    internal static void AddGold(decimal amount)
    {
      Gold += amount;
    }

    internal static void RemoveGold(decimal amount)
    {
      Gold -= amount;
    }

    internal static void LevelUp()
    {
      Level++;
    }

    internal static void AddExperience(int amount)
    {

    }

    internal static void AddToInventory(Item item)
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
  }
}
