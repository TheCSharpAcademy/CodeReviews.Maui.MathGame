using SQLite;
using mathGame.Maui.Models;


namespace mathGame.Maui.Data
{
    // Created folder - Data -> for communication between SQLite and the rest of the program.
    public class GameRepository
    {
        string _dbPath;
        private SQLiteConnection conn;

        public GameRepository(string dbPath)
        {
            // Assigning incoming string from connection to declared string at the beggining.
            _dbPath = dbPath;
        }

        
        public void Init()
        {
            // For initialization of the database, with instiation of the connection with the PC path.
            conn = new SQLiteConnection(_dbPath);

            // Initialization of the table, from table u will read and store data.
            conn.CreateTable<Game>(); // Vytvoří table založen na vytvořeném Modelu - Games.
        }

        // Method to retrieve played games from the database.
        public List<Game> GetAllGames()
        {
            Init(); // Table will be created if it doesnt exist.
            return conn.Table<Game>().ToList(); // Retrieves data from database to a list.
        }

        // Add a new record to the table.
        public void Add(Game game)
        {
            conn = new SQLiteConnection(_dbPath); // Establish connection with the database
            conn.Insert(game); // Vloží data z databaze games do Game tablu.
        }

        // Delete a record from the table.
        public void Delete(int id)
        {
            conn = new SQLiteConnection(_dbPath);
            conn.Delete(new Game { Id = id });
        }

    }
}
