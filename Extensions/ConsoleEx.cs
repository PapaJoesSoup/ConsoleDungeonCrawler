using System.Drawing;
using System.Globalization;
using ConsoleDungeonCrawler.GameData;
using ConsoleDungeonCrawler.Extensions;

namespace ConsoleDungeonCrawler.Extensions
{
    internal static class ConsoleEx
  {
    internal static int ScreenHeight;
    internal static int ScreenWidth;

    private static Color foregroundColor = Color.Gray;
    internal static Color ForegroundColor
    {
      get => foregroundColor;
      set
      {
        foregroundColor = value;
        Console.Write(ConsoleColorEx.ToFgHiColor(value) );
      }
    }

    private static Color backgroundColor = Color.Black;
    internal static Color BackgroundColor
    {
      get => backgroundColor;
      set
      {
        backgroundColor = value;
        Console.Write(ConsoleColorEx.ToBgHiColor(value));
      }
    }

    internal static void InitializeConsole()
    {
      Console.CursorVisible = false;
      Console.Title = Game.Title;
    }

    internal static void WriteLegendItem(MapObject mapObject, int col, int row, int width)
    {
      // create a formatted line containing the symbol and the name of the map object
      int padding = width - mapObject.Type.Name.Length - 5;
      int paddingStart = col + mapObject.Type.Name.Length + 3 ;
      WriteAt(' ', col, row, Color.White, Color.DimGray);
      WriteAt(mapObject.Type.Symbol.ToString(), col + 1, row, mapObject.Type.ForegroundColor, mapObject.Type.BackgroundColor );
      WriteAt(':', col + 2,row,Color.White, Color.DimGray);
      WriteAt(mapObject.Type.Name, col + 3, row, mapObject.Type.ForegroundColor, Color.DimGray);
      WriteAt(new string(' ', padding), paddingStart, row, Color.White, Color.DimGray);
    }

    // WriteAlignedAt Methods
    internal static void WriteAlignedAt(string s, HAlign hAlign)
    {
      int x = hAlign switch
      {
        HAlign.Left => 0,
        HAlign.Center => (Console.WindowWidth / 2) - (s.Length / 2),
        HAlign.Right => Console.WindowWidth - s.Length,
        _ => 0
      };
      WriteAt(s, x, Console.CursorTop);
    }

    internal static void WriteAlignedAt(string s, HAlign hAlign, ConsoleColor color)
    {
      int x = 0;
      switch (hAlign)
      {
        case HAlign.Left:
          x = 0;
          break;
        case HAlign.Center:
          x = (Console.WindowWidth / 2) - (s.Length / 2);
          break;
        case HAlign.Right:
          x = Console.WindowWidth - s.Length;
          break;
      }
      WriteAt(s, x, Console.CursorTop, color);
    }

    internal static void WriteAlignedAt(string s, HAlign hAlign, int y)
    {
      int x = 0;
      switch (hAlign)
      {
        case HAlign.Left:
          x = 0;
          break;
        case HAlign.Center:
          x = (Console.WindowWidth / 2) - (s.Length / 2);
          break;
        case HAlign.Right:
          x = Console.WindowWidth - s.Length;
          break;
      }
      WriteAt(s, x, y);
    }

    internal static void WriteAlignedAt(string s, HAlign hAlign, int y, ConsoleColor color)
    {
      int x = 0;
      switch (hAlign)
      {
        case HAlign.Left:
          x = 0;
          break;
        case HAlign.Center:
          x = (Console.WindowWidth / 2) - (s.Length / 2);
          break;
        case HAlign.Right:
          x = Console.WindowWidth - s.Length;
          break;
      }
      WriteAt(s, x, y, color);
    }

