namespace ConsoleDungeonCrawler.Game.Entities
{
  internal class Spell
  {
    internal readonly SpellName Name = SpellName.Heal;
    internal readonly string Description = "";
    internal readonly SpellType Type = SpellType.Heal;
    internal readonly int TypeAmount = 1;
    internal readonly int ManaCost = 1;
    internal int LevelLearned = 1;

    internal Spell(SpellName name, string description, SpellType type, int typeAmount, int manaCost, int levellearned)
    {
      Name = name;
      Description = description;
      Type = type;
      TypeAmount = typeAmount;
      ManaCost = manaCost;
      LevelLearned = levellearned;
    }

    internal Spell()
    {

    }

    internal void Cast()
    {
      switch (Type)
      {
        case SpellType.Heal:
          Player.Heal(TypeAmount);
          break;
        case SpellType.Damage:
          Player.UseSpell(this);
          break;
        case SpellType.Mana:
          Player.Mana += TypeAmount;
          break;
        case SpellType.Buff:
          break;
        case SpellType.Debuff:
          break;
        case SpellType.Other:
          break;
      }
    }
  }
}
