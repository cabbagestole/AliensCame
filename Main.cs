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
	
	private GameProperties _gameProperties = GameProperties.Instance();
	
	public override void _Ready()
	{
		_inputSystem = (InputSystem)InputSystem.Instantiate();
		AddChild(_inputSystem);
		sceneChange(GameScene.Main, GameScene.Title);
	}
	
	private void sceneChange(GameScene currnt, GameScene next)
	{
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
			GetNode<Timer>("InGameFreeTimer").Start();
//			EmitSignal(SignalName.FreeInGame);
		}

		if(GameScene.InGame == next){
			GetNode<Timer>("InGameStartTimer").Start();
//			EmitSignal(SignalName.GenerateInGame);
		}
//		PrintTreePretty();
	}
	

	private void OnFreeInGame()
	{
		RemoveChild(_inGame);
		_inGame = null;
	}

	private void OnGenerateInGame()
	{
		_inGame = (InGame)InGame.Instantiate();
		_inGame.InputSystem = _inputSystem;
		_inGame.AddObserver(sceneChange);
		AddChild(_inGame);
	}

}





