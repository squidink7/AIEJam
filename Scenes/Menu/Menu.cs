using Godot;
using System;

public partial class Menu : Control
{
	[Export] AnimationPlayer Animator;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Animator.Play("bob");
	}

	void startclick ()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Game/Game.tscn");
	}

	void quitclick ()
	{
		GetTree().Quit();
	}
}

