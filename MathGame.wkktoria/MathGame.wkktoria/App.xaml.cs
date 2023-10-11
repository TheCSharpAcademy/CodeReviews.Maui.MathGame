using MathGame.wkktoria.Data;

namespace MathGame.wkktoria;

public partial class App
{
    public App(GameRepository gameRepository)
    {
        InitializeComponent();

        MainPage = new AppShell();

        GameRepository = gameRepository;
    }

    public static GameRepository GameRepository { get; private set; }
}