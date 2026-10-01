using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Luffarshack.UnitTests;

public class EndPointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    
    public EndPointsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task PostNewGame_WithValidPlayerCount_ReturnsOk()
    {
        var response = await _client.PostAsync("/api/game/new?players=2", null);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task PostNewGame_WithInvalidPlayerCount_ReturnsBadRequest()
    {
        var response = await _client.PostAsync("/api/game/new?players=0", null);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}