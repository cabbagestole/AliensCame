using Godot;
using System;

public partial class EnemyBase : NotifiableEnemy
{
	[Export] private PackedScene Explosion { get; set; }
	private Vector2 _screenSize;
	private RayCast2D _rayCast;
	
	public override void _Ready()
	{
		_screenSize = GetViewportRect().Size;
		_rayCast = GetNode<RayCast2D>("RayCast2D");
	}

	public override void _Process(double delta)
	{
		float posx = GetParent<Node2D>().Position.X + Position.X;
		if(posx > _screenSize.X * (float)0.95)
			notifyTouchObservers(Direction.West);
		if(posx < _screenSize.X * (float)0.05)
			notifyTouchObservers(Direction.East);
		
		if(_rayCast.IsColliding()){
		//	GD.Print(" rat hit = " + Name);
		}
	}

	private void OnAreaEntered(Area2D area)
	{
		GD.Print(" EnemyBase OnAreaEntered = " + Name);
		if(area.Name == "Ship")
		{
			GD.Print(" EnemyBase touch = " + area.Name);
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


