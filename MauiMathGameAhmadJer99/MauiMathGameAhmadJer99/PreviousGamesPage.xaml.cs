namespace MauiMathGameAhmadJer99;
using MauiMathGameAhmadJer99.Models;

public partial class PreviousGamesPage : ContentPage
{
	public PreviousGamesPage()
    {
        InitializeComponent();

        gamesList.ItemsSource = App.GameRepository.GetAllGames();
    }
    private void OnDelete(Object sender,EventArgs e)
    {
        Button button = (Button)sender;

        App.GameRepository.RemoveGame((int)button.BindingContext);

        gamesList.ItemsSource = App.GameRepository.GetAllGames();
    }
}