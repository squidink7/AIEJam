using Godot;
using System;

public partial class Player : RigidBody2D
{
	[Export] int Speed = 300;
	[Export] int JumpHeight = 400;
	[Export] AnimatedSprite2D? Sprite;

	public override void _PhysicsProcess(double delta)
	{
		var input = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");

		var velocity = input * Speed;
		
		if (velocity.LengthSquared() >= 1)
		{
			AnimateSprite(velocity.Normalized());
		}
		else
		{
			Sprite?.Stop();
		}

		ApplyCentralForce(velocity);
	}

	void AnimateSprite(Vector2 direction)
	{
		if (Sprite == null) return;

		if (direction.X < 0)
		{
			Sprite.FlipH = false;
			Sprite.Play("run");
		}
		else if (direction.X < 0)
		{
			Sprite.FlipH = true;
			Sprite.Play("run");
		}

	}
}
