using Godot;
using System;

[GlobalClass]
public partial class StatPlugin : Node
{
   private float _value;

   [Export]
   public string Tag { get; set; }

   [Export]
   public float MinValue { get; set; }

   [Export]
   public float MaxValue { get; set; }

   [Export]
   public float DefaultValue { get; set; }

   [Export]
   public float RechargeRate { get; set; } = 0.0f;

   public bool IsEmpty
   {
      get
      {
         return CurrentValue <= MinValue;
      }
   }

   public float CurrentValue
   {
      get
      {
         return _value;
      }
      set
      {
         _value = value;
         EmitSignal(nameof(ValueChanged), _value);
      }
   }

   public float Progress
   {
      get
      {
         return (CurrentValue - MinValue) / (MaxValue - MinValue);
      }
   }

   [Signal]
   public delegate void ValueChangedEventHandler(float value);

   public override void _Ready()
   {
      CurrentValue = DefaultValue;
   }

   public StatPlugin Add(float value)
   {
      CurrentValue = Mathf.Clamp(CurrentValue + value, MinValue, MaxValue);
      return this;
   }

   public StatPlugin Subtract(float value)
   {
      CurrentValue = Mathf.Clamp(CurrentValue - value, MinValue, MaxValue);
      return this;
   }

   public StatPlugin Set(float value)
   {
      CurrentValue = Mathf.Clamp(value, MinValue, MaxValue);
      return this;
   }

   public StatPlugin Tick(double delta)
   {
      if (CurrentValue < MaxValue)
      {
         Add(RechargeRate * (float)delta);
      }
      return this;
   }

   public override void _Process(double delta)
   {
      if (RechargeRate > 0.0f)
      {
         Tick(delta);
      }
   }
}
