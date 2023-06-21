using System.Drawing;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Screens;

namespace ConsoleDungeonCrawler.Game.Entities
{
  internal class Message
  {
    internal readonly string Text;
    internal Color ForegroundColor = Color.White;
    internal Color BackgroundColor = Color.Black;

    internal Message(string text, Color foregroundColor, Color backgroundColor)
    {
      Text = $"{DateTime.Now:MM/dd/y HH:mm:ss} - {text}";
      ForegroundColor = foregroundColor;
      BackgroundColor = backgroundColor;
    }

    public Message(string text)
    {
      Text = $"{DateTime.Now:MM/dd/y HH:mm:ss} - {text}";
    }
    internal void WriteMessageAt(int x, int y)
    {
      Text.PadRight(GamePlay.MessageWidth).WriteAt(x, y, ForegroundColor, BackgroundColor);
    }
  }
}
