using Godot;

namespace project_attempt.Scripts;


public partial class CharacterBody2D : Godot.CharacterBody2D
{
	private float _speed = 750.0f;
	public const float JumpVelocity = -400.0f;
	private float _health = 0;
	public Healthbar Healthbar = null;
	private bool _animationPlaying = false;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	
	public override void _Ready()
	{
		
		this.Healthbar = GetNode<Healthbar>("CanvasLayer/Healthbar");
		this.Healthbar.initialize_health(100);
		
	}

	public override void _Process(double delta)
	{
		var sceneLoaderScene = (PackedScene) GD.Load("res://Scenes/second_map.tscn");
		var sceneInstance = sceneLoaderScene.Instantiate();
		var player = GetNode<AnimatedSprite2D>("AnimatedSprite");
		AnimationPlayer animPlayer = new AnimationPlayer();
		
		var packedScene = new PackedScene();
		var result = packedScene.Pack(sceneInstance);
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
		else{
			
			
			player.Play("idle");	
			_animationPlaying = false;

			
			_speed = 750.0f;
		}
		//GD.Print("Entered");
		//GetTree().ChangeSceneToPacked(packedScene);
		//GD.Print("Exited");
	}


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		
		
		if (!IsOnFloor())
			velocity.Y += Gravity * (float)delta;


		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * _speed;
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
		GD.Print("anim changed");
		_animationPlaying = true;
	}


	private void _on_animated_sprite_animation_looped()
	{
		_animationPlaying = false;
	}

}
