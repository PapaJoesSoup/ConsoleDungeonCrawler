using System.Drawing;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;

namespace ConsoleDungeonCrawler.Game.Screens.Dialogs;

internal static class PlayerInventory
{
#region Properties
  private static readonly Color Color = Color.DarkOrange;
  private static readonly Color BackgroundColor = Color.Black;
  private static readonly Color FillColor = Color.Olive;
  private static readonly Color TextColor = Color.Bisque;
  private static readonly Color SelectedColor = Color.Lime;
  private static readonly Color SelectedBackgroundColor = Color.DarkOrange;

  private static readonly Box DialogBox = new(Console.WindowWidth / 2 - 58, Console.WindowHeight / 2 - 13, 116, 25);
  private static readonly Box LegendBox = new(DialogBox.Left, DialogBox.Top + 11, 22, 10);
  private static readonly Position ListPosition = new(DialogBox.Left + LegendBox.Width + 4, DialogBox.Top + 1);

  private const int ListWidth = 40;
  private const int ListHeight = 20;
  private static int activeBag;
  private static int activeItem;
  private static bool dialogOpen;
#endregion Properties

  internal static void Draw()
  {
    dialogOpen = true;
    Dialog.Draw(" Player Inventory Manager ", Color, BackgroundColor, FillColor, TextColor, DialogBox);

    while (dialogOpen)
    {
      // Create a new box for the player inventory
      int x = 1;
      int y = 1;
      "Select Bag: ([x])".WriteAt(DialogBox.Left + 2, DialogBox.Top + 1, TextColor, FillColor);
      y += 2;
      foreach (Bag bag in Inventory.Bags)
      {
        $"[{x}] Bag {x}".WriteAt(DialogBox.Left + 2, DialogBox.Top + y, TextColor, activeBag == x - 1 ? SelectedBackgroundColor : FillColor);
        x++;
        y++;
      }
      DrawLegend();
      DrawBag(Inventory.Bags[activeBag]);
      KeyHandler();
    }
  }

  private static void DrawLegend()
  {
    int x = LegendBox.Left + 2;
    int y = LegendBox.Top + 1;
    "Legend:".WriteAt(x, LegendBox.Top, TextColor, FillColor); y++;

    "[1-5] Select Bag".WriteAt(x, y, TextColor, FillColor); y++;
    $"[{ConsoleKey.PageUp}] Prev Bag".WriteAt(x, y, TextColor, FillColor); y++;
    $"[{ConsoleKey.PageDown}] Next Bag".WriteAt(x, y, TextColor, FillColor); y++;
    $"[{ConsoleKey.UpArrow}] Prev Item".WriteAt(x, y, TextColor, FillColor); y++;
    $"[{ConsoleKey.DownArrow}] Next Item".WriteAt(x, y, TextColor, FillColor); y++;
    $"[{ConsoleKey.M}] Move Item".WriteAt(x, y, TextColor, FillColor); y++;
    $"[{ConsoleKey.R}] Remove Item".WriteAt(x, y, TextColor, FillColor); y++;
    $"[{ConsoleKey.S}] Sell Item".WriteAt(x, y, TextColor, FillColor); y++;
    $"[{ConsoleKey.U}] Use Item".WriteAt(x, y, TextColor, FillColor); y++;
    $"[{ConsoleKey.Escape}] Close Dialog".WriteAt(x, y, TextColor, FillColor);
  }

  private static void DrawBag(Bag bag)
  {
    int x = ListPosition.X;
    int y = ListPosition.Y;
    if (activeItem >= bag.Items.Count) activeItem = bag.Items.Count - 1;
    $"Bag {activeBag + 1} Contents:".WriteAt(x, y, TextColor, FillColor);
    y += 2;
    for (int i = 0; i < (bag.Capacity < ListHeight? bag.Capacity: ListHeight); i++)
    {
      if (i >= bag.Items.Count)
        $"[{i + 1}]:  Empty".PadRight(ListWidth).WriteAt(x, y, TextColor, FillColor);
      else
        ($"[{i + 1}]:  ({bag.Items[i].Quantity}) {bag.Items[i].Name}".PadRight(ListWidth - 7) +
         $"{decimal.Round(bag.Items[i].SellCost, 2):C}g".PadLeft(7))
          .WriteAt(x, y, i == activeItem ? SelectedColor : TextColor, i == activeItem ? SelectedBackgroundColor : FillColor);
      y++;
    }
  }

