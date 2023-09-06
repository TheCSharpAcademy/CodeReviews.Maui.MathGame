using Windows.Gaming.Preview.GamesEnumeration;
using MauiMathGame.Models;
using SQLite;

namespace MauiMathGame;

public partial class PreviousGames : ContentPage
{
    public PreviousGames()
    {
        InitializeComponent();
        App.GameRepository.GetAllGames();
        gamesList.ItemsSource = App.GameRepository.GetAllGames();
    }

    private void OnDelete(object sender, EventArgs e)
    {
        ImageButton button = (ImageButton)sender;

        App.GameRepository.Delete((int)button.BindingContext);

        gamesList.ItemsSource = App.GameRepository.GetAllGames();
    }
}