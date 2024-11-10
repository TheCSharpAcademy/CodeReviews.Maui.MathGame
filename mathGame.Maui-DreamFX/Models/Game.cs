using SQLite;
using System.ComponentModel.DataAnnotations;

namespace mathGame.Maui.Models
{
    // SQLite
    [Table("game")]
    public class Game
    {
        // Vytvořena Models složka pro storovaní dat pomocí SQL.


        // Příkaz pro SQL - refaktoring - nainstalovat nabízený balíček.
        [PrimaryKey, AutoIncrement, Column("Id")]

        // Setting properties (Vlastnosti) databáze ve které se budou ukládat data.
        public int Id { get; set; }
        public GameOperation Type { get; set; }
        public int Score { get; set; }
        public DateTime DatePlayed { get; set; }

    }

    // Název operace - ENUMERACE
    // Typy pro akci GameOperation, na výběr.
    public enum GameOperation
    {
        Sčítání,
        Odčítání,
        Násobení,
        Dělení,
    }
}
