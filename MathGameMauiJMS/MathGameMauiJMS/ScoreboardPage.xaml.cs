using MathGameMauiJMS.Models;
using System;

namespace MathGameMauiJMS;

public partial class ScoreboardPage : ContentPage
{
	public ScoreboardPage()
	{
		InitializeComponent();
        App.GameRepository.GetAllGames();
        gamesList.ItemsSource = App.GameRepository.GetAllGames();
	}

	public void OnDelete(object sender, EventArgs e)
	{
		ImageButton button = (ImageButton)sender;
		App.GameRepository.Delete((int)button.BindingContext);
        gamesList.ItemsSource = App.GameRepository.GetAllGames();
    }
}