using System;
using System.Xml.Serialization;
using Godot;

public partial class Actor : CharacterBody2D
{
   public const float GRAVITY = 9.81f;

   public const float GOLDEN_RATIO = 1.61803398875f;

   [Export]
   public float Meter { get; set; } = 16.0f; // 1 meter = 16 pixels

   [Export]
   public float WalkingModifier { get; set; } = GOLDEN_RATIO; // 1.5 m/s

   [Export]
   public float RunningModifier { get; set; } = GOLDEN_RATIO * 2.5f; // 4.5 m/s

   [Export]
   public float JumpHeight { get; set; } = 12.0f; // 12 meters

   [Export]
   public int JumpMax { get; set; } = 2;


   public Vector2 Direction { get; set; } = Vector2.Zero;

   public ActorState CurrentState { get; set; } = ActorState.Idle;

   public float WalkingSpeed
   {
      get { return Meter * WalkingModifier; }
   }

   public float RunningSpeed
   {
      get { return Meter * RunningModifier; }
   }

   public float JumpSpeed
   {
      get { return JumpHeight * Meter; }
   }

   public int JumpCount { get; set; } = 0;

   public float DashRefresh { get; set; } = -1.0f;

   public float AttackRefresh { get; set; } = -1.0f;

   private Vector2 AttackPosition { get; set; } = Vector2.Zero;

   [Export]
   public AnimatedSprite2D sprite;

   [Export]
   public AnimatedSprite2D attackSprite;

   [Export]
   public CollisionShape2D shape;

   [Export]
   public PointLight2D light;

   [Export]
   public Marker2D hook;

   public virtual Vector2 GetInputDirection()
   {
      return Vector2.Zero;
   }

    public override void _Ready()
    {
        AttackPosition = attackSprite.Position;
    }

    public void Jump()
   {
      JumpCount++;
      if (JumpCount >= JumpMax) { return; }
      Velocity = new Vector2(Velocity.X, -JumpSpeed);
   }

   public void Dash()
   {
      if (DashRefresh > 0.0f) { return; }
      DashRefresh = 1.8f;
      Velocity = new Vector2(Direction.X * RunningSpeed * 3, Velocity.Y);
   }

   public void Attack()
   {
      if (AttackRefresh > 0.0f || CurrentState.IsDashing()) { return; }
      AttackRefresh = 1.0f;
   }

   private void UpdateAnimation()
   {
      sprite.FlipH = Direction != Vector2.Zero ? Direction.X < 0 : sprite.FlipH;

      if (CurrentState.KeepAtFrame >= 0)
      {
         sprite.Stop();
         sprite.Animation = CurrentState.Animation;
         sprite.Frame = CurrentState.KeepAtFrame;
      }
      else
      {
         sprite.Play(CurrentState.Animation);
      }

      if (CurrentState.IsAttacking()) 
      {
         if (sprite.FlipH) {
            attackSprite.Position = AttackPosition * new Vector2(-1, 1);
            attackSprite.FlipH = true;
         } else {
            attackSprite.Position = AttackPosition * new Vector2(1, 1);
            attackSprite.FlipH = false;
         }
         attackSprite.Play();
      } else {
         attackSprite.Stop();
      }
   }

   private void UpdateState()
   {
      if (IsOnFloor())
      {
         if (Direction == Vector2.Zero)
         {
            CurrentState = ActorState.Idle;
         }
         else
         {
            CurrentState = ActorState.Running;
         }

         // Reset jump count when on floor
         JumpCount = 0;
      }
      else
      {
         if (Velocity.Y < 0)
         {
            CurrentState = ActorState.Jumping;
         }
         else
         {
            CurrentState = ActorState.Falling;
         }
      }

      if (DashRefresh > 0.0f)
      {
         DashRefresh -= 0.1f;
         CurrentState = ActorState.Dashing;
      }

      if (AttackRefresh > 0.0f)
      {
         CurrentState = ActorState.Attacking;
         if (attackSprite.IsPlaying() && attackSprite.Frame > 4)
         {
            AttackRefresh = -1.0f;
         }
      }
   }

   public override void _PhysicsProcess(double delta)
   {
      Direction = GetInputDirection();
      UpdateState();
      UpdateAnimation();
      Velocity = GetNextVelocity(delta);
      MoveAndSlide();
   }

   private Vector2 GetNextVelocity(double delta)
   {
      Vector2 velocity = Velocity;

      if (Direction == Vector2.Zero)
      {
         velocity = new Vector2(0, velocity.Y);
      }
      else
      {
         if (CurrentState.IsDashing())
         {
            velocity = new Vector2(Direction.X * RunningSpeed * 3 * DashRefresh, velocity.Y);
         }
         else
         {
            if (CurrentState.IsAttacking() && IsOnFloor())
            {
               velocity = new Vector2(Direction.X * (RunningSpeed / 2), velocity.Y);
            }
            else
            {
               velocity = new Vector2(Direction.X * RunningSpeed, velocity.Y);
            }
         }
      }

      if (!IsOnFloor())
      {
         velocity = new Vector2(velocity.X, velocity.Y + GRAVITY);
      }

      return velocity;
   }
}
