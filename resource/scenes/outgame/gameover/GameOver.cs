using Godot;
using System;

public partial class GameOver : NotifiableCanvasLayer
{
	public InputSystem InputSystem { get; set; }


	public override void _Ready()
	{
		GetNode<Timer>("Timer").Start();
	}

	// note
	// After a certain period of time, it prompts the user to press a button.
	// The button input is disabled until the time elapses.
	// 一定時間経過後、ボタンを押す事を促す。
	// 時間経過までボタン入力は無効化している。
	private void OnTimerTimeout()
	{
		GetNode<Label>("Label").Visible = true;
		InputSystem.AddObserver(buttonCheck);
	}

	// note
	// When the button is pressed, 
	// disconnect input and communicate the screen switch to the observer.
	// ボタンが押されたら入力を切断し、画面切り替えをオブザーバーへ伝える。
	// 
	private void buttonCheck(GUBButton button, GUBButtonState state, int durationCount)
	{
		if((button == GUBButton.ButtonSouth) && (state == GUBButtonState.Press))
		{
			InputSystem.RemoveObserver(buttonCheck);
			notifyObservers(GameScene.GameOver, GameScene.Title);
		}
	}


}


