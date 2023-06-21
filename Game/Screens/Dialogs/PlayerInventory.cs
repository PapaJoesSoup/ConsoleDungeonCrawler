using System.Drawing;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;

namespace ConsoleDungeonCrawler.Game.Screens.Dialogs
{
  internal static class PlayerInventory
  {
    static int activeBag = 0;
    static int activeItem = 0;
    static bool dialogOpen = false;

    internal static void Draw()
    {
      dialogOpen = true;
      Dialog.Draw("Player Inventory Manager");

      while (dialogOpen)
      {
        // Create a new box for the player inventory
        int x = 1;
        int y = 1;
        "Select Bag: ([x])".WriteAt(Dialog.Box.Left + 2, Dialog.Box.Top + 1, Color.White, Color.Olive);
        y += 2;
        foreach (var bag in Inventory.Bags)
        {
          $"[{y}] Bag {y}".WriteAt(Dialog.Box.Left + 2, Dialog.Box.Top + 1 + y, Color.White,
            activeBag == y - 1 ? Color.Orange : Color.Olive);
          x++;
          y++;
        }
        DrawLegend();
        DrawBag(Inventory.Bags[activeBag]);
        KeyHandler();
      }



      //ConsoleEx.WriteAlignedAt(Dialog.Box, "Press any key to continue", HAlign.Center, VAlign.Middle, Color.Bisque, Color.Olive);
      Console.ReadKey(true);
      Dialog.Close();
    }

    private static void DrawLegend()
    {
      Box box = new(Dialog.Box.Left, Dialog.Box.Top + 8, 22, 10);
      int y = box.Top + 1;
      "Legend:".WriteAt(box.Left + 2, box.Top, Color.White, Color.Olive); y++;

      "[1-5] Select Bag".WriteAt(box.Left + 2, y, Color.White, Color.Olive); y++;
      $"[{ConsoleKey.PageUp}] Prev Bag".WriteAt(box.Left + 2, y, Color.White, Color.Olive); y++;
      $"[{ConsoleKey.PageDown}] Next Bag".WriteAt(box.Left + 2, y, Color.White, Color.Olive); y++;
      $"[{ConsoleKey.UpArrow}] Prev Item".WriteAt(box.Left + 2, y, Color.White, Color.Olive); y++;
      $"[{ConsoleKey.DownArrow}] Next Item".WriteAt(box.Left + 2, y, Color.White, Color.Olive); y++;
      $"[{ConsoleKey.Escape}] Close Dialog".WriteAt(box.Left + 2, y, Color.White, Color.Olive); y++;
      $"[{ConsoleKey.M}] Move Item".WriteAt(box.Left + 2, y, Color.White, Color.Olive); y++;
      $"[{ConsoleKey.R}] Remove Item".WriteAt(box.Left + 2, y, Color.White, Color.Olive); y++;
      $"[{ConsoleKey.S}] Sell Item".WriteAt(box.Left + 2, y, Color.White, Color.Olive); y++;
      $"[{ConsoleKey.U}] Use Item".WriteAt(box.Left + 2, y, Color.White, Color.Olive);
    }

    internal static void DrawBag(Bag bag)
    {
      int x = Dialog.Box.Left + 25;
      int y = Dialog.Box.Top + 1;
      $"Bag {activeBag + 1} Contents:".WriteAt(x, y, Color.White, Color.Olive);
      y += 2;

      for (int i = 0; i < bag.Capacity; i++)
      {
        if (i >= bag.Items.Count)
          $"[{i + 1}]:  Empty".WriteAt(x, y, Color.White, Color.Olive);
        else
          $"[{i + 1}]:  ({bag.Items[i].Quantity}) {bag.Items[i].Name}".WriteAt(x, y, Color.White,
          i == activeItem ? Color.DarkOrange : Color.Olive);
        y++;
      }
    }

    internal static void MoveItem(Item item, int bag)
    {
      if (Inventory.Bags[bag].Items.Count >= Inventory.Bags[bag].Capacity) return;
      Inventory.Bags[bag].Items.Add(item);
      Inventory.Bags[activeBag].Items.Remove(item);
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

    internal static void SellItem(Item item)
    {
      if (item.Quantity > 0)
      {
        Player.Gold += item.SellCost;
        item.Quantity--;
      }
      if (item.Quantity == 0) Inventory.Bags[activeBag].Items.Remove(item);
    }

    internal static void KeyHandler()
    {
      ConsoleKeyInfo keyInfo = Console.ReadKey(true);
      switch (keyInfo.Key)
      {
        case ConsoleKey.Escape:
          dialogOpen = false;
          Dialog.Close();
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
}
