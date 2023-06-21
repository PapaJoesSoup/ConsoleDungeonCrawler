using System.Drawing;
using System.Globalization;

namespace ConsoleDungeonCrawler.Extensions
{
  /// <summary>
  /// This is the class for drawing box characters
  /// </summary>
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

    // WriteBorder Methods
    internal void Draw(BoxChars bChars, ConsoleColor color)
    {
      try
      {
        for (int i = 0; i < Height; i++)
        {
          if (i == 0)
          {
            StringInfo.GetNextTextElement(bChars.topLeft).WriteAt(Left, Top + i, color);
            StringInfo.GetNextTextElement(bChars.topRight).WriteAt(Left + Width - 1, Top + i, color);
            StringInfo.GetNextTextElement(bChars.hor).WriteAt(Left + 1, Top + i, color, Width - 2);
          }
          else if (i == Height - 1)
          {
            StringInfo.GetNextTextElement(bChars.botLeft).WriteAt(Left, Top + i, color);
            StringInfo.GetNextTextElement(bChars.botRight).WriteAt(Left + Width - 1, Top + i, color);
            StringInfo.GetNextTextElement(bChars.hor).WriteAt(Left + 1, Top + i, color, Width - 2);
          }
          else
          {
            StringInfo.GetNextTextElement(bChars.ver).WriteAt(Left, Top + i, color);
            StringInfo.GetNextTextElement(bChars.ver).WriteAt(Left + Width - 1, Top + i, color);
          }
        }

        Console.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }
    
    internal void Draw(BoxChars bChars, Color color, Color backgroundColor, Color fillColor)
    {
      try
      {
        for (int i = 0; i < Height; i++)
        {
          if (i == 0)
          {
            StringInfo.GetNextTextElement(bChars.topLeft).WriteAt(Left, Top + i, color, backgroundColor);
            StringInfo.GetNextTextElement(bChars.topRight).WriteAt(Left + Width - 1, Top + i, color, backgroundColor);
            StringInfo.GetNextTextElement(bChars.hor).WriteAt(Left + 1, Top + i, color, backgroundColor, Width - 2, 0);
          }
          else if (i == Height - 1)
          {
            StringInfo.GetNextTextElement(bChars.botLeft).WriteAt(Left, Top + i, color, backgroundColor);
            StringInfo.GetNextTextElement(bChars.botRight).WriteAt(Left + Width - 1, Top + i, color, backgroundColor);
            StringInfo.GetNextTextElement(bChars.hor).WriteAt(Left + 1, Top + i, color, backgroundColor, Width - 2, 0);
          }
          else
          {
            StringInfo.GetNextTextElement(bChars.ver).WriteAt(Left, Top + i, color);
            StringInfo.GetNextTextElement(bChars.ver).WriteAt(Left + Width - 1, Top + i, color, backgroundColor);
          }
        }

        // fill inside of box with fill color
        for (int i = 1; i < Height - 1; i++)
        {
          ' '.WriteAt(Left + 1, Top + i, fillColor, fillColor, Width - 2);
        }

        Console.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }
  }

  /// <summary>
  /// This is the class for Extended Box Characters
  /// </summary>
  internal class BoxChars
  {
    internal string topLeft = " ";
    internal string topRight = " ";
    internal string botLeft = " ";
    internal string botRight = " ";
    internal string hor = " ";
    internal string ver = " ";
    internal string midLeft = " ";
    internal string midRight = " ";
    internal string midTop = " ";
    internal string midBottom = " ";
    internal string mid = " ";
    
    internal BoxChars() { }

    internal BoxChars(string topLeft, string topRight, string botLeft, string botRight, string hor, string ver)
    {
      this.topLeft = topLeft;
      this.topRight = topRight;
      this.botLeft = botLeft;
      this.botRight = botRight;
      this.hor = hor;
      this.ver = ver;
    }

    internal BoxChars(string topLeft, string topRight, string botLeft, string botRight, string hor, string ver,
      string midLeft, string midRight, string midTop, string midBottom, string mid)
    {
      this.topLeft = topLeft;
      this.topRight = topRight;
      this.botLeft = botLeft;
      this.botRight = botRight;
      this.hor = hor;
      this.ver = ver;
      this.midLeft = midLeft;
      this.midRight = midRight;
      this.midTop = midTop;
      this.midBottom = midBottom;
      this.mid = mid;
    }
  }

}
