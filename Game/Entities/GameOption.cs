namespace ConsoleDungeonCrawler.Game.Entities
{
  internal class GameOption<T>
  {
    internal string Name { get; set; }
    internal T? Value;
    internal T? TempValue;
    internal readonly int ActionCount;
    internal int ActionIdx;

    internal event EventHandler OnValueChanged;

    internal GameOption()
    {
      Name = "";
      Value = default;
      TempValue = default;
      ActionCount = GetActionCount();
      SetActionIdx();
    }

    internal GameOption(string name, T? value)
    {
      Name = name;
      this.Value = value;
      TempValue = value;
      ActionCount = GetActionCount();
      SetActionIdx();
    }

    private int GetActionCount()
    {
      return Value is bool ? 2 : 4;
    }

    private void SetActionIdx()
    {
      if (this.Value is bool)
        ActionIdx = Value.Equals(true) ? 1 : 0;
      else
        ActionIdx = 0;
    }

    internal void RaiseEvent()
    {
      EventHandler raiseEvent = OnValueChanged;
      if (raiseEvent == null) return;
      raiseEvent(this, EventArgs.Empty);
    }
  }
}
