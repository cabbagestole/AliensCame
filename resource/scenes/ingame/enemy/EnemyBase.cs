using Godot;
using System;

public partial class EnemyBase : NotifiableEnemy
{
	[Export] private PackedScene Explosion { get; set; }
	[Export] private PackedScene EnemyBullet { get; set; }
	private Vector2 _screenSize;
	private RayCast2D _rayCast;
	private AudioStreamPlayer _enemyBulletSE;
		
	private GameProperties _GP = GameProperties.Inst();
	
	public override void _Ready()
	{
		_screenSize = GetViewportRect().Size;
		_enemyBulletSE = GetNode<AudioStreamPlayer>("EnemyBulletSE");
		_rayCast = GetNode<RayCast2D>("RayCast2D");
	}

	public override void _Process(double delta)
	{
		Vector2 pos = GetParent<Node2D>().Position + Position;
		if(pos.X > _screenSize.X * (float)0.95)
			notifyTouchObservers(Direction.West);
		if(pos.X < _screenSize.X * (float)0.05)
			notifyTouchObservers(Direction.East);
		if(pos.Y > _screenSize.Y * (float)0.95)
			notifyInvadeObservers();
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
		_enemyBulletSE.Play();
		EnemyBullet enemyBullet = (EnemyBullet)EnemyBullet.Instantiate();
		EnemyOrigin parent = GetParent() as EnemyOrigin;
		enemyBullet.Position = parent.Position + Position;
		GetParent().GetParent().AddChild(enemyBullet);
	}

	protected virtual int Point()
	{
		return 0;//派生先で点数をオーバーライドしてください
	}


}


