using MauiMathGame.Models;

namespace MauiMathGame;

public partial class GamePage : ContentPage
{
	public string GameType { get; set; }
	int firstNumber = 0;
	int secondNumber = 0;
	int score = 0;
	int totalQuestions = 0; 
	int gamesLeft = 0;
	DifficultyLevel difficultyLevel = DifficultyLevel.Easy;
	bool randomGame = false;

	public GamePage(string gameType,int questions,DifficultyLevel difficulty)
	{
		InitializeComponent();
		GameType = gameType;
		totalQuestions = questions;
		gamesLeft = totalQuestions;
		BindingContext = this;
		difficultyLevel = difficulty;
        if (GameType == "R")
            {
                randomGame = true;

            }
        CreateNewQuestion();
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Delay for a short period to allow the page to fully load
        await Task.Delay(100);

        // Set focus to the Entry and set the cursor position
        AnswerEntry.Focus();
        AnswerEntry.CursorPosition = 0;
    }

    private void CreateNewQuestion()
	{

        var random = new Random();

		if (randomGame)
		{
			int randomGameType = random.Next(0,3);
			
			switch (randomGameType) 
			{
			case 0: GameType = "+"; break;
			case 1: GameType = "-"; break;
			case 2: GameType = "×"; break;
			case 3: GameType = "÷"; break;
			}
		}

        switch (GameType)
		{
			case "+":
				switch (difficultyLevel)
				{
					case DifficultyLevel.Easy:
						firstNumber = random.Next(1, 9);
						secondNumber = random.Next(1, 9);
						break;
					case DifficultyLevel.Medium:
						firstNumber = random.Next(1, 20);
						secondNumber = random.Next(1, 20);
						break;
					case DifficultyLevel.Hard:
						firstNumber = random.Next(1, 100);
						secondNumber = random.Next(1, 100);
						break;
				}
				break;
			case "-":
				switch (difficultyLevel)
				{
					case DifficultyLevel.Easy:
						while (firstNumber < secondNumber)
						{
							firstNumber = random.Next(1, 9);
							secondNumber = random.Next(1, 9);
						}
						break;
					case DifficultyLevel.Medium:
						while (firstNumber < secondNumber)
						{
							firstNumber = random.Next(1, 99);
							secondNumber = random.Next(1, 99);
						}
						break;
					case DifficultyLevel.Hard:
						firstNumber = random.Next(1, 99);
						secondNumber = random.Next(1, 99);
						break;
				}
				break;
			case "×":
				switch (difficultyLevel)
				{
					case DifficultyLevel.Easy:
						firstNumber = random.Next(1, 5);
						secondNumber = random.Next(1, 10);
						break;
					case DifficultyLevel.Medium:
						firstNumber = random.Next(1, 10);
						secondNumber = random.Next(1, 10);
						break;
					case DifficultyLevel.Hard:
						firstNumber = random.Next(1, 100);
						secondNumber = random.Next(1, 100);
						break;
				}
				break;
			case "÷":
				switch (difficultyLevel)
				{
					case DifficultyLevel.Easy:

						do
						{
							firstNumber = random.Next(1, 20);
							secondNumber = random.Next(1, 20);
						} while (firstNumber < secondNumber || firstNumber % secondNumber != 0);
						break;
					case DifficultyLevel.Medium:

						do
						{
							firstNumber = random.Next(1, 100);
							secondNumber = random.Next(1, 100);
						} while (firstNumber < secondNumber || firstNumber % secondNumber != 0 || firstNumber % secondNumber! > 1);
						break;
					case DifficultyLevel.Hard:

						do
						{
							firstNumber = random.Next(1, 1000);
							secondNumber = random.Next(1, 1000);
						} while (firstNumber < secondNumber || firstNumber % secondNumber != 0 || firstNumber % secondNumber! > 4);
						break;
				}
				break;
		}

		QuestionLabel.Text = $"{firstNumber} {GameType} {secondNumber}";
		OnAppearing();


    }

	private void OnAnswerSubmitted(object sender, EventArgs e)
	{
        OnAnswer(AnswerEntry.Text);
    }
    private void OnAnswerEntered(object sender, EventArgs e)
    {
		OnAnswer(AnswerEntry.Text);  
    }

	private void OnAnswer(string entry)
	{
        if (int.TryParse(entry, out int answer))
        {
            var isCorrect = false;

            switch (GameType)
            {
                case "+":
                    isCorrect = answer == firstNumber + secondNumber;
                    break;
                case "-":
                    isCorrect = answer == firstNumber - secondNumber;
                    break;
                case "×":
                    isCorrect = answer == firstNumber * secondNumber;
                    break;
                case "÷":
                    isCorrect = answer == firstNumber / secondNumber;
                    break;
            }

            ProcessAnswer(isCorrect);
            gamesLeft--;
            AnswerEntry.Text = "";
            if (gamesLeft > 0)
            {
                CreateNewQuestion();
            }
            else
                GameOver();
        }
        else
        {
            InvalidAnswer();
        }

    }

    private void GameOver()
    {
		if (randomGame)
		{
			GameType = "R";
		}
		GameOperation gameOperation = GameType switch
		{
			"+" => GameOperation.Addition,
			"-" => GameOperation.Subtraction,
            "×" => GameOperation.Multiplication,
            "÷" => GameOperation.Division,
			"R" => GameOperation.Random,

		};

		QuestionArea.IsVisible = false;
		BackToMenuBtn.IsVisible = true;
        GameOverLabel.Text = $"Game over! You got {score} out of {totalQuestions} right.";

		App.GameRepository.Add(new Game
		{
			DatePlayed = DateTime.Now,
			Type = gameOperation,
			Score = score,
			TotalQuestions = totalQuestions,
            Difficulty = difficultyLevel,
		});

	}

    private void ProcessAnswer(bool isCorrect)
	{
		if (isCorrect)
			score++;

        AnswerLabel.Text = isCorrect ? "Correct" : "Incorrect";
	}

	private void InvalidAnswer()
	{
		AnswerLabel.Text = "Invalid Awnser";
	}



    private void OnBackToMenu(object sender,EventArgs e)
	{
		Navigation.PushAsync(new MainPage(totalQuestions, difficultyLevel));
	}
}