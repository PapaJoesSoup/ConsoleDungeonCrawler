﻿using System.Drawing;
using ConsoleDungeonCrawler.Game.Entities.Items;
using ConsoleDungeonCrawler.Game.Screens;

namespace ConsoleDungeonCrawler.Game.Entities;

internal static class Inventory
{
  // Inventory interface dialog vars

  // Player Inventory
  internal static int BagCount = 1;
  internal static int MaxBags = 4;
  internal static readonly List<Bag> Bags = new();

  //Loot Tables
  internal static readonly Dictionary<WeaponType, List<Weapon>> WeaponTypes = new();
  internal static readonly Dictionary<ArmorType, Dictionary<ArmorName, List<Armor>>> ArmorDictionary = new();
  internal static readonly List<Bandage> Bandages = new();
  internal static readonly Dictionary<BuffType, List<Food>> Foods = new();
  internal static readonly Dictionary<SpellName, Spell> Spells = new();
  internal static readonly Dictionary<BuffType, List<Potion>> Potions = new();


  static Inventory()
  {
    InitWeaponTypes();
    InitArmorDictionary();
    InitBandages();
    InitFoodTypes();
    InitPotionTypes();
    InitSpellTypes();
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
    WeaponTypes[WeaponType.Wand].Add(new Weapon("Wand", "A wand", WeaponType.Wand, ItemRarity.Poor, 12, 100, 100, 6));
    WeaponTypes[WeaponType.Wand].Add(new Weapon("Great Wand", "A great wand", WeaponType.Wand, ItemRarity.Poor, 13, 100, 100, 6));
    WeaponTypes[WeaponType.Wand].Add(new Weapon("Wand", "A wand", WeaponType.Wand, ItemRarity.Common, 12, 100, 100, 6));
    WeaponTypes[WeaponType.Wand].Add(new Weapon("Great Wand", "A great wand", WeaponType.Wand, ItemRarity.Common, 13, 100, 100, 6));
    WeaponTypes[WeaponType.Wand].Add(new Weapon("Wand", "A wand", WeaponType.Wand, ItemRarity.Uncommon, 12, 100, 100, 6));
    WeaponTypes[WeaponType.Wand].Add(new Weapon("Great Wand", "A great wand", WeaponType.Wand, ItemRarity.Uncommon, 13, 100, 100, 6));
    WeaponTypes[WeaponType.Wand].Add(new Weapon("Wand", "A wand", WeaponType.Wand, ItemRarity.Rare, 12, 100, 100, 6));
    WeaponTypes[WeaponType.Wand].Add(new Weapon("Great Wand", "A great wand", WeaponType.Wand, ItemRarity.Rare, 13, 100, 100, 6));
    WeaponTypes[WeaponType.Wand].Add(new Weapon("Wand", "A wand", WeaponType.Wand, ItemRarity.Epic, 12, 100, 100, 6));
    WeaponTypes[WeaponType.Wand].Add(new Weapon("Great Wand", "A great wand", WeaponType.Wand, ItemRarity.Epic, 13, 100, 100, 6));
    WeaponTypes[WeaponType.Wand].Add(new Weapon("Wand", "A wand", WeaponType.Wand, ItemRarity.Legendary, 12, 100, 100, 6));
    WeaponTypes[WeaponType.Wand].Add(new Weapon("Great Wand", "A great wand", WeaponType.Wand, ItemRarity.Legendary, 13, 100, 100, 6));

    WeaponTypes.Add(WeaponType.Bow, new List<Weapon>());
    WeaponTypes[WeaponType.Bow].Add(new Weapon("Bow", "A bow", WeaponType.Bow, ItemRarity.Poor, 13, 100, 100, 6));
    WeaponTypes[WeaponType.Bow].Add(new Weapon("Long Bow", "A Long bow", WeaponType.Bow, ItemRarity.Poor, 13, 100, 100, 6));
    WeaponTypes[WeaponType.Bow].Add(new Weapon("Cross Bow", "A Cross bow", WeaponType.Bow, ItemRarity.Poor, 13, 100, 100, 6));
    WeaponTypes[WeaponType.Bow].Add(new Weapon("Bow", "A bow", WeaponType.Bow, ItemRarity.Common, 13, 100, 100, 6));
    WeaponTypes[WeaponType.Bow].Add(new Weapon("Long Bow", "A Long bow", WeaponType.Bow, ItemRarity.Common, 13, 100, 100, 6));
    WeaponTypes[WeaponType.Bow].Add(new Weapon("Cross Bow", "A Cross bow", WeaponType.Bow, ItemRarity.Common, 13, 100, 100, 6));
    WeaponTypes[WeaponType.Bow].Add(new Weapon("Bow", "A bow", WeaponType.Bow, ItemRarity.Uncommon, 13, 100, 100, 1));
    WeaponTypes[WeaponType.Bow].Add(new Weapon("Long Bow", "A Long bow", WeaponType.Bow, ItemRarity.Uncommon, 13, 100, 100, 6));
    WeaponTypes[WeaponType.Bow].Add(new Weapon("Cross Bow", "A Cross bow", WeaponType.Bow, ItemRarity.Uncommon, 13, 100, 100, 6));
    WeaponTypes[WeaponType.Bow].Add(new Weapon("Bow", "A bow", WeaponType.Bow, ItemRarity.Rare, 13, 100, 100, 1));
    WeaponTypes[WeaponType.Bow].Add(new Weapon("Long Bow", "A Long bow", WeaponType.Bow, ItemRarity.Rare, 13, 100, 100, 6));
    WeaponTypes[WeaponType.Bow].Add(new Weapon("Cross Bow", "A Cross bow", WeaponType.Bow, ItemRarity.Rare, 13, 100, 100, 6));
    WeaponTypes[WeaponType.Bow].Add(new Weapon("Bow", "A bow", WeaponType.Bow, ItemRarity.Epic, 13, 100, 100, 1));
    WeaponTypes[WeaponType.Bow].Add(new Weapon("Long Bow", "A Long bow", WeaponType.Bow, ItemRarity.Epic, 13, 100, 100, 6));
    WeaponTypes[WeaponType.Bow].Add(new Weapon("Cross Bow", "A Cross bow", WeaponType.Bow, ItemRarity.Epic, 13, 100, 100, 6));
    WeaponTypes[WeaponType.Bow].Add(new Weapon("Bow", "A bow", WeaponType.Bow, ItemRarity.Legendary, 13, 100, 100, 6));
    WeaponTypes[WeaponType.Bow].Add(new Weapon("Long Bow", "A Long bow", WeaponType.Bow, ItemRarity.Legendary, 13, 100, 100, 6));
    WeaponTypes[WeaponType.Bow].Add(new Weapon("Cross Bow", "A Cross bow", WeaponType.Bow, ItemRarity.Legendary, 13, 100, 100, 6));
  }

