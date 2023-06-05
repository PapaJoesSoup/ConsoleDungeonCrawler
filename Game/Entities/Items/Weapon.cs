namespace ConsoleDungeonCrawler.Game.Entities.Items
{
  internal class Weapon : Item
  {
    internal WeaponType WeaponType = WeaponType.Fists;
    internal int Damage = 1;
    internal int Durability = 100;
    internal int MaxDurability = 100;
    internal int Range = 1;

    internal static Dictionary<WeaponType, List<Weapon>> WeaponTypes = new Dictionary<WeaponType, List<Weapon>>();

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

      InitWeaponTypes();
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

      InitWeaponTypes();
    }

    private static void InitWeaponTypes()
    {
      WeaponTypes.Add(WeaponType.Fists, new List<Weapon>());
      WeaponTypes[WeaponType.Fists].Add(new Weapon());
      WeaponTypes[WeaponType.Fists].Add(new Weapon("Wolverine Claws", "A set of Wolverine Claws", WeaponType.Fists, 3, 100, 100, 1));
      WeaponTypes[WeaponType.Fists].Add(new Weapon("Tiger Claws", "A set of Tiger Claws", WeaponType.Fists, 6, 100, 100, 1));

      WeaponTypes.Add(WeaponType.Dagger, new List<Weapon>());
      WeaponTypes[WeaponType.Dagger].Add(new Weapon("Dagger", "A small dagger", WeaponType.Dagger, 2, 100, 100, 1));
      WeaponTypes[WeaponType.Dagger].Add(new Weapon("Jagged Dagger", "A Jagged dagger", WeaponType.Dagger, 4, 100, 100, 1));

      WeaponTypes.Add(WeaponType.Sword, new List<Weapon>());
      WeaponTypes[WeaponType.Sword].Add(new Weapon("Short Sword", "A short sword", WeaponType.Sword, 3, 100, 100, 1));
      WeaponTypes[WeaponType.Sword].Add(new Weapon("Long Sword", "A long sword", WeaponType.Sword, 4, 100, 100, 1));
      WeaponTypes[WeaponType.Sword].Add(new Weapon("Great Sword", "A great sword", WeaponType.Sword, 5, 100, 100, 1));

      WeaponTypes.Add(WeaponType.Axe, new List<Weapon>());
      WeaponTypes[WeaponType.Axe].Add(new Weapon("Battle Axe", "A battle axe", WeaponType.Axe, 6, 100, 100, 1));
      WeaponTypes[WeaponType.Axe].Add(new Weapon("Great Axe", "A great axe", WeaponType.Axe, 7, 100, 100, 1));

      WeaponTypes.Add(WeaponType.Mace, new List<Weapon>());
      WeaponTypes[WeaponType.Mace].Add(new Weapon("War Hammer", "A war hammer", WeaponType.Mace, 8, 100, 100, 1));
      WeaponTypes[WeaponType.Mace].Add(new Weapon("Great Hammer", "A great hammer", WeaponType.Mace, 9, 100, 100, 1));

      WeaponTypes.Add(WeaponType.Staff, new List<Weapon>());
      WeaponTypes[WeaponType.Staff].Add(new Weapon("Staff", "A staff", WeaponType.Staff, 10, 100, 100, 1));
      WeaponTypes[WeaponType.Staff].Add(new Weapon("Great Staff", "A great staff", WeaponType.Staff, 11, 100, 100, 1));
      
      WeaponTypes.Add(WeaponType.Wand, new List<Weapon>());
      WeaponTypes[WeaponType.Wand].Add(new Weapon("Wand", "A wand", WeaponType.Wand, 12, 100, 100, 1));
      WeaponTypes[WeaponType.Wand].Add(new Weapon("Great Wand", "A great wand", WeaponType.Wand, 13, 100, 100, 1));

      WeaponTypes.Add(WeaponType.Bow, new List<Weapon>());
      WeaponTypes[WeaponType.Bow].Add(new Weapon("Bow", "A bow", WeaponType.Bow, 13, 100, 100, 1));
      WeaponTypes[WeaponType.Bow].Add(new Weapon("Long Bow", "A Long bow", WeaponType.Bow, 13, 100, 100, 1));
      WeaponTypes[WeaponType.Bow].Add(new Weapon("Cross Bow", "A Cross bow", WeaponType.Bow, 13, 100, 100, 1));
    }

    internal static Weapon GetRandomWeapon()
    {
      Random rnd = new Random();
      WeaponType weaponType = (WeaponType)rnd.Next(1, WeaponTypes.Count);
      Weapon weapon = WeaponTypes[weaponType][rnd.Next(0, WeaponTypes[weaponType].Count)];
      return weapon;
    }
  }

}
