namespace ConsoleDungeonCrawler.Game.Entities;

internal class Position
{
  internal int X { get; set; }
  internal int Y { get; set; }
  internal Dictionary<Direction, Position> Dir => new()
  {
    {Direction.North, new(X, Y - 1)},
    {Direction.NorthEast, new(X + 1, Y - 1)},
    {Direction.East, new(X + 1, Y)},
    {Direction.SouthEast, new(X + 1, Y + 1)},
    {Direction.South, new(X, Y + 1)},
    {Direction.SouthWest, new(X - 1, Y + 1)},
    {Direction.West, new(X - 1, Y)},
    {Direction.NorthWest, new(X - 1, Y - 1)}
  };

  // These are used for path finding
  internal int Cost { get; set; }
  private int Distance { get; set; }
  internal int CostDistance => Cost + Distance;
  internal Position? Parent = null;

  internal Position()
  {
  }

  internal Position(int x, int y)
  {
    X = x;
    Y = y;
  }

  internal Position(int x, int y, Position parent, int cost)
  {
    X = x;
    Y = y;
    Parent = parent;
    this.Cost = cost;
  }

  internal int GetDistance(Position target)
  {
    return Math.Abs(target.X - X) + Math.Abs(target.Y - Y);
  }

  internal void SetDistance(Position target)
  {
    Distance = Math.Abs(target.X - X) + Math.Abs(target.Y - Y);
  }
}