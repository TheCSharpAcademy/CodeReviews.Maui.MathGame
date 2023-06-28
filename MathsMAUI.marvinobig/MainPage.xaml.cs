namespace MathsMAUI;

public partial class MainPage : ContentPage
{
	public MainPage() { 
		InitializeComponent();
	}

	private async void Navigate(object sender, EventArgs e)
	{
		Button gameBtn = (Button)sender;

		switch (gameBtn.Text)
		{
			case "Addition":
			case "Subtraction":
            case "Multiplication":
            case "Division":
                await Navigation.PushAsync(new GamePage(gameBtn.Text));
                break;
			case "Game History":
                await Navigation.PushAsync(new GameHistory(gameBtn.Text));
                break;
        }
	}
}

