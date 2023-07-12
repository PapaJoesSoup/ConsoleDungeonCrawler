using ConsoleDungeonCrawler.Extensions;
using System.Drawing;

namespace ConsoleDungeonCrawler.Game.Screens.Dialogs
{
  internal static class BorderConfig
  {
    #region Properties
    private static bool dialogOpen;
    private static readonly Box DialogBox = new(Console.WindowWidth / 2 - 50, Console.WindowHeight / 2 - 12, 100, 25);
    private static readonly Box LegendBox = new(DialogBox.Left + 3, DialogBox.Top + 2, 0, 0);
    private static readonly Box SampleCharBox = new(DialogBox.Left + 37, DialogBox.Top + 2, 0, 0);
    private static readonly Box SampleBox = new(DialogBox.Left + 62, DialogBox.Top + 4, 35, 17);
    private static readonly Box SampleColorBox = new(DialogBox.Left + 3, DialogBox.Top + 15, 0, 0);
    private static readonly Box ColorSelectorBox = new(DialogBox.Left + 37, DialogBox.Top + 17, 0, 0);

    private static readonly Color Color = Color.DarkOrange;
    private static readonly Color BackgroundColor = Color.Black;
    private static readonly Color FillColor = Color.MidnightBlue;
    private static readonly Color HeaderColor = Color.Yellow;
    private static readonly Color TextColor = Color.Bisque;
    private static readonly Color SelectedColor = Color.Lime;
    private static readonly Color SelectedBackgroundColor = Color.DarkOrange;

    private static BoxChars sampleChars = BoxChars.Default;
    private static bool charsActive = true;
    private static int selectedCharIdx = 0;
    private static string selectedChar = string.Empty;

    private static Color sampleColor = Color;
    private static Color sampleBackgroundColor = BackgroundColor;
    private static Color sampleFillColor = Color.SaddleBrown;
    private static Color sampleTextColor = TextColor;
    private static Color sampleSelectedColor = SelectedColor;
    private static Color sampleSelectedBackgroundColor = SelectedBackgroundColor;
    private static Color selectedColor = Color.Black;
    private static int selectedColorIdx = 0;
    private static string strRGB = string.Empty;
    private static int iRed;
    private static int iGreen;
    private static int iBlue;
    #endregion Properties

    internal static void Draw()
    {
      dialogOpen = true;
      Dialog.Draw(" Dialog Configuration ", Color, BackgroundColor, FillColor, TextColor, DialogBox);

      while (dialogOpen)
      {
        DrawLegend();
        DrawSampleChars();
        DrawSampleBox();

        // Order is important here.  Color Box updates Selector data.
        DrawBoxColors();
        DrawColorSelector();

        KeyHandler();
      }
    }

    private static void DrawLegend()
    {
      int x = LegendBox.Left;
      int y = LegendBox.Top;

      "Legend:".WriteAt(x, y, HeaderColor, FillColor); y+=2;

      $"[{ConsoleKey.PageUp}] Border Chars Active".WriteAt(x, y, TextColor, FillColor); y++;
      $"[{ConsoleKey.PageDown}] Char Colors Active".WriteAt(x, y, TextColor, FillColor); y++;
      $"[{ConsoleKey.UpArrow}] Prev Item".WriteAt(x, y, TextColor, FillColor); y++;
      $"[{ConsoleKey.DownArrow}] Next Item".WriteAt(x, y, TextColor, FillColor); y++;
      $"[{ConsoleKey.R}][{ConsoleKey.G}][{ConsoleKey.B}] Edit RGB values".WriteAt(x, y, TextColor, FillColor); y++;
      $"[{ConsoleKey.LeftArrow}] Change Value".WriteAt(x, y, TextColor, FillColor); y++;
      $"[{ConsoleKey.RightArrow}] Change Value".WriteAt(x, y, TextColor, FillColor); y++;
      $"[{ConsoleKey.S}] Save Changes".WriteAt(x, y, TextColor, FillColor); y++;
      $"[{ConsoleKey.Escape}] Close Dialog".WriteAt(x, y, TextColor, FillColor);
    }

