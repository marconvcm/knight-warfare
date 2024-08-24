using Godot;
using System;

public partial class Player : Actor
{
   [Export]
   public Marker2D topLeftLimit;

   [Export]
   public Marker2D bottomRightLimit;

   [Export]
   public Camera2D camera;

   public override Vector2 GetInputDirection() => InputManager.GetDirection();

   public override void _Ready()
   {
      base._Ready();
      camera.LimitLeft = (int)topLeftLimit.Position.X;
      camera.LimitTop = (int)topLeftLimit.Position.Y;
      camera.LimitRight = (int)bottomRightLimit.Position.X;
      camera.LimitBottom = (int)bottomRightLimit.Position.Y;
   }

   public override void _Input(InputEvent @event)
   {
      if (InputManager.IsSecondaryActionJustPressed())
      {
         Fire();
      }

      if (InputManager.IsMainActionJustPressed())
      {
         Jump();
      }

      if (InputManager.IsRightModifierActionJustPressed())
      {
         Dash();
      }

      if (InputManager.IsAux1ActionJustPressed())
      {
         Attack();
      }
   }
}
