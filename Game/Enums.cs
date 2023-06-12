﻿namespace ConsoleDungeonCrawler.Game
{
  // enums for the game

  internal enum Direction
  {
    None,
    North,
    East,
    South,
    West
  }

  internal enum PlayerClass
  {
    Rogue,
    Warrior,
    Mage,
    Priest,
    Hunter
  }

  internal enum ArmorType
  {
    None,
    Head,
    Body,
    Hands,
    Legs,
    Feet
  }

  internal enum ArmorName
  {
    None,
    Cloth,
    Leather,
    ChainMail,
    Plate
  }

  internal enum WeaponType
  {
    Fists,
    Sword,
    Axe,
    Mace,
    Staff,
    Dagger,
    Bow,
    Wand
  }

  internal enum MonsterName
  {
    None,
    Goblin,
    Orc,
    Skeleton,
    Zombie,
    Ghoul,
    Imp,
    Demon,
    Dragon
  }

  internal enum ItemType
  {
    None,
    Potion,
    Weapon,
    Armor,
    Gold,
    Bandage,
    Food,
    Chest
  }

  internal enum ItemRarity
  {
    Poor,
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
  }

  internal enum FoodType
  {
    Ration,
    BearSteak,
    WolfSteak,
    DeerSteak,
    BoarChop,
    Salmon,
    Trout,
    Snapper,
    Feast,
    Bread,
    Fruit,
    Vegetable,
    MelonJuice,
    FruitJuice,
    Water,
    Tea,
    Coffee,
    Milk,
    Wine,
    Beer,
    Ale,
    Whiskey,
    Cider
  }

  internal enum BandageType
  {
    Cloth,
    Linen,
    Silk,
    Wool,
    Cotton,
    RuneCloth
  }

  internal enum BuffType
  {
    None,
    Health,
    Mana,
    HealthAndMana
  }

  internal enum DamageType
  {
    Physical,
    Magical
  }

  internal enum SpellName
  {
    None,
    Fireball,
    IceSpike,
    LightningBolt,
    PoisonCloud,
    HolyBlast,
    DarkBlast
  }

  internal enum SpellCategory
  {
    Fire,
    Ice,
    Lightning,
    Poison,
    Holy,
    Dark
  }

  internal enum DamageElement
  {
    None,
    Fire,
    Ice,
    Lightning,
    Poison,
    Holy,
    Dark
  }

  public enum HAlign
  {
    Left,
    Right,
    Center
  }

  public enum VAlign
  {
    Top,
    Bottom,
    Middle
  }

  public enum MapSymbol
  {
    Empty = ' ',
    Wall = '#',
    Floor = '.',
    DoorO = '-',
    DoorC = '+',
    StairsU = '^',
    StairsD = 'v',
    Fire = '!',
    Water = '~',
    Acid = 'A',
    Lava = 'L',
    Ice = 'I'
  }

  public enum OverlaySymbol
  {
    Empty = ' ',
    Start = 'S',
    Exit = 'E',
    Player = 'P',
    Ogre = 'O',
    Kobald = 'k',
    Ooze = 'z',
    Goblin = 'g',
    Chest = 'm',
    Item = 'i',
    Gold = '$',
    Teleporter = 'T',
    Trap = 'x'
  }

}