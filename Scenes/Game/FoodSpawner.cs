using Godot;
using System;

public partial class FoodSpawner : Node
{
	[Export] TileMap? World;
	[Export] PackedScene[]? FoodScenes;
	[Export] Player? Player;

	public void SpawnFood()
	{
		if (FoodScenes == null) return;
		if (World == null) return;
		if (Player == null) return;

		var posX = Random.Shared.Next() % World.GetUsedRect().Size.X * 32;
		var posY = Random.Shared.Next() % World.GetUsedRect().Size.Y * 32;

		var pos = new Vector2(posX, posY);

		var lengthToPlayer = Mathf.Abs((Player.GlobalPosition - pos).Length());
		
		if (lengthToPlayer < 80) return;

		var food = FoodScenes[Random.Shared.Next() % FoodScenes.Length].Instantiate<Node2D>();
		GetNode("/root/Game").AddChild(food);

		food.GlobalPosition = pos;
	}
}