  private static void InitArmorDictionary()
  {
    ArmorDictionary.Add(ArmorType.Head, new Dictionary<ArmorName, List<Armor>>());
    ArmorDictionary.Add(ArmorType.Body, new Dictionary<ArmorName, List<Armor>>());
    ArmorDictionary.Add(ArmorType.Leg, new Dictionary<ArmorName, List<Armor>>());
    ArmorDictionary.Add(ArmorType.Feet, new Dictionary<ArmorName, List<Armor>>());
    ArmorDictionary.Add(ArmorType.Hand, new Dictionary<ArmorName, List<Armor>>());

    ArmorDictionary[ArmorType.Head].Add(ArmorName.None, new List<Armor>());
    ArmorDictionary[ArmorType.Head].Add(ArmorName.Cloth, new List<Armor>());
    ArmorDictionary[ArmorType.Head].Add(ArmorName.Leather, new List<Armor>());
    ArmorDictionary[ArmorType.Head].Add(ArmorName.Mail, new List<Armor>());
    ArmorDictionary[ArmorType.Head].Add(ArmorName.Plate, new List<Armor>());

    ArmorDictionary[ArmorType.Body].Add(ArmorName.None, new List<Armor>());
    ArmorDictionary[ArmorType.Body].Add(ArmorName.Cloth, new List<Armor>());
    ArmorDictionary[ArmorType.Body].Add(ArmorName.Leather, new List<Armor>());
    ArmorDictionary[ArmorType.Body].Add(ArmorName.Mail, new List<Armor>());
    ArmorDictionary[ArmorType.Body].Add(ArmorName.Plate, new List<Armor>());

    ArmorDictionary[ArmorType.Leg].Add(ArmorName.None, new List<Armor>());
    ArmorDictionary[ArmorType.Leg].Add(ArmorName.Cloth, new List<Armor>());
    ArmorDictionary[ArmorType.Leg].Add(ArmorName.Leather, new List<Armor>());
    ArmorDictionary[ArmorType.Leg].Add(ArmorName.Mail, new List<Armor>());
    ArmorDictionary[ArmorType.Leg].Add(ArmorName.Plate, new List<Armor>());

    ArmorDictionary[ArmorType.Feet].Add(ArmorName.None, new List<Armor>());
    ArmorDictionary[ArmorType.Feet].Add(ArmorName.Cloth, new List<Armor>());
    ArmorDictionary[ArmorType.Feet].Add(ArmorName.Leather, new List<Armor>());
    ArmorDictionary[ArmorType.Feet].Add(ArmorName.Mail, new List<Armor>());
    ArmorDictionary[ArmorType.Feet].Add(ArmorName.Plate, new List<Armor>());

    ArmorDictionary[ArmorType.Hand].Add(ArmorName.None, new List<Armor>());
    ArmorDictionary[ArmorType.Hand].Add(ArmorName.Cloth, new List<Armor>());
    ArmorDictionary[ArmorType.Hand].Add(ArmorName.Leather, new List<Armor>());
    ArmorDictionary[ArmorType.Hand].Add(ArmorName.Mail, new List<Armor>());
    ArmorDictionary[ArmorType.Hand].Add(ArmorName.Plate, new List<Armor>());

    ArmorDictionary[ArmorType.Head][ArmorName.None].Add(new Armor(ArmorType.Head, ArmorName.None, BuffType.None, ItemRarity.Poor, 0, 0, 0, 0));

    ArmorDictionary[ArmorType.Head][ArmorName.Cloth].Add(new Armor(ArmorType.Head, ArmorName.Cloth, BuffType.None, ItemRarity.Poor, 1, 10, 1, 1));
    ArmorDictionary[ArmorType.Head][ArmorName.Leather].Add(new Armor(ArmorType.Head, ArmorName.Leather, BuffType.None, ItemRarity.Poor, 2, 20, 2, 2));
    ArmorDictionary[ArmorType.Head][ArmorName.Mail].Add(new Armor(ArmorType.Head, ArmorName.Mail, BuffType.None, ItemRarity.Poor, 3, 30, 3, 3));
    ArmorDictionary[ArmorType.Head][ArmorName.Plate].Add(new Armor(ArmorType.Head, ArmorName.Plate, BuffType.None, ItemRarity.Poor, 4, 40, 4, 4));
      
    ArmorDictionary[ArmorType.Head][ArmorName.Cloth].Add(new Armor(ArmorType.Head, ArmorName.Cloth, BuffType.None, ItemRarity.Common, 1, 10, 1, 1));
    ArmorDictionary[ArmorType.Head][ArmorName.Leather].Add(new Armor(ArmorType.Head, ArmorName.Leather, BuffType.None, ItemRarity.Common, 2, 20, 2, 2));
    ArmorDictionary[ArmorType.Head][ArmorName.Mail].Add(new Armor(ArmorType.Head, ArmorName.Mail, BuffType.None, ItemRarity.Common, 3, 30, 3, 3));
    ArmorDictionary[ArmorType.Head][ArmorName.Plate].Add(new Armor(ArmorType.Head, ArmorName.Plate, BuffType.None, ItemRarity.Common, 4, 40, 4, 4));

    ArmorDictionary[ArmorType.Head][ArmorName.Cloth].Add(new Armor(ArmorType.Head, ArmorName.Cloth, BuffType.None, ItemRarity.Uncommon, 2, 10, 1, 1));
    ArmorDictionary[ArmorType.Head][ArmorName.Leather].Add(new Armor(ArmorType.Head, ArmorName.Leather, BuffType.None, ItemRarity.Uncommon, 4, 20, 2, 2));
    ArmorDictionary[ArmorType.Head][ArmorName.Mail].Add(new Armor(ArmorType.Head, ArmorName.Mail, BuffType.None, ItemRarity.Uncommon, 6, 30, 3, 3));
    ArmorDictionary[ArmorType.Head][ArmorName.Plate].Add(new Armor(ArmorType.Head, ArmorName.Plate, BuffType.None, ItemRarity.Uncommon, 8, 40, 4, 4));

    ArmorDictionary[ArmorType.Head][ArmorName.Cloth].Add(new Armor(ArmorType.Head, ArmorName.Cloth, BuffType.None, ItemRarity.Rare, 3, 10, 1, 1));
    ArmorDictionary[ArmorType.Head][ArmorName.Leather].Add(new Armor(ArmorType.Head, ArmorName.Leather, BuffType.None, ItemRarity.Rare, 6, 20, 2, 2));
    ArmorDictionary[ArmorType.Head][ArmorName.Mail].Add(new Armor(ArmorType.Head, ArmorName.Mail, BuffType.None, ItemRarity.Rare, 9, 30, 3, 3));
    ArmorDictionary[ArmorType.Head][ArmorName.Plate].Add(new Armor(ArmorType.Head, ArmorName.Plate, BuffType.None, ItemRarity.Rare, 12, 40, 4, 4));

    ArmorDictionary[ArmorType.Head][ArmorName.Cloth].Add(new Armor(ArmorType.Head, ArmorName.Cloth, BuffType.None, ItemRarity.Epic, 4, 10, 1, 1));
    ArmorDictionary[ArmorType.Head][ArmorName.Leather].Add(new Armor(ArmorType.Head, ArmorName.Leather, BuffType.None, ItemRarity.Epic, 8, 20, 2, 2));
    ArmorDictionary[ArmorType.Head][ArmorName.Mail].Add(new Armor(ArmorType.Head, ArmorName.Mail, BuffType.None, ItemRarity.Epic, 12, 30, 3, 3));
    ArmorDictionary[ArmorType.Head][ArmorName.Plate].Add(new Armor(ArmorType.Head, ArmorName.Plate, BuffType.None, ItemRarity.Epic, 16, 40, 4, 4));

    ArmorDictionary[ArmorType.Head][ArmorName.Cloth].Add(new Armor(ArmorType.Head, ArmorName.Cloth, BuffType.None, ItemRarity.Legendary, 5, 10, 1, 1));
    ArmorDictionary[ArmorType.Head][ArmorName.Leather].Add(new Armor(ArmorType.Head, ArmorName.Leather, BuffType.None, ItemRarity.Legendary, 10, 20, 2, 2));
    ArmorDictionary[ArmorType.Head][ArmorName.Mail].Add(new Armor(ArmorType.Head, ArmorName.Mail, BuffType.None, ItemRarity.Legendary, 15, 30, 3, 3));
    ArmorDictionary[ArmorType.Head][ArmorName.Plate].Add(new Armor(ArmorType.Head, ArmorName.Plate, BuffType.None, ItemRarity.Legendary, 20, 40, 4, 4));
      
    ArmorDictionary[ArmorType.Body][ArmorName.None].Add(new Armor(ArmorType.Body, ArmorName.None, BuffType.None, ItemRarity.Poor, 0, 0, 0, 0));
    ArmorDictionary[ArmorType.Body][ArmorName.Cloth].Add(new Armor(ArmorType.Body, ArmorName.Cloth, BuffType.None, ItemRarity.Poor, 1, 10, 1, 1));
    ArmorDictionary[ArmorType.Body][ArmorName.Leather].Add(new Armor(ArmorType.Body, ArmorName.Leather, BuffType.None, ItemRarity.Poor, 2, 20, 2, 2));
    ArmorDictionary[ArmorType.Body][ArmorName.Mail].Add(new Armor(ArmorType.Body, ArmorName.Mail, BuffType.None, ItemRarity.Poor, 3, 30, 3, 3));
    ArmorDictionary[ArmorType.Body][ArmorName.Plate].Add(new Armor(ArmorType.Body, ArmorName.Plate, BuffType.None, ItemRarity.Poor, 4, 40, 4, 4));


    ArmorDictionary[ArmorType.Body][ArmorName.Cloth].Add(new Armor(ArmorType.Body, ArmorName.Cloth, BuffType.None, ItemRarity.Common, 1, 10, 1, 1));
    ArmorDictionary[ArmorType.Body][ArmorName.Leather].Add(new Armor(ArmorType.Body, ArmorName.Leather, BuffType.None, ItemRarity.Common, 2, 20, 2, 2));
    ArmorDictionary[ArmorType.Body][ArmorName.Mail].Add(new Armor(ArmorType.Body, ArmorName.Mail, BuffType.None, ItemRarity.Common, 3, 30, 3, 3));
    ArmorDictionary[ArmorType.Body][ArmorName.Plate].Add(new Armor(ArmorType.Body, ArmorName.Plate, BuffType.None, ItemRarity.Common, 4, 40, 4, 4));

    ArmorDictionary[ArmorType.Body][ArmorName.Cloth].Add(new Armor(ArmorType.Body, ArmorName.Cloth, BuffType.None, ItemRarity.Uncommon, 2, 10, 1, 1));
    ArmorDictionary[ArmorType.Body][ArmorName.Leather].Add(new Armor(ArmorType.Body, ArmorName.Leather, BuffType.None, ItemRarity.Uncommon, 4, 20, 2, 2));
    ArmorDictionary[ArmorType.Body][ArmorName.Mail].Add(new Armor(ArmorType.Body, ArmorName.Mail, BuffType.None, ItemRarity.Uncommon, 6, 30, 3, 3));
    ArmorDictionary[ArmorType.Body][ArmorName.Plate].Add(new Armor(ArmorType.Body, ArmorName.Plate, BuffType.None, ItemRarity.Uncommon, 8, 40, 4, 4));

    ArmorDictionary[ArmorType.Body][ArmorName.Cloth].Add(new Armor(ArmorType.Body, ArmorName.Cloth, BuffType.None, ItemRarity.Rare, 3, 10, 1, 1));
    ArmorDictionary[ArmorType.Body][ArmorName.Leather].Add(new Armor(ArmorType.Body, ArmorName.Leather, BuffType.None, ItemRarity.Rare, 6, 20, 2, 2));
    ArmorDictionary[ArmorType.Body][ArmorName.Mail].Add(new Armor(ArmorType.Body, ArmorName.Mail, BuffType.None, ItemRarity.Rare, 9, 30, 3, 3));
    ArmorDictionary[ArmorType.Body][ArmorName.Plate].Add(new Armor(ArmorType.Body, ArmorName.Plate, BuffType.None, ItemRarity.Rare, 12, 40, 4, 4));

    ArmorDictionary[ArmorType.Body][ArmorName.Cloth].Add(new Armor(ArmorType.Body, ArmorName.Cloth, BuffType.None, ItemRarity.Epic, 4, 10, 1, 1));
    ArmorDictionary[ArmorType.Body][ArmorName.Leather].Add(new Armor(ArmorType.Body, ArmorName.Leather, BuffType.None, ItemRarity.Epic, 8, 20, 2, 2));
    ArmorDictionary[ArmorType.Body][ArmorName.Mail].Add(new Armor(ArmorType.Body, ArmorName.Mail, BuffType.None, ItemRarity.Epic, 12, 30, 3, 3));
    ArmorDictionary[ArmorType.Body][ArmorName.Plate].Add(new Armor(ArmorType.Body, ArmorName.Plate, BuffType.None, ItemRarity.Epic, 16, 40, 4, 4));

    ArmorDictionary[ArmorType.Body][ArmorName.Cloth].Add(new Armor(ArmorType.Body, ArmorName.Cloth, BuffType.None, ItemRarity.Legendary, 5, 10, 1, 1));
    ArmorDictionary[ArmorType.Body][ArmorName.Leather].Add(new Armor(ArmorType.Body, ArmorName.Leather, BuffType.None, ItemRarity.Legendary, 10, 20, 2, 2));
    ArmorDictionary[ArmorType.Body][ArmorName.Mail].Add(new Armor(ArmorType.Body, ArmorName.Mail, BuffType.None, ItemRarity.Legendary, 15, 30, 3, 3));
    ArmorDictionary[ArmorType.Body][ArmorName.Plate].Add(new Armor(ArmorType.Body, ArmorName.Plate, BuffType.None, ItemRarity.Legendary, 20, 40, 4, 4));
  
    ArmorDictionary[ArmorType.Leg][ArmorName.None].Add(new Armor(ArmorType.Leg, ArmorName.None, BuffType.None, ItemRarity.Poor, 0, 0, 0, 0));
    ArmorDictionary[ArmorType.Leg][ArmorName.Cloth].Add(new Armor(ArmorType.Leg, ArmorName.Cloth, BuffType.None, ItemRarity.Poor, 1, 10, 1, 1));
    ArmorDictionary[ArmorType.Leg][ArmorName.Leather].Add(new Armor(ArmorType.Leg, ArmorName.Leather, BuffType.None, ItemRarity.Poor, 2, 20, 2, 2));
    ArmorDictionary[ArmorType.Leg][ArmorName.Mail].Add(new Armor(ArmorType.Leg, ArmorName.Mail, BuffType.None, ItemRarity.Poor, 3, 30, 3, 3));
    ArmorDictionary[ArmorType.Leg][ArmorName.Plate].Add(new Armor(ArmorType.Leg, ArmorName.Plate, BuffType.None, ItemRarity.Poor, 4, 40, 4, 4));

    ArmorDictionary[ArmorType.Leg][ArmorName.Cloth].Add(new Armor(ArmorType.Leg, ArmorName.Cloth, BuffType.None, ItemRarity.Common, 1, 10, 1, 1));
    ArmorDictionary[ArmorType.Leg][ArmorName.Leather].Add(new Armor(ArmorType.Leg, ArmorName.Leather, BuffType.None, ItemRarity.Common, 2, 20, 2, 2));
    ArmorDictionary[ArmorType.Leg][ArmorName.Mail].Add(new Armor(ArmorType.Leg, ArmorName.Mail, BuffType.None, ItemRarity.Common, 3, 30, 3, 3));
    ArmorDictionary[ArmorType.Leg][ArmorName.Plate].Add(new Armor(ArmorType.Leg, ArmorName.Plate, BuffType.None, ItemRarity.Common, 4, 40, 4, 4));

    ArmorDictionary[ArmorType.Leg][ArmorName.Cloth].Add(new Armor(ArmorType.Leg, ArmorName.Cloth, BuffType.None, ItemRarity.Uncommon, 2, 10, 1, 1));
    ArmorDictionary[ArmorType.Leg][ArmorName.Leather].Add(new Armor(ArmorType.Leg, ArmorName.Leather, BuffType.None, ItemRarity.Uncommon, 4, 20, 2, 2));
    ArmorDictionary[ArmorType.Leg][ArmorName.Mail].Add(new Armor(ArmorType.Leg, ArmorName.Mail, BuffType.None, ItemRarity.Uncommon, 6, 30, 3, 3));
    ArmorDictionary[ArmorType.Leg][ArmorName.Plate].Add(new Armor(ArmorType.Leg, ArmorName.Plate, BuffType.None, ItemRarity.Uncommon, 8, 40, 4, 4));

    ArmorDictionary[ArmorType.Leg][ArmorName.Cloth].Add(new Armor(ArmorType.Leg, ArmorName.Cloth, BuffType.None, ItemRarity.Rare, 3, 10, 1, 1));
    ArmorDictionary[ArmorType.Leg][ArmorName.Leather].Add(new Armor(ArmorType.Leg, ArmorName.Leather, BuffType.None, ItemRarity.Rare, 6, 20, 2, 2));
    ArmorDictionary[ArmorType.Leg][ArmorName.Mail].Add(new Armor(ArmorType.Leg, ArmorName.Mail, BuffType.None, ItemRarity.Rare, 9, 30, 3, 3));
    ArmorDictionary[ArmorType.Leg][ArmorName.Plate].Add(new Armor(ArmorType.Leg, ArmorName.Plate, BuffType.None, ItemRarity.Rare, 12, 40, 4, 4));

    ArmorDictionary[ArmorType.Leg][ArmorName.Cloth].Add(new Armor(ArmorType.Leg, ArmorName.Cloth, BuffType.None, ItemRarity.Epic, 4, 10, 1, 1));
    ArmorDictionary[ArmorType.Leg][ArmorName.Leather].Add(new Armor(ArmorType.Leg, ArmorName.Leather, BuffType.None, ItemRarity.Epic, 8, 20, 2, 2));
    ArmorDictionary[ArmorType.Leg][ArmorName.Mail].Add(new Armor(ArmorType.Leg, ArmorName.Mail, BuffType.None, ItemRarity.Epic, 12, 30, 3, 3));
    ArmorDictionary[ArmorType.Leg][ArmorName.Plate].Add(new Armor(ArmorType.Leg, ArmorName.Plate, BuffType.None, ItemRarity.Epic, 16, 40, 4, 4));

    ArmorDictionary[ArmorType.Leg][ArmorName.Cloth].Add(new Armor(ArmorType.Leg, ArmorName.Cloth, BuffType.None, ItemRarity.Legendary, 5, 10, 1, 1));
    ArmorDictionary[ArmorType.Leg][ArmorName.Leather].Add(new Armor(ArmorType.Leg, ArmorName.Leather, BuffType.None, ItemRarity.Legendary, 10, 20, 2, 2));
    ArmorDictionary[ArmorType.Leg][ArmorName.Mail].Add(new Armor(ArmorType.Leg, ArmorName.Mail, BuffType.None, ItemRarity.Legendary, 15, 30, 3, 3));
    ArmorDictionary[ArmorType.Leg][ArmorName.Plate].Add(new Armor(ArmorType.Leg, ArmorName.Plate, BuffType.None, ItemRarity.Legendary, 20, 40, 4, 4));

    ArmorDictionary[ArmorType.Feet][ArmorName.None].Add(new Armor(ArmorType.Feet, ArmorName.None, BuffType.None, ItemRarity.Poor, 0, 0, 0, 0));
    ArmorDictionary[ArmorType.Feet][ArmorName.Cloth].Add(new Armor(ArmorType.Feet, ArmorName.Cloth, BuffType.None, ItemRarity.Poor, 1, 10, 1, 1));
    ArmorDictionary[ArmorType.Feet][ArmorName.Leather].Add(new Armor(ArmorType.Feet, ArmorName.Leather, BuffType.None, ItemRarity.Poor, 2, 20, 2, 2));
    ArmorDictionary[ArmorType.Feet][ArmorName.Mail].Add(new Armor(ArmorType.Feet, ArmorName.Mail, BuffType.None, ItemRarity.Poor, 3, 30, 3, 3));
    ArmorDictionary[ArmorType.Feet][ArmorName.Plate].Add(new Armor(ArmorType.Feet, ArmorName.Plate, BuffType.None, ItemRarity.Poor, 4, 40, 4, 4));

    ArmorDictionary[ArmorType.Feet][ArmorName.Cloth].Add(new Armor(ArmorType.Feet, ArmorName.Cloth, BuffType.None, ItemRarity.Common, 1, 10, 1, 1));
    ArmorDictionary[ArmorType.Feet][ArmorName.Leather].Add(new Armor(ArmorType.Feet, ArmorName.Leather, BuffType.None, ItemRarity.Common, 2, 20, 2, 2));
    ArmorDictionary[ArmorType.Feet][ArmorName.Mail].Add(new Armor(ArmorType.Feet, ArmorName.Mail, BuffType.None, ItemRarity.Common, 3, 30, 3, 3));
    ArmorDictionary[ArmorType.Feet][ArmorName.Plate].Add(new Armor(ArmorType.Feet, ArmorName.Plate, BuffType.None, ItemRarity.Common, 4, 40, 4, 4));

    ArmorDictionary[ArmorType.Feet][ArmorName.Cloth].Add(new Armor(ArmorType.Feet, ArmorName.Cloth, BuffType.None, ItemRarity.Uncommon, 2, 10, 1, 1));
    ArmorDictionary[ArmorType.Feet][ArmorName.Leather].Add(new Armor(ArmorType.Feet, ArmorName.Leather, BuffType.None, ItemRarity.Uncommon, 4, 20, 2, 2));
    ArmorDictionary[ArmorType.Feet][ArmorName.Mail].Add(new Armor(ArmorType.Feet, ArmorName.Mail, BuffType.None, ItemRarity.Uncommon, 6, 30, 3, 3));
    ArmorDictionary[ArmorType.Feet][ArmorName.Plate].Add(new Armor(ArmorType.Feet, ArmorName.Plate, BuffType.None, ItemRarity.Uncommon, 8, 40, 4, 4));

    ArmorDictionary[ArmorType.Feet][ArmorName.Cloth].Add(new Armor(ArmorType.Feet, ArmorName.Cloth, BuffType.None, ItemRarity.Rare, 3, 10, 1, 1));
    ArmorDictionary[ArmorType.Feet][ArmorName.Leather].Add(new Armor(ArmorType.Feet, ArmorName.Leather, BuffType.None, ItemRarity.Rare, 6, 20, 2, 2));
    ArmorDictionary[ArmorType.Feet][ArmorName.Mail].Add(new Armor(ArmorType.Feet, ArmorName.Mail, BuffType.None, ItemRarity.Rare, 9, 30, 3, 3));
    ArmorDictionary[ArmorType.Feet][ArmorName.Plate].Add(new Armor(ArmorType.Feet, ArmorName.Plate, BuffType.None, ItemRarity.Rare, 12, 40, 4, 4));

    ArmorDictionary[ArmorType.Feet][ArmorName.Cloth].Add(new Armor(ArmorType.Feet, ArmorName.Cloth, BuffType.None, ItemRarity.Epic, 4, 10, 1, 1));
    ArmorDictionary[ArmorType.Feet][ArmorName.Leather].Add(new Armor(ArmorType.Feet, ArmorName.Leather, BuffType.None, ItemRarity.Epic, 8, 20, 2, 2));
    ArmorDictionary[ArmorType.Feet][ArmorName.Mail].Add(new Armor(ArmorType.Feet, ArmorName.Mail, BuffType.None, ItemRarity.Epic, 12, 30, 3, 3));
    ArmorDictionary[ArmorType.Feet][ArmorName.Plate].Add(new Armor(ArmorType.Feet, ArmorName.Plate, BuffType.None, ItemRarity.Epic, 16, 40, 4, 4));

    ArmorDictionary[ArmorType.Feet][ArmorName.Cloth].Add(new Armor(ArmorType.Feet, ArmorName.Cloth, BuffType.None, ItemRarity.Legendary, 5, 10, 1, 1));
    ArmorDictionary[ArmorType.Feet][ArmorName.Leather].Add(new Armor(ArmorType.Feet, ArmorName.Leather, BuffType.None, ItemRarity.Legendary, 10, 20, 2, 2));
    ArmorDictionary[ArmorType.Feet][ArmorName.Mail].Add(new Armor(ArmorType.Feet, ArmorName.Mail, BuffType.None, ItemRarity.Legendary, 15, 30, 3, 3));
    ArmorDictionary[ArmorType.Feet][ArmorName.Plate].Add(new Armor(ArmorType.Feet, ArmorName.Plate, BuffType.None, ItemRarity.Legendary, 20, 40, 4, 4));

    ArmorDictionary[ArmorType.Hand][ArmorName.None].Add(new Armor(ArmorType.Hand, ArmorName.None, BuffType.None, ItemRarity.Poor, 0, 0, 0, 0));
    ArmorDictionary[ArmorType.Hand][ArmorName.Cloth].Add(new Armor(ArmorType.Hand, ArmorName.Cloth, BuffType.None, ItemRarity.Poor, 1, 10, 1, 1));
    ArmorDictionary[ArmorType.Hand][ArmorName.Leather].Add(new Armor(ArmorType.Hand, ArmorName.Leather, BuffType.None, ItemRarity.Poor, 2, 20, 2, 2));
    ArmorDictionary[ArmorType.Hand][ArmorName.Mail].Add(new Armor(ArmorType.Hand, ArmorName.Mail, BuffType.None, ItemRarity.Poor, 3, 30, 3, 3));
    ArmorDictionary[ArmorType.Hand][ArmorName.Plate].Add(new Armor(ArmorType.Hand, ArmorName.Plate, BuffType.None, ItemRarity.Poor, 4, 40, 4, 4));

    ArmorDictionary[ArmorType.Hand][ArmorName.Cloth].Add(new Armor(ArmorType.Hand, ArmorName.Cloth, BuffType.None, ItemRarity.Common, 1, 10, 1, 1));
    ArmorDictionary[ArmorType.Hand][ArmorName.Leather].Add(new Armor(ArmorType.Hand, ArmorName.Leather, BuffType.None, ItemRarity.Common, 2, 20, 2, 2));
    ArmorDictionary[ArmorType.Hand][ArmorName.Mail].Add(new Armor(ArmorType.Hand, ArmorName.Mail, BuffType.None, ItemRarity.Common, 3, 30, 3, 3));
    ArmorDictionary[ArmorType.Hand][ArmorName.Plate].Add(new Armor(ArmorType.Hand, ArmorName.Plate, BuffType.None, ItemRarity.Common, 4, 40, 4, 4));

    ArmorDictionary[ArmorType.Hand][ArmorName.Cloth].Add(new Armor(ArmorType.Hand, ArmorName.Cloth, BuffType.None, ItemRarity.Uncommon, 2, 10, 1, 1));
    ArmorDictionary[ArmorType.Hand][ArmorName.Leather].Add(new Armor(ArmorType.Hand, ArmorName.Leather, BuffType.None, ItemRarity.Uncommon, 4, 20, 2, 2));
    ArmorDictionary[ArmorType.Hand][ArmorName.Mail].Add(new Armor(ArmorType.Hand, ArmorName.Mail, BuffType.None, ItemRarity.Uncommon, 6, 30, 3, 3));
    ArmorDictionary[ArmorType.Hand][ArmorName.Plate].Add(new Armor(ArmorType.Hand, ArmorName.Plate, BuffType.None, ItemRarity.Uncommon, 8, 40, 4, 4));

    ArmorDictionary[ArmorType.Hand][ArmorName.Cloth].Add(new Armor(ArmorType.Hand, ArmorName.Cloth, BuffType.None, ItemRarity.Rare, 3, 10, 1, 1));
    ArmorDictionary[ArmorType.Hand][ArmorName.Leather].Add(new Armor(ArmorType.Hand, ArmorName.Leather, BuffType.None, ItemRarity.Rare, 6, 20, 2, 2));
    ArmorDictionary[ArmorType.Hand][ArmorName.Mail].Add(new Armor(ArmorType.Hand, ArmorName.Mail, BuffType.None, ItemRarity.Rare, 9, 30, 3, 3));
    ArmorDictionary[ArmorType.Hand][ArmorName.Plate].Add(new Armor(ArmorType.Hand, ArmorName.Plate, BuffType.None, ItemRarity.Rare, 12, 40, 4, 4));

    ArmorDictionary[ArmorType.Hand][ArmorName.Cloth].Add(new Armor(ArmorType.Hand, ArmorName.Cloth, BuffType.None, ItemRarity.Epic, 4, 10, 1, 1));
    ArmorDictionary[ArmorType.Hand][ArmorName.Leather].Add(new Armor(ArmorType.Hand, ArmorName.Leather, BuffType.None, ItemRarity.Epic, 8, 20, 2, 2));
    ArmorDictionary[ArmorType.Hand][ArmorName.Mail].Add(new Armor(ArmorType.Hand, ArmorName.Mail, BuffType.None, ItemRarity.Epic, 12, 30, 3, 3));
    ArmorDictionary[ArmorType.Hand][ArmorName.Plate].Add(new Armor(ArmorType.Hand, ArmorName.Plate, BuffType.None, ItemRarity.Epic, 16, 40, 4, 4));

    ArmorDictionary[ArmorType.Hand][ArmorName.Cloth].Add(new Armor(ArmorType.Hand, ArmorName.Cloth, BuffType.None, ItemRarity.Legendary, 5, 10, 1, 1));
    ArmorDictionary[ArmorType.Hand][ArmorName.Leather].Add(new Armor(ArmorType.Hand, ArmorName.Leather, BuffType.None, ItemRarity.Legendary, 10, 20, 2, 2));
    ArmorDictionary[ArmorType.Hand][ArmorName.Mail].Add(new Armor(ArmorType.Hand, ArmorName.Mail, BuffType.None, ItemRarity.Legendary, 15, 30, 3, 3));
    ArmorDictionary[ArmorType.Hand][ArmorName.Plate].Add(new Armor(ArmorType.Hand, ArmorName.Plate, BuffType.None, ItemRarity.Legendary, 20, 40, 4, 4));
  }

