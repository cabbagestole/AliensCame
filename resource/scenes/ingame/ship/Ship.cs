using Godot;
using System;

public partial class Ship : NotifiableArea2D
{
	public InputSystem InputSystem { get; set; }
	[Export] private float Speed = 200;
	[Export] private PackedScene Bullet { get; set; }
	[Export] private PackedScene ChargeBullet { get; set; }
	
	private Vector2 _move = Vector2.Zero;
	private Vector2 _screenSize;
	private Sprite2D _charge;

	private AudioStreamPlayer _bulletSE;
	private AudioStreamPlayer _chargeBulletSE;
	private AudioStreamPlayer _shipExplosionSE;
	private AnimatedSprite2D _animatedSprite2D;

	private GameProperties _GP = GameProperties.Instance();
	
	public override void _Ready()
	{
		InputSystem.AddObserver(fire);
		InputSystem.AddObserver(moveShip);
		_screenSize = GetViewportRect().Size;
		_charge = (Sprite2D)FindChildren("Charge","Sprite2D")[0];
		_bulletSE = GetNode<AudioStreamPlayer>("BulletSE");
		_chargeBulletSE = GetNode<AudioStreamPlayer>("ChargeBulletSE");
		_shipExplosionSE = GetNode<AudioStreamPlayer>("ShipExplosionSE");
				
		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_animatedSprite2D.Animation = "default";
				
		Position = new Vector2(_screenSize.X /2, _screenSize.Y *(float)0.9);
	}
	
	public override void _Process(double delta)
	{
		Position += _move * (float)delta * Speed;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, _screenSize.X * (float)0.05, _screenSize.X * (float)0.95),
			y: Mathf.Clamp(Position.Y, _screenSize.Y * (float)0.05, _screenSize.Y * (float)0.95)
		);
	}

	private void moveShip(Vector2 vec)
	{
		_move = vec;
	}

	private float _chargeCount = 0;

	private int _bulletCount = 0;
	
	private void fire(GUBButton button, GUBButtonState state, int durationCount)
	{
		if((button == GUBButton.ButtonSouth) && (state == GUBButtonState.Press))
		{
			if(2 > _bulletCount){
				Bullet bullet = (Bullet)Bullet.Instantiate();
				bullet.SetPosition(Position);
				bullet.AddObserver(bulletDestroyed);
				//bulletをthis(ship)の子ではなくshipの親(InGame)の子にする事で
				// bulletの座標系はあったく移動しないInGmaeの座標系と等しくなります。
				GetParent().AddChild(bullet);
				_bulletCount++;
				_bulletSE.Play();
			}
		}
		if((button == GUBButton.ButtonSouth) && (state == GUBButtonState.HoldOn))
		{
			_chargeCount += (float)0.02;
			_charge.Material.Set("shader_parameter/fill", _chargeCount);
		}
		if((button == GUBButton.ButtonSouth) && (state == GUBButtonState.Release))
		{
			if(_chargeCount >= 1){
				
				ChargeBullet chargeBullet = (ChargeBullet)ChargeBullet.Instantiate();
				chargeBullet.SetPosition(Position);
				GetParent().AddChild(chargeBullet);
				_chargeBulletSE.Play();
			}
			_chargeCount = 0;
			_charge.Material.Set("shader_parameter/fill", _chargeCount);
		}
	}

	private void bulletDestroyed()
	{
		_bulletCount--;
	}

	private void OnAreaEntered(Area2D area)
	{
		EnemyBase enemy = area as EnemyBase;
		if(null != enemy) 
			destroy();
		EnemyBullet eBullet = area as EnemyBullet;
		if(null != eBullet) 
			destroy();
	}


	private void destroy()
	{
		InputSystem.RemoveObserver(fire);
		_shipExplosionSE.Play();
		_animatedSprite2D.Visible = true;
		_animatedSprite2D.Play();
	}
	
	private void OnAnimatedSprite2dAnimationFinished()
	{
		_animatedSprite2D.Visible = false;
		InputSystem.AddObserver(fire);
		if(_GP.ShipRest == 0)
			gameOver();
		_GP.DecreaseShip();
	}
	
	private void gameOver()
	{
		InputSystem.RemoveObserver(fire);
		InputSystem.RemoveObserver(moveShip);
		_move = Vector2.Zero;
		notifyObservers();
	}
	
	
}




