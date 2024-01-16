using Godot;
using System;

public partial class main_menu : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//Idea for the map->characters->game logic; Make a hashmap/array literally whatever, and when after all the slots are 
		//picked, (map, player1, player2) it launches the scene "map", with the added child scenes of "player1" and "player2"
		var sceneLoaderScene = (PackedScene) GD.Load("res://Scenes/test_map.tscn");
		var sceneInstance = sceneLoaderScene.Instantiate();
		
		var packedScene = new PackedScene();
		var result = packedScene.Pack(sceneInstance);
		Node parentNode = GetParent();
		if (Input.GetActionStrength("versus") != 0)
		{
			GD.Print("Entered");
			GetTree().ChangeSceneToPacked(packedScene);
			GD.Print("Exited");
		}
	}
}
