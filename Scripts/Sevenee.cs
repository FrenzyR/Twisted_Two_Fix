using Godot;

namespace project_attempt.Scripts;


public partial class Sevenee : PlayableCharacter
{
	

	public override void _Process(double delta)
	{
		Player = GetNode<AnimationPlayer>("AnimationPlayer");
		_heavy_hitbox = (CollisionShape2D)GetNode("Heavy_HitboxArea/Heavy_Hitbox");
		_special_hitbox = (CollisionShape2D)GetNode("Special_HitboxArea/Special_Hitbox");
		
		PlayCharacterAnimation();
	}

	
	
}









