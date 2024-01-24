using Godot;
using System;



public partial class healthbar : ProgressBar
{
	public Timer timer = null;
	public ProgressBar damageBar = null;
	private float _health;
	public float health 
	{ 
		get
			{
			 	return _health;	
			} 
		set
			{
				float previous_health = _health;
				_health = (float)Math.Min(this.MaxValue, health);
				this.Value = _health;
				
				if(_health <= 0)
				{
					QueueFree();
				}
				
				if(_health < previous_health)
				{
					this.timer.Start();
				}
				else
				{
					this.damageBar.Value = _health;
				}
			} 
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		this.timer = GetNode<Timer>("HealthbarTimer");
		this.damageBar = GetNode<ProgressBar>("DamageBar");
	}

	public void initialize_health(float _health){
		this.health = _health;
		this.MaxValue = this.health;
		this.Value = this.health;
		this.damageBar.MaxValue = this.health;
		this.damageBar.Value = this.health;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_healthbar_timer_timeout()
	{
		this.damageBar.Value = this.health;
	}
	
}



