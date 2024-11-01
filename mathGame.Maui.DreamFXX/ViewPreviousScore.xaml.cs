using mathGame.Maui.Models;
using System;

namespace mathGame.Maui;

public partial class ViewPreviousScore : ContentPage
{
	public ViewPreviousScore()
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