  private static void InitBandages()
  {
    Bandages.Add(new Bandage(BandageType.Cloth, 1, 1, 1, 0.1M));
    Bandages.Add(new Bandage(BandageType.Linen, 2, 1, 2, 0.2M));
    Bandages.Add(new Bandage(BandageType.Wool, 3, 1, 3, 0.3M));
    Bandages.Add(new Bandage(BandageType.Silk, 4, 1, 4, 0.4M));
    Bandages.Add(new Bandage(BandageType.Cotton, 5, 1, 5, 0.5M));
    Bandages.Add(new Bandage(BandageType.RuneCloth, 6, 1, 6, 0.6M));
  }

  private static void InitFoodTypes()
  {
    Foods.Add(BuffType.Health, new List<Food>());
    Foods[BuffType.Health].Add(new Food(FoodName.Ration, BuffType.Health, 1, 1, 0.1M));
    Foods[BuffType.Health].Add(new Food(FoodName.Vegetable, BuffType.Health, 1, 1, 0.1M));
    Foods[BuffType.Health].Add(new Food(FoodName.Fruit, BuffType.Health, 1, 1, 0.1M));
    Foods[BuffType.Health].Add(new Food(FoodName.BearSteak, BuffType.Health, 1, 1, 0.1M));
    Foods[BuffType.Health].Add(new Food(FoodName.Bread, BuffType.Health, 1, 1, 0.1M));
    Foods[BuffType.Health].Add(new Food(FoodName.WolfSteak, BuffType.Health, 1, 1, 0.1M));
    Foods[BuffType.Health].Add(new Food(FoodName.DeerSteak, BuffType.Health, 1, 1, 0.1M));
    Foods[BuffType.Health].Add(new Food(FoodName.BoarChop, BuffType.Health, 1, 1, 0.1M));

    Foods.Add(BuffType.ManaHeal, new List<Food>());
    Foods[BuffType.ManaHeal].Add(new Food(FoodName.Salmon, BuffType.ManaHeal, 1, 1, 0.1M));
    Foods[BuffType.ManaHeal].Add(new Food(FoodName.Trout, BuffType.ManaHeal, 1, 2, 0.1M));
    Foods[BuffType.ManaHeal].Add(new Food(FoodName.Snapper, BuffType.ManaHeal, 1, 4, 0.1M));
    Foods[BuffType.ManaHeal].Add(new Food(FoodName.Feast, BuffType.ManaHeal, 1, 5, 0.5M, ItemRarity.Uncommon));
      
    Foods.Add(BuffType.Mana, new List<Food>());
    Foods[BuffType.Mana].Add(new Food(FoodName.MelonJuice, BuffType.Mana, 1, 1, 0.1M));
    Foods[BuffType.Mana].Add(new Food(FoodName.FruitJuice, BuffType.Mana, 1, 1, 0.1M));
    Foods[BuffType.Mana].Add(new Food(FoodName.Water, BuffType.Mana, 1, 1, 0.1M));
    Foods[BuffType.Mana].Add(new Food(FoodName.Tea, BuffType.Mana, 1, 1, 0.1M));
    Foods[BuffType.Mana].Add(new Food(FoodName.Coffee, BuffType.Mana, 1, 1, 0.1M));
    Foods[BuffType.Mana].Add(new Food(FoodName.Milk, BuffType.Mana, 1, 1, 0.1M));
    Foods[BuffType.Mana].Add(new Food(FoodName.Wine, BuffType.Mana, 1, 1, 0.1M));
    Foods[BuffType.Mana].Add(new Food(FoodName.Beer, BuffType.Mana, 1, 1, 0.1M));
    Foods[BuffType.Mana].Add(new Food(FoodName.Ale, BuffType.Mana, 1, 1, 0.1M));
    Foods[BuffType.Mana].Add(new Food(FoodName.Whiskey, BuffType.Mana, 1, 1, 0.1M));
    Foods[BuffType.Mana].Add(new Food(FoodName.Cider, BuffType.Mana, 1, 1, 0.1M));
  }

