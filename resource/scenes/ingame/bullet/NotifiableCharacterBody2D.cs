using Godot;
using System;

public partial class NotifiableCharacterBody2D : CharacterBody2D
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
