using mathGame.Maui.Models;
using Windows.Media.AppBroadcasting;

namespace mathGame.Maui;

public partial class GamePage : ContentPage
{
	public string GameType { get; set; }
	int firstNumber = 0;
	int secondNumber = 0;
	int score = 0;

	const int totalQuestions = 6;
	int gamesLeft = totalQuestions;

	public GamePage(string gameType)
	{
		InitializeComponent();
		GameType = gameType;
		BindingContext = this;

		CreateNewQuestion();

	}

	private void CreateNewQuestion()

	{ 
		// - Všechno ostatní muzu definovat _
		

        var random = new Random();

		//if (GameType != "Dìlení")
		//{
		//	firstNumber = random.Next(1, 9);
		//	secondNumber = random.Next(1, 9);
		//}
		//else
		//{
		//    firstNumber = random.Next(1, 99);
		//    secondNumber = random.Next(1, 99);
		//}

		// Zkrácená verze zápisu IF a ELSE, 1-9 pokud není dìlení, pokud je tak do 99.
		firstNumber = GameType != "Dìlení" ? random.Next(1, 9) : random.Next(0, 99);
        secondNumber = GameType != "Dìlení" ? random.Next(1, 9) : random.Next(0, 99);

		if (GameType == "Dìlení") // Pouze pøíklady bez zbytku, zlomkù, také nemùžu dìlit menší vìtším (OR).
		{
			while (firstNumber < secondNumber || firstNumber % secondNumber != 0)
			{
                firstNumber = random.Next(1, 99);
                secondNumber = random.Next(1, 99);
            }
		}

		QuestionLabel.Text = $"{firstNumber} {GameType} {secondNumber}";

    }

    private void OnAnswerSubmitted(object sender, EventArgs e)
    {
		var answer = Int32.Parse(AnswerEntry.Text);
		var isCorrect = false;

		switch (GameType)
		{
			case "+":
				isCorrect = answer == firstNumber + secondNumber;
                ProcessAnswer(isCorrect);
                break;
			case "-":
				isCorrect = answer == firstNumber - secondNumber;
                ProcessAnswer(isCorrect);
                break;
			case "x":
				isCorrect = answer == secondNumber * firstNumber;
                ProcessAnswer(isCorrect);
                break;
			case "/":
				isCorrect = answer == firstNumber / secondNumber;
                ProcessAnswer(isCorrect);
                break;
        }

        gamesLeft--;
        AnswerEntry.Text = "";

		if (gamesLeft > 0)
			CreateNewQuestion();
		else
			GameOver();

	}

    private void ProcessAnswer(bool isCorrect)
    {
		 // isCorrect == true; ?? try., jako IF Statement.
		
			score = isCorrect ? score += 1 : score; //Pokud je spravne, + bod, pokud ne tak pouze pøevezme poèet bodu.

			AnswerLabel.Text = isCorrect ? "Správnì!" : "Špatnì."; // Pokud je pravda (true) tak platí vlevo, pokud ne tak vpravo.
    }

	private void GameOver()
	{
		// GameOperation je již definován jako enumerace - typy herních typù v souboru Game.cs
		GameOperation gameOperation = GameType switch
		{
		"+" => GameOperation.Sèítání,
		"-" => GameOperation.Odèítání,
		"x" => GameOperation.Násobení,
		"/" => GameOperation.Dìlení,
		};

		QuestionArea.IsVisible = false;
        BackToMenuBtn.IsVisible = true;

        GameOverLabel.Text = $"Cvièení skonèilo! Z této lekce jsi získal {score} bodù z celkových {totalQuestions}. To není špatné!";


		//SQL - Add a game record to the database.
		App.GameRepository.Add(new Game
		{
			DatePlayed = DateTime.Now,
			Type = gameOperation,
			Score = score,

		});
    }

	private void OnBackToMenu(object sender, EventArgs e)
	{
		score = 0;
		gamesLeft = totalQuestions;

		Navigation.PushAsync(new MainPage()); // Návrat do Menu, lehce a jednoduše!
	}
}