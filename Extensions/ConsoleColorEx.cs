using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Extensions
{
  internal static class ConsoleColorEx
  {
    // Setup console to use extended ansii 256 colors
    internal static string bgColor = "\x1b[48;5;";
    internal static string fgColor = "\x1b[38;5;";

    // Setup console to use ansii 24 bit rgb colors
    internal static string bgHiColor = "\x1b[48;2;";
    internal static string fgHiColor = "\x1b[38;2;";

    // Reset console colors
    internal static string resetColor = "\x1b[0m";

    static ConsoleColorEx()
    {
    }

    internal static string ToFgHiColor(Color color)
    {
      return $"{fgHiColor}{ToRGB(color)}m";
    }

    internal static string ToBgHiColor(Color color)
    {
      return $"{bgHiColor}{ToRGB(color)}m";
    }
    internal static string ToRGB(Color color)
    {
      return $"{color.R};{color.G};{color.B}";
    }
  }
}
