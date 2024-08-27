using Godot;
using System;

public partial class Player : Actor
{
   [Export]
   public Marker2D topLeftLimit;

   [Export]
   public Marker2D bottomRightLimit;

   [Export]
   public TweenPlugin HintTweenPlugin;

   [Export]
   public Camera2D camera;

   public override Vector2 GetInputDirection() => IsStopped ? Vector2.Zero : InputManager.GetDirection();

   public override void _Ready()
   {
      base._Ready();
      camera.LimitLeft = (int)topLeftLimit.Position.X;
      camera.LimitTop = (int)topLeftLimit.Position.Y;
      camera.LimitRight = (int)bottomRightLimit.Position.X;
      camera.LimitBottom = (int)bottomRightLimit.Position.Y;

      Sensor.BodyEntered += (body) =>
      {
         if (body is Artillery artillery)
         {
            HintTweenPlugin.Play();
            this.Mount = artillery;
         }
      };

      Sensor.BodyExited += (body) =>
      {
         if (body is Artillery artillery)
         {
            HintTweenPlugin.Stop();
            this.Mount = null;
         }
      };

      Sensor.AreaEntered += (area) =>
      {
         if (area is Area2D)
         {
            HintTweenPlugin.Play();
         }
      };

      Sensor.AreaExited += (area) =>
      {
         if (area is Area2D)
         {
            HintTweenPlugin.Stop();
         }
      };
   }

   public override void _Input(InputEvent @event)
   {
      if (Mount is MountInterface mount && mount.IsMounted)
      {
         if (InputManager.IsAux2ActionJustPressed())
         {
            mount.Unmount();
            this.OnUnmount();
         }
         else
         {
            mount.HandlerOwnerInput(@event);
         }
      }
      else
      {
         if (InputManager.IsAux2ActionJustPressed())
         {
            if (Mount is Artillery artillery)
            {
               artillery.Mount(this);
               this.OnMount();
            }
         }

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

   private void OnMount()
   {
      defaultValuesBag["Offset"] = this.camera.Offset;
      this.camera.Offset = new Vector2(100f, 0);
      IsStopped = true;
   }

   private void OnUnmount()
   {
      this.camera.Offset = (Vector2)defaultValuesBag["Offset"];
      IsStopped = false;
   }
}
