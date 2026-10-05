using Luffarschack.Core;
using Luffarschack.Orchestration;
using Xunit;

namespace Luffarshack.UnitTests;

public class MakeMoveTests
{

    [Fact]
    public void MakeMove_EmptyCell_ReturnsTrue()
    {
        // arrange
        var service = new GameService();
        service.StartGame(2);
        // act
        var result = service.MakeMove(new MoveRequest { x = 1, y = 2, z = 3 });
        // assert
        Assert.True(result); // Result was successful
        Assert.Equal(1, service.GameState.BoardState[1, 2, 3]); // player one is at this place
    }

    [Fact]
    public void MakeMove_EmptyCell_AdvancesTurn()
    {
        var service = new GameService();
        service.StartGame(2);

        var result = service.MakeMove(new MoveRequest { x = 1, y = 2, z = 3 });

        Assert.Equal(1, service.GameState.Turn); // Checks turn swapped
        Assert.Equal(2, service.GameState.CurrentPlayer); // checks it is player 2's turn (which is part of turn passing)
    }


    [Fact]
    public void MakeMove_OccupiedCell_DoesNotPlaceOrAdvanceTurn()
    {
        var service = new GameService();
        service.StartGame(2);
        service.MakeMove(new MoveRequest { x = 0, y = 0, z = 0 });

        var result = service.MakeMove(new MoveRequest { x = 0, y = 0, z = 0 });

        Assert.False(result); // Player failed to perform action
        Assert.Equal(1, service.GameState.Turn); // Turn doesn't advance past 1
        Assert.Equal(1, service.GameState.BoardState[0, 0, 0]); // Doesn't change the board
    }

    [Theory]
    [InlineData(-1, 0, 0)]
    [InlineData(4, 0, 0)]
    [InlineData(0, -1, 0)]
    [InlineData(0, 4, 0)]
    [InlineData(0, 0, -1)]
    [InlineData(0, 0, 4)]
    public void MakeMove_OutOfBounds_ReturnsFalse(int x, int y, int z)
    {
        var service = new GameService();
        service.StartGame(2);

        var result = service.MakeMove(new MoveRequest { x = x, y = y, z = z });

        Assert.False(result);
    }
}