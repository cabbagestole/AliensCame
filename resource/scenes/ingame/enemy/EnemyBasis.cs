using Godot;
using System;

public partial class EnemyBasis : NotifiableEnemy
{

	[Export] private PackedScene Explosion { get; set; }
	
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
		if(posx > _screenSize.X * (float)0.95)
			notifyTouchObservers(Direction.West);
		if(posx < _screenSize.X * (float)0.05)
			notifyTouchObservers(Direction.East);
	}


	private void OnAreaEntered(Area2D area)
	{
		if(area.Name == "Ship")
		{
			GD.Print(" EnemyBasis touch = " + area.Name);
		}
	}

	private int _hp = 1;
	public void Damage(int damage)
	{
		_hp -= damage;
		if(_hp <= 0) {
			explode();
			notifyDeathObservers();
			QueueFree();
		}
	}
	
	private void explode()
	{
		Explosion explosion = (Explosion)Explosion.Instantiate();
		explosion.Position = Position;
		GetParent().AddChild(explosion);
	}
}


