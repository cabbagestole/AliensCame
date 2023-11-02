using Godot;
using System;

// note
// Stubs for various tests. Not used.
// 色々テストをする為のスタブ。使用していません。
//
public partial class Stub : Node2D
{
	
	public void setNotifyer(InputSystem input)
	{
//		input.AddObserver(callbackA);
//		input.AddObserver(callbackB);
//		input.AddObserver(callbackD);
		input.AddObserver(callbackE, true);
	}
	

	private void callbackA(GUBButton button, GUBButtonState state, int durationCount)
	{
		GD.Print("player 1 button = " + button + " state = " + state + " durationCount = " + durationCount);
	}

	private void callbackB(GUBButton button, GUBButtonState state, int durationCount)
	{
		GD.Print("player 2 button = " + button + " state = " + state + " durationCount = " + durationCount);
	}

	private void callbackD(Vector2 vec)
	{
		GD.Print("callbackD vec = " + vec);
	}

	private void callbackE(Vector2 vec)
	{
		GD.Print("once callbackE vec = " + vec);
	}

	
}
