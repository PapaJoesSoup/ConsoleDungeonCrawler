using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleDungeonCrawler.Game.Entities.Items;

namespace ConsoleDungeonCrawler.Game.Entities
{
  internal static class Inventory
  {
    internal static Dictionary<ItemType, List<Item>> Items = new Dictionary<ItemType, List<Item>>();

    internal static Dictionary<WeaponType, List<Weapon>> WeaponTypes = new Dictionary<WeaponType, List<Weapon>>();
    internal static Dictionary<ArmorType, Dictionary<ArmorName, List<Armor>>> ArmorDictionary = new Dictionary<ArmorType, Dictionary<ArmorName, List<Armor>>>();
    internal static List<Bandage> Bandages = new List<Bandage>();
    internal static List<Food> Foods = new List<Food>();

    static Inventory()
    {
      InitWeaponTypes();
      InitArmorDictionary();
      InitBandages();
      InitFoodTypes();

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

    internal static void InitArmorDictionary()
    {
      ArmorDictionary.Add(ArmorType.Head, new Dictionary<ArmorName, List<Armor>>());
      ArmorDictionary.Add(ArmorType.Body, new Dictionary<ArmorName, List<Armor>>());
      ArmorDictionary.Add(ArmorType.Legs, new Dictionary<ArmorName, List<Armor>>());
      ArmorDictionary.Add(ArmorType.Feet, new Dictionary<ArmorName, List<Armor>>());
      ArmorDictionary.Add(ArmorType.Hands, new Dictionary<ArmorName, List<Armor>>());

      ArmorDictionary[ArmorType.Head].Add(ArmorName.None, new List<Armor>());
      ArmorDictionary[ArmorType.Head].Add(ArmorName.Cloth, new List<Armor>());
      ArmorDictionary[ArmorType.Head].Add(ArmorName.Leather, new List<Armor>());
      ArmorDictionary[ArmorType.Head].Add(ArmorName.ChainMail, new List<Armor>());
      ArmorDictionary[ArmorType.Head].Add(ArmorName.Plate, new List<Armor>());

      ArmorDictionary[ArmorType.Body].Add(ArmorName.None, new List<Armor>());
      ArmorDictionary[ArmorType.Body].Add(ArmorName.Cloth, new List<Armor>());
      ArmorDictionary[ArmorType.Body].Add(ArmorName.Leather, new List<Armor>());
      ArmorDictionary[ArmorType.Body].Add(ArmorName.ChainMail, new List<Armor>());
      ArmorDictionary[ArmorType.Body].Add(ArmorName.Plate, new List<Armor>());

      ArmorDictionary[ArmorType.Legs].Add(ArmorName.None, new List<Armor>());
      ArmorDictionary[ArmorType.Legs].Add(ArmorName.Cloth, new List<Armor>());
      ArmorDictionary[ArmorType.Legs].Add(ArmorName.Leather, new List<Armor>());
      ArmorDictionary[ArmorType.Legs].Add(ArmorName.ChainMail, new List<Armor>());
      ArmorDictionary[ArmorType.Legs].Add(ArmorName.Plate, new List<Armor>());

      ArmorDictionary[ArmorType.Feet].Add(ArmorName.None, new List<Armor>());
      ArmorDictionary[ArmorType.Feet].Add(ArmorName.Cloth, new List<Armor>());
      ArmorDictionary[ArmorType.Feet].Add(ArmorName.Leather, new List<Armor>());
      ArmorDictionary[ArmorType.Feet].Add(ArmorName.ChainMail, new List<Armor>());
      ArmorDictionary[ArmorType.Feet].Add(ArmorName.Plate, new List<Armor>());

      ArmorDictionary[ArmorType.Hands].Add(ArmorName.None, new List<Armor>());
      ArmorDictionary[ArmorType.Hands].Add(ArmorName.Cloth, new List<Armor>());
      ArmorDictionary[ArmorType.Hands].Add(ArmorName.Leather, new List<Armor>());
      ArmorDictionary[ArmorType.Hands].Add(ArmorName.ChainMail, new List<Armor>());
      ArmorDictionary[ArmorType.Hands].Add(ArmorName.Plate, new List<Armor>());

      ArmorDictionary[ArmorType.Head][ArmorName.None].Add(new Armor(ArmorType.Head, ArmorName.None, BuffType.None, ItemRarity.Common, 0, 0, 0, 0));
      ArmorDictionary[ArmorType.Head][ArmorName.Cloth].Add(new Armor(ArmorType.Head, ArmorName.Cloth, BuffType.None, ItemRarity.Common, 1, 10, 1, 1));
      ArmorDictionary[ArmorType.Head][ArmorName.Leather].Add(new Armor(ArmorType.Head, ArmorName.Leather, BuffType.None, ItemRarity.Common, 2, 20, 2, 2));
      ArmorDictionary[ArmorType.Head][ArmorName.ChainMail].Add(new Armor(ArmorType.Head, ArmorName.ChainMail, BuffType.None, ItemRarity.Common, 3, 30, 3, 3));
      ArmorDictionary[ArmorType.Head][ArmorName.Plate].Add(new Armor(ArmorType.Head, ArmorName.Plate, BuffType.None, ItemRarity.Common, 4, 40, 4, 4));

      ArmorDictionary[ArmorType.Head][ArmorName.Cloth].Add(new Armor(ArmorType.Head, ArmorName.Cloth, BuffType.None, ItemRarity.Uncommon, 2, 10, 1, 1));
      ArmorDictionary[ArmorType.Head][ArmorName.Leather].Add(new Armor(ArmorType.Head, ArmorName.Leather, BuffType.None, ItemRarity.Uncommon, 4, 20, 2, 2));
      ArmorDictionary[ArmorType.Head][ArmorName.ChainMail].Add(new Armor(ArmorType.Head, ArmorName.ChainMail, BuffType.None, ItemRarity.Uncommon, 6, 30, 3, 3));
      ArmorDictionary[ArmorType.Head][ArmorName.Plate].Add(new Armor(ArmorType.Head, ArmorName.Plate, BuffType.None, ItemRarity.Uncommon, 8, 40, 4, 4));

      ArmorDictionary[ArmorType.Head][ArmorName.Cloth].Add(new Armor(ArmorType.Head, ArmorName.Cloth, BuffType.None, ItemRarity.Rare, 3, 10, 1, 1));
      ArmorDictionary[ArmorType.Head][ArmorName.Leather].Add(new Armor(ArmorType.Head, ArmorName.Leather, BuffType.None, ItemRarity.Rare, 6, 20, 2, 2));
      ArmorDictionary[ArmorType.Head][ArmorName.ChainMail].Add(new Armor(ArmorType.Head, ArmorName.ChainMail, BuffType.None, ItemRarity.Rare, 9, 30, 3, 3));
      ArmorDictionary[ArmorType.Head][ArmorName.Plate].Add(new Armor(ArmorType.Head, ArmorName.Plate, BuffType.None, ItemRarity.Rare, 12, 40, 4, 4));

      ArmorDictionary[ArmorType.Head][ArmorName.Cloth].Add(new Armor(ArmorType.Head, ArmorName.Cloth, BuffType.None, ItemRarity.Epic, 4, 10, 1, 1));
      ArmorDictionary[ArmorType.Head][ArmorName.Leather].Add(new Armor(ArmorType.Head, ArmorName.Leather, BuffType.None, ItemRarity.Epic, 8, 20, 2, 2));
      ArmorDictionary[ArmorType.Head][ArmorName.ChainMail].Add(new Armor(ArmorType.Head, ArmorName.ChainMail, BuffType.None, ItemRarity.Epic, 12, 30, 3, 3));
      ArmorDictionary[ArmorType.Head][ArmorName.Plate].Add(new Armor(ArmorType.Head, ArmorName.Plate, BuffType.None, ItemRarity.Epic, 16, 40, 4, 4));

      ArmorDictionary[ArmorType.Head][ArmorName.Cloth].Add(new Armor(ArmorType.Head, ArmorName.Cloth, BuffType.None, ItemRarity.Legendary, 5, 10, 1, 1));
      ArmorDictionary[ArmorType.Head][ArmorName.Leather].Add(new Armor(ArmorType.Head, ArmorName.Leather, BuffType.None, ItemRarity.Legendary, 10, 20, 2, 2));
      ArmorDictionary[ArmorType.Head][ArmorName.ChainMail].Add(new Armor(ArmorType.Head, ArmorName.ChainMail, BuffType.None, ItemRarity.Legendary, 15, 30, 3, 3));
      ArmorDictionary[ArmorType.Head][ArmorName.Plate].Add(new Armor(ArmorType.Head, ArmorName.Plate, BuffType.None, ItemRarity.Legendary, 20, 40, 4, 4));
      
      ArmorDictionary[ArmorType.Body][ArmorName.None].Add(new Armor(ArmorType.Body, ArmorName.None, BuffType.None, ItemRarity.Common, 0, 0, 0, 0));
      ArmorDictionary[ArmorType.Body][ArmorName.Cloth].Add(new Armor(ArmorType.Body, ArmorName.Cloth, BuffType.None, ItemRarity.Common, 1, 10, 1, 1));
      ArmorDictionary[ArmorType.Body][ArmorName.Leather].Add(new Armor(ArmorType.Body, ArmorName.Leather, BuffType.None, ItemRarity.Common, 2, 20, 2, 2));
      ArmorDictionary[ArmorType.Body][ArmorName.ChainMail].Add(new Armor(ArmorType.Body, ArmorName.ChainMail, BuffType.None, ItemRarity.Common, 3, 30, 3, 3));
      ArmorDictionary[ArmorType.Body][ArmorName.Plate].Add(new Armor(ArmorType.Body, ArmorName.Plate, BuffType.None, ItemRarity.Common, 4, 40, 4, 4));

      ArmorDictionary[ArmorType.Body][ArmorName.Cloth].Add(new Armor(ArmorType.Body, ArmorName.Cloth, BuffType.None, ItemRarity.Uncommon, 2, 10, 1, 1));
      ArmorDictionary[ArmorType.Body][ArmorName.Leather].Add(new Armor(ArmorType.Body, ArmorName.Leather, BuffType.None, ItemRarity.Uncommon, 4, 20, 2, 2));
      ArmorDictionary[ArmorType.Body][ArmorName.ChainMail].Add(new Armor(ArmorType.Body, ArmorName.ChainMail, BuffType.None, ItemRarity.Uncommon, 6, 30, 3, 3));
      ArmorDictionary[ArmorType.Body][ArmorName.Plate].Add(new Armor(ArmorType.Body, ArmorName.Plate, BuffType.None, ItemRarity.Uncommon, 8, 40, 4, 4));

      ArmorDictionary[ArmorType.Body][ArmorName.Cloth].Add(new Armor(ArmorType.Body, ArmorName.Cloth, BuffType.None, ItemRarity.Rare, 3, 10, 1, 1));
      ArmorDictionary[ArmorType.Body][ArmorName.Leather].Add(new Armor(ArmorType.Body, ArmorName.Leather, BuffType.None, ItemRarity.Rare, 6, 20, 2, 2));
      ArmorDictionary[ArmorType.Body][ArmorName.ChainMail].Add(new Armor(ArmorType.Body, ArmorName.ChainMail, BuffType.None, ItemRarity.Rare, 9, 30, 3, 3));
      ArmorDictionary[ArmorType.Body][ArmorName.Plate].Add(new Armor(ArmorType.Body, ArmorName.Plate, BuffType.None, ItemRarity.Rare, 12, 40, 4, 4));

      ArmorDictionary[ArmorType.Body][ArmorName.Cloth].Add(new Armor(ArmorType.Body, ArmorName.Cloth, BuffType.None, ItemRarity.Epic, 4, 10, 1, 1));
      ArmorDictionary[ArmorType.Body][ArmorName.Leather].Add(new Armor(ArmorType.Body, ArmorName.Leather, BuffType.None, ItemRarity.Epic, 8, 20, 2, 2));
      ArmorDictionary[ArmorType.Body][ArmorName.ChainMail].Add(new Armor(ArmorType.Body, ArmorName.ChainMail, BuffType.None, ItemRarity.Epic, 12, 30, 3, 3));
      ArmorDictionary[ArmorType.Body][ArmorName.Plate].Add(new Armor(ArmorType.Body, ArmorName.Plate, BuffType.None, ItemRarity.Epic, 16, 40, 4, 4));

      ArmorDictionary[ArmorType.Body][ArmorName.Cloth].Add(new Armor(ArmorType.Body, ArmorName.Cloth, BuffType.None, ItemRarity.Legendary, 5, 10, 1, 1));
      ArmorDictionary[ArmorType.Body][ArmorName.Leather].Add(new Armor(ArmorType.Body, ArmorName.Leather, BuffType.None, ItemRarity.Legendary, 10, 20, 2, 2));
      ArmorDictionary[ArmorType.Body][ArmorName.ChainMail].Add(new Armor(ArmorType.Body, ArmorName.ChainMail, BuffType.None, ItemRarity.Legendary, 15, 30, 3, 3));
      ArmorDictionary[ArmorType.Body][ArmorName.Plate].Add(new Armor(ArmorType.Body, ArmorName.Plate, BuffType.None, ItemRarity.Legendary, 20, 40, 4, 4));
  
      ArmorDictionary[ArmorType.Legs][ArmorName.None].Add(new Armor(ArmorType.Legs, ArmorName.None, BuffType.None, ItemRarity.Common, 0, 0, 0, 0));
      ArmorDictionary[ArmorType.Legs][ArmorName.Cloth].Add(new Armor(ArmorType.Legs, ArmorName.Cloth, BuffType.None, ItemRarity.Common, 1, 10, 1, 1));
      ArmorDictionary[ArmorType.Legs][ArmorName.Leather].Add(new Armor(ArmorType.Legs, ArmorName.Leather, BuffType.None, ItemRarity.Common, 2, 20, 2, 2));
      ArmorDictionary[ArmorType.Legs][ArmorName.ChainMail].Add(new Armor(ArmorType.Legs, ArmorName.ChainMail, BuffType.None, ItemRarity.Common, 3, 30, 3, 3));
      ArmorDictionary[ArmorType.Legs][ArmorName.Plate].Add(new Armor(ArmorType.Legs, ArmorName.Plate, BuffType.None, ItemRarity.Common, 4, 40, 4, 4));

      ArmorDictionary[ArmorType.Legs][ArmorName.Cloth].Add(new Armor(ArmorType.Legs, ArmorName.Cloth, BuffType.None, ItemRarity.Uncommon, 2, 10, 1, 1));
      ArmorDictionary[ArmorType.Legs][ArmorName.Leather].Add(new Armor(ArmorType.Legs, ArmorName.Leather, BuffType.None, ItemRarity.Uncommon, 4, 20, 2, 2));
      ArmorDictionary[ArmorType.Legs][ArmorName.ChainMail].Add(new Armor(ArmorType.Legs, ArmorName.ChainMail, BuffType.None, ItemRarity.Uncommon, 6, 30, 3, 3));
      ArmorDictionary[ArmorType.Legs][ArmorName.Plate].Add(new Armor(ArmorType.Legs, ArmorName.Plate, BuffType.None, ItemRarity.Uncommon, 8, 40, 4, 4));

      ArmorDictionary[ArmorType.Legs][ArmorName.Cloth].Add(new Armor(ArmorType.Legs, ArmorName.Cloth, BuffType.None, ItemRarity.Rare, 3, 10, 1, 1));
      ArmorDictionary[ArmorType.Legs][ArmorName.Leather].Add(new Armor(ArmorType.Legs, ArmorName.Leather, BuffType.None, ItemRarity.Rare, 6, 20, 2, 2));
      ArmorDictionary[ArmorType.Legs][ArmorName.ChainMail].Add(new Armor(ArmorType.Legs, ArmorName.ChainMail, BuffType.None, ItemRarity.Rare, 9, 30, 3, 3));
      ArmorDictionary[ArmorType.Legs][ArmorName.Plate].Add(new Armor(ArmorType.Legs, ArmorName.Plate, BuffType.None, ItemRarity.Rare, 12, 40, 4, 4));

      ArmorDictionary[ArmorType.Legs][ArmorName.Cloth].Add(new Armor(ArmorType.Legs, ArmorName.Cloth, BuffType.None, ItemRarity.Epic, 4, 10, 1, 1));
      ArmorDictionary[ArmorType.Legs][ArmorName.Leather].Add(new Armor(ArmorType.Legs, ArmorName.Leather, BuffType.None, ItemRarity.Epic, 8, 20, 2, 2));
      ArmorDictionary[ArmorType.Legs][ArmorName.ChainMail].Add(new Armor(ArmorType.Legs, ArmorName.ChainMail, BuffType.None, ItemRarity.Epic, 12, 30, 3, 3));
      ArmorDictionary[ArmorType.Legs][ArmorName.Plate].Add(new Armor(ArmorType.Legs, ArmorName.Plate, BuffType.None, ItemRarity.Epic, 16, 40, 4, 4));

      ArmorDictionary[ArmorType.Legs][ArmorName.Cloth].Add(new Armor(ArmorType.Legs, ArmorName.Cloth, BuffType.None, ItemRarity.Legendary, 5, 10, 1, 1));
      ArmorDictionary[ArmorType.Legs][ArmorName.Leather].Add(new Armor(ArmorType.Legs, ArmorName.Leather, BuffType.None, ItemRarity.Legendary, 10, 20, 2, 2));
      ArmorDictionary[ArmorType.Legs][ArmorName.ChainMail].Add(new Armor(ArmorType.Legs, ArmorName.ChainMail, BuffType.None, ItemRarity.Legendary, 15, 30, 3, 3));
      ArmorDictionary[ArmorType.Legs][ArmorName.Plate].Add(new Armor(ArmorType.Legs, ArmorName.Plate, BuffType.None, ItemRarity.Legendary, 20, 40, 4, 4));

      ArmorDictionary[ArmorType.Feet][ArmorName.None].Add(new Armor(ArmorType.Feet, ArmorName.None, BuffType.None, ItemRarity.Common, 0, 0, 0, 0));
      ArmorDictionary[ArmorType.Feet][ArmorName.Cloth].Add(new Armor(ArmorType.Feet, ArmorName.Cloth, BuffType.None, ItemRarity.Common, 1, 10, 1, 1));
      ArmorDictionary[ArmorType.Feet][ArmorName.Leather].Add(new Armor(ArmorType.Feet, ArmorName.Leather, BuffType.None, ItemRarity.Common, 2, 20, 2, 2));
      ArmorDictionary[ArmorType.Feet][ArmorName.ChainMail].Add(new Armor(ArmorType.Feet, ArmorName.ChainMail, BuffType.None, ItemRarity.Common, 3, 30, 3, 3));
      ArmorDictionary[ArmorType.Feet][ArmorName.Plate].Add(new Armor(ArmorType.Feet, ArmorName.Plate, BuffType.None, ItemRarity.Common, 4, 40, 4, 4));

      ArmorDictionary[ArmorType.Feet][ArmorName.Cloth].Add(new Armor(ArmorType.Feet, ArmorName.Cloth, BuffType.None, ItemRarity.Uncommon, 2, 10, 1, 1));
      ArmorDictionary[ArmorType.Feet][ArmorName.Leather].Add(new Armor(ArmorType.Feet, ArmorName.Leather, BuffType.None, ItemRarity.Uncommon, 4, 20, 2, 2));
      ArmorDictionary[ArmorType.Feet][ArmorName.ChainMail].Add(new Armor(ArmorType.Feet, ArmorName.ChainMail, BuffType.None, ItemRarity.Uncommon, 6, 30, 3, 3));
      ArmorDictionary[ArmorType.Feet][ArmorName.Plate].Add(new Armor(ArmorType.Feet, ArmorName.Plate, BuffType.None, ItemRarity.Uncommon, 8, 40, 4, 4));

      ArmorDictionary[ArmorType.Feet][ArmorName.Cloth].Add(new Armor(ArmorType.Feet, ArmorName.Cloth, BuffType.None, ItemRarity.Rare, 3, 10, 1, 1));
      ArmorDictionary[ArmorType.Feet][ArmorName.Leather].Add(new Armor(ArmorType.Feet, ArmorName.Leather, BuffType.None, ItemRarity.Rare, 6, 20, 2, 2));
      ArmorDictionary[ArmorType.Feet][ArmorName.ChainMail].Add(new Armor(ArmorType.Feet, ArmorName.ChainMail, BuffType.None, ItemRarity.Rare, 9, 30, 3, 3));
      ArmorDictionary[ArmorType.Feet][ArmorName.Plate].Add(new Armor(ArmorType.Feet, ArmorName.Plate, BuffType.None, ItemRarity.Rare, 12, 40, 4, 4));

      ArmorDictionary[ArmorType.Feet][ArmorName.Cloth].Add(new Armor(ArmorType.Feet, ArmorName.Cloth, BuffType.None, ItemRarity.Epic, 4, 10, 1, 1));
      ArmorDictionary[ArmorType.Feet][ArmorName.Leather].Add(new Armor(ArmorType.Feet, ArmorName.Leather, BuffType.None, ItemRarity.Epic, 8, 20, 2, 2));
      ArmorDictionary[ArmorType.Feet][ArmorName.ChainMail].Add(new Armor(ArmorType.Feet, ArmorName.ChainMail, BuffType.None, ItemRarity.Epic, 12, 30, 3, 3));
      ArmorDictionary[ArmorType.Feet][ArmorName.Plate].Add(new Armor(ArmorType.Feet, ArmorName.Plate, BuffType.None, ItemRarity.Epic, 16, 40, 4, 4));

      ArmorDictionary[ArmorType.Feet][ArmorName.Cloth].Add(new Armor(ArmorType.Feet, ArmorName.Cloth, BuffType.None, ItemRarity.Legendary, 5, 10, 1, 1));
      ArmorDictionary[ArmorType.Feet][ArmorName.Leather].Add(new Armor(ArmorType.Feet, ArmorName.Leather, BuffType.None, ItemRarity.Legendary, 10, 20, 2, 2));
      ArmorDictionary[ArmorType.Feet][ArmorName.ChainMail].Add(new Armor(ArmorType.Feet, ArmorName.ChainMail, BuffType.None, ItemRarity.Legendary, 15, 30, 3, 3));
      ArmorDictionary[ArmorType.Feet][ArmorName.Plate].Add(new Armor(ArmorType.Feet, ArmorName.Plate, BuffType.None, ItemRarity.Legendary, 20, 40, 4, 4));

      ArmorDictionary[ArmorType.Hands][ArmorName.None].Add(new Armor(ArmorType.Hands, ArmorName.None, BuffType.None, ItemRarity.Common, 0, 0, 0, 0));
      ArmorDictionary[ArmorType.Hands][ArmorName.Cloth].Add(new Armor(ArmorType.Hands, ArmorName.Cloth, BuffType.None, ItemRarity.Common, 1, 10, 1, 1));
      ArmorDictionary[ArmorType.Hands][ArmorName.Leather].Add(new Armor(ArmorType.Hands, ArmorName.Leather, BuffType.None, ItemRarity.Common, 2, 20, 2, 2));
      ArmorDictionary[ArmorType.Hands][ArmorName.ChainMail].Add(new Armor(ArmorType.Hands, ArmorName.ChainMail, BuffType.None, ItemRarity.Common, 3, 30, 3, 3));
      ArmorDictionary[ArmorType.Hands][ArmorName.Plate].Add(new Armor(ArmorType.Hands, ArmorName.Plate, BuffType.None, ItemRarity.Common, 4, 40, 4, 4));

      ArmorDictionary[ArmorType.Hands][ArmorName.Cloth].Add(new Armor(ArmorType.Hands, ArmorName.Cloth, BuffType.None, ItemRarity.Uncommon, 2, 10, 1, 1));
      ArmorDictionary[ArmorType.Hands][ArmorName.Leather].Add(new Armor(ArmorType.Hands, ArmorName.Leather, BuffType.None, ItemRarity.Uncommon, 4, 20, 2, 2));
      ArmorDictionary[ArmorType.Hands][ArmorName.ChainMail].Add(new Armor(ArmorType.Hands, ArmorName.ChainMail, BuffType.None, ItemRarity.Uncommon, 6, 30, 3, 3));
      ArmorDictionary[ArmorType.Hands][ArmorName.Plate].Add(new Armor(ArmorType.Hands, ArmorName.Plate, BuffType.None, ItemRarity.Uncommon, 8, 40, 4, 4));

      ArmorDictionary[ArmorType.Hands][ArmorName.Cloth].Add(new Armor(ArmorType.Hands, ArmorName.Cloth, BuffType.None, ItemRarity.Rare, 3, 10, 1, 1));
      ArmorDictionary[ArmorType.Hands][ArmorName.Leather].Add(new Armor(ArmorType.Hands, ArmorName.Leather, BuffType.None, ItemRarity.Rare, 6, 20, 2, 2));
      ArmorDictionary[ArmorType.Hands][ArmorName.ChainMail].Add(new Armor(ArmorType.Hands, ArmorName.ChainMail, BuffType.None, ItemRarity.Rare, 9, 30, 3, 3));
      ArmorDictionary[ArmorType.Hands][ArmorName.Plate].Add(new Armor(ArmorType.Hands, ArmorName.Plate, BuffType.None, ItemRarity.Rare, 12, 40, 4, 4));

      ArmorDictionary[ArmorType.Hands][ArmorName.Cloth].Add(new Armor(ArmorType.Hands, ArmorName.Cloth, BuffType.None, ItemRarity.Epic, 4, 10, 1, 1));
      ArmorDictionary[ArmorType.Hands][ArmorName.Leather].Add(new Armor(ArmorType.Hands, ArmorName.Leather, BuffType.None, ItemRarity.Epic, 8, 20, 2, 2));
      ArmorDictionary[ArmorType.Hands][ArmorName.ChainMail].Add(new Armor(ArmorType.Hands, ArmorName.ChainMail, BuffType.None, ItemRarity.Epic, 12, 30, 3, 3));
      ArmorDictionary[ArmorType.Hands][ArmorName.Plate].Add(new Armor(ArmorType.Hands, ArmorName.Plate, BuffType.None, ItemRarity.Epic, 16, 40, 4, 4));

      ArmorDictionary[ArmorType.Hands][ArmorName.Cloth].Add(new Armor(ArmorType.Hands, ArmorName.Cloth, BuffType.None, ItemRarity.Legendary, 5, 10, 1, 1));
      ArmorDictionary[ArmorType.Hands][ArmorName.Leather].Add(new Armor(ArmorType.Hands, ArmorName.Leather, BuffType.None, ItemRarity.Legendary, 10, 20, 2, 2));
      ArmorDictionary[ArmorType.Hands][ArmorName.ChainMail].Add(new Armor(ArmorType.Hands, ArmorName.ChainMail, BuffType.None, ItemRarity.Legendary, 15, 30, 3, 3));
      ArmorDictionary[ArmorType.Hands][ArmorName.Plate].Add(new Armor(ArmorType.Hands, ArmorName.Plate, BuffType.None, ItemRarity.Legendary, 20, 40, 4, 4));
    }

    internal static void InitBandages()
    {
      Bandages.Add(new Bandage(BandageType.Cloth, 1, 1, 1, 0));
      Bandages.Add(new Bandage(BandageType.Linen, 2, 1, 1, 0));
      Bandages.Add(new Bandage(BandageType.Wool, 3, 1, 1, 0));
      Bandages.Add(new Bandage(BandageType.Silk, 4, 1, 1, 0));
      Bandages.Add(new Bandage(BandageType.Cotton, 5, 1, 1, 0));
      Bandages.Add(new Bandage(BandageType.RuneCloth, 6, 1, 1, 0));
    }

    internal static void InitFoodTypes()
    {
      Foods.Add(new Food(FoodType.Vegetable, BuffType.Health, 1, 1, 0));
      Foods.Add(new Food(FoodType.Fruit, BuffType.Health, 1, 1, 0));
      Foods.Add(new Food(FoodType.BearSteak, BuffType.Health, 1, 1, 0));
      Foods.Add(new Food(FoodType.Bread, BuffType.Health, 1, 1, 0));
      Foods.Add(new Food(FoodType.WolfSteak, BuffType.Health, 1, 1, 0));
      Foods.Add(new Food(FoodType.DeerSteak, BuffType.Health, 1, 1, 0));
      Foods.Add(new Food(FoodType.BoarChop, BuffType.Health, 1, 1, 0));
      Foods.Add(new Food(FoodType.Salmon, BuffType.HealthAndMana, 1, 1, 0));
      Foods.Add(new Food(FoodType.Trout, BuffType.HealthAndMana, 1, 1, 0));
      Foods.Add(new Food(FoodType.Snapper, BuffType.HealthAndMana, 1, 1, 0));
      Foods.Add(new Food(FoodType.MelonJuice, BuffType.Mana, 1, 1, 0));
      Foods.Add(new Food(FoodType.FruitJuice, BuffType.Mana, 1, 1, 0));
      Foods.Add(new Food(FoodType.Water, BuffType.Mana, 1, 1, 0));
      Foods.Add(new Food(FoodType.Tea, BuffType.Mana, 1, 1, 0));
      Foods.Add(new Food(FoodType.Coffee, BuffType.Mana, 1, 1, 0));
      Foods.Add(new Food(FoodType.Milk, BuffType.Mana, 1, 1, 0));
      Foods.Add(new Food(FoodType.Wine, BuffType.Mana, 1, 1, 0));
      Foods.Add(new Food(FoodType.Beer, BuffType.Mana, 1, 1, 0));
      Foods.Add(new Food(FoodType.Ale, BuffType.Mana, 1, 1, 0));
      Foods.Add(new Food(FoodType.Whiskey, BuffType.Mana, 1, 1, 0));
      Foods.Add(new Food(FoodType.Cider, BuffType.Mana, 1, 1, 0));
    }


    public static void AddItem(Item item)
    {
      if (!Items.ContainsKey(item.Type))
        Items.Add(item.Type, new List<Item>());

      Items[item.Type].Add(item);
    }

    public static void RemoveItem(Item item)
    {
      if (Items.ContainsKey(item.Type))
        Items[item.Type].Remove(item);
    }

    public static void RemoveAllItems(ItemType itemType)
    {
      if (Items.ContainsKey(itemType))
      {
        Items.Remove(itemType);
      }
    }

    public static void RemoveAllItems()
    {
      Items.Clear();
    }

    public static int GetQuantity(Item item)
    {
      if (Items.ContainsKey(item.Type))
      {
        return Items[item.Type].Count;
      }
      else
      {
        return 0;
      }
    }

    public static bool HasItem(Item item)
    {
      if (!Items.ContainsKey(item.Type)) return false;
      return Items[item.Type].Count > 0;
    }

    public static bool HasItems(List<Item> items)
    {
      foreach (Item item in items)
        if (!HasItem(item)) return false;

      return true;
    }

    // Create a random item and instantiate the associated class based on the ItemType return the item
    public static Item GetRandomItem()
    {
      int itemIdx = Dice.Roll(1, Enum.GetNames(typeof(ItemType)).Length);

      Item item = new Item((ItemType)itemIdx, 1, 0, 0);
      switch (item.Type)
      {
        case ItemType.Weapon:
          item = Weapon.GetRandomWeapon();
          break;
        case ItemType.Potion:
          item = Potion.GetRandomPotion();
          break;
        case ItemType.Food:
          item = Food.GetRandomFood();
          break;
        case ItemType.Gold:
          item.Quantity = new Random().Next(0, 5);
          item.Value = new Random().Next(0, 5);
          break;
        case ItemType.Armor:
          item = Armor.GetRandomArmor();
          break;
        case ItemType.Chest:
          item = Chest.GetRandomChest();
          break;
        case ItemType.Bandage:
          item = Bandage.GetRandomBandage();
          break;
      }
      return item;
    }
  }
}
