using Godot;
using System;

public partial class Credit : NotifiableCanvasLayer
{
	[Export] private Json Text { get; set; }
	[Export] private PackedScene MovingText { get; set; }

	public InputSystem InputSystem { get; set; }

	private AudioStreamPlayer _creditBGM;
		
	public override void _Ready()
	{
		startCredit();
		InputSystem.AddObserver(buttonCheck);
		_creditBGM = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
	//	_creditBGM.Play();
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
			InputSystem.RemoveObserver(buttonCheck);
			_creditBGM.Stop();
			notifyObservers(GameScene.Credit, GameScene.Title);
		}
	}

	private void OnAudioStreamPlayerFinished()
	{
	//	_creditBGM.Play();
	}

}


