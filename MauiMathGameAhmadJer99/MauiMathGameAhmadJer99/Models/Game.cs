using SQLite;

namespace MauiMathGameAhmadJer99.Models;

[Table("game")]
public class Game
{

    [PrimaryKey, AutoIncrement, Column("_id")]
    public int Id { get; set; }
    public GameOperation GameType { get; set; }
    public int Score { get; set; }
    public int GameTimeTaken { get; set; }
    public DateTime DatePlayed { get; set; }

    public enum GameOperation
    {
        Addition,
        Subtraction,
        Division,
        Multiplication
    }
    
}
