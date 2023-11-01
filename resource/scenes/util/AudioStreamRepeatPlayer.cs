using Godot;
using System;

// note
// AudioStreamPlayer that repeats automatically.
// 
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


