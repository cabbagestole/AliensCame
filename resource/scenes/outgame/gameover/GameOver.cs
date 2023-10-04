using Godot;
using System;

public partial class GameOver : NotifiableCanvasLayer
{
	public InputSystem InputSystem { get; set; }


	public override void _Ready()
	{
		GetNode<Timer>("Timer").Start();
	}

	private void OnTimerTimeout()
	{
		GetNode<Label>("Label").Visible = true;
		InputSystem.AddObserver(buttonCheck);
	}


	private void buttonCheck(GUBButton button, GUBButtonState state, int durationCount)
	{
		if((button == GUBButton.ButtonSouth) && (state == GUBButtonState.Press))
		{
			notifyObservers(GameScene.GameOver, GameScene.Title);
			InputSystem.RemoveObserver(buttonCheck);
		}
	}


}


