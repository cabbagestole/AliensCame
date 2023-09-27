using Godot;
using System;

public static class Const
{
	public const String HelloWorld = "Hello World";
	
}


///////////////////////////
// prefix "GUB" is 
// Godot to Unity-taste Bredge symbols.
//
///////////////////////////

// Enum for InputSystem

// joypad button
public enum GUBButton
{
	ButtonNorth = 0,
	ButtonSouth = 1,
	ButtonEast = 2,
	ButtonWest = 3,
}


// joypad button state
public enum GUBButtonState
{
	Press = 0,
	HoldOn = 1,
	Release = 2,
}

public enum GameScene
{
	Main = 0,
	Title = 1,
	Control = 2,
	InGame = 3,
}
