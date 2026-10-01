using System.Numerics;
using System.Text.Json.Serialization;

namespace Luffarschack.Core;

public class GameState
{
    public int[,,] BoardState { get; set; }
    public List<int> Players { get; set; }
    public int CurrentPlayer { get; set; }
    public int Winner { get; set; } 
    public int Turn { get; set; }
}