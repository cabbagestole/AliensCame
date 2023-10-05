using Godot;
using System;

public partial class Operation : NotifiableCanvasLayer
{
	public InputSystem InputSystem { get; set; }

	public override void _Ready()
	{
		InputSystem.AddObserver(buttonCheck);
	}

	private void buttonCheck(GUBButton button, GUBButtonState state, int durationCount)
	{
		if((button == GUBButton.ButtonSouth) && (state == GUBButtonState.Press))
		{
			notifyObservers(GameScene.Operation, GameScene.Title);
			InputSystem.RemoveObserver(buttonCheck);
		}
	}

}
