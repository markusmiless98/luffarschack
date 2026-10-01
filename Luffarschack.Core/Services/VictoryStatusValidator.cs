
namespace Luffarschack.Core.Services;

public class VictoryStatusValidator
{
    const int maxAmountOfTurns = 4 * 4;

    public int CurrentGameState(int[,,] board, int turn)
    {
        if (CheckForDraw(turn))
        {
            return -1;
        }
        else
        {
            return CheckForWinner(board);
        }
    }
    private static bool CheckForDraw(int turns) => (turns >= maxAmountOfTurns);

    private static int CheckForWinner(int[,,] board)
    {
        int x = 0;
        int y = 0;
        int z = 0;

        while (x < 4)
        {
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
                    if (board[i, y, z] == value)
                    {
                        x_dir_value++;
                        if (x_dir_value >= 4) return value;
                    }
                    if (board[x, i, z] == value)
                    {
                        y_dir_value++;
                        if (y_dir_value >= 4) return value;
                    }
                    if (board[x, y, i] == value)
                    {
                        z_dir_value++;
                        if (z_dir_value >= 4) return value;
                    }
                    i++;
                }
                if (z_dir_value >= 4 || y_dir_value >= 4 || x_dir_value >= 4) return value; // Backup
                if ((x == 0 || x == 3) && (y == 0 || y == 3))
                {
                    if (board[0, 0, z] == value && board[1, 1, z] == value && board[2, 2, z] == value && board[3, 3, z] == value) return value;
                    if (board[3, 0, z] == value && board[2, 1, z] == value && board[1, 2, z] == value && board[0, 3, z] == value) return value;
                }
            }
            if (y < 3)
            {
                y++;
            }
            else
            {
                x++;
                y = 0;
            }
        }

        return 0;
    }
}

