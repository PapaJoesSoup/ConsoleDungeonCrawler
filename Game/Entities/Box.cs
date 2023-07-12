using System.Drawing;
using System.Globalization;
using ConsoleDungeonCrawler.Extensions;

namespace ConsoleDungeonCrawler.Game.Entities;

/// <summary>
/// This is the class for drawing box characters
/// </summary>
internal class Box
{
    internal readonly int Height;
    internal readonly int Width;
    internal readonly int Left;
    internal readonly int Top;

    internal Box(int left, int top, int width, int height)
    {
        Left = left;
        Top = top;
        Width = width;
        Height = height;
    }

    // WriteBorder Methods
    internal void Draw(BoxChars bChars, Color color, Color backgroundColor, Color fillColor)
    {
        try
        {
            for (int i = 0; i < Height; i++)
            {
                if (i == 0)
                {
                    StringInfo.GetNextTextElement(bChars.TopLeft).WriteAt(Left, Top + i, color, backgroundColor);
                    StringInfo.GetNextTextElement(bChars.TopRight).WriteAt(Left + Width - 1, Top + i, color, backgroundColor);
                    StringInfo.GetNextTextElement(bChars.Hor).WriteAt(Left + 1, Top + i, color, backgroundColor, Width - 2, 0);
                }
                else if (i == Height - 1)
                {
                    StringInfo.GetNextTextElement(bChars.BotLeft).WriteAt(Left, Top + i, color, backgroundColor);
                    StringInfo.GetNextTextElement(bChars.BotRight).WriteAt(Left + Width - 1, Top + i, color, backgroundColor);
                    StringInfo.GetNextTextElement(bChars.Hor).WriteAt(Left + 1, Top + i, color, backgroundColor, Width - 2, 0);
                }
                else
                {
                    StringInfo.GetNextTextElement(bChars.Ver).WriteAt(Left, Top + i, color, backgroundColor);
                    StringInfo.GetNextTextElement(bChars.Ver).WriteAt(Left + Width - 1, Top + i, color, backgroundColor);
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