using Godot;
using System;

public partial class Food : Area2D
{
	[Export] public int Value = 10;
	[Export] public FoodType FoodType;
}

public enum FoodType {
	Normal,
	Poison,
	Conductive
}
