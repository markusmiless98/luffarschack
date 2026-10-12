using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Luffarschack.Database
{
    public class SQLiteDataBase
    {
        private readonly string _dbPath;

        private readonly SqliteConnection _connection;

        private static SQLiteDataBase Db = new("Luffarschack");

        public static SQLiteDataBase GetDb()
        {
            return Db;
        }
        public SqliteConnection GetConnection()
        {
            return _connection;
        }
        private SQLiteDataBase(string dbName)
        {
            //fix the file path somehow
            _dbPath = $"{dbName}.db"; 

            _connection = new SqliteConnection($"Data Source={_dbPath}");

            _connection.Open(); 

            Init();

        }
        private void Init()
        {
            string dbPath = "Luffarschack.db";
            using var connection = new SqliteConnection($"Data Source={_dbPath}"); 
            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS GameState (
                    Turn INT,
                    Players INT,
                    CurrentPlayer INT,
                    Winner INT
                );
            ";
            command.ExecuteNonQuery();

            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS GamePositions (
                    x INT,
                    y INT,
                    z INT,
                    Player INT
                );
            ";
            command.ExecuteNonQuery();

            command.CommandText = @"
            INSERT INTO GameState (Outcome, CurrentPlayer, Winner) VALUES (0, 0, 0);
            ";
            command.ExecuteNonQuery();

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        command.CommandText = $@"
                        INSERT INTO GamePositions (x, y, z, Player) VALUES ({x}, {y}, {z}, 0);
                        ";
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    


        

        private void Init2()
        {
            string dbPath = "Luffarschack.db";
            using var connection = new SqliteConnection($"Data Source={_dbPath}");
            connection.Open();
            var command = connection.CreateCommand();
            // Using TEXT instead of VARCHAR because SQLite treats them the same, and TEXT is simpler
            // Channels table - Main Entity Table
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS GameState (
                    GameId TEXT PRIMARY KEY,
                    BoardState TEXT,
                    CurrentPlayer INT,
                    Winner INT
                );
            ";
            command.ExecuteNonQuery();
        }
    }
}
