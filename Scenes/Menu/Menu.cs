using Godot;
using System;

public partial class Menu : Control
{
	[Export] AnimationPlayer animator;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animator.Play("bob");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
