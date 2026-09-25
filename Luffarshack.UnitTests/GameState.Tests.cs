using Luffarschack.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Luffarshack.UnitTests
{
    public class GameStateTests
    {

        FakeFixture _fake = new FakeFixture();

        [Theory]
        [InlineData(3, 2, 1, 1)]
        [InlineData(3, 2, 3, 0)]
        [InlineData(2, 0, 1, 2)]
        public void ConfirmBoardHasPieceAtPosition(int x, int y, int z, int _playerNum)
        {
            // Arrange
            var i = 0;
            _fake = new FakeFixture();
            _fake.SetUp();
            
            Move _move = new Move();
            _move.x = x;
            _move.y = y;
            _move.z = z;
            _move.Player = _playerNum.ToString();
            

            // Act
            i = _fake.CheckBoard(_move);
            // Assert
            Assert.Equal(_playerNum, i);
        }
    }
}
