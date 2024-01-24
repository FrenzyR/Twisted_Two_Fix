using Godot;
using System;



public partial class healthbar : ProgressBar
{
	public Timer timer = null;
	public ProgressBar damageBar = null;
	public float health 
	{ 
		get
			{
			 	return health;	
			} 
		set
			{
				float previous_health = health;
				
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
}
