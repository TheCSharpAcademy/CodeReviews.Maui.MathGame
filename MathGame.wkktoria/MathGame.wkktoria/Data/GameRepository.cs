using MathGame.wkktoria.Models;
using SQLite;

namespace MathGame.wkktoria.Data;

public class GameRepository
{
    private readonly string _dbPath;
    private SQLiteConnection _connection;

    public GameRepository(string dbPath)
    {
        _dbPath = dbPath;
    }

    private void Init()
    {
        _connection = new SQLiteConnection(_dbPath);
        _connection.CreateTable<Game>();
    }

    public IEnumerable<Game> GetAllGames()
    {
        Init();
        return _connection.Table<Game>().ToList();
    }

    public void Add(Game game)
    {
        _connection = new SQLiteConnection(_dbPath);
        _connection.Insert(game);
    }

    public void Delete(int id)
    {
        _connection = new SQLiteConnection(_dbPath);
        _connection.Delete(new Game { Id = id });
    }
}