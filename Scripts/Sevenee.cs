using System;
using Godot;

namespace project_attempt.Scripts;


public partial class Sevenee : PlayableCharacter
{

	public override void _Process(double delta)
	{
		Player = GetNode<AnimatedSprite2D>("AnimatedSprite");
		ParentNode = GetParent();
		_hitbox = (CollisionShape2D)GetNode("Tohu/Area2D/Special_Hitbox");
		Player.FlipH = true;
	}
	
	private void _on_hurtbox_area_body_entered(Node2D body)
	{
		if (body.Name == "Special_Hitbox")
		{
			_healthbar.Health -= 20;	
		}
		
	}

}





