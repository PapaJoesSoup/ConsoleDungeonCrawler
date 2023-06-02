using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;

namespace ConsoleDungeonCrawler.Game.Maps
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
            X = x;
            Y = y;
        }

        internal MapObject(int x, int y, ObjectType type)
        {
            X = x;
            Y = y;
            Type = type;
        }

        internal MapObject(int x, int y, ObjectType type, bool isVisible)
        {
            X = x;
            Y = y;
            Type = type;
            Visible = isVisible;
        }

        internal void Draw()
        {
            if (!Visible || Type.Symbol == ' ') return;
            ConsoleEx.WriteAt(Type.Symbol, X + Map.Left, Y + Map.Top, Type.ForegroundColor, Type.BackgroundColor);
        }
    }

}
