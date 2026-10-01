namespace Luffarschack.Core.Services;

public class MoveValidator
{
    public static bool IsValidMove(int[,,] board, int x, int y, int z) => IndexIsNotOutOfRange(x, y, z) && PositionIsEmpty(board, x, y, z);
    private static bool PositionIsEmpty(int[,,] board, int x, int y, int z) => board[x, y, z] == 0;
    private static bool IndexIsNotOutOfRange(int x, int y, int z) => !(x < 0 || x > 3 || y < 0 || y > 3 || z < 0 || z > 3);
}