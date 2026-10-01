namespace Luffarschack.Core.Services;

public static class GameBuilder
{
    public static GameState NewGame(int playerAmount)
    {
        var players = Enumerable.Range(1, playerAmount).ToList(); 
        
        return new GameState
        {
            BoardState = new int[4,4,4],
            Players = players,
            CurrentPlayer = 1,
            Winner = 0,
            Turn = 0
        };
    }

}