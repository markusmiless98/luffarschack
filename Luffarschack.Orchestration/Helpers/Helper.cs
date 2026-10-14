using System;
using System.Collections.Generic;
using System.Text;
using Luffarschack.Orchestration.dto;

namespace Luffarschack.Core
{
    public static class Helper
    {
        public static int[][][] ToJagged(this int[,,] board)
        {
            int[][][] result = new int[4][][];
            for (int x = 0; x < 4; x++)
            {
                result[x] = new int[4][];
                for (int y = 0; y < 4; y++)
                {
                    result[x][y] = new int[4];
                    for (int z = 0; z < 4; z++)
                        result[x][y][z] = board[x, y, z];
                }
            }
            return result;
        }
        
        public static GameStateDTO ToDto(GameState state) => new()
        {
            BoardState = state.BoardState.ToJagged(),
            CurrentPlayer = state.CurrentPlayer,
            Winner = state.Winner,
            Turn = state.Turn
            
        };

        public static bool FileExistsInProjectDirectory(this string file)
        {
            if (File.Exists(file)) return true; // Really just use file exist at this point
            string dir = Environment.CurrentDirectory;
            dir += file;
            if (File.Exists(dir)) return true;

            return false;
        }
    }
}
