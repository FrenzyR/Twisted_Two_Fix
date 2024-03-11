using Godot;
using System;
using System.IO;
using System.Text;
using project_attempt.Scripts;
using FileAccess = Godot.FileAccess;

public partial class map : Node2D
{
	private Node charInstancer;
	private PackedScene tohuPackedLoader = (PackedScene)GD.Load("Scenes/player_tohu.tscn");
	private PackedScene seveneePackedLoader = (PackedScene)GD.Load("Scenes/player_sevenee.tscn");
	private PackedScene mainMenuPackedLoader = (PackedScene)GD.Load("res://Scenes/main_menu.tscn");
	private CharacterBody2D tohuEnemy;
	private CharacterBody2D seveneeEnemy;
	private Label timeLabel;
	private Label winLabel;
	private int time;
	private CharacterBody2D character;
	private AudioStreamPlayer2D music;
	private PackedScene packedScene;
	private AudioStreamPlayer deathSound;
	private Timer deathTimer;
	private Timer timer;

	/// <summary>
	/// El main, se encarga de el mapa que se visualiza a la vez que los personajes que se utilizan
	/// </summary>
	public override void _Ready()
	{
		deathTimer = GetNode<Timer>("OnDeath/DeathTimer");
		deathSound = GetNode<AudioStreamPlayer>("OnDeath");	
		timeLabel = GetNode<Label>("timeLabel");
		winLabel = GetNode<Label>("winLabel");
		music = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
		music.Playing = settings.Music;
		

		tohuEnemy = GetNode<CharacterBody2D>("Tohu");
		seveneeEnemy = GetNode<CharacterBody2D>("Sevenee");
		Sprite2D map;
		if (MapSelection.selectedMap)
		{
			map = GetNode<Sprite2D>("MapTwo");
			map.Visible = false;
		}
		else if (!MapSelection.selectedMap)
		{
			map = GetNode<Sprite2D>("MapOne");
			map.Visible = false;
		}
		
		if (CharacterSelect.SelectedChar)
		{
			charInstancer = tohuPackedLoader.Instantiate();
			RemoveChild(tohuEnemy);
		}
		else if (!CharacterSelect.SelectedChar)
		{
			charInstancer = seveneePackedLoader.Instantiate();
			RemoveChild(seveneeEnemy);
		}
		AddChild(charInstancer);
		
		character = (CharacterBody2D)charInstancer;
		character.Scale = new Vector2(2, 2);
		

		
	}

	/// <summary>
	/// Comportamiento del final de la partida
	/// </summary>
	/// <param name="delta"></param>
	public override void _Process(double delta)
	{
		packedScene = new PackedScene();
		Node sceneInstance = mainMenuPackedLoader.Instantiate();
		packedScene.Pack(sceneInstance);
		time = int.Parse(timeLabel.Text);	
		timer = GetNode<Timer>("Timer");
		
		if (((PlayableCharacter)character).Healthbar.Health <= 0)
		{
			if (!deathSound.Playing)
			{
				deathSound.Play();	
				deathSound.Dispose();
			}
			SetLeftTimeOnFile();
			((EnemyCharacter)tohuEnemy).Speed = 0f;
			((EnemyCharacter)seveneeEnemy).Speed = 0f;
		
			((EnemyCharacter)tohuEnemy).Dispose();
			((EnemyCharacter)seveneeEnemy).Dispose();
			character.Dispose();
			winLabel.Text = "CPU!";
			timer.Stop();
			if (timer.IsStopped())
			{
				deathTimer.Start();	
			}
			
		}
		else if 
			(((EnemyCharacter)tohuEnemy).Healthbar.Health <= 0 ||((EnemyCharacter)seveneeEnemy).Healthbar.Health <= 0)
		{
			if (!deathSound.Playing)
			{
				deathSound.Play();	
				deathSound.Dispose();
			}
			SetLeftTimeOnFile();
			winLabel.Text = "U WIN!";
			tohuEnemy.Dispose();
			seveneeEnemy.Dispose();
			((PlayableCharacter)character).Speed = 0f;
			timer.Stop();

			if (timer.IsStopped())
			{
				deathTimer.Start();	
			}
		}
		
	}

	/// <summary>
	/// Guarda el ultimo tiempo del usuario
	/// </summary>
	private void SetLeftTimeOnFile()
	{
		using var saveGame = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Write);
		var timeAsString = Json.Stringify(time);

		saveGame.StoreLine(timeAsString);
	}

	/// <summary>
	/// Evento que actualiza el tiempo restante
	/// </summary>
	private void _on_timer_timeout()
	{
		
		
		if (timeLabel.Text == 0.ToString())
		{
			GetTree().ChangeSceneToPacked(packedScene);
		}	
		timeLabel.Text =(time-1)  + "";
	}

	private void _on_death_timer_timeout()
	{
		GetTree().ChangeSceneToPacked(packedScene);
	}
}
