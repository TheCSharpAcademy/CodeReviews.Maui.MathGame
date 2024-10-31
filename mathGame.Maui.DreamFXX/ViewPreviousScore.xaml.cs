using mathGame.Maui.Models;
using System;

namespace mathGame.Maui;

public partial class ViewPreviousScore : ContentPage
{
	public ViewPreviousScore()
	{
		InitializeComponent();
		App.GameRepository.GetAllGames(); //Game.cs

		//gamesList - created scrollview on the previous html file.
		gamesList.ItemsSource = App.GameRepository.GetAllGames();
	}

	private void OnDelete(object sender, EventArgs e)
	{
		ImageButton button = (ImageButton)sender;

		App.GameRepository.Delete((int)button.BindingContext); // int, pøeveden BindingContext na èíslo (id)

		gamesList.ItemsSource = App.GameRepository.GetAllGames(); // refresh Data after deleting a record.
	}
}