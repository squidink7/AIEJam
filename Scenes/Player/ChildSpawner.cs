using Godot;
using System;

public partial class ChildSpawner : Node2D
{
	[Export] PackedScene? ChildScene;
	[Export] int MaxDistanceFromPlayer = 100;
	[Export] int TargetEnergy = 80;

	void TimerTick()
	{
		var player = GetParent<Player>();
		if (player.Energy >= 80)
			SpawnChild(player.GlobalPosition);
	}
	
	public void SpawnChild(Vector2 parentPosition)
	{
		if (ChildScene == null) return;

		var randomX = (Random.Shared.Next() % MaxDistanceFromPlayer) - MaxDistanceFromPlayer / 2;
		var randomY = (Random.Shared.Next() % MaxDistanceFromPlayer) - MaxDistanceFromPlayer / 2;

		var newRat = ChildScene.Instantiate<Minion>();
		GetNode("/root/Game").AddChild(newRat);

		newRat.GlobalPosition = parentPosition + new Vector2(randomX, randomY);
	}
}
