using Godot;
using System;

// note
// Observer pattern for Enemy only.
// Enemy専用のObserverパターン。
// 
public partial class NotifiableEnemy : Area2D
{
	private Action<Direction> _touchObserver = null;
	private Action _deathObserver = null;
	private Action _invadeObserver = null;
	
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


	public void AddInvadeObserver(Action callback)
	{
		_invadeObserver+= callback;
	}
	
	protected void notifyInvadeObservers()
	{
		_invadeObserver?.Invoke();
	}

}
