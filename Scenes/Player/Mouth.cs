using Godot;
using System;
using System.Threading.Tasks;

public partial class Mouth : Area2D
{
	[Signal] public delegate void EatenEventHandler(int type);
	
	async void OnEnter(Area2D area)
	{
		if (area is Food food)
		{
			EmitSignal("Eaten", (int)food.FoodType);
			
			food.SetDeferred("monitorable", false);
			await Task.Delay(500);
			if (IsInstanceValid(food))
				food.QueueFree();
		}
	}
}
