using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using ConsoleDungeonCrawler.Game;
using ConsoleDungeonCrawler.Game.Entities;

namespace ConsoleDungeonCrawler.Extensions
{
    internal static class ConsoleEx
  {
    private struct Rect
    {
      public int Left;
      public int Top;
      public int Right;
      public int Bottom;
    }

    private static Color foregroundColor = Color.Gray;
    internal static Color ForegroundColor
    {
      get => foregroundColor;
      set
      {
        foregroundColor = value;
        Console.Write(ColorEx.ToFgHiColor(value));
      }
    }

    private static Color backgroundColor = Color.Black;
    internal static Color BackgroundColor
    {
      get => backgroundColor;
      set
      {
        backgroundColor = value;
        Console.Write(ColorEx.ToBgHiColor(value));
      }
    }

    internal static void InitializeConsole()
    {
      ConsoleEx.Clear();
      Console.OutputEncoding = System.Text.Encoding.Unicode;
      // Maximize console window  Use if you have a console window set to a specific size and is not maximized (fullscreen focused
      //MaximizeConsoleWindow();
      // Enable extended colors
      EnableExtendedColors();

      Console.CursorVisible = false;
      Console.Title = Game.Game.Title;
    }

    /// <summary>
    /// This is a windows only feature...
    /// </summary>
    private static void MaximizeConsoleWindow()
    {
      // Setup console to allow window to be maximized
      // Import the necessary functions from user32.dll
      [DllImport("user32.dll")]
      static extern IntPtr GetForegroundWindow();

      [DllImport("user32.dll")]
      static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

      [DllImport("user32.dll")]
      static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);

      [DllImport("user32.dll")]
      static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

      // Constants for the ShowWindow function
      const int swMaximize = 3;
      IntPtr consoleWindowHandle = GetForegroundWindow();
      ShowWindow(consoleWindowHandle, swMaximize);

      // Get the screen size
      GetWindowRect(consoleWindowHandle, out Rect screenRect);
      // Resize and reposition the console window to fill the screen
      int width = screenRect.Right - screenRect.Left;
      int height = screenRect.Bottom - screenRect.Top;
      MoveWindow(consoleWindowHandle, screenRect.Left, screenRect.Top, width, height, true);
      SetScreenSizes(width, height);
    }

    /// <summary>
    /// This is a windows only feature...
    /// </summary>
    private static void EnableExtendedColors()
    {
      // Setup console to use extended ansi colors
      [DllImport("kernel32.dll", SetLastError = true)]
      static extern bool SetConsoleMode(IntPtr hConsoleHandle, int mode);

      [DllImport("kernel32.dll", SetLastError = true)]
      static extern bool GetConsoleMode(IntPtr handle, out int mode);

      [DllImport("kernel32.dll", SetLastError = true)]
      static extern IntPtr GetStdHandle(int handle);

      IntPtr handle = GetStdHandle(-11);
      GetConsoleMode(handle, out int mode);
      SetConsoleMode(handle, mode | 0x4);
    }

    /// <summary>
    /// This is a windows only feature...
    /// </summary>
    private static void SetScreenSizes(int width, int height)
    {
      // Set console window size
      Console.SetWindowSize(width, height);
      Console.SetBufferSize(width, height);
    }

    /// <summary>
    /// Prevent stacking of the Keyboard input buffer
    /// </summary>
    internal static void FlushInput()
    {
      while (Console.KeyAvailable) Console.ReadKey(true);
    }

    // Extension methods for char and string console writes

    internal static void WriteLegendItem(this MapObject mapObject, int col, int row, int width)
    {
      // create a formatted line containing the symbol and the type of the map object
      int padding = width - mapObject.Type.Name.Length - 5;
      int paddingStart = col + mapObject.Type.Name.Length + 3;
      WriteAt(' ', col, row, Color.White, Color.DimGray);
      WriteAt(mapObject.Type.Symbol.ToString(), col + 1, row, mapObject.Type.ForegroundColor, mapObject.Type.BackgroundColor);
      WriteAt(':', col + 2, row, Color.White, Color.DimGray);
      WriteAt(mapObject.Type.Name, col + 3, row, mapObject.Type.ForegroundColor, Color.DimGray);
      WriteAt(new string(' ', padding), paddingStart, row, Color.White, Color.DimGray);
    }

