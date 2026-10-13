using Luffarschack.Core;
using Luffarschack.Core.Interface;
using Microsoft.Data.Sqlite;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Channels;

namespace Luffarschack.Database
{
    public class GameRepository : IGameRepository
    {
        private readonly SqliteConnection _connection;
        public GameRepository(SqliteDatabase db)
        {
            _connection = db.GetConnection();
        }
        public GameState LoadGame()
        {
            using var cmd = _connection.CreateCommand();

            cmd.CommandText = "SELECT * FROM GameState";

            using var reader = cmd.ExecuteReader();

            GameState gamestate = new();

            if (reader.Read())
            {
                gamestate.Turn = reader.GetInt32(0);
                gamestate.CurrentPlayer = reader.GetInt32(2);
                gamestate.Winner = reader.GetInt32(3);
                gamestate.BoardState = GetAllGamePositions();
            }
            else
            {
                Console.WriteLine("problem was encountered while loading game state.");
            }
            
            return gamestate;
        }
        private int[,,] GetAllGamePositions()
        {
            using var cmd = _connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM GamePositions";

            using var reader = cmd.ExecuteReader();
            int[,,] boardState = new int[4, 4, 4];
            if (reader.Read())
            {
                int x = reader.GetInt32(0);
                int y = reader.GetInt32(1);
                int z = reader.GetInt32(2);
                int player = reader.GetInt32(3);
                boardState[x, y, z] = player;
            }
            else
            {
                Debug.WriteLine("problem was encountered while loading game positions.");
            }


            return boardState;
        }

        public void MakeMove(int x, int y, int z, int player, GameState game)//SaveMove might be better name
        {
            try
            {
                //i'm using a transaction here becuse it will group the two commands and ensure that they are both executed together, if one fails the other will not be executed.
                using var transaction = _connection.BeginTransaction();

                UpdatePosition(x, y, z, player, transaction);
                UpdateGameState(game, transaction);

                transaction.Commit();
                //return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[SaveMove] dbfail: {ex}");
                //return false;
            }
        }

        private void UpdatePosition(int x, int y, int z, int player, SqliteTransaction transaction)
        {
            using var cmd = _connection.CreateCommand();
            cmd.Transaction = transaction;
            cmd.CommandText = "UPDATE GamePositions SET Player = @player WHERE x = @x AND y = @y AND z = @z";
            cmd.Parameters.AddWithValue("@x", x);
            cmd.Parameters.AddWithValue("@y", y);
            cmd.Parameters.AddWithValue("@z", z);
            cmd.Parameters.AddWithValue("@player", player);
            cmd.ExecuteNonQuery();
        }

        private void UpdateGameState(GameState game, SqliteTransaction transaction)
        {
            using var cmd = _connection.CreateCommand();
            cmd.Transaction = transaction;
            cmd.CommandText = "UPDATE GameState SET Turn = @turn, CurrentPlayer = @currentPlayer, Outcome = @winner";
            cmd.Parameters.AddWithValue("@currentPlayer", game.CurrentPlayer);
            cmd.Parameters.AddWithValue("@winner", game.Winner);
            cmd.Parameters.AddWithValue("@turn", game.Turn);
            cmd.ExecuteNonQuery();
        }

        public void SaveGame(GameState state)
        {
            try
            {
                using var transaction = _connection.BeginTransaction();

                using (var cmd = _connection.CreateCommand())
                {
                    //i'm using a transaction here becuse it will group the two commands and ensure that they are both executed together, if one fails the other will not be executed.
                    cmd.Transaction = transaction;
                    cmd.CommandText = "UPDATE GameState SET Turn = @turn, CurrentPlayer = @player, Outcome = @winner";
                    cmd.Parameters.AddWithValue("@player", state.CurrentPlayer);
                    cmd.Parameters.AddWithValue("@winner", state.Winner);
                    cmd.Parameters.AddWithValue("@turn", state.Turn);
                    cmd.ExecuteNonQuery();
                }

                SaveGamePositions(state.BoardState, transaction);

                transaction.Commit();
                //return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SaveGame] dbfail: {ex}");
                //return false;
            }
        }
        private void SaveGamePositions(int[,,] boardState, SqliteTransaction transaction)
        {
            using var cmd = _connection.CreateCommand();
            cmd.Transaction = transaction;
            cmd.CommandText = "UPDATE GamePositions SET Player = @player WHERE x = @x AND y = @y AND z = @z";

            var paramX = cmd.Parameters.AddWithValue("@x", 0);
            var paramY = cmd.Parameters.AddWithValue("@y", 0);
            var paramZ = cmd.Parameters.AddWithValue("@z", 0);
            var paramPlayer = cmd.Parameters.AddWithValue("@player", 0);

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        paramX.Value = x;
                        paramY.Value = y;
                        paramZ.Value = z;
                        paramPlayer.Value = boardState[x, y, z];
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void StartNewGame(int playerNum)
        {
            using var cmd = _connection.CreateCommand();
            cmd.CommandText = "UPDATE GamePositions SET Player = 0";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "UPDATE GameState SET CurrentPlayer = 1, Outcome = 0, Turn = 0, PlayerCount = @players";
            cmd.Parameters.AddWithValue("@players", playerNum);
            cmd.ExecuteNonQuery();
        }
    }
}
