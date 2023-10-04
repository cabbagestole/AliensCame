using Godot;
using System;

public partial class MovingText : Node2D
{
	public float Start = 0;
	public String Text = "";
	[Export]private float Speed = 30;
	[Export]private float InitX = 0;
	[Export]private float InitY = 430;

	private double _erapsed;
	public override void _Ready()
	{
		Position = new Vector2(InitX, InitY);
		GetNode<Label>("Label").Text = Text;
	}
	
	public override void _Process(double delta)
	{
		_erapsed += delta;
		if(_erapsed <= Start) return;
		Vector2 next = Position;
		next.Y -= (float)delta * Speed;
		Position = next;
		
		if(Position.Y <= 0)
			QueueFree();
	}
	
	
}
