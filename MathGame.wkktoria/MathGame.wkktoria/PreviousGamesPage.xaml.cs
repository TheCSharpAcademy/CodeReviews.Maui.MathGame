namespace MathGame.wkktoria;

public partial class PreviousGamesPage
{
    public PreviousGamesPage()
    {
        InitializeComponent();

        GamesList.ItemsSource = App.GameRepository.GetAllGames();
    }

    private void OnDelete(object sender, EventArgs e)
    {
        var button = (Button)sender;

        App.GameRepository.Delete((int)button.BindingContext);

        GamesList.ItemsSource = App.GameRepository.GetAllGames();
    }
}