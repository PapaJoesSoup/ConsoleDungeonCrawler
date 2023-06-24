﻿using System.Drawing;

namespace ConsoleDungeonCrawler.Game.Entities;

internal class ObjectType
{
  internal readonly char Symbol = ' ';
  internal readonly string Name = "Empty";
  internal readonly string Singular = "Empty Space";
  internal readonly string Plural = "Empty Spaces";
  internal Color ForegroundColor = Color.Black;
  internal Color BackgroundColor = Color.Black;
  internal readonly bool IsPassable = false;
  internal readonly bool IsVisible = true;
  internal readonly bool IsAttackable = false;
  internal readonly bool IsLootable = false;

  internal ObjectType()
  {
  }

  internal ObjectType(bool isPassable, bool isVisible = true)
  {
    IsPassable = isPassable;
    IsVisible = isVisible;
  }

  public ObjectType(char symbol, string name, string singular, string plural, Color foregroundColor, Color backgroundColor, bool isPassable, bool isAttackable, bool isLootable) : this()
  {
    Symbol = symbol;
    Name = name;
    Singular = singular;
    Plural = plural;
    ForegroundColor = foregroundColor;
    BackgroundColor = backgroundColor;
    IsPassable = isPassable;
    IsAttackable = isAttackable;
    IsLootable = isLootable;
  }
}