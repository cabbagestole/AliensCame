using Godot;
using System;

//
// 色々テストをする為のスタブ
// 
//
public partial class Stub : Node2D
{
	
	public void setNotifyer(InputSystem input)
	{
		input.AddObserver(callbackA);
		input.AddObserver(callbackB);
		input.AddObserver(callbackD);
	}
	

	public void callbackA(GUBButton button, GUBButtonState state, int durationCount)
	{
		GD.Print("player 1 button = " + button + " state = " + state + " durationCount = " + durationCount);
	}

	public void callbackB(GUBButton button, GUBButtonState state, int durationCount)
	{
		GD.Print("player 2 button = " + button + " state = " + state + " durationCount = " + durationCount);
	}

	public void callbackD(Vector2 vec)
	{
		GD.Print("callbackD vec = " + vec);
	}
	
}