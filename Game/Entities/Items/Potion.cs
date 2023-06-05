using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Game.Entities.Items
{
  internal class Potion : Item
  {
    internal PotionType PotionType = PotionType.Health;
    internal Potion() { }
  }
}
