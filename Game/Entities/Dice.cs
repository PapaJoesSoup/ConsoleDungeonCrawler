
namespace ConsoleDungeonCrawler.Game.Entities;

internal static class Dice
{
  private static readonly Random Random = new();

  /// <summary>
  /// Returns a random number between 1 and sides
  /// </summary>
  /// <param name="sides"></param>
  /// <returns></returns>
  internal static int Roll(int sides)
  {
    return Random.Next(1, sides + 1);
  }

  /// <summary>
  /// Returns a random number between minimum number and sides
  /// </summary>
  /// <param name="min"></param>
  /// <param name="sides"></param>
  /// <returns></returns>
  internal static int Roll(int min, int sides)
  {
    return Random.Next(min, sides + 1);
  }

  /// <summary>
  /// Returns a random  decimal number between minimum number and max number
  /// </summary>
  /// <param name="min"></param>
  /// <param name="max"></param>
  /// <returns></returns>
  internal static decimal Roll(decimal min, decimal max)
  {
    return (decimal)Random.NextDouble() * (max - min) + min;
  }


  /// <summary>
  /// Returns a weighted random number between a min number and a max number
  /// with each number having a weight.  Each number must have a weight.
  /// and the number of numbers must match the number of weights.
  /// </summary>
  /// <param name="numbers">List containing number range</param>
  /// <param name="weights">List containing matching weight for each number</param>
  /// <returns></returns>
  /// <exception cref="ArgumentException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  internal static int WeightedRoll(List<int> numbers, List<int> weights)
  {
    if (numbers.Count != weights.Count)
      throw new ArgumentException("The number of weights must match the number of numbers.");
    int totalWeight = weights.Sum();
    int randomNumber = Random.Next(0, totalWeight);

    for (int i = 0; i < numbers.Count; i++)
    {
      if (randomNumber <= weights[i]) return numbers[i];
      randomNumber -= weights[i];
    }

    // This line should never be reached
    throw new InvalidOperationException("Failed to generate a weighted random number.");
  }
}