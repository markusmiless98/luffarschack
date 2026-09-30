using Luffarschack.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Luffarschack.Orchestration;
using Xunit;

namespace Luffarshack.UnitTests
{
    public class GameStateTests
    {
        private GameService _sut = new GameService(); //system under test
        
        [Theory]
        [InlineData(3, 2, 1, 1)]
        [InlineData(3, 2, 3, 0)]
        [InlineData(2, 0, 1, 2)]
        public void ConfirmBoardHasPieceAtPosition(int x, int y, int z, int expected)
        {
            // Arrange
            var mr = new MoveRequest{x = x, y = y, z = z};
            _sut.GameState.CurrentPlayer = expected;
            _sut.MakeMove(mr);
            
            // Act
            var actual = _sut.GameState.BoardState[x, y, z];
            
            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
