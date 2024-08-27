using Godot;


/// <summary>
/// The plugin that will be used to display a hint using tween animation.
/// </summary>
[GlobalClass]
public partial class TweenPlugin : Node
{
   /// <summary>
   /// The node that will be used as a hint. It will display using tween animation.
   /// </summary>
   [Export]
   public Node2D target;

   /// <summary>
   /// The configuration for the hint plugin.
   /// </summary>
   [Export]
   public TweenPluginResource resource;

   private Tween tween;

   /// <summary>
   /// The event that will be triggered when the tween animation is completed.
   /// </summary>
   [Signal]
   public delegate void TweenCompletedEventHandler();

   private Vector2 initialScale;
   private Vector2 initialPosition;
   public override void _Ready()
   {
      if (target == null)
      {
         GD.PrintErr("The hint node is not set.");
         return;
      }

      initialScale = target.Scale;
      initialPosition = target.Position;

      target.Hide();

      if (resource.AutoPlay)
      {
         Play();
      }
   }

   public void Play()
   {
      tween = GetTree().CreateTween();

      tween.Finished += () => EmitSignal(nameof(TweenCompleted));

      target.Show();

      tween.TweenProperty(target, "position", target.Position + resource.PositionOffset, resource.Duration).From(initialPosition);
      tween.TweenProperty(target, "scale", target.Scale + resource.ScaleOffset, resource.Duration).From(initialScale);

      if (resource.EnableOpacity)
      {
         tween.TweenProperty(target, "modulate", Colors.White, resource.Duration).From(Colors.Transparent);
      }

      tween.Play();
   }

   public void Stop()
   {
      tween.Stop();
      target.Hide();
   }
}
