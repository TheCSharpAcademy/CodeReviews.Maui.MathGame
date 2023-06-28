using SQLite;

namespace MathsMAUI.Models
{
    [Table("history")]
    public class History
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int id {  get; set; }
        public MathOperation gameChoice { get; set; }
        public string question { get; set; }
        public int answer { get; set; }
        public int userAnswer { get; set; }
        public string wasAnswerRight { get; set; }
        public DateTime playedAt { get; set; }
    }

    public enum MathOperation
    {
        Addition,
        Subtraction,
        Multiplication,
        Division,
    }
}
