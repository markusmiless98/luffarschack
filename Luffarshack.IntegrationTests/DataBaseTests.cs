using Luffarschack.Core;
using Luffarschack.Core.Interface;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;

public class DataBaseTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly IGameRepository _repos;
    private readonly HttpClient _client;

    public DataBaseTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task SaveAndLoadGame_WithRepository_SuccessfulLoad()
    {
        GameState gameState = new GameState();
        gameState.BoardState[1, 1, 1] = 1;
        gameState.Turn = 1;
        gameState.CurrentPlayer = 2;
        gameState.Players.Add(1);
        gameState.Players.Add(2);

        _repos.SaveGame(gameState);
        GameState fake = _repos.LoadGame(); // Maybe make into string later

        Assert.True(fake != null);
        Assert.True(fake.CurrentPlayer == 2);
        Assert.True(fake.Turn == 1);
        Assert.True(fake.BoardState[1,1,1] == 1);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public async Task StartNewGame_Successful_IsNotNull(int players)
    {
        _repos.StartNewGame(players);
        GameState fake = _repos.LoadGame(); // Maybe make into string later

        Assert.True(fake != null);
        Assert.True(fake.Players.Count == players);
    }
}
