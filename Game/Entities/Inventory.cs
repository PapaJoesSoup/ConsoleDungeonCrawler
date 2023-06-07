using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities.Items;
using ConsoleDungeonCrawler.Game.Screens;

namespace ConsoleDungeonCrawler.Game.Entities
{
  internal static class Inventory
  {
    // Inventory interface dialog vars
    private static BoxChars b = new BoxChars() { botLeft = '=', botRight = '=', topRight = '=', topLeft = '=', hor = '=', ver = '|' };

    // Player Inventory
    internal static int BagCount = 1;
    internal static List<Bag> Bags = new List<Bag>();

    //Loot Tables
    internal static Dictionary<WeaponType, List<Weapon>> WeaponTypes = new Dictionary<WeaponType, List<Weapon>>();
    internal static Dictionary<ArmorType, Dictionary<ArmorName, List<Armor>>> ArmorDictionary = new Dictionary<ArmorType, Dictionary<ArmorName, List<Armor>>>();
    internal static List<Bandage> Bandages = new List<Bandage>();
    internal static Dictionary<BuffType, List<Food>> Foods = new Dictionary<BuffType, List<Food>>();


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
      WeaponTypes[WeaponType.Fists].Add(new Weapon("Wolverine Claws", "A set of Wolverine Claws", WeaponType.Fists, ItemRarity.Poor, 3, 100, 100, 1));
      WeaponTypes[WeaponType.Fists].Add(new Weapon("Tiger Claws", "A set of Tiger Claws", WeaponType.Fists, ItemRarity.Poor, 6, 100, 100, 1));
      WeaponTypes[WeaponType.Fists].Add(new Weapon("Wolverine Claws", "A set of Wolverine Claws", WeaponType.Fists, ItemRarity.Common, 3, 100, 100, 1));
      WeaponTypes[WeaponType.Fists].Add(new Weapon("Tiger Claws", "A set of Tiger Claws", WeaponType.Fists, ItemRarity.Common, 6, 100, 100, 1));
      WeaponTypes[WeaponType.Fists].Add(new Weapon("Wolverine Claws", "A set of Wolverine Claws", WeaponType.Fists, ItemRarity.Uncommon, 3, 100, 100, 1));
      WeaponTypes[WeaponType.Fists].Add(new Weapon("Tiger Claws", "A set of Tiger Claws", WeaponType.Fists, ItemRarity.Uncommon, 6, 100, 100, 1));
      WeaponTypes[WeaponType.Fists].Add(new Weapon("Wolverine Claws", "A set of Wolverine Claws", WeaponType.Fists, ItemRarity.Rare, 3, 100, 100, 1));
      WeaponTypes[WeaponType.Fists].Add(new Weapon("Tiger Claws", "A set of Tiger Claws", WeaponType.Fists, ItemRarity.Rare, 6, 100, 100, 1));
      WeaponTypes[WeaponType.Fists].Add(new Weapon("Wolverine Claws", "A set of Wolverine Claws", WeaponType.Fists, ItemRarity.Epic, 3, 100, 100, 1));
      WeaponTypes[WeaponType.Fists].Add(new Weapon("Tiger Claws", "A set of Tiger Claws", WeaponType.Fists, ItemRarity.Epic, 6, 100, 100, 1));
      WeaponTypes[WeaponType.Fists].Add(new Weapon("Wolverine Claws", "A set of Wolverine Claws", WeaponType.Fists, ItemRarity.Legendary, 3, 100, 100, 1));
      WeaponTypes[WeaponType.Fists].Add(new Weapon("Tiger Claws", "A set of Tiger Claws", WeaponType.Fists, ItemRarity.Legendary, 6, 100, 100, 1));

      WeaponTypes.Add(WeaponType.Dagger, new List<Weapon>());
      WeaponTypes[WeaponType.Dagger].Add(new Weapon("Dagger", "A small dagger", WeaponType.Dagger, ItemRarity.Poor, 2, 100, 100, 1));
      WeaponTypes[WeaponType.Dagger].Add(new Weapon("Jagged Dagger", "A Jagged dagger", WeaponType.Dagger, ItemRarity.Poor, 4, 100, 100, 1));