  private static void InitSpellTypes()
  {
    // Heals
    Spells.Add(SpellName.Heal, new Spell(SpellName.Heal, "Heals the player", SpellType.Heal, 3, 3, 10));
    Spells.Add(SpellName.GreaterHeal, new Spell(SpellName.GreaterHeal, "Large Heal for the player", SpellType.Heal, 3, 3, 10));
    Spells.Add(SpellName.HealOverTime, new Spell(SpellName.HealOverTime, "Heals the player over time", SpellType.Heal, 3, 3, 10));

    // Damage
    Spells.Add(SpellName.Fireball, new Spell(SpellName.Fireball, "Shoots a ball of fire at the target", SpellType.Damage, 3, 3, 10));
    Spells.Add(SpellName.IceSpike, new Spell(SpellName.IceSpike, "Causes a Spike of Ice to appear at the feet of the target", SpellType.Damage, 3, 3, 10));
    Spells.Add(SpellName.LightningBolt, new Spell(SpellName.LightningBolt, "Shoots a bolt of lightning at the target", SpellType.Damage, 3, 3, 10));
    Spells.Add(SpellName.HolyBolt, new Spell(SpellName.HolyBolt, "Shoots a bolt of Holy energy at the target", SpellType.Damage, 3, 3, 10));
    Spells.Add(SpellName.PoisonBolt, new Spell(SpellName.PoisonBolt, "Shoots a bolt of poison at the target", SpellType.Damage, 3, 3, 10));
    Spells.Add(SpellName.FrostBolt, new Spell(SpellName.FrostBolt, "Shoots a bolt of frost at the target", SpellType.Damage, 3, 3, 1));
    Spells.Add(SpellName.ArcaneBolt, new Spell(SpellName.ArcaneBolt, "Shoots a bolt of arcane energy at the target", SpellType.Damage, 3, 3, 10));
    Spells.Add(SpellName.PoisonStorm, new Spell(SpellName.PoisonStorm, "Blasts an area with poison, damaging all targets within the area", SpellType.Damage, 3, 3, 10));
    Spells.Add(SpellName.FireStorm, new Spell(SpellName.FireStorm, "Blasts an area with fire, damaging all targets within the area", SpellType.Damage, 3, 3, 10));
    Spells.Add(SpellName.IceStorm, new Spell(SpellName.IceStorm, "Blasts an area with ice, damaging all targets within the area", SpellType.Damage, 3, 3, 10));
    Spells.Add(SpellName.LightningStorm, new Spell(SpellName.LightningStorm, "Blasts an area with lightning, damaging all targets within the area", SpellType.Damage, 3, 3, 10));
    Spells.Add(SpellName.FrostStorm, new Spell(SpellName.FrostStorm, "Blasts an area with frost, damaging all targets within the area", SpellType.Damage, 3, 3, 10));
    Spells.Add(SpellName.ArcaneStorm, new Spell(SpellName.ArcaneStorm, "Blasts an area with arcane energy, damaging all targets within the area", SpellType.Damage, 3, 3, 10));
    Spells.Add(SpellName.HolyStorm, new Spell(SpellName.HolyStorm, "Blasts an area with Holy energy, damaging all targets within the area", SpellType.Damage, 3, 3, 10));
    Spells.Add(SpellName.DrainLife, new Spell(SpellName.DrainLife, "Drains the life energy of the target", SpellType.Damage, 3, 3, 10));
    Spells.Add(SpellName.DrainMana, new Spell(SpellName.DrainMana, "Drains the mana of the target", SpellType.Damage, 3, 3, 10));

    // Buff
    Spells.Add(SpellName.Stamina, new Spell(SpellName.Stamina, "Increases your maximum health", SpellType.Buff, 3, 3, 10));
    Spells.Add(SpellName.GreaterStamina, new Spell(SpellName.GreaterStamina, "Increases your maximum health", SpellType.Buff, 6, 3, 10));
    Spells.Add(SpellName.Haste, new Spell(SpellName.Haste, "Get an extra attack in a turn", SpellType.Buff, 3, 3, 10));

    // Debuff
    Spells.Add(SpellName.SunderArmor, new Spell(SpellName.SunderArmor, "Shows any monsters on the map of the current level for a short time", SpellType.Debuff, 3, 3, 10));
    Spells.Add(SpellName.Slow, new Spell(SpellName.Slow, "Slows a monsters and prevents an attack for the next turn", SpellType.Debuff, 3, 3, 10));

    // Other
    Spells.Add(SpellName.ShowMap, new Spell(SpellName.ShowMap, "Shows the entire map of the current level for a short time", SpellType.Other, 3, 3, 10));
    Spells.Add(SpellName.ShowMonsters, new Spell(SpellName.ShowMap, "Shows any monsters on the map of the current level for a short time", SpellType.Other, 3, 3, 10));
  }

