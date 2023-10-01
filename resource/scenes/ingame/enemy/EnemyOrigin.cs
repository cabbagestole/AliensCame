using Godot;
using System;

public partial class EnemyOrigin : Node2D
{

	[Export] private PackedScene MartianOctopus { get; set; }
	[Export] private PackedScene Gray { get; set; }
	[Export] private PackedScene FlatwoodsMonster { get; set; }
	[Export] public float Speed = 1;
	
	private bool _isTouch = false;
	private Direction _direction = Direction.East;
	
	public override void _Ready()
	{
		Position = (GetViewportRect().Size * (float)0.1);
		for(int y = 0; y < 5 ; y++){
			for(int x = 0; x < 8 ; x++){
				if(y == 0) 
					generate(MartianOctopus, x, y);
				if((y == 1)||(y ==2)) 
					generate(Gray, x, y);
				if((y == 3)||(y ==4)) 
					generate(FlatwoodsMonster, x, y);
			}
		}
	}

	
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

	private void generate(PackedScene enemyScene, float x, float y)
	{
		generateImpl(enemyScene, x * 48, y * 48);
	}

	private void generateImpl(PackedScene enemyScene, float x, float y)
	{
		EnemyBase enemy = (EnemyBase)enemyScene.Instantiate();
		enemy.AddTouchObserver(touch);
		enemy.AddDeathObserver(death);
		enemy.Position = new Vector2(x, y);
		AddChild(enemy);
		_enemyCount++;
	}

	private int _enemyCount = 0;
	
	private void touch(Direction direction)
	{
		_direction = direction;
		_isTouch = true;
	}


	private void death()
	{
		_enemyCount--;
		if(0 >= _enemyCount)
			GD.Print("destory them all");
	}

}
