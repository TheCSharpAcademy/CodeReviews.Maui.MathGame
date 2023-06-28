using MathsMAUI.Models;
using SQLite;

namespace MathsMAUI.Data
{
    public class GameRepository
    {
        private readonly string _dbPath;
        private SQLiteConnection _dbConn;

        public GameRepository(string dbPath)
        {
            _dbPath = dbPath;
            Init(_dbPath);
        }

        private void Init(string dbPath)
        {
            if (_dbConn != null)
                return;

            _dbConn = new SQLiteConnection(dbPath);
            _dbConn.CreateTable<History>();
        }

        public List<History> GetAllHistory()
        {
            _dbConn = new SQLiteConnection(_dbPath);
            List<History> AllHistory = _dbConn.Table<History>().ToList();
            return AllHistory;
        }
        
        public void AddHistory(History history)
        {
            _dbConn = new SQLiteConnection(_dbPath);
            _dbConn.Insert(history);
        }

        public void DeleteHistory(int id) 
        {
            _dbConn = new SQLiteConnection(_dbPath);
            _dbConn.Delete(new History { id = id });
        }
    }
}
