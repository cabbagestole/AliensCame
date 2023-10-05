using Godot;
using System;

public partial class InGame : NotifiableCanvasLayer
{
	[Export] private PackedScene Ship { get; set; }
	[Export] private PackedScene EnemyOrigin { get; set; }

	public InputSystem InputSystem { get; set; }

	private GameProperties _GP = GameProperties.Inst();

	private Ship _ship;
	private EnemyOrigin _enemyorigin;
		
	public override void _Ready()
	{
		_GP.IncreaseWave();

		_ship = (Ship)Ship.Instantiate();
		_ship.InputSystem = InputSystem;
		_ship.AddObserver(loseAllShip);
		AddChild(_ship);

		_enemyorigin = (EnemyOrigin)EnemyOrigin.Instantiate();
		_enemyorigin.AddObserver(destroyEnemyAll);
		AddChild(_enemyorigin);
		GetNode<AudioStreamRepeatPlayer>("AudioStreamRepeatPlayer").Play();
	}


	public override void _Process(double delta)
	{
		GetNode<Label>("Wave").Text = _GP.Wave.ToString();
		GetNode<Label>("Score").Text = _GP.Score.ToString();
		GetNode<Label>("HiScore").Text = _GP.HiScore.ToString();
		GetNode<Label>("ShipRest").Text = _GP.ShipRest.ToString();
	}

	public void destroyEnemyAll()
	{
		_ship.stopMoving();
		_enemyorigin.stopMoving();
		Timer timer = GetNode<Timer>("WaveClearTimer");
		timer.Start(2);
	}

	public void loseAllShip()
	{
		invade();
	}

	public void invade()
	{
		_ship.stopMoving();
		_enemyorigin.stopMoving();
		
		Timer timer = GetNode<Timer>("GameOverTimer");
		timer.Start(2);
		
	}


	private void OnWaveClearTimerTimeout()
	{
		GetNode<AudioStreamRepeatPlayer>("AudioStreamRepeatPlayer").Stop();
		notifyObservers(GameScene.InGame, GameScene.InGame);
	}

	private void OnGameOverTimerTimeout()
	{
		GetNode<AudioStreamRepeatPlayer>("AudioStreamRepeatPlayer").Stop();
		notifyObservers(GameScene.InGame, GameScene.GameOver);
	}
	
}







