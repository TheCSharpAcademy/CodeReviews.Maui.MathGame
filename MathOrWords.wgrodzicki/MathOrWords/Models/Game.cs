using SQLite;

namespace MathOrWords.Models;

[Table("Games")] // Name of the table in the database
public class Game
{
    [PrimaryKey, AutoIncrement, Column("id")]
    public int Id { get; set; }
    [NotNull, Column("date")]
    public DateTime Date { get; set; }
    [NotNull, Column("score")]
    public int Score { get; set; }
    [NotNull, Column("mode")]
    public GameMode GameMode { get; set; }
    [Column("math_variant")]
    public string? MathVariant { get; set; }
}


public enum GameMode
{
    Math,
    Words
}
