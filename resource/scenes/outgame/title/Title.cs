using Godot;
using System;

public partial class Title : NotifiableCanvasLayer
{
	public InputSystem InputSystem { get; set; }

	private int _select = 0;
	private Sprite2D _startSprite;
	private Sprite2D _operationSprite;
	private Sprite2D _creditSprite;
	private AudioStreamPlayer _click;
	private AudioStreamPlayer _titleBGM;

	private GameProperties _GP = GameProperties.Inst();

	public override void _Ready()
	{
		InputSystem.AddObserver(buttonCheck);
		InputSystem.AddObserver(moveCursor, true);
		_startSprite = (Sprite2D)FindChildren("Selector01","Sprite2D")[0];
		_operationSprite = (Sprite2D)FindChildren("Selector02","Sprite2D")[0];
		_creditSprite = (Sprite2D)FindChildren("Selector03","Sprite2D")[0];
		_startSprite.Visible = true;
		_operationSprite.Visible = false;
		_creditSprite.Visible = false;
		_click = GetNode<AudioStreamPlayer>("Click");
		GetNode<AudioStreamRepeatPlayer>("AudioStreamRepeatPlayer").Play();
		_GP.Setup();
	}
	

	private void moveCursor(Vector2 vec)
	{
		_select = regurate(_select, vec.Y, 3);
		_startSprite.Visible = (0 == _select)? true: false;
		_operationSprite.Visible = (1 == _select)? true: false;
		_creditSprite.Visible = (2 == _select)? true: false;
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
			switch(_select){
				case 0:
					notifyObservers(GameScene.Title, GameScene.InGame);
					break;
				case 1:
					notifyObservers(GameScene.Title, GameScene.Operation);
					break;
				case 2:
					notifyObservers(GameScene.Title, GameScene.Credit);
					break;
			}
			GetNode<AudioStreamRepeatPlayer>("AudioStreamRepeatPlayer").Stop();
			InputSystem.RemoveObserver(buttonCheck);
			InputSystem.RemoveObserver(moveCursor);
		}
	}
	

}


