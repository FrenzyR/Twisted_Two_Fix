using Godot;

namespace project_attempt.Scripts;



public partial class EnemyCharacter : Godot.CharacterBody2D
{
	[Export] protected NodePath CharacterNodePath = null;
	protected float _speed = 350.0f;
	public const float JumpVelocity = -400.0f;
	protected float _health = 0;
	protected Healthbar _healthbar = null;
	private Vector2 _velocity;
	protected Node ParentNode;
	private AnimationPlayer _animPlayer = new AnimationPlayer();
	protected bool _animationPlaying = false;
	private float _knockback = 1.0f;
	protected AnimatedSprite2D Player;
	protected CollisionShape2D _special_hitbox;
	protected CollisionShape2D _heavy_hitbox;
	protected CollisionShape2D _light_hitbox;
	protected bool hasBeenTriggered;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		
		this._healthbar = GetNode<Healthbar>("CanvasLayer/Healthbar");
		this._healthbar.initialize_health(100);
		
		
	}
	
	public override void _Process(double delta)
	{
		this.Player = GetNode<AnimatedSprite2D>("AnimatedSprite");
		Player.FlipH = true;
		
		PlayCharacterAnimation();
	}

	private void Take_Damage(int damage)
	{
		GD.Print("taking damage");
		_healthbar.Health -= damage;
		this.Player.Play("onhit");
	}
	
	public void TriggerVibration()
	{
		Input.VibrateHandheld(1000);
	}

	protected void PlayCharacterAnimation()
	{
		if (_animationPlaying)
			return;
		if (hasBeenTriggered)
		{
			Player.Play("run");
		}
		else
		{
			Player.Play("idle");
		}
	}

	protected bool EnemyHasBeen(bool triggered)
	{
		if (!triggered)
			return false;
		return true;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		this._velocity = Velocity;
		
		
		if (!IsOnFloor())
			_velocity.Y += Gravity * (float)delta;

		
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (hasBeenTriggered)
		{
			_velocity.X = (float)(-1.0) * _speed * _knockback;
		}
		else
		{
			_velocity.X = Mathf.MoveToward(Velocity.X, 0, _speed);
		}

		Velocity = _velocity;
		MoveAndSlide();
	}
	
	private void _on_area_detection_area_entered(Area2D area)
	{
		GD.Print("hello there buddy");
		hasBeenTriggered = EnemyHasBeen(true);
	}

	private void _on_area_detection_area_exited(Area2D area)
	{
		hasBeenTriggered = EnemyHasBeen(false);
	}

}




