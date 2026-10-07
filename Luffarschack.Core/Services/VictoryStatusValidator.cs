
namespace Luffarschack.Core.Services;

public class VictoryStatusValidator
{
    const int maxAmountOfTurns = 4 * 4;

    //any comment that starts with //> is a comment about what i changed
    public int CheckGameOutcome(GameState gameState, MoveRequest move) //>renamed boardState to gameState becuse its more clear
    {
        //>move the null check up so it wont even try if the move request is null
        if (move == null) // In case move request isn't sent
        {
            Console.Error.Write("No Move Request was Given to CheckGameOutcome()");
            return 0;
        }

        //> you can get the player from the gameState
        return CheckGameOutcome(gameState.BoardState, gameState.Turn, gameState.CurrentPlayer, move.x, move.y, move.z);
    }

    public int CheckGameOutcome(int[,,] board, int player, int turn, int x, int y, int z) //> changed the args here so that its easyer to test as an individual unit
    {
        if (board[x, y, z] != player) return 0;//> this code to ensures that the player in the GameState is the one that did the move, should probably return an error, or print an error

        if (CheckForDraw(turn)) return -1;
        
        return CheckForWinner(board, player, x, y, z);//> maybe check for winner returns bool and then the CheckGameOutcome returns the player value if true

    }
    private static bool CheckForDraw(int turns) => (turns >= maxAmountOfTurns);

    private static int CheckForWinner(int[,,] board, int player, int x, int y, int z)//> changed MoveRequest arg to x, y, z //i also added player as an in arg
    {

        //> i think its not the responcibility of the CheckIfWinner method to ensure that the value on the move is not 0 and it shuld be checked before the method is used
        
        // Check X directional
        int x_dir_value = 0;
        int y_dir_value = 0;
        int z_dir_value = 0;

        for(int i = 0; i < 4; i++) //>i think a for loop is better here
        {
            if (board[(x + i) % 4, y, z] == player)//>i added the player so that it can check against that instead of the value on the move position
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

