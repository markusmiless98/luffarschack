namespace Luffarschack.Core;

public class GameState
{
    public int[,,] BoardState { get; set; }
    public List<int> Players { get; set; }
    public int CurrentPlayer { get; set; }
    public bool IsWinner { get; set; } 
    public int Turn { get; set; }
}