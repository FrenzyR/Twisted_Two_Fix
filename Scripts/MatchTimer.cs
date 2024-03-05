using Godot;
using System;

public partial class MatchTimer : Godot.Timer
{
	private Label timeLabel;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		timeLabel = (Label)GetNode("Label");
		timeLabel.Text =""+ (int)TimeLeft;
	}
}
