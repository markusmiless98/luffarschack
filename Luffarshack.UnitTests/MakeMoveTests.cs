using Luffarschack.Core;
using Luffarschack.Orchestration;
using Xunit;

namespace Luffarshack.UnitTests;

public class MakeMoveTests
{

    [Fact]
    public void MakeMove_EmptyCell_ReturnsTrue()
    {
        var service = new GameService();
        service.StartGame(2);

        var result = service.MakeMove(new MoveRequest { x = 1, y = 2, z = 3 });

        Assert.True(result);
    }

    [Fact]
    public void MakeMove_EmptyCell_PlacesCurrentPlayer()
    {
        var service = new GameService();
        service.StartGame(2);

        var result = service.MakeMove(new MoveRequest{x = 1, y = 2, z = 3});

        Assert.Equal(1, service.GameState.BoardState[1, 2, 3]);
    }

    [Fact]
    public void MakeMove_EmptyCell_AdvancesTurn()
    {
        var service = new GameService();
        service.StartGame(2);

        var result = service.MakeMove(new MoveRequest { x = 1, y = 2, z = 3 });

        Assert.Equal(1, service.GameState.Turn);
    }

    [Fact]
    public void MakeMove_EmptyCell_SetsCorrectCurrentPlayer()
    {
        var service = new GameService();
        service.StartGame(2);

        service.MakeMove(new MoveRequest { x = 1, y = 2, z = 3 });
        service.MakeMove(new MoveRequest { x = 0, y = 0, z = 0 });

        Assert.Equal(2, service.GameState.CurrentPlayer);
    }


    [Fact]
    public void MakeMove_OccupiedCell_ReturnsFalse()
    {
        var service = new GameService();
        service.StartGame(2);
        service.MakeMove(new MoveRequest { x = 0, y = 0, z = 0 });

        var result = service.MakeMove(new MoveRequest { x = 0, y = 0, z = 0 });

        Assert.False(result);
    }

    [Fact]
    public void MakeMove_OccupiedCell_DoesNotAdvanceTurn()
    {
        var service = new GameService();
        service.StartGame(2);
        service.MakeMove(new MoveRequest { x = 0, y = 0, z = 0 }); 

        service.MakeMove(new MoveRequest { x = 0, y = 0, z = 0 });

        Assert.Equal(1, service.GameState.Turn);           
    }

    [Fact]
    public void MakeMove_OccupiedCell_DoesNotChangeBoard()
    {
        var service = new GameService();
        service.StartGame(2);
        service.MakeMove(new MoveRequest { x = 0, y = 0, z = 0 });

        service.MakeMove(new MoveRequest { x = 0, y = 0, z = 0 });

        Assert.Equal(1, service.GameState.BoardState[0, 0, 0]);
        //other assertions can be added to check that other cells remain unchanged
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