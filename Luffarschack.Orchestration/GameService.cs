using Luffarschack.Core;
using Luffarschack.Orchestration.dto;
using Luffarschack.Orchestration.Interface;

using System.Text.Json;
using Luffarschack.Core.Services;

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

    public GameStateDTO GetGameStateDTO()
    {
        GameStateDTO dto = Helper.ToDto(GameState);
        return dto;
    }
    
    public bool MakeMove(MoveRequest move)
    {
        if (GameState == null)
            return false;
        
        if (MoveValidator.IsValidMove(GameState.BoardState, move.x, move.y, move.z))
        {
            try //this try catch is in case i've missed something in the validation, it should not crash the program
            {
                GameState.BoardState[move.x, move.y, move.z] = GameState.CurrentPlayer;
                GameState.Turn++;
                GoToNextPlayer();
                return true;
            }
            catch
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void GoToNextPlayer()
    {
        int currentIndex = GameState.Players.IndexOf(GameState.CurrentPlayer);
        int nextIndex = (currentIndex + 1) % GameState.Players.Count;
        GameState.CurrentPlayer = GameState.Players[nextIndex];
    }

    public void SaveGame()
    {
        throw new NotImplementedException();
        //databasecall via interface
    }

    public bool StartGame(int players)
    {
        if (players < 1 || players > 4) // player range
            return false;
        
        GameState = GameBuilder.NewGame(players);
        return true;
    }
}