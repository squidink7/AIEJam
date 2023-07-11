using Godot;
using System;

public partial class ChildSpawner : Node2D
{
	[Export] PackedScene? ChildScene;
	[Export] int MaxDistanceFromPlayer = 100;
	[Export] int TargetEnergy = 80;

	public static int RatCount = 0;

    public override void _Ready()
    {
        RatCount = 0;
    }

    

	void TimerTick()
	{
		var player = GetParent<Player>();
		if (player.Energy >= 80)
			SpawnChild(player);
	}
	
	public void SpawnChild(Player parent)
	{
		if (ChildScene == null) return;

		var randomX = (Random.Shared.Next() % MaxDistanceFromPlayer) - MaxDistanceFromPlayer / 2;
		var randomY = (Random.Shared.Next() % MaxDistanceFromPlayer) - MaxDistanceFromPlayer / 2;

		var newRat = ChildScene.Instantiate<Minion>();
		GetNode("/root/Game").AddChild(newRat);

		newRat.GlobalPosition = parent.GlobalPosition + new Vector2(randomX, randomY);
		newRat.Parent = parent;

		RatCount++;
		
		if (RatCount >= 100)
		{
			GetTree().ChangeSceneToFile("res://Scenes/UI/Win.tscn");
		}
	}
}
