namespace ConsoleDungeonCrawler.Game.Entities;

internal static class PathFinding
{
  /// <summary>
  /// This is an A* path finding algorithm 
  /// https://en.wikipedia.org/wiki/A*_search_algorithm
  /// https://gist.github.com/DotNetCoreTutorials/08b0210616769e81034f53a6a420a6d9
  /// https://www.youtube.com/watch?v=-L-WgKMFuhE
  /// </summary>
  /// <param name="start"></param>
  /// <param name="destination"></param>
  /// <returns>a path list of non-subClassed Positions that are passable</returns>
  internal static List<Position> FindPath(Position start, Position destination)
  {
    List<Position> path = new(); // path is the list of positions that lead from the destination to the start
    List<Position> openList = new(); // open list is a list of positions that have not been checked yet
    List<Position> checkedList = new(); // closed list is a list of positions that have been checked

    start.SetDistance(destination);
    openList.Add(start);

    while (openList.Any())
    {
      Position currentPos = openList.OrderByDescending(x => x.CostDistance).Last();
      if (currentPos.X == destination.X && currentPos.Y == destination.Y)
      {
        Position? pathPosition = currentPos;
        while (true) // work backwards from destination to start
        {
          if (!path.Contains(pathPosition)) path.Add(pathPosition);
          pathPosition = pathPosition.Parent;
          if (pathPosition == null) return path;
        }
      }
      checkedList.Add(currentPos);
      openList.Remove(currentPos);

      List<Position>? positions = GetPassableArea(currentPos, destination);
      foreach (Position passablePos in positions)
      {
        //We have already visited this tile so we don't need to do so again!
        if (checkedList.Any(x => x.X == passablePos.X && x.Y == passablePos.Y)) continue;

        //It's already in the open list, but that's OK, maybe this new tile has a better value (e.g. We might zigzag earlier but this is now straighter). 
        if (openList.Any(x => x.X == passablePos.X && x.Y == passablePos.Y))
        {
          Position existingPos = openList.First(x => x.X == passablePos.X && x.Y == passablePos.Y);
          if (existingPos.CostDistance <= currentPos.CostDistance) continue;
          openList.Remove(existingPos);
          openList.Add(passablePos);
        }
        else
        {
          //We've never seen this tile before so add it to the list. 
          openList.Add(passablePos);
        }
      }

    }
    return path;
  }

  /// <summary>
  /// Search the immediate surrounding area for passable positions
  /// </summary>
  /// <param name="map"></param>
  /// <param name="currentPos"></param>
  /// <param name="targetPos"></param>
  /// <returns>adjacent coords that are passable</returns>
  private static List<Position> GetPassableArea(Position currentPos, Position targetPos)
  {
    Dictionary<int, Dictionary<int, Tile>> map = Map.LevelMapGrids[Game.CurrentLevel];
    Dictionary<int, Dictionary<int, List<Tile>>> overlay = Map.LevelOverlayGrids[Game.CurrentLevel];

    // get the 4 adjacent positions (non subClassed to treat them like value types)
    // this ensures the parent position values of each position are not overwritten
    List<Position> possiblePositions = new()
    {
      new Position(currentPos.X, currentPos.Dir[Direction.North].Y, currentPos, currentPos.Cost + 1),
      new Position(currentPos.X, currentPos.Dir[Direction.South].Y, currentPos, currentPos.Cost + 1),
      new Position(currentPos.Dir[Direction.West].X, currentPos.Y, currentPos, currentPos.Cost + 1),
      new Position(currentPos.Dir[Direction.East].X, currentPos.Y, currentPos, currentPos.Cost + 1)
    };

    possiblePositions.ForEach(pos => pos.SetDistance(targetPos));

    int maxX = map.Count - 1;
    int maxY = map[0].Count - 1;

    return possiblePositions
      .Where(pos => pos.X >= 0 && pos.X <= maxX)
      .Where(pos => pos.Y >= 0 && pos.Y <= maxY)
      .Where(pos => Monster.CanMoveTo(pos) || map[pos.X][pos.Y] == targetPos)
      .ToList();
  }
}