  private static void InitPotionTypes()
  {
    Potions.Add(BuffType.Health, new List<Potion>());
    Potions[BuffType.Health].Add(new Potion(BuffType.Health, ItemRarity.Common, 1, 1M, 0.1M));
    Potions[BuffType.Health].Add(new Potion(BuffType.Health, ItemRarity.Uncommon, 1, 5M, 0.5M));
    Potions[BuffType.Health].Add(new Potion(BuffType.Health, ItemRarity.Rare, 1, 10M, 1M));

    Potions.Add(BuffType.Mana, new List<Potion>());
    Potions[BuffType.Mana].Add(new Potion(BuffType.Mana, ItemRarity.Common, 1, 1M, 0.1M));
    Potions[BuffType.Mana].Add(new Potion(BuffType.Mana, ItemRarity.Uncommon, 1, 5M, 0.5M));
    Potions[BuffType.Mana].Add(new Potion(BuffType.Mana, ItemRarity.Rare, 1, 10M, 1M));

    Potions.Add(BuffType.ManaHeal, new List<Potion>());
    Potions[BuffType.ManaHeal].Add(new Potion(BuffType.ManaHeal, ItemRarity.Common, 1, 1M, 0.1M));
    Potions[BuffType.ManaHeal].Add(new Potion(BuffType.ManaHeal, ItemRarity.Uncommon, 1, 5M, .5M));
    Potions[BuffType.ManaHeal].Add(new Potion(BuffType.ManaHeal, ItemRarity.Rare, 1, 10M, 1M));
  }

