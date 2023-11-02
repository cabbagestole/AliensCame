using Godot;
using System;

// note
// Observer pattern for Area2D only.
// Area2D専用のObserverパターン。
public partial class NotifiableArea2D : Area2D
{
	private Action _observer = null;
	
	public void AddObserver(Action callback)
	{
		_observer+= callback;
	}
	
	protected void notifyObservers()
	{
		_observer?.Invoke();
	}
	
}
