namespace ConsoleDungeonCrawler.Game.Entities
{
    internal class Weapon
    {
        internal WeaponName Name = WeaponName.Fists;
        internal DamageType DamageType = DamageType.Physical;
        internal DamageEffectAmount DamageAmount = DamageEffectAmount.One;
        internal List<DamageEffect> DamageEffects = new List<DamageEffect>();
        internal int Durability = 0;
        internal int MaxDurability = 0;
    }

}
