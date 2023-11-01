using Godot;
using System;

// note
// Data class to record scores and high scores.
// スコアやハイスコアを記録するデータクラス。
// 
public class GameProperties
{
	private int _score = 0;
	private int _hiScore = 765;
	private int _shipRest = 0;
	private int _wave = 0;

	private static GameProperties _self = null;

	// note
	// Make the constructor private to prohibit direct new. 
	// Singleton's standard.
	// コンストラクタをprivateにして直接newを禁止する。シングルトンの定石。
	//
	private GameProperties()
	{
		// for singleton
	}


	public static GameProperties Inst()
	{
		return _self ??= new GameProperties();
	}

	public void Setup()
	{
		_score = 0;
		_shipRest = 3;
		_wave = 0;
	}

	public int Score
	{
		get { return _score; }
	}	

	public int HiScore
	{
		get { return _hiScore; }
	}
	
	public int ShipRest
	{
		get { return _shipRest; }
	}
	
	public int Wave
	{
		get { return _wave; }
	}

	public void AddScore(int point)
	{
		_score += point;
		if (_score > _hiScore)
			_hiScore = _score;

	}

	public void DecreaseShip()
	{
		_shipRest -= 1;
	}

	public void IncreaseWave()
	{
		_wave += 1;
	}
	
}
