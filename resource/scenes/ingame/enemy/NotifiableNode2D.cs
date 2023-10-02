using Godot;
using System;

public partial class NotifiableNode2D : Node2D
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
