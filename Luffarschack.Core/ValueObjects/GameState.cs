namespace Luffarschack.Core;

public class GameState
{
    public int[,,] BoardState { get; set; }
    public List<string> Players { get; set; }
    public string Winner { get; set; } = string.Empty;
    //public string PlayerTurn { get; set; }
    public int Turn { get; set; }
    //public void ApplyMove(Move move) => BoardState.Positions[move.x, move.y, move.z] = move.Player;//check IsWithinBounds with if statement
}