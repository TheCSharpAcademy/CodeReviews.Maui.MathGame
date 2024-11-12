namespace MauiMathGameAhmadJer99;

public partial class DifficultyPage : ContentPage
{
    public DifficultyPage()
    {
        InitializeComponent();
        SetChosenDifficultyToGreen();
    }
    private async void OnBackToMenu(Object sender, EventArgs e)
    {
        try
        {
            await Navigation.PopAsync(); // Use PopAsync to avoid creating new instances
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        };
    }
    private void OnDifficultyChosen(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        GameSettings.DifficultyLevel = button.Text.ToLower();

        SetChosenDifficultyToGreen();
    }

    private void ResetUnchosenDifficultyToRed()
    {
        EasyButton.BackgroundColor = Color.Parse("DarkRed");
        MediumButton.BackgroundColor = Color.Parse("DarkRed");
        HardButton.BackgroundColor = Color.Parse("DarkRed");
    }

    private void SetChosenDifficultyToGreen()
    {
        ResetUnchosenDifficultyToRed();

        switch (GameSettings.DifficultyLevel)
        {
            case "easy":
                EasyButton.BackgroundColor = Color.Parse("ForestGreen");
                EasyButton.Text = "Easy";
                break;
            case "medium":
                MediumButton.BackgroundColor = Color.Parse("ForestGreen");
                MediumButton.Text = "Medium";
                break;
            case "hard":
                HardButton.BackgroundColor = Color.Parse("ForestGreen");
                HardButton.Text = "Hard";
                break;
            default:
                throw new NotImplementedException();
        }
    }
}