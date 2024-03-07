using System;
using Godot;

namespace project_attempt.Scripts;

public partial class Healthbar : ProgressBar
{
	private Timer _healthTimer;
	private ProgressBar _damageBar;
	private float _health;
	
	public float Health 
	{ 
		get => _health;
		set
		{
			var previousHealth = _health;
			_health = (float)Mathf.Clamp(value, 0f, Double.MaxValue);
			this.Value = _health;
			
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
	
	
	/// <summary>
	/// El main
	/// </summary>
	public override void _Ready()
	{
		
		_healthTimer = (Timer)GetNode("HealthbarTimer");
		_damageBar = GetNode<ProgressBar>("DamageBar");
	}

	/// <summary>
	/// Inicializa la vida del personaje
	/// </summary>
	/// <param name="health"></param>
	public void initialize_health(float health){
		
		this.Health = health;
		this.MaxValue = this.Health;
		this.Value = this.Health;
		this._damageBar.MaxValue = this.Health;
		this._damageBar.Value = this.Health;
	}


	/// <summary>
	/// Evento que causa que la barra de daño sea igual que la barra de vida
	/// </summary>
	private void _on_healthbar_timer_timeout()
	{
		this._damageBar.Value = this.Health;
	}
	
}
