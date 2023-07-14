using ConsoleDungeonCrawler.Extensions;
using System.Drawing;
using ConsoleDungeonCrawler.Game.Entities;

namespace ConsoleDungeonCrawler.Game.Screens.Dialogs
{
    internal static class ThemeConfig
  {
    #region Properties
    private static bool dialogOpen;
    private static Theme theme = Theme.DefaultTheme;
    private static Theme selectedTheme = Theme.DefaultTheme;
    private static readonly Colors Colors = theme.Colors;

    private static BoxChars sampleChars = theme.BoxChars;
    private static Colors sampleColors = Theme.DefaultColors;

    private static readonly Box DialogBox = new(Dialog.MapCenter, 100, 24);
    //private static readonly ScreenBox DialogBox = new(Dialog.ScreenCenter, 100, 24);

    private static readonly Box LegendBox = new(DialogBox.Left + 3, DialogBox.Top + 2, 0, 0);
    private static readonly Box SampleCharBox = new(DialogBox.Left + 37, DialogBox.Top + 2, 0, 0);
    private static readonly Box SampleBox = new(DialogBox.Left + 62, DialogBox.Top + 4, 35, 17);
    private static readonly Box SampleColorBox = new(DialogBox.Left + 3, DialogBox.Top + 14, 0, 0);
    private static readonly Box ColorSelectorBox = new(DialogBox.Left + 37, DialogBox.Top + 17, 0, 0);

    private static bool charsActive = true;
    private static int selectedCharIdx = 0;
    private static string selectedChar = string.Empty;

    private static Color selectedColor = Color.Black;
    private static int selectedColorIdx = 0;
    private static string strRGB = string.Empty;
    private static int iRed;
    private static int iGreen;
    private static int iBlue;
    #endregion Properties

    static ThemeConfig()
    {
      if (!Theme.Themes.ContainsKey("ThemeConfig"))
      {
        Colors colors = new()
        {
          Color = Color.DarkOrange,
          BackgroundColor = Color.Black,
          FillColor = Color.MidnightBlue,
          HeaderColor = Color.Yellow,
          TextColor = Color.Bisque,
          SelectedColor = Color.Lime,
          SelectedBackgroundColor = Color.DarkOrange
        };
        Theme newTheme = new("ThemeConfig", colors, BoxChars.Default);
        Theme.Themes.Add("ThemeConfig", newTheme);
      }

      theme = Theme.Themes["ThemeConfig"];
      Colors.Color = theme.Colors.Color;
      Colors.BackgroundColor = theme.Colors.BackgroundColor;
      Colors.FillColor = theme.Colors.FillColor;
      Colors.HeaderColor = theme.Colors.HeaderColor;
      Colors.TextColor = theme.Colors.TextColor;
      Colors.SelectedColor = theme.Colors.SelectedColor;
      Colors.SelectedBackgroundColor = theme.Colors.SelectedBackgroundColor;

      sampleChars = theme.BoxChars;
      sampleColors = new Colors();
    }

  internal static void Draw()
    {
      dialogOpen = true;
      Dialog.Draw(" Dialog Configuration ", Colors.Color, Colors.BackgroundColor, Colors.FillColor, Colors.TextColor, DialogBox);

      while (dialogOpen)
      {
        DrawLegend();
        DrawSampleChars();
        DrawSampleBox();

        // Order is important here.  Color ScreenBox updates Selector data.
        DrawBoxColors();
        DrawColorSelector();

        KeyHandler();
      }
    }

    private static void DrawLegend()
    {
      int x = LegendBox.Left;
      int y = LegendBox.Top;

      "Legend:".WriteAt(x, y, Colors.HeaderColor, Colors.FillColor); y++; x++;

      $"[{ConsoleKey.PageUp}] Border Chars Active".WriteAt(x, y, Colors.TextColor, Colors.FillColor); y++;
      $"[{ConsoleKey.PageDown}] Char Colors Active".WriteAt(x, y, Colors.TextColor, Colors.FillColor); y++;
      $"[{ConsoleKey.R}][{ConsoleKey.G}][{ConsoleKey.B}] Edit RGB values".WriteAt(x, y, Colors.TextColor, Colors.FillColor); y++;
      $"[\u2191] Prev Item".WriteAt(x, y, Colors.TextColor, Colors.FillColor); y++;
      $"[\u2193] Next Item".WriteAt(x, y, Colors.TextColor, Colors.FillColor); y++;
      $"[\u2190] Change Value".WriteAt(x, y, Colors.TextColor, Colors.FillColor); y++;
      $"[\u2192] Change Value".WriteAt(x, y, Colors.TextColor, Colors.FillColor); y++;
      $"[{ConsoleKey.S}] Save Changes".WriteAt(x, y, Colors.TextColor, Colors.FillColor); y++;
      $"[{ConsoleKey.Escape}] Close Dialog".WriteAt(x, y, Colors.TextColor, Colors.FillColor);
    }

