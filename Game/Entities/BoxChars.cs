namespace ConsoleDungeonCrawler.Game.Entities;

/// <summary>
/// This is the class for Extended Box Characters
/// </summary>
internal class BoxChars
{
    internal string TopLeft;
    internal string TopMid;
    internal string TopRight;
    internal string MidLeft;
    internal string Mid;
    internal string MidRight;
    internal string BotLeft;
    internal string BotMid;
    internal string BotRight;
    internal string Hor;
    internal string Ver;

    // Number of properties. Useful in ThemeConfig.
    internal const int Count = 11;

    // These are unicode values for box drawing characters.   Expects Console.OutputEncoding = Encoding.Unicode and Consolas font selected in Terminal Settings.
    // Note that font settings cannot be changed in code, so the user must do this manually in the terminal app.
    // refer to: https://www.fileformat.info/info/unicode/font/consolas/grid.htm for a grid of all characters
    internal static readonly BoxChars Default = new("\u2554", "\u2566", "\u2557", "\u2560", "\u256c", "\u2563", "\u255a", "\u2569", "\u255d", "\u2550", "\u2551");

    private BoxChars(string topLeft, string topMid, string topRight, string midLeft, string mid, string midRight, string botLeft, string botMid, string botRight, string hor, string ver)
    {
        TopLeft = topLeft;
        TopMid = topMid;
        TopRight = topRight;
        MidLeft = midLeft;
        Mid = mid;
        MidRight = midRight;
        BotLeft = botLeft;
        BotMid = botMid;
        BotRight = botRight;
        Hor = hor;
        Ver = ver;
    }
}