using System;
using Godot;
/// <summary>
/// The resource that will be used to configure the tween animation.
/// </summary>
[GlobalClass]
public partial class TweenPluginResource : Resource
{
   /// <summary>
   /// The duration of the tween animation.
   /// </summary>
   [Export]
   public float Duration { get; set; } = 1.0f;

   /// <summary>
   /// The offset that will be applied to the target node.
   /// </summary>
   [Export]
   public Vector2 PositionOffset { get; set; } = new Vector2(0, -50);

   /// <summary>
   /// The final scale of the target node.
   /// </summary>
   [Export]
   public Vector2 ScaleOffset { get; set; } = Vector2.Zero;

   /// <summary>
   /// The transition type of the tween animation.
   /// </summary>
   [Export]
   public Tween.TransitionType TransitionType { get; set; } = Tween.TransitionType.Linear;

   /// <summary>
   /// The ease type of the tween animation.
   /// </summary>
   [Export]
   public Tween.EaseType EaseType { get; set; } = Tween.EaseType.InOut;

   /// <summary>
   /// Enable the opacity of the target node.
   /// </summary>
   [Export]
   public bool EnableOpacity { get; set; } = false;

   /// <summary>
   /// Enable the auto play of the tween animation.
   /// </summary>
   [Export]
   public Boolean AutoPlay { get; set; } = false;
}