    private static void DrawSampleChars()
    {
      int x = SampleCharBox.Left;
      int x2 = SampleCharBox.Left + 22;
      int y = SampleCharBox.Top;
      int i = 0;

      "Border Characters".WriteAt(x, y, charsActive ? Colors.SelectedColor : Colors.HeaderColor, Colors.FillColor); y++; x++;
      
      "Top Left char:".WriteAt(x, y, selectedCharIdx == i ? Colors.SelectedColor : Colors.TextColor, selectedCharIdx == i ? Colors.SelectedBackgroundColor : Colors.FillColor);
      $"{sampleChars.TopLeft}".WriteAt(x2, y, sampleColors.Color, sampleColors.FillColor);
      if (selectedCharIdx == i) selectedChar = sampleChars.TopLeft;
      y++; i++;

      "Top Middle char:".WriteAt(x, y, selectedCharIdx == i ? Colors.SelectedColor : Colors.TextColor, selectedCharIdx == i ? Colors.SelectedBackgroundColor : Colors.FillColor);
      $"{sampleChars.TopCtr}".WriteAt(x2, y, sampleColors.Color, sampleColors.FillColor);
      if (selectedCharIdx == i) selectedChar = sampleChars.TopCtr;
      y++; i++;

      "Top Right char:".WriteAt(x, y, selectedCharIdx == i ? Colors.SelectedColor : Colors.TextColor, selectedCharIdx == i ? Colors.SelectedBackgroundColor : Colors.FillColor);
      $"{sampleChars.TopRight}".WriteAt(x2, y, sampleColors.Color, sampleColors.FillColor);
      if (selectedCharIdx == i) selectedChar = sampleChars.TopRight;
      y++; i++;

      "Middle Left char:".WriteAt(x, y, selectedCharIdx == i ? Colors.SelectedColor : Colors.TextColor, selectedCharIdx == i ? Colors.SelectedBackgroundColor : Colors.FillColor);
      $"{sampleChars.MidLeft}".WriteAt(x2, y, sampleColors.Color, sampleColors.FillColor);
      if (selectedCharIdx == i) selectedChar = sampleChars.MidLeft;
      y++; i++;

      "Middle char:".WriteAt(x, y, selectedCharIdx == i ? Colors.SelectedColor : Colors.TextColor, selectedCharIdx == i ? Colors.SelectedBackgroundColor : Colors.FillColor);
      $"{sampleChars.MidCtr}".WriteAt(x2, y, sampleColors.Color, sampleColors.FillColor);
      if (selectedCharIdx == i) selectedChar = sampleChars.MidCtr;
      y++; i++;

      "Middle Right char:".WriteAt(x, y, selectedCharIdx == i ? Colors.SelectedColor : Colors.TextColor, selectedCharIdx == i ? Colors.SelectedBackgroundColor : Colors.FillColor);
      $"{sampleChars.MidRight}".WriteAt(x2, y, sampleColors.Color, sampleColors.FillColor);
      if (selectedCharIdx == i) selectedChar = sampleChars.MidRight;
      y++; i++;

      "Bottom Left char:".WriteAt(x, y, selectedCharIdx == i ? Colors.SelectedColor : Colors.TextColor, selectedCharIdx == i ? Colors.SelectedBackgroundColor : Colors.FillColor);
      $"{sampleChars.BotLeft}".WriteAt(x2, y, sampleColors.Color, sampleColors.FillColor);
      if (selectedCharIdx == i) selectedChar = sampleChars.BotLeft;
      y++; i++;

      "Bottom Middle char:".WriteAt(x, y, selectedCharIdx == i ? Colors.SelectedColor : Colors.TextColor, selectedCharIdx == i ? Colors.SelectedBackgroundColor : Colors.FillColor);
      $"{sampleChars.BotCtr}".WriteAt(x2, y, sampleColors.Color, sampleColors.FillColor);
      if (selectedCharIdx == i) selectedChar = sampleChars.BotCtr;
      y++; i++;

      "Bottom Right char:".WriteAt(x, y, selectedCharIdx == i ? Colors.SelectedColor : Colors.TextColor, selectedCharIdx == i ? Colors.SelectedBackgroundColor : Colors.FillColor);
      $"{sampleChars.BotRight}".WriteAt(x2, y, sampleColors.Color, sampleColors.FillColor);
      if (selectedCharIdx == i) selectedChar = sampleChars.BotRight;
      y++; i++;

      "Horizontal char:".WriteAt(x, y, selectedCharIdx == i ? Colors.SelectedColor : Colors.TextColor, selectedCharIdx == i ? Colors.SelectedBackgroundColor : Colors.FillColor);
      $"{sampleChars.Hor}".WriteAt(x2, y, sampleColors.Color, sampleColors.FillColor);
      if (selectedCharIdx == i) selectedChar = sampleChars.Hor;
      y++; i++;

      "Vertical char:".WriteAt(x, y, selectedCharIdx == i ? Colors.SelectedColor : Colors.TextColor, selectedCharIdx == i ? Colors.SelectedBackgroundColor : Colors.FillColor);
      $"{sampleChars.Ver}".WriteAt(x2, y, sampleColors.Color, sampleColors.FillColor);
      if (selectedCharIdx == i) selectedChar = sampleChars.Ver;
    }

