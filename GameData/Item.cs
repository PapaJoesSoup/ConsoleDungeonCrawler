using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.GameData
{
  internal class Item
  {
    public int id;
    public ItemName name;
    public string Description = "";
    public ItemEffect Effect;
    public ItemRarity rarity;
    public int Cost;
    public int Value;

    public Item() { }

    public Item(int id, ItemName name, string description, ItemEffect effect, ItemRarity rarity, int cost, int value)
    {
      this.id = id;
      this.name = name;
      this.Description = description;
      this.Effect = effect;
      this.rarity = rarity;
      this.Cost = cost;
      this.Value = value;
    }
  }
}