      WeaponTypes.Add(WeaponType.Sword, new List<Weapon>());
      WeaponTypes[WeaponType.Sword].Add(new Weapon("Short Sword", "A short sword", WeaponType.Sword, ItemRarity.Poor, 3, 100, 100, 1));
      WeaponTypes[WeaponType.Sword].Add(new Weapon("Long Sword", "A long sword", WeaponType.Sword, ItemRarity.Poor, 4, 100, 100, 1));
      WeaponTypes[WeaponType.Sword].Add(new Weapon("Great Sword", "A great sword", WeaponType.Sword, ItemRarity.Poor, 5, 100, 100, 1));
      WeaponTypes[WeaponType.Sword].Add(new Weapon("Short Sword", "A short sword", WeaponType.Sword, ItemRarity.Common, 3, 100, 100, 1));
      WeaponTypes[WeaponType.Sword].Add(new Weapon("Long Sword", "A long sword", WeaponType.Sword, ItemRarity.Common, 4, 100, 100, 1));
      WeaponTypes[WeaponType.Sword].Add(new Weapon("Great Sword", "A great sword", WeaponType.Sword, ItemRarity.Common, 5, 100, 100, 1));
      WeaponTypes[WeaponType.Sword].Add(new Weapon("Short Sword", "A short sword", WeaponType.Sword, ItemRarity.Uncommon, 3, 100, 100, 1));
      WeaponTypes[WeaponType.Sword].Add(new Weapon("Long Sword", "A long sword", WeaponType.Sword, ItemRarity.Uncommon, 4, 100, 100, 1));
      WeaponTypes[WeaponType.Sword].Add(new Weapon("Great Sword", "A great sword", WeaponType.Sword, ItemRarity.Uncommon, 5, 100, 100, 1));
      WeaponTypes[WeaponType.Sword].Add(new Weapon("Short Sword", "A short sword", WeaponType.Sword, ItemRarity.Rare, 3, 100, 100, 1));
      WeaponTypes[WeaponType.Sword].Add(new Weapon("Long Sword", "A long sword", WeaponType.Sword, ItemRarity.Rare, 4, 100, 100, 1));
      WeaponTypes[WeaponType.Sword].Add(new Weapon("Great Sword", "A great sword", WeaponType.Sword, ItemRarity.Rare, 5, 100, 100, 1));
      WeaponTypes[WeaponType.Sword].Add(new Weapon("Short Sword", "A short sword", WeaponType.Sword, ItemRarity.Epic, 3, 100, 100, 1));
      WeaponTypes[WeaponType.Sword].Add(new Weapon("Long Sword", "A long sword", WeaponType.Sword, ItemRarity.Epic, 4, 100, 100, 1));
      WeaponTypes[WeaponType.Sword].Add(new Weapon("Great Sword", "A great sword", WeaponType.Sword, ItemRarity.Epic, 5, 100, 100, 1));
      WeaponTypes[WeaponType.Sword].Add(new Weapon("Short Sword", "A short sword", WeaponType.Sword, ItemRarity.Legendary, 3, 100, 100, 1));
      WeaponTypes[WeaponType.Sword].Add(new Weapon("Long Sword", "A long sword", WeaponType.Sword, ItemRarity.Legendary, 4, 100, 100, 1));
      WeaponTypes[WeaponType.Sword].Add(new Weapon("Great Sword", "A great sword", WeaponType.Sword, ItemRarity.Legendary, 5, 100, 100, 1));

