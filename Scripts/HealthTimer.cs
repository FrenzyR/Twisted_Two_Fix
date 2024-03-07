using Godot;

namespace project_attempt.Scripts;

public partial class HealthTimer : Godot.Timer
{
	private Label _timeLabel;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	/// <summary>
	/// Se ejecuta cada frame
	/// </summary>
	/// <param name="delta"></param>
	public override void _Process(double delta)
	{
		_timeLabel = (Label)GetNode("Label");
		_timeLabel.Text =""+ (int)TimeLeft;
	}
}