using Godot;
using System;

// note
// Handmade device input handler
// 
// Keyboard and gamepad button wrappers.
// Commonize keyboard and gamepad long presses and communicate them to observers.
// Commonize directional key and left stick input and communicate to observers.
// 
// keyboad [Z],[X],[C],[V] corresponds to joypad[South],[East],[North],[West].
//
// Not necessary to configure [project]-[project setting]-[input map].
// The inspection target is hard-coded within the class.
//
//

///////////////////////////
// prefix "GUB" is 
// Godot to Unity-taste Bredge symbols.
//
// Enum for InputSystem
//
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


public partial class InputSystem : Node
{
	// Threshold for ignoring micro-movements of the stick
	[Export] public double IgnoreMicromotionRatio = 0.1;
	
	// Number of frames to wait after starting to press the button 
	[Export] public int DelayFrameCount = 15;

	// keyboad and joypad-button observer
	// [note]
	// Using Godot's [Signal] is a bit confusing because it requires the "EventHandler" suffix. 
	// So I used C#'s Delagete/Action.
	// 
	private Action<GUBButton, GUBButtonState, int> _buttonObserver = null;
	
	public void AddObserver(Action<GUBButton, GUBButtonState, int> callback)
	{
		_buttonObserver+= callback;
	}
	
	public void RemoveObserver(Action<GUBButton, GUBButtonState, int> callback)
	{
		_buttonObserver-= callback;
	}

	private void notifyObservers(GUBButton button, GUBButtonState state, int durationCount)
	{
		_buttonObserver?.Invoke(button, state, durationCount);
	}



	// keyboad-direction-key and joypad-L-stick observer
	private Action<Vector2> _directionObserver = null;
	private Action<Vector2> _directionOnceObserver = null;
	
	public void AddObserver(Action<Vector2> callback, bool isOnce = false)
	{
		if(isOnce){
			_directionOnceObserver+= callback;
		}else{
			_directionObserver+= callback;
		}
	}
	
	public void RemoveObserver(Action<Vector2> callback)
	{
		_directionObserver-= callback;
		_directionOnceObserver-= callback;
	}

	private void notifyObservers(Vector2 vec)
	{
		_directionObserver?.Invoke(vec);
		if(_isFirstCall)
			_directionOnceObserver?.Invoke(vec);
	}
	


