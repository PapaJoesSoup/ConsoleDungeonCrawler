using System.Runtime.InteropServices;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;

namespace ConsoleDungeonCrawler
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      // Setup console to support unicode characters
      ConsoleEx.InitializeConsole();
      Game.Game.Run();
    }
  }
}


