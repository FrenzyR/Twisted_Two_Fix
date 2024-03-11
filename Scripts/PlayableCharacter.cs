using System;
using Godot;

namespace project_attempt.Scripts;


public partial class PlayableCharacter : Godot.CharacterBody2D
{
	[Export] protected NodePath CharacterNodePath = null;
	private AudioStreamPlayer damageNoise;
	protected bool WasDamaged = false;
	public float Speed = 650.0f;
	public const float JumpVelocity = -400.0f;
	protected float Health = 0;
	public Healthbar Healthbar = null;
	private Vector2 _velocity;
	protected Node ParentNode;
	private AnimationPlayer _animPlayer = new AnimationPlayer();
	protected bool AnimationPlaying = false;
	private float _knockback = 1.0f;
	protected AnimationPlayer Player;
	protected CollisionShape2D SpecialHitbox;
	protected CollisionShape2D HeavyHitbox;
	protected CollisionShape2D LightHitbox;
	private Vector3 _accelerometer;
	protected CollisionShape2D Hurtbox;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	
	/// <summary>
	/// Apaga la hurtbox si se bloquea
	/// </summary>
	private void turnHurtboxOff()
	{
		Hurtbox.Disabled = true;
	}
	/// <summary>
	/// 
	/// </summary>
	public override void _Ready()
	{
		damageNoise = GetNode<AudioStreamPlayer>("OnHit");
		this.Healthbar = GetNode<Healthbar>("CanvasLayer/Healthbar");
		this.Healthbar.initialize_health(100);
		

	}
	/// <summary>
	/// Lo mismo que en EnemyCharacter
	/// </summary>
	/// <param name="damage"></param>
	/// <param name="knockback"></param>
	private void Take_Damage(int damage, Vector2 knockback)
	{
		TriggerVibration();
		_velocity = new Vector2(-knockback.X+30, knockback.Y);
		var newPosition = Position;
		newPosition.X -= 20;
		Position = newPosition;
		MoveAndCollide(_velocity);
		Healthbar.Health -= damage;
		WasDamaged = true;
		damageNoise.Play();
		Player.Play("onhit");
	}

	/// <summary>
	/// Se ejecuta cada frame 
	/// </summary>
	/// <param name="delta"></param>
	public override void _Process(double delta)
	{
		this.Player = GetNode<AnimationPlayer>("AnimationPlayer");
		
		PlayCharacterAnimation();
	}

	/// <summary>
	/// Causa que vibre el telefono
	/// </summary>
	public void TriggerVibration()
	{
		Input.VibrateHandheld(1000);
	}
	/// <summary>
	/// Lo mismo que en enemyCharacter
	/// </summary>
	protected void PlayCharacterAnimation()
	{
		if (AnimationPlaying)
		{
			return;
		}

		if (WasDamaged)
		{
			Player.Play("onhit");
			WasDamaged = false;
			AnimationPlaying = false;
		}
		if (Input.GetActionStrength("ui_right") != 0)
		{
			Player.SpeedScale = 4;
			Player.Play("run");
			
			AnimationPlaying = false;
			
		}
		else if(Input.GetActionStrength("ui_left") != 0)
		{
			Player.SpeedScale = 4;
			Player.Play("backward");
			
			AnimationPlaying = false;
		}
		else if(Input.GetActionStrength("heavy") != 0)
		{
			Player.Play("heavy");
			AnimationPlaying = false;
			
			Speed = 0.0f;
		}
		else if(Input.GetActionStrength("light") != 0)
		{
			Player.SpeedScale = 4;	
			Player.Play("light");
			AnimationPlaying = false;
			Speed = 0.0f;
		}
		
		else if(Input.GetActionStrength("special") != 0)
		{
			Player.Play("special");
			AnimationPlaying = false;

		}
		else if (Input.GetAccelerometer().X <= -4.0f)
		{
			
			Player.SpeedScale = 1f;
			Player.Play("block");
		}
		GD.Print(Input.GetAccelerometer().X);
		
	}
	/// <summary>
	/// 
	/// </summary>

	public void turnLightHitboxOn()
	{
		LightHitbox.Disabled = false;
	
	}

	/// <summary>
	/// 
	/// </summary>
	public void turnHeavyHitboxOn()
	{
		HeavyHitbox.Disabled = false;
	
	}

	/// <summary>
	/// 
	/// </summary>
	public void turnSpecialHitboxOn()
	{
		SpecialHitbox.Disabled = false;
	
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="delta"></param>
	public override void _PhysicsProcess(double delta)
	{
		this._velocity = Velocity;
		
		
		if (Position.Y >= 1000)
		{
			var position = new Vector2(Position.X, 100);
			Position = position;
		}

		if (Position.X <= 540)
		{
			var position = new Vector2(570, Position.Y);
			Position = position;
		}
		
		if (!IsOnFloor())
			_velocity.Y += Gravity * (float)delta;

		
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			_velocity.X = direction.X * Speed * _knockback;
		}
		else
		{
			_velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = _velocity;
		MoveAndSlide();
	}

	
	





}


