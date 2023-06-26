namespace ConsoleDungeonCrawler.Game.Entities;

internal class Position
{
  internal int X { get; set; }
  internal int Y { get; set; }

  // Used for pathfinding
  internal int Cost { get; set; }
  private int Distance { get; set; }
  internal int CostDistance => Cost + Distance;
  internal Position? Parent = null;


  internal Position(int x, int y)
  {
    X = x;
    Y = y;
  }

  internal Position()
  {

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