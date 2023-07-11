using Godot;
using System;

public partial class Food : Area2D
{
	[Export] public int Value = 10;
	[Export] public bool Poison = false;
	[Export] public bool Conductive = false;
}
