using Godot;
using System;

public partial class Player : RigidBody2D
{
	[Export] int Speed;
	[Export] int JumpHeight = 400;

	[Export] AnimatedSprite2D? Sprite;
	[Export] GpuParticles2D? Particles;
	[Export] GpuParticles2D? Particles2;

	PlayerState State;

	public override void _PhysicsProcess(double delta)
	{
		if (State == PlayerState.Normal)
			Move();
	}

	void Move()
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
			SetSpriteFlip(false);
			Sprite.Play("run");
		}
		else if (direction.X > 0)
		{
			SetSpriteFlip(true);
			Sprite.Play("run");
		}
	}

	void SetSpriteFlip(bool flip)
	{
		if (Sprite == null) return;

		if (flip)
		{
			Sprite.Scale = new Vector2(-2, 2);
		}
		else
		{
			Sprite.Scale = new Vector2(2, 2);
		}
	}

	async void OnFoodEaten(int value)
	{
		State = PlayerState.Eating;
		Sprite?.Play("chomp");
		await ToSignal(Sprite, "animation_finished");
		State = PlayerState.Normal;
	}

	async void OnPoisonEaten()
	{
		State = PlayerState.Eating;
		Sprite?.Play("dead");
		await ToSignal(Sprite, "animation_finished");
		GetTree().Quit();
	}

	public override void _Input(InputEvent ev)
	{
		if (ev.IsActionPressed("Quit"))
		{
			GetTree().Quit();
		}
	}
}

enum PlayerState
{
	Normal,
	Eating,
	Dead
}
