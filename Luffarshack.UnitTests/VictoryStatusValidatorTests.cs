using Luffarschack.Core;
using Luffarschack.Core.Services;
using Luffarschack.Orchestration;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Luffarshack.UnitTests
{
    public class VictoryStatusValidatorTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)] 
        public void VictoryStatusValidator_ValidAnyPlayerVictory_ReturnsPlayerNumber(int player)
        {
            //arrange
            var validator = new VictoryStatusValidator();
            var board = new int[4, 4, 4];
            board[0,0,0] = player;
            board[1,0,0] = player;
            board[2,0,0] = player;
            board[3,0,0] = player;
            int turn = 7; 
            int x = 3, y = 0, z = 0;

            //act

            var actual = validator.CheckGameOutcome(board, player, turn, x, y, z);

            //assert
            Assert.Equal(actual, player);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void VictoryStatusValidator_FourInARowAcrossCube_ReturnsPlayerWinningNumber(int player)
        {
            //arrange
            var validator = new VictoryStatusValidator();
            var board = new int[4, 4, 4];
            board[0, 0, 0] = player;
            board[1, 1, 1] = player;
            board[2, 2, 2] = player;
            board[3, 3, 3] = player;
            int turn = 7;
            int x = 0, y = 0, z = 0; 

            //act

            var actual = validator.CheckGameOutcome(board, player, turn, x, y, z);

            //assert
            Assert.Equal(actual, player);
        }
    }
}
