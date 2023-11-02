using Godot;
using System;

// note
// Operation description screen.
// 操作説明画面。
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
