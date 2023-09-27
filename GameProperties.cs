using Godot;
using System;

public class GameProperties
{
	private int _score = 0;
	private int _hiScore = 0;
	
	public int Score
	{
		get { return _score; }
	}	

	public int HiScore
	{
		get { return _hiScore; }
	}
	
	public void AddScore(int point)
	{
		_score += point;
		if (_score > _hiScore)
			_hiScore = _score;
	}


	
}
