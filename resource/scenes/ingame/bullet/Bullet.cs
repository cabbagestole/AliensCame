using Godot;
using System;

public partial class Bullet : NotifiableArea2D
{
	[Export]private float Speed = 300;
	private Vector2 _screenSize;
		
	public override void _Ready()
	{
		_screenSize = GetViewportRect().Size;
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
	}

	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
		notifyObservers();
	}
	
	private void OnAreaEntered(Area2D area)
	{
		EnemyBase enemybasse = area as EnemyBase;
		if(null != enemybasse)
		{
			enemybasse.Damage(1);
			notifyObservers();
			QueueFree();
		}
	}
}






