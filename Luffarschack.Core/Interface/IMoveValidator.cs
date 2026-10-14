namespace Luffarschack.Core.Interface
{
    public interface IMoveValidator
    {
        bool IsValidMove(int[,,] board, int x, int y, int z);
    }
}