namespace Luffarschack.Core;

public class GameState
{
    public Player[,,] BoardState { get; set; }
    public string[] Players { get; set; }
    public string? Winner { get; set; }
    //public string PlayerTurn { get; set; }
    public int Turn { get; set; }
    public void ApplyMove(Move move) => BoardState[move.x, move.y, move.z] = move.Player;//try catch for out of index exception
}