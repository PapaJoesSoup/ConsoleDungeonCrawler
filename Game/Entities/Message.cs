using System.Drawing;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Screens;

namespace ConsoleDungeonCrawler.Game.Entities;

internal class Message
{
  private readonly string text;
  private readonly Item? item;
  private readonly Color foregroundColor = Color.White;
  private readonly Color backgroundColor = Color.Black;
  internal static readonly int PageSize = GamePlay.MessageBox.Height - 2;

  internal Message(string text, Color fgColor, Color bgColor)
  {
    this.text = $"{DateTime.Now:MM/dd/y HH:mm:ss} - {text}";
    foregroundColor = fgColor;
    backgroundColor = bgColor;
  }

  internal Message(string text, Item item, Color fgColor, Color bgColor)
  {
    this.text = $"{DateTime.Now:MM/dd/y HH:mm:ss} - {text}";
    foregroundColor = fgColor;
    backgroundColor = bgColor;
    this.item = item;
  }

  public Message(string text)
  {
    this.text = $"{DateTime.Now:MM/dd/y HH:mm:ss} - {text}";
  }

  internal void WriteMessageAt(int x, int y)
  {
    if (this.item == null)
      text.PadRight(GamePlay.MessageWidth).WriteAt(x, y, foregroundColor, backgroundColor);
    else
    {
      // Lets get the item from the text string...
      string itemText = $" {item.Description}";
      string endText = "!";
      text.WriteAt(x, y, foregroundColor, backgroundColor);
      itemText.WriteAt(x + text.Length, y, ColorEx.RarityColor(item.Rarity), backgroundColor);
      endText.PadRight(GamePlay.MessageWidth - text.Length - itemText.Length).WriteAt(x + text.Length + itemText.Length, y, foregroundColor, backgroundColor);
    }
  }
}