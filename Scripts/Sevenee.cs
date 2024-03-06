using Godot;

namespace project_attempt.Scripts;


public partial class Sevenee : PlayableCharacter
{
	

	public override void _Process(double delta)
	{
		Player = GetNode<AnimationPlayer>("AnimationPlayer");
		Player.SpeedScale = 2;
		_heavy_hitbox = (CollisionShape2D)GetNode("Heavy_HitboxArea/Heavy_Hitbox");
		_special_hitbox = (CollisionShape2D)GetNode("Special_HitboxArea/Special_Hitbox");
		_light_hitbox = (CollisionShape2D)GetNode("Light_HitboxArea/Light_Hitbox");
		PlayCharacterAnimation();
	}

	private void _on_animation_player_animation_finished(string anim)
	{
		Player.SpeedScale = 2;
		_speed = 650f;
		Player.Play("idle");
		_special_hitbox.Disabled = true;
		_heavy_hitbox.Disabled = true;
		_light_hitbox.Disabled = true;
		_wasDamaged = false;
		_animationPlaying = false;
	}
	
}









