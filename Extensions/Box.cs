using System.Drawing;
using System.Globalization;

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

    // WriteBorder Methods
    internal void Draw(char h, char v, ConsoleColor color)
    {
      try
      {
        for (int i = 0; i < Height; i++)
        {
          if (i == 0 || i == Height - 1)
          {
            ConsoleEx.WriteAt(h, Left, Top + i, color, Width);
          }
          else
          {
            ConsoleEx.WriteAt(v, Left, Top + i, color);
            ConsoleEx.WriteAt(v, Left + Width - 1, Top + i, color);
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

    internal void Draw(BoxChars bChars, ConsoleColor color)
    {
      try
      {
        for (int i = 0; i < Height; i++)
        {
          if (i == 0)
          {
            ConsoleEx.WriteAt(bChars.topLeft, Left, Top + i, color);
            ConsoleEx.WriteAt(bChars.topRight, Left + Width - 1, Top + i, color);
            ConsoleEx.WriteAt(bChars.hor, Left + 1, Top + i, color, Width - 2);
          }
          else if (i == Height - 1)
          {
            ConsoleEx.WriteAt(bChars.botLeft, Left, Top + i, color);
            ConsoleEx.WriteAt(bChars.botRight, Left + Width - 1, Top + i, color);
            ConsoleEx.WriteAt(bChars.hor, Left + 1, Top + i, color, Width - 2);
          }
          else
          {
            ConsoleEx.WriteAt(bChars.ver, Left, Top + i, color);
            ConsoleEx.WriteAt(bChars.ver, Left + Width - 1, Top + i, color);
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
            ConsoleEx.WriteAt(bChars.topLeft, Left, Top + i, color, backgroundColor);
            ConsoleEx.WriteAt(bChars.topRight, Left + Width - 1, Top + i, color, backgroundColor);
            ConsoleEx.WriteAt(bChars.hor.ToString(), Left + 1, Top + i, color, backgroundColor, 0, Width - 2);
          }
          else if (i == Height - 1)
          {
            ConsoleEx.WriteAt(bChars.botLeft, Left, Top + i, color, backgroundColor);
            ConsoleEx.WriteAt(bChars.botRight, Left + Width - 1, Top + i, color, backgroundColor);
            ConsoleEx.WriteAt(bChars.hor.ToString(), Left + 1, Top + i, color, backgroundColor, 0, Width - 2);
          }
          else
          {
            ConsoleEx.WriteAt(bChars.ver, Left, Top + i, color);
            ConsoleEx.WriteAt(bChars.ver, Left + Width - 1, Top + i, color, backgroundColor);
          }
        }

        // fill inside of box with fill color
        for (int i = 1; i < Height - 1; i++)
        {
          ConsoleEx.WriteAt(" ", Left + 1, Top + i, fillColor, fillColor, 0, Width - 2);
        }

        Console.ResetColor();
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }

    internal void Draw(BoxCharsEx bChars, ConsoleColor color)
    {
      try
      {
        for (int i = 0; i < Height; i++)
        {
          if (i == 0)
          {
            ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.topLeft), Left, Top + i, color);
            ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.topRight), Left + Width - 1, Top + i, color);
            ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.hor), Left + 1, Top + i, color, Width - 2);
          }
          else if (i == Height - 1)
          {
            ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.botLeft), Left, Top + i, color);
            ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.botRight), Left + Width - 1, Top + i, color);
            ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.hor), Left + 1, Top + i, color, Width - 2);
          }
          else
          {
            ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.ver), Left, Top + i, color);
            ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.ver), Left + Width - 1, Top + i, color);
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

    internal void Draw(BoxCharsEx bChars, Color color, Color backgroundColor, Color fillColor)
    {
      try
      {
        for (int i = 0; i < Height; i++)
        {
          if (i == 0)
          {
            ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.topLeft), Left, Top + i, color, backgroundColor);
            ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.topRight), Left + Width - 1, Top + i, color, backgroundColor);
            ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.hor), Left + 1, Top + i, color, Width - 2);
          }
          else if (i == Height - 1)
          {
            ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.botLeft), Left, Top + i, color, backgroundColor);
            ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.botRight), Left + Width - 1, Top + i, color, backgroundColor);
            ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.hor), Left + 1, Top + i, color, backgroundColor, 0, Width - 2);
          }
          else
          {
            ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.ver), Left, Top + i, color);
            ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.ver), Left + Width - 1, Top + i, color, backgroundColor);
          }
        }

        // fill inside of box with fill color
        for (int i = 1; i < Height - 1; i++)
        {
          ConsoleEx.WriteAt(StringInfo.GetNextTextElement(bChars.hor), Left + 1, Top + i, fillColor, Width - 2);
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
