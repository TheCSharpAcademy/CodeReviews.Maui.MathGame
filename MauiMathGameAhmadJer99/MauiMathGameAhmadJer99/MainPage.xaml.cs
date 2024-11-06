namespace MauiMathGameAhmadJer99
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnGameChosen(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string chosenGameType = button.Text;

            Navigation.PushAsync(new GamePage(chosenGameType));
        }
        private void OnRandomizerChosen(object sender, EventArgs e)
        {
            Random randomGame = new();

            string randomGameType = randomGame.Next(0, 4) switch
            {
                0 => "+",
                1 => "-",
                2 => "*",
                3 => "/",
                _ => throw new NotImplementedException()
            };

            Navigation.PushAsync(new GamePage(randomGameType));
        }
        private void OnDifficultyChosen(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DifficultyPage());
        }
        private void OnPreviousGamesChosen(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PreviousGamesPage());
        }
    }

}
