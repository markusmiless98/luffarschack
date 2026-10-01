using Luffarschack.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Luffarshack.UnitTests
{
    public class FakeFixture
    {
        public GameState _state = new GameState();

        public FakeFixture()
        {
            SetUp();
        }
        public void SetUp()
        {
            _state.BoardState = new int[4, 4, 4];
            int i = 0;
            int x = 0;
            int y = 0;
            int z = 0;

            while (i < _state.BoardState.Length)
            {
                if (x == 3 && y >= 2 && z != 3)
                {
                    _state.BoardState[x, y, z] = 1;
                }
                else if (x == 2 && y <= 1 && z != 2)
                {
                    _state.BoardState[x, y, z] = 2;
                }
                else
                {
                    _state.BoardState[x, y, z] = 0;
                }
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
        public int PerformMove(Move _move)
        {
            if (_move == null) return -1;


            if (_state.BoardState[_move.x, _move.y, _move.z] == 0)
            {
                int i = 0;
                if (Int32.TryParse(_move.Player, out i) != null)
                {
                    _state.BoardState[_move.x, _move.y, _move.z] = i;
                    return i;
                }
            }

            return -1;
        }
        public int CheckBoard(Move _move)
        {
            if (_move == null) return -1;

            int i = _state.BoardState[_move.x, _move.y, _move.z];

            return i;
        }
    }
}
