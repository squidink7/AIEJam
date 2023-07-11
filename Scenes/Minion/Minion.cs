using Godot;
using System;

public partial class Minion : RigidBody2D
{
	[Export] int Speed;
	[Export] int JumpHeight = 400;

	[Export] AnimatedSprite2D? Sprite;
	[Export] GpuParticles2D? Particles;
	[Export] GpuParticles2D? Particles2;
	[Export] NavigationAgent2D? Navigator;
	[Export] public Player? Parent;
	string furColour;

	PlayerState State;
	
	public override void _Ready()
	{
		furColour = (Random.Shared.Next()%3).ToString();
	}

	public override void _PhysicsProcess(double delta)
	{
		if (State == PlayerState.Normal)
			Move();
	}

	void Move()
	{
		// var input = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
		if (Navigator == null) return;

		var velocity = ToLocal(Navigator.GetNextPathPosition()).Normalized() * Speed;
		
		if (velocity.LengthSquared() >= 1 && !Navigator.IsTargetReached())
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

		if (!Navigator.IsTargetReached())
			ApplyCentralForce(velocity);
	}

	void AnimateSprite(Vector2 direction)
	{
		if (Sprite == null) return;

		if (direction.X < 0)
		{
			SetSpriteFlip(false);
			Sprite.Play("Run" + furColour);
		}
		else if (direction.X > 0)
		{
			SetSpriteFlip(true);
			Sprite.Play("Run" + furColour);
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

	async void OnEaten(int type)
	{
		var foodType = (FoodType)type;
		if (foodType == FoodType.Normal)
		{
			State = PlayerState.Eating;
			Sprite?.Play("Chomp" + furColour);
			await ToSignal(Sprite, "animation_finished");
			State = PlayerState.Normal;
		}
		else if (foodType == FoodType.Poison)
		{
			State = PlayerState.Dead;
			Sprite?.Play("Death" + furColour);
			ChildSpawner.RatCount--;
		}
		else if (foodType == FoodType.Conductive)
		{
			State = PlayerState.Dead;
			Sprite?.Play("Electrocuted" + furColour);
			ChildSpawner.RatCount--;
		}
	}

	void FindParent()
	{
		var parentPosition = Parent?.GlobalPosition ?? Vector2.Zero;
		var randomX = (Random.Shared.Next() % 200) - 200 / 2;
		var randomY = (Random.Shared.Next() % 200) - 200 / 2;
		Navigator.TargetPosition = parentPosition + new Vector2(randomX, randomY);;
	}

	public override void _Input(InputEvent ev)
	{
		if (ev.IsActionPressed("Quit"))
		{
			GetTree().Quit();
		}
	}
}
