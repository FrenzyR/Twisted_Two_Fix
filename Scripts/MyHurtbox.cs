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

		if (Owner.HasMethod("Take_Damage"))
		{
			Owner.Call("Take_Damage", hitbox.Damage);
		}
	}
	
}
