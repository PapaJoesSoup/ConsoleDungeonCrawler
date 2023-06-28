using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Extensions;

/// <summary>
/// This is the class for Extended Box Characters
/// </summary>
internal class BoxChars
{
  internal readonly string TopLeft;
  internal readonly string TopRight;
  internal readonly string BotLeft;
  internal readonly string BotRight;
  internal readonly string Hor;
  internal readonly string Ver;
  internal readonly string MidLeft;
  internal readonly string MidRight;
  internal readonly string MidTop;
  internal readonly string MidBottom;
  internal readonly string Mid;

  internal BoxChars(string topLeft, string topRight, string botLeft, string botRight, string hor, string ver,
    string midLeft, string midRight, string midTop, string midBottom, string mid)
  {
    TopLeft = topLeft;
    TopRight = topRight;
    BotLeft = botLeft;
    BotRight = botRight;
    Hor = hor;
    Ver = ver;
    MidLeft = midLeft;
    MidRight = midRight;
    MidTop = midTop;
    MidBottom = midBottom;
    Mid = mid;
  }
}