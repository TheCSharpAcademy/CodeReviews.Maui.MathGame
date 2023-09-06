using MathGameMauiJMS.Models;
using System.Diagnostics;

namespace MathGameMauiJMS;

public partial class GamePage : ContentPage
{
	public string GameMode { get; set; }
    public string operandSymbol { get; set; }
	public int difficultyValue { get; set; }
    public int totalOfGames { get; set; }
    public Button buttonD { get; set; }
    public Stopwatch stopwatch { get; set; }
    int firstNumber = 0;
	int secondNumber = 0;
	int score = 0;

    public GamePage(string gameMode)
	{
		InitializeComponent();
		GameMode = gameMode;
		BindingContext = this;
        stopwatch = new Stopwatch();
    }

	public void GenerateOperandSymbol()
	{
		if(GameMode == "Random")
		{
			var random = new Random();
            
			operandSymbol =  random.Next(1,5) switch
            {
                1 => "+",
                2 => "-",
                3 => "*",
                4 => "/",
                _ => "",
            };
        }
		else 
		{
            operandSymbol = GameMode switch
            {
                "Addition" => "+",
                "Subtraction" => "-",
                "Multiplication" => "*",
                "Division" => "/",
                _ => "",
            };
        }
	}

    private void GenerateQuestion()
	{
		GenerateOperandSymbol();
        var random = new Random();

		firstNumber = operandSymbol != "/" ? random.Next(1, 9 * difficultyValue) : random.Next(1, 99 * difficultyValue);
		secondNumber = operandSymbol != "/" ? random.Next(1, 9 * difficultyValue) : random.Next(1, 99 * difficultyValue);

		if(operandSymbol == "/")
		{
			while(firstNumber < secondNumber || firstNumber % secondNumber !=0)
			{
                firstNumber = random.Next(1, 99 * difficultyValue);
                secondNumber = random.Next(1, 99 * difficultyValue);
            }
		}
		QuestionGenerated.Text = $"{firstNumber} {operandSymbol} {secondNumber}";
    }

	private void DifficultySelector(object sender, EventArgs e)
	{
        buttonD = (Button)sender;
		difficultyValue = buttonD.Text switch
		{
			"Easy" => 1,
			"Intermediate" => 2,
			"Hard" => 3,
		};

		DifficultySelectionMenu.IsVisible = false;
        NumberOfGames.IsVisible = true;
    }

    private void SetTotalGames(object sender, EventArgs e)
    {
        int input = 0;
        bool isValidInput = Int32.TryParse(NumberOfGamesInput.Text, out input);

        if (isValidInput && input >= 1 && input <= 20)
        {
            totalOfGames = input;
            NumberOfGames.IsVisible = false;
            QuestionArea.IsVisible = true;
            GenerateQuestion();
            stopwatch.Start();
        }
        else
        {
            NumberOfGamesInvalidInput.Text = "Invalid Input! Please input a number between 1 and 20!";
            NumberOfGamesInvalidInput.IsVisible = true;
        }
    }

    private void OnPlayerAnswerSubmit(object sender, EventArgs e)
	{
        bool validInput = Int32.TryParse(PlayerAnswerInput.Text, out int input);
  
        if (validInput)
        {
            InvalidInput.IsVisible = false;
            var answer = Int32.Parse(PlayerAnswerInput.Text);
            var correctAnswer = false;

            switch (operandSymbol)
            {
                case "+":
                    correctAnswer = answer == firstNumber + secondNumber;
                    ProcessAnswer(correctAnswer);
                    break;
                case "-":
                    correctAnswer = answer == firstNumber - secondNumber;
                    ProcessAnswer(correctAnswer);
                    break;
                case "*":
                    correctAnswer = answer == firstNumber * secondNumber;
                    ProcessAnswer(correctAnswer);
                    break;
                case "/":
                    correctAnswer = answer == firstNumber / secondNumber;
                    ProcessAnswer(correctAnswer);
                    break;
            }

            totalOfGames--;
            PlayerAnswerInput.Text = "";

            if (totalOfGames > 0)
            {
                GenerateQuestion();
            }
            else
            {
                GameOver();
            }
        }
        else
        {
            InvalidInput.Text = "Invalid Input! Please write a number!";
        }
    }

    private void ProcessAnswer(bool correctAnswer)
    {
		score = correctAnswer ? score += difficultyValue : score;
		AnswerLabel.Text = correctAnswer ? "Correct!" : "Incorrect!";
    }

	private void GameOver()
    {
        stopwatch.Stop();
        GameType gameType;

		if(GameMode == "Random")
		{
			gameType = GameType.Random;
		}
		else
		{
            gameType = operandSymbol switch
            {
                "+" => GameType.Addition,
                "-" => GameType.Subtraction,
                "×" => GameType.Multiplication,
                "÷" => GameType.Division,
            };
        }

        GameOverLabel.Text = $"Game Over! You have scored {score} points!";
        QuestionArea.IsVisible = false;
        BackToMenuButton.IsVisible = true;

        App.GameRepository.Add(new Game
        {
            DatePlayed = DateTime.Now,
            Type = gameType,
            Score = score,
            Difficulty = buttonD.Text,
            TimePlayed = (int)stopwatch.Elapsed.TotalSeconds,
        });
	}

	private void OnBackToMenu(object sender, EventArgs e)
	{
		Navigation.PushAsync(new MainPage());
	}
}