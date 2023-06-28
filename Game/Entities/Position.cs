namespace ConsoleDungeonCrawler.Game.Entities;

internal class Position
{
  internal int X { get; set; }
  internal int Y { get; set; }

  internal Position West => new(X - 1, Y);
  internal Position East => new(X + 1, Y);
  internal Position North => new(X, Y - 1);
  internal Position South => new(X, Y + 1);


  // Used for pathfinding
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

  internal Position(int x, int y, Position parent, int Cost)
  {
    X = x;
    Y = y;
    Parent = parent;
    this.Cost = Cost;
  }

  internal int GetDistance(Position target)
  {
    return Math.Abs(target.X - X) + Math.Abs(target.Y - Y);
  }

  internal void SetDistance(Position target)
  {
    Distance = Math.Abs(target.X - X) + Math.Abs(target.Y - Y);
  }

  internal static Direction GetDirection(Position from, Position to)
  {
    if (from.X < to.X) return Direction.East;
    if (from.X > to.X) return Direction.West;
    if (from.Y < to.Y) return Direction.South;
    if (from.Y > to.Y) return Direction.North;
    return Direction.None;
  }
}