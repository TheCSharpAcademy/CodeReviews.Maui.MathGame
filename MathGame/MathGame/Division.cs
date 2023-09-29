namespace MathGame;

public class Division : Game
{
    private Game _game;
    public Division()
    {
        _game = new Game();
        Console.WriteLine("Division Game");
        ValidateRandomNumbers();
        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
        Console.Clear();
    }

    public void ValidateRandomNumbers()
    {
        if (_game.SecondNumber == 0)
        {
            FakeConstructor();
            return;
        }
        string quotient = $"{(double)_game.FirstNumber / (double)_game.SecondNumber}";
        int blah;
        if (int.TryParse(quotient, out _))
        {
            Console.WriteLine($"{_game.FirstNumber} / {_game.SecondNumber}");
            _game.ValidateInput();
            CheckAnswer();
        }
        else
        {
            FakeConstructor();
        }
    }

    public void CheckAnswer()
    {
        if (_game.userAnswer == (_game.FirstNumber / _game.SecondNumber))
        {
            history.Add($"{_game.FirstNumber} / {_game.SecondNumber} = {_game.FirstNumber / _game.SecondNumber}." +
                        $" Your answer was correct!");
            Console.WriteLine("Your answer was correct! Type any key for the next question");
            Game.numberOfQuestions++;
            Game.correctAnswer++;
        }
        else
        {
            history.Add($"{_game.FirstNumber} / {_game.SecondNumber} = {_game.FirstNumber / _game.SecondNumber}." +
                        $" You answered {_game.userAnswer}");
            Console.WriteLine("Incorrect answer");
            Game.numberOfQuestions++;
            Game.incorrectAnswer++;
        }
    }

    public void FakeConstructor()
    {
        _game = new Game();
        ValidateRandomNumbers(); 
    }
}