using Godot;
using System;

public partial class EnemyBase : NotifiableEnemy
{
	[Export] private PackedScene Explosion { get; set; }
	[Export] private PackedScene EnemyBullet { get; set; }
	private Vector2 _screenSize;
	private RayCast2D _rayCast;
	
	private GameProperties _GP = GameProperties.Instance();
	
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
	}

	private int _hp = 1;
	public void Damage(int damage)
	{
		_hp -= damage;
		if(_hp <= 0) {
			_GP.AddScore(Point());
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


	public void Fire()
	{
		// 32×32 ドットの敵キャラを48ドット間隔で配置
		// つまり48-(32/2)=32ドット長のraycastには反応しません。
		// 前列の空間が空いている敵キャラが弾を撃ちます。
		if(_rayCast.IsColliding()) return;
		fireImpl();
	}

	private void fireImpl()
	{
		EnemyBullet enemyBullet = (EnemyBullet)EnemyBullet.Instantiate();
		EnemyOrigin parent = GetParent() as EnemyOrigin;
		enemyBullet.Position = parent.Position + Position;
		GetParent().GetParent().AddChild(enemyBullet);
	}

	protected virtual int Point()
	{
		return 0;
	}


}