      WeaponTypes.Add(WeaponType.Axe, new List<Weapon>());
      WeaponTypes[WeaponType.Axe].Add(new Weapon("Battle Axe", "A battle axe", WeaponType.Axe, ItemRarity.Poor, 6, 100, 100, 1));
      WeaponTypes[WeaponType.Axe].Add(new Weapon("Great Axe", "A great axe", WeaponType.Axe, ItemRarity.Poor, 7, 100, 100, 1));
      WeaponTypes[WeaponType.Axe].Add(new Weapon("Battle Axe", "A battle axe", WeaponType.Axe, ItemRarity.Common, 6, 100, 100, 1));
      WeaponTypes[WeaponType.Axe].Add(new Weapon("Great Axe", "A great axe", WeaponType.Axe, ItemRarity.Common, 7, 100, 100, 1));
      WeaponTypes[WeaponType.Axe].Add(new Weapon("Battle Axe", "A battle axe", WeaponType.Axe, ItemRarity.Uncommon, 6, 100, 100, 1));
      WeaponTypes[WeaponType.Axe].Add(new Weapon("Great Axe", "A great axe", WeaponType.Axe, ItemRarity.Uncommon, 7, 100, 100, 1));
      WeaponTypes[WeaponType.Axe].Add(new Weapon("Battle Axe", "A battle axe", WeaponType.Axe, ItemRarity.Rare, 6, 100, 100, 1));
      WeaponTypes[WeaponType.Axe].Add(new Weapon("Great Axe", "A great axe", WeaponType.Axe, ItemRarity.Rare, 7, 100, 100, 1));
      WeaponTypes[WeaponType.Axe].Add(new Weapon("Battle Axe", "A battle axe", WeaponType.Axe, ItemRarity.Epic, 6, 100, 100, 1));
      WeaponTypes[WeaponType.Axe].Add(new Weapon("Great Axe", "A great axe", WeaponType.Axe, ItemRarity.Epic, 7, 100, 100, 1));
      WeaponTypes[WeaponType.Axe].Add(new Weapon("Battle Axe", "A battle axe", WeaponType.Axe, ItemRarity.Legendary, 6, 100, 100, 1));
      WeaponTypes[WeaponType.Axe].Add(new Weapon("Great Axe", "A great axe", WeaponType.Axe, ItemRarity.Legendary, 7, 100, 100, 1));

      WeaponTypes.Add(WeaponType.Mace, new List<Weapon>());
      WeaponTypes[WeaponType.Mace].Add(new Weapon("War Hammer", "A war hammer", WeaponType.Mace, ItemRarity.Poor, 8, 100, 100, 1));
      WeaponTypes[WeaponType.Mace].Add(new Weapon("Great Hammer", "A great hammer", WeaponType.Mace, ItemRarity.Poor, 9, 100, 100, 1));
      WeaponTypes[WeaponType.Mace].Add(new Weapon("War Hammer", "A war hammer", WeaponType.Mace, ItemRarity.Common, 8, 100, 100, 1));
      WeaponTypes[WeaponType.Mace].Add(new Weapon("Great Hammer", "A great hammer", WeaponType.Mace, ItemRarity.Common, 9, 100, 100, 1));
      WeaponTypes[WeaponType.Mace].Add(new Weapon("War Hammer", "A war hammer", WeaponType.Mace, ItemRarity.Uncommon, 8, 100, 100, 1));
      WeaponTypes[WeaponType.Mace].Add(new Weapon("Great Hammer", "A great hammer", WeaponType.Mace, ItemRarity.Uncommon, 9, 100, 100, 1));
      WeaponTypes[WeaponType.Mace].Add(new Weapon("War Hammer", "A war hammer", WeaponType.Mace, ItemRarity.Rare, 8, 100, 100, 1));
      WeaponTypes[WeaponType.Mace].Add(new Weapon("Great Hammer", "A great hammer", WeaponType.Mace, ItemRarity.Rare, 9, 100, 100, 1));
      WeaponTypes[WeaponType.Mace].Add(new Weapon("War Hammer", "A war hammer", WeaponType.Mace, ItemRarity.Epic, 8, 100, 100, 1));
      WeaponTypes[WeaponType.Mace].Add(new Weapon("Great Hammer", "A great hammer", WeaponType.Mace, ItemRarity.Epic, 9, 100, 100, 1));
      WeaponTypes[WeaponType.Mace].Add(new Weapon("War Hammer", "A war hammer", WeaponType.Mace, ItemRarity.Legendary, 8, 100, 100, 1));
      WeaponTypes[WeaponType.Mace].Add(new Weapon("Great Hammer", "A great hammer", WeaponType.Mace, ItemRarity.Legendary, 9, 100, 100, 1));

