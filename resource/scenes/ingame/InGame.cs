using Godot;
using System;

public partial class InGame : NotifiableCanvasLayer
{
	[Export] private PackedScene Ship { get; set; }
	[Export] private PackedScene EnemyOrigin { get; set; }

	public InputSystem InputSystem { get; set; }

	private GameProperties _GP = GameProperties.Instance();
		
	public override void _Ready()
	{
		if(_GP.Wave == 0) 
			_GP.Setup();
		_GP.IncreaseWave();

		Ship ship = (Ship)Ship.Instantiate();
		ship.InputSystem = InputSystem;
		ship.AddObserver(loseAllShip);
		AddChild(ship);

		EnemyOrigin enemyorigin = (EnemyOrigin)EnemyOrigin.Instantiate();
		enemyorigin.AddObserver(destroyEnemyAll);
		AddChild(enemyorigin);
		
		OnTimerTimeout();
		GetNode<Timer>("Timer").Start();
	}


	private void OnTimerTimeout()
	{
		GetNode<Label>("Wave").Text = _GP.Wave.ToString();
		GetNode<Label>("Score").Text = _GP.Score.ToString();
		GetNode<Label>("HiScore").Text = _GP.HiScore.ToString();
		GetNode<Label>("ShipRest").Text = _GP.ShipRest.ToString();
	}

	public void loseAllShip()
	{
		GD.Print("loseAllShip");
		notifyObservers(GameScene.InGame, GameScene.Title);
	}

	public void destroyEnemyAll()
	{
		GD.Print("destroyEnemyAll");
		notifyObservers(GameScene.InGame, GameScene.InGame);
	}
	
}


