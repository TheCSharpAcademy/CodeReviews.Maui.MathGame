using SQLite;
using MauiMathGameAhmadJer99.Models;

namespace MauiMathGameAhmadJer99.Data;

public class GameRepository
{
    string _databasePath;
    private SQLiteConnection connection;

    public GameRepository(string databasePath)
    {
        _databasePath = databasePath;
    }
    public void InitConnection()
    {
        connection = new SQLiteConnection(_databasePath);
        connection.CreateTable<Game>();
    }
    public List<Game> GetAllGames()
    {
        InitConnection();
        return connection.Table<Game>().ToList();
    }
    public void AddGame(Game game)
    {
        connection = new SQLiteConnection(_databasePath);
        connection.Insert(game);
    }
    public void RemoveGame(int id)
    {
        connection = new SQLiteConnection(_databasePath);
        connection.Delete(new Game { Id = id});
    }
}