  internal static void AddItem(Item item)
  {
    if (item.Type == ItemType.Gold)
    {
      Player.Gold += ((Gold)item).GetValue();
      return;
    }
    foreach (Bag bag in Bags)
    {
      if (bag.AddItem(item))
      {
        return;
      }
    }
    GamePlay.Messages.Add(new Message("Your bags are full", Color.Red, Color.Black));
  }

  internal static BuffType GetRandomBuffType(int min = 1)
  {
    int index = Dice.Roll(min, Enum.GetNames(typeof(BuffType)).Length - 1);
    return (BuffType)index;
  }

  internal static bool RemoveItem(Item item)
  {
    foreach (Bag bag in Bags)
    {
      if (bag.RemoveItem(item)) return true;
    }
    return false;
  }

  internal static void RemoveAllItems()
  {
    foreach (Bag bag in Bags)
    {
      bag.Items.Clear();
    }
  }

  internal static int GetQuantity(Item item)
  {
    int quantity = 0;
    foreach (Bag bag in Bags)
    {
      quantity += bag.GetQuantity(item);
    }
    return quantity;
  }

  private static bool HasItem(Item item)
  {
    foreach(Bag bag in Bags)
    {
      if (bag.GetQuantity(item) > 0) return true;
    }
    return false;
  }

  internal static bool HasItems(List<Item> items)
  {
    foreach (Item item in items)
      if (!HasItem(item)) return false;

    return true;
  }

  internal static Item GetRandomItem()
  {
    int itemIdx = Dice.Roll(1, Enum.GetNames(typeof(ItemType)).Length - 1);
    Item item = GetRandomItem((ItemType)itemIdx);
    return item;
  }

  private static Item GetRandomItem(ItemType type)
  {
    //default result is gold, so set the quantity and value
    switch (type)
    {
      case ItemType.Weapon:
        return Weapon.GetRandomItem();
      case ItemType.Potion:
        return Potion.GetRandomItem();
      case ItemType.Food:
        return Food.GetRandomItem();
      case ItemType.Gold:
        return Gold.GetRandomItem();
      case ItemType.Armor:
        return Armor.GetRandomItem();
      case ItemType.Chest:
        return Chest.GetRandomItem();
      case ItemType.Bandage:
        return Bandage.GetRandomItem();
      default:  
        return Gold.GetRandomItem();
    }
  }
}