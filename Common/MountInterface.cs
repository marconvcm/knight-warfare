using Godot;

public partial interface MountInterface
{
	public Actor CurrentOwner { get; }
	public Vector2 Offset { get; }
	public bool IsMounted { get; }
	public void Unmount();
	public void Mount(Actor owner);
	public void HandlerOwnerInput(InputEvent @event);
	public delegate void OnMountEventHandler(Actor owner);
}