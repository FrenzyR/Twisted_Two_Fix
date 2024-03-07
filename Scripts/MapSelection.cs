using Godot;
using System;

public partial class MapSelection : Node2D
{

	[Export] protected internal bool selectedMap = false;
	PackedScene sceneLoaderScene = (PackedScene)GD.Load("Scenes/character_select.tscn");
	Timer mapChangeTimer;
	PackedScene packedScene = new PackedScene();

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var mapSelect = GetNode<Sprite2D>("SelectionBG");
		var mapOne = GetNode<Sprite2D>("MapOne");
		var mapTwo = GetNode<Sprite2D>("MapTwo");
		var firstButton = GetNode<TouchScreenButton>("MapOneSelect");
		var secondButton = GetNode<TouchScreenButton>("MapTwoSelect");
		mapChangeTimer =  GetNode<Timer>("Timer");
		
		Node sceneInstance = sceneLoaderScene.Instantiate();
		
		
		packedScene.Pack(sceneInstance);
		
		Node parentNode = GetParent();
		if (Input.GetActionStrength("firstMap") != 0)
		{
			secondButton.Visible = false;
			firstButton.Visible = false;
			mapSelect.Visible = false;
			mapOne.Visible = true;
			GD.Print("Entered");
			mapChangeTimer.Start();
			GD.Print("Exited");
		}
		else if(Input.GetActionStrength("secondMap") != 0){
			secondButton.Visible = false;
			firstButton.Visible = false;
			mapSelect.Visible = false;
			mapTwo.Visible = true;
			GD.Print("Entered");
			selectedMap = true;
			mapChangeTimer.Start();
			GD.Print("Exited");
		}
	}
	private void _on_timer_timeout()
	{
			GetTree().ChangeSceneToPacked(packedScene);
	}

}


