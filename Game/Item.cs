namespace ConsoleDungeonCrawler.Game
{
  internal class Item
  {
    public int id;
    public ItemName name;
    public string Description = "";
    public ItemEffect Effect;
    public ItemRarity rarity;
    public int Quantity = 1;
    public int Cost;
    public int Value;

    public Item() { }

    public Item(int id, ItemName name, string description, ItemEffect effect, ItemRarity rarity, int qty, int cost, int value)
    {
      this.id = id;
      this.name = name;
      this.Description = description;
      this.Effect = effect;
      this.rarity = rarity;
      this.Quantity = qty;
      this.Cost = cost;
      this.Value = value;
    }

    public void UseItem()
    {
      switch (Effect)
      {
        case ItemEffect.Heal:
          Player.Health += Value;
          break;
        case ItemEffect.Mana:
          Player.Mana += Value;
          break;
        case ItemEffect.Damage:
          Player.Health -= Value;
          break;
        case ItemEffect.Armor:
          Player.ArmorSet[0].ArmorBonus += Value;
          break;
        case ItemEffect.None:
        default:
          break;
      }
    }
  }
}