  private static void MoveItem(Item item, int bag)
  {
    if (Inventory.Bags[bag].Items.Count >= Inventory.Bags[bag].Capacity) return;
    Inventory.Bags[bag].AddItem(item);
    Inventory.Bags[activeBag].RemoveItem(item);
  }

  private static void SellItem()
  {
    Dialog.Confirm("Sell Item",
      $"Sell this item for {Inventory.Bags[activeBag].Items[activeItem].SellCost} gold? (Y or N)", out bool sell);
    if (sell) SellItem(Inventory.Bags[activeBag].Items[activeItem]);
    Draw();
  }

  private static void RemoveItem()
  {
    if (Inventory.Bags[activeBag].Items[activeItem].Quantity == 0)
    {
      Dialog.Notify("Can't Remove Item", "You don't have any of this item.");
      Draw();
      return;
    }

    Dialog.Confirm("Remove Item", "Are you sure you want to destroy this item? (Y or N)", out bool remove);
    if (remove) Inventory.Bags[activeBag].Items.RemoveAt(activeItem);
    Draw();
  }

  private static void MoveItem()
  {
    if (Inventory.Bags.Count == 1)
    {
      Dialog.Notify("Can't Move Item", "You only have one bag.");
      Draw();
      return;
    }

    Dialog.AskForInt("Move Item To Bag", "Enter a bag number: ", out int newBag);
    newBag--; // Adjust for indexing.
    if (newBag <= Inventory.Bags.Count && newBag >= 1 && newBag != activeBag)
      MoveItem(Inventory.Bags[activeBag].Items[activeItem], newBag);
    else
    {
      Dialog.Notify("Invalid Bag", "You entered an invalid bag number.");
    }

    Draw();
  }

  private static void UseItem()
  {
    Dialog.Confirm("Use Item", "Are you sure you want to use this item? (Y or N)", out bool use);
    if (!use)
    {
      Draw();
      return;
    }
    if (!Inventory.Bags[activeBag].Items[activeItem].Use())
    {
      Draw();
      Dialog.Notify("Can't Use Item", "You can't use this item.");
      Draw();
      return;
    }
    GamePlay.StatusSection();
    Draw();
  }

  private static void SellItem(Item item)
  {
    Player.Gold += item.SellCost;
    Inventory.Bags[activeBag].RemoveItem(item);
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
      case ConsoleKey.D1:
      case ConsoleKey.D2:
      case ConsoleKey.D3:
      case ConsoleKey.D4:
      case ConsoleKey.D5:
        if (Inventory.Bags.Count < (int)keyInfo.Key - 48) return;
        activeBag = (int)keyInfo.Key - 49;
        DrawBag(Inventory.Bags[(int)keyInfo.Key - 49]);
        break;
      case ConsoleKey.PageUp:
        if (activeBag == 0) activeBag = Inventory.Bags.Count - 1;
        else activeBag--;
        break;
      case ConsoleKey.PageDown:
        if (activeBag == Inventory.Bags.Count - 1) activeBag = 0;
        else activeBag++;
        break;
      case ConsoleKey.UpArrow:
        if (activeItem == 0) activeItem = Inventory.Bags[activeBag].Items.Count - 1;
        else activeItem--;
        break;
      case ConsoleKey.DownArrow:
        if (activeItem == Inventory.Bags[activeBag].Items.Count - 1) activeItem = 0;
        else activeItem++;
        break;
      case ConsoleKey.M:
        MoveItem();
        break;
      case ConsoleKey.R:
        RemoveItem();
        break;
      case ConsoleKey.S:
        SellItem();
        break;
      case ConsoleKey.U:
        UseItem();
        break;
    }
  }
}