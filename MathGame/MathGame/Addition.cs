namespace MathGame;

public class Addition : Game
{ 
    private Game _game = new Game();
    public Addition()
    {
        Console.WriteLine("Addition Game");
        Console.WriteLine($"{_game.FirstNumber} + {_game.SecondNumber}");
        _game.ValidateInput();
        CheckAnswer();
        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
        Console.Clear();
    }

    public void CheckAnswer()
    {
        if (_game.userAnswer == (_game.FirstNumber + _game.SecondNumber))
        {
            history.Add($"{_game.FirstNumber} + {_game.SecondNumber} = {_game.FirstNumber + _game.SecondNumber}." +
                        $" Your answer was correct!");
            Console.WriteLine("Your answer was correct! Type any key for the next question");
            Game.numberOfQuestions++;
            Game.correctAnswer++;
        }
        else
        {
            history.Add($"{_game.FirstNumber} + {_game.SecondNumber} = {_game.FirstNumber + _game.SecondNumber}." +
                        $" You answered {_game.userAnswer}");
            Console.WriteLine("Incorrect answer");
            Game.numberOfQuestions++;
            Game.incorrectAnswer++;
        }
    }
}