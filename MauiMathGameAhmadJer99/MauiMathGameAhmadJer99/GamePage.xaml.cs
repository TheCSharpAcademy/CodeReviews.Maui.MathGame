using MauiMathGameAhmadJer99.Data;
using MauiMathGameAhmadJer99.Models;

namespace MauiMathGameAhmadJer99;

public partial class GamePage : ContentPage
{
    int firstNumber = 0;
    int secondNumber = 0;
    int score = 0;
    const int timePerQuestion = 6;

    // To bet set later when the user sets the number of questions, default questions number is 2
    int questionsLeft;
    int totalTime;
    int triesLeft;
    // ...

    public string GameType { get; set; }
    public string GameTitle { get; set; }
    public bool TimeOut { get; set; }
    public int TimeTaken { get; set; }

    private DateTime questionStartTime;
    private CancellationTokenSource? _timerCancellationTokenSource;


    public GamePage(string gameType)
    {
        InitializeComponent();
        _timerCancellationTokenSource?.Cancel();

        GameType = gameType;
        GameTitle = gameType switch
        {
            "+" => "Addition",
            "-" => "Subtraction",
            "x" => "Multiplication",
            "/" => "Division",
            _ => throw new ArgumentException("Invalid Operation")
        };
        BindingContext = this;

        QuestionPhase.IsVisible = false;
        SetQuestionsNumberPhase.IsVisible = true;


    }
    private void StartGame()
    {
        StartTimer();
        questionStartTime = DateTime.Now;
        CreateNewQuestion();
    }


    private void OnSetQuestions(object sender, EventArgs e)
    {
        if (int.TryParse(QuestionsNumberEntry.Text, out int numQuestions) && numQuestions > 0)
        {
            questionsLeft = numQuestions;
            triesLeft = questionsLeft;
            totalTime = timePerQuestion * questionsLeft;

            SetQuestionsNumberPhase.IsVisible = false;  // Hide the setup screen
            QuestionPhase.IsVisible = true;            // Show the question screen
            StartGame();                               // Start the game with user settings
        }
        else
        {
            InvalidQuestionNumberEntry.IsVisible = true;
            InvalidQuestionNumberEntry.Text = "Please enter a valid number of questions";

        }
    }

    private void CreateNewQuestion()
    {
        SetNumbersBasedOnDifficulty();
        string questionText = $"{firstNumber} {GameType} {secondNumber}";
        QuestionLabel.Text = questionText;
    }

    private void SetNumbersBasedOnDifficulty()
    {
        var random = new Random();

        switch (GameSettings.DifficultyLevel)
        {
            case "easy":
                firstNumber = GameType != "Division" ? random.Next(1, 20) : random.Next(1, 99);
                secondNumber = GameType != "Division" ? random.Next(1, 20) : random.Next(1, 99);

                if (GameType == "/")
                {
                    while ((firstNumber < secondNumber) | (firstNumber % secondNumber != 0))
                    {
                        firstNumber = random.Next(1, 99);
                        secondNumber = random.Next(1, 99);
                    }
                }
                break;
            case "medium":
                firstNumber = GameType != "Division" ? random.Next(10, 70) : random.Next(10, 150);
                secondNumber = GameType != "Division" ? random.Next(10, 70) : random.Next(10, 150);

                if (GameType == "/")
                {
                    while ((firstNumber < secondNumber) | (firstNumber % secondNumber != 0))
                    {
                        firstNumber = random.Next(10, 150);
                        secondNumber = random.Next(10, 150);
                    }
                }
                break;
            case "hard":
                firstNumber = GameType != "Division" ? random.Next(35, 200) : random.Next(1, 300);
                secondNumber = GameType != "Division" ? random.Next(35, 200) : random.Next(1, 300);

                if (GameType == "/")
                {
                    while ((firstNumber < secondNumber) | (firstNumber % secondNumber != 0))
                    {
                        firstNumber = random.Next(1, 300);
                        secondNumber = random.Next(1, 300);
                    }
                }
                break;
        }


    }