    private static void DrawBoxColors()
    {
      int x = SampleColorBox.Left;
      int y = SampleColorBox.Top;
      int i = 0;
      "ScreenBox Char Colors".WriteAt(x, y, charsActive ? Colors.HeaderColor : Colors.SelectedColor, Colors.FillColor); y++; x++;

      "Border Char Color:".WriteAt(x, y, selectedColorIdx == i ? Colors.SelectedColor : Colors.TextColor, selectedColorIdx == i ? Colors.SelectedBackgroundColor : Colors.FillColor);
      ' '.WriteAt(x + 27, y, sampleColors.Color, sampleColors.Color);
      if (selectedColorIdx == i) GetSelectedColor(sampleColors.Color);
      y++; i++;

      "Border Background Color:".WriteAt(x, y, selectedColorIdx == i ? Colors.SelectedColor : Colors.TextColor, selectedColorIdx == i ? Colors.SelectedBackgroundColor : Colors.FillColor);
      ' '.WriteAt(x + 27, y, sampleColors.BackgroundColor, sampleColors.BackgroundColor);
      if (selectedColorIdx == i) GetSelectedColor(sampleColors.BackgroundColor);
      y++; i++;

      "ScreenBox Fill Color:".WriteAt(x, y, selectedColorIdx == i ? Colors.SelectedColor : Colors.TextColor, selectedColorIdx == i ? Colors.SelectedBackgroundColor : Colors.FillColor);
      ' '.WriteAt(x + 27, y, sampleColors.FillColor, sampleColors.FillColor);
      if (selectedColorIdx == i) GetSelectedColor(sampleColors.FillColor);
      y++; i++;

      "Header Color:".WriteAt(x, y, selectedColorIdx == i ? Colors.SelectedColor : Colors.TextColor, selectedColorIdx == i ? Colors.SelectedBackgroundColor : Colors.FillColor);
      ' '.WriteAt(x + 27, y, sampleColors.HeaderColor, sampleColors.HeaderColor);
      if (selectedColorIdx == i) GetSelectedColor(sampleColors.HeaderColor);
      y++; i++;

      "Text Color:".WriteAt(x, y, selectedColorIdx == i ? Colors.SelectedColor : Colors.TextColor, selectedColorIdx == i ? Colors.SelectedBackgroundColor : Colors.FillColor);
      ' '.WriteAt(x + 27, y, sampleColors.TextColor, sampleColors.TextColor);
      if (selectedColorIdx == i) GetSelectedColor(sampleColors.TextColor);
      y++; i++;

      "Selected Item Color:".WriteAt(x, y, selectedColorIdx == i ? Colors.SelectedColor : Colors.TextColor, selectedColorIdx == i ? Colors.SelectedBackgroundColor : Colors.FillColor);
      ' '.WriteAt(x + 27, y, sampleColors.SelectedColor, sampleColors.SelectedColor);
      if (selectedColorIdx == i) GetSelectedColor(sampleColors.SelectedColor);
      y++; i++;

      "Selected Background Color:".WriteAt(x, y, selectedColorIdx == i ? Colors.SelectedColor : Colors.TextColor, selectedColorIdx == i ? Colors.SelectedBackgroundColor : Colors.FillColor);
      ' '.WriteAt(x + 27, y, sampleColors.SelectedBackgroundColor, sampleColors.SelectedBackgroundColor);
      if (selectedColorIdx == i) GetSelectedColor(sampleColors.SelectedBackgroundColor);
    }

