using Godot;
using System;

public partial class MartianOctopus : EnemyBase
{

	public override void _Ready()
	{
		base._Ready();
		AnimatedSprite2D animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animatedSprite2D.Animation = "move";
		animatedSprite2D.Play();
	}

	protected override int Point()
	{
		return 30;
	}

}
