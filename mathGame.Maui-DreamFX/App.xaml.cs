using mathGame.Maui.Data;

namespace mathGame.Maui
{
    public partial class App : Application
    {
        // Injekction dependency - for whole app, App.cs
        public static GameRepository GameRepository { get; private set; }


        public App(GameRepository gameRepository)
        {
            InitializeComponent();

            MainPage = new AppShell();

            GameRepository = gameRepository;
        }
    }
}
