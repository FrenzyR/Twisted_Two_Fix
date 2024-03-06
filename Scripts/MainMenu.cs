using Godot;

namespace project_attempt.Scripts;

public partial class MainMenu : Node2D
{

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		//Idea for the map->characters->game logic; Make a hashmap/array literally whatever, and when after all the slots are 
		//picked, (map, player1, player2) it launches the scene "map", with the added child scenes of "player1" and "player2"
		PackedScene sceneLoaderScene = (PackedScene)GD.Load("Scenes/map_selection.tscn");
		Node sceneInstance = sceneLoaderScene.Instantiate();
		
		PackedScene packedScene = new PackedScene();
		packedScene.Pack(sceneInstance);
		Node parentNode = GetParent();
		if (Input.GetActionStrength("versus") != 0)
		{
			GD.Print("Entered");
			GetTree().ChangeSceneToPacked(packedScene);
			GD.Print("Exited");
		}
	}
}
