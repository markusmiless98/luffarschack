using System;
using System.Collections.Generic;
using System.Text;
using Luffarschack.Core;

namespace Luffarshack.UnitTests
{
    public class FakeFixture
    {
        public GameState _state = new GameState();

        public FakeFixture()
        {
            _state.BoardState = new int[4, 4, 4];
            int i = _state.BoardState.Length;
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
                if (y >= 3 && x >= 3)
                {
                    x = 0;
                    y = 0;
                    z++;
                }
                else if (x >= 3)
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


            if (_state.BoardState[_move._x, _move._y, _move._z] == 0)
            {
                int i = 0;
                i = Int32.Parse(_move.Player);
                _state.BoardState[_move._x, _move._y, _move._z] = i;
                return i;
            }

            return -1;
        }
    }
}
