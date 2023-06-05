namespace ConsoleDungeonCrawler.Game.Entities
{
  internal class Spell
  {
    internal SpellName Name = SpellName.None;
    internal string Description = "";
    internal DamageType DamageType = DamageType.Magical;
    internal int Damage = 0;
    internal int ManaCost = 0;
  }
}
