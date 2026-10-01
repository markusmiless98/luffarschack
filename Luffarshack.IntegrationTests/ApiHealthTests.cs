using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

using Xunit;

namespace Luffarshack.UnitTests;

public class ApiHealthTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    
    public ApiHealthTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task PostMove_WithJsonBody_IsAccepted()
    {
        var response = await _client.PostAsJsonAsync("api/game", new { x = 1, y = 2, z = 3 });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}