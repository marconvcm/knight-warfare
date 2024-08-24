using Godot;
using System;
using System.Threading.Tasks;


public partial class Bullet : Area2D
{
   private Boolean IsTween = false;
   private Vector2 InitialPosition = Vector2.Zero;

   [Export]
   public Node2D Creator;

   [Export]
   public float DistanceLifeSpan = 200f;

   [Export]
   public Vector2 Direction = Vector2.Right;

   [Export]
   public AnimatedSprite2D Sprite;

   [Export]
   public CollisionShape2D Shape;

   [Export]
   public PointLight2D Light;

   [Export]
   public CpuParticles2D Particles;

   [Export]
   public float speed = 800;

   public override void _Ready()
   {
      InitialPosition = Position;

      BodyEntered += (body) =>
      {
         if (body != Creator)
         {
            if (body is Actor actor)
            {
               actor.Damage(10);
            }
            
            QueueFree();
         }
      };
   }

   // Called every frame. 'delta' is the elapsed time since the previous frame.
   public override void _Process(double delta)
   {
      if (Direction == Vector2.Left)
      {
         FlipLeft();
      }
      else
      {
         FlipRight();
      }

      Position += Direction * (speed * (float)delta);

      if (Position.DistanceTo(InitialPosition) > DistanceLifeSpan)
      {
         QueueFree();
      }
   }

   public void FlipLeft()
   {
      Particles.Gravity = Vector2.Left * Particles.Gravity.Abs().X;
      Sprite.FlipH = true;
   }

   public void FlipRight()
   {
      Particles.Gravity = Vector2.Right * Particles.Gravity.Abs().X;
      Sprite.FlipH = false;
   }
}