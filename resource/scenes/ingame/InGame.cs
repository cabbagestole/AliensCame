using Godot;
using System;

public partial class InGame : NotifiableCanvasLayer
{
	[Export] private PackedScene Ship { get; set; }
	public InputSystem InputSystem { get; set; }
	
	public override void _Ready()
	{
			Ship ship = (Ship)Ship.Instantiate();
			ship.InputSystem = InputSystem;
			AddChild(ship);
	}


}
