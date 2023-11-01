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
	
	// note
	// I felt that the cost of searching the member nodes of a scene 
	// by string each time was too high, 
	// so I cached them in private member variables in advance,
	//  as in Unity.
	// シーンのメンバーノードを毎回文字列でサーチする
	// コストが高いと感じたので、Unity のように
	// 予めプライベートメンバ変数にキャッシュしています。
	// 
	public override void _Ready()
	{
		_screenSize = GetViewportRect().Size;
		_enemyBulletSE = GetNode<AudioStreamPlayer>("EnemyBulletSE");
		_rayCast = GetNode<RayCast2D>("RayCast2D");
	}

	// note
	// The alien notifies the observer that 
	// it has reached the left or right side of the screen.
	// The alien itself does not move, only notifies the observer. 
	// The parent node is responsible for the movement.
	// エイリアンは画面の左右に到達した事を、オブザーバーに通知します。
	// 通知するだけでエイリアン自身は移動を行いません。親ノードが移動を担当します。
	//
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
	// note
	// This method is called from Bullet.
	// このメソッドはBulletから呼び出されます。
	//
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
	
	// note
	// Explosion is added to the scene tree as a child of its parent,
	//  not of the current node (EnemyBase).
	// This allows the Explosion to continue to exist
	//  in the scene tree even if EnemyBase disappears.
	// 爆発はカレントのノード(EnemyBase)ではなくその親の子としてシーンツリーに追加します。
	// これによってEnamyBaseが消滅しても爆発はシーンツリー上に存在し続ける事が出来ます。
	// 
	private void explode()
	{
		Explosion explosion = (Explosion)Explosion.Instantiate();
		explosion.Position = Position;
		GetParent().AddChild(explosion);
	}

	public void Fire()
	{
		// note
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

	// note
	// No score is set for the base class. Override the score in the derived class.
	// 基底クラスでは点数を定めません。派生クラスで点数をオーバーライドしてください。
	protected virtual int Point()
	{
		return 0;
	}


}


