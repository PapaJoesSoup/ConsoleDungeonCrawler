namespace ConsoleDungeonCrawler.Game.Entities;

/// <summary>
/// This is the class for Extended ScreenBox Characters
/// </summary>
internal class BoxChars
{
    internal string TopLeft;
    internal string TopCtr;
    internal string TopRight;
    internal string MidLeft;
    internal string MidCtr;
    internal string MidRight;
    internal string BotLeft;
    internal string BotCtr;
    internal string BotRight;
    internal string Hor;
    internal string Ver;

    // Number of properties. Useful in ThemeConfig.
    internal const int Count = 11;

    // These are unicode values for box drawing characters.   Expects Console.OutputEncoding = Encoding.Unicode and Consolas font selected in Terminal Settings.
    // Note that font settings cannot be changed in code, so the user must do this manually in the terminal app.
    // refer to: https://www.fileformat.info/info/unicode/font/consolas/grid.htm for a grid of all characters
    internal static readonly BoxChars Default = new("\u2554", "\u2566", "\u2557", "\u2560", "\u256c", "\u2563", "\u255a", "\u2569", "\u255d", "\u2550", "\u2551");

    private BoxChars(string topLeft, string topCtr, string topRight, string midLeft, string midCtr, string midRight, string botLeft, string botCtr, string botRight, string hor, string ver)
    {
        TopLeft = topLeft;
        TopCtr = topCtr;
        TopRight = topRight;
        MidLeft = midLeft;
        MidCtr = midCtr;
        MidRight = midRight;
        BotLeft = botLeft;
        BotCtr = botCtr;
        BotRight = botRight;
        Hor = hor;
        Ver = ver;
    }
}