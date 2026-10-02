
namespace Luffarschack.Core.Services;

public class VictoryStatusValidator
{
    const int maxAmountOfTurns = 4 * 4;

    public int CurrentGameState(GameState boardState, MoveRequest move)
    {
        return CurrentGameState(boardState.BoardState, boardState.Turn, move);
    }

    public int CurrentGameState(int[,,] board, int turn, MoveRequest move)
    {
        if (CheckForDraw(turn))
        {
            return -1;
        }
        else
        {
            return CheckForWinner(board, move);
        }
    }
    private static bool CheckForDraw(int turns) => (turns >= maxAmountOfTurns);

    private static int CheckForWinner(int[,,] board, MoveRequest move)
    {
        int x = move.x;
        int y = move.y;
        int z = move.z;

        if (move == null) // In case move request isn't sent
        {
            Console.Error.Write("No Move Request was Given to CheckForWinner()");
            return 0;
        }

        if (board[x, y, z] != 0)
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
                if (board[x, y, (y + i) % 4] == value)
                {
                    z_dir_value++;
                    if (z_dir_value >= 4) return value;
                }
                i++;
            }
            if (z_dir_value >= 4 || y_dir_value >= 4 || x_dir_value >= 4) return value; // Backup
            if (board[0, 0, z] == value && board[1, 1, z] == value && board[2, 2, z] == value && board[3, 3, z] == value) return value;
            if (board[3, 0, z] == value && board[2, 1, z] == value && board[1, 2, z] == value && board[0, 3, z] == value) return value;
            // Diagonals will be annoying in 3d so will be done much later lol
        }

        return 0;
    }
}

