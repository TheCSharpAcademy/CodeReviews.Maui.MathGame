using SQLite;
using System.Diagnostics;

namespace MathGameMauiJMS.Models
{
    [Table("game")]
    public class Game
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        public GameType Type { get; set; }
        public int Score { get; set; }
        public DateTime DatePlayed { get; set; }
        public string Difficulty { get; set; }
        public int TimePlayed { get; set; }
    }

    public enum GameType
    {
        Addition,
        Subtraction,
        Multiplication,
        Division,
        Random,
    }
}
