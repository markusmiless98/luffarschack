using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Luffarschack.Core;
using Luffarschack.Core.Services;

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
            _move.x = x;
            _move.y = y;
            _move.z = z;
            _move.Player = _playerNum.ToString();

            // Act
            i = _fake.PerformMove(_move);

            // Assert
            Assert.Equal(_playerNum, i);
        }
        
        [Fact]
        public void IsValidMove_EmptyCell_ReturnsTrue()
        {
            //arrange
            var board = new int[4,4,4];

            //act
            var result = MoveValidator.IsValidMove(board, 0, 0, 0);

            //assert
            Assert.True(result);  
        }

        [Fact]
        public void IsValidMove_OccupiedCell_ReturnsFalse()
        {
            //arrange
            var board = new int[4, 4, 4];
            board[0, 0, 0] = 1;
            
            //act
            var result = MoveValidator.IsValidMove(board, 0, 0, 0);

            //assert
            Assert.False(result);  
        }
        
        [Theory]
        [InlineData(4, 0, 0)]
        [InlineData(-1, 0, 0)]
        public void IsMove_OutOfBounds_ReturnsFalse( int x, int y, int z)
        {
            //arrange
            var board = new int[4, 4, 4];
            
            //act
            var result = MoveValidator.IsValidMove(board, x, y, z);

            //assert
            Assert.False(result);  
        }
    }
}