    internal static void WriteAlignedAt(string s, HAlign hAlign, int y, ConsoleColor color,
      ConsoleColor backgroundColor)
    {
      int x = 0;
      switch (hAlign)
      {
        case HAlign.Left:
          x = 0;
          break;
        case HAlign.Center:
          x = (Console.WindowWidth / 2) - (s.Length / 2);
          break;
        case HAlign.Right:
          x = Console.WindowWidth - s.Length;
          break;
      }
      WriteAt(s, x, y, color, backgroundColor);
    }

    internal static void WriteAlignedAt(string s, HAlign hAlign, VAlign vAlign)
    {
      int x = 0;
      int y = 0;
      switch (hAlign)
      {
        case HAlign.Left:
          x = 0;
          break;
        case HAlign.Center:
          x = (Console.WindowWidth / 2) - (s.Length / 2);
          break;
        case HAlign.Right:
          x = Console.WindowWidth - s.Length;
          break;
      }

      switch (vAlign)
      {
        case VAlign.Top:
          y = 0;
          break;
        case VAlign.Middle:
          y = (Console.WindowHeight / 2) - 1;
          break;
        case VAlign.Bottom:
          y = Console.WindowHeight - 1;
          break;
      }
      WriteAt(s, x, y);
    }

    internal static void WriteAlignedAt(string s, HAlign hAlign, VAlign vAlign, ConsoleColor color)
    {
      int x = 0;
      int y = 0;
      switch (hAlign)
      {
        case HAlign.Left:
          x = 0;
          break;
        case HAlign.Center:
          x = (Console.WindowWidth / 2) - (s.Length / 2);
          break;
        case HAlign.Right:
          x = Console.WindowWidth - s.Length;
          break;
      }

      switch (vAlign)
      {
        case VAlign.Top:
          y = 0;
          break;
        case VAlign.Middle:
          y = (Console.WindowHeight / 2) - 1;
          break;
        case VAlign.Bottom:
          y = Console.WindowHeight - 1;
          break;
      }
      WriteAt(s, x, y, color);
    }

    internal static void WriteAlignedAt(string s, HAlign hAlign, VAlign vAlign, ConsoleColor color,
      ConsoleColor backgroundColor)
    {
      int x = 0;
      int y = 0;
      switch (hAlign)
      {
        case HAlign.Left:
          x = 0;
          break;
        case HAlign.Center:
          x = (Console.WindowWidth / 2) - (s.Length / 2);
          break;
        case HAlign.Right:
          x = Console.WindowWidth - s.Length;
          break;
      }

      switch (vAlign)
      {
        case VAlign.Top:
          y = 0;
          break;
        case VAlign.Middle:
          y = (Console.WindowHeight / 2) - 1;
          break;
        case VAlign.Bottom:
          y = Console.WindowHeight - 1;
          break;
      }
      WriteAt(s, x, y, color, backgroundColor);
    }

    internal static void WriteAlignedAt(string s, HAlign hAlign, VAlign vAlign, int xOffset, int yOffset)
    {
      int x = 0;
      int y = 0;
      switch (hAlign)
      {
        case HAlign.Left:
          x = 0;
          break;
        case HAlign.Center:
          x = (Console.WindowWidth / 2) - (s.Length / 2);
          break;
        case HAlign.Right:
          x = Console.WindowWidth - s.Length;
          break;
      }

      switch (vAlign)
      {
        case VAlign.Top:
          y = 0;
          break;
        case VAlign.Middle:
          y = (Console.WindowHeight / 2) - 1;
          break;
        case VAlign.Bottom:
          y = Console.WindowHeight - 1;
          break;
      }
      WriteAt(s, x + xOffset, y + yOffset);
    }

    internal static void WriteAlignedAt(string s, HAlign hAlign, VAlign vAlign, ConsoleColor color,
      int xOffset, int yOffset)
    {
      int x = 0;
      int y = 0;
      switch (hAlign)
      {
        case HAlign.Left:
          x = 0;
          break;
        case HAlign.Center:
          x = (Console.WindowWidth / 2) - (s.Length / 2);
          break;
        case HAlign.Right:
          x = Console.WindowWidth - s.Length;
          break;
      }

      switch (vAlign)
      {
        case VAlign.Top:
          y = 0;
          break;
        case VAlign.Middle:
          y = (Console.WindowHeight / 2) - 1;
          break;
        case VAlign.Bottom:
          y = Console.WindowHeight - 1;
          break;
      }
      WriteAt(s, x + xOffset, y + yOffset, color);
    }

