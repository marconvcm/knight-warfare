using Godot;
using System;

public partial class Minion : Actor
{
   [Export]
   public NodePath PlayerPath { get; set; }

   private Player Player;

   [Export]
   public float AttackRange = 30.0f;
   [Export]
   public float FireRange = 200.0f;


   public override void _Ready()
   {
      base._Ready();


      Player = GetNode<Player>(PlayerPath);
   }

   public override Vector2 GetInputDirection()
   {
      if (Player != null)
      {

         Vector2 directionToPlayer = Player.GlobalPosition - GlobalPosition;

         if (directionToPlayer.Length() <= AttackRange)
         {
            return Vector2.Zero;
         }

         return new Vector2(Mathf.Sign(directionToPlayer.X), 0);
      }

      return Vector2.Zero;
   }


   public override void _PhysicsProcess(double delta)
   {
      base._PhysicsProcess(delta);

      if (Player == null)
      {
         return;
      }

      Vector2 directionToPlayer = Player.GlobalPosition - GlobalPosition;

      if (IsOnWall())
      {
         Jump();
      }

      if (directionToPlayer.Length() <= AttackRange && IsOnFloor())
      {
         Attack();
      }

      else if (directionToPlayer.Length() > FireRange && directionToPlayer.Length() > AttackRange)
      {
         Fire();
      }
   }
}
