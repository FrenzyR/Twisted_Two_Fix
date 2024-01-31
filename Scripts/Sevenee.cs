using System;
using Godot;

namespace project_attempt.Scripts;


public partial class Sevenee : Godot.CharacterBody2D
{
	//[Export] private NodePath _characterNodePath = null;
	private float _speed = 450.0f;
	public const float JumpVelocity = -400.0f;
	private float _health = 0;
	public Healthbar Healthbar = null;
	public Vector2 velocity;
	
	private bool _animationPlaying = false;
	private float knockback = 1.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	
	public override void _Ready()
	{
		
		this.Healthbar = GetNode<Healthbar>("CanvasLayer/Healthbar");
		this.Healthbar.initialize_health(100);
		
		
	}

	public override void _Process(double delta)
	{
		var player = GetNode<AnimatedSprite2D>("AnimatedSprite");
		//var hitbox = GetNode<CollisionShape2D>(_characterNodePath);
		AnimationPlayer animPlayer = new AnimationPlayer();
		
		
		player.FlipH = true;
		
		Node parentNode = GetParent();
		if (_animationPlaying == true)
		{
			return;
		}
		if (Input.GetActionStrength("ui_right") != 0)
		{
			player.Play("run");
			
			_animationPlaying = false;
			
		}
		else if(Input.GetActionStrength("ui_left") != 0)
		{
			player.Play("backward");
			
			_animationPlaying = false;
		}
		else if(Input.GetActionStrength("heavy") != 0)
		{
			player.Play("heavy");
			this.Healthbar.Health -= 0;
			_speed = 0.0f;
		}
		else if(Input.GetActionStrength("light") != 0)
		{
			player.Play("light");
			_speed = 0.0f;
		}
		
		else if(Input.GetActionStrength("special") != 0)
		{
			player.Play("special");
			//hitbox.Disabled = true;
			
			_speed = 0.0f;
			
		}
		else
		{
			player.Play("idle");	
			//hitbox.Disabled = true;
			_animationPlaying = false;

			
			_speed = 750.0f;
		}
		
		
		
	}




	public override void _PhysicsProcess(double delta)
	{
		this.velocity = Velocity;
		
		
		if (!IsOnFloor())
			velocity.Y += Gravity * (float)delta;

		
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * _speed * knockback;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, _speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private void _on_animated_sprite_animation_changed()
	{
		
		_animationPlaying = true;
		
	}


	private void _on_animated_sprite_animation_looped()
	{
		_animationPlaying = false;
	}

		private void _on_special_hitbox_child_entered_tree(Node node)
	{
		
		
	
	}


	private void _on_special_hitbox_child_exiting_tree(Node node)
	{
		
	}


}


