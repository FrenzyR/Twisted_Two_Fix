using Godot;
using System;

public partial class MapSelection : Node2D
{

	[Export] protected static internal bool selectedMap = false;
	PackedScene sceneLoaderScene = (PackedScene)GD.Load("Scenes/character_select.tscn");
	PackedScene packedScene = new PackedScene();
	Timer mapChangeTimer;
	private bool Language;
	private AudioStreamPlayer2D music;
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	
    /// <summary>
    ///  El main
    /// </summary>
	public override void _Ready()
	{
		music = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
		music.Playing = settings.Music;
		
	}
    /// <summary>
    /// Se ejecuta cada frame y se encarga de qué se muestra dependiendo del lenguaje, además de escoger el mapa
    /// </summary>
    /// <param name="delta"></param>
	public override void _Process(double delta)
	{
		var mapSelect = GetNode<Sprite2D>("SelectionBG");
		var mapOne = GetNode<Sprite2D>("MapOne");
		var mapTwo = GetNode<Sprite2D>("MapTwo");
		var mapSelectEs = GetNode<Sprite2D>("SelectionBGEs");
		var mapOneEs = GetNode<Sprite2D>("MapOneEs");
		var mapTwoEs = GetNode<Sprite2D>("MapTwoEs");
		var firstButton = GetNode<TouchScreenButton>("MapOneSelect");
		var secondButton = GetNode<TouchScreenButton>("MapTwoSelect");
		if (Language == settings.GetIsLanguageSpanish())
		{
			mapSelect.Visible = true;
			mapSelectEs.Visible = false;
		}
		else
		{
			mapSelect.Visible = false;
			mapSelectEs.Visible = true;
		}
		mapChangeTimer =  GetNode<Timer>("Timer");
		
		Node sceneInstance = sceneLoaderScene.Instantiate();
		
		
		packedScene.Pack(sceneInstance);
		
		Node parentNode = GetParent();
		if (Input.GetActionStrength("firstMap") != 0)
		{
			if (Language == settings.GetIsLanguageSpanish())
			{
				mapSelect.Visible = false;
				mapOneEs.Visible = false;
				mapOne.Visible = true;
			}
			else
			{
				mapSelectEs.Visible = false;
				mapOne.Visible = false;
				mapOneEs.Visible = true;
			}
			secondButton.Visible = false;
			firstButton.Visible = false;
			
			GD.Print("Entered");
			mapChangeTimer.Start();
			GD.Print("Exited");
		}
		else if(Input.GetActionStrength("secondMap") != 0){
			
			if (Language == settings.GetIsLanguageSpanish())
			{
				mapSelect.Visible = false;
				mapTwoEs.Visible = false;
				mapTwo.Visible = true;
			}
			else
			{
				mapSelectEs.Visible = false;
				mapTwo.Visible = false;
				mapTwoEs.Visible = true;
			}
			secondButton.Visible = false;
			firstButton.Visible = false;
			
			GD.Print("Entered");
			selectedMap = true;
			mapChangeTimer.Start();
			GD.Print("Exited");
		}
	}
    /// <summary>
    /// 
    /// </summary>
	private void _on_timer_timeout()
	{
			GetTree().ChangeSceneToPacked(packedScene);
	}

}


