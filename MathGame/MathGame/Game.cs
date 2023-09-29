namespace MathGame;

public  class Game
{
    private Random _random = new Random();
    public int FirstNumber { get; set; }
    
    public int SecondNumber { get; set; }

    public int userAnswer { get; set; }

    public static int numberOfQuestions = 0;
    public static int correctAnswer = 0;
    public static int incorrectAnswer = 0;
    public static List<string> history = new List<string>();
    
    public Game()
    {
        FirstNumber = _random.Next(0, 101);
        SecondNumber = _random.Next(0, 101);
    }
    
    public void ValidateInput()
    {
        String input;
        
        while (true)
        {
            input = Console.ReadLine();

            if (input.All(char.IsDigit) && input != "")
            {
                userAnswer = int.Parse(input);
                break;
            }
            else
            {
                Console.WriteLine("Please enter a valid value!");
                Game.numberOfQuestions++;
            Game.incorrectAnswer++;
            }
        }
    }
}