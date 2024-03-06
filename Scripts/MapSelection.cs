using Godot;
using System;

public partial class MapSelection : Node2D
{

	[Export] protected internal bool selectedMap = false;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		PackedScene sceneLoaderScene = (PackedScene)GD.Load("Scenes/character_select.tscn");
		Node sceneInstance = sceneLoaderScene.Instantiate();
		
		PackedScene packedScene = new PackedScene();
		packedScene.Pack(sceneInstance);
		
		Node parentNode = GetParent();
		if (Input.GetActionStrength("firstMap") != 0)
		{
			GD.Print("Entered");
			GetTree().ChangeSceneToPacked(packedScene);
			GD.Print("Exited");
		}
		else if(Input.GetActionStrength("secondMap") != 0){
			GD.Print("Entered");
			selectedMap = true;
			GetTree().ChangeSceneToPacked(packedScene);
			GD.Print("Exited");
		}
	}
}
