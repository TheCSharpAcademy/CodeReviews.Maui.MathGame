using SQLite;

namespace MathGame.wkktoria.Models;

[Table("game")]
public class Game
{
    [PrimaryKey]
    [AutoIncrement]
    [Column("Id")]
    public int Id { get; set; }

    public GameOperation Type { get; set; }
    public DifficultyLevel Difficulty { get; set; }
    public int Score { get; set; }
    public DateTime DatePlayed { get; set; }
}

public enum GameOperation
{
    Addition,
    Subtraction,
    Multiplication,
    Division,
    Undefined
}

public enum DifficultyLevel
{
    Easy,
    Medium,
    Hard,
    NotSelected
}