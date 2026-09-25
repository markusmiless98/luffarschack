namespace Luffarschack.Core;

public class Board
{
    public const int Size = 4;
    public Player?[,,] Positions { get; set; } = new Player?[Size, Size, Size];
    private bool IsWithinBounds(int x, int y) => x is >= 0 and < Size && y is >= 0 and < Size;
}