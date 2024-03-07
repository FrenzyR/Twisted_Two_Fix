using Godot;

namespace project_attempt.Scripts;

public partial  class MyHitbox : Area2D
{	
	[Export] protected internal int Damage = 20;
	[Export] protected internal Vector2 Knockback = new(165 , -60.5f);

	private void Initialize()
	{
		CollisionLayer = 2;
		CollisionMask = 0;
	}

   
}
