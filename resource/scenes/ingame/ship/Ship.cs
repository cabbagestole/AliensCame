using Godot;
using System;

public partial class Ship : Area2D
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

	public override void _Ready()
	{
		InputSystem.AddObserver(fire);
		InputSystem.AddObserver(moveShip);
		_screenSize = GetViewportRect().Size;
		_charge = (Sprite2D)FindChildren("Charge","Sprite2D")[0];
		_bulletSE = GetNode<AudioStreamPlayer>("BulletSE");
		_chargeBulletSE = GetNode<AudioStreamPlayer>("ChargeBulletSE");
		Position = new Vector2(_screenSize.X /2, _screenSize.Y *(float)0.9);
	}
	
	public override void _Process(double delta)
	{
		Position += _move * (float)delta * Speed;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, _screenSize.X * (float)0.1, _screenSize.X * (float)0.9),
			y: Mathf.Clamp(Position.Y, _screenSize.Y * (float)0.1, _screenSize.Y * (float)0.9)
		);
	}

	private void moveShip(Vector2 vec)
	{
		_move = vec;
	}

	private float chargeCount = 0;

	private int _bulletCount = 0;
	
	private void fire(GUBButton button, GUBButtonState state, int durationCount)
	{
		if((button == GUBButton.ButtonSouth) && (state == GUBButtonState.Press))
		{
			if(2 > _bulletCount){
				Bullet bullet = (Bullet)Bullet.Instantiate();
				bullet.SetPosition(Position);
				bullet.AddObserver(bulletDestroyed);
				//bulletをthis(ship)の子ではなくshipの親(InGame)の子する事で
				// bulletの座標系が移動していないInGmaeの座標系と等しくなる
				// shipの子にするとInGameから見てShipPos *2 の座標にbulletが配置される為、困る
				GetParent().AddChild(bullet);
				_bulletCount++;
				_bulletSE.Play();
			}
		}
		if((button == GUBButton.ButtonSouth) && (state == GUBButtonState.HoldOn))
		{
			chargeCount += (float)0.02;
			_charge.Material.Set("shader_parameter/fill", chargeCount);
		}
		if((button == GUBButton.ButtonSouth) && (state == GUBButtonState.Release))
		{
			if(chargeCount >= 1){
				
				ChargeBullet chargeBullet = (ChargeBullet)ChargeBullet.Instantiate();
				chargeBullet.SetPosition(Position);
				GetParent().AddChild(chargeBullet);
				_chargeBulletSE.Play();
			}
			chargeCount = 0;
			_charge.Material.Set("shader_parameter/fill", chargeCount);
		}
	}

	private void bulletDestroyed()
	{
		_bulletCount--;
	}

	private void gameOver()
	{
		InputSystem.RemoveObserver(fire);
		InputSystem.RemoveObserver(moveShip);
//		notifyObservers(GameScene.InGame, GameScene.Title);
	}
	
	
}
