using Godot;
using System;

public partial class CharacterSelect : Node2D
{
	
	[Export] protected static internal bool SelectedChar = true;
	PackedScene sceneLoaderScene = (PackedScene)GD.Load("Scenes/map.tscn");
	Timer moveToMapTimer;
	PackedScene packedScene = new PackedScene();
	private AudioStreamPlayer2D music;
	private bool Language;

	/// <summary>
	/// Main
	/// </summary>
	public override void _Ready()
	{
		music = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
		music.Playing = settings.Music;
		var charSelectEn = GetNode < Sprite2D > ("BasicBackground/CharacterSelectEn");
		var charSelectEs = GetNode < Sprite2D > ("BasicBackground/CharacterSelectEs");
		if (Language == settings.GetIsLanguageSpanish())
		{
			charSelectEs.Visible = false;
			
		}
		else
		{
			charSelectEn.Visible = false;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	/// <summary>
	/// Se ejecuta cada frame, se escoge el personaje
	/// </summary>
	/// <param name="delta"></param>
	public override void _Process(double delta)
	{
		var charSelect = GetNode<Sprite2D>("BasicBackground/NotSelected");
		var tohu = GetNode<Sprite2D>("BasicBackground/TohuSelected");
		var sevenee = GetNode<Sprite2D>("BasicBackground/SeveneeSelected");
		moveToMapTimer =  GetNode<Timer>("Timer");
		var firstButton = GetNode<TouchScreenButton>("TohuSelect");
		var secondButton = GetNode<TouchScreenButton>("NeeSelect");
		
		Node sceneInstance = sceneLoaderScene.Instantiate();
		
		
		packedScene.Pack(sceneInstance);
		
		Node parentNode = GetParent();
		if (Input.GetActionStrength("tohu") != 0)
		{
			secondButton.Visible = false;
			firstButton.Visible = false;
			charSelect.Visible = false;
			tohu.Visible = true;
			SelectedChar = true;
			GD.Print("Entered");
			moveToMapTimer.Start();
			GD.Print("Exited");
		}
		else if(Input.GetActionStrength("sevenee") != 0)
		{
			SelectedChar = true;
			secondButton.Visible = false;
			firstButton.Visible = false;
			charSelect.Visible = false;
			sevenee.Visible = true;
			GD.Print("Entered");
			SelectedChar = false;
			GD.Print(SelectedChar);
			moveToMapTimer.Start();
			GD.Print("Exited");
		}
	}
	/// <summary>
	/// Evento timer para cambiar de escena
	/// </summary>
	private void _on_timer_timeout()
	{
		GetTree().ChangeSceneToPacked(packedScene);
	}

}


