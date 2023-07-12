namespace ConsoleDungeonCrawler.Game.Entities
{
    internal class Theme
  {
    internal string Name = "NewTheme";
    internal Colors Colors = Colors.Default;
    internal BoxChars BoxChars = BoxChars.Default;

    internal static BoxChars DefaultBoxChars = BoxChars.Default;
    internal static Colors DefaultColors = Colors.Default;
    internal static Theme DefaultTheme = new Theme("DefaultTheme", DefaultColors, DefaultBoxChars);
    internal static Dictionary<string, Theme> Themes = new Dictionary<string, Theme>();

    internal Theme(){}

    internal Theme(string name, Colors colors, BoxChars boxChars)
    {
      Name = name;
      Colors = colors;
      BoxChars = boxChars;
    }

    internal static void AddTheme(Theme theme)
    {
      Themes.Add(theme.Name, theme);
    }

    internal static void AddTheme(string name, Colors colors, BoxChars boxChars)
    {
      Themes.Add(name, new Theme(name, colors, boxChars));
    }

    internal static void RemoveTheme(string name)
    {
      Themes.Remove(name);
    }

    internal static void RemoveTheme(Theme theme)
    {
      Themes.Remove(theme.Name);
    }

  }
}