      WeaponTypes.Add(WeaponType.Staff, new List<Weapon>());
      WeaponTypes[WeaponType.Staff].Add(new Weapon("Staff", "A staff", WeaponType.Staff, ItemRarity.Poor, 10, 100, 100, 1));
      WeaponTypes[WeaponType.Staff].Add(new Weapon("Great Staff", "A great staff", WeaponType.Staff, ItemRarity.Poor, 11, 100, 100, 1));
      WeaponTypes[WeaponType.Staff].Add(new Weapon("Staff", "A staff", WeaponType.Staff, ItemRarity.Common, 10, 100, 100, 1));
      WeaponTypes[WeaponType.Staff].Add(new Weapon("Great Staff", "A great staff", WeaponType.Staff, ItemRarity.Common, 11, 100, 100, 1));
      WeaponTypes[WeaponType.Staff].Add(new Weapon("Staff", "A staff", WeaponType.Staff, ItemRarity.Uncommon, 10, 100, 100, 1));
      WeaponTypes[WeaponType.Staff].Add(new Weapon("Great Staff", "A great staff", WeaponType.Staff, ItemRarity.Uncommon, 11, 100, 100, 1));
      WeaponTypes[WeaponType.Staff].Add(new Weapon("Staff", "A staff", WeaponType.Staff, ItemRarity.Rare, 10, 100, 100, 1));
      WeaponTypes[WeaponType.Staff].Add(new Weapon("Great Staff", "A great staff", WeaponType.Staff, ItemRarity.Rare, 11, 100, 100, 1));
      WeaponTypes[WeaponType.Staff].Add(new Weapon("Staff", "A staff", WeaponType.Staff, ItemRarity.Epic, 10, 100, 100, 1));
      WeaponTypes[WeaponType.Staff].Add(new Weapon("Great Staff", "A great staff", WeaponType.Staff, ItemRarity.Epic, 11, 100, 100, 1));
      WeaponTypes[WeaponType.Staff].Add(new Weapon("Staff", "A staff", WeaponType.Staff, ItemRarity.Legendary, 10, 100, 100, 1));
      WeaponTypes[WeaponType.Staff].Add(new Weapon("Great Staff", "A great staff", WeaponType.Staff, ItemRarity.Legendary, 11, 100, 100, 1));

      WeaponTypes.Add(WeaponType.Wand, new List<Weapon>());
      WeaponTypes[WeaponType.Wand].Add(new Weapon("Wand", "A wand", WeaponType.Wand, ItemRarity.Poor, 12, 100, 100, 1));
      WeaponTypes[WeaponType.Wand].Add(new Weapon("Great Wand", "A great wand", WeaponType.Wand, ItemRarity.Poor, 13, 100, 100, 1));
      WeaponTypes[WeaponType.Wand].Add(new Weapon("Wand", "A wand", WeaponType.Wand, ItemRarity.Common, 12, 100, 100, 1));
      WeaponTypes[WeaponType.Wand].Add(new Weapon("Great Wand", "A great wand", WeaponType.Wand, ItemRarity.Common, 13, 100, 100, 1));
      WeaponTypes[WeaponType.Wand].Add(new Weapon("Wand", "A wand", WeaponType.Wand, ItemRarity.Uncommon, 12, 100, 100, 1));
      WeaponTypes[WeaponType.Wand].Add(new Weapon("Great Wand", "A great wand", WeaponType.Wand, ItemRarity.Uncommon, 13, 100, 100, 1));
      WeaponTypes[WeaponType.Wand].Add(new Weapon("Wand", "A wand", WeaponType.Wand, ItemRarity.Rare, 12, 100, 100, 1));
      WeaponTypes[WeaponType.Wand].Add(new Weapon("Great Wand", "A great wand", WeaponType.Wand, ItemRarity.Rare, 13, 100, 100, 1));
      WeaponTypes[WeaponType.Wand].Add(new Weapon("Wand", "A wand", WeaponType.Wand, ItemRarity.Epic, 12, 100, 100, 1));
      WeaponTypes[WeaponType.Wand].Add(new Weapon("Great Wand", "A great wand", WeaponType.Wand, ItemRarity.Epic, 13, 100, 100, 1));
      WeaponTypes[WeaponType.Wand].Add(new Weapon("Wand", "A wand", WeaponType.Wand, ItemRarity.Legendary, 12, 100, 100, 1));
      WeaponTypes[WeaponType.Wand].Add(new Weapon("Great Wand", "A great wand", WeaponType.Wand, ItemRarity.Legendary, 13, 100, 100, 1));

