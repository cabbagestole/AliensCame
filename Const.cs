using Godot;
using System;

// note
// A file to define an enum 
// that is valid in the global scope. 
// Classes defining string constants are not specifically used.
// 
public static class Const
{
	public const String HelloWorld = "Hello World";
}



public enum GameScene
{
	Main = 0,
	Title = 1,
	Operation = 2,
	InGame = 3,
	GameOver = 4,
	Credit = 5,
}

public enum Direction{
		East = 0,
		West = 1,
	}
	