    private static void DrawSampleChars()
    {
      int x = SampleCharBox.Left;
      int x2 = SampleCharBox.Left + 21;
      int y = SampleCharBox.Top;
      int i = 0;

      "Border Characters".WriteAt(x, y, charsActive ? SelectedColor : HeaderColor, FillColor); y += 2;

      "Top Left char:".WriteAt(x, y, selectedCharIdx == i ? SelectedColor : TextColor, selectedCharIdx == i ? SelectedBackgroundColor : FillColor);
      $"{sampleChars.TopLeft}".WriteAt(x2, y, sampleColor, sampleFillColor);
      if (selectedCharIdx == i) selectedChar = sampleChars.TopLeft;
      y++; i++;

      "Top Middle char:".WriteAt(x, y, selectedCharIdx == i ? SelectedColor : TextColor, selectedCharIdx == i ? SelectedBackgroundColor : FillColor);
      $"{sampleChars.TopMid}".WriteAt(x2, y, sampleColor, sampleFillColor);
      if (selectedCharIdx == i) selectedChar = sampleChars.TopMid;
      y++; i++;

      "Top Right char:".WriteAt(x, y, selectedCharIdx == i ? SelectedColor : TextColor, selectedCharIdx == i ? SelectedBackgroundColor : FillColor);
      $"{sampleChars.TopRight}".WriteAt(x2, y, sampleColor, sampleFillColor);
      if (selectedCharIdx == i) selectedChar = sampleChars.TopRight;
      y++; i++;

      "Middle Left char:".WriteAt(x, y, selectedCharIdx == i ? SelectedColor : TextColor, selectedCharIdx == i ? SelectedBackgroundColor : FillColor);
      $"{sampleChars.MidLeft}".WriteAt(x2, y, sampleColor, sampleFillColor);
      if (selectedCharIdx == i) selectedChar = sampleChars.MidLeft;
      y++; i++;

      "Middle char:".WriteAt(x, y, selectedCharIdx == i ? SelectedColor : TextColor, selectedCharIdx == i ? SelectedBackgroundColor : FillColor);
      $"{sampleChars.Mid}".WriteAt(x2, y, sampleColor, sampleFillColor);
      if (selectedCharIdx == i) selectedChar = sampleChars.Mid;
      y++; i++;

      "Middle Right char:".WriteAt(x, y, selectedCharIdx == i ? SelectedColor : TextColor, selectedCharIdx == i ? SelectedBackgroundColor : FillColor);
      $"{sampleChars.MidRight}".WriteAt(x2, y, sampleColor, sampleFillColor);
      if (selectedCharIdx == i) selectedChar = sampleChars.MidRight;
      y++; i++;

      "Bottom Left char:".WriteAt(x, y, selectedCharIdx == i ? SelectedColor : TextColor, selectedCharIdx == i ? SelectedBackgroundColor : FillColor);
      $"{sampleChars.BotLeft}".WriteAt(x2, y, sampleColor, sampleFillColor);
      if (selectedCharIdx == i) selectedChar = sampleChars.BotLeft;
      y++; i++;

      "Bottom Middle char:".WriteAt(x, y, selectedCharIdx == i ? SelectedColor : TextColor, selectedCharIdx == i ? SelectedBackgroundColor : FillColor);
      $"{sampleChars.BotMid}".WriteAt(x2, y, sampleColor, sampleFillColor);
      if (selectedCharIdx == i) selectedChar = sampleChars.BotMid;
      y++; i++;

      "Bottom Right char:".WriteAt(x, y, selectedCharIdx == i ? SelectedColor : TextColor, selectedCharIdx == i ? SelectedBackgroundColor : FillColor);
      $"{sampleChars.BotRight}".WriteAt(x2, y, sampleColor, sampleFillColor);
      if (selectedCharIdx == i) selectedChar = sampleChars.BotRight;
      y++; i++;

      "Horizontal char:".WriteAt(x, y, selectedCharIdx == i ? SelectedColor : TextColor, selectedCharIdx == i ? SelectedBackgroundColor : FillColor);
      $"{sampleChars.Hor}".WriteAt(x2, y, sampleColor, sampleFillColor);
      if (selectedCharIdx == i) selectedChar = sampleChars.Hor;
      y++; i++;

      "Vertical char:".WriteAt(x, y, selectedCharIdx == i ? SelectedColor : TextColor, selectedCharIdx == i ? SelectedBackgroundColor : FillColor);
      $"{sampleChars.Ver}".WriteAt(x2, y, sampleColor, sampleFillColor);
      if (selectedCharIdx == i) selectedChar = sampleChars.Ver;
    }

