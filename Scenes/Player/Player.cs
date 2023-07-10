using Godot;
using System;

public partial class Player : RigidBody2D
{
	[Export] int Speed;
	[Export] int JumpHeight = 400;

	[Export] AnimatedSprite2D? Sprite;
	[Export] GpuParticles2D? Particles;
	[Export] GpuParticles2D? Particles2;


	public override void _PhysicsProcess(double delta)
	{
		var input = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");

		var velocity = input * Speed;
		
		if (velocity.LengthSquared() >= 1)
		{
			AnimateSprite(velocity.Normalized());
			if (Particles != null) Particles!.Emitting = true;
			if (Particles2 != null) Particles2!.Emitting = true;
		}
		else
		{
			Sprite?.Stop();
			if (Particles != null) Particles.Emitting = false;
			if (Particles2 != null) Particles2!.Emitting = true;
		}

		ApplyCentralForce(velocity);
	}

	void AnimateSprite(Vector2 direction)
	{
		if (Sprite == null) return;

		if (direction.X < 0)
		{
			Sprite.FlipH = false;
			Sprite.Offset = Vector2.Right * 5;
			Sprite.Play("run");
		}
		else if (direction.X > 0)
		{
			Sprite.FlipH = true;
			Sprite.Offset = Vector2.Left * 5;
			Sprite.Play("run");
		}
	}

    public override void _Input(InputEvent ev)
    {
		if (ev.IsActionPressed("Quit"))
		{
			GetTree().Quit();
		}
    }
}
