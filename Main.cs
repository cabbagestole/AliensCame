using Godot;
using System;


public partial class Main : Node2D
{
	[Export] private PackedScene InputSystem { get; set; }	
	
	public override void _Ready()
	{
		InputSystem inputSystem = (InputSystem)InputSystem.Instantiate();
		AddChild(inputSystem);
	}
	
}
