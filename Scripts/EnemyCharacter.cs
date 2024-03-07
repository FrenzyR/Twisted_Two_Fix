using Godot;

namespace project_attempt.Scripts;



public partial class EnemyCharacter : Godot.CharacterBody2D
{
	[Export] protected NodePath CharacterNodePath = null;
	CollisionShape2D areaDetection;
	protected bool WasDamaged = false;
	protected float Speed = 350.0f;
	public const float JumpVelocity = -400.0f;
	protected float Health = 0;
	protected Healthbar Healthbar = null;
	private Vector2 _velocity;
	protected Node ParentNode;
	private AnimationPlayer _animPlayer = new AnimationPlayer();
	protected bool AnimationPlaying = false;
	private float _knockback = 1.0f;
	protected AnimationPlayer Player;
	protected CollisionShape2D SpecialHitbox;
	protected CollisionShape2D HeavyHitbox;
	protected CollisionShape2D LightHitbox;
	protected bool HasBeenTriggered;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		
		this.Healthbar = GetNode<Healthbar>("CanvasLayer/Healthbar");
		this.Healthbar.initialize_health(100);
		
		
	}
	
	public override void _Process(double delta)
	{
		areaDetection = GetNode<CollisionShape2D>("AreaDetection/TriggeredCollision");
		this.Player = GetNode<AnimationPlayer>("AnimationPlayer");
		Player.SpeedScale = 2;
		HeavyHitbox = (CollisionShape2D)GetNode("Heavy_HitboxArea/Heavy_Hitbox");
		SpecialHitbox = (CollisionShape2D)GetNode("Special_HitboxArea/Special_Hitbox");
		LightHitbox = (CollisionShape2D)GetNode("Light_HitboxArea/Light_Hitbox");
		PlayCharacterAnimation();
	}

	public override void _PhysicsProcess(double delta)
	{
		this._velocity = Velocity;

		GD.Print(GlobalPosition);
		if (Position.Y >= 1000)
		{
			var position = new Vector2(Position.X, 380);
			Position = position;
		}

		if (Position.X >= 2850)
		{
			var position = new Vector2(2700, Position.Y);
			Position = position;
		}
		
		if (!IsOnFloor())
			_velocity.Y += Gravity * (float)delta;

		
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (WasDamaged)
		{
			Speed = 0f;	

			MoveAndCollide(_velocity);
			GD.Print(_velocity.X);
		}
		if (HasBeenTriggered)
		{
			_velocity.X = (float)(-1.0) * Speed;
		}
		else
		{
			_velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = _velocity;
		MoveAndSlide();
	}
	protected void PlayCharacterAnimation()
	{

		if (AnimationPlaying)
		{
			return;
		}
		if (WasDamaged)
		{
			GD.Print("hit");
			
			areaDetection.Disabled = true;
			Player.Stop();
			
			Player.Play("onhit");
			WasDamaged = false;
			AnimationPlaying = false;
			
		}
		else if (HasBeenTriggered)
		{
			
			Player.Play("run");
			AnimationPlaying = false;
			
		}
		else
		{
			Player.Play("idle");
		}
		
	}

	private void Take_Damage(int damage, Vector2 knockback)
	{
		TriggerVibration();
		_velocity = knockback;
		var newPosition = Position;
		newPosition.X += 20;
		Position = newPosition;
		MoveAndCollide(_velocity);
		Healthbar.Health -= damage;
		WasDamaged = true;
	}
	
	public void TriggerVibration()
	{
		Input.VibrateHandheld(1000);
	}

	


	protected bool EnemyHasBeen(bool triggered)
	{
		if (!triggered)
			return false;
		return true;
	}

	private void _on_area_detection_area_entered(Area2D area)
	{
		GD.Print("hello there buddy");
		HasBeenTriggered = EnemyHasBeen(true);
	}

	private void _on_area_detection_area_exited(Area2D area)
	{
		HasBeenTriggered = EnemyHasBeen(false);
	}

	public void _on_animation_player_animation_finished(string anim)
	{
		if (anim != "idle")
		{
			GD.Print("Animation finished: ", anim);	
		}
		areaDetection.Disabled = false;
		Player.SpeedScale = 2;
		Speed = 350f;
		SpecialHitbox.Disabled = true;
		HeavyHitbox.Disabled = true;
		LightHitbox.Disabled = true;
		WasDamaged = false;
		AnimationPlaying = false;
		
	}

}