      WeaponTypes.Add(WeaponType.Bow, new List<Weapon>());
      WeaponTypes[WeaponType.Bow].Add(new Weapon("Bow", "A bow", WeaponType.Bow, ItemRarity.Poor, 13, 100, 100, 1));
      WeaponTypes[WeaponType.Bow].Add(new Weapon("Long Bow", "A Long bow", WeaponType.Bow, ItemRarity.Poor, 13, 100, 100, 1));
      WeaponTypes[WeaponType.Bow].Add(new Weapon("Cross Bow", "A Cross bow", WeaponType.Bow, ItemRarity.Poor, 13, 100, 100, 1));
      WeaponTypes[WeaponType.Bow].Add(new Weapon("Bow", "A bow", WeaponType.Bow, ItemRarity.Common, 13, 100, 100, 1));
      WeaponTypes[WeaponType.Bow].Add(new Weapon("Long Bow", "A Long bow", WeaponType.Bow, ItemRarity.Common, 13, 100, 100, 1));
      WeaponTypes[WeaponType.Bow].Add(new Weapon("Cross Bow", "A Cross bow", WeaponType.Bow, ItemRarity.Common, 13, 100, 100, 1));
      WeaponTypes[WeaponType.Bow].Add(new Weapon("Bow", "A bow", WeaponType.Bow, ItemRarity.Uncommon, 13, 100, 100, 1));
      WeaponTypes[WeaponType.Bow].Add(new Weapon("Long Bow", "A Long bow", WeaponType.Bow, ItemRarity.Uncommon, 13, 100, 100, 1));
      WeaponTypes[WeaponType.Bow].Add(new Weapon("Cross Bow", "A Cross bow", WeaponType.Bow, ItemRarity.Uncommon, 13, 100, 100, 1));
      WeaponTypes[WeaponType.Bow].Add(new Weapon("Bow", "A bow", WeaponType.Bow, ItemRarity.Rare, 13, 100, 100, 1));
      WeaponTypes[WeaponType.Bow].Add(new Weapon("Long Bow", "A Long bow", WeaponType.Bow, ItemRarity.Rare, 13, 100, 100, 1));
      WeaponTypes[WeaponType.Bow].Add(new Weapon("Cross Bow", "A Cross bow", WeaponType.Bow, ItemRarity.Rare, 13, 100, 100, 1));
      WeaponTypes[WeaponType.Bow].Add(new Weapon("Bow", "A bow", WeaponType.Bow, ItemRarity.Epic, 13, 100, 100, 1));
      WeaponTypes[WeaponType.Bow].Add(new Weapon("Long Bow", "A Long bow", WeaponType.Bow, ItemRarity.Epic, 13, 100, 100, 1));
      WeaponTypes[WeaponType.Bow].Add(new Weapon("Cross Bow", "A Cross bow", WeaponType.Bow, ItemRarity.Epic, 13, 100, 100, 1));
      WeaponTypes[WeaponType.Bow].Add(new Weapon("Bow", "A bow", WeaponType.Bow, ItemRarity.Legendary, 13, 100, 100, 1));
      WeaponTypes[WeaponType.Bow].Add(new Weapon("Long Bow", "A Long bow", WeaponType.Bow, ItemRarity.Legendary, 13, 100, 100, 1));
      WeaponTypes[WeaponType.Bow].Add(new Weapon("Cross Bow", "A Cross bow", WeaponType.Bow, ItemRarity.Legendary, 13, 100, 100, 1));
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

