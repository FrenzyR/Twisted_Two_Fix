using Godot;

namespace project_attempt.Scripts;

public partial class MainMenu : Node2D
{

	/// <summary>
	/// El main de cada clase
	/// </summary>
	public override void _Ready()
	{
		music = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
		music.Playing = settings.Music;
		var versusEn = GetNode < Sprite2D > ("MenuBackground/versusEn");
		var versusEs = GetNode < Sprite2D > ("MenuBackground/versusEs");
		if (Language == settings.GetIsLanguageSpanish())
		{
			versusEs.Visible = false;
			versusEn.Visible = true;

		}
		else
		{
			versusEn.Visible = false;
			versusEs.Visible = true;
		}
	}

	private bool Language;
	private PackedScene mapSelectPack;
	private PackedScene settingsSelectPack;
	private PackedScene creditsSelectPack;
	private int selectVar;
	private AudioStreamPlayer2D music;

	/// <summary>
	/// Un método que se ejecuta cada frame
	/// </summary>
	/// <param name="delta"></param>
	public override void _Process(double delta)
	{
		Timer timer = GetNode<Timer>("Timer");
		
		PackedScene mapSelectScene = (PackedScene)GD.Load("Scenes/map_selection.tscn");
		Node mapSelectInstance = mapSelectScene.Instantiate();
		mapSelectPack = new PackedScene();
		mapSelectPack.Pack(mapSelectInstance);

		PackedScene settingsSelectScene = (PackedScene)GD.Load("Scenes/settings.tscn");
		Node settingsSelectInstance = settingsSelectScene.Instantiate();
		settingsSelectPack = new PackedScene();
		settingsSelectPack.Pack(settingsSelectInstance);
		
		PackedScene creditsSelectScene = (PackedScene)GD.Load("Scenes/credits.tscn");
		Node creditsSelectInstance = creditsSelectScene.Instantiate();
		creditsSelectPack = new PackedScene();
		creditsSelectPack.Pack(creditsSelectInstance);
		
		var selectClick = GetNode<AudioStreamPlayer2D>("SelectClick");
		
		GetParent();
		if (Input.GetActionStrength("versus") != 0)
		{
			selectVar = 0;
			if (!selectClick.Playing)
			{
				selectClick.Play();	
			}
			
			timer.Start();

		}
		if (Input.GetActionStrength("settings") != 0)
		{
			if (!selectClick.Playing)
			{
				selectClick.Play();	
			}
			selectVar = 1;
			selectClick.Play();
			timer.Start();

		}
		if (Input.GetActionStrength("credits") != 0)
		{
			if (!selectClick.Playing)
			{
				selectClick.Play();	
			}
			selectVar = 2;
			selectClick.Play();
			timer.Start();
		}
	}
	/// <summary>
	/// Evento que se ejecuta cuando timer llega a 0
	/// </summary>
	private void _on_timer_timeout()
	{
		GD.Print("hi");
		if (selectVar == 0)
		{
			GetTree().ChangeSceneToPacked(mapSelectPack);
		}
		else if (selectVar == 1)
		{
			GetTree().ChangeSceneToPacked(settingsSelectPack);
		}
		else if (selectVar == 2)
		{
			GetTree().ChangeSceneToPacked(creditsSelectPack);
		}
	}
}
