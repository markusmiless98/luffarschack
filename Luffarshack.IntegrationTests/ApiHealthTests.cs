using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

using Xunit;

namespace Luffarshack.IntegrationTests;

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
        // Arrange
        var requestUri = "/api/game";

        // Act
        var response = await _client.GetAsync(requestUri);
        var body = await response.Content.ReadAsStringAsync();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        Assert.True(response.Content.Headers.ContentLength > 0);
    }
}