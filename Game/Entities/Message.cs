using System.Drawing;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Screens;

namespace ConsoleDungeonCrawler.Game.Entities
{
  internal class Message
  {
    private readonly string text;
    private readonly Color foregroundColor = Color.White;
    private readonly Color backgroundColor = Color.Black;

    internal Message(string text, Color fgColor, Color bgColor)
    {
      this.text = $"{DateTime.Now:MM/dd/y HH:mm:ss} - {text}";
      this.foregroundColor = fgColor;
      this.backgroundColor = bgColor;
    }

    public Message(string text)
    {
      this.text = $"{DateTime.Now:MM/dd/y HH:mm:ss} - {text}";
    }

    internal void WriteMessageAt(int x, int y)
    {
      text.PadRight(GamePlay.MessageWidth).WriteAt(x, y, foregroundColor, backgroundColor);
    }
  }
}
