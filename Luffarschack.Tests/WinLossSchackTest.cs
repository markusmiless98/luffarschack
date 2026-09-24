using Luffarschack.Tests.Placeholder.Board;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Xunit;

namespace Luffarschack.Tests
{
    public class WinLossSchackTest
    {
        private readonly BoardStateHandler _stateHandler = new BoardStateHandler();

        [Theory]
        [InlineData(1, 0, 0, 1)]
        [InlineData(2, 0, 1, 2)]
        public void CheckWinConditionTest(int x, int y, int z, int _playerNum)
        {
            // Arrange
            var i = 0;

            // Act
            _stateHandler.SetSpace(x, y, z, _playerNum);

            // Assert
            i = _stateHandler.GetWinner();
            Assert.Equal(_playerNum, i);
        }

    }
}
