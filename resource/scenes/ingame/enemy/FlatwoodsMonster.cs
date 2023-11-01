using Godot;
using System;

public partial class FlatwoodsMonster : EnemyBase
{

	// note
	// Call base._Ready() because _Ready() is overridden twice during the derivation.
	// 派生途中で_Ready()を2回オーバーライドしているため、base._Ready()を呼び出す。
	// 
	public override void _Ready()
	{
		base._Ready();
		AnimatedSprite2D animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animatedSprite2D.Animation = "move";
		animatedSprite2D.Play();
	}

	protected override int Point()
	{
		return 10;
	}


}
