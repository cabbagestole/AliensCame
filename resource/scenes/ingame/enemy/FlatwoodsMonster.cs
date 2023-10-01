using Godot;
using System;

public partial class FlatwoodsMonster : EnemyBase
{
	private AnimatedSprite2D _animatedSprite2D;

	public override void _Ready()
	{
		base._Ready();
		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_animatedSprite2D.Animation = "move";
		_animatedSprite2D.Play();
	}

}
