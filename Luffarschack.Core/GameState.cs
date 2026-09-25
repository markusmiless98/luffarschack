namespace Luffarschack.Core;

public class GameState
{
    public int[,,] BoardState { get; set; }
    public string[] Players { get; set; }
    public string? Winner { get; set; }
    //public string PlayerTurn { get; set; }
    public int Turn { get; set; }
}