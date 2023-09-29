using Godot;
using System;

public partial class Explosion : Node2D
{
	private AnimatedSprite2D _animatedSprite2D;
	
	public override void _Ready()
	{
		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_animatedSprite2D.Animation = "default";
		_animatedSprite2D.Play();
	}

	private void OnAnimatedSprite2dAnimationFinished()
	{
			QueueFree();
	}


	
}