      ArmorDictionary[ArmorType.Head][ArmorName.None].Add(new Armor(ArmorType.Head, ArmorName.None, BuffType.None, ItemRarity.Poor, 0, 0, 0, 0));

      ArmorDictionary[ArmorType.Head][ArmorName.Cloth].Add(new Armor(ArmorType.Head, ArmorName.Cloth, BuffType.None, ItemRarity.Poor, 1, 10, 1, 1));
      ArmorDictionary[ArmorType.Head][ArmorName.Leather].Add(new Armor(ArmorType.Head, ArmorName.Leather, BuffType.None, ItemRarity.Poor, 2, 20, 2, 2));
      ArmorDictionary[ArmorType.Head][ArmorName.ChainMail].Add(new Armor(ArmorType.Head, ArmorName.ChainMail, BuffType.None, ItemRarity.Poor, 3, 30, 3, 3));
      ArmorDictionary[ArmorType.Head][ArmorName.Plate].Add(new Armor(ArmorType.Head, ArmorName.Plate, BuffType.None, ItemRarity.Poor, 4, 40, 4, 4));
      
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
      
      ArmorDictionary[ArmorType.Body][ArmorName.None].Add(new Armor(ArmorType.Body, ArmorName.None, BuffType.None, ItemRarity.Poor, 0, 0, 0, 0));
      ArmorDictionary[ArmorType.Body][ArmorName.Cloth].Add(new Armor(ArmorType.Body, ArmorName.Cloth, BuffType.None, ItemRarity.Poor, 1, 10, 1, 1));
      ArmorDictionary[ArmorType.Body][ArmorName.Leather].Add(new Armor(ArmorType.Body, ArmorName.Leather, BuffType.None, ItemRarity.Poor, 2, 20, 2, 2));
      ArmorDictionary[ArmorType.Body][ArmorName.ChainMail].Add(new Armor(ArmorType.Body, ArmorName.ChainMail, BuffType.None, ItemRarity.Poor, 3, 30, 3, 3));
      ArmorDictionary[ArmorType.Body][ArmorName.Plate].Add(new Armor(ArmorType.Body, ArmorName.Plate, BuffType.None, ItemRarity.Poor, 4, 40, 4, 4));


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
  
      ArmorDictionary[ArmorType.Legs][ArmorName.None].Add(new Armor(ArmorType.Legs, ArmorName.None, BuffType.None, ItemRarity.Poor, 0, 0, 0, 0));
      ArmorDictionary[ArmorType.Legs][ArmorName.Cloth].Add(new Armor(ArmorType.Legs, ArmorName.Cloth, BuffType.None, ItemRarity.Poor, 1, 10, 1, 1));
      ArmorDictionary[ArmorType.Legs][ArmorName.Leather].Add(new Armor(ArmorType.Legs, ArmorName.Leather, BuffType.None, ItemRarity.Poor, 2, 20, 2, 2));
      ArmorDictionary[ArmorType.Legs][ArmorName.ChainMail].Add(new Armor(ArmorType.Legs, ArmorName.ChainMail, BuffType.None, ItemRarity.Poor, 3, 30, 3, 3));
      ArmorDictionary[ArmorType.Legs][ArmorName.Plate].Add(new Armor(ArmorType.Legs, ArmorName.Plate, BuffType.None, ItemRarity.Poor, 4, 40, 4, 4));

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

