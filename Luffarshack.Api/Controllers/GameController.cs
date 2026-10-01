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
    public GameController(IGameService gameService)
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
    public ActionResult<GameStateDTO> NewGame([FromQuery] int players)
    {
        var result = _gameService.StartGame(players);
        if (result)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetGameState()//IActionResult<GameState>
    {
        var dto = _gameService.GetGameStateDTO();
        return Ok(dto);
    }
    
    

}