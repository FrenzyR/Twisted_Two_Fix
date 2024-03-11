using System;
using Godot;

namespace project_attempt.Scripts;



public partial class EnemyCharacter : Godot.CharacterBody2D
{
	[Export] protected NodePath CharacterNodePath = null;
	CollisionShape2D triggeredCollision;
	private AudioStreamPlayer damageNoise;
	protected bool WasDamaged = false;
	public float Speed = 350.0f;
	protected float Health = 0;
	public Healthbar Healthbar = null;
	private Vector2 _velocity;
	protected Node ParentNode;
	public AnimationPlayer _animPlayer = new AnimationPlayer();
	protected bool AnimationPlaying = false;
	private float _knockback = 1.0f;
	protected AnimationPlayer Player;
	protected CollisionShape2D LightHitbox;
	protected CollisionShape2D HeavyHitbox;
	public bool HasBeenTriggered;
	public bool _closeEnough;
	private CollisionShape2D attackCollision;

	
	public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	/// <summary>
	/// Enciende la hitbox de ataques fuertes
	/// </summary>
	public void turnHeavyHitboxOn()
	{
		HeavyHitbox.Disabled = false;
	
	}
	
	/// <summary>
	/// Enciende la hitbox de ataques ligeros
	/// </summary>
	public void turnLightHitboxOn()
	{
		LightHitbox.Disabled = false;
	
	}

	/// <summary>
	/// Main
	/// </summary>
	public override void _Ready()
	{
		damageNoise = GetNode<AudioStreamPlayer>("OnHit");
		damageNoise.Autoplay = true;
		this.Healthbar = GetNode<Healthbar>("CanvasLayer/Healthbar");
		this.Healthbar.initialize_health(100);
		triggeredCollision = GetNode<CollisionShape2D>("AreaDetection/TriggeredCollision");
		attackCollision = GetNode<CollisionShape2D>("AttackDetection/AttackCollision");
		HeavyHitbox = (CollisionShape2D)GetNode("Heavy_HitboxArea/Heavy_Hitbox");
		LightHitbox = (CollisionShape2D)GetNode("Light_HitboxArea/Light_Hitbox");
	}

	/// <summary>
	/// Cada frame
	/// </summary>
	/// <param name="delta"></param>
	public override void _Process(double delta)
	{
		
		this.Player = GetNode<AnimationPlayer>("AnimationPlayer");
		
		PlayCharacterAnimation();
	}

	/// <summary>
	/// Se ejecuta cada frame y tiene control de las fisicas
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

	/// <summary>
	/// Se encarga de jugar con el AnimationPlayer
	/// </summary>
	private void PlayCharacterAnimation()
	{
		

		if (AnimationPlaying)
		{
			return;
		}
		if (WasDamaged)
		{
			GD.Print("he was hit");
			
			triggeredCollision.Disabled = true;
			Player.Stop();
			Player.Play("onhit");
			WasDamaged = false;
			AnimationPlaying = false;
			
		}
		else if (_closeEnough)
		{
			Speed = 0f;
			HasBeenTriggered = false;
			var randomAttack = new Random();
			var heavyOrLight = randomAttack.Next(0, 8);
			GD.Print("im here");

			triggeredCollision.Disabled = true;
			attackCollision.Disabled = true;
			if (heavyOrLight <= 2)
			{
				Player.SpeedScale = 1;
				Player.Play("heavy");	
				
				AnimationPlaying = true;
			}
			else
			{
				Player.Play("light");
				AnimationPlaying = true;
			}
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


	/// <summary>
	/// Damage es la cantidad de daño, y knockback cuanto se empuja al recibirlo
	/// </summary>
	/// <param name="damage"></param>
	/// <param name="knockback"></param>

	private void Take_Damage(int damage, Vector2 knockback)
	{
		GD.Print("supposedly damaged");
		
		
		TriggerVibration();
		GD.Print("he took damage");
		_velocity = knockback;
		var newPosition = Position;
		newPosition.X += 20;
		Position = newPosition;
		MoveAndCollide(_velocity);
		Healthbar.Health -= damage;
		damageNoise.Play();
		Player.Play("onhit");
		WasDamaged = true;
		
		
		
	}

	/// <summary>
	/// Causa que el dispositivo movil vibre
	/// </summary>
	public void TriggerVibration()
	{
		Input.VibrateHandheld(1000);
	}


	/// <summary>
	/// 
	/// </summary>
	/// <param name="triggered"></param>
	/// <returns></returns>
	protected bool EnemyHasBeen(bool triggered)
	{
		if (!triggered)
			return false;
		return true;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="area"></param>
	private void _on_area_detection_area_entered(Area2D area)
	{
		HasBeenTriggered = EnemyHasBeen(true);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="area"></param>
	private void _on_area_detection_area_exited(Area2D area)
	{
		HasBeenTriggered = EnemyHasBeen(false);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="area"></param>
	private void on_area_attack_area_entered(Area2D area)
	{
		_closeEnough = true;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="area"></param>
	private void on_area_attack_area_exited(Area2D area)
	{
		_closeEnough = false;
	}
	/// <summary>
	/// 
	/// </summary>
	/// <param name="anim"></param>
	public void _on_animation_player_animation_finished(string anim)
	{
		triggeredCollision.Disabled = false;
		Player.SpeedScale = 2;
		Speed = 350f;
		LightHitbox.Disabled = true;
		HeavyHitbox.Disabled = true;
		WasDamaged = false;
		AnimationPlaying = false;
		attackCollision.Disabled = false;		
	}
}




