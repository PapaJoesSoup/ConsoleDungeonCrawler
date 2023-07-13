using System.Drawing;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;

namespace ConsoleDungeonCrawler.Game.Screens.Dialogs;

internal static class GamePaused
{
    internal static readonly Colors Colors = new();
    internal static void Draw()
    {
        Box box = new(Dialog.ScreenCenter, 100, 20);
        box.Draw(BoxChars.Default, Colors.Color, Colors.BackgroundColor, Colors.FillColor);
        "Game Paused".WriteAlignedAt(HAlign.Center, VAlign.Middle, Colors.TextColor, Colors.FillColor);
        "Press any key to continue".WriteAlignedAt(HAlign.Center, VAlign.Middle, Colors.SelectedColor, Colors.FillColor, 0, 2);
        Console.ReadKey(true);
        Game.IsPaused = false;
        ConsoleEx.Clear();
        GamePlay.Draw();
    }
}