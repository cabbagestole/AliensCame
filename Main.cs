using Godot;
using System;


public partial class Main : Node2D
{
	[Export] private PackedScene InputSystem { get; set; }
	[Export] private PackedScene Stub { get; set; }
	
	private GameProperties _gameProperties = new GameProperties();
	
	public override void _Ready()
	{
		InputSystem inputSystem = (InputSystem)InputSystem.Instantiate();
		AddChild(inputSystem);

		Stub stub = (Stub)Stub.Instantiate();
		stub.setNotifyer(inputSystem);
		AddChild(stub);

	}
	
}
