using Godot;
using System;

public partial class NotifiableEnemy : Area2D
{
	private Action<Direction> _touchObserver = null;
	private Action _deathObserver = null;
	
	public void AddTouchObserver(Action<Direction> callback)
	{
		_touchObserver+= callback;
	}
	
	protected void notifyTouchObservers(Direction direction)
	{
		_touchObserver?.Invoke(direction);
	}

	public void AddDeathObserver(Action callback)
	{
		_deathObserver+= callback;
	}
	
	protected void notifyDeathObservers()
	{
		_deathObserver?.Invoke();
	}

}
