using Godot;

namespace project_attempt.Scripts;


public partial class Sevenee : PlayableCharacter
{
	

	public override void _Process(double delta)
	{
		Player = GetNode<AnimatedSprite2D>("AnimatedSprite");
		_heavy_hitbox = (CollisionShape2D)GetNode("Heavy_HitboxArea/Heavy_Hitbox");
		_special_hitbox = (CollisionShape2D)GetNode("Special_HitboxArea/Special_Hitbox");
		
		PlayCharacterAnimation();
	}

	private void _on_animated_sprite_animation_changed()
	{
		
		_animationPlaying = true;
		
	}


	private void _on_animated_sprite_animation_looped()
	{
		_speed = 350.0f;
		_animationPlaying = false;
	}

	
}









