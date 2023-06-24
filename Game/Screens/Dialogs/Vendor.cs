using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;
using System.Drawing;
using ConsoleDungeonCrawler.Game.Entities.Items;

namespace ConsoleDungeonCrawler.Game.Screens.Dialogs
{
  internal static class Vendor
  {
    private static readonly Dictionary<int, Bag> StoreInventory = new();
    private static VendorType vendorType = VendorType.General;

    private static readonly Color Color = Color.DarkOrange;
    private static readonly Color BackgroundColor = Color.Black;
    private static readonly Color FillColor = Color.SaddleBrown;
    private static readonly Color TextColor = Color.Bisque;
    private static readonly Color SelectedColor = Color.Lime;
    private static readonly Color SelectedBackgroundColor = Color.DarkOrange;

    private static int activeBag;
    private static int activeItem;
    private static int activeVendorTab;
    private static int activeVendorItem;
    private static int listStart;
    private static readonly int ListSize = 20;
    private static readonly int ListWidth = 40;
    private static bool storeIsActive = true;
    private static bool dialogOpen = false;

    private static readonly Box DialogBox = new(Console.WindowWidth / 2 - 58, Console.WindowHeight / 2 - 13, 116, 26);
    private static readonly Box LegendBox = new(DialogBox.Left, DialogBox.Top + 11, 25, 12);
    private static readonly Position TabPosition = new(LegendBox.Left + LegendBox.Width + 4, DialogBox.Top + 1);
    private static readonly Position BagPosition = new(TabPosition.X + ListWidth + 4, DialogBox.Top + 1);

    static Vendor()
    {
      BuildVendorInventory();
    }

    internal static void Draw()
    {
      dialogOpen = true;
      Dialog.Draw($" {vendorType} {(vendorType == VendorType.General ? "Items" : "")} Merchant ", Color, BackgroundColor, FillColor, TextColor, DialogBox);

      SelectVendorInventory(vendorType);

      while (dialogOpen)
      {
        // Create a new dialogBox for the player inventory
        int x = 1;
        int y = 1;
        "Select Bag: ([x])".WriteAt(DialogBox.Left + 2, DialogBox.Top + 1, TextColor, FillColor);
        y += 2;
        foreach (var bag in Inventory.Bags)
        {
          $"[{x}] Bag {x}".WriteAt(DialogBox.Left + 2, DialogBox.Top + y, TextColor, activeBag == x - 1 ? SelectedBackgroundColor : FillColor);
          x++;
          y++;
        }
        DrawLegend();

        // Draw the vendor inventory
        DrawTab(StoreInventory[activeVendorTab], TabPosition, ListSize, listStart);
        // Draw the player inventory
        DrawBag(Inventory.Bags[activeBag], BagPosition);

        KeyHandler();
      }
    }

    private static void BuildVendorInventory()
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

      StoreInventory.Add((int)VendorType.General, new Bag(generalItems.Count));
      StoreInventory[(int)VendorType.General].Items = generalItems;

      // weapons
      List<Item> weapons = new ();
      foreach (KeyValuePair<WeaponType, List<Weapon>> weapon in Inventory.WeaponTypes)
        foreach (Weapon w in weapon.Value) weapons.Add(w);

      StoreInventory.Add((int)VendorType.Weapons, new Bag(weapons.Count));
      StoreInventory[(int)VendorType.Weapons].Items = weapons;

      // armor
      List<Item> armor = new ();
      foreach (KeyValuePair<ArmorType, Dictionary<ArmorName, List<Armor>>> armorType in Inventory.ArmorDictionary)
        foreach (KeyValuePair<ArmorName, List<Armor>> n in armorType.Value)
          foreach (Armor a in n.Value) armor.Add(a);

      StoreInventory.Add((int)VendorType.Armor, new Bag(armor.Count));
      StoreInventory[(int)VendorType.Armor].Items = armor;
    }

