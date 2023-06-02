namespace ConsoleDungeonCrawler.Game.Entities
{
    internal class Armor
    {
        internal ArmorType Type = ArmorType.None;
        internal ArmorName Name = ArmorName.None;
        internal ItemRarity Rarity = ItemRarity.Common;
        internal int ArmorValue = 0;
        internal int ArmorBonus = 0;
        internal int Durability = 0;
        internal int MaxDurability = 0;

        internal Armor()
        {
        }

        internal Armor(ArmorType type)
        {
            Type = type;
        }

        internal Armor(ArmorType type, ArmorName name, ItemRarity rarity, int armorValue, int durability)
        {
            Type = type;
            Name = name;
            Rarity = rarity;
            ArmorValue = armorValue;
            Durability = durability;
            MaxDurability = durability;
        }
    }

}
