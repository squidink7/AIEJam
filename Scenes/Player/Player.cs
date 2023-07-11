using Godot;
using System;

public partial class Player : RigidBody2D
{
	[Export] int Speed;
	[Export] int JumpHeight = 200;

	[Export] AnimatedSprite2D? Sprite;
	[Export] GpuParticles2D? Particles;
	[Export] GpuParticles2D? Particles2;
	[Export] UI? UI;

	PlayerState State;
	public double Energy = 100;

	public override void _PhysicsProcess(double delta)
	{
		if (State == PlayerState.Normal)
			Move();

		Energy -= delta * 2;
		Energy = Math.Clamp(Energy, 0, 100);

		if (Energy == 0) Die();
	}

	void Move()
	{
		var input = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");

		var velocity = input * (float)((Speed * (Energy / 100)) + 300);
		
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
		}
		else if (direction.X > 0)
		{
			SetSpriteFlip(true);
		}
		Sprite.Play("run");
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

	async void OnEaten(int type)
	{
		var foodType = (FoodType)type;
		
		if (foodType == FoodType.Normal)
		{
			State = PlayerState.Eating;
			Sprite?.Play("chomp");
			await ToSignal(Sprite, "animation_finished");
			Energy += 10;
			State = PlayerState.Normal;
		}
		else if (foodType == FoodType.Poison)
		{
			State = PlayerState.Dead;
			Sprite?.Play("chomp");
			await ToSignal(Sprite, "animation_finished");
			Sprite?.Play("dead");
			await ToSignal(Sprite, "animation_finished");
			GetTree().ChangeSceneToFile("res://Scenes/UI/Lose.tscn");
		}
		else if (foodType == FoodType.Conductive)
		{
			State = PlayerState.Dead;
			Sprite?.Play("chomp");
			await ToSignal(Sprite, "animation_finished");
			Sprite?.Play("electrocuted");
			await ToSignal(Sprite, "animation_finished");
			GetTree().ChangeSceneToFile("res://Scenes/UI/Lose.tscn");
		}
	}

	async void Die()
	{
		State = PlayerState.Dead;
		Sprite?.Play("dead");
		await ToSignal(Sprite, "animation_finished");
		GetTree().ChangeSceneToFile("res://Scenes/UI/Lose.tscn");
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
