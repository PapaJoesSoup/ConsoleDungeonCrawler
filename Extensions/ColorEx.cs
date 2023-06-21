using System.Drawing;
using ConsoleDungeonCrawler.Game;

namespace ConsoleDungeonCrawler.Extensions
{
  internal static class ColorEx
  {
    // Setup console to use extended ansi 256 colors
    private static readonly string BgColor = "\x1b[48;5;";
    internal static readonly string FgColor = "\x1b[38;5;";

    // Setup console to use ansi 24 bit rgb colors
    private static readonly string BgHiColor = "\x1b[48;2;";
    private static readonly string FgHiColor = "\x1b[38;2;";

    // Reset console colors
    internal static readonly string ResetColor = "\x1b[0m";

    static ColorEx()
    {
    }

    internal static string ToFgHiColor(Color color)
    {
      return $"{FgHiColor}{ToRgb(color)}m";
    }

    internal static string ToBgHiColor(Color color)
    {
      return $"{BgHiColor}{ToRgb(color)}m";
    }

    private static string ToRgb(Color color)
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

    private static void Test256Colors()
    {
      // Print all 256 colors
      for (int i = 0; i < 255; i++)
      {
        Console.Write($"{ColorEx.BgColor}{i}m*");
      }
    }

    private static void Test24BitColors()
    {
      //Print all 24 bit rgb colors
      for (int r = 0; r < 255; r++)
      {
        for (int g = 0; g < 255; g++)
        {
          for (int b = 0; b < 255; b++)
          {
            Console.Write($"{ColorEx.BgHiColor}{r};{g};{b}m*");
          }
        }
      }
    }
  }
}
