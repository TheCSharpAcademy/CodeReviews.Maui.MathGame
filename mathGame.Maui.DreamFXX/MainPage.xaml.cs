namespace mathGame.Maui;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnGameChosen(object sender, EventArgs e)
    { 
        Button button = (Button)sender;

        Navigation.PushAsync(new GamePage(button.Text));
    }

    private void OnViewPreviousScore(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ViewPreviousScore());
    }

}
