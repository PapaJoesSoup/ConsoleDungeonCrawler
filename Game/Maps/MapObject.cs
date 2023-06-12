﻿using System.Drawing;
using ConsoleDungeonCrawler.Extensions;
using ConsoleDungeonCrawler.Game.Entities;

namespace ConsoleDungeonCrawler.Game.Maps
{
  internal class MapObject : Position
  {
    internal ObjectType Type = new ObjectType();
    internal bool IsVisible = false;
    internal bool IsPassable = false;
    internal bool IsLootable = false;
    internal bool IsAttackable = false;
    internal Color ForegroundColor = Color.White;
    internal Color BackgroundColor = Color.Black;

    internal MapObject()
    {
    }

    internal MapObject(int x, int y)
    {
      X = x;
      Y = y;
      Type = new ObjectType();
      IsVisible = Type.IsVisible;
      IsPassable = Type.IsPassable;
      IsAttackable = Type.IsAttackable;
      IsLootable = Type.IsLootable;
      ForegroundColor = Type.ForegroundColor;
      BackgroundColor = Type.BackgroundColor;
    }

    internal MapObject(int x, int y, ObjectType type)
    {
      X = x;
      Y = y;
      Type = type;
      IsVisible = type.IsVisible;
      IsPassable = type.IsPassable;
      IsAttackable = type.IsAttackable;
      IsLootable = type.IsLootable;
      ForegroundColor = type.ForegroundColor;
      BackgroundColor = type.BackgroundColor;

    }

    internal MapObject(int x, int y, ObjectType type, bool isVisible)
    {
      X = x;
      Y = y;
      Type = type;
      IsVisible = isVisible;
      IsPassable = type.IsPassable;
      IsAttackable = type.IsAttackable;
      IsLootable = type.IsLootable;
      ForegroundColor = type.ForegroundColor;
      BackgroundColor = type.BackgroundColor;
    }

    internal void Draw()
    {
      if (!IsVisible || Type.Symbol == ' ') return;
      ConsoleEx.WriteAt(Type.Symbol, X + Map.Left, Y + Map.Top, ForegroundColor, BackgroundColor);
    }
  }

}