using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using ConsoleDungeonCrawler.Game;
using ConsoleDungeonCrawler.Game.Entities;

namespace ConsoleDungeonCrawler.Extensions;

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
    Clear();
    Console.OutputEncoding = System.Text.Encoding.Unicode;

    // It is possible to set the console window using the settings for the console window.  If in Focus mode the Title Bar 
    // is hidden and the window is maximized.  This is a windows only feature.  to access settings when in focus mode press (Ctrl + Shift + P) 
    // The maximize method below will work in both focus and non-focus mode.  The maximize method will not work if the window is set to a specific size.
    // Settings for the console window can be accessed by right-clicking the title bar and selecting properties.  The settings are saved per console window.
    // the default settings are stored in settings.json and are loaded when the console window is created.  The settings are stored in the registry.

    // Maximize console window  Use if you have a standard console window that is not maximized (not in fullscreen focus mode)
    MaximizeConsoleWindow();
    // Enable extended colors
    EnableExtendedColors();

    Console.CursorVisible = false;
    Console.Title = Game.Game.Title;
  }

  internal static void ResetConsole()
  {
    RestoreConsoleWindow();
    ResetColor();
    Console.CursorVisible = true;
  }

  /// <summary>
  /// This is a windows only feature...
  /// </summary>
  private static void MaximizeConsoleWindow()
  {
    // Only maximize if the window is not already maximized
    if (Console.WindowWidth >= Program.MinWindowWidth && Console.WindowHeight >= Program.MinWindowHeight) return;

    // Setup console to allow window to be maximized
    // We cannot do this inside CSharp normally so we need to attach to the windows API
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

    // Get the screen size
    GetWindowRect(consoleWindowHandle, out Rect screenRect);
    // Resize and reposition the console window to fill the screen
    int width = screenRect.Right - screenRect.Left;
    int height = screenRect.Bottom - screenRect.Top;
    MoveWindow(consoleWindowHandle, screenRect.Left, screenRect.Top, width, height, true);
    ShowWindow(consoleWindowHandle, swMaximize);
    SetScreenSizes(width, height);
  }

  /// <summary>
  /// This is a windows only feature...
  /// </summary>
  private static void RestoreConsoleWindow()
  {
    // Only restore if the window is maximized
    if (Console.WindowWidth < Program.MinWindowWidth || Console.WindowHeight < Program.MinWindowHeight) return;

    // Setup console to allow window to be maximized
    // We cannot do this inside CSharp normally, so we need to attach to the windows API
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
    const int swRestore = 9;
    IntPtr consoleWindowHandle = GetForegroundWindow();

    // Get the screen size
    GetWindowRect(consoleWindowHandle, out Rect screenRect);
    // Resize and reposition the console window to fit inside the screen
    int width = screenRect.Right - screenRect.Left;
    int height = screenRect.Bottom - screenRect.Top;
    MoveWindow(consoleWindowHandle, screenRect.Left, screenRect.Top, width / 2, height / 2, true);
    ShowWindow(consoleWindowHandle, swRestore);
    SetScreenSizes(width / 2, height / 2);
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

  // string Extension methods for custom screen and dialog writes
  internal static void WriteLegendItem(this Tile tile, int col, int row, int width)
  {
    // create a formatted line containing the symbol and the type of the map object
    int padding = width - tile.Type.Name.Length - 6;
    int paddingStart = col + tile.Type.Name.Length + 4;
    WriteAt(' ', col, row, Color.White, Color.DimGray);
    WriteAt(tile.Type.Symbol.ToString(), col + 1, row, tile.Type.ForegroundColor, tile.Type.BackgroundColor);
    WriteAt(": ", col + 2, row, Color.White, Color.DimGray);
    WriteAt(tile.Type.Name, col + 4, row, tile.Type.ForegroundColor, Color.DimGray);
    WriteAt(new string(' ', padding), paddingStart, row, Color.White, Color.DimGray);
  }

  internal static void WriteInventoryItem(this Item item, int col, int row, int colWidth)
  {
    // create a formatted line containing the symbol and the type of the map object

    WriteAt($"({item.Quantity})  ", col, row, ColorEx.RarityColor(item.Rarity));
    WriteAt(item.Name.PadRight(col + colWidth), col + 5, row, ColorEx.RarityColor(item.Rarity));
  }

  // string and char extension methods

  // WriteAlignedAt Methods
  #region WriteAlignedAt Methods
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
    ConsoleColor bgColor)
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
    WriteAt(s, x, y, color, bgColor);
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
    ConsoleColor bgColor)
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
    WriteAt(s, x, y, color, bgColor);
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

  /// <summary>
  /// Writes a string aligned to the screen using legacy ConsoleColors and an Offset ability
  /// </summary>
  /// <param name="s">String to write</param>
  /// <param name="hAlign">Horizontal alignment</param>
  /// <param name="vAlign">Vertical alignment</param>
  /// <param name="color">Foreground Console color</param>
  /// <param name="bgColor">Background Console color</param>
  /// <param name="xOffset">left and right offset amount.</param>
  /// <param name="yOffset">up and down offset amount</param>
  internal static void WriteAlignedAt(this string s, HAlign hAlign, VAlign vAlign, ConsoleColor color,
    ConsoleColor bgColor, int xOffset, int yOffset)
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
    WriteAt(s, x + xOffset, y + yOffset, color, bgColor);
  }

  // Extended Color WriteAlignedAt Methods

  /// <summary>
  /// Writes a string aligned with the screen
  /// </summary>
  /// <param name="s">String to write</param>
  /// <param name="hAlign">Horizontal alignment</param>
  /// <param name="vAlign">Vertical alignment</param>
  /// <param name="color">Foreground color</param>
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

  /// <summary>
  /// Writes a string aligned with the screen and includes an offset ability
  /// </summary>
  /// <param name="s">String to write</param>
  /// <param name="hAlign">Horizontal alignment</param>
  /// <param name="vAlign">Vertical alignment</param>
  /// <param name="color">Foreground color</param>
  /// <param name="bgColor">Background color</param>
  /// <param name="xOffset">left and right offset amount.</param>
  /// <param name="yOffset">up and down offset amount</param>
  internal static void WriteAlignedAt(this string s, HAlign hAlign, VAlign vAlign, Color color,
    Color bgColor, int xOffset, int yOffset)
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
    WriteAt(s, x + xOffset, y + yOffset, color, bgColor);
  }

  /// <summary>
  /// Writes a string aligned with the screen
  /// </summary>
  /// <param name="s">String to write</param>
  /// <param name="hAlign">Horizontal alignment</param>
  /// <param name="vAlign">Vertical alignment</param>
  /// <param name="color">Foreground color</param>
  /// <param name="bgColor">Background color</param>
  internal static void WriteAlignedAt(this string s, HAlign hAlign, VAlign vAlign, Color color, Color bgColor)
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
    WriteAt(s, x, y, color, bgColor);
  }

  /// <summary>
  /// Writes a string aligned with a box
  /// </summary>
  /// <param name="s">String to write</param>
  /// <param name="hAlign">Horizontal alignment</param>
  /// <param name="vAlign">Vertical alignment</param>
  /// <param name="color">Foreground color</param>
  /// <param name="bgColor">Background color</param>
  internal static void WriteAlignedAt(this string s, Box box, HAlign hAlign, VAlign vAlign, Color color, Color bgColor)
  {
    int x = 0;
    int y = 0;
    switch (hAlign)
    {
      case HAlign.Left:
        x = box.Left + 1;
        break;
      case HAlign.Center:
        x = box.Left + (box.Width / 2) - (s.Length / 2);
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
    WriteAt(s, x, y, color, bgColor);
  }

  /// <summary>
  /// Writes a string aligned with a box and includes an offset ability
  /// </summary>
  /// <param name="s">String to write</param>
  /// <param name="box">ScreenBox to use for alignment</param>
  /// <param name="hAlign">Horizontal alignment</param>
  /// <param name="vAlign">Vertical alignment</param>
  /// <param name="color">Foreground color</param>
  /// <param name="bgColor">Background color</param>
  /// <param name="xOffset">left and right offset amount.</param>
  /// <param name="yOffset">up and down offset amount</param>
  internal static void WriteAlignedAt(this string s, Box box, HAlign hAlign, VAlign vAlign, Color color,
    Color bgColor, int xOffset, int yOffset)
  {
    int x = 0;
    int y = 0;
    switch (hAlign)
    {
      case HAlign.Left:
        x = box.Left + 1;
        break;
      case HAlign.Center:
        x = box.Center.X - (s.Length / 2);
        break;
      case HAlign.Right:
        x = box.Left + box.Width - s.Length;
        break;
    }

    switch (vAlign)
    {
      case VAlign.Top:
        y = box.Top + 1;
        break;
      case VAlign.Middle:
        y = (box.Center.Y);
        break;
      case VAlign.Bottom:
        y = box.Top + box.Height - 1;
        break;
    }
    WriteAt(s, x + xOffset, y + yOffset, color, bgColor);
  }
  #endregion WriteAlignedAt Methods

  // WriteAt string methods
  #region WriteAt string methods
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

  internal static void WriteAt(this string s, int x, int y, ConsoleColor color, ConsoleColor bgColor)
  {
    try
    {
      Console.SetCursorPosition(x, y);
      Console.ForegroundColor = color;
      Console.BackgroundColor = bgColor;
      Console.Write(s);
      Console.ResetColor();
    }
    catch (ArgumentOutOfRangeException e)
    {
      Console.Clear();
      Console.WriteLine(e.Message);
    }
  }

  internal static void WriteAt(this string s, int x, int y, int repeat)
  {
    try
    {
      Console.SetCursorPosition(x, y);
      for (int i = 0; i < repeat; i++)
      {
        foreach (char c in s)
        {
          Console.Write(c);
        }
      }
    }
    catch (ArgumentOutOfRangeException e)
    {
      Console.Clear();
      Console.WriteLine(e.Message);
    }
  }

  internal static void WriteAt(this string s, int x, int y, ConsoleColor color, int repeat)
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

  internal static void WriteAt(this string s, int x, int y, ConsoleColor color, ConsoleColor bgColor, int repeat)
  {
    try
    {
      Console.SetCursorPosition(x, y);
      Console.ForegroundColor = color;
      Console.BackgroundColor = bgColor;
      for (int i = 0; i < repeat; i++)
      {
        foreach (char c in s)
        {
          Console.Write(c);
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

  internal static void WriteAt(this string s, int x, int y, ConsoleColor color, ConsoleColor bgColor, int repeat,
    int delay)
  {
    try
    {
      Console.SetCursorPosition(x, y);
      Console.ForegroundColor = color;
      Console.BackgroundColor = bgColor;
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

  internal static void WriteAt(this string s, int x, int y, int repeat, int delay)
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

  internal static void WriteAt(this string s, int x, int y, ConsoleColor color, ConsoleColor bgColor, int repeat,
    int delay, int repeatDelay)
  {
    try
    {
      Console.SetCursorPosition(x, y);
      Console.ForegroundColor = color;
      Console.BackgroundColor = bgColor;
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

  internal static void WriteAt(this string s, int x, int y, int repeat, int delay, int repeatDelay)
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

  internal static void WriteAt(this string s, int x, int y, ConsoleColor color, int repeat, int delay)
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

  internal static void WriteAt(this string s, int x, int y, ConsoleColor color, int repeat, int delay, int repeatDelay)
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
  #endregion WriteAt string methods

  //WriteAt Char Methods
  #region WriteAt char methods
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

  internal static void WriteAt(this char c, int x, int y, ConsoleColor color, ConsoleColor bgColor)
  {
    try
    {
      Console.SetCursorPosition(x, y);
      Console.ForegroundColor = color;
      Console.BackgroundColor = bgColor;
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

  internal static void WriteAt(this char c, int x, int y, ConsoleColor color, ConsoleColor bgColor, int repeat)
  {
    try
    {
      Console.SetCursorPosition(x, y);
      Console.ForegroundColor = color;
      Console.BackgroundColor = bgColor;
      Console.Write(new string(c, repeat));
      Console.ResetColor();
    }
    catch (ArgumentOutOfRangeException e)
    {
      Console.Clear();
      Console.WriteLine(e.Message);
    }
  }

  internal static void WriteAt(this char c, int x, int y, int repeat)
  {
    try
    {
      Console.SetCursorPosition(x, y);
      Console.Write(new string(c, repeat));
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

  internal static void WriteAt(this char c, int x, int y, ConsoleColor color, ConsoleColor bgColor, int repeat,
    int delay)
  {
    try
    {
      Console.SetCursorPosition(x, y);
      Console.ForegroundColor = color;
      Console.BackgroundColor = bgColor;
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

  internal static void WriteAt(this char c, int x, int y, ConsoleColor color, ConsoleColor bgColor, int delay,
    int repeat, int repeatDelay)
  {
    try
    {
      Console.SetCursorPosition(x, y);
      Console.ForegroundColor = color;
      Console.BackgroundColor = bgColor;
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
  #endregion WriteAt char methods

  #region Extended Color WriteAt Char Methods
  internal static void WriteAt(this char c, int x, int y, Color color)
  {
    try
    {
      Console.SetCursorPosition(x, y);
      ForegroundColor = color;
      Console.Write(c);
      ResetColor();
    }
    catch (ArgumentOutOfRangeException e)
    {
      Console.Clear(); Console.WriteLine(e.Message);
    }
  }

  internal static void WriteAt(this char c, int x, int y, Color color, Color bgColor)
  {
    try
    {
      Console.SetCursorPosition(x, y);
      ForegroundColor = color;
      BackgroundColor = bgColor;
      Console.Write(c);
      ResetColor();
    }
    catch (ArgumentOutOfRangeException e)
    {
      Console.Clear();
      Console.WriteLine(e.Message);
    }
  }

  internal static void WriteAt(this char c, int x, int y, Color color, int repeat)
  {
    try
    {
      Console.SetCursorPosition(x, y);
      ForegroundColor = color;
      Console.Write(new string(c, repeat));
      ResetColor();
    }
    catch (ArgumentOutOfRangeException e)
    {
      Console.Clear();
      Console.WriteLine(e.Message);
    }
  }

  internal static void WriteAt(this char c, int x, int y, Color color, Color bgColor, int repeat)
  {
    try
    {
      Console.SetCursorPosition(x, y);
      ForegroundColor = color;
      BackgroundColor = bgColor;
      Console.Write(new string(c, repeat));
      ResetColor();
    }
    catch (ArgumentOutOfRangeException e)
    {
      Console.Clear();
      Console.WriteLine(e.Message);
    }
  }

  internal static void WriteAt(this char c, int x, int y, Color color, Color bgColor, int repeat, int delay)
  {
    try
    {
      Console.SetCursorPosition(x, y);
      ForegroundColor = color;
      BackgroundColor = bgColor;
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
  #endregion Extended Color WriteAt Char Methods

  #region Extended Color WriteAt String Methods
  internal static void WriteAt(this string s, int x, int y, Color color)
  {
    try
    {
      Console.SetCursorPosition(x, y);
      ForegroundColor = color;
      Console.Write(s);
      ResetColor();
    }
    catch (ArgumentOutOfRangeException e)
    {
      Clear();
      Console.WriteLine(e.Message);
    }
  }

  internal static void WriteAt(this string s, int x, int y, Color color, Color bgColor)
  {
    try
    {
      Console.SetCursorPosition(x, y);
      ForegroundColor = color;
      BackgroundColor = bgColor;
      Console.Write(s);
      ResetColor();
    }
    catch (ArgumentOutOfRangeException e)
    {
      Clear();
      Console.WriteLine(e.Message);
    }
  }

  internal static void WriteAt(this string s, int x, int y, Color color, Color bgColor, int repeat)
  {
    try
    {
      Console.SetCursorPosition(x, y);
      ForegroundColor = color;
      BackgroundColor = bgColor;
      for (int i = 0; i < repeat; i++)
      {
          Console.Write(s);
      }

      ResetColor();
    }
    catch (ArgumentOutOfRangeException e)
    {
      Clear();
      Console.WriteLine(e.Message);
    }
  }

  internal static void WriteAt(this string s, int x, int y, Color color, int repeat)
  {
    try
    {
      Console.SetCursorPosition(x, y);
      ForegroundColor = color;
      for (int i = 0; i < repeat; i++)
      {
          Console.Write(s);
      }

      ResetColor();
    }
    catch (ArgumentOutOfRangeException e)
    {
      Clear();
      Console.WriteLine(e.Message);
    }
  }

  internal static void WriteAt(this string s, int x, int y, Color color, Color bgColor, int repeat, int delay)
  {
    try
    {
      Console.SetCursorPosition(x, y);
      ForegroundColor = color;
      BackgroundColor = bgColor;
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
#endregion Extended Color WriteAt String Methods

  // WriteBorder Methods
  #region WriteBorder Methods
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
          StringInfo.GetNextTextElement(bChars.Hor).WriteAt(box.Left + 1, box.Top + i, color, box.Width - 2);
        }
        else if (i == box.Height - 1)
        {
          StringInfo.GetNextTextElement(bChars.BotLeft).WriteAt(box.Left, box.Top + i, color);
          StringInfo.GetNextTextElement(bChars.BotRight).WriteAt(box.Left + box.Width - 1, box.Top + i, color);
          StringInfo.GetNextTextElement(bChars.Hor).WriteAt(box.Left + 1, box.Top + i, color, box.Width - 2);
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

  internal static void WriteBorder(this Box box, BoxChars bChars, ConsoleColor color, ConsoleColor bgColor)
  {
    try
    {
      for (int i = 0; i < box.Height; i++)
      {
        if (i == 0)
        {
          StringInfo.GetNextTextElement(bChars.TopLeft).WriteAt(box.Left, box.Top + i, color, bgColor);
          StringInfo.GetNextTextElement(bChars.TopRight).WriteAt(box.Left + box.Width - 1, box.Top + i, color, bgColor);
          StringInfo.GetNextTextElement(bChars.Hor).WriteAt(box.Left + 1, box.Top + i, color, bgColor, box.Width - 2);
        }
        else if (i == box.Height - 1)
        {
          StringInfo.GetNextTextElement(bChars.BotLeft).WriteAt(box.Left, box.Top + i, color, bgColor);
          StringInfo.GetNextTextElement(bChars.BotRight).WriteAt(box.Left + box.Width - 1, box.Top + i, color, bgColor);
          StringInfo.GetNextTextElement(bChars.Hor).WriteAt(box.Left + 1, box.Top + i, color, bgColor, box.Width - 2);
        }
        else
        {
          StringInfo.GetNextTextElement(bChars.Ver).WriteAt(box.Left, box.Top + i, color, bgColor);
          StringInfo.GetNextTextElement(bChars.Ver).WriteAt(box.Left + box.Width - 1, box.Top + i, color, bgColor);
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
          StringInfo.GetNextTextElement(bChars.Hor).WriteAt(box.Left + 1, box.Top, color, Color.Black, box.Width - 2);
        }
        else if (i == box.Height - 1)
        {
          StringInfo.GetNextTextElement(bChars.BotLeft).WriteAt(box.Left, box.Top + i, color);
          StringInfo.GetNextTextElement(bChars.BotRight).WriteAt(box.Left + box.Width - 1, box.Top + i, color);
          StringInfo.GetNextTextElement(bChars.Hor).WriteAt(box.Left + 1, box.Top + i, color, Color.Black, box.Width - 2);
        }
        else
        {
          StringInfo.GetNextTextElement(bChars.Ver).WriteAt(box.Left, box.Top + i, color);
          StringInfo.GetNextTextElement(bChars.Ver).WriteAt(box.Left + box.Width - 1, box.Top + i, color);
        }
      }

      ResetColor();
    }
    catch (ArgumentOutOfRangeException e)
    {
      Clear();
      Console.WriteLine(e.Message);
    }
  }
  
  internal static void WriteBorder(this Box box, BoxChars bChars, Color color, Color bgColor)
  {
    try
    {
      for (int i = 0; i < box.Height; i++)
      {
        if (i == 0)
        {
          StringInfo.GetNextTextElement(bChars.TopLeft).WriteAt(box.Left, box.Top + i, color, bgColor);
          StringInfo.GetNextTextElement(bChars.TopRight).WriteAt(box.Left + box.Width - 1, box.Top + i, color, bgColor);
          StringInfo.GetNextTextElement(bChars.Hor).WriteAt(box.Left + 1, box.Top, color, bgColor, box.Width - 2);
        }
        else if (i == box.Height - 1)
        {
          StringInfo.GetNextTextElement(bChars.BotLeft).WriteAt(box.Left, box.Top + i, color, bgColor);
          StringInfo.GetNextTextElement(bChars.BotRight).WriteAt(box.Left + box.Width - 1, box.Top + i, color, bgColor);
          StringInfo.GetNextTextElement(bChars.Hor).WriteAt(box.Left + 1, box.Top + i, color, bgColor, box.Width - 2);
        }
        else
        {
          StringInfo.GetNextTextElement(bChars.Ver).WriteAt(box.Left, box.Top + i, color, bgColor);
          StringInfo.GetNextTextElement(bChars.Ver).WriteAt(box.Left + box.Width - 1, box.Top + i, color, bgColor);
        }
      }

      ResetColor();
    }
    catch (ArgumentOutOfRangeException e)
    {
      Clear();
      Console.WriteLine(e.Message);
    }
  }
  #endregion WriteBorder Methods

  // Read Methods
  internal static int ReadInt(int x, int y, Color fgColor, Color bgColor)
  {
    ForegroundColor = fgColor;
    BackgroundColor = bgColor;
    bool valid = false;
    int result = -1;
    while (!valid)
    {
      Console.SetCursorPosition(x, y);
      ConsoleKeyInfo keyInfo = Console.ReadKey(true);
      valid = int.TryParse(keyInfo.KeyChar.ToString(), out result);
      if (valid) continue;
      Console.SetCursorPosition(x, y);
      Console.Write(" ");
    }
    ResetColor();
    return result;
  }

  internal static bool ReadBool(int x, int y, Color fgColor, Color bgColor)
  {
    ForegroundColor = fgColor;
    BackgroundColor = bgColor;
    bool valid = false;
    bool result = false;
    while (!valid)
    {
      Console.SetCursorPosition(x, y);
      ConsoleKeyInfo keyInfo = Console.ReadKey(true);
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
}