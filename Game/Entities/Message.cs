using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Screens;

namespace ConsoleDungeonCrawler.Game.Entities
{
  internal class Message
  {
    internal string Text = "";
    internal Color ForegroundColor = Color.White;
    internal Color BackgroundColor = Color.Black;

    internal Message()
    {

    }

    internal Message(string text, Color foregroundColor, Color backgroundColor)
    {
      Text = text;
      ForegroundColor = foregroundColor;
      BackgroundColor = backgroundColor;
    }

    public Message(string text) : this()
    {
      Text = text;
    }

    internal void WriteAt(int x, int y)
    {
      ConsoleEx.WriteAt(Text.PadRight(GamePlayScreen.MessageBox.Width - 3), x, y, ForegroundColor, BackgroundColor);
    }
  }
}
