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
		_heavy_hitbox = (CollisionShape2D)GetNode("Heavy_HitboxArea/Heavy_Hitbox");
		_special_hitbox = (CollisionShape2D)GetNode("Special_HitboxArea/Special_Hitbox");
		Player.FlipH = true;
		PlayCharacterAnimation();
	}

	private void _on_animated_sprite_animation_changed()
	{
		
		_animationPlaying = true;
		
	}


	private void _on_animated_sprite_animation_looped()
	{
		_speed = 450.0f;
		_animationPlaying = false;
	}

	
}









