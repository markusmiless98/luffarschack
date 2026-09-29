using Luffarschack.Core;
using Luffarschack.Orchestration.dto;
using Luffarschack.Orchestration.Interface;

using System.Text.Json;

namespace Luffarschack.Orchestration;

public class GameService : IGameService
{
    public GameState GameState { get; set; } 

    public GameService()
    {
        //temporaray initilization of gamestate
        GameState = new GameState()
        {
            BoardState = new int[4,4,4],
            CurrentPlayer = 1, 
            Players = new List<int> {1,2},
            Turn = 0,
            Winner = 0,
        };
    }

    public GameStateDTO GetGameStateDTO()//returnera GameState dto Object
    {
        GameStateDTO dto = Helper.ToDto(GameState);
        return dto;
    }
    
    public bool MakeMove(MoveRequest move)//maybe return a status code instead of bool? becuse then it can return diffrent responses and tell frontend more about what went wrong
    {
        //this is a temporary try catch, it should have more valid checks but for now this ensures that the program does not crash
        try
        {
            GameState.BoardState[move.x, move.y, move.z] = GameState.CurrentPlayer;
            //need a method to change current player to next player
            //need a win check as well
            //turn ++
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void ChangePlayer(int player)//maybe try change player? what could go wrong here...
    {
        
    }

    public void SaveGame()
    {
        throw new NotImplementedException();
        //databasecall via interface
    }
}