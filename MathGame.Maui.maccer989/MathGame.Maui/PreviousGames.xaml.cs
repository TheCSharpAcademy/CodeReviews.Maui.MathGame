namespace MathGame.Maui;

public partial class PreviousGames : ContentPage
{
	public PreviousGames()
	{
		InitializeComponent();
		gamesList.ItemsSource = App.GameRespository.GetAllGames();
	}

	private void OnDelete(object sender, EventArgs e)
	{ 
		ImageButton button = (ImageButton) sender;
		App.GameRespository.Delete((int)button.BindingContext);
		gamesList.ItemsSource = App.GameRespository.GetAllGames();
	}
}