    internal static void WriteAlignedAt(string s, HAlign hAlign, VAlign vAlign, ConsoleColor color,
      ConsoleColor backgroundColor, int xOffset, int yOffset)
    {
      int x = 0;
      int y = 0;
      switch (hAlign)
      {
        case HAlign.Left:
          x = 0;
          break;
        case HAlign.Center:
          x = (Console.WindowWidth / 2) - (s.Length / 2);
          break;
        case HAlign.Right:
          x = Console.WindowWidth - s.Length;
          break;
      }

      switch (vAlign)
      {
        case VAlign.Top:
          y = 0;
          break;
        case VAlign.Middle:
          y = (Console.WindowHeight / 2) - 1;
          break;
        case VAlign.Bottom:
          y = Console.WindowHeight - 1;
          break;
      }
      WriteAt(s, x + xOffset, y + yOffset, color, backgroundColor);
    }

    // Extended Color WriteAlignedAt Methods

    internal static void WriteAlignedAt(string s, HAlign hAlign, VAlign vAlign, Color color)
    {
      int x = 0;
      int y = 0;
      switch (hAlign)
      {
        case HAlign.Left:
          x = 0;
          break;
        case HAlign.Center:
          x = (Console.WindowWidth / 2) - (s.Length / 2);
          break;
        case HAlign.Right:
          x = Console.WindowWidth - s.Length;
          break;
      }

      switch (vAlign)
      {
        case VAlign.Top:
          y = 0;
          break;
        case VAlign.Middle:
          y = (Console.WindowHeight / 2) - 1;
          break;
        case VAlign.Bottom:
          y = Console.WindowHeight - 1;
          break;
      }
      WriteAt(s, x, y, color);
    }



    // WriteAt string methods
    internal static void WriteAt(string s, int x, int y)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        Console.Write(s);
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(string s, int x, int y, ConsoleColor color)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(s);
        Console.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(string s, int x, int y, ConsoleColor color, ConsoleColor backgroundColor)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.BackgroundColor = backgroundColor;
        Console.Write(s);
        Console.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(string s, int x, int y, ConsoleColor color, ConsoleColor backgroundColor, int delay)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.BackgroundColor = backgroundColor;
        foreach (char c in s)
        {
          Console.Write(c);
          if (delay > 0) Thread.Sleep(delay);
        }

        Console.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(string s, int x, int y, ConsoleColor color, int delay)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        foreach (char c in s)
        {
          Console.Write(c);
          if (delay > 0) Thread.Sleep(delay);
        }

        Console.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(string s, int x, int y, int delay)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        foreach (char c in s)
        {
          Console.Write(c);
          Thread.Sleep(delay);
        }
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(string s, int x, int y, ConsoleColor color, ConsoleColor backgroundColor, int delay,
      int repeat)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.BackgroundColor = backgroundColor;
        for (int i = 0; i < repeat; i++)
        {
          foreach (char c in s)
          {
            Console.Write(c);
            if (delay > 0) Thread.Sleep(delay);
          }
        }

        Console.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(string s, int x, int y, int delay, int repeat)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        for (int i = 0; i < repeat; i++)
        {
          foreach (char c in s)
          {
            Console.Write(c);
            if (delay > 0) Thread.Sleep(delay);
          }
        }
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(string s, int x, int y, ConsoleColor color, ConsoleColor backgroundColor, int delay,
      int repeat, int repeatDelay)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.BackgroundColor = backgroundColor;
        for (int i = 0; i < repeat; i++)
        {
          foreach (char c in s)
          {
            Console.Write(c);
            Thread.Sleep(delay);
          }

          Thread.Sleep(repeatDelay);
        }

