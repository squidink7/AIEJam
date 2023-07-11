using Godot;
using System;

public partial class Lose : Control
{
	void RetryClicked()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Menu/Menu.tscn");
	}
}
