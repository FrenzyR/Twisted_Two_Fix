using System;
using Godot;

namespace project_attempt.Scripts;

public partial class Healthbar : ProgressBar
{
	private Timer _timer;
	private ProgressBar _damageBar;
	private float _health;
	public float Health 
	{ 
		get => _health;
		set
		{
			var previousHealth = _health;
			_health = (float)Mathf.Clamp(value, 0f, Double.MaxValue);
			Value = _health;
				
			if(_health <= 0)
			{
				QueueFree();
			}
				
			if(_health < previousHealth)
			{
				_timer.Start();
			}
			else
			{
				_damageBar.Value = _health;
			}
		} 
	}
	
	
	public override void _Ready()
	{
		
		this._timer = GetNode<Timer>("HealthbarTimer");
		this._damageBar = GetNode<ProgressBar>("DamageBar");
	}

	public void initialize_health(float health){
		
		this.Health = health;
		this.MaxValue = this.Health;
		this.Value = this.Health;
		this._damageBar.MaxValue = this.Health;
		this._damageBar.Value = this.Health;
	}

	
	public override void _Process(double delta)
	{
	}
	
	private void _on_healthbar_timer_timeout()
	{
		this._damageBar.Value = this.Health;
	}
	
}
