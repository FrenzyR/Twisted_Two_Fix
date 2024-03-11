using Godot;
using System;

public partial class credits : Node2D
{
	private bool Language;

	private Label hpLabel;

	private Label attacksLabel;
	// Called when the node enters the scene tree for the first time.
	/// <summary>
	/// Main
	/// </summary>
	public override void _Ready()
	{
		music = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
		music.Playing = settings.Music;
		var healthbarStringEn = "If your HP falls below zero, you lose, \nif theirs does, you win, simple as that";
		var healthbarStringEs = "Si tu vida baja de cero, pierdes,\n si la suya lo hace, ganas, así de simple";

		var attacksStringEn =
			"The Cross serves as the light attack button,\nthe square as the heavy attack button, and\nthe triangle represents the special attack";
		var attacksStringEs =
			"La Cruz sirve de ataque ligero,\n el cuadrado es el ataque fuerte, \n y el triangulo el ataque especial";

		hpLabel = GetNode<Label>("Healthbar/Label2");
		attacksLabel = GetNode<Label>("Node2D/Label2");

		if (Language != settings.GetIsLanguageSpanish())
		{
			hpLabel.Text = healthbarStringEs;
			attacksLabel.Text = attacksStringEs;
		}
		else
		{
			hpLabel.Text = healthbarStringEn;
			attacksLabel.Text = attacksStringEn;
		}
	}

	private PackedScene mainMenuPack;
	private AudioStreamPlayer2D music;
	/// <summary>
	/// Te da la opcion cada frame de volver al menu
	/// </summary>
	/// <param name="delta"></param>
	public override void _Process(double delta)
	{
		
		Timer timer = GetNode<Timer>("Timer");
		
		PackedScene mainMenuScene = (PackedScene)GD.Load("Scenes/main_menu.tscn");
		Node mainMenuInstance = mainMenuScene.Instantiate();
		mainMenuPack = new PackedScene();
		mainMenuPack.Pack(mainMenuInstance);
		var selectClick = GetNode<AudioStreamPlayer2D>("SelectClick");
		
		if (Input.GetActionStrength("main_menu") != 0)
		{
			if (!selectClick.Playing)
			{
				selectClick.Play();	
			}
			timer.Start();
		}
	}

	/// <summary>
	/// Al llegar a 0 te lleva al menu
	/// </summary>
	private void on_timer_timeout()
	{
		GetTree().ChangeSceneToPacked(mainMenuPack);
	}
}
