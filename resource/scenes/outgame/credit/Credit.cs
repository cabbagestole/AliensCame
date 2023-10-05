using Godot;
using System;

public partial class Credit : NotifiableCanvasLayer
{
	[Export] private Json Text { get; set; }
	[Export] private PackedScene MovingText { get; set; }

	public InputSystem InputSystem { get; set; }

	public override void _Ready()
	{
		startCredit();
		InputSystem.AddObserver(buttonCheck);
		GetNode<AudioStreamRepeatPlayer>("AudioStreamRepeatPlayer").Play();
	}
	
	private void startCredit()
	{
		Json json = new Json();
		Error error = json.Parse(Json.Stringify(Text.Data));
		if(error == Error.Ok){
			Godot.Collections.Dictionary dic = json.Data.AsGodotDictionary();
			foreach (var (key, value) in dic)
			{
				MovingText movingText = (MovingText)MovingText.Instantiate();
				movingText.Start = key.As<float>();
				movingText.Text = value.As<String>();
				AddChild(movingText);
			}
		}else{
			GD.Print("JSON Parse Error: " + json.GetErrorMessage() + " line = " + json.GetErrorLine());
		}
	}

	private void buttonCheck(GUBButton button, GUBButtonState state, int durationCount)
	{
		if((button == GUBButton.ButtonSouth) && (state == GUBButtonState.Press))
		{
			GetNode<AudioStreamRepeatPlayer>("AudioStreamRepeatPlayer").Stop();
			InputSystem.RemoveObserver(buttonCheck);
			notifyObservers(GameScene.Credit, GameScene.Title);
		}
	}


}


