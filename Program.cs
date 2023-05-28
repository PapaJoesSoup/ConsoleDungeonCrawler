using System.Runtime.InteropServices;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.GameData;

namespace ConsoleDungeonCrawler
{
  public static class Program
  {
    internal static Map? map;
    private struct Rect
    {
      public int Left;
      public int Top;
      public int Right;
      public int Bottom;
    }

    public static void Main(string[] args)
    {
      // Maximize console window
      MaximizeConsoleWindow();
      // Enable extended colors
      EnableExtendedColors();

      ConsoleEx.ScreenHeight = 70;
      ConsoleEx.ScreenWidth = 200;

      map = new Map();
      Game.Run();
    }

    private static void EnableExtendedColors()
    {
      // Setup console to use extended ansii colors
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
      const int SW_MAXIMIZE = 3;
      IntPtr consoleWindowHandle = GetForegroundWindow();
      ShowWindow(consoleWindowHandle, SW_MAXIMIZE);

      // Get the screen size
      Rect screenRect;
      GetWindowRect(consoleWindowHandle, out screenRect);
      // Resize and reposition the console window to fill the screen
      int width = screenRect.Right - screenRect.Left;
      int height = screenRect.Bottom - screenRect.Top;
      MoveWindow(consoleWindowHandle, screenRect.Left, screenRect.Top, width, height, true);
    }

    private static void PrintColorsEx()
    {
      // Print all 256 colors
      for (int i = 0; i < 255; i++)
      {
        Console.Write($"{ConsoleColorEx.bgColor}{i}m*");
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


