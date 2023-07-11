using Godot;
using System;

public partial class UI : Control
{
	[Export] ProgressBar Bar;
	[Export] Player Player;

    public override void _Process(double delta)
    {
        Bar.Value = Player.Energy;
    }
}
