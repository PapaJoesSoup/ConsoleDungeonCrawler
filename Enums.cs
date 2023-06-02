namespace ConsoleDungeonCrawler
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
    Fist
  }

  internal enum WeaponName
  {
    None,
    ShortSword,
    LongSword,
    BattleAxe,
    WarAxe,
    MorningStar,
    WarHammer,
    LongBow,
    ShortBow,
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

  internal enum ItemName
  {
    None,
    HealthPotion,
    ManaPotion,
    StrengthPotion,
    DexterityPotion,
    IntelligencePotion,
    WisdomPotion,
    ConstitutionPotion,
    CharismaPotion,
    StrengthScroll,
    DexterityScroll,
    IntelligenceScroll,
    WisdomScroll,
    ConstitutionScroll,
    CharismaScroll
  }

  internal enum ItemRarity
  {
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
  }

  internal enum ItemCategory
  {
    Potion,
    Scroll
  }

  internal enum ItemEffect
  {
    None,
    Heal,
    Mana,
    Damage,
    Armor
  }

  internal enum ItemEffectType
  {
    Instant,
    OverTime
  }

  internal enum ItemEffectDuration
  {
    None,
    OneTurn,
    TwoTurns,
    ThreeTurns,
    FourTurns,
    FiveTurns,
    SixTurns,
    SevenTurns,
    EightTurns,
    NineTurns,
    TenTurns
  }


  internal enum ItemEffectAmount
  {
    None,
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten
  }

  internal enum ItemEffectInterval
  {
    None,
    OneTurn,
    TwoTurns,
    ThreeTurns,
    FourTurns,
    FiveTurns,
    SixTurns,
    SevenTurns,
    EightTurns,
    NineTurns,
    TenTurns
  }

  internal enum ItemEffectIntervalAmount
  {
    None,
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten
  }

  internal enum ItemEffectIntervalDuration
  {
    None,
    OneTurn,
    TwoTurns,
    ThreeTurns,
    FourTurns,
    FiveTurns,
    SixTurns,
    SevenTurns,
    EightTurns,
    NineTurns,
    TenTurns
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


  internal enum DamageEffect
  {
    None,
    Bleed,
    Burn,
    Freeze,
    Shock,
    Poison,
    Blind,
    Stun,
    Silence,
    Curse,
    Slow,
    Confuse,
    Fear,
    Charm,
    Sleep,
    Paralyze,
    Drain,
    Lifesteal,
    ManaSteal,
    HealthSteal,
    ManaDrain,
    HealthDrain
  }

  internal enum DamageEffectType
  {
    Instant,
    OverTime
  }

  internal enum DamageEffectDuration
  {
    None,
    OneTurn,
    TwoTurns,
    ThreeTurns,
    FourTurns,
    FiveTurns,
    SixTurns,
    SevenTurns,
    EightTurns,
    NineTurns,
    TenTurns
  }

  internal enum DamageEffectAmount
  {
    None,
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten
  }


  internal enum DamageEffectInterval
  {
    None,
    OneTurn,
    TwoTurns,
    ThreeTurns,
    FourTurns,
    FiveTurns,
    SixTurns,
    SevenTurns,
    EightTurns,
    NineTurns,
    TenTurns
  }

  internal enum DamageEffectIntervalAmount
  {
    None,
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten
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
