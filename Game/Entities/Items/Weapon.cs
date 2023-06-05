using System.Runtime.CompilerServices;

namespace ConsoleDungeonCrawler.Game.Entities.Items
{
  internal class Weapon : Item
  {
    internal WeaponType WeaponType = WeaponType.Fists;
    internal int Damage = 1;
    internal int Durability = 100;
    internal int MaxDurability = 100;
    internal int Range = 1;


    internal Weapon()
    {
      Type = ItemType.Weapon;
      Name = "Fists";
      Description = "Your fists";

      WeaponType = WeaponType.Fists;
      Damage = 1;
      Durability = 100;
      MaxDurability = 100;
      Range = 1;

    }

    internal Weapon(string name, string description, WeaponType weaponType, int damage, int durability,
      int maxDurability, int range)
    {
      Type = ItemType.Weapon;
      Name = name;
      Description = description;

      WeaponType = weaponType;
      Damage = damage;
      Durability = durability;
      MaxDurability = maxDurability;
      Range = range;

    }

    internal static Weapon GetRandomWeapon()
    {
      Random rnd = new Random();
      WeaponType weaponType = (WeaponType)rnd.Next(1, Inventory.WeaponTypes.Count);
      Weapon weapon = Inventory.WeaponTypes[weaponType][rnd.Next(0, Inventory.WeaponTypes[weaponType].Count)];
      return weapon;
    }
  }

}
