namespace ConsoleDungeonCrawler.Extensions
{
  internal static class StringEx
  {

    internal static string PadCenter(this string text, int width)
    {
      int totalPad = width - text.Length;
      int padLeft = totalPad / 2 + text.Length;
      return text.PadLeft(padLeft).PadRight(width);
    }
  }
}
