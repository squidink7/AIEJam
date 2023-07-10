using Godot;
using System;

public partial class Player : RigidBody2D
{
	[Export] int Speed = 300;
	[Export] int Deceleration = 10;
	[Export] int JumpHeight = 400;

	public override void _PhysicsProcess(double delta)
	{
		var input = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

		var velocity = new Vector2();
		
		if (input != Vector2.Zero)
		{
			velocity = input * Speed;
		}
		else
		{
			// velocity = velocity.MoveToward(Vector2.Zero, Speed / Deceleration);
		}
		
		ApplyCentralForce(velocity);
	}
}
