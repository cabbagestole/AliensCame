using Godot;
using System;

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
