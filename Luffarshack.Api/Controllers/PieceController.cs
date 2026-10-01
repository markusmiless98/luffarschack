using Luffarschack.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
    [HttpPost]
    public ActionResult<Piece> PostPiece([FromBody] Piece data)
    {
        int _x = -1;
        int _y = -1;
        int _z = -1;

        if (data != null)
        {
            _x = data._x;
            _y = data._y;
            _z = data._z;
        }

        var piece = new Piece(_x,_y,_z);

        return piece;
    }
}