namespace ConsoleDungeonCrawler.Extensions
{
    internal class Box
    {
        internal int Height = 0;
        internal int Width = 0;
        internal int Left = 0;
        internal int Top = 0;

        internal Box()
        {
        }

        internal Box(int left, int top, int width, int height)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }
    }
    internal class BoxChars
    {
        internal char topLeft = ' ';
        internal char topRight = ' ';
        internal char botLeft = ' ';
        internal char botRight = ' ';
        internal char hor = ' ';
        internal char ver = ' ';

        internal BoxChars()
        {
        }

        internal BoxChars(char topLeft, char topRight, char botLeft, char botRight, char hor, char ver)
        {
            this.topLeft = topLeft;
            this.topRight = topRight;
            this.botLeft = botLeft;
            this.botRight = botRight;
            this.hor = hor;
            this.ver = ver;
        }
    }

    internal class BoxCharsEx
    {
        internal string topLeft = " ";
        internal string topRight = " ";
        internal string botLeft = " ";
        internal string botRight = " ";
        internal string hor = " ";
        internal string ver = " ";
        internal BoxCharsEx() { }

        internal BoxCharsEx(string topLeft, string topRight, string botLeft, string botRight, string hor, string ver)
        {
            this.topLeft = topLeft;
            this.topRight = topRight;
            this.botLeft = botLeft;
            this.botRight = botRight;
            this.hor = hor;
            this.ver = ver;
        }
    }

}
