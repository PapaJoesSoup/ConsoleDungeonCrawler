namespace ConsoleDungeonCrawler.Game.Entities;

internal class Position
{
  internal int X { get; set; }
  internal int Y { get; set; }
  internal Dictionary<Direction, Position> AdjPos => new()
  {
    {Direction.North, new Position(X, Y - 1)},
    {Direction.NorthEast, new Position(X + 1, Y - 1)},
    {Direction.East, new Position(X + 1, Y)},
    {Direction.SouthEast, new Position(X + 1, Y + 1)},
    {Direction.South, new Position(X, Y + 1)},
    {Direction.SouthWest, new Position(X - 1, Y + 1)},
    {Direction.West, new Position(X - 1, Y)},
    {Direction.NorthWest, new Position(X - 1, Y - 1)}
  };

  // These are used for path finding
  internal int Cost;
  private int distance;
  internal int CostDistance => Cost + distance;
  internal Position? Parent;

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
    distance = Math.Abs(target.X - X) + Math.Abs(target.Y - Y);
  }
}