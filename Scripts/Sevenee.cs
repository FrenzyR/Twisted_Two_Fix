using Godot;

namespace project_attempt.Scripts;


public partial class Sevenee : PlayableCharacter
{
	
	/// <summary>
	/// Extension de PlayableCharacter, inicializa
	/// </summary>
	/// <param name="delta"></param>
	public override void _Process(double delta)
	{
		Player = GetNode<AnimationPlayer>("AnimationPlayer");
		Player.SpeedScale = 2;
		HeavyHitbox = (CollisionShape2D)GetNode("Heavy_HitboxArea/Heavy_Hitbox");
		SpecialHitbox = (CollisionShape2D)GetNode("Special_HitboxArea/Special_Hitbox");
		LightHitbox = (CollisionShape2D)GetNode("Light_HitboxArea/Light_Hitbox");
		Hurtbox = (CollisionShape2D)GetNode("HurtboxArea/Hurtbox");
		PlayCharacterAnimation();
		
	}

	/// <summary>
	/// Evento que hace el comportamiento cuando deja de haber animaciones
	/// </summary>
	/// <param name="anim"></param>
	private void _on_animation_player_animation_finished(string anim)
	{
		Player.SpeedScale = 2;
		Speed = 650f;
		Player.Play("idle");
		SpecialHitbox.Disabled = true;
		HeavyHitbox.Disabled = true;
		LightHitbox.Disabled = true;
		WasDamaged = false;
		AnimationPlaying = false;
		Hurtbox.Disabled = false;
	}
	
}









