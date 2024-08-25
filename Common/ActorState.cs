using System;

public struct ActorState
{
   public static readonly ActorState Idle = new ActorState { Animation = "default" };

   public static readonly ActorState Running = new ActorState { Animation = "run" };

   public static readonly ActorState Jumping = new ActorState { Animation = "jump", KeepAtFrame = 0 };

   public static readonly ActorState Falling = new ActorState { Animation = "jump", KeepAtFrame = 1 };

   public static readonly ActorState Attacking = new ActorState { Animation = "attack" };

   public static readonly ActorState Shooting = new ActorState { Animation = "attack" };

   public static readonly ActorState Dashing = new ActorState { Animation = "run", KeepAtFrame = 1};

   public string Animation = "idle";

   public int KeepAtFrame = -1;

   public ActorState() { }


   public bool IsDashing() => Animation == Dashing.Animation && KeepAtFrame == Dashing.KeepAtFrame;

   public bool IsAttacking() => Animation == Attacking.Animation;

   public bool IsShooting() => Animation == Shooting.Animation;

   public bool IsIdle() => Animation == Idle.Animation;
}