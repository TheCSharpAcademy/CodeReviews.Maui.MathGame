using SQLite;
using MathOrWords.Models;

namespace MathOrWords.Data;

public class GameRepository
{
    private string _dbPath;
    private SQLiteConnection _connection;


    public GameRepository(string dbPath)
    {
        _dbPath = dbPath;
    }


    /// <summary>
    /// Initializes database connection and creates a new Games table if one doesn't exist.
    /// </summary>
    public void Init()
    {
        _connection = new SQLiteConnection(_dbPath);
        _connection.CreateTable<Game>(); // Creates a table if one doesn't exist
    }


    /// <summary>
    /// Gets all the records from the Games table.
    /// </summary>
    /// <returns></returns>
    public List<Game> GetAllGames()
    {
        Init(); // Make sure the table gets created when visiting scores for the first time
        return _connection.Table<Game>().ToList();
    }


    /// <summary>
    /// Inserts a new record into the Games table.
    /// </summary>
    /// <param name="game"></param>
    public void AddGame(Game game)
    {
        Init(); // Make sure the table gets created when adding a game for the first time
        _connection.Insert(game);
    }


    /// <summary>
    /// Deletes a record from the Games table.
    /// </summary>
    /// <param name="id"></param>
    public void DeleteGame(int id)
    {
        _connection = new SQLiteConnection(_dbPath);
        _connection.Delete(new Game { Id = id });
    }
}