    internal static void WriteInventoryItem(this Item item, int col, int row, int colWidth)
    {
      // create a formatted line containing the symbol and the type of the map object

      WriteAt($"({item.Quantity})", col, row, ColorEx.RarityColor(item.Rarity));
      WriteAt(item.Name.PadRight(col + colWidth), col + 5, row, ColorEx.RarityColor(item.Rarity));
    }

    // WriteAlignedAt Methods
    internal static void WriteAlignedAt(this string s, HAlign hAlign)
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

    internal static void WriteAlignedAt(this string s, HAlign hAlign, ConsoleColor color)
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

    internal static void WriteAlignedAt(this string s, HAlign hAlign, int y)
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

    internal static void WriteAlignedAt(this string s, HAlign hAlign, int y, ConsoleColor color)
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

    internal static void WriteAlignedAt(this string s, HAlign hAlign, int y, ConsoleColor color,
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

    internal static void WriteAlignedAt(this string s, HAlign hAlign, VAlign vAlign)
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

    internal static void WriteAlignedAt(this string s, HAlign hAlign, VAlign vAlign, ConsoleColor color)
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

    internal static void WriteAlignedAt(this string s, HAlign hAlign, VAlign vAlign, ConsoleColor color,
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

    internal static void WriteAlignedAt(this string s, HAlign hAlign, VAlign vAlign, int xOffset, int yOffset)
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

    internal static void WriteAlignedAt(this string s, HAlign hAlign, VAlign vAlign, ConsoleColor color,
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

    internal static void WriteAlignedAt(this string s, HAlign hAlign, VAlign vAlign, ConsoleColor color,
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

    internal static void WriteAlignedAt(this string s, HAlign hAlign, VAlign vAlign, Color color)
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

    internal static void WriteAlignedAt(this string s, HAlign hAlign, VAlign vAlign, Color color,
      Color backgroundColor, int xOffset, int yOffset)
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

    internal static void WriteAlignedAt(this string s, HAlign hAlign, VAlign vAlign, Color color,
      Color backgroundColor)
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

    internal static void WriteAlignedAt(this string s, Box box, HAlign hAlign, VAlign vAlign, Color color,
      Color backgroundColor)
    {
      int x = 0;
      int y = 0;
      switch (hAlign)
      {
        case HAlign.Left:
          x = box.Left + 1;
          break;
        case HAlign.Center:
          x = (Console.WindowWidth / 2) - (s.Length / 2);
          break;
        case HAlign.Right:
          x = box.Width - s.Length;
          break;
      }

      switch (vAlign)
      {
        case VAlign.Top:
          y = box.Top + 1;
          break;
        case VAlign.Middle:
          y = ((Console.WindowHeight) / 2) - 1;
          break;
        case VAlign.Bottom:
          y = box.Height + box.Top - 1;
          break;
      }
      WriteAt(s, x, y, color, backgroundColor);
    }

    internal static void WriteAlignedAt(this string s, Box box, HAlign hAlign, VAlign vAlign, Color color,
      Color backgroundColor, int xOffset, int yOffset)
    {
      int x = 0;
      int y = 0;
      switch (hAlign)
      {
        case HAlign.Left:
          x = box.Left + 1;
          break;
        case HAlign.Center:
          x = (Console.WindowWidth / 2) - (s.Length / 2);
          break;
        case HAlign.Right:
          x = box.Width + box.Left - s.Length;
          break;
      }

      switch (vAlign)
      {
        case VAlign.Top:
          y = box.Top + 1;
          break;
        case VAlign.Middle:
          y = (Console.WindowHeight / 2) - 1;
          break;
        case VAlign.Bottom:
          y = box.Height + box.Top - 1;
          break;
      }
      WriteAt(s, x + xOffset, y + yOffset, color, backgroundColor);
    }

    // WriteAt string methods
    private static void WriteAt(this string s, int x, int y)
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

    internal static void WriteAt(this string s, int x, int y, ConsoleColor color)
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

    internal static void WriteAt(this string s, int x, int y, ConsoleColor color, ConsoleColor backgroundColor)
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

    internal static void WriteAt(this string s, int x, int y, ConsoleColor color, ConsoleColor backgroundColor, int delay)
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

    internal static void WriteAt(this string s, int x, int y, ConsoleColor color, int delay)
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

    internal static void WriteAt(this string s, int x, int y, int delay)
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

    internal static void WriteAt(this string s, int x, int y, ConsoleColor color, ConsoleColor backgroundColor, int delay,
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

    internal static void WriteAt(this string s, int x, int y, int delay, int repeat)
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

    internal static void WriteAt(this string s, int x, int y, ConsoleColor color, ConsoleColor backgroundColor, int delay,
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

    internal static void WriteAt(this string s, int x, int y, int delay, int repeat, int repeatDelay)
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

    internal static void WriteAt(this string s, int x, int y, ConsoleColor color, int delay, int repeat)
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

    internal static void WriteAt(this string s, int x, int y, ConsoleColor color, int delay, int repeat, int repeatDelay)
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
    internal static void WriteAt(this char c, int x, int y, ConsoleColor color)
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

    internal static void WriteAt(this char c, int x, int y, ConsoleColor color, ConsoleColor backgroundColor)
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

    internal static void WriteAt(this char c, int x, int y, ConsoleColor color, int repeat)
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

    internal static void WriteAt(this char c, int x, int y, ConsoleColor color, ConsoleColor backgroundColor, int delay)
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

    internal static void WriteAt(this char c, int x, int y, int delay)
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

    internal static void WriteAt(this char c, int x, int y, int repeat, int delay)
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

    internal static void WriteAt(this char c, int x, int y, int repeat, int delay, int repeatDelay)
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

    internal static void WriteAt(this char c, int x, int y, ConsoleColor color, ConsoleColor backgroundColor, int repeat,
      int delay)
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

    internal static void WriteAt(this char c, int x, int y, ConsoleColor color, int repeat, int delay)
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

    internal static void WriteAt(this char c, int x, int y, ConsoleColor color, ConsoleColor backgroundColor, int delay,
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
    internal static void WriteAt(this char c, int x, int y, Color color, Color backgroundColor)
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

    internal static void WriteAt(this char c, int x, int y, Color color)
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

    internal static void WriteAt(this char c, int x, int y, Color color, int repeat)
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

    internal static void WriteAt(this char c, int x, int y, Color color, Color backgroundColor, int repeat,
      int delay = 0)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        ConsoleEx.ForegroundColor = color;
        ConsoleEx.BackgroundColor = backgroundColor;
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


    // Extended Color WriteAt String Methods
    internal static void WriteAt(this string s, int x, int y, Color color)
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

    internal static void WriteAt(this string s, int x, int y, Color color, Color backgroundColor)
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

    internal static void WriteAt(this string s, int x, int y, Color color, Color backgroundColor, int delay)
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

    internal static void WriteAt(this string s, int x, int y, Color color, int delay)
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

    internal static void WriteAt(this string s, int x, int y, Color color, Color backgroundColor, int repeat,
      int delay)
    {
      try
      {
        Console.SetCursorPosition(x, y);
        ConsoleEx.ForegroundColor = color;
        ConsoleEx.BackgroundColor = backgroundColor;
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


    // WriteBorder Methods
    internal static void WriteBorder(this Box box, BoxChars bChars, ConsoleColor color)
    {
      try
      {
        for (int i = 0; i < box.Height; i++)
        {
          if (i == 0)
          {
            StringInfo.GetNextTextElement(bChars.TopLeft).WriteAt(box.Left, box.Top + i, color);
            StringInfo.GetNextTextElement(bChars.TopRight).WriteAt(box.Left + box.Width - 1, box.Top + i, color);
            StringInfo.GetNextTextElement(bChars.Hor).WriteAt(box.Left + 1, box.Top + i, color, box.Width - 2, 0);
          }
          else if (i == box.Height - 1)
          {
            StringInfo.GetNextTextElement(bChars.BotLeft).WriteAt(box.Left, box.Top + i, color);
            StringInfo.GetNextTextElement(bChars.BotRight).WriteAt(box.Left + box.Width - 1, box.Top + i, color);
            StringInfo.GetNextTextElement(bChars.Hor).WriteAt(box.Left + 1, box.Top + i, color, box.Width - 2, 0);
          }
          else
          {
            StringInfo.GetNextTextElement(bChars.Ver).WriteAt(box.Left, box.Top + i, color);
            StringInfo.GetNextTextElement(bChars.Ver).WriteAt(box.Left + box.Width - 1, box.Top + i, color);
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

    internal static void WriteBorder(this Box box, BoxChars bChars, Color color)
    {
      try
      {
        for (int i = 0; i < box.Height; i++)
        {
          if (i == 0)
          {
            StringInfo.GetNextTextElement(bChars.TopLeft).WriteAt(box.Left, box.Top + i, color);
            StringInfo.GetNextTextElement(bChars.TopRight).WriteAt(box.Left + box.Width - 1, box.Top + i, color);
            StringInfo.GetNextTextElement(bChars.Hor).WriteAt(box.Left + 1, box.Top, color, Color.Black, box.Width - 2, 0);
          }
          else if (i == box.Height - 1)
          {
            StringInfo.GetNextTextElement(bChars.BotLeft).WriteAt(box.Left, box.Top + i, color);
            StringInfo.GetNextTextElement(bChars.BotRight).WriteAt(box.Left + box.Width - 1, box.Top + i, color);
            StringInfo.GetNextTextElement(bChars.Hor).WriteAt(box.Left + 1, box.Top + i, color, Color.Black, box.Width - 2, 0);
          }
          else
          {
            StringInfo.GetNextTextElement(bChars.Ver).WriteAt(box.Left, box.Top + i, color);
            StringInfo.GetNextTextElement(bChars.Ver).WriteAt(box.Left + box.Width - 1, box.Top + i, color);
          }
        }

        ConsoleEx.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        ConsoleEx.Clear();
        Console.WriteLine(e.Message);
      }
    }


    // Flashing Methods
    // make a char flash
    internal static void FlashAt(this char c, int x, int y, int flashCount, int flashDelay)
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
    internal static void FlashAt(this char c, int x, int y, int flashCount, int flashDelay,
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
    internal static void FlashAt(this char c, int x, int y, int flashCount, int flashDelay,
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
    internal static void FlashAt(this string s, int x, int y, int flashCount, int flashDelay)
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
    internal static void FlashAt(this string s, int x, int y, int flashCount, int flashDelay,
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
    internal static void FlashAt(this string s, int x, int y, int flashCount, int flashDelay,
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


    // Read Methods
    internal static int ReadInt(int x, int y, Color foregroundColor, Color backgroundColor)
    {
      ForegroundColor = foregroundColor;
      BackgroundColor = backgroundColor;
      bool valid = false;
      int result = -1;
      while (!valid)
      {
        Console.SetCursorPosition(x, y);
        ConsoleKeyInfo keyInfo = Console.ReadKey();
        valid = int.TryParse(keyInfo.KeyChar.ToString(), out result);
        if (valid) continue;
        Console.SetCursorPosition(x, y);
        Console.Write(" ");
      }
      ResetColor();
      return result;
    }

    internal static bool ReadBool(int x, int y, Color foregroundColor, Color backgroundColor)
    {
      ForegroundColor = foregroundColor;
      BackgroundColor = backgroundColor;
      bool valid = false;
      bool result = false;
      while (!valid)
      {
        Console.SetCursorPosition(x, y);
        ConsoleKeyInfo keyInfo = Console.ReadKey();
        valid = keyInfo.Key is ConsoleKey.Y or ConsoleKey.N;
        if (valid)
        {
          result = keyInfo.Key is ConsoleKey.Y;
          continue;
        }
        Console.SetCursorPosition(x, y);
        Console.Write(" ");
      }
      ResetColor();
      return result;
    }

    //Utility Methods
    internal static void ResetColor()
    {
      Console.Write(ColorEx.ResetColor);
    }

    internal static void Clear()
    {
      ResetColor();
      Console.Clear();
    }
  }
}
