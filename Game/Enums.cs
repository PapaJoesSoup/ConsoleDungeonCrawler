namespace ConsoleDungeonCrawler.Game
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

  internal enum SpellType
  {
    Heal,
    Mana,
    Damage,
    Buff,
    Debuff,
    Other
  }

  internal enum SpellName
  {
    Heal,
    GreaterHeal,
    HealOverTime,
    Fireball,
    IceSpike,
    LightningBolt,
    FrostBolt,
    ArcaneBolt,
    PoisonBolt,
    HolyStorm,
    HolyBolt,
    IceStorm,
    LightningStorm,
    FireStorm,
    FrostStorm,
    ArcaneStorm,
    PoisonStorm,
    DrainLife,
    DrainMana,
    ShowMap,
    ShowMonsters,
    SunderArmor,
    Stamina,
    GreaterStamina,
    Slow,
    Haste,
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
}