        Console.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(string s, int x, int y, int delay, int repeat, int repeatDelay)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        for (int i = 0; i < repeat; i++)
        {
          foreach (char c in s)
          {
            Console.Write(c);
            Thread.Sleep(delay);
          }

          Thread.Sleep(repeatDelay);
        }
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(string s, int x, int y, ConsoleColor color, int delay, int repeat)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        for (int i = 0; i < repeat; i++)
        {
          foreach (char c in s)
          {
            Console.Write(c);
            Thread.Sleep(delay);
          }
        }

        Console.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(string s, int x, int y, ConsoleColor color, int delay, int repeat, int repeatDelay)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        for (int i = 0; i < repeat; i++)
        {
          foreach (char c in s)
          {
            Console.Write(c);
            Thread.Sleep(delay);
          }

          Thread.Sleep(repeatDelay);
        }

        Console.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    //WriteAt Char Methods
    internal static void WriteAt(char c, int x, int y, ConsoleColor color)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(c);
        Console.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(char c, int x, int y, ConsoleColor color, ConsoleColor backgroundColor)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.BackgroundColor = backgroundColor;
        Console.Write(c);
        Console.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(char c, int x, int y, ConsoleColor color, int repeat)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(new string(c, repeat));
        Console.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(char c, int x, int y, ConsoleColor color, ConsoleColor backgroundColor, int delay)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.BackgroundColor = backgroundColor;
        Console.Write(c);
        Thread.Sleep(delay);
        Console.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(char c, int x, int y, int delay)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        Console.Write(c);
        Thread.Sleep(delay);
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(char c, int x, int y, int delay, int repeat)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        for (int i = 0; i < repeat; i++)
        {
          Console.Write(c);
          Thread.Sleep(delay);
        }
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(char c, int x, int y, int delay, int repeat, int repeatDelay)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        for (int i = 0; i < repeat; i++)
        {
          Console.Write(c);
          Thread.Sleep(delay);
        }

        Thread.Sleep(repeatDelay);
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(char c, int x, int y, ConsoleColor color, ConsoleColor backgroundColor, int delay,
      int repeat)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.BackgroundColor = backgroundColor;
        for (int i = 0; i < repeat; i++)
        {
          Console.Write(c);
          Thread.Sleep(delay);
        }

        Console.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(char c, int x, int y, ConsoleColor color, int delay, int repeat)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        for (int i = 0; i < repeat; i++)
        {
          Console.Write(c);
          Thread.Sleep(delay);
        }

        Console.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(char c, int x, int y, ConsoleColor color, ConsoleColor backgroundColor, int delay,
      int repeat, int repeatDelay)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.BackgroundColor = backgroundColor;
        for (int i = 0; i < repeat; i++)
        {
          Console.Write(c);
          Thread.Sleep(delay);
        }

        Thread.Sleep(repeatDelay);
        Console.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }


