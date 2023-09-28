using Godot;
using System;

public partial class EnemyOrigin : Node2D
{

	[Export] private PackedScene EnemyFM { get; set; }
	
	private bool _isTouch = false;
	private Direction _direction = Direction.East;
	
	public override void _Ready()
	{
		Position = (GetViewportRect().Size * (float)0.1);

		EnemyFM enemyFm = (EnemyFM)EnemyFM.Instantiate();
		enemyFm.AddTouchObserver(touch);
		AddChild(enemyFm);

		
	}

	[Export] public float Speed = 1;
	
	public override void _Process(double delta)
	{
		float x = 0;
		float y = 0;
		if(_direction == Direction.East) {
			x = Speed;
		} else {
			x = -Speed;
		}
		if(_isTouch){
			y = 16;
			_isTouch = false;
		}
		Vector2 next = Position;
		next.X += x;
		next.Y += y;
		Position = next;

	}
	
	private void touch(Direction direction)
	{
		GD.Print("touch = " + direction);
		_direction = direction;
		_isTouch = true;
	}

	private void death()
	{
	}

}
