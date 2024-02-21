using MathOrWords.Models;

namespace MathOrWords;

public partial class MathGamePage : ContentPage
{
	private const int DivisionUpperLimit = 100;
	private const int MultiplicationUpperLimit = 11;
	private const int AdditionSubtractionUpperLimit = 100;
    private const int IncorrectAnswersAllowed = 3;

	private string _variant;
    private int _firstOperand = 0;
	private int _secondOperand = 0;
	private int _score = 0;
	private int _incorrectAnswers = 0;

	public MathGamePage(string variant)
	{
		InitializeComponent();
		_variant = variant;

        BindingContext = this;
		
		NextEquationButton.IsEnabled = false;
        IncorrectCounterLabel.Text = $"Attempts: {IncorrectAnswersAllowed - _incorrectAnswers}/{IncorrectAnswersAllowed}";
        ScoreLabel.Text = $"Score: {_score}";
        GenerateEquation();
	}


    /// <summary>
    /// Generates a new equation to solve by the player. Makes sure the division results only in whole numbers.
    /// </summary>
    private void GenerateEquation()
    {
        string? mathOperator = _variant switch // New switch notation
        {
            "Addition" => "+", // case => value
            "Subtraction" => "-",
            "Multiplication" => "×",
            "Division" => "÷",
            _ => "" // Default
        };

        Random random = new Random();

        if (_variant == "Division")
        {
            do
            {
                _firstOperand = random.Next(1, DivisionUpperLimit);
                _secondOperand = random.Next(1, DivisionUpperLimit);
            } while (_firstOperand < _secondOperand || _firstOperand % _secondOperand != 0);
        }
        else if (_variant == "Multiplication")

        {
            _firstOperand = random.Next(1, MultiplicationUpperLimit);
            _secondOperand = random.Next(1, MultiplicationUpperLimit);
        }
        else
        {
            _firstOperand = random.Next(1, AdditionSubtractionUpperLimit);
            _secondOperand = random.Next(1, AdditionSubtractionUpperLimit);
        }

        EquationLabel.Text = $"{_firstOperand} {mathOperator} {_secondOperand}";
    }


    /// <summary>
    /// Handles answer submission and further processing of the game (continue or call the game over).
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnAnswerSubmitted(object sender, EventArgs e)
	{
		string rawAnswer = AnswerEntry.Text;
		int answer;

        AnswerLabel.IsVisible = true;

        // Initial verification if the answer is an int
        if (int.TryParse(rawAnswer, out answer))
		{
            AnswerEntry.IsEnabled = false;

            if (ValidateAnswer(answer))
			{
                AnswerLabel.Text = "Correct!";
				_score++;
                ScoreLabel.Text = $"Score: {_score}";
                SubmitAnswerButton.IsVisible = false;
            }
			else
			{
                AnswerLabel.Text = "Incorrect";
				_incorrectAnswers++;
                IncorrectCounterLabel.Text = $"Attempts: {IncorrectAnswersAllowed - _incorrectAnswers}/{IncorrectAnswersAllowed}";
                SubmitAnswerButton.IsVisible = false;
            }

            if (_incorrectAnswers < IncorrectAnswersAllowed)
            {
                NextEquationButton.IsEnabled = true;
            }
            else
            {
				GamePage.GameOver(_score, GameMode.Math, EquationArea, GameOverLabel, IncorrectCounterLabel, ScoreLabel, ReturnButton, _variant);
            }
        }
		else
		{
            AnswerLabel.Text = "Invalid answer";
        }
	}


	/// <summary>
	/// Validates player answer depending on the math operation.
	/// </summary>
	/// <param name="answer"></param>
	/// <returns></returns>
	private bool ValidateAnswer(int answer)
	{
		switch (_variant)
		{
			case "Addition":
				return _firstOperand + _secondOperand == answer ? true : false;
			case "Subtraction":
                return _firstOperand - _secondOperand == answer ? true : false;
			case "Multiplication":
                return _firstOperand * _secondOperand == answer ? true : false;
			case "Division":
                return _firstOperand / _secondOperand == answer ? true : false;
			default:
				return false;
		}
	}


	/// <summary>
	/// Displays the next equation.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
    private void OnNextEquationChosen(object sender, EventArgs e)
    {
        AnswerEntry.IsEnabled = true;
        AnswerEntry.Text = "";

        AnswerLabel.IsVisible = false;
        AnswerLabel.Text = "";

        SubmitAnswerButton.IsVisible = true;
        NextEquationButton.IsEnabled = false;

        GenerateEquation();
	}


	/// <summary>
	/// Switches back to the main page.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void OnReturnButtonChosen(object sender, EventArgs e)
	{
		GamePage.ReturnToMainPage();
    }
}