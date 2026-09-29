namespace Luffarschack.Core.Services;

public class GameBuilder
{
    public GameState GetNewGame(int playerAmount)
    {
        var players = Enumerable.Range(1, playerAmount).ToList(); 
        
        return new GameState
        {
            BoardState = new int[4,4,4],
            Players = players,
            CurrentPlayer = 1,
            IsWinner = false,
            Turn = 0
        };
    }

}