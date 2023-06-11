using ConsoleDungeonCrawler.Extensions;
using System.Drawing;

namespace ConsoleDungeonCrawler.Game.Screens
{
  internal static class Dialog
  {
    // This is the default box for the generic dialog box
    internal static Box Box = new Box(Console.WindowWidth / 2 - 52, Console.WindowHeight / 2 - 12, 100, 25);
    
    // These are the default box characters for all dialog boxes
    internal static BoxChars BChars = new BoxChars() { botLeft = '=', botRight = '=', topRight = '=', topLeft = '=', hor = '=', ver = '|' };
    
    // These colors are for the default theme of a dialog box
    internal static Color BackgroundColor = Color.Black;
    internal static Color ForegroundColor = Color.DarkOrange;
    internal static Color FillColor = Color.Olive;
    internal static Color TextColor = Color.Bisque;

    /// <summary>
    /// Generic Dialog box with a title everything else is default
    /// </summary>
    /// <param name="title"></param>
    internal static void Draw(string title)
    {
      Box.Draw(BChars, ForegroundColor, BackgroundColor, FillColor);
      ConsoleEx.WriteAlignedAt(Box, $"[{title}]", HAlign.Center, VAlign.Top, TextColor, BackgroundColor, 0, -1);
    }


    /// <summary>
    /// Generic Dialog box with a title, and default colors
    /// </summary>
    /// <param name="title"></param>
    /// <param name="box"></param>
    /// <param name="bchars"></param>
    internal static void Draw(string title, Box? box = null, BoxChars? bchars = null)
    {
      box ??= Box;
      bchars ??= BChars;
      box.Draw(bchars, ForegroundColor, BackgroundColor, FillColor);
      ConsoleEx.WriteAlignedAt(box, $"[{title}]", HAlign.Center, VAlign.Top, Color.Bisque, Color.Black, 0, -1);
    }

    /// <summary>
    /// Draw a box with a title, custom box, and custom colors
    /// </summary>
    /// <param name="title"></param>
    /// <param name="color"></param>
    /// <param name="backgroundColor"></param>
    /// <param name="fillColor"></param>
    /// <param name="box"></param>
    /// <param name="bchars"></param>
    internal static void Draw(string title, Color color, Color backgroundColor, Color fillColor, Color textColor, Box? box = null, BoxChars? bchars = null)
    {
      box ??= Box;
      bchars ??= BChars;
      box.Draw(bchars, color, backgroundColor, fillColor);
      ConsoleEx.WriteAlignedAt(box, $"[{title}]", HAlign.Center, VAlign.Top, textColor, backgroundColor, 0, -1);
    }

    internal static void AskForInt(string question, string prompt, out int result)
    {
      int width = (question.Length > prompt.Length ? question.Length : prompt.Length);

      Box box = new Box(Console.WindowWidth / 2 - (width + 8) / 2, Console.WindowHeight / 2 - 3, width + 8, 5);
      Draw(question, Color.DarkOrange, Color.Black, Color.Black, Color.Bisque, box);
      ConsoleEx.WriteAlignedAt(box, prompt, HAlign.Center, VAlign.Middle, Color.Bisque, Color.Black, - 1, 0);
      result = ConsoleEx.ReadInt(Console.GetCursorPosition().Left, Console.GetCursorPosition().Top, Color.White, Color.Black);
    }

    internal static void Confirm(string question, string prompt, out bool result)
    {
      int width = (question.Length > prompt.Length ? question.Length : prompt.Length);

      Box box = new Box(Console.WindowWidth / 2 - (width + 8) / 2, Console.WindowHeight / 2 - 3, width + 8, 5);
      Draw(question, Color.DarkOrange, Color.Black, Color.Black, Color.Bisque, box);
      ConsoleEx.WriteAlignedAt(box, prompt, HAlign.Center, VAlign.Middle, Color.Bisque, Color.Black, -1, 0);
      result = ConsoleEx.ReadBool(Console.GetCursorPosition().Left, Console.GetCursorPosition().Top, Color.White, Color.Black);
    }

    internal static void Notify(string title, string message)
    {
      Box box = new Box(Console.WindowWidth / 2 - (message.Length + 6) / 2, Console.WindowHeight / 2 - 4, message.Length + 10, 7);
      Draw(title, Color.DarkOrange, Color.Black, Color.Black, Color.Bisque, box);
      ConsoleEx.WriteAlignedAt(Box, message, HAlign.Center, VAlign.Middle, Color.Bisque, Color.Black, 0, - 1);
      ConsoleEx.WriteAlignedAt(Box, "Press any key to continue", HAlign.Center, VAlign.Middle, Color.Bisque, Color.Black, 0, 1);
      Console.ReadKey(true);
    }

    internal static void Close()
    {
      Game.IsPaused = false;
      ConsoleEx.Clear();
      GamePlay.Draw();
    }
  }
}
