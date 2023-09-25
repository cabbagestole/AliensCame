using Godot;
using System;

// Handmade device input handler
// Not necessary to configure [project]-[project setting]-[input map].
// The inspection target is hard-coded within the class.
//	
//
public partial class InputSystem : Node
{

	[Export] public double LowerLimit = 0.1;

	public override void _Ready()
	{
	}

/*
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventKey eventKey)
			GD.Print("UnHandled = " + eventKey.AsText());
	}
*/

	// Inspects events and assigns them to corresponding devices
	//
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent)
			keyboadInput(keyEvent);
		if (@event is InputEventJoypadButton joyPadButton)
			joyPadButtonInput(joyPadButton);
		if (@event is InputEventJoypadMotion joyPadStick)
			joyPadStickInput(joyPadStick);
	}

	// Assign keyboard presses or releases to corresponding methods.
	//	
	private void keyboadInput(InputEventKey keyEvent)
	{
		if(keyEvent.Pressed && !keyEvent.Echo)
			pressKey(keyEvent.Keycode);
		if(!keyEvent.Pressed && !keyEvent.Echo)
			releaseKey(keyEvent.Keycode);
/*			
		if(keyEvent.Pressed && keyEvent.Echo)
			// pass. ignore it in this game.  
		if(!keyEvent.Pressed && keyEvent.Echo)
			// it's never occur
*/
	}
	
	// Assign joypad presses or releases to corresponding methods.
	//
	private void joyPadButtonInput(InputEventJoypadButton joyPadButton)
	{
		if(joyPadButton.Pressed)
			pressButton(joyPadButton.ButtonIndex);
		if(!joyPadButton.Pressed)
			releaseButton(joyPadButton.ButtonIndex);
	}


	private bool isPressA = false;
	private int pressCountA = 0;

	
	// Assign a process to each key pressed.
	// Hard-coded is here.
	//
	private void pressKey(Key keycode)
	{
		if(keycode == Key.Z)
		{
			isPressA = true;
			 GD.Print("press Z ");
		}
		if(keycode == Key.X) GD.Print("press X ");
		if(keycode == Key.C) GD.Print("press C ");
		if(keycode == Key.V) GD.Print("press V ");
	}


	// Assign a process to each key release.
	// Hard-coded is here.
	// 
	private void releaseKey(Key keycode)
	{
		if(keycode == Key.Z)
		{
			isPressA = false;
			 GD.Print("release Z ");
		}
		if(keycode == Key.X) GD.Print("release X ");
		if(keycode == Key.C) GD.Print("release C ");
		if(keycode == Key.V) GD.Print("release V ");
	}


	private void pressButton(JoyButton button)
	{
		if(button == JoyButton.A) 
		{
			isPressA = true;
			GD.Print("press Sony Cross, Xbox A, Nintendo B. ");
		}
		if(button == JoyButton.B) GD.Print("press Sony Circle, Xbox B, Nintendo A.");
		if(button == JoyButton.X) GD.Print("press Sony Square, Xbox X, Nintendo Y.");
		if(button == JoyButton.Y) GD.Print("press Sony Triangle, Xbox Y, Nintendo X.");
	}

	private void releaseButton(JoyButton button)
	{
		if(button == JoyButton.A) 
		{
			isPressA = false;
			GD.Print("release Sony Cross, Xbox A, Nintendo B. ");
		}

		if(button == JoyButton.B) GD.Print("release Sony Circle, Xbox B, Nintendo A.");
		if(button == JoyButton.X) GD.Print("release Sony Square, Xbox X, Nintendo Y.");
		if(button == JoyButton.Y) GD.Print("release Sony Triangle, Xbox Y, Nintendo X.");
	}



	
	private void joyPadStickInput(InputEventJoypadMotion joyPadStick)
	{
		if(Mathf.Abs(joyPadStick.AxisValue) >= LowerLimit ){
			if((joyPadStick.Axis == JoyAxis.LeftX) || (joyPadStick.Axis == JoyAxis.LeftY))
				GD.Print("enter joyPadStick (L) " + joyPadStick.AsText());
			if((joyPadStick.Axis == JoyAxis.RightX) || (joyPadStick.Axis == JoyAxis.RightY))
				GD.Print("enter joyPadStick (R) " + joyPadStick.AsText());
		}
	}
	
	
	public override void _PhysicsProcess(double delta)
	{
		if(isPressA){
			pressCountA++;
			GD.Print("continous press count = " + pressCountA);
		}else{
			pressCountA = 0;
		}
	}


}
