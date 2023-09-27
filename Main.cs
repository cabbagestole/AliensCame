using Godot;
using System;


public partial class Main : Node2D
{
	[Export] private PackedScene InputSystem { get; set; }
	[Export] private PackedScene Title { get; set; }
	[Export] private PackedScene Control { get; set; }
	[Export] private PackedScene InGame { get; set; }
	[Export] private PackedScene Stub { get; set; }


	private InputSystem _inputSystem;
	private Title _title;
	private Control _control;
	private InGame _inGame;
	
	private GameProperties _gameProperties = new GameProperties();
	
	public override void _Ready()
	{
		GD.Print("_Ready");
		_inputSystem = (InputSystem)InputSystem.Instantiate();
		AddChild(_inputSystem);
		sceneChange(GameScene.Main, GameScene.Title);

/*
		GD.Print("hogehoge");
		Stub stub = (Stub)Stub.Instantiate();
		stub.setNotifyer(inputSystem);
		AddChild(stub);
*/
		

	}
	
	private void sceneChange(GameScene currnt, GameScene next)
	{

		GD.Print("currnt = " + currnt + " next = " + next);

		if(GameScene.Title == currnt){
			RemoveChild(_title);
			_title = null;
		}
		if(GameScene.Title == next){
			_title = (Title)Title.Instantiate();
			_title.InputSystem = _inputSystem;
			_title.AddObserver(sceneChange);
			AddChild(_title);
		}
			
			
			
		if(GameScene.Control == currnt){
			RemoveChild(_control);
			_control = null;
		}
		if(GameScene.Control == next){
			_control = (Control)Control.Instantiate();
			_control.InputSystem = _inputSystem;
			_control.AddObserver(sceneChange);
			AddChild(_control);
			
		}
		
		
		if(GameScene.InGame == currnt){
			RemoveChild(_inGame);
			_inGame = null;
		}
		
		
		if(GameScene.InGame == next){
			_inGame = (InGame)InGame.Instantiate();
			_inGame.InputSystem = _inputSystem;
			_inGame.AddObserver(sceneChange);
			AddChild(_inGame);

		}
		PrintTreePretty();
	}
	
}
