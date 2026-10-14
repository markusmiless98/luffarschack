using Luffarschack.Core;
using Luffarschack.Orchestration.dto;
using Luffarschack.Orchestration.Interface;

using System.Text.Json;
using Luffarschack.Core.Services;
using Luffarschack.Core.Interface;
using System.Diagnostics;

namespace Luffarschack.Orchestration;

public class GameService : IGameService
{
    public GameState GameState { get; set; } 
    private IGameRepository _gameRepo { get; set; }
    private IMoveValidator _moveValidator { get; set; }
    private IVictoryStatusValidator _victoryValidator { get; set; }
    private IGameBuilder _gameBuilder { get; set; }

    public GameService(IGameRepository repo, IMoveValidator moveValidator, IGameBuilder gameBuilder, IVictoryStatusValidator victoryValidator)
    {
        if (repo != null && moveValidator != null && gameBuilder != null)
        {
            _gameRepo = repo;
            _moveValidator = moveValidator;
            _victoryValidator = victoryValidator;
            _gameBuilder = gameBuilder;
            GameState = _gameRepo.LoadGame();
        }
        else
        {
            Init();
        }
    }
    public GameService()
    {
        Init();
    }

    private void Init()
    {
        //initiation of gamestate and validators for tests/fallback if somethething goes wrong?
        GameState = new GameState()
        {
            BoardState = new int[4, 4, 4],
            CurrentPlayer = 1,
            Players = new List<int> { 1, 2 },
            Turn = 0,
            Winner = 0,
        };
        _moveValidator = new MoveValidator();
        _victoryValidator = new VictoryStatusValidator();
        _gameBuilder = new GameBuilder();
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
        
        if (_moveValidator.IsValidMove(GameState.BoardState, move.x, move.y, move.z))
        {
            try //this try catch is in case i've missed something in the validation, it should not crash the program
            {
                PlaceMove(move);
                GameState.Turn++;
                GameState.Winner = _victoryValidator.CheckGameOutcome(GameState, move);
                GoToNextPlayer();
                SaveGame();
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

    private void PlaceMove(MoveRequest move)
    {
        GameState.BoardState[move.x, move.y, move.z] = GameState.CurrentPlayer;
        if (_gameRepo != null)
        {
            _gameRepo.MakeMove(move.x,move.y,move.z,GameState.CurrentPlayer,GameState);
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
        if (_gameRepo != null)
        {
            _gameRepo.SaveGame(GameState);
        }
        //databasecall via interface
    }

    public bool StartGame(int players)
    {
        if (players < 2 || players > 4) return false;
        
        GameState = _gameBuilder.NewGame(players);
        if (_gameRepo != null)
        {
            _gameRepo.StartNewGame(players);
        }
        return true;
    }
}