
namespace Luffarschack.Core.Services;

public class VictoryStatusValidator
{
    const int maxAmountOfTurns = 4 * 4;
    public int CheckGameOutcome(GameState gameState, MoveRequest move)
    {
        if (move == null) // In case move request isn't sent
        {
            Console.Error.Write("No Move Request was Given to CheckGameOutcome()");
            return 0;
        }

        return CheckGameOutcome(gameState.BoardState, gameState.CurrentPlayer, gameState.Turn, move.x, move.y, move.z);
    }

    public int CheckGameOutcome(int[,,] board, int player, int turn, int x, int y, int z) 
    {
        if (CheckForDraw(turn)) return -1;

        if (board[x, y, z] != player) return 0;

        return CheckForWinner(board, player, x, y, z);

    }
    private static bool CheckForDraw(int turns) => (turns >= maxAmountOfTurns);

    private static int CheckForWinner(int[,,] board, int player, int x, int y, int z)
    {
        
        // Check X directional
        int x_dir_value = 0;
        int y_dir_value = 0;
        int z_dir_value = 0;

        for(int i = 0; i < 4; i++) 
        {
            if (board[(x + i) % 4, y, z] == player)
            {
                x_dir_value++;
                if (x_dir_value >= 4) return player;
            }
            if (board[x, (y + i) % 4, z] == player)
            {
                y_dir_value++;
                if (y_dir_value >= 4) return player;
            }
            if (board[x, y, (z + i) % 4] == player)
            {
                z_dir_value++;
                if (z_dir_value >= 4) return player;
            }
        }
        if (z_dir_value >= 4 || y_dir_value >= 4 || x_dir_value >= 4) return player; // Backup 

        //check for diagonals in 2d plane
        if (board[0, 0, z] == player && board[1, 1, z] == player && board[2, 2, z] == player && board[3, 3, z] == player) return player;
        if (board[3, 0, z] == player && board[2, 1, z] == player && board[1, 2, z] == player && board[0, 3, z] == player) return player;

        //check for diagonals in 3d plane
        if (board[x, 3, 3] == player && board[x, 2, 2] == player && board[x, 1, 1] == player && board[x, 0, 0] == player) return player;
        if (board[x, 3, 0] == player && board[x, 2, 1] == player && board[x, 1, 2] == player && board[x, 0, 3] == player) return player;

        if (board[3, y, 3] == player && board[2, y, 2] == player && board[1, y, 1] == player && board[0, y, 0] == player) return player;
        if (board[0, y, 3] == player && board[1, y, 2] == player && board[2, y, 1] == player && board[3, y, 0] == player) return player;

        //check diagonals across the 3d board 
        if (board[0, 0, 0] == player && board[1, 1, 1] == player && board[2, 2, 2] == player && board[3, 3, 3] == player) return player;
        if (board[3, 0, 0] == player && board[2, 1, 1] == player && board[1, 2, 2] == player && board[0, 3, 3] == player) return player;
        if (board[0, 3, 0] == player && board[1, 2, 1] == player && board[2, 1, 2] == player && board[3, 0, 3] == player) return player;
        if (board[0, 0, 3] == player && board[1, 1, 2] == player && board[2, 2, 1] == player && board[3, 3, 0] == player) return player;
        
        return 0;
    }
}

