namespace MathGame;

public class Substraction : Game
{
    private Game _game = new Game();
    public Substraction()
    {
        Console.WriteLine("Substraction Game");
        Console.WriteLine($"{_game.FirstNumber} - {_game.SecondNumber}");
        _game.ValidateInput();
        CheckAnswer();
        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
        Console.Clear();
    }

    public void CheckAnswer()
    {
        if (_game.userAnswer == (_game.FirstNumber - _game.SecondNumber))
        {
            history.Add($"{_game.FirstNumber} - {_game.SecondNumber} = {_game.FirstNumber - _game.SecondNumber}." +
                        $" Your answer was correct!");
            Console.WriteLine("Your answer was correct! Type any key for the next question");
            Game.numberOfQuestions++;
            Game.correctAnswer++;
        }
        else
        {
            history.Add($"{_game.FirstNumber} - {_game.SecondNumber} = {_game.FirstNumber - _game.SecondNumber}." +
                        $" You answered {_game.userAnswer}");
            Console.WriteLine("Incorrect answer");
            Game.numberOfQuestions++;
            Game.incorrectAnswer++;
        }
    }
}