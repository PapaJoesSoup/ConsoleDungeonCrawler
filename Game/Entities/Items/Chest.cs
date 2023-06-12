namespace ConsoleDungeonCrawler.Game.Entities.Items
{
  internal class Chest : Item
  {
    internal ItemRarity ItemRarity = ItemRarity.Common;
    internal List<Item> Items = new List<Item>();

    internal Chest()
    {
      Type = ItemType.Chest;
      Name = $"{ItemRarity} Chest";
      Description = $"A {ItemRarity} Chest";
      Quantity = 1;
      StackSize = 1;
      BuyCost = 1M;
      SellCost = 0.10M;

      Items = GetChestItems();
    }

    internal Chest(int quantity, ItemRarity rarity, decimal buyCost, decimal sellCost)
    {
      Type = ItemType.Chest;
      Name = "Chest";
      Description = "A Chest";
      ItemRarity = rarity;
      Quantity = quantity;
      StackSize = 1;
      BuyCost = buyCost;
      SellCost = sellCost;

      Items = GetChestItems();
    }

    internal static Chest GetRandomChest(int min = 0, int max = 5)
    {
      if (min < 0) min = 0;
      if (max > 5) max = 5;
      int randomChest = Dice.Roll(min, max);
      switch (randomChest)
      {
        case 0:
          return new Chest(1, ItemRarity.Poor, 0.5M, 0.1M);
        case 1:
          return new Chest(1, ItemRarity.Common, 1, 0.5M);
        case 2:
          return new Chest(1, ItemRarity.Uncommon, 5, 1M);
        case 3:
          return new Chest(1, ItemRarity.Rare, 10, 1.5M);
        case 4:
          return new Chest(1, ItemRarity.Epic, 20, 4M);
        case 5:
          return new Chest(1, ItemRarity.Legendary, 50, 7.5M);
        default:
          return new Chest(1, ItemRarity.Common, 1, 0.5M);
      }
    }

    private List<Item> GetChestItems()
    {
      int randomItems = Dice.Roll(0, 10);
      Item randomGold = new Item(ItemType.Gold, Player.Level, Dice.Roll(1, 10), 1, 1);
      switch (randomItems)
      {
        case 0:
          return new List<Item>() { Armor.GetRandomArmor(), Potion.GetRandomPotion(), randomGold };
        case 1:
          return new List<Item>() { Bandage.GetRandomBandage(), randomGold };
        case 2:
          return new List<Item>() { Weapon.GetRandomWeapon(),Potion.GetRandomPotion(), Bandage.GetRandomBandage(), randomGold };
        case 3:
          return new List<Item>() { Food.GetRandomFood(), Food.GetRandomFood(), Potion.GetRandomPotion(), Bandage.GetRandomBandage(), randomGold };
        case 4:
          return new List<Item>() { Armor.GetRandomArmor(), Bandage.GetRandomBandage(), randomGold };
        case 5:
          return new List<Item>() { Potion.GetRandomPotion(), Food.GetRandomFood(), randomGold };
        case 6:
          return new List<Item>() { Armor.GetRandomArmor(), Weapon.GetRandomWeapon(), randomGold };
        case 7:
          return new List<Item>() { Armor.GetRandomArmor(), Weapon.GetRandomWeapon(), Potion.GetRandomPotion(), Bandage.GetRandomBandage(), randomGold };
        case 8:
          return new List<Item>() { Food.GetRandomFood(), Potion.GetRandomPotion(), Potion.GetRandomPotion(), randomGold };
        case 9:
          return new List<Item>() { Potion.GetRandomPotion(), Bandage.GetRandomBandage(), randomGold };
        case 10:
          return new List<Item>() { Potion.GetRandomPotion(), Armor.GetRandomArmor(), randomGold };
         default:
          return new List<Item>() { Potion.GetRandomPotion(), randomGold };
      }
    }

  }
}
