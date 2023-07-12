using System.Drawing;

namespace ConsoleDungeonCrawler.Game.Entities;

internal class Colors
{
  internal Color Color = Color.DarkOrange;
  internal Color BackgroundColor = Color.Black;
  internal Color FillColor = Color.SaddleBrown;
  internal Color HeaderColor = Color.Yellow;
  internal Color TextColor = Color.Bisque;
  internal Color SelectedColor = Color.Lime;
  internal Color SelectedBackgroundColor = Color.DarkOrange;

  // Number of properties.  Useful in ThemeConfig.
  internal const int Count = 7;

  internal static readonly Colors Default = new ();

  internal Colors()
  {
  }

  internal Colors(Color color, Color backgroundColor, Color fillColor, Color headerColor, Color textColor,
    Color selectedColor, Color selectedBackgroundColor)
  {
    Color = color;
    BackgroundColor = backgroundColor;
    FillColor = fillColor;
    HeaderColor = headerColor;
    TextColor = textColor;
    SelectedColor = selectedColor;
    SelectedBackgroundColor = selectedBackgroundColor;
  }
}