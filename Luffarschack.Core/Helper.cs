using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
