using Godot;
using System;

public partial class ChargeBullet : NotifiableArea2D
{
	[Export]private float Speed = 400;
	private Vector2 _screenSize;
	private Vector2 _move = Vector2.Zero;	
	private Sprite2D _sprite;
		
	public override void _Ready()
	{
		_screenSize = GetViewportRect().Size;
		_sprite = (Sprite2D)FindChildren("ChargeBullet","Sprite2D")[0];

	}
	
	public void SetPosition(Vector2 pos)
	{
		Position = pos;
	}
	
	public override void _Process(double delta)
	{
		Vector2 next = Position;
		next.Y = next.Y -(Speed * (float)delta );
		Position = next;
		_sprite.FlipH = (0 == ((int)next.Y) % 2)? true: false;
	}

	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		GD.Print("ChargeBullet out of screen");
		QueueFree();
		notifyObservers();
	}
}
