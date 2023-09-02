using MathGame.Maui.Data;

namespace MathGame.Maui;

public partial class App : Application
{
	public static GameRespository GameRespository { get; private set; }
	public App(GameRespository gameRespository)
	{
		InitializeComponent();

		MainPage = new AppShell();

		GameRespository = gameRespository;
	}
}
