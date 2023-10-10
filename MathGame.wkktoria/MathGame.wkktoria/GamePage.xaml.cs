using MathGame.wkktoria.Models;

namespace MathGame.wkktoria;

public partial class GamePage
{
    private const int TotalQuestions = 2;
    private DifficultyLevel _difficultyLevel;
    private int _firstNumber;
    private int _gamesLeft = TotalQuestions;
    private int _maxNumber;
    private int _minNumber;
    private int _score;
    private int _secondNumber;

    public GamePage(string gameType)
    {
        InitializeComponent();

        GameType = gameType;
        BindingContext = this;
    }

    private string GameType { get; }

    private void OnDifficultyLevelSelected(object sender, EventArgs e)
    {
        var button = (Button)sender;

        switch (button.Text)
        {
            case "Easy":
                _minNumber = 1;
                _maxNumber = 9;
                _difficultyLevel = DifficultyLevel.Easy;
                break;
            case "Medium":
                _minNumber = 10;
                _maxNumber = 49;
                _difficultyLevel = DifficultyLevel.Medium;
                break;
            case "Hard":
                _minNumber = 50;
                _maxNumber = 99;
                _difficultyLevel = DifficultyLevel.Hard;
                break;
            default:
                _minNumber = 1;
                _maxNumber = 9;
                _difficultyLevel = DifficultyLevel.NotSelected;
                break;
        }

        SelectionArea.IsVisible = false;
        QuestionArea.IsVisible = true;

        CreateNewQuestion();
    }

    private void CreateNewQuestion()
    {
        var gameOperand = GameType switch
        {
            "Addition" => "+",
            "Subtraction" => "-",
            "Multiplication" => "*",
            "Division" => "/",
            _ => ""
        };

        var random = new Random();

        _firstNumber = random.Next(_minNumber, _maxNumber);
        _secondNumber = random.Next(_minNumber, _maxNumber);

        if (GameType == "Division")
            while (_firstNumber < _secondNumber || _firstNumber % _secondNumber != 0)
            {
                _firstNumber = random.Next(_minNumber, _maxNumber);
                _secondNumber = random.Next(_minNumber, _maxNumber);
            }

        QuestionLabel.Text = $"{_firstNumber} {gameOperand} {_secondNumber}";
    }

    private void OnAnswerSubmitted(object sender, EventArgs e)
    {
        var answer = int.Parse(AnswerEntry.Text);

        var isCorrect = GameType switch
        {
            "Addition" => answer == _firstNumber + _secondNumber,
            "Subtraction" => answer == _firstNumber - _secondNumber,
            "Multiplication" => answer == _firstNumber * _secondNumber,
            "Division" => answer == _firstNumber / _secondNumber,
            _ => false
        };

        ProcessAnswer(isCorrect);

        _gamesLeft--;
        AnswerEntry.Text = "";

        if (_gamesLeft > 0)
            CreateNewQuestion();
        else
            GameOver();
    }

    private void ProcessAnswer(bool isCorrect)
    {
        if (isCorrect) _score++;

        AnswerLabel.Text = isCorrect ? "Correct!" : "Wrong!";
    }

    private void GameOver()
    {
        var gameOperation = GameType switch
        {
            "Addition" => GameOperation.Addition,
            "Subtraction" => GameOperation.Subtraction,
            "Multiplication" => GameOperation.Multiplication,
            "Division" => GameOperation.Division,
            _ => GameOperation.Undefined
        };

        QuestionArea.IsVisible = false;
        GameOverArea.IsVisible = true;

        GameOverLabel.Text = $"Game over! Your got {_score} out of {TotalQuestions} right!";

        App.GameRepository.Add(new Game
        {
            Type = gameOperation,
            Difficulty = _difficultyLevel,
            Score = _score,
            DatePlayed = DateTime.UtcNow
        });
    }

    private void OnBackToMenu(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }
}