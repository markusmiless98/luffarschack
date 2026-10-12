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
    
    [Fact]
    public async Task MakeMoveLoadGame_WithRepository_SuccessfulLoad()
    {
        // Assemble
        GameState gameState = new GameState();
        _repos.MakeMove(1, 1, 1, 1, gameState);

        // Act
        GameState fake = _repos.LoadGame(); // Maybe make into string later

        // Assert
        Assert.NotNull(fake);
        Assert.Equal(1, fake.BoardState[1, 1, 1]);
    }
    [Fact]
    public async Task SaveAndLoadGame_WithRepository_SuccessfulLoad()
    {
        // Assemble
        GameState gameState = new GameState();
        gameState.BoardState[1, 1, 1] = 1;
        gameState.Turn = 1;
        gameState.CurrentPlayer = 2;
        gameState.Players.Add(1);
        gameState.Players.Add(2);
        // Act
        _repos.SaveGame(gameState);
        GameState fake = _repos.LoadGame(); // Maybe make into string later
        // Assert
        Assert.NotNull(fake);
        Assert.Equal(2, fake.CurrentPlayer);
        Assert.Equal(1,fake.Turn);
        Assert.Equal(1, fake.BoardState[1,1,1]);
    }

    [Fact]
    public async Task Save_WithRepository_ChangeOccured()
    {
        // Assemble
        GameState gameStateOriginal = _repos.LoadGame();
        _repos.MakeMove(1, 1, 1, 1, gameStateOriginal);
        // Act
        GameState gameStateNew = _repos.LoadGame();

        // Assert
        Assert.NotEqual(gameStateOriginal, gameStateNew);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public async Task StartNewGame_Successful_IsNotNull(int players)
    {
        // Assemble
        _repos.StartNewGame(players);
        // Act
        GameState fake = _repos.LoadGame(); // Maybe make into string later
        // Assert
        Assert.NotNull(fake);
        Assert.Equal(fake.Players.Count, players);
    }
}
