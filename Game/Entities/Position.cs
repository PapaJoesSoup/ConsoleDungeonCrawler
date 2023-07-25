using ConsoleDungeonCrawler.Game.Screens;

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
  internal readonly int Cost;
  private int distance;
  internal int CostDistance => Cost + distance;
  internal readonly Position? Parent;

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

  internal bool IsNextToMapGrid(char symbol, out Tile obj)
  {
    if (IsNextToMap(symbol, AdjPos[Direction.West], out obj)) return true;
    if (IsNextToMap(symbol, AdjPos[Direction.East], out obj)) return true;
    if (IsNextToMap(symbol, AdjPos[Direction.North], out obj)) return true;
    if (IsNextToMap(symbol, AdjPos[Direction.South], out obj)) return true;

    // not found
    obj = new Tile();
    return false;
  }

  internal char IsNextToOverlayGrid(out Tile obj)
  {
    // we need to account for monsters on a different overlay level
    if (IsNextToOverlay(AdjPos[Direction.West], out obj)) return obj.Type.Symbol;
    if (IsNextToOverlay(AdjPos[Direction.East], out obj)) return obj.Type.Symbol;
    if (IsNextToOverlay(AdjPos[Direction.North], out obj)) return obj.Type.Symbol;
    if (IsNextToOverlay(AdjPos[Direction.South], out obj)) return obj.Type.Symbol;

    // not found
    obj = new Tile();
    return ' ';
  }

  internal bool IsNextToOverlayGrid(char symbol, out Tile obj)
  {
    // we need to account for monsters on a different overlay level
    if (IsNextToOverlay(AdjPos[Direction.West], out obj, symbol)) return true;
    if (IsNextToOverlay(AdjPos[Direction.East], out obj, symbol)) return true;
    if (IsNextToOverlay(AdjPos[Direction.North], out obj, symbol)) return true;
    if (IsNextToOverlay(AdjPos[Direction.South], out obj, symbol)) return true;

    // not found
    obj = new Tile();
    return false;
  }

  internal bool IsInRange(int radius, out Tile obj)
  {
    // find the closest object within the radius
    for (int i = 1; i <= radius; i++)
    {
      Position left = new(X - i, Y);
      Position right = new(X + i, Y);
      Position up = new(X, Y - i);
      Position down = new(X, Y + i);

      if (IsNextToMap(left, out obj)) return true;
      if (IsNextToMap(right, out obj)) return true;
      if (IsNextToMap(up, out obj)) return true;
      if (IsNextToMap(down, out obj)) return true;
    }

    // not found
    obj = new Tile();
    return false;
  }

  internal static bool CanMoveTo(Position pos)
  {
    // check to see if there is an object there that is not passable
    for (int layer = Map.CurrentOverlay[pos.X][pos.Y].Count - 1; layer >= 0; layer--)
      if (!Map.CurrentOverlay[pos.X][pos.Y][layer].IsPassable) return false;
    return Map.CurrentMap[pos.X][pos.Y].IsPassable;
  }

  internal static bool CanJumpTo(Position oldPos, Position newPos)
  {
    // check to see if there is an object in between old and new location that is not passable and not transparent
    Direction dir = Map.GetDirection(oldPos, newPos);
    Position curPos = oldPos;
    if (dir == Direction.West) curPos = oldPos.AdjPos[Direction.West];
    if (dir == Direction.East) curPos = oldPos.AdjPos[Direction.East];
    if (dir == Direction.North) curPos = oldPos.AdjPos[Direction.North];
    if (dir == Direction.South) curPos = oldPos.AdjPos[Direction.South];
    if (curPos == oldPos) return false;

    List<Tile> layers = Map.CurrentOverlay[curPos.X][curPos.Y];
    for (int i = layers.Count - 1; i >= 0; i--)
      if (!layers[i].IsPassable) return false;

    Tile mapObj = Map.CurrentMap[curPos.X][curPos.Y];
    return mapObj is not { IsPassable: false, Type.IsTransparent: false } && CanMoveTo(newPos);
  }

  private bool IsNextToOverlay(Position pos, out Tile obj, char symbol = char.MinValue)
  {
    obj = new Tile();
    if (pos is not { X: > 0, Y: > 0 } || pos.X > GamePlay.MapBox.Width || pos.Y > GamePlay.MapBox.Height) return false;
    // if we have any live monsters, we want to get them first, so we work our way down from the top layer.
    for (int layer = Map.CurrentOverlay[pos.X][pos.Y].Count - 1; layer >= 0; layer--)
    {
      if (symbol != char.MinValue)
      {
        if (Map.CurrentOverlay[pos.X][pos.Y][layer].Type.Symbol != symbol) continue;
      }
      else if (Map.CurrentOverlay[pos.X][pos.Y][layer].Type.Symbol == ' ') continue;

      obj = Map.CurrentOverlay[pos.X][pos.Y][layer];
      if (obj is Monster { IsAlive: false }) continue;
      return true;
    }

    // not found
    return false;
  }

  private bool IsNextToMap(char symbol, Position pos, out Tile obj)
  {
    if (pos is { X: > 0, Y: > 0 } && pos.X <= GamePlay.MapBox.Width && pos.Y <= GamePlay.MapBox.Height)
    {
      if (Map.CurrentMap[pos.X][pos.Y].Type.Symbol == symbol)
      {
        obj = Map.CurrentMap[pos.X][pos.Y];
        return true;
      }
    }

    obj = new Tile();
    return false;
  }

  private bool IsNextToMap(Position pos, out Tile obj)
  {
    if (pos is { X: > 0, Y: > 0 } && pos.X <= GamePlay.MapBox.Width && pos.Y <= GamePlay.MapBox.Height)
    {
      if (Map.CurrentMap[pos.X][pos.Y].Type.Symbol != ' ')
      {
        obj = Map.CurrentMap[pos.X][pos.Y];
        return true;
      }
    }

    obj = new Tile();
    return false;
  }
}