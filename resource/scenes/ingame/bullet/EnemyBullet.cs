using Godot;
using System;

public partial class EnemyBullet : Area2D
{
	[Export]private float Speed = 100;
	private Vector2 _screenSize;
	
	public override void _Ready()
	{
		_screenSize = GetViewportRect().Size;
		AnimatedSprite2D animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animatedSprite2D.Animation = "default";
		animatedSprite2D.Play();

	}

	public void SetPosition(Vector2 pos)
	{
		Position = pos;
	}
	
	public override void _Process(double delta)
	{
		Vector2 next = Position;
		next.Y = next.Y +(Speed * (float)delta );
		Position = next;
	}



	private void OnVisibleOnScreenNotifier2dScreenExited()
	{
		QueueFree();
	}
}


