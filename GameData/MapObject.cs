using ConsoleDungeonCrawler.Extensions;

namespace ConsoleDungeonCrawler.GameData
{
  internal class MapObject
  {
    // Note:  x and y are in relation to the map area, not the console screen.
    // We will add the x and y position of the map area to these

    /// <summary>
    /// the x coordinate within the map area
    /// </summary>
    internal int X { get; set; }

    /// <summary>
    /// the y coordinate within the map area
    /// </summary>
    internal int Y { get; set; }

    internal ObjectType Type = new ObjectType();

    internal bool Visible = true;

    internal Item Loot = new Item();


    internal MapObject()
    {
    }

    internal MapObject(int x, int y)
    {
      this.X = x;
      this.Y = y;
    }

    internal MapObject(int x, int y, ObjectType type)
    {
      this.X = x;
      this.Y = y;
      Type = type;
    }

    internal MapObject(int x, int y, ObjectType type, bool isVisible)
    {
      this.X = x;
      this.Y = y;
      Type = type;
      Visible = isVisible;
    }

    internal void Draw()
    {
      ConsoleEx.WriteAt(this.Type.Symbol, this.X + Map.Left, this.Y + Map.Top, this.Type.ForegroundColor, this.Type.BackgroundColor);
    }
  }

}
