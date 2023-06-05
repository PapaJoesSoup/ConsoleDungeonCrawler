namespace ConsoleDungeonCrawler.Game.Entities.Items
{
  internal class Weapon : Item
  {
    internal WeaponType WeaponType = WeaponType.Fists;
    internal int Damage = 1;
    internal int Durability = 100;
    internal int MaxDurability = 100;
    internal int Range = 1;
  }

}