    private void StartTimer()
    {
        // Cancel any previous timer
        _timerCancellationTokenSource?.Cancel();
        _timerCancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = _timerCancellationTokenSource.Token;

        TimeOut = false;
        _ = RunTimerAsync(totalTime, cancellationToken);
    }

    private async Task RunTimerAsync(int seconds, CancellationToken cancellationToken)
    {

        try
        {
            TimerBox.IsVisible = true;
            int timeCountDown;
            for (timeCountDown = seconds; timeCountDown > 0; timeCountDown--)
            {
                TimerBox.Text = $"Time Left: {timeCountDown}s";
                await Task.Delay(1000, cancellationToken); // Delay with cancellation support

            }
            TimeTaken = timeCountDown;
            // Time out if timer completes without interruption
            TimeOut = true;
            triesLeft = 0;
            GameOver();
        }
        catch (TaskCanceledException)
        {
            // Timer was canceled, meaning the user submitted an answer
        }
    }


    private async void OnBackToMenu(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PopAsync(); // Use PopAsync to avoid creating new instances
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        };
    }
    private void OnAnswerSubmit(object sender, EventArgs e)
    {

        var validUserEntry = ValidateUserEntry();
        if (!validUserEntry.HasValue)
            return;

        ProcessAnswer((int)validUserEntry);

        triesLeft--;
        GameOver();
        UserEntry.Text = string.Empty;

        CreateNewQuestion();
    }

    private int? ValidateUserEntry()
    {
        string? rawEntry = UserEntry.Text;
        int validEntry;

        if (string.IsNullOrWhiteSpace(rawEntry))
        {
            UpdateAnswerFeedBack("Invalid Input - Value can't be empty");
            return null;
        }


        else if (!Int32.TryParse(rawEntry, out validEntry))
        {
            UpdateAnswerFeedBack("Invalid Input - Value can't be a string");
            return null;
        }
        else
            return validEntry;

    }

    private void GameOver()
    {
        if (!TimeOut && triesLeft != 0)
            return;

        Game.GameOperation gameOperation = GameTitle switch
        {
            "Addition" => Game.GameOperation.Addition,
            "Subtraction" => Game.GameOperation.Subtraction,
            "Division" => Game.GameOperation.Division,
            "Multiplication" => Game.GameOperation.Multiplication,
            _ => throw new NotImplementedException(),
        };


        _timerCancellationTokenSource?.Cancel();
        _timerCancellationTokenSource?.Dispose();
        _timerCancellationTokenSource = null;

        QuestionPhase.IsVisible = false;
        ResultPhase.IsVisible = true;

        TimeTaken += (int)(DateTime.Now - questionStartTime).TotalSeconds;

        DateTime datePlayed = DateTime.Now;


        if (score == questionsLeft)
        {
            GameOverLabel.BackgroundColor = Color.Parse("ForestGreen");
            GameOverLabel.Text = "You Won!";
        }
        ScoreResult.Text = $"You got [{score}] out of [{questionsLeft}] right! time taken = {TimeTaken}s";

        App.GameRepository.AddGame(new Game
        {
            DatePlayed = datePlayed,
            Score = score,
            GameTimeTaken = TimeTaken,
            GameType = gameOperation
        });
    }

    private void ProcessAnswer(int userAnswer)
    {

        bool isCorrect = false;
        var correctAnswer = GameType switch
        {
            "+" => firstNumber + secondNumber,
            "-" => firstNumber - secondNumber,
            "x" => firstNumber * secondNumber,
            "/" => firstNumber / secondNumber,
            _ => 0
        };

        isCorrect = userAnswer == correctAnswer;
        score = isCorrect ? score += 1 : score;

        string feedbackMessage = isCorrect ? "Correcto!" : $"Wrong! Correct answer = {correctAnswer}";
        UpdateAnswerFeedBack(feedbackMessage);
    }

    private void UpdateAnswerFeedBack(string message)
    {
        AnswerFeedback.IsVisible = true;
        AnswerFeedback.Text = message;
    }

}