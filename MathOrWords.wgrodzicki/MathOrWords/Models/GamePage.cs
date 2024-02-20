namespace MathOrWords.Models;

public class GamePage
{
    /// <summary>
    /// Ends the game, saves the score and displays the game over screen.
    /// </summary>
    public static async void GameOver(int score, GameMode mode, VerticalStackLayout questionArea, Label gameOverLabel, Label incorrectCounterLabel, Label scoreLabel, Button returnButton, string variant = "")
    {
        await Task.Delay(2000);

        App.GameRepository.AddGame(new Game
        {
            Score = score,
            Date = DateTime.Now,
            GameMode = mode,
            MathVariant = variant
        });

        questionArea.IsVisible = false;
        gameOverLabel.IsVisible = true;
        incorrectCounterLabel.IsVisible = false;
        scoreLabel.IsVisible = false;
        gameOverLabel.Text = $"Game over! Your score is: {score}";
        returnButton.IsVisible = true;
    }


    /// <summary>
    /// Displays the main page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public static async void ReturnToMainPage()
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}
