using Godot;
using System;

public partial class Title : NotifiableCanvasLayer
{
	 public InputSystem InputSystem { get; set; }

	private int _select = 0;
	private Sprite2D _startLabel;
	private Sprite2D _operationLabel;
	private AudioStreamPlayer _click;
			
	public override void _Ready()
	{
		InputSystem.AddObserver(buttonCheck);
		InputSystem.AddObserver(moveCursor, true);
		_startLabel = (Sprite2D)FindChildren("Selector01","Sprite2D")[0];
		_operationLabel = (Sprite2D)FindChildren("Selector02","Sprite2D")[0];
		_startLabel.Visible = true;
		_operationLabel.Visible = false;
		_click = GetNode<AudioStreamPlayer>("Click");
		
	}
	

	private void moveCursor(Vector2 vec)
	{
		_select = regurate(_select, vec.Y, 2);
		_startLabel.Visible = (0 == _select)? true: false;
		_operationLabel.Visible = (1 == _select)? true: false;
		_click.Play();
	}

	private int regurate(int current, float add, int max)
	{
		return regurate(current, (int)(add / Mathf.Abs(add)) , max);
	}
	
	private int regurate(int current, int add, int max)
	{
		int ret = current += Mathf.Sign(add);
		ret %= max;
		return (ret < 0)? max -1: ret;
	}

	
	private void buttonCheck(GUBButton button, GUBButtonState state, int durationCount)
	{
		if((button == GUBButton.ButtonSouth) && (state == GUBButtonState.Press))
		{
			if(0 == _select){
				notifyObservers(GameScene.Title, GameScene.InGame);
			}else{
				notifyObservers(GameScene.Title, GameScene.Control);
			}
			InputSystem.RemoveObserver(buttonCheck);
			InputSystem.RemoveObserver(moveCursor);
		}
	}
	

}
