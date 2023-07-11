using Godot;
using System;

public partial class UI : Control
{
	[Export] ProgressBar EnergyBar;
	[Export] ProgressBar RatsBar;
	[Export] Player Player;

	public override void _Process(double delta)
	{
		EnergyBar.Value = Player.Energy;
		RatsBar.Value = ChildSpawner.RatCount;
	}
}