    private static void DrawColorSelector()
    {
      int x = ColorSelectorBox.Left;
      int y = ColorSelectorBox.Top;

      "RGB Color Picker".WriteAt(x, y, Colors.HeaderColor, Colors.FillColor); y++; x++;

      "[R]: ".WriteAt(x, y, Color.Red, Colors.FillColor); $"{iRed}".PadRight(3).WriteAt(x + 5, y, strRGB == "R"? Colors.SelectedColor : Colors.TextColor, strRGB == "R" ? Colors.SelectedBackgroundColor : Colors.FillColor); y++;
      "[G]: ".WriteAt(x, y, Color.Green, Colors.FillColor); $"{iGreen}".PadRight(3).WriteAt(x + 5, y, strRGB == "G" ? Colors.SelectedColor : Colors.TextColor, strRGB == "G" ? Colors.SelectedBackgroundColor : Colors.FillColor);
      "RGB Color: ".WriteAt(x + 10, y, Colors.TextColor, Colors.FillColor); " ".WriteAt(x + 21, y, Color.FromArgb(255, iRed, iGreen, iBlue), Color.FromArgb(255, iRed, iGreen, iBlue)); y++;
      "[B]: ".WriteAt(x, y, Color.Blue, Colors.FillColor); $"{iBlue}".PadRight(3).WriteAt(x + 5, y, strRGB == "B" ? Colors.SelectedColor : Colors.TextColor, strRGB == "B" ? Colors.SelectedBackgroundColor : Colors.FillColor);
      "[Enter]: Updates Color".WriteAt(x, y+1, Colors.TextColor, Colors.FillColor); 
    }

    private static void DrawSampleBox()
    {
      int top = SampleBox.Top;
      int left = SampleBox.Left;
      int right = SampleBox.Left + SampleBox.Width - 1;
      int bottom = SampleBox.Top + SampleBox.Height - 1;
      int center = SampleBox.Left + SampleBox.Width / 2;
      int middle = SampleBox.Top + SampleBox.Height / 2;

      SampleBox.Draw(sampleChars, sampleColors.Color, sampleColors.BackgroundColor, sampleColors.FillColor);

      sampleChars.TopCtr.WriteAt(center, top, sampleColors.Color, sampleColors.BackgroundColor);
      sampleChars.MidLeft.WriteAt(left, middle, sampleColors.Color, sampleColors.BackgroundColor);
      sampleChars.MidRight.WriteAt(right, middle, sampleColors.Color, sampleColors.BackgroundColor);
      sampleChars.BotCtr.WriteAt(center, bottom, sampleColors.Color, sampleColors.BackgroundColor);
      sampleChars.Hor.WriteAt(left + 1, middle, sampleColors.Color, sampleColors.FillColor, SampleBox.Width - 2, 0);
      for (int y = top + 1; y < bottom; y++)
        sampleChars.Ver.WriteAt(center, y, sampleColors.Color, sampleColors.FillColor);

      sampleChars.MidCtr.WriteAt(center, middle, sampleColors.Color, sampleColors.FillColor);

      // draw text elements in box.
      "Theme Sample".WriteAt(left, top-1, Colors.HeaderColor, Colors.FillColor);
      "Header color".WriteAt(left + 2, top + 2, sampleColors.HeaderColor, sampleColors.FillColor);
      "Text Color".WriteAt(left + 3, top + 3, sampleColors.TextColor, sampleColors.FillColor);
      "Selected Item".WriteAt(left + 3, top + 4, sampleColors.SelectedColor, sampleColors.SelectedBackgroundColor);
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
          sampleChars.TopCtr = newChar;
          break;
        case 2:
          sampleChars.TopRight = newChar;
          break;
        case 3:
          sampleChars.MidLeft = newChar;
          break;
        case 4:
          sampleChars.MidCtr = newChar;
          break;
        case 5:
          sampleChars.MidRight = newChar;
          break;
        case 6:
          sampleChars.BotLeft = newChar;
          break;
        case 7:
          sampleChars.BotCtr = newChar;
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
    
    private static void UpdateSelectedColor()
    {
      Color newColor = Color.FromArgb(255, iRed, iGreen, iBlue);
      switch (selectedColorIdx)
      {
        case 0:
          sampleColors.Color = newColor;
          break;
        case 1:
          sampleColors.BackgroundColor = newColor;
          break;
        case 2:
          sampleColors.FillColor = newColor;
          break;
        case 3:
          sampleColors.HeaderColor = newColor;
          break;
        case 4:
          sampleColors.TextColor = newColor;
          break;
        case 5:
          sampleColors.SelectedColor = newColor;
          break;
        case 6:
          sampleColors.SelectedBackgroundColor = newColor;
          break;
      }
    }
    
    private static void SaveChanges()
    {
      //add persistence of changes to themes here.
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
            if (selectedCharIdx < 0) selectedCharIdx = BoxChars.Count - 1;
          }
          else
          {
            selectedColorIdx--;
            if (selectedColorIdx < 0) selectedColorIdx = Colors.Count - 1;
          }
          break;
        case ConsoleKey.DownArrow:
          if (charsActive)
          {
            selectedCharIdx++;
            if (selectedCharIdx >= BoxChars.Count) selectedCharIdx = 0;
          }
          else
          {
            selectedColorIdx++;
            if (selectedColorIdx >= Colors.Count) selectedColorIdx = 0;
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
          UpdateSelectedColor();
          strRGB = string.Empty;
          break;
        case ConsoleKey.S:
          SaveChanges();
          break;
      }
    }
  }
}
