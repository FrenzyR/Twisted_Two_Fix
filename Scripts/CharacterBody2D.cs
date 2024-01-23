using Godot;
using System;

public partial class CharacterBody2D : Godot.CharacterBody2D
{
	public const float Speed = 750.0f;
	public const float JumpVelocity = -400.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	public AnimatedSprite2D player = AnimatedSprite2D. "";
	

	public override void _Process(double delta)
	{
		var sceneLoaderScene = (PackedScene) GD.Load("res://Scenes/second_map.tscn");
		var sceneInstance = sceneLoaderScene.Instantiate();
		var packedScene = new PackedScene();
		var result = packedScene.Pack(sceneInstance);
		Node parentNode = GetParent();
		if (Input.GetActionStrength("ui_right") != 0)
		{
			player.Play("run");
		}
		else if(Input.GetActionStrength("heavy") != 0)
		{
			player.Play("heavy");
		}
		else if(Input.GetActionStrength("light") != 0)
		{
			player.Play("light");
		}
		else{
			player.Play("idle");
		}
		//GD.Print("Entered");
		//GetTree().ChangeSceneToPacked(packedScene);
		//GD.Print("Exited");
	}


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
			
			var myLabel = GetNode("NormalLabel") as Label;
			
		
		
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;


		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
	
	private void _on_animated_sprite_animation_finished()
	{
		// Replace with function body.
	}

}

