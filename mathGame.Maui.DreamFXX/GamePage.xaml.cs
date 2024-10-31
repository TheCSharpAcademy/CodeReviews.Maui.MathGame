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
		// - V�echno ostatn� muzu definovat _
		

        var random = new Random();

		//if (GameType != "D�len�")
		//{
		//	firstNumber = random.Next(1, 9);
		//	secondNumber = random.Next(1, 9);
		//}
		//else
		//{
		//    firstNumber = random.Next(1, 99);
		//    secondNumber = random.Next(1, 99);
		//}

		// Zkr�cen� verze z�pisu IF a ELSE, 1-9 pokud nen� d�len�, pokud je tak do 99.
		firstNumber = GameType != "D�len�" ? random.Next(1, 9) : random.Next(0, 99);
        secondNumber = GameType != "D�len�" ? random.Next(1, 9) : random.Next(0, 99);

		if (GameType == "D�len�") // Pouze p��klady bez zbytku, zlomk�, tak� nem��u d�lit men�� v�t��m (OR).
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
		
			score = isCorrect ? score += 1 : score; //Pokud je spravne, + bod, pokud ne tak pouze p�evezme po�et bodu.

			AnswerLabel.Text = isCorrect ? "Spr�vn�!" : "�patn�."; // Pokud je pravda (true) tak plat� vlevo, pokud ne tak vpravo.
    }

	private void GameOver()
	{
		// GameOperation je ji� definov�n jako enumerace - typy hern�ch typ� v souboru Game.cs
		GameOperation gameOperation = GameType switch
		{
		"+" => GameOperation.S��t�n�,
		"-" => GameOperation.Od��t�n�,
		"x" => GameOperation.N�soben�,
		"/" => GameOperation.D�len�,
		};

		QuestionArea.IsVisible = false;
        BackToMenuBtn.IsVisible = true;

        GameOverLabel.Text = $"Cvi�en� skon�ilo! Z t�to lekce jsi z�skal {score} bod� z celkov�ch {totalQuestions}. To nen� �patn�!";


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

		Navigation.PushAsync(new MainPage()); // N�vrat do Menu, lehce a jednodu�e!
	}
}