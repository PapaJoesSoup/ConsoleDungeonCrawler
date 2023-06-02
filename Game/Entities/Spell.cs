namespace ConsoleDungeonCrawler.Game.Entities
{
  internal class Spell
  {
    internal int id = 0;
    internal SpellName Name = SpellName.None;
    internal string Description = "";
    internal DamageType DamageType = DamageType.Magical;
    internal DamageEffectAmount DamageAmount = DamageEffectAmount.One;
    internal List<DamageEffect> DamageEffects = new List<DamageEffect>();
    internal int ManaCost = 0;
    internal int MaxManaCost = 0;
  }
}