    private static void DrawBoxColors()
    {
      int x = SampleColorBox.Left;
      int y = SampleColorBox.Top;
      int i = 0;
      "Box Char Colors".WriteAt(x, y, charsActive ? HeaderColor : SelectedColor, FillColor); y += 2;

      "Border Char Color:".WriteAt(x, y, selectedColorIdx == i ? SelectedColor : TextColor, selectedColorIdx == i ? SelectedBackgroundColor : FillColor);
      ' '.WriteAt(x + 27, y, sampleColor, sampleColor);
      if (selectedColorIdx == i) GetSelectedColor(sampleColor);
      y++; i++;

      "Border Background Color:".WriteAt(x, y, selectedColorIdx == i ? SelectedColor : TextColor, selectedColorIdx == i ? SelectedBackgroundColor : FillColor);
      ' '.WriteAt(x + 27, y, sampleBackgroundColor, sampleBackgroundColor);
      if (selectedColorIdx == i) GetSelectedColor(sampleBackgroundColor);
      y++; i++;

      "Box Fill Color:".WriteAt(x, y, selectedColorIdx == i ? SelectedColor : TextColor, selectedColorIdx == i ? SelectedBackgroundColor : FillColor);
      ' '.WriteAt(x + 27, y, sampleFillColor, sampleFillColor);
      if (selectedColorIdx == i) GetSelectedColor(sampleFillColor);
      y++; i++;

      "Text Color:".WriteAt(x, y, selectedColorIdx == i ? SelectedColor : TextColor, selectedColorIdx == i ? SelectedBackgroundColor : FillColor);
      ' '.WriteAt(x + 27, y, sampleTextColor, sampleTextColor);
      if (selectedColorIdx == i) GetSelectedColor(sampleTextColor);
      y++; i++;

      "Selected Item Color:".WriteAt(x, y, selectedColorIdx == i ? SelectedColor : TextColor, selectedColorIdx == i ? SelectedBackgroundColor : FillColor);
      ' '.WriteAt(x + 27, y, sampleSelectedColor, sampleSelectedColor);
      if (selectedColorIdx == i) GetSelectedColor(sampleSelectedColor);
      y++; i++;

      "Selected Background Color:".WriteAt(x, y, selectedColorIdx == i ? SelectedColor : TextColor, selectedColorIdx == i ? SelectedBackgroundColor : FillColor);
      ' '.WriteAt(x + 27, y, sampleSelectedBackgroundColor, sampleSelectedBackgroundColor);
      if (selectedColorIdx == i) GetSelectedColor(sampleSelectedBackgroundColor);
    }

    private static void DrawColorSelector()
    {
      int x = ColorSelectorBox.Left;
      int y = ColorSelectorBox.Top;

      "RGB Color Picker".WriteAt(x, y, HeaderColor, FillColor); y += 2;
      "[R]: ".WriteAt(x, y, Color.Red, FillColor); $"{iRed}".PadRight(3).WriteAt(x + 5, y, strRGB == "R"? SelectedColor : TextColor, strRGB == "R" ? SelectedBackgroundColor : FillColor); y++;
      "[G]: ".WriteAt(x, y, Color.Green, FillColor); $"{iGreen}".PadRight(3).WriteAt(x + 5, y, strRGB == "G" ? SelectedColor : TextColor, strRGB == "G" ? SelectedBackgroundColor : FillColor); y++;
      "[B]: ".WriteAt(x, y, Color.Blue, FillColor); $"{iBlue}".PadRight(3).WriteAt(x + 5, y, strRGB == "B" ? SelectedColor : TextColor, strRGB == "B" ? SelectedBackgroundColor : FillColor);
      "RGB Color: ".WriteAt(x + 10, y - 1, TextColor, FillColor); " ".WriteAt(x + 21, y - 1, Color.FromArgb(255, iRed, iGreen, iBlue), Color.FromArgb(255, iRed, iGreen, iBlue));
      "[Enter]: Updates Color".WriteAt(x, y+1, TextColor, FillColor); 
    }

    private static void DrawSampleBox()
    {
      int top = SampleBox.Top;
      int left = SampleBox.Left;
      int right = SampleBox.Left + SampleBox.Width - 1;
      int bottom = SampleBox.Top + SampleBox.Height - 1;
      int center = SampleBox.Left + SampleBox.Width / 2;
      int middle = SampleBox.Top + SampleBox.Height / 2;

      SampleBox.Draw(sampleChars, sampleColor, sampleBackgroundColor, sampleFillColor);

      sampleChars.TopMid.WriteAt(center, top, sampleColor, sampleBackgroundColor);
      sampleChars.MidLeft.WriteAt(left, middle, sampleColor, sampleBackgroundColor);
      sampleChars.MidRight.WriteAt(right, middle, sampleColor, sampleBackgroundColor);
      sampleChars.BotMid.WriteAt(center, bottom, sampleColor, sampleBackgroundColor);
      sampleChars.Hor.WriteAt(left + 1, middle, sampleColor, sampleFillColor, SampleBox.Width - 2, 0);
      for (int y = top + 1; y < bottom; y++)
        sampleChars.Ver.WriteAt(center, y, sampleColor, sampleFillColor);

      sampleChars.Mid.WriteAt(center, middle, sampleColor, sampleFillColor);
    }