      ArmorDictionary[ArmorType.Feet][ArmorName.None].Add(new Armor(ArmorType.Feet, ArmorName.None, BuffType.None, ItemRarity.Poor, 0, 0, 0, 0));
      ArmorDictionary[ArmorType.Feet][ArmorName.Cloth].Add(new Armor(ArmorType.Feet, ArmorName.Cloth, BuffType.None, ItemRarity.Poor, 1, 10, 1, 1));
      ArmorDictionary[ArmorType.Feet][ArmorName.Leather].Add(new Armor(ArmorType.Feet, ArmorName.Leather, BuffType.None, ItemRarity.Poor, 2, 20, 2, 2));
      ArmorDictionary[ArmorType.Feet][ArmorName.ChainMail].Add(new Armor(ArmorType.Feet, ArmorName.ChainMail, BuffType.None, ItemRarity.Poor, 3, 30, 3, 3));
      ArmorDictionary[ArmorType.Feet][ArmorName.Plate].Add(new Armor(ArmorType.Feet, ArmorName.Plate, BuffType.None, ItemRarity.Poor, 4, 40, 4, 4));

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

      ArmorDictionary[ArmorType.Hands][ArmorName.None].Add(new Armor(ArmorType.Hands, ArmorName.None, BuffType.None, ItemRarity.Poor, 0, 0, 0, 0));
      ArmorDictionary[ArmorType.Hands][ArmorName.Cloth].Add(new Armor(ArmorType.Hands, ArmorName.Cloth, BuffType.None, ItemRarity.Poor, 1, 10, 1, 1));
      ArmorDictionary[ArmorType.Hands][ArmorName.Leather].Add(new Armor(ArmorType.Hands, ArmorName.Leather, BuffType.None, ItemRarity.Poor, 2, 20, 2, 2));
      ArmorDictionary[ArmorType.Hands][ArmorName.ChainMail].Add(new Armor(ArmorType.Hands, ArmorName.ChainMail, BuffType.None, ItemRarity.Poor, 3, 30, 3, 3));
      ArmorDictionary[ArmorType.Hands][ArmorName.Plate].Add(new Armor(ArmorType.Hands, ArmorName.Plate, BuffType.None, ItemRarity.Poor, 4, 40, 4, 4));

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
      Foods.Add(BuffType.Health, new List<Food>());
      Foods[BuffType.Health].Add(new Food(FoodType.Vegetable, BuffType.Health, 1, 1, 0));
      Foods[BuffType.Health].Add(new Food(FoodType.Fruit, BuffType.Health, 1, 1, 0));
      Foods[BuffType.Health].Add(new Food(FoodType.BearSteak, BuffType.Health, 1, 1, 0));
      Foods[BuffType.Health].Add(new Food(FoodType.Bread, BuffType.Health, 1, 1, 0));
      Foods[BuffType.Health].Add(new Food(FoodType.WolfSteak, BuffType.Health, 1, 1, 0));
      Foods[BuffType.Health].Add(new Food(FoodType.DeerSteak, BuffType.Health, 1, 1, 0));
      Foods[BuffType.Health].Add(new Food(FoodType.BoarChop, BuffType.Health, 1, 1, 0));

      Foods.Add(BuffType.HealthAndMana, new List<Food>());
      Foods[BuffType.HealthAndMana].Add(new Food(FoodType.Salmon, BuffType.HealthAndMana, 1, 1, 0));
      Foods[BuffType.HealthAndMana].Add(new Food(FoodType.Trout, BuffType.HealthAndMana, 1, 1, 0));
      Foods[BuffType.HealthAndMana].Add(new Food(FoodType.Snapper, BuffType.HealthAndMana, 1, 1, 0));
      Foods[BuffType.HealthAndMana].Add(new Food(FoodType.Feast, BuffType.HealthAndMana, 1, 1, 0));
      
      Foods.Add(BuffType.Mana, new List<Food>());
      Foods[BuffType.Mana].Add(new Food(FoodType.MelonJuice, BuffType.Mana, 1, 1, 0));
      Foods[BuffType.Mana].Add(new Food(FoodType.FruitJuice, BuffType.Mana, 1, 1, 0));
      Foods[BuffType.Mana].Add(new Food(FoodType.Water, BuffType.Mana, 1, 1, 0));
      Foods[BuffType.Mana].Add(new Food(FoodType.Tea, BuffType.Mana, 1, 1, 0));
      Foods[BuffType.Mana].Add(new Food(FoodType.Coffee, BuffType.Mana, 1, 1, 0));
      Foods[BuffType.Mana].Add(new Food(FoodType.Milk, BuffType.Mana, 1, 1, 0));
      Foods[BuffType.Mana].Add(new Food(FoodType.Wine, BuffType.Mana, 1, 1, 0));
      Foods[BuffType.Mana].Add(new Food(FoodType.Beer, BuffType.Mana, 1, 1, 0));
      Foods[BuffType.Mana].Add(new Food(FoodType.Ale, BuffType.Mana, 1, 1, 0));
      Foods[BuffType.Mana].Add(new Food(FoodType.Whiskey, BuffType.Mana, 1, 1, 0));
      Foods[BuffType.Mana].Add(new Food(FoodType.Cider, BuffType.Mana, 1, 1, 0));
    }

    internal static void Draw()
    {
      Dialog.Draw();
      ConsoleEx.WriteAlignedAt("Player Inventory Management", HAlign.Center, VAlign.Middle, Color.Bisque, Color.Olive);
      ConsoleEx.WriteAlignedAt("Press any key to continue", HAlign.Center, VAlign.Middle, Color.Bisque, Color.Olive, 0, 2);
      Console.ReadKey(true);
      Dialog.Close();
    }

    public static bool AddItem(Item item)
    {
      if (item.Type == ItemType.Gold)
      {
        Player.Gold += item.Quantity;
        return true;
      }
      foreach (Bag bag in Bags)
      {
        if (bag.AddItem(item))
        {
          return true;
        }
      }
      GamePlay.Messages.Add(new Message("Your bags are full", Color.Red, Color.Black));
      return false;
    }

    public static BuffType GetRandomBuffType(int min = 1)
    {
      int index = Dice.Roll(min, Enum.GetNames(typeof(BuffType)).Length);
      return (BuffType)index;
    }

    public static bool RemoveItem(Item item)
    {
      foreach (Bag bag in Bags)
      {
        if (bag.RemoveItem(item)) return true;
      }
      return false;
    }

    public static void RemoveAllItems()
    {
      foreach (Bag bag in Bags)
      {
        bag.Items.Clear();
      }
    }

    public static int GetQuantity(Item item)
    {
      int quantity = 0;
      foreach (Bag bag in Bags)
      {
        quantity += bag.GetQuantity(item);
      }
      return quantity;
    }

    public static bool HasItem(Item item)
    {
      foreach(Bag bag in Bags)
      {
        if (bag.GetQuantity(item) > 0) return true;
      }
      return false;
    }

    public static bool HasItems(List<Item> items)
    {
      foreach (Item item in items)
        if (!HasItem(item)) return false;

      return true;
    }

    public static Item GetRandomItem()
    {
      int itemIdx = Dice.Roll(1, Enum.GetNames(typeof(ItemType)).Length);
      Item item = GetRandomItem((ItemType)itemIdx);
      return item;
    }
    
    public static Item GetRandomItem(ItemType type)
    {
      switch (type)
      {
        case ItemType.Weapon:
          return Weapon.GetRandomWeapon();
        case ItemType.Potion:
          return Potion.GetRandomPotion();
        case ItemType.Food:
          return Food.GetRandomFood();
        case ItemType.Gold:
          return new Item(ItemType.Gold, Dice.Roll(1, 5), 1, 1);
        case ItemType.Armor:
          return Armor.GetRandomArmor();
        case ItemType.Chest:
          return Chest.GetRandomChest();
        case ItemType.Bandage:
          return Bandage.GetRandomBandage();
      }
      return new Item();
    }
  }
}
