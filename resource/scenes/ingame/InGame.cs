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
		_GP.IncreaseWave();

		Ship ship = (Ship)Ship.Instantiate();
		ship.InputSystem = InputSystem;
		ship.AddObserver(loseAllShip);
		AddChild(ship);

		EnemyOrigin enemyorigin = (EnemyOrigin)EnemyOrigin.Instantiate();
		enemyorigin.AddObserver(destroyEnemyAll);
		AddChild(enemyorigin);
		
	}


	public override void _Process(double delta)
	{
		GetNode<Label>("Wave").Text = _GP.Wave.ToString();
		GetNode<Label>("Score").Text = _GP.Score.ToString();
		GetNode<Label>("HiScore").Text = _GP.HiScore.ToString();
		GetNode<Label>("ShipRest").Text = _GP.ShipRest.ToString();
	}

	public void loseAllShip()
	{
		notifyObservers(GameScene.InGame, GameScene.GameOver);
	}

	public void invade()
	{
		notifyObservers(GameScene.InGame, GameScene.GameOver);
	}

	public void destroyEnemyAll()
	{
		notifyObservers(GameScene.InGame, GameScene.InGame);
	}
	
}


