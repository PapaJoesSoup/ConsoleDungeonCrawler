using System.Drawing;
using ConsoleDungeonCrawler.Game;

namespace ConsoleDungeonCrawler.Extensions;

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
}