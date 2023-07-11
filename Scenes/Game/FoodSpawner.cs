using Godot;
using System;

public partial class FoodSpawner : Node
{
	[Export] TileMap? World;
	[Export] PackedScene[]? FoodScenes;

	public void SpawnFood()
	{
		if (FoodScenes == null) return;
		if (World == null) return;

		var posX = Random.Shared.Next() % World.GetUsedRect().Size.X * 32;
		var posY = Random.Shared.Next() % World.GetUsedRect().Size.Y * 32;

		var newRat = FoodScenes[Random.Shared.Next() % FoodScenes.Length].Instantiate<Food>();
		GetNode("/root/Game").AddChild(newRat);

		newRat.GlobalPosition = new Vector2(posX, posY);
	}
}
