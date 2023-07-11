using Godot;
using System;

public partial class UI : Control
{
	[Export] ProgressBar? EnergyBar;
	[Export] ProgressBar? RatsBar;
	[Export] Player? Player;

	public override void _Process(double delta)
	{
		if (Player == null || EnergyBar == null || RatsBar == null) return;

		GetNode<ProgressBar>("ProgressBar").Value = Player.Energy;
		GetNode<ProgressBar>("ProgressBar2").Value = (float)ChildSpawner.RatCount / (float)ChildSpawner.MaxRatCount * 100;
	}
}
