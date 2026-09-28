using Luffarschack.Core;
using Microsoft.AspNetCore.Mvc;

namespace Luffarshack.Api.Controllers;

[ApiController]

[Route("api/game")] 

public class GameController : ControllerBase
{
    
    [HttpPost] //api/game
    public async Task<IActionResult> PostMoveRequest([FromBody] MoveRequest request)
    {
        return Ok();
    }
}