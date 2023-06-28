using MathsMAUI.Data;

namespace MathsMAUI;

public partial class App : Application
{
    public static GameRepository GameRepository { get; private set; }
    public App(GameRepository gameRepository)
    {
        InitializeComponent();

        MainPage = new AppShell();

        GameRepository = gameRepository;
    }
}
