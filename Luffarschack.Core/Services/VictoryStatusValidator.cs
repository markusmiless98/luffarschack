
namespace Luffarschack.Core.Services;

public class VictoryStatusValidator
{
    const int maxAmountOfTurns = 4 * 4;

    public int CheckGameOutcome(GameState boardState, MoveRequest move)
    {
        return CheckGameOutcome(boardState.BoardState, boardState.Turn, move);
    }

    public int CheckGameOutcome(int[,,] board, int turn, MoveRequest move)
    {
        if (CheckForDraw(turn)) return -1;
        
        return CheckForWinner(board, move);
       
    }
    private static bool CheckForDraw(int turns) => (turns >= maxAmountOfTurns);

    private static int CheckForWinner(int[,,] board, MoveRequest move)
    {
        if (move == null) // In case move request isn't sent
        {
            Console.Error.Write("No Move Request was Given to CheckForWinner()");
            return 0;
        }

        int x = move.x;
        int y = move.y;
        int z = move.z; 

        if (board[x, y, z] != 0) //can this ensure that it not checking the wrong player?
        {
            // Check X directional
            int i = 0;
            int value = board[x, y, z];
            int x_dir_value = 0;
            int y_dir_value = 0;
            int z_dir_value = 0;

            while (i < 4)
            {
                if (board[(x + i) % 4, y, z] == value)
                {
                    x_dir_value++;
                    if (x_dir_value >= 4) return value;
                }
                if (board[x, (y + i) % 4, z] == value)
                {
                    y_dir_value++;
                    if (y_dir_value >= 4) return value;
                }
                if (board[x, y, (z + i) % 4] == value)
                {
                    z_dir_value++;
                    if (z_dir_value >= 4) return value;
                }
                i++;
            }
            if (z_dir_value >= 4 || y_dir_value >= 4 || x_dir_value >= 4) return value; // Backup 

            //check for diagonals in 2d plane
            if (board[0, 0, z] == value && board[1, 1, z] == value && board[2, 2, z] == value && board[3, 3, z] == value) return value;
            if (board[3, 0, z] == value && board[2, 1, z] == value && board[1, 2, z] == value && board[0, 3, z] == value) return value;

            //check for diagonals in 3d plane
            if (board[x, 3, 3] == value && board[x, 2, 2] == value && board[x, 1, 1] == value && board[x, 0, 0] == value) return value;
            if (board[x, 3, 0] == value && board[x, 2, 1] == value && board[x, 1, 2] == value && board[x, 0, 3] == value) return value;

            if (board[3, y, 3] == value && board[2, y, 2] == value && board[1, y, 1] == value && board[0, y, 0] == value) return value;
            if (board[0, y, 3] == value && board[1, y, 2] == value && board[2, y, 1] == value && board[3, y, 0] == value) return value;

            //check diagonals across the 3d board 
            if (board[0, 0, 0] == value && board[1, 1, 1] == value && board[2, 2, 2] == value && board[3, 3, 3] == value) return value;
            if (board[3, 0, 0] == value && board[2, 1, 1] == value && board[1, 2, 2] == value && board[0, 3, 3] == value) return value;
            if (board[0, 3, 0] == value && board[1, 2, 1] == value && board[2, 1, 2] == value && board[3, 0, 3] == value) return value;
            if (board[0, 0, 3] == value && board[1, 1, 2] == value && board[2, 2, 1] == value && board[3, 3, 0] == value) return value;
        }

        return 0;
    }
}

