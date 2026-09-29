using Luffarschack.Orchestration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Xunit;

namespace Luffarshack.UnitTests
{
    public class TempTest
    {
        GameService _controller = new GameService();

        public TempTest()
        {
            SetUp();
        }
        public void SetUp()
        {
            _controller.GameState.BoardState = new int[4, 4, 4];
            int i = 0;
            int x = 0;
            int y = 0;
            int z = 0;

            while (i < _controller.GameState.BoardState.Length)
            {
                _controller.GameState.BoardState[x, y, z] = 0;
                if (y >= 3 && x == 3)
                {
                    x = 0;
                    y = 0;
                    z++;
                }
                else if (x == 3)
                {
                    x = 0;
                    y++;
                }
                else
                {
                    x++;
                }

                i++;
            }
        }

        [Fact]
        public void AttemptToWrite()
        {
            //string l = _controller.GetGameState();
            //Console.WriteLine(l);

            //Assert.True(l != null);
        }
    }
}
