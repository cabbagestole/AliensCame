using Godot;
using System;

public partial class AudioStreamRepeatPlayer : AudioStreamPlayer
{
	public override void _Ready()
	{
		Play();
	}

	private void OnFinished()
	{
		Play();
	}

}


