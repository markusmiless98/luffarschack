using Luffarschack.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luffarshack.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PieceController
{
    [HttpGet]
    public async Task<Piece> GetPiece()
    {
        var piece = new Piece();
        return piece;
    }
}