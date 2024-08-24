using Godot;
using System;

public partial class Player : Actor
{
   private PackedScene BulletTemplate = GD.Load<PackedScene>("res://Templates/Bullet.tscn");

   [Export]
   public Marker2D topLeftLimit;

   [Export]
   public Marker2D bottomRightLimit;

   [Export]
   public Camera2D camera;

   private Bullet SpawnBullet() => BulletTemplate.Instantiate<Bullet>();

   public override void _Ready()
   {
      base._Ready();
      SpawnBullet().QueueFree();

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

   private void Fire()
   {
      Bullet bullet = SpawnBullet();
      bullet.Direction = sprite.FlipH ? Vector2.Left : Vector2.Right;
      bullet.Position = hook.GlobalPosition;
      GetParent().AddChild(bullet);
      if (bullet.Direction == Vector2.Left)
      {
         bullet.FlipLeft();
      }
   }

   public override Vector2 GetInputDirection()
   {
      return InputManager.GetDirection();
   }
}
