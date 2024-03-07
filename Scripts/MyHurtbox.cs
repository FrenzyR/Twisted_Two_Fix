using Godot;

namespace project_attempt.Scripts;

public partial class MyHurtbox : Area2D
{
	public void Initialize()
	{
		CollisionLayer = 0;
		CollisionMask = 2;
	}
	public override void _Ready()
	{
		Connect("area_entered", new Callable(this, "_on_area_entered"));
	}

	private void _on_area_entered(MyHitbox hitbox)
	{
		GD.Print("hello");
		if (hitbox == null)
		{
			return;
		}

		hitbox.Damage = 30;
		hitbox.Knockback = new Vector2(300, -60.5f);
		if (hitbox.Name == "Light_HitboxArea")
		{
			hitbox.Knockback = new Vector2(90, -60.5f);
			hitbox.Damage = 3;
		}
		else if (hitbox.Name == "Heavy_HitboxArea")
		{
			hitbox.Knockback = new Vector2(240, -60.5f);
			hitbox.Damage = 20;
		}
		
		if (Owner.HasMethod("Take_Damage"))
		{
			Owner.Call("Take_Damage", hitbox.Damage, hitbox.Knockback);
		}
	}
}
