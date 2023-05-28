namespace ConsoleDungeonCrawler.GameData
{
    internal static class Player
  {
    internal static string Name = "";
    internal static int Level = 1;
    internal static PlayerClass Class = PlayerClass.Rogue;
    internal static int Health = 100;
    internal static int MaxHealth = 100;
    internal static List<Armor> ArmorSet;
    internal static Weapon Weapon = new Weapon();
    internal static Dictionary<int, Spell> Spells;
    internal static Dictionary<int, Item> Inventory;


    static Player()
    {
      // Add 4 empty slots to armor set
      ArmorSet = new List<Armor>();
      ArmorSet.Add(new Armor(ArmorType.Head));
      ArmorSet.Add(new Armor(ArmorType.Body));
      ArmorSet.Add(new Armor(ArmorType.Legs));
      ArmorSet.Add(new Armor(ArmorType.Feet));

      // Add 5 empty slots to inventory
      Inventory = new Dictionary<int, Item>();
      for (int i = 1; i < 6; i++)
      {
        Inventory.Add(i, new Item(){ name = ItemName.None, rarity = ItemRarity.Common, id = 0 });
      }

      // Add 5 empty slots to spells
      Spells = new Dictionary<int, Spell>();
      for (int i = 1; i < 6; i++)
      {
        Spells.Add(i, new Spell() { Name = SpellName.None, DamageType = DamageType.Magical, DamageAmount = DamageEffectAmount.None });
      }
    }
  }


  internal class Armor
  {
    internal ArmorType Type = ArmorType.None;
    internal ArmorName Name = ArmorName.None;
    internal ItemRarity Rarity = ItemRarity.Common;
    internal int Defense = 0;
    internal int MaxDefense = 0;
    internal int Durability = 0;
    internal int MaxDurability = 0;

    internal Armor()
    {
    }

    internal Armor(ArmorType type)
    {
      Type = type;
    }

    internal Armor(ArmorType type, ArmorName name, ItemRarity rarity, int defense, int durability)
    {
      Type = type;
      Name = name;
      Rarity = rarity;
      Defense = defense;
      MaxDefense = defense;
      Durability = durability;
      MaxDurability = durability;
    }
  }

  internal class Weapon
  {
    internal WeaponName Name = WeaponName.Fists;
    internal DamageType DamageType = DamageType.Physical;
    internal DamageEffectAmount DamageAmount = DamageEffectAmount.One;
    internal List<DamageEffect> DamageEffects = new List<DamageEffect>();
    internal int Durability = 0;
    internal int MaxDurability = 0;
  }

  internal class Spell
  {
    internal int id = 0;
    internal SpellName Name = SpellName.None;
    internal string Description = "";
    internal DamageType DamageType = DamageType.Magical;
    internal DamageEffectAmount DamageAmount = DamageEffectAmount.One;
    internal List<DamageEffect> DamageEffects = new List<DamageEffect>();
    internal int ManaCost = 0;
    internal int MaxManaCost = 0;
  }
}
