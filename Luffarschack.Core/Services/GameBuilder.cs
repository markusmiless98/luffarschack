using Luffarschack.Core.Interface;

namespace Luffarschack.Core.Services;

public class GameBuilder : IGameBuilder
{
    public GameState NewGame(int playerAmount)
    {
        var players = Enumerable.Range(1, playerAmount).ToList();

        return new GameState
        {
            BoardState = new int[4, 4, 4],
            Players = players,
            CurrentPlayer = 1,
            Winner = 0,
            Turn = 0
        };
    }

}