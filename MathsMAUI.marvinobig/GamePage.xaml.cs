using MathsMAUI.Models;

namespace MathsMAUI;

public partial class GamePage : ContentPage
{
	private string _game { get; set; }
    public string GameName
	{
		get { return $"{_game} Area"; }
	}
    private int _firstNum { get; set; }
    private int _secondNum { get; set; }
    private string _gameQuestion { get; set; }
    public string GameQuestion
    {
        get { return _gameQuestion; }
        set { _gameQuestion = value; }
    }

	public GamePage(string game)
	{
		InitializeComponent();
		_game = game;
		BindingContext = this;

		GenerateQuestion();
	}

	private void GenerateQuestion()
	{
        char mathOperand = _game switch
        {
            "Addition" => '+',
            "Subtraction" => '-',
            "Multiplication" => 'x',
            "Division" => '\u00F7',
            _ => ' '
        };

		Random random = new();

		_firstNum = _game != "Division" ? random.Next(1, 9) : random.Next(1, 99);
        _secondNum = _game != "Division" ? random.Next(1, 9) : random.Next(1, 99);

		if (_game == "Division")
		{
			while (_firstNum < _secondNum || _firstNum % _secondNum != 0) 
			{
				_firstNum = random.Next(1, 99);
				_secondNum = random.Next(1, 99);
			}
		}

        GameQuestion = $"{_firstNum} {mathOperand} {_secondNum}";
        Question.Text = GameQuestion;
    }

	private async void HandleAnswer(object sender, EventArgs e)
	{
		int solution;
		bool isUserAnswerNum = int.TryParse(AnswerInput.Text, out int intAnswer);

		if (isUserAnswerNum)
		{
			if (_game == "Addition")
			{
                solution = _firstNum + _secondNum;

                GameResponse(solution, intAnswer);
            }

            if (_game == "Subtraction")
            {
                solution = _firstNum - _secondNum;

                GameResponse(solution, intAnswer);
            }

            if (_game == "Multiplication")
            {
                solution = _firstNum * _secondNum;

                GameResponse(solution, intAnswer);
            }

            if (_game == "Division")
            {
                solution = _firstNum / _secondNum;

                GameResponse(solution, intAnswer);
            }
        }
		else
		{
            await DisplayAlert("Math: Basic Arithmetic", "Your answer is not a number", "Okay");
        }
	}

    private async void GameResponse(int solution, int intAnswer)
    {
        MathOperation mathOperation = _game switch
        {
            "Addition" => MathOperation.Addition,
            "Subtraction" => MathOperation.Subtraction,
            "Multiplication" => MathOperation.Multiplication,
            "Division" => MathOperation.Division,
            _ => throw new NotImplementedException()
        };

        if (intAnswer == solution)
        {
            await DisplayAlert("Math: Basic Arithmetic", "Result: You got that question right", "Next");          
            AnswerInput.Text = "";

            App.GameRepository.AddHistory(new History
            {
                gameChoice = mathOperation,
                question = GameQuestion,
                playedAt = DateTime.Now,
                answer = solution,
                wasAnswerRight = "Correct",
                userAnswer = intAnswer
            });

            GenerateQuestion();
        }
        else
        {
            await DisplayAlert("Math: Basic Arithmetic", "Result: You got that question wrong", "Next");
            AnswerInput.Text = "";

            App.GameRepository.AddHistory(new History
            {
                gameChoice = mathOperation,
                question = GameQuestion,
                playedAt = DateTime.Now,
                answer = solution,
                wasAnswerRight = "Incorrect",
                userAnswer = intAnswer
            });

            GenerateQuestion();
        }
    }
}