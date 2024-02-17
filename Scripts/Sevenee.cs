using System;
using Godot;

namespace project_attempt.Scripts;


public partial class Sevenee : PlayableCharacter
{

	PackedScene _sceneLoaderScene = (PackedScene) GD.Load("res://Scenes/tohu.tscn");
	

	public override void _Process(double delta)
	{
		var sceneInstance = _sceneLoaderScene.Instantiate();
		Player = GetNode<AnimatedSprite2D>("AnimatedSprite");
		ParentNode = GetParent();
		_hitbox = sceneInstance.GetNode<CollisionShape2D>("Hitbox_Area/Special_Hitbox");
		
		Player.FlipH = true;
	}

	private void _on_hurtbox_child_entered_tree(Node2D node)
	{
		GD.Print("Hello");
		if (node.Name != "Special_Hitbox") return;
		_healthbar.Health -= 20;
		GD.Print("Hello again");
	}
	
	//deberia haber alguien por encima?

	private void _on_hurtbox_child_exiting_tree(Node2D node)
	{
		GD.Print("Hello");
		if (node.Name != "Special_Hitbox") return;
		_healthbar.Health -= 20;
		GD.Print("Hello again");
	}
	
}





