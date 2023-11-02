using Godot;
using System;

// note
// Observer pattern for Node2D only.
// Node2D専用のObserverパターン。
// 
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
