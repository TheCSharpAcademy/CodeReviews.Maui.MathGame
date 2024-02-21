using MathOrWords.Data;
using MathOrWords.Models;
using System.Linq;

namespace MathOrWords;

public partial class ScoresPage : ContentPage
{
	public ScoresPage()
	{
		InitializeComponent();
		BindingContext = this;

		PrintGames();
	}


	/// <summary>
	/// Prints all records stored in the database.
	/// </summary>
	private void PrintGames()
	{
		// Order records by date
		IEnumerable<Game> gamesToPrint = App.GameRepository.GetAllGames().OrderByDescending(x => x.Date);

        GamesList.ItemsSource = gamesToPrint;
    }


	/// <summary>
	/// Handles the deletion of a score.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void OnDeleteButtonChosen(object sender, EventArgs e)
	{
		ImageButton button = (ImageButton)sender;
		App.GameRepository.DeleteGame(Convert.ToInt32(button.BindingContext));

		// Refresh scores
		PrintGames();
	}
}