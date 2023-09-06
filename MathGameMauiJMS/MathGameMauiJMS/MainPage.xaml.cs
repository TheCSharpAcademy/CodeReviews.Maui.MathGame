namespace MathGameMauiJMS;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	public void GameModeChosen(object sender, EventArgs e)
	{
		Button button = (Button)sender;
		Navigation.PushAsync(new GamePage(button.Text)); //GamePage needs to be created!!;
	}

    private void OnViewScoreboard(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ScoreboardPage());
    }
}