	// Inspects events and assigns them to corresponding devices
	//
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent)
			keyboadInput(keyEvent);
		if (@event is InputEventJoypadButton joyPadButton)
			joyPadButtonInput(joyPadButton);
		if (@event is InputEventJoypadMotion joyPadStick)
			joyPadStickInput(joyPadStick);
	}

	// Assign keyboard press or release to corresponding methods.
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
	
	// Assign joypad press or release to corresponding methods.
	//
	private void joyPadButtonInput(InputEventJoypadButton joyPadButton)
	{
		if(joyPadButton.Pressed)
			pressButton(joyPadButton.ButtonIndex);
		if(!joyPadButton.Pressed)
			releaseButton(joyPadButton.ButtonIndex);
	}

	// South button Hold
	private bool _isPressSouth = false;
	private int _pressCountSouth = 0;

	
	// Assign a process to each key pressed.
	// Hard-coded is here.
	//
	private void pressKey(Key keycode)
	{
		if(keycode == Key.Z)
		{
			_isPressSouth = true;
			notifyObservers(GUBButton.ButtonSouth, GUBButtonState.Press, 0);
		}
		if(keycode == Key.X)
			notifyObservers(GUBButton.ButtonEast, GUBButtonState.Press, 0);
		if(keycode == Key.C)
			notifyObservers(GUBButton.ButtonNorth, GUBButtonState.Press, 0);
		if(keycode == Key.V)
			notifyObservers(GUBButton.ButtonWest, GUBButtonState.Press, 0);
		if(keycode == Key.Left)
			_goWest = true;
		if(keycode == Key.Right)
			_goEast = true;
		if(keycode == Key.Up)
			_goNorth = true;
		if(keycode == Key.Down)
			_goSouth = true;
		
	}


	// Assign a process to each key release.
	// Hard-coded is here.
	// 
	private void releaseKey(Key keycode)
	{
		if(keycode == Key.Z)
		{
			_isPressSouth = false;
			notifyObservers(GUBButton.ButtonSouth, GUBButtonState.Release, 0);
		}
		if(keycode == Key.X)
			notifyObservers(GUBButton.ButtonEast, GUBButtonState.Release, 0);
		if(keycode == Key.C)
			notifyObservers(GUBButton.ButtonNorth, GUBButtonState.Release, 0);
		if(keycode == Key.V)
			notifyObservers(GUBButton.ButtonWest, GUBButtonState.Release, 0);
		if(keycode == Key.Left)
		{
			_goWest = false;
			_x = 0;
		}
		if(keycode == Key.Right)
		{
			_goEast = false;
			_x = 0;
		}
		if(keycode == Key.Up)
		{
			_goNorth = false;
			_y = 0;
		}
		if(keycode == Key.Down)
		{
			_goSouth = false;
			_y = 0;
		}
	}


	private void pressButton(JoyButton button)
	{
		if(button == JoyButton.A) 
		{
			_isPressSouth = true;
			notifyObservers(GUBButton.ButtonSouth, GUBButtonState.Press, 0);
		}
		if(button == JoyButton.B)
			notifyObservers(GUBButton.ButtonEast, GUBButtonState.Press, 0);
		if(button == JoyButton.X)
			notifyObservers(GUBButton.ButtonNorth, GUBButtonState.Press, 0);
		if(button == JoyButton.Y)
			notifyObservers(GUBButton.ButtonWest, GUBButtonState.Press, 0);
	}

	private void releaseButton(JoyButton button)
	{
		if(button == JoyButton.A) 
		{
			_isPressSouth = false;
			notifyObservers(GUBButton.ButtonSouth, GUBButtonState.Release, 0);
		}
		if(button == JoyButton.B)
			notifyObservers(GUBButton.ButtonEast, GUBButtonState.Release, 0);
		if(button == JoyButton.X)
			notifyObservers(GUBButton.ButtonNorth, GUBButtonState.Release, 0);
		if(button == JoyButton.Y)
			notifyObservers(GUBButton.ButtonWest, GUBButtonState.Release, 0);
	}


	// joypad x,y value
	private float _x = 0;
	private float _y = 0;
	
	private void joyPadStickInput(InputEventJoypadMotion joyPadStick)
	{
		if((joyPadStick.Axis == JoyAxis.LeftX)||(joyPadStick.Axis == JoyAxis.LeftY)){
			if(joyPadStick.Axis == JoyAxis.LeftX)
				_x = joyPadStick.AxisValue;
			if(joyPadStick.Axis == JoyAxis.LeftY)
				_y = joyPadStick.AxisValue;
			
			Vector2 vec = new Vector2(_x, _y);
			if(vec.Length() > IgnoreMicromotionRatio ){
				notifyObservers(vec);
				_isFirstCall = false;
			} else {
				notifyObservers(Vector2.Zero);
				_isFirstCall = true;
			}
		}
	}
	
	// keyboad direction-key flag
	// 
	private bool _goNorth = false;
	private bool _goSouth = false;
	private bool _goEast = false;
	private bool _goWest = false;

	private bool _isMove = false;
	private bool _isFirstCall = true;	
	
	public override void _Process(double delta)
	{
		// note
		// Press Button Check
		//
		if(_isPressSouth){
			_pressCountSouth++;
			if(_pressCountSouth >= DelayFrameCount){
			notifyObservers(GUBButton.ButtonSouth, GUBButtonState.HoldOn, _pressCountSouth - DelayFrameCount);
		}
		}else{
			_pressCountSouth = 0;
		}
		// note
		// arrow key  Check
		if(_goNorth || _goSouth || _goEast || _goWest ){
			_isMove = true;
			float x = (_goEast? 1: 0) + (_goWest? -1: 0);
			float y = (_goSouth? 1: 0) + (_goNorth? -1: 0);
			notifyObservers(new Vector2(x, y).LimitLength(1));
			_isFirstCall = false;
		}else{
			// this is service. Send Vector.Zero only once when there is no more input.
			if(_isMove){
				notifyObservers(Vector2.Zero);
				_isMove = false;
				_isFirstCall = true;
			}
		}


	}


}
