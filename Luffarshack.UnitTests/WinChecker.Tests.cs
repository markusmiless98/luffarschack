using Luffarschack.Core;
using Luffarschack.Core.Services;
using Luffarschack.Orchestration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xunit;

namespace Luffarshack.UnitTests
{
    public class WinCheckerTests
    {
        [Fact]
        public void EndGame_ValidAnyPlayerVictory_ReturnsTrue()
        {
            //arrange
            var service = new GameService();
            var pop = new VictoryStatusValidator();

            //act
            service.StartGame(2);
            MoveRequest move = new MoveRequest(0, 0, 0);
            service.MakeMove(move);
            move = new MoveRequest(2, 2, 0);
            service.MakeMove(move);
            move = new MoveRequest(1, 0, 0);
            service.MakeMove(move);
            move = new MoveRequest(3, 1, 0);
            service.MakeMove(move);
            move = new MoveRequest(2, 0, 0);
            service.MakeMove(move);
            move = new MoveRequest(3, 2, 0);
            service.MakeMove(move);
            move = new MoveRequest(3, 0, 0);
            service.MakeMove(move);

            int i = pop.CurrentGameState(service.GameState.BoardState, service.GameState.Turn);

            //assert
            Assert.True(i > 0);
        }
        [Theory]
        [InlineData(3,0,0,1)] // Checks if player 1 won
        [InlineData(1, 2, 0, 0)] // Checks in case it didn't happen
        public void EndGame_ValidGameState_ReturnsTrue(int x, int y, int z, int player)
        {
            //arrange
            var service = new GameService();
            var pop = new VictoryStatusValidator();
            
            //act
            service.StartGame(2);
            MoveRequest move = new MoveRequest(0,0,0);
            service.MakeMove(move);
            move = new MoveRequest(2, 2, 0);
            service.MakeMove(move);
            move = new MoveRequest(1, 0, 0);
            service.MakeMove(move);
            move = new MoveRequest(3, 1, 0);
            service.MakeMove(move);
            move = new MoveRequest(2, 0, 0);
            service.MakeMove(move);
            move = new MoveRequest(3, 2, 0);
            service.MakeMove(move);
            move = new MoveRequest(x, y, z);
            service.MakeMove(move);

            int i = pop.CurrentGameState(service.GameState.BoardState,service.GameState.Turn);

            //assert
            Assert.Equal(player,i);
        }
        [Fact]
        public void EndGame_DrawState_ReturnsTrue()
        {
            //arrange
            var service = new GameService();
            var pop = new VictoryStatusValidator();

            //act
            service.StartGame(2);
            MoveRequest move = new MoveRequest();
            service.MakeMove(move);
            int x = 0;
            int y = 0;
            while (y <= 3) // Just to not write out a whole 16 turns this is done
            {
                while (x <= 3)
                {
                    if (y == 0)
                    {
                        move = new MoveRequest(3 - x, y, 0);
                    }
                    else
                    {
                        move = new MoveRequest(x, y, 0);
                    }
                    service.MakeMove(move);
                    x++;
                }
                y++;
                x = 0;
            }


            int i = pop.CurrentGameState(service.GameState.BoardState, service.GameState.Turn);

            //assert
            Assert.Equal(-1, i);
        }
    }
}
