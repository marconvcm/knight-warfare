using Godot;
using System.Threading.Tasks;

public static class InputManager
{
   public const string ButtonLeft = "Player_Left";
   public const string ButtonRight = "Player_Right";
   public const string ButtonUp = "Player_Up";
   public const string ButtonDown = "Player_Down";
   public const string ButtonA = "Player_A";
   public const string ButtonB = "Player_B";
   public const string ButtonX = "Player_X";
   public const string ButtonY = "Player_Y";
   public const string ButtonL = "Player_L";
   public const string ButtonR = "Player_R";

   public static Vector2 GetDirection() => Input.GetVector(ButtonLeft, ButtonRight, ButtonUp, ButtonDown);

   public static bool IsMainActionPressed() => Input.IsActionPressed(ButtonA);

   public static bool IsSecondaryActionPressed() => Input.IsActionPressed(ButtonB);

   public static bool IsAux1ActionPressed() => Input.IsActionPressed(ButtonX);

   public static bool IsAux2ActionPressed() => Input.IsActionPressed(ButtonY);

   public static bool IsLeftModifierActionPressed() => Input.IsActionPressed(ButtonL);

   public static bool IsRightModifierActionPressed() => Input.IsActionPressed(ButtonR);

   public static bool IsAnyActionPressed() => IsMainActionPressed() || IsSecondaryActionPressed() || IsAux1ActionPressed() || IsAux2ActionPressed();

   public static bool IsAnyModifierPressed() => IsLeftModifierActionPressed() || IsRightModifierActionPressed();

   public static bool IsAnyDirectionPressed() => GetDirection() != Vector2.Zero;

   public static bool IsMainActionJustPressed() => Input.IsActionJustPressed(ButtonA);

   public static bool IsSecondaryActionJustPressed() => Input.IsActionJustPressed(ButtonB);

   public static bool IsAux1ActionJustPressed() => Input.IsActionJustPressed(ButtonX);

   public static bool IsAux2ActionJustPressed() => Input.IsActionJustPressed(ButtonY);

   public static bool IsLeftModifierActionJustPressed() => Input.IsActionJustPressed(ButtonL);

   public static bool IsRightModifierActionJustPressed() => Input.IsActionJustPressed(ButtonR);

   public static bool IsAnyActionJustPressed() => IsMainActionJustPressed() || IsSecondaryActionJustPressed() || IsAux1ActionJustPressed() || IsAux2ActionJustPressed();

   public static bool IsAnyModifierJustPressed() => IsLeftModifierActionJustPressed() || IsRightModifierActionJustPressed();

   public static bool IsAnyDirectionJustPressed() => GetDirection() != Vector2.Zero;
   
   

   public static bool IsMainActionReleased() => Input.IsActionJustReleased(ButtonA);

   public static bool IsSecondaryActionReleased() => Input.IsActionJustReleased(ButtonB);

   public static bool IsAux1ActionReleased() => Input.IsActionJustReleased(ButtonX);

   public static bool IsAux2ActionReleased() => Input.IsActionJustReleased(ButtonY);

   public static bool IsLeftModifierActionReleased() => Input.IsActionJustReleased(ButtonL);

   public static bool IsRightModifierActionReleased() => Input.IsActionJustReleased(ButtonR);

   public static bool IsAnyActionReleased() => IsMainActionReleased() || IsSecondaryActionReleased() || IsAux1ActionReleased() || IsAux2ActionReleased();

   public static bool IsAnyModifierReleased() => IsLeftModifierActionReleased() || IsRightModifierActionReleased();

}
