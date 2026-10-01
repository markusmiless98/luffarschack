using Luffarschack.Orchestration;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Luffarshack.UnitTests
{
    public class StartGameTests
    {
        [Theory]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(4)]
        public void StartGame_ValidPlayerCount_ReturnsTrue(int players)
        {
            //arrange
            var service = new GameService();

            //act
            var result = service.StartGame(players);

            //assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(5)]
        public void StartGame_InvalidPlayerCount_ReturnsFalse(int players)
        {
            //arrange
            var service = new GameService();

            //act
            var result = service.StartGame(players);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void StartGame_ValidPlayerCount_SetsGameState()
        {
            //arrange
            var service = new GameService();

            //act
            service.StartGame(2);

            //assert
            Assert.NotNull(service.GameState);
            Assert.Equal(0, service.GameState.Turn);
            Assert.Equal(0, service.GameState.Winner);
            Assert.Equal(1, service.GameState.CurrentPlayer);
        }
    }
}
