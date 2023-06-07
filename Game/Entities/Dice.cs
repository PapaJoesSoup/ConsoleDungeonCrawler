
namespace ConsoleDungeonCrawler.Game.Entities
{
  internal static class Dice
  {
    private static Random _random = new Random();

    /// <summary>
    /// Returns a random number between 1 and sides
    /// </summary>
    /// <param name="sides"></param>
    /// <returns></returns>
    internal static int Roll(int sides)
    {
      return _random.Next(1, sides + 1);
    }

    /// <summary>
    /// Returns a random number between minimum number and sides
    /// </summary>
    /// <param name="min"></param>
    /// <param name="sides"></param>
    /// <returns></returns>
    internal static int Roll(int min, int sides)
    {
      return _random.Next(min, sides + 1);
    }
  }
}
