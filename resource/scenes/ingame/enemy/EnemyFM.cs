using Godot;
using System;

public partial class EnemyFM : NotifiableEnemy
{

	private Vector2 _screenSize;

	private AnimatedSprite2D _animatedSprite2D;
	
	public override void _Ready()
	{
		_screenSize = GetViewportRect().Size;

		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_animatedSprite2D.Animation = "move";
		_animatedSprite2D.Play();
		
	}

	public override void _Process(double delta)
	{
		float posx = GetParent<Node2D>().Position.X + Position.X;
		if(posx > _screenSize.X * (float)0.9)
			notifyTouchObservers(Direction.West);
		if(posx < _screenSize.X * (float)0.1)
			notifyTouchObservers(Direction.East);
	}


	private void OnAreaEntered(Area2D area)
	{
		GD.Print("OnAreaEntered EnemyFM");
	}
	
	
}


