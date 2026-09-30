using Luffarschack.Core;
using Luffarschack.Orchestration.dto;
using Luffarschack.Orchestration.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Luffarshack.Api.Controllers;

[ApiController]

[Route("api/game")] 

public class GameController : ControllerBase
{
    private readonly IGameService _gameService;
    public GameController(IGameService gameService)//fixa DI
    {
        _gameService = gameService;
    }
    
    [HttpPost] //api/game
    public async Task<IActionResult> PostMoveRequest([FromBody] MoveRequest moveRequest)
    {
        var result =_gameService.MakeMove(moveRequest);
        if (result)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
        
    }
    
    [HttpPost("new")] 
    public ActionResult<GameStateDTO> NewGame()
    {
        //activate method from GameService that starts new game 
        //what data does backend need from frontend to start a new game ouside of just the request?
        //the amount of players? could make it two by default for now but its worth thinking of later
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetGameState()//IActionResult<GameState>
    {
        var dto = _gameService.GetGameStateDTO();
        return Ok(dto);
    }
    
    

}