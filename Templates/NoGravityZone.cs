using Godot;
using System;

public partial class NoGravityZone : Area2D
{
   [Export]
   public float GravityScale = 0.0f;

   public override void _Ready()
   {
      base._Ready();
      BodyEntered += OnBodyEntered;
      BodyExited += OnBodyExited;
   }

   private void OnBodyExited(Node2D body)
   {
      if (body is Actor actor)
      {
         actor.GravityScale = 1.0f;
      }
   }

   private void OnBodyEntered(Node2D body)
   {
      if (body is Actor actor)
      {
         actor.GravityScale = GravityScale;
      }
   }
}