    private static void GetPrevChar()
    {
      char[] value = selectedChar.ToCharArray();
      // convert char to numeric value to get next number;
      short code = Convert.ToInt16(value[0]);
      code--;
      char newChar = Convert.ToChar(code);
      UpdateChar(newChar.ToString());
    }

    private static void GetNextChar()
    {
      char[] value = selectedChar.ToCharArray();
      // convert char to numeric value to get next number;
      short code = Convert.ToInt16(value[0]);
      code++;
      char newChar = Convert.ToChar(code);
      UpdateChar(newChar.ToString());
    }

    private static void UpdateChar(string newChar)
    {
      switch (selectedCharIdx)
      {
        case 0:
          sampleChars.TopLeft = newChar;
          break;
        case 1:
          sampleChars.TopMid = newChar;
          break;
        case 2:
          sampleChars.TopRight = newChar;
          break;
        case 3:
          sampleChars.MidLeft = newChar;
          break;
        case 4:
          sampleChars.Mid = newChar;
          break;
        case 5:
          sampleChars.MidRight = newChar;
          break;
        case 6:
          sampleChars.BotLeft = newChar;
          break;
        case 7:
          sampleChars.BotMid = newChar;
          break;
        case 8:
          sampleChars.BotRight = newChar;
          break;
        case 9:
          sampleChars.Hor = newChar;
          break;
        case 10:
          sampleChars.Ver = newChar;
          break;
      }
    }

    private static void GetSelectedColor(Color color)
    {
      if (color == selectedColor) return;
      selectedColor = color;
      iRed = selectedColor.R;
      iGreen = selectedColor.G;
      iBlue = selectedColor.B;
    }
    
    private static void UpdateSelectedColor(int r, int g, int b)
    {
      Color newColor = Color.FromArgb(255, iRed, iGreen, iBlue);
      switch (selectedColorIdx)
      {
        case 0:
          sampleColor = newColor;
          break;
        case 1:
          sampleBackgroundColor = newColor;
          break;
        case 2:
          sampleFillColor = newColor;
          break;
        case 3:
          sampleTextColor = newColor;
          break;
        case 4:
          sampleSelectedColor = newColor;
          break;
        case 5:
          sampleSelectedBackgroundColor = newColor;
          break;
      }
    }
    
    private static void SaveChanges()
    {

    }

    private static void KeyHandler()
    {
      ConsoleKeyInfo keyInfo = Console.ReadKey(true);
      switch (keyInfo.Key)
      {
        case ConsoleKey.Escape:
          dialogOpen = false;
          Dialog.Close("GamePlay");
          break;
        case ConsoleKey.PageUp:
          charsActive = true;
          break;
        case ConsoleKey.PageDown:
          charsActive = false;
          break;
        case ConsoleKey.UpArrow:
          if (charsActive)
          {
            selectedCharIdx--;
            if (selectedCharIdx < 0) selectedCharIdx = 10;
          }
          else
          {
            selectedColorIdx--;
            if (selectedColorIdx < 0) selectedColorIdx = 5;
          }
          break;
        case ConsoleKey.DownArrow:
          if (charsActive)
          {
            selectedCharIdx++;
            if (selectedCharIdx > 10) selectedCharIdx = 0;
          }
          else
          {
            selectedColorIdx++;
            if (selectedColorIdx > 5) selectedColorIdx = 0;
          }

          break;
        case ConsoleKey.LeftArrow:
          if (charsActive)
            GetPrevChar();
          else
          {
            switch (strRGB)
            {
              case "R":
                iRed--;
                if (iRed < 0) iRed = 255;
                break;
              case "G":
                iGreen--;
                if (iGreen < 0) iGreen = 255;
                break;
              case "B":
                iBlue--;
                if (iBlue < 0) iBlue = 255;
                break;
            }
          }
          break;
        case ConsoleKey.RightArrow:
          if (charsActive)
            GetNextChar();
          else
          {
            switch (strRGB)
            {
              case "R":
                iRed++;
                if (iRed > 255) iRed = 0;
                break;
              case "G":
                iGreen++;
                if (iGreen > 255) iGreen = 0;
                break;
              case "B":
                iBlue++;
                if (iBlue > 255) iBlue = 0;
                break;
            }
          }
          break;
        case ConsoleKey.R:
          strRGB = !charsActive ? "R" : string.Empty;
          break;
        case ConsoleKey.G:
          strRGB = !charsActive ? "G" : string.Empty;
          break;
        case ConsoleKey.B:
          strRGB = !charsActive ? "B" : string.Empty;
          break;
        case ConsoleKey.Enter:
          if (charsActive) break;
          UpdateSelectedColor(iRed, iGreen, iBlue);
          strRGB = string.Empty;
          break;
        case ConsoleKey.S:
          SaveChanges();
          break;
      }
    }
  }
}
