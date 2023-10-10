using MathGame.wkktoria.Models;

namespace MathGame.wkktoria;

public partial class GamePage
{
    private DifficultyLevel _difficultyLevel;
    private int _firstNumber;
    private int _gamesLeft;
    private int _maxNumber;
    private int _minNumber;
    private int _score;
    private int _secondNumber;
    private int _totalQuestions;

    public GamePage(string gameType)
    {
        InitializeComponent();

        GameType = gameType;
        BindingContext = this;
    }

    private string GameType { get; }

    private void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
    {
        var value = (int)args.NewValue;
        QuestionsNumberLabel.Text = $"{value}";
        _totalQuestions = value;
        _gamesLeft = value;
    }

    private void OnDifficultyLevelSelected(object sender, EventArgs e)
    {
        var button = (Button)sender;

        switch (button.Text)
        {
            case "Easy":
                _minNumber = 1;
                _maxNumber = 9;
                _difficultyLevel = DifficultyLevel.Easy;
                EasyDifficultyButton.BorderWidth = 1;
                MediumDifficultyButton.BorderWidth = 0;
                HardDifficultyButton.BorderWidth = 0;
                break;
            case "Medium":
                _minNumber = 10;
                _maxNumber = 49;
                _difficultyLevel = DifficultyLevel.Medium;
                EasyDifficultyButton.BorderWidth = 0;
                MediumDifficultyButton.BorderWidth = 1;
                HardDifficultyButton.BorderWidth = 0;
                break;
            case "Hard":
                _minNumber = 50;
                _maxNumber = 99;
                _difficultyLevel = DifficultyLevel.Hard;
                EasyDifficultyButton.BorderWidth = 0;
                MediumDifficultyButton.BorderWidth = 0;
                HardDifficultyButton.BorderWidth = 1;
                break;
            default:
                _minNumber = 1;
                _maxNumber = 9;
                _difficultyLevel = DifficultyLevel.NotSelected;
                break;
        }
    }

    private void OnGoToQuestions(object sender, EventArgs e)
    {
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

        GameOverLabel.Text = $"Game over! Your got {_score} out of {_totalQuestions} right!";

        App.GameRepository.Add(new Game
        {
            Type = gameOperation,
            Difficulty = _difficultyLevel,
            Score = _score,
            Total = _totalQuestions,
            DatePlayed = DateTime.UtcNow
        });
    }

    private void OnBackToMenu(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }
}