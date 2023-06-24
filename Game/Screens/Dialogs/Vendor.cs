using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;
using System.Drawing;
using ConsoleDungeonCrawler.Game.Entities.Items;

namespace ConsoleDungeonCrawler.Game.Screens.Dialogs
{
  internal static class Vendor
  {
    private static Dictionary<int, Bag> storeInventory = new();
    internal static VendorType VendorType = VendorType.General;

    private static Color color = Color.DarkOrange;
    private static Color backgroundColor = Color.Black;
    private static Color fillColor = Color.SaddleBrown;
    private static Color textColor = Color.Bisque;
    private static Color selectedColor = Color.Lime;
    private static Color selectedBackgroundColor = Color.DarkOrange;

    private static int activeBag;
    private static int activeItem;
    private static int activeVendorTab;
    private static int activeVendorItem;
    private static int listStart;
    private static int listSize = 20;
    private static int listWidth = 40;
    private static bool storeIsActive = true;
    static bool dialogOpen = false;

    static Box dialogBox = new(Console.WindowWidth / 2 - 58, Console.WindowHeight / 2 - 13, 116, 26);
    static Box legendBox = new(dialogBox.Left, dialogBox.Top + 11, 25, 12);
    private static Position tabPosition = new(legendBox.Left + legendBox.Width + 4, dialogBox.Top + 1);
    private static Position bagPosition = new(tabPosition.X + listWidth + 4, dialogBox.Top + 1);

    static Vendor()
    {
      BuildVendorInventory();
    }

    internal static void Draw()
    {
      dialogOpen = true;
      Dialog.Draw($" {VendorType} {(VendorType == VendorType.General ? "Items" : "")} Merchant ", color, backgroundColor, fillColor, textColor, dialogBox);

      SelectVendorInventory(VendorType);

      while (dialogOpen)
      {
        // Create a new dialogBox for the player inventory
        int x = 1;
        int y = 1;
        "Select Bag: ([x])".WriteAt(dialogBox.Left + 2, dialogBox.Top + 1, textColor, fillColor);
        y += 2;
        foreach (var bag in Inventory.Bags)
        {
          $"[{x}] Bag {x}".WriteAt(dialogBox.Left + 2, dialogBox.Top + y, textColor, activeBag == x - 1 ? selectedBackgroundColor : fillColor);
          x++;
          y++;
        }
        DrawLegend();

        // Draw the vendor inventory
        DrawTab(storeInventory[activeVendorTab], tabPosition, listSize, listStart);
        // Draw the player inventory
        DrawBag(Inventory.Bags[activeBag], bagPosition);

        KeyHandler();
      }
    }

    internal static void BuildVendorInventory()
    {
      // General
      // potions
      List<Item> generalItems = new ();
      foreach (KeyValuePair<BuffType, List<Potion>> potions in Inventory.Potions)
        foreach (Potion potion in potions.Value)
          generalItems.Add(potion);

      // Food and Drink
      Bag food = new(Inventory.Foods.Count);
      foreach (KeyValuePair<BuffType, List<Food>> foodType in Inventory.Foods)
        foreach (Food f in foodType.Value) 
          generalItems.Add(f);

      // Bandages
      Bag bandages = new(Inventory.Bandages.Count);
      foreach (Bandage bandage in Inventory.Bandages)
        generalItems.Add(bandage);

      storeInventory.Add((int)VendorType.General, new Bag(generalItems.Count));
      storeInventory[(int)VendorType.General].Items = generalItems;

      // weapons
      List<Item> weapons = new ();
      foreach (KeyValuePair<WeaponType, List<Weapon>> weapon in Inventory.WeaponTypes)
        foreach (Weapon w in weapon.Value) weapons.Add(w);

      storeInventory.Add((int)VendorType.Weapons, new Bag(weapons.Count));
      storeInventory[(int)VendorType.Weapons].Items = weapons;

      // armor
      List<Item> armor = new ();
      foreach (KeyValuePair<ArmorType, Dictionary<ArmorName, List<Armor>>> armorType in Inventory.ArmorDictionary)
        foreach (KeyValuePair<ArmorName, List<Armor>> N in armorType.Value)
          foreach (Armor a in N.Value) armor.Add(a);

      storeInventory.Add((int)VendorType.Armor, new Bag(armor.Count));
      storeInventory[(int)VendorType.Armor].Items = armor;
    }

    internal static void SelectVendorInventory(VendorType vendorType)
    {
      VendorType = vendorType;
      switch (vendorType)
      {
        case VendorType.General:
          activeVendorTab = 0;
          break;
        case VendorType.Weapons:
          activeVendorTab = 1;
          break;
        case VendorType.Armor:
          activeVendorTab = 2;
          break;
      }
    }

    private static void DrawLegend()
    {
      int y = legendBox.Top + 1;
      "Legend:".WriteAt(legendBox.Left + 2, legendBox.Top, textColor, fillColor); y++;

      "[1-5] Select Bag".WriteAt(dialogBox.Left + 2, y, textColor, fillColor); y++;
      $"[{ConsoleKey.PageUp}] Prev Bag".WriteAt(legendBox.Left + 2, y, textColor, fillColor); y++;
      $"[{ConsoleKey.PageDown}] Next Bag".WriteAt(legendBox.Left + 2, y, textColor, fillColor); y++;
      $"[{ConsoleKey.LeftArrow}] Vendor Items".WriteAt(legendBox.Left + 2, y, textColor, fillColor); y++;
      $"[{ConsoleKey.RightArrow}] Bag Items".WriteAt(legendBox.Left + 2, y, textColor, fillColor); y++;
      $"[{ConsoleKey.UpArrow}] Prev Item".WriteAt(legendBox.Left + 2, y, textColor, fillColor); y++;
      $"[{ConsoleKey.DownArrow}] Next Item".WriteAt(legendBox.Left + 2, y, textColor, fillColor); y++;
      $"[{ConsoleKey.B}] Buy Item".WriteAt(legendBox.Left + 2, y, textColor, fillColor); y++;
      $"[{ConsoleKey.S}] Sell Item".WriteAt(legendBox.Left + 2, y, textColor, fillColor); y++;
      $"[{ConsoleKey.R}] Remove Item".WriteAt(legendBox.Left + 2, y, textColor, fillColor); y++;
      $"[{ConsoleKey.Escape}] Close Dialog".WriteAt(legendBox.Left + 2, y, textColor, fillColor);
    }

    internal static void DrawTab(Bag bag, Position position, int maxHeight, int scrollY)
    {
      int x = position.X;
      int y = position.Y;
      $"{VendorType} {(VendorType == VendorType.General? "items": "")} for sale:".WriteAt(x, y, storeIsActive? selectedColor : textColor, fillColor);
      y += 2;
      ("Item".PadRight(listWidth - 7) + "Buy".PadLeft(7)).WriteAt(x, y, textColor, fillColor);
      y++;
      // foreach loop to draw the items in the bag using the scrollY and maxHeight to determine which items to draw
      for (int i = scrollY; i < scrollY + maxHeight; i++)
      {
        ($"[{i + 1}]:  {storeInventory[activeVendorTab].Items[i].Name}".PadRight(listWidth - 7) + 
         $"${decimal.Round(storeInventory[activeVendorTab].Items[i].BuyCost, 2)}g".PadLeft(7))
          .WriteAt(x, y, i == activeVendorItem ? selectedColor : textColor, i == activeVendorItem ? selectedBackgroundColor : fillColor);
        y++;
      }
    }

    internal static void DrawBag(Bag bag, Position position)
    {
      int x = position.X;
      int y = position.Y;
      $"Bag {activeBag + 1} Contents:".WriteAt(x, y, !storeIsActive ? selectedColor : textColor, fillColor);
      y += 2;
      ("Item".PadRight(listWidth - 7) + "Sell".PadLeft(7)).WriteAt(x, y, textColor, fillColor);
      y++;
      for (int i = 0; i < bag.Capacity; i++)
      {
        if (i >= bag.Items.Count)
          $"[{i + 1}]:  Empty".PadRight(listWidth).WriteAt(x, y, textColor, fillColor);
        else
          ($"[{i + 1}]:  ({bag.Items[i].Quantity}) {bag.Items[i].Name}".PadRight(listWidth - 7) +
           $"${decimal.Round(bag.Items[i].SellCost, 2)}g".PadLeft(7))
             .WriteAt(x, y, i == activeItem ? selectedColor : textColor, i == activeItem ? selectedBackgroundColor : fillColor);
        y++;
      }
    }
    
    internal static void SellItem(Item item)
    {
      Player.Gold += item.SellCost;
      Inventory.Bags[activeBag].RemoveItem(item);
    }

    internal static void BuyItem(Item item)
    {
      if (Player.Gold < item.BuyCost)
      {
        Dialog.Notify("Buy Item", "You don't have enough gold to buy this item.");
        Draw();
        return;
      }
      Player.Gold -= item.BuyCost;
      Inventory.Bags[activeBag].AddItem(item);
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

    internal static void KeyHandler()
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
          DrawBag(Inventory.Bags[(int)keyInfo.Key - 49], bagPosition);
          break;
          break;
        case ConsoleKey.PageUp:
          break;
        case ConsoleKey.PageDown:
          break;
        case ConsoleKey.UpArrow:
          if (storeIsActive)
          {
            if (activeVendorItem == 0) 
              activeVendorItem = storeInventory[activeVendorTab].Items.Count - 1;
            else activeVendorItem--; 

            if (activeVendorItem > listSize - 1) listStart = activeVendorItem - (listSize - 1);
            if (activeVendorItem < listSize - 1) listStart = 0;
          }
          else
          {
            if (activeItem == 0) 
              activeItem = Inventory.Bags[activeBag].Items.Count - 1;
            else activeItem--;
          }
          break;
        case ConsoleKey.DownArrow:
          if (storeIsActive)
          {
            if (activeVendorItem == storeInventory[activeVendorTab].Items.Count - 1) 
              activeVendorItem = 0;
            else activeVendorItem++;

            if (activeVendorItem > listSize - 1) listStart = activeVendorItem - (listSize - 1);
            if (activeVendorItem < listSize - 1) listStart = 0;
          }
          else
          {
            if (activeItem == Inventory.Bags[activeBag].Items.Count - 1) activeItem = 0;
            else activeItem++;
          }
          break;
        case ConsoleKey.LeftArrow:
          storeIsActive = true;
          break;
        case ConsoleKey.RightArrow:
          storeIsActive = false;
          break;
        case ConsoleKey.R:
          RemoveItem();
          break;
        case ConsoleKey.S:
          SellItem(Inventory.Bags[activeBag].Items[activeItem]);
          break;
        case ConsoleKey.B:
          BuyItem(storeInventory[activeVendorTab].Items[activeVendorItem]);
          break;
      }
    }
  }
}
