using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Luffarschack.Core;

namespace Luffarshack.UnitTests
{
    public class MoveValidatorTests
    {
        FakeFixture _fake = new FakeFixture();

        [Theory]
        [InlineData(0, 0, 0, 0)]
        [InlineData(1, 1, 0, 1)]
        [InlineData(3, 0, 0, 1)]
        public void CanPerformMove(int x, int y, int z, int _playerNum)
        {
            // Arrange
            var i = 0;
            _fake = new FakeFixture();
            _fake.SetUp();
            Move _move = new Move();
            _move._x = x;
            _move._y = y;
            _move._z = z;
            _move.Player = _playerNum.ToString();

            // Act
            i = _fake.PerformMove(_move);

            // Assert
            Assert.Equal(_playerNum, i);
        }
    }
}
