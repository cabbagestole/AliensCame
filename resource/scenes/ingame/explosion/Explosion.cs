using Godot;
using System;

public partial class Explosion : Node2D
{
	
	public override void _Ready()
	{
		AnimatedSprite2D animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animatedSprite2D.Animation = "default";
		animatedSprite2D.Play();
	}

	private void OnAnimatedSprite2dAnimationFinished()
	{
			QueueFree();
	}


	
}


