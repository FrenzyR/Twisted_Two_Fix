using Godot;
using System;

public partial class credits : Node2D
{
	// Called when the node enters the scene tree for the first time.
	/// <summary>
	/// Main
	/// </summary>
	public override void _Ready()
	{
		music = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
		music.Playing = settings.Music;
	}

	private PackedScene mainMenuPack;
	private AudioStreamPlayer2D music;
	/// <summary>
	/// Te da la opcion cada frame de volver al menu
	/// </summary>
	/// <param name="delta"></param>
	public override void _Process(double delta)
	{
		
		Timer timer = GetNode<Timer>("Timer");
		
		PackedScene mainMenuScene = (PackedScene)GD.Load("Scenes/main_menu.tscn");
		Node mainMenuInstance = mainMenuScene.Instantiate();
		mainMenuPack = new PackedScene();
		mainMenuPack.Pack(mainMenuInstance);
		var selectClick = GetNode<AudioStreamPlayer2D>("SelectClick");
		
		if (Input.GetActionStrength("main_menu") != 0)
		{
			if (!selectClick.Playing)
			{
				selectClick.Play();	
			}
			timer.Start();
		}
	}

	/// <summary>
	/// Al llegar a 0 te lleva al menu
	/// </summary>
	private void on_timer_timeout()
	{
		GetTree().ChangeSceneToPacked(mainMenuPack);
	}
}
