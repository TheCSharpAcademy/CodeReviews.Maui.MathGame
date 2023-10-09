namespace MathGame.wkktoria;

public partial class MainPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnGameChosen(object sender, EventArgs e)
    {
        var button = (Button)sender;

        Navigation.PushAsync(new GamePage(button.Text));
    }

    private void OnViewPreviousGamesChosen(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PreviousGamesPage());
    }
}