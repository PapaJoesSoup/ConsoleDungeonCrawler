using System.Drawing;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;

namespace ConsoleDungeonCrawler.Game.Screens.Dialogs
{
  internal static class PlayerInventory
  {
    static int ActiveBag = 0;
    static int ActiveItem = 0;
    static bool DialogOpen = false;

    internal static void Draw()
    {
      DialogOpen = true;
      Dialog.Draw("Player Inventory Manager");

      while (DialogOpen)
      {
        // Create a new box for the player inventory
        int x = 1;
        int y = 1;
        ConsoleEx.WriteAt("Select Bag: ([x])", Dialog.Box.Left + 2, Dialog.Box.Top + 1, Color.White, Color.Olive);
        y += 2;
        foreach (var bag in Inventory.Bags)
        {
          ConsoleEx.WriteAt($"[{y}] Bag {y}", Dialog.Box.Left + 2, Dialog.Box.Top + 1 + y, Color.White,
            ActiveBag == y - 1 ? Color.Orange : Color.Olive);
          x++;
          y++;
        }
        DrawLegend();
        DrawBag(Inventory.Bags[ActiveBag]);
        KeyHandler();
      }



      //ConsoleEx.WriteAlignedAt(Dialog.Box, "Press any key to continue", HAlign.Center, VAlign.Middle, Color.Bisque, Color.Olive);
      Console.ReadKey(true);
      Dialog.Close();
    }

    private static void DrawLegend()
    {
      Box box = new Box(Dialog.Box.Left, Dialog.Box.Top + 8, 22, 10);
      int y = box.Top + 1;
      ConsoleEx.WriteAt($"Legend:", box.Left + 2, box.Top, Color.White, Color.Olive); y++;

      ConsoleEx.WriteAt($"[1-5] Select Bag", box.Left + 2, y, Color.White, Color.Olive); y++;
      ConsoleEx.WriteAt($"[{ConsoleKey.PageUp}] Prev Bag", box.Left + 2, y, Color.White, Color.Olive); y++;
      ConsoleEx.WriteAt($"[{ConsoleKey.PageDown}] Next Bag", box.Left + 2, y, Color.White, Color.Olive); y++;
      ConsoleEx.WriteAt($"[{ConsoleKey.UpArrow}] Prev Item", box.Left + 2, y, Color.White, Color.Olive); y++;
      ConsoleEx.WriteAt($"[{ConsoleKey.DownArrow}] Next Item", box.Left + 2, y, Color.White, Color.Olive); y++;
      ConsoleEx.WriteAt($"[{ConsoleKey.Escape}] Close Dialog", box.Left + 2, y, Color.White, Color.Olive); y++;
      ConsoleEx.WriteAt($"[{ConsoleKey.M}] Move Item", box.Left + 2, y, Color.White, Color.Olive); y++;
      ConsoleEx.WriteAt($"[{ConsoleKey.R}] Remove Item", box.Left + 2, y, Color.White, Color.Olive); y++;
      ConsoleEx.WriteAt($"[{ConsoleKey.S}] Sell Item", box.Left + 2, y, Color.White, Color.Olive); y++;
      ConsoleEx.WriteAt($"[{ConsoleKey.U}] Use Item", box.Left + 2, y, Color.White, Color.Olive); y++;
    }

    internal static void DrawBag(Bag bag)
    {
      int x = Dialog.Box.Left + 25;
      int y = Dialog.Box.Top + 1;
      ConsoleEx.WriteAt($"Bag {ActiveBag + 1} Contents:", x, y, Color.White, Color.Olive);
      y += 2;

      for (int i = 0; i < bag.Capacity; i++)
      {
        if (i >= bag.Items.Count)
          ConsoleEx.WriteAt($"[{i + 1}]:  Empty", x, y, Color.White, Color.Olive);
        else
          ConsoleEx.WriteAt($"[{i + 1}]:  ({bag.Items[i].Quantity}) {bag.Items[i].Name}", x, y, Color.White,
          i == ActiveItem ? Color.DarkOrange : Color.Olive);
        y++;
      }
    }

    internal static void MoveItem(Item item, int bag)
    {
      if (Inventory.Bags[bag].Items.Count >= Inventory.Bags[bag].Capacity) return;
      Inventory.Bags[bag].Items.Add(item);
      Inventory.Bags[ActiveBag].Items.Remove(item);
    }

    private static void SellItem()
    {
      Dialog.Confirm("Sell Item",
        $"Sell this item for {Inventory.Bags[ActiveBag].Items[ActiveItem].SellCost} gold? (Y or N)", out bool sell);
      if (sell) SellItem(Inventory.Bags[ActiveBag].Items[ActiveItem]);
      Draw();
    }

    private static void RemoveItem()
    {
      if (Inventory.Bags[ActiveBag].Items[ActiveItem].Quantity == 0)
      {
        Dialog.Notify("Can't Remove Item", "You don't have any of this item.");
        Draw();
        return;
      }

      Dialog.Confirm("Remove Item", "Are you sure you want to destroy this item? (Y or N)", out bool remove);
      if (remove) Inventory.Bags[ActiveBag].Items.RemoveAt(ActiveItem);
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
      if (newBag <= Inventory.Bags.Count && newBag >= 1 && newBag != ActiveBag)
        MoveItem(Inventory.Bags[ActiveBag].Items[ActiveItem], newBag);
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
      if (!Inventory.Bags[ActiveBag].Items[ActiveItem].Use())
      {
        Draw();
        Dialog.Notify("Can't Use Item", "You can't use this item.");
        Draw();
        return;
      }
      if (Inventory.Bags[ActiveBag].Items[ActiveItem].Quantity > 0) Inventory.Bags[ActiveBag].Items[ActiveItem].Quantity--;
      if (Inventory.Bags[ActiveBag].Items[ActiveItem].Quantity == 0) Inventory.Bags[ActiveBag].Items.RemoveAt(ActiveItem);
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
      if (item.Quantity == 0) Inventory.Bags[ActiveBag].Items.Remove(item);
    }

    internal static void KeyHandler()
    {
      ConsoleKeyInfo keyInfo = Console.ReadKey(true);
      switch (keyInfo.Key)
      {
        case ConsoleKey.Escape:
          DialogOpen = false;
          Dialog.Close();
          break;
        case ConsoleKey.D1:
        case ConsoleKey.D2:
        case ConsoleKey.D3:
        case ConsoleKey.D4:
        case ConsoleKey.D5:
          if (Inventory.Bags.Count < (int)keyInfo.Key - 48) return;
          ActiveBag = (int)keyInfo.Key - 49;
          DrawBag(Inventory.Bags[(int)keyInfo.Key - 49]);
          break;
        case ConsoleKey.PageUp:
          if (ActiveBag == 0) ActiveBag = Inventory.Bags.Count - 1;
          else ActiveBag--;
          break;
        case ConsoleKey.PageDown:
          if (ActiveBag == Inventory.Bags.Count - 1) ActiveBag = 0;
          else ActiveBag++;
          break;
        case ConsoleKey.UpArrow:
          if (ActiveItem == 0) ActiveItem = Inventory.Bags[ActiveBag].Items.Count - 1;
          else ActiveItem--;
          break;
        case ConsoleKey.DownArrow:
          if (ActiveItem == Inventory.Bags[ActiveBag].Items.Count - 1) ActiveItem = 0;
          else ActiveItem++;
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
