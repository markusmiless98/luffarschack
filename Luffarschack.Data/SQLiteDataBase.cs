using Luffarschack.Core;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Luffarschack.Database
{
    public class SqliteDatabase
    {
        private readonly string _dbPath;
        private readonly SqliteConnection _connection;

        public SqliteDatabase(string dbPath)
        {
            _dbPath = dbPath;
            bool file_exists = dbPath.FileExistsInProjectDirectory();
            _connection = new SqliteConnection($"Data Source={_dbPath}");
            _connection.Open();
            Init(file_exists);
        }

        public SqliteConnection GetConnection() => _connection;
        
        private void Init(bool file_exists = false)
        {
            string dbPath = "Luffarschack.db";

            using var connection = new SqliteConnection($"Data Source={_dbPath}"); 
            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS GameState (
                    Turn INT,
                    PlayerCount INT,
                    CurrentPlayer INT,
                    Outcome INT
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
        
            if (file_exists)
            {
                return; // Should not Insert Into if file already exists in most cases
            }

            command.CommandText = @"
            INSERT INTO GameState (Turn, PlayerCount, CurrentPlayer, Outcome) VALUES (0, 0, 0, 0);
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
    }
}
