using Godot;
using System;

public partial class settings : Node2D
{

	private PackedScene mainMenuPack;
	[Export] protected static internal bool Language = false;
	[Export] protected static internal bool Music = false;
	private Label scoreLabel;
	private CheckButton languageButton;
	private CheckButton musicButton;
	private AudioStreamPlayer2D music;
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
	public override void _Ready()
	{
		languageButton = GetNode<CheckButton>("LanguageButton");
		musicButton = GetNode<CheckButton>("MusicButton");
		Language = GetIsLanguageSpanish();
		Music = GetIsLMusicMuted();
		music = GetNode<AudioStreamPlayer2D>("Music");
		music.Playing = Music;


	}



	/// <summary>
	/// 
	/// </summary>
	/// <param name="delta"></param>
    public override void _Process(double delta)
	{
		
		
		Timer timer = GetNode<Timer>("Timer");
		scoreLabel = GetNode<Label>("TimeButton");
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

		
		if (Language)
		{
			languageButton.Text = "Lenguaje: Es";
			scoreLabel.Text = "Tu ultima puntuacion de tiempo: " + GetLeftTimeFromFile();
			musicButton.Text = "Musica: " + GetIsLMusicMuted();
		}
		else if (!Language)
		{
			languageButton.Text = "Language: En";
			musicButton.Text = "Music: " + GetIsLMusicMuted();
			scoreLabel.Text = "Your last timescore: " + GetLeftTimeFromFile();
		}

		
	}

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	private string GetLeftTimeFromFile()
	{
		if (!FileAccess.FileExists("user://savegame.save"))
		{
			return "00";
		}
		using var saveGame = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Read);
		return  saveGame.GetLine();
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="on"></param>
	private void _on_language_button_toggled(bool on)
	{
		if (on)
		{
			Language = true;	
		}
		else
		{
			Language = false;
		}
		SaveIsLanguageSpanish(Language);
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="isLanguageSpanish"></param>
	private void SaveIsLanguageSpanish(bool isLanguageSpanish)
	{
		using var saveGame = FileAccess.Open("user://language.settings.save", FileAccess.ModeFlags.Write);

		saveGame.StoreLine(isLanguageSpanish.ToString());
	}

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public static bool GetIsLanguageSpanish()
	{
		using var saveGame = FileAccess.Open("user://language.settings.save", FileAccess.ModeFlags.Read);

		return Convert.ToBoolean(saveGame.GetLine());
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public static bool GetIsLMusicMuted()
	{
		using var saveGame = FileAccess.Open("user://music.settings.save", FileAccess.ModeFlags.Read);

		return Convert.ToBoolean(saveGame.GetLine());
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="on"></param>
	private void _on_music_button_toggled(bool on)
	{
		if (on)
		{
			Music = true;	
		}
		else
		{
			Music = false;
		}
		SaveIsLMusicMuted(Music);
		
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="isMusicMuted"></param>
	private void SaveIsLMusicMuted(bool isMusicMuted)
	{
		using var saveGame = FileAccess.Open("user://music.settings.save", FileAccess.ModeFlags.Write);

		saveGame.StoreLine(isMusicMuted.ToString());
	}
	
	/// <summary>
	/// 
	/// </summary>
	private void on_timer_timeout()
	{
		GD.Print("hi");
		GetTree().ChangeSceneToPacked(mainMenuPack);
	}
	
}
