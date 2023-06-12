using System.Drawing;
using ConsoleDungeonCrawler.Game;

namespace ConsoleDungeonCrawler.Extensions
{
  internal static class ColorEx
  {
    // Setup console to use extended ansii 256 colors
    internal static string bgColor = "\x1b[48;5;";
    internal static string fgColor = "\x1b[38;5;";

    // Setup console to use ansii 24 bit rgb colors
    internal static string bgHiColor = "\x1b[48;2;";
    internal static string fgHiColor = "\x1b[38;2;";

    // Reset console colors
    internal static string resetColor = "\x1b[0m";

    static ColorEx()
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

    internal static Color RarityColor(ItemRarity rarity)
    {
      switch (rarity)
      {
        case ItemRarity.Poor:
          return Color.Gray;
        case ItemRarity.Common:
          return Color.White;
        case ItemRarity.Uncommon:
          return Color.Green;
        case ItemRarity.Rare:
          return Color.Blue;
        case ItemRarity.Epic:
          return Color.Purple;
        case ItemRarity.Legendary:
          return Color.Orange;
        default:
          return Color.White;
      }
    }

    private static void PrintColorsEx()
    {
      // Print all 256 colors
      for (int i = 0; i < 255; i++)
      {
        Console.Write($"{ColorEx.bgColor}{i}m*");
      }

      // Print all 24 bit rgb colors
      //for (int r = 0; r < 255; r++)
      //{
      //  for (int g = 0; g < 255; g++)
      //  {
      //    for (int b = 0; b < 255; b++)
      //    {
      //      Console.Write($"{ConsoleColorEx.bgHiColor}{r};{g};{b}m*");
      //    }
      //  }
      //}
    }

  }
}
