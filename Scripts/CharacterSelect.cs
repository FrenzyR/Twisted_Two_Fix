using Godot;
using System;

public partial class CharacterSelect : Node2D
{
	
	[Export] protected internal bool selectedChar = false;
	PackedScene sceneLoaderScene = (PackedScene)GD.Load("Scenes/map.tscn");
	Timer moveToMapTimer;
	PackedScene packedScene = new PackedScene();

	// Called every frame. 'delta' is the elapsed time since the previous frame.
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
			GD.Print("Entered");
			moveToMapTimer.Start();
			GD.Print("Exited");
		}
		else if(Input.GetActionStrength("sevenee") != 0){
			secondButton.Visible = false;
			firstButton.Visible = false;
			charSelect.Visible = false;
			sevenee.Visible = true;
			GD.Print("Entered");
			selectedChar = true;
			moveToMapTimer.Start();
			GD.Print("Exited");
		}
	}
	private void _on_timer_timeout()
	{
		GetTree().ChangeSceneToPacked(packedScene);
	}

}


