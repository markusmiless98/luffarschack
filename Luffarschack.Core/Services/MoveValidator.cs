namespace Luffarschack.Core.Services;

public class MoveValidator
{
    public bool IsValidMove(int[,,] board, int x, int y, int z) => IndexIsNotOutOfRange(x, y, z) && PositionIsEmpty(board, x, y, z);
    private bool PositionIsEmpty(int[,,] board, int x, int y, int z) => board[x, y, z] == 0;
    private bool IndexIsNotOutOfRange(int x, int y, int z) => !(x < 0 || x > 3 || y < 0 || y > 3 || z < 0 || z > 3);
}