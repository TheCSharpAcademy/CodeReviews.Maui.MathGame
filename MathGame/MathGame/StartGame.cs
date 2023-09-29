
namespace MathGame;

public class StartGame
{
    
    public static void Menu(){
    bool gameover = false;

        while (!gameover)
        {
            Console.WriteLine("What game would you like to play?");
        
            Console.WriteLine("V - view previous games");
            Console.WriteLine("A - Addition");
            Console.WriteLine("S - Subtraction");
            Console.WriteLine("M - Multiplication");
            Console.WriteLine("D - Division");
            Console.WriteLine("Q - Quit");

            string  input;

            while (true)
            {
                input = Console.ReadLine();

                if (input.ToUpper() == "V" || input.ToUpper() == "A" || input.ToUpper() == "S" ||
                    input.ToUpper() == "M" || input.ToUpper() == "D" || input.ToUpper() == "Q")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Enter a valid input!");
                }
            }
            
            switch (input.ToUpper())
            {
                case "V":
                    Console.Clear();
                    ViewList();
                    break;
                case "A":
                    Console.Clear();
                    for (int i = 0; i < 5; i++)
                    {
                        Addition addition = new Addition();
                    }
                    Console.WriteLine($"So far you've gotten {Game.correctAnswer} questions right out of {Game.numberOfQuestions}");
                    break;
                case "S":
                    Console.Clear();
                    for (int i = 0; i < 5; i++)
                    {
                        Substraction substraction = new Substraction();
                    }
                    Console.WriteLine($"So far you've gotten {Game.correctAnswer} questions right out of {Game.numberOfQuestions}");
                    break;
                case "M":
                    Console.Clear();
                    for (int i = 0; i < 5; i++)
                    {
                        Multipilication multipilication = new Multipilication();
                    }
                    Console.WriteLine($"So far you've gotten {Game.correctAnswer} questions right out of {Game.numberOfQuestions}");
                    break;
                case "D":
                    Console.Clear();
                    for (int i = 0; i < 5; i++)
                    {
                        Division division = new Division();
                    }
                    Console.WriteLine($"So far you've gotten {Game.correctAnswer} questions right out of {Game.numberOfQuestions}");
                    break;
                case "Q":
                    Console.Clear();
                    Console.WriteLine($"you got {Game.correctAnswer} questions right out of {Game.numberOfQuestions}");
                    gameover = true;
                    break;
            }
        }
    }

    public static void ViewList()
    {
        foreach (string i in Game.history)
        {
            Console.WriteLine(i);
        }
        Console.WriteLine("Enter any key to continue");
        Console.ReadLine();
        Console.Clear();
    }
}