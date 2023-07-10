using Godot;
using System;
using System.Threading.Tasks;

public partial class Mouth : Area2D
{
	[Signal] public delegate void FoodEatenEventHandler(int value);
	[Signal] public delegate void PoisonEatenEventHandler();
	
	async void OnEnter(Area2D area)
	{
		if (area is Food food)
		{
			if (!food.Poison)
				EmitSignal("FoodEaten", food.Value);
			else
				EmitSignal("PoisonEaten");
			
			food.SetDeferred("monitorable", false);
			await Task.Delay(500);
			food.QueueFree();
		}
	}
}
