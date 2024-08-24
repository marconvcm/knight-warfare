using Godot;
using System;

[GlobalClass]
public partial class StatProgressBarPlugin : Node
{
   [Export]
   public ProgressBar ProgressBar { get; set; }

   [Export]
   public StatPlugin Stat { get; set; }

   public override void _Ready()
   {
      Stat.ValueChanged += (value) => UpdateProgressBar(value);
      ProgressBar.MaxValue = Stat.MaxValue;
      ProgressBar.MinValue = Stat.MinValue;
      ProgressBar.Value = Stat.CurrentValue;
   }

   private void UpdateProgressBar(float value)
   {
      ProgressBar.Value = value;
   }
}
