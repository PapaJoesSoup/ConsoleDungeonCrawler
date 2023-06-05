
namespace ConsoleDungeonCrawler.Game.Entities
{
  internal static class Dice
  {
    internal static int Roll(int sides)
    {
      return new Random().Next(1, sides + 1);
    }

    internal static int Roll(int numberOfDice, int sides)
    {
      int total = 0;
      for (int i = 0; i < numberOfDice; i++)
      {
        total += Roll(sides);
      }
      return total;
    }

    internal static int Roll(int numberOfDice, int sides, int modifier)
    {
      return Roll(sides, numberOfDice) + modifier;
    }
  }
}
