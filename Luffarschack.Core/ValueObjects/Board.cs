namespace Luffarschack.Core;

public class Board
{
    public const int Size = 4;
    public int[,,] Positions { get; set; } = new int[Size, Size, Size];
    private bool IsWithinBounds(int x, int y) => x is >= 0 and < Size && y is >= 0 and < Size;
}