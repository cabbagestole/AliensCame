using Godot;
using System;

public partial class EnemyOrigin : NotifiableNode2D
{

	[Export] private PackedScene MartianOctopus { get; set; }
	[Export] private PackedScene Gray { get; set; }
	[Export] private PackedScene FlatwoodsMonster { get; set; }
	[Export] public float Speed = 1;
	
	private bool _isTouch = false;
	private Direction _direction = Direction.East;
	private Timer _timer;
	private int _enemyCount = 0;
	
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
		_timer = GetNode<Timer>("Timer");
		_timer.Start(2);

	}

	
	public override void _Process(double delta)
	{
		float x = 0;
		float y = 0;
		float ratio = 1 / Mathf.Sqrt(_enemyCount +1);
		if(_direction == Direction.East) {
			x = Speed * ratio;
		} else {
			x = -Speed * ratio;
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
		enemy.AddInvadeObserver(invade);
		enemy.Position = new Vector2(x, y);
		AddChild(enemy);
		_enemyCount++;
	}

	
	private void touch(Direction direction)
	{
		_direction = direction;
		_isTouch = true;
	}


	private void death()
	{
		_enemyCount--;
		if(0 >= _enemyCount)
			notifyObservers();
	}

	private bool isNotify = false;
	private void invade()
	{
		if(isNotify) return;
		isNotify = true;
		InGame parent = (InGame)GetParent();
		parent.invade();
	}


	private void OnTimerTimeout()
	{
		int count = GetChildCount() -1;//EnemyBaseの子ノードTimerの分を -1し、Enemybaseの数を得る
		if(0 <= count) {
			EnemyBase enemy = GetChild((int)GD.Randi() % count) as EnemyBase;
			enemy?.Fire();
			_timer.Start(1);
		}
	}


}


