using Godot;

namespace project_attempt.Scripts;
	

public partial class Tohu : PlayableCharacter
{

	public override void _Process(double delta)
	{
		Player = GetNode<AnimatedSprite2D>("AnimatedSprite");
		ParentNode = GetParent();
		_hitbox = (CollisionShape2D)GetNode("Hitbox_Area/Special_Hitbox");
		PlayCharacterAnimation();
	}

	
	private void _on_area_2d_body_entered(Node2D body)
	{
		_healthbar.Health -= 20;
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