    private static void SelectVendorInventory(VendorType vendorType)
    {
      Vendor.vendorType = vendorType;
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
      int y = LegendBox.Top + 1;
      "Legend:".WriteAt(LegendBox.Left + 2, LegendBox.Top, TextColor, FillColor); y++;

      "[1-5] Select Bag".WriteAt(DialogBox.Left + 2, y, TextColor, FillColor); y++;
      $"[{ConsoleKey.PageUp}] Prev Bag".WriteAt(LegendBox.Left + 2, y, TextColor, FillColor); y++;
      $"[{ConsoleKey.PageDown}] Next Bag".WriteAt(LegendBox.Left + 2, y, TextColor, FillColor); y++;
      $"[{ConsoleKey.LeftArrow}] Vendor Items".WriteAt(LegendBox.Left + 2, y, TextColor, FillColor); y++;
      $"[{ConsoleKey.RightArrow}] Bag Items".WriteAt(LegendBox.Left + 2, y, TextColor, FillColor); y++;
      $"[{ConsoleKey.UpArrow}] Prev Item".WriteAt(LegendBox.Left + 2, y, TextColor, FillColor); y++;
      $"[{ConsoleKey.DownArrow}] Next Item".WriteAt(LegendBox.Left + 2, y, TextColor, FillColor); y++;
      $"[{ConsoleKey.B}] Buy Item".WriteAt(LegendBox.Left + 2, y, TextColor, FillColor); y++;
      $"[{ConsoleKey.S}] Sell Item".WriteAt(LegendBox.Left + 2, y, TextColor, FillColor); y++;
      $"[{ConsoleKey.R}] Remove Item".WriteAt(LegendBox.Left + 2, y, TextColor, FillColor); y++;
      $"[{ConsoleKey.Escape}] Close Dialog".WriteAt(LegendBox.Left + 2, y, TextColor, FillColor);
    }

    private static void DrawTab(Bag bag, Position position, int maxHeight, int scrollY)
    {
      int x = position.X;
      int y = position.Y;
      $"{vendorType} {(vendorType == VendorType.General? "items": "")} for sale:".WriteAt(x, y, storeIsActive? SelectedColor : TextColor, FillColor);
      y += 2;
      ("Item".PadRight(ListWidth - 7) + "Buy".PadLeft(7)).WriteAt(x, y, TextColor, FillColor);
      y++;
      // foreach loop to draw the items in the bag using the scrollY and maxHeight to determine which items to draw
      for (int i = scrollY; i < scrollY + maxHeight; i++)
      {
        ($"[{i + 1}]:  {StoreInventory[activeVendorTab].Items[i].Name}".PadRight(ListWidth - 7) + 
         $"${decimal.Round(StoreInventory[activeVendorTab].Items[i].BuyCost, 2)}g".PadLeft(7))
          .WriteAt(x, y, i == activeVendorItem ? SelectedColor : TextColor, i == activeVendorItem ? SelectedBackgroundColor : FillColor);
        y++;
      }
    }

    private static void DrawBag(Bag bag, Position position)
    {
      int x = position.X;
      int y = position.Y;
      $"Bag {activeBag + 1} Contents:".WriteAt(x, y, !storeIsActive ? SelectedColor : TextColor, FillColor);
      y += 2;
      ("Item".PadRight(ListWidth - 7) + "Sell".PadLeft(7)).WriteAt(x, y, TextColor, FillColor);
      y++;
      for (int i = 0; i < bag.Capacity; i++)
      {
        if (i >= bag.Items.Count)
          $"[{i + 1}]:  Empty".PadRight(ListWidth).WriteAt(x, y, TextColor, FillColor);
        else
          ($"[{i + 1}]:  ({bag.Items[i].Quantity}) {bag.Items[i].Name}".PadRight(ListWidth - 7) +
           $"${decimal.Round(bag.Items[i].SellCost, 2)}g".PadLeft(7))
             .WriteAt(x, y, i == activeItem ? SelectedColor : TextColor, i == activeItem ? SelectedBackgroundColor : FillColor);
        y++;
      }
    }

    private static void SellItem(Item item)
    {
      Player.Gold += item.SellCost;
      Inventory.Bags[activeBag].RemoveItem(item);
    }

    private static void BuyItem(Item item)
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
          DrawBag(Inventory.Bags[(int)keyInfo.Key - 49], BagPosition);
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
              activeVendorItem = StoreInventory[activeVendorTab].Items.Count - 1;
            else activeVendorItem--; 

            if (activeVendorItem > ListSize - 1) listStart = activeVendorItem - (ListSize - 1);
            if (activeVendorItem < ListSize - 1) listStart = 0;
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
            if (activeVendorItem == StoreInventory[activeVendorTab].Items.Count - 1) 
              activeVendorItem = 0;
            else activeVendorItem++;

            if (activeVendorItem > ListSize - 1) listStart = activeVendorItem - (ListSize - 1);
            if (activeVendorItem < ListSize - 1) listStart = 0;
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
          BuyItem(StoreInventory[activeVendorTab].Items[activeVendorItem]);
          break;
      }
    }
  }
}
