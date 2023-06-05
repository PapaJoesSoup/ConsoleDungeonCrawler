namespace ConsoleDungeonCrawler.Game
{
  // enums for the game

  internal enum Direction
  {
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
    Chainmail,
    Plate
  }

  internal enum WeaponType
  {
    Sword,
    Axe,
    Mace,
    Bow,
    Staff,
    Dagger,
    Wand,
    Fists
  }

  internal enum MonsterType
  {
    Humanoid,
    Beast,
    Undead,
    Demon,
    Dragon
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
    Food
  }

  internal enum ItemRarity
  {
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
  }

  internal enum PotionType
  {
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
