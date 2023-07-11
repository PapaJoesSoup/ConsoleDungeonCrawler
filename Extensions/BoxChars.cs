namespace ConsoleDungeonCrawler.Extensions;

/// <summary>
/// This is the class for Extended Box Characters
/// </summary>
internal class BoxChars
{
  internal readonly string TopLeft;
  internal readonly string TopRight;
  internal readonly string BotLeft;
  internal readonly string BotRight;
  internal readonly string Hor;
  internal readonly string Ver;
  internal readonly string MidLeft;
  internal readonly string MidRight;
  internal readonly string MidTop;
  internal readonly string MidBottom;
  internal readonly string Mid;

  // These are unicode values for box drawing characters.   Expects Console.OutputEncoding = Encoding.Unicode and Consolas font selected in Terminal Settings.
  // Note that font settings cannot be changed in code, so the user must do this manually in the terminal app.
  // refer to: https://www.fileformat.info/info/unicode/font/consolas/grid.htm for a grid of all characters
  internal static readonly BoxChars Default = new("\u2554", "\u2557", "\u255a", "\u255d", "\u2550", "\u2551", "\u2560",
    "\u2563", "\u2566", "\u2569", "\u256c");

  private BoxChars(string topLeft, string topRight, string botLeft, string botRight, string hor, string ver,
    string midLeft, string midRight, string midTop, string midBottom, string mid)
  {
    TopLeft = topLeft;
    TopRight = topRight;
    BotLeft = botLeft;
    BotRight = botRight;
    Hor = hor;
    Ver = ver;
    MidLeft = midLeft;
    MidRight = midRight;
    MidTop = midTop;
    MidBottom = midBottom;
    Mid = mid;
  }
}