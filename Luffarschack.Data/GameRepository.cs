using Luffarschack.Core;
using Luffarschack.Core.Interface;
using Microsoft.Data.Sqlite;
using System.Text.RegularExpressions;
using System.Threading.Channels;

namespace Luffarschack.Database
{
    public class GameRepository : IGameRepository
    {
        private readonly SQLiteDataBase _db = SQLiteDataBase.GetDb();
        private readonly SqliteConnection _connection;
        public GameRepository()
        {
            _connection = SQLiteDataBase.GetDb().GetConnection();
        }
        public GameState LoadGame()
        {
            using var cmd = _connection.CreateCommand();

            cmd.CommandText = "SELECT * FROM GameState";

            using var reader = cmd.ExecuteReader();

            GameState gamestate = new();
 
            gamestate.CurrentPlayer = reader.GetInt32(1);
            gamestate.Winner = reader.GetInt32(2);
            gamestate.BoardState = getAllGamePositions();
            
            return gamestate;
        }
        private int[,,] getAllGamePositions()
        {
            using var cmd = _connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM GamePositions";

            using var reader = cmd.ExecuteReader();
            int[,,] boardState = new int[4, 4, 4];
            while (reader.Read())
            {
                int x = reader.GetInt32(0);
                int y = reader.GetInt32(1);
                int z = reader.GetInt32(2);
                int player = reader.GetInt32(3);
                boardState[x, y, z] = player;
            }
            return boardState;
        }

        public void MakeMove(int x, int y, int z, int player, GameState game)
        {
            UpdatePosition(x, y, z, player);
            UpdateGameState(game);
        }
        private void UpdatePosition(int x, int y, int z, int player)
        {
            using var cmd = _connection.CreateCommand();
            cmd.CommandText = "UPDATE GamePositions SET Player = @player WHERE x = @x AND y = @y AND z = @z";
            cmd.Parameters.AddWithValue("@x", x);
            cmd.Parameters.AddWithValue("@y", y);
            cmd.Parameters.AddWithValue("@z", z);
            cmd.Parameters.AddWithValue("@player", player);
            cmd.ExecuteNonQuery();
        }
        private void UpdateGameState(GameState game)
        {
            using var cmd = _connection.CreateCommand();
            cmd.CommandText = "UPDATE GameState SET CurrentPlayer = @currentPlayer, Outcome = @winner, Turn = @turn";
            cmd.Parameters.AddWithValue("@currentPlayer", game.CurrentPlayer);
            cmd.Parameters.AddWithValue("@winner", game.Winner);
            cmd.Parameters.AddWithValue("@turn", game.Turn);
            cmd.ExecuteNonQuery();
        }

        public void SaveGame(GameState state)
        {
            using var cmd = _connection.CreateCommand();
            cmd.CommandText = "UPDATE GameState SET CurrentPlayer = @player, Outcome = @winner";
            cmd.Parameters.AddWithValue("@player", state.CurrentPlayer);
            cmd.Parameters.AddWithValue("@winner", state.Winner);
            cmd.Parameters.AddWithValue("@turn", state.Turn);

            cmd.ExecuteNonQuery();

            SaveGamePositions(state.BoardState);
        }
        private void SaveGamePositions(int[,,] boardState)
        {
            using var cmd = _connection.CreateCommand();
            cmd.CommandText = "UPDATE GamePositions SET Player = @player WHERE x = @x AND y = @y AND z = @z";
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        cmd.Parameters.Clear();//clear previous parameters before adding new ones
                        cmd.Parameters.AddWithValue("@x", x);
                        cmd.Parameters.AddWithValue("@y", y);
                        cmd.Parameters.AddWithValue("@z", z);
                        cmd.Parameters.AddWithValue("@player", boardState[x, y, z]);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void StartNewGame(int playerNum)
        {
            throw new NotImplementedException();
        }
    }
}
