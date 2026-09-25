using Luffarschack.Core;

namespace Luffarschack.Orchestration;

public class GameService
{
    public GameState _gameState { get; set; }

    public GameService()
    {
        
    }

    public void MakeMove(Move move)
    {
        throw new NotImplementedException();
        //if(IsValidMove(move))
        //_gameState.BoardState = 
        //if(IsWinner()) {}
        //else{_gameState.Turn ++}
        //
    }

    private void PlaceMove(Move move)
    {
        throw new NotImplementedException();
        //place out the move in the array
        //_gameState.BoardState
        
        //refactor and move method later
    }
    
    private void IsValidMove(Move move)
    {
        throw new NotImplementedException();
        //refactor and move method later
    }

    public void IsWinner()
    {
        throw new NotImplementedException();
        //refactor and move method later
    }

    public void SaveGame()
    {
        throw new NotImplementedException();
        //databasecall via interface
    }
}