using Godot;
using System;

public partial class NotifiableCanvasLayer : CanvasLayer
{
	private Action<GameScene, GameScene> _sceneChangeObserver = null;
	
	public void AddObserver(Action<GameScene, GameScene> callback)
	{
		_sceneChangeObserver+= callback;
	}
	
	public void RemoveObserver(Action<GameScene, GameScene> callback)
	{
		_sceneChangeObserver-= callback;
	}
	
	protected void notifyObservers(GameScene current, GameScene next)
	{
		_sceneChangeObserver?.Invoke(current, next);
	}
	
}