    // Extended Color WriteAt Char Methods
    internal static void WriteAt(char c, int x, int y, Color color, Color backgroundColor)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        ConsoleEx.ForegroundColor = color;
        ConsoleEx.BackgroundColor = backgroundColor;
        Console.Write(c);
        ConsoleEx.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(char c, int x, int y, Color color)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        ConsoleEx.ForegroundColor = color;
        Console.Write(c);
        ConsoleEx.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear(); Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(char c, int x, int y, Color color, int repeat)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        ConsoleEx.ForegroundColor = color;
        Console.Write(new string(c, repeat));
        ConsoleEx.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }


    // Extended Color WriteAt String Methods
    internal static void WriteAt(string s, int x, int y, Color color)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        ConsoleEx.ForegroundColor = color;
        Console.Write(s);
        ConsoleEx.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        ConsoleEx.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(string s, int x, int y, Color color, Color backgroundColor)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        ConsoleEx.ForegroundColor = color;
        ConsoleEx.BackgroundColor = backgroundColor;
        Console.Write(s);
        ConsoleEx.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        ConsoleEx.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(string s, int x, int y, Color color, Color backgroundColor, int delay)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        ConsoleEx.ForegroundColor = color;
        ConsoleEx.BackgroundColor = backgroundColor;
        foreach (char c in s)
        {
          Console.Write(c);
          Thread.Sleep(delay);
        }

        ConsoleEx.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        ConsoleEx.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteAt(string s, int x, int y, Color color, int delay)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        ConsoleEx.ForegroundColor = color;
        foreach (char c in s)
        {
          Console.Write(c);
          Thread.Sleep(delay);
        }

        ConsoleEx.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        ConsoleEx.Clear();
        Console.WriteLine(e.Message);
      }
    }


    // WriteBorder Methods
    internal static void WriteBorder(Box box, char h, char v, ConsoleColor color)
    {
      try
      {
        for (int i = 0; i < box.Height; i++)
        {
          if (i == 0 || i == box.Height - 1)
          {
            ConsoleEx.WriteAt(h, box.Left, box.Top + i, color, box.Width);
          }
          else
          {
            ConsoleEx.WriteAt(v,box.Left, box.Top + i, color);
            ConsoleEx.WriteAt(v, box.Left + box.Width - 1, box.Top + i, color);
          }
        }
        Console.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteBorder(Box box, BoxChars bChars, ConsoleColor color)
    {
      try
      {
        for (int i = 0; i < box.Height; i++)
        {
          if (i == 0)
          {
            ConsoleEx.WriteAt(bChars.topLeft, box.Left, box.Top + i, color);
            ConsoleEx.WriteAt(bChars.topRight, box.Left + box.Width - 1, box.Top + i, color);
            ConsoleEx.WriteAt(bChars.hor, box.Left + 1, box.Top + i, color, box.Width - 2);
          }
          else if (i == box.Height - 1)
          {
            ConsoleEx.WriteAt(bChars.botLeft, box.Left, box.Top + i, color);
            ConsoleEx.WriteAt(bChars.botRight, box.Left + box.Width - 1, box.Top + i, color);
            ConsoleEx.WriteAt(bChars.hor, box.Left + 1, box.Top + i, color, box.Width - 2);
          }
          else
          {
            ConsoleEx.WriteAt(bChars.ver, box.Left, box.Top + i, color);
            ConsoleEx.WriteAt(bChars.ver, box.Left + box.Width - 1, box.Top + i, color);
          }
        }

        Console.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal static void WriteBorder(Box box, BoxCharsEx bChars, ConsoleColor color)
    {
      try
      {
        for (int i = 0; i < box.Height; i++)
        {
          if (i == 0)
          {
            ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.topLeft), box.Left, box.Top + i, color);
            ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.topRight), box.Left + box.Width - 1, box.Top + i, color);
            ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.hor), box.Left + 1, box.Top + i, color, box.Width - 2);
          }
          else if (i == box.Height - 1)
          {
            ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.botLeft), box.Left, box.Top + i, color);
            ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.botRight), box.Left + box.Width - 1, box.Top + i, color);
            ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.hor), box.Left + 1, box.Top + i, color, box.Width - 2);
          }
          else
          {
            ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.ver), box.Left, box.Top + i, color);
            ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.ver), box.Left + box.Width - 1, box.Top + i, color);
          }
        }

        Console.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }


    // Flashing Methods
    // make a char flash
    internal static void FlashAt(char c, int x, int y, int flashCount, int flashDelay)
    {
      for (int i = 0; i < flashCount; i++)
      {
        WriteAt(c, x, y, ConsoleColor.Black, ConsoleColor.White);
        Thread.Sleep(flashDelay);
        WriteAt(c, x, y, ConsoleColor.White, ConsoleColor.Black);
        Thread.Sleep(flashDelay);
      }
    }

    // make a char flash with a given color and delay
    internal static void FlashAt(char c, int x, int y, int flashCount, int flashDelay,
      ConsoleColor color)
    {
      for (int i = 0; i < flashCount; i++)
      {
        WriteAt(c, x, y, color, ConsoleColor.Black);
        Thread.Sleep(flashDelay);
        WriteAt(c, x, y, ConsoleColor.Black, color);
        Thread.Sleep(flashDelay);
      }
    }

    // make a char flash with a given color and background color and delay
    internal static void FlashAt(char c, int x, int y, int flashCount, int flashDelay,
      ConsoleColor color, ConsoleColor backgroundColor)
    {
      for (int i = 0; i < flashCount; i++)
      {
        WriteAt(c, x, y, color, backgroundColor);
        Thread.Sleep(flashDelay);
        WriteAt(c, x, y, backgroundColor, color);
        Thread.Sleep(flashDelay);
      }
    }

    // make a string flash
    internal static void FlashAt(string s, int x, int y, int flashCount, int flashDelay)
    {
      for (int i = 0; i < flashCount; i++)
      {
        WriteAt(s, x, y, ConsoleColor.Black, ConsoleColor.White);
        Thread.Sleep(flashDelay);
        WriteAt(s, x, y, ConsoleColor.White, ConsoleColor.Black);
        Thread.Sleep(flashDelay);
      }
    }

    // make a string flash with a given color and delay
    internal static void FlashAt(string s, int x, int y, int flashCount, int flashDelay,
      ConsoleColor color)
    {
      for (int i = 0; i < flashCount; i++)
      {
        WriteAt(s, x, y, color, ConsoleColor.Black);
        Thread.Sleep(flashDelay);
        WriteAt(s, x, y, ConsoleColor.Black, color);
        Thread.Sleep(flashDelay);
      }
    }

    // make a string flash with a given color and background color and delay
    internal static void FlashAt(string s, int x, int y, int flashCount, int flashDelay,
      ConsoleColor color, ConsoleColor backgroundColor)
    {
      for (int i = 0; i < flashCount; i++)
      {
        WriteAt(s, x, y, color, backgroundColor);
        Thread.Sleep(flashDelay);
        WriteAt(s, x, y, backgroundColor, color);
        Thread.Sleep(flashDelay);
      }
    }


    //Utility Methods
    internal static void ResetColor()
    {
      Console.Write(ConsoleColorEx.resetColor);
    }

    internal static void Clear()
    {
      ResetColor();
      Console.Clear();
    }

  }

  internal class BoxChars
  {
    internal char topLeft = ' ';
    internal char topRight = ' ';
    internal char botLeft = ' ';
    internal char botRight = ' ';
    internal char hor = ' ';
    internal char ver = ' ';

    internal BoxChars()
    {
    }

    internal BoxChars(char topLeft, char topRight, char botLeft, char botRight, char hor, char ver)
    {
      this.topLeft = topLeft;
      this.topRight = topRight;
      this.botLeft = botLeft;
      this.botRight = botRight;
      this.hor = hor;
      this.ver = ver;
    }
  }

  internal class BoxCharsEx
  {
    internal string topLeft = " ";
    internal string topRight = " ";
    internal string botLeft = " ";
    internal string botRight = " ";
    internal string hor = " ";
    internal string ver = " ";
    internal BoxCharsEx() { }

    internal BoxCharsEx(string topLeft, string topRight, string botLeft, string botRight, string hor, string ver)
    {
      this.topLeft = topLeft;
      this.topRight = topRight;
      this.botLeft = botLeft;
      this.botRight = botRight;
      this.hor = hor;
      this.ver = ver;
    }

  }

  internal class Box
  {
    internal int Height = 0;
    internal int Width = 0;
    internal int Left = 0;
    internal int Top = 0;

    internal Box()
    {
    }

    internal Box(int left, int top, int width, int height)
    {
      Left = left;
      Top = top;
      Width = width;
      Height = height;
    }
  }
}
