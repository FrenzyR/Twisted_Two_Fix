using System;
using Godot;

namespace project_attempt.Scripts;

public partial class Healthbar : ProgressBar
{
	private Timer _healthTimer;
	private ProgressBar _damageBar;
	private float _health;
	private PackedScene _sceneLoaderScene = (PackedScene) GD.Load("res://Scenes/main_menu.tscn");
	public float Health 
	{ 
		get => _health;
		set
		{
			var previousHealth = _health;
			_health = (float)Mathf.Clamp(value, 0f, Double.MaxValue);
			this.Value = _health;
				
			if(_health <= 0)
			{
				var sceneInstance = _sceneLoaderScene.Instantiate();
				var packedScene = new PackedScene();
				packedScene.Pack(sceneInstance);
				QueueFree();
				GetTree().ChangeSceneToPacked(packedScene);
			}
				
			if(_health < previousHealth)
			{
				_healthTimer.Start();
			}
			else
			{
				_damageBar.Value = _health;
			}
		} 
	}
	
	
	public override void _Ready()
	{
		
		_healthTimer = (Timer)GetNode("HealthbarTimer");
		_damageBar = GetNode<ProgressBar>("DamageBar");
	}

	public void initialize_health(float health){
		
		this.Health = health;
		this.MaxValue = this.Health;
		this.Value = this.Health;
		this._damageBar.MaxValue = this.Health;
		this._damageBar.Value = this.Health;
	}


	
	private void _on_healthbar_timer_timeout()
	{
		this._damageBar.Value = this.Health;
	}
	
}
