using MauiMathGame.Models;

namespace MauiMathGame;

public partial class MainPage : ContentPage
{

    int numberOfQuestions = 3;
    DifficultyLevel difficultyLevel;


    public MainPage()
    {
        InitializeComponent();
        NumOfQuestions.SelectedIndex = 0;
        numberOfQuestions = (int)NumOfQuestions.SelectedItem;
        Difficulty.SelectedIndex = 0;
        SetDifficulty(Difficulty.SelectedIndex);
        App.GameRepository.Init();

    }
    public MainPage(int questionPassBack, DifficultyLevel difficultyPassBack)
    {
        InitializeComponent();
        NumOfQuestions.SelectedItem = questionPassBack;
        numberOfQuestions = (int)NumOfQuestions.SelectedItem;
        difficultyLevel = difficultyPassBack;
        SetDifficulty(difficultyLevel);
        App.GameRepository.Init();

    }

    private void OnGameChosen(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        Navigation.PushAsync(new GamePage(button.Text, numberOfQuestions, difficultyLevel));
    }
    private void OnRandomGame(object sender, EventArgs e)
    {
        string gametext = "R";

        Navigation.PushAsync(new GamePage(gametext, numberOfQuestions, difficultyLevel));
    }

    private void OnViewPreviousGamesChosen(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PreviousGames());
    }

    private void NumberOfGames_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (NumOfQuestions.SelectedIndex != -1)
        {
            
            numberOfQuestions = (int)((Picker)sender).SelectedItem;

            
        }
    }
    private void Difficulty_SelectedIndexChanged(Object sender, EventArgs e)
    {
        SetDifficulty(Difficulty.SelectedIndex);
    }

    private void SetDifficulty(int selected)
    {
        switch(selected)
        {
            case 0:
                difficultyLevel = DifficultyLevel.Easy;
                break; 
            case 1:
                difficultyLevel = DifficultyLevel.Medium;
                break;
            case 2:
                difficultyLevel = DifficultyLevel.Hard;
                break;
        }
    }
    private void SetDifficulty(DifficultyLevel difficultyLevel)
    {
        switch (difficultyLevel)
        {
            case DifficultyLevel.Easy:
                Difficulty.SelectedIndex = 0;
                break;
            case DifficultyLevel.Medium:
                Difficulty.SelectedIndex = 1;
                break;
            case DifficultyLevel.Hard:
                Difficulty.SelectedIndex = 3;
                break;
        }
    }

}





