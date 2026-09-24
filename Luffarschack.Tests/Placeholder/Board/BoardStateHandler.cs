using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Luffarschack.Tests.Placeholder.Board
{
    public class BoardStateHandler
    {

        BoardLayer[] _layers = new BoardLayer[4];

        public BoardStateHandler()
        {
            SetUp();
        }


        public void SetUp()
        {
            int[,] _layout =
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            };
            _layers = new BoardLayer[4];
            _layers[0] = new BoardLayer();
            _layers[1] = new BoardLayer();
            _layers[2] = new BoardLayer();
            _layers[3] = new BoardLayer();

            _layers[0].SetBoard(_layout);
            _layout[1, 1] = 1;
            _layout[1, 2] = 1;
            _layout[1, 3] = 1;
            _layout[2, 1] = 2;
            _layout[2, 2] = 2;
            _layout[2, 3] = 2;
            _layout[3, 3] = 2;
            _layers[1].SetBoard(_layout);
            _layers[2].SetBoard(_layout);
            _layers[3].SetBoard(_layout);
        }

        public void SetSpace(int _x, int _y, int _z, int _player)
        {
            _layers[_z].UpdateBoard(_x, _y, _player);
        }

        public void Dispose()
        {
            _layers = new BoardLayer[4];
        }

        public int GetWinner()
        {
            int final = -1;

            foreach (var item in _layers)
            {
                final = item.CheckForLayerWinner();
                if (final != -1)
                {
                    return final;
                }
            }

            int _play1 = CheckVictoryRelative(1);
            int _play2 = CheckVictoryRelative(2);

            if (CheckVictoryRelative(0) != -1)
            {
                return _play1;
            }
            if (CheckVictoryRelative(0) != -1)
            {
                return _play2;
            }

            // Make that checks top-bottom

            return final;
        }
        private int CheckVictoryRelative(int player)
        {
            int x = 0;
            int y = 0;
            // check down
            for (int i = 0; i < 16; i++)
            {
                if (_layers[0].GetBoard()[x, y] == player && _layers[1].GetBoard()[x, y] == player && _layers[2].GetBoard()[x, y] == player && _layers[3].GetBoard()[x, y] == player)
                {
                    return player;
                }
                if (x >= 3)
                {
                    x = 0;
                    y++;
                }
                else
                {
                    x++;
                }
            }

            // check diags
            if (_layers[0].GetBoard()[0, 0] == player && _layers[1].GetBoard()[1, 1] == player && _layers[2].GetBoard()[2, 2] == player && _layers[3].GetBoard()[3, 3] == player) { return player; }
            if (_layers[0].GetBoard()[3, 0] == player && _layers[1].GetBoard()[2, 1] == player && _layers[2].GetBoard()[1, 2] == player && _layers[3].GetBoard()[0, 3] == player) { return player; }
            if (_layers[0].GetBoard()[0, 3] == player && _layers[1].GetBoard()[1, 2] == player && _layers[2].GetBoard()[2, 1] == player && _layers[3].GetBoard()[3, 0] == player) { return player; }
            if (_layers[0].GetBoard()[3, 3] == player && _layers[1].GetBoard()[2, 2] == player && _layers[2].GetBoard()[1, 1] == player && _layers[3].GetBoard()[0, 0] == player) { return player; }
            // Checks /
            if (_layers[0].GetBoard()[0, 0] == player && _layers[1].GetBoard()[0, 1] == player && _layers[2].GetBoard()[0, 2] == player && _layers[3].GetBoard()[0, 3] == player) { return player; }
            if (_layers[0].GetBoard()[1, 0] == player && _layers[1].GetBoard()[1, 1] == player && _layers[2].GetBoard()[1, 2] == player && _layers[3].GetBoard()[1, 3] == player) { return player; }
            if (_layers[0].GetBoard()[2, 0] == player && _layers[1].GetBoard()[2, 1] == player && _layers[2].GetBoard()[2, 2] == player && _layers[3].GetBoard()[2, 3] == player) { return player; }
            if (_layers[0].GetBoard()[3, 0] == player && _layers[1].GetBoard()[3, 1] == player && _layers[2].GetBoard()[3, 2] == player && _layers[3].GetBoard()[3, 3] == player) { return player; }
            // Checks / - reverse
            if (_layers[3].GetBoard()[0, 0] == player && _layers[2].GetBoard()[0, 1] == player && _layers[1].GetBoard()[0, 2] == player && _layers[0].GetBoard()[0, 3] == player) { return player; }
            if (_layers[3].GetBoard()[1, 0] == player && _layers[2].GetBoard()[1, 1] == player && _layers[1].GetBoard()[1, 2] == player && _layers[0].GetBoard()[1, 3] == player) { return player; }
            if (_layers[3].GetBoard()[2, 0] == player && _layers[2].GetBoard()[2, 1] == player && _layers[1].GetBoard()[2, 2] == player && _layers[0].GetBoard()[2, 3] == player) { return player; }
            if (_layers[3].GetBoard()[3, 0] == player && _layers[2].GetBoard()[3, 1] == player && _layers[1].GetBoard()[3, 2] == player && _layers[0].GetBoard()[3, 3] == player) { return player; }
            // Checks \
            if (_layers[0].GetBoard()[0, 0] == player && _layers[1].GetBoard()[1, 0] == player && _layers[2].GetBoard()[2, 0] == player && _layers[3].GetBoard()[3, 0] == player) { return player; }
            if (_layers[0].GetBoard()[0, 1] == player && _layers[1].GetBoard()[1, 1] == player && _layers[2].GetBoard()[2, 1] == player && _layers[3].GetBoard()[3, 1] == player) { return player; }
            if (_layers[0].GetBoard()[0, 2] == player && _layers[1].GetBoard()[1, 2] == player && _layers[2].GetBoard()[2, 2] == player && _layers[3].GetBoard()[3, 2] == player) { return player; }
            if (_layers[0].GetBoard()[0, 3] == player && _layers[1].GetBoard()[1, 3] == player && _layers[2].GetBoard()[2, 3] == player && _layers[3].GetBoard()[3, 3] == player) { return player; }
            // Checks \ - reverse
            if (_layers[3].GetBoard()[0, 0] == player && _layers[2].GetBoard()[1, 0] == player && _layers[1].GetBoard()[2, 0] == player && _layers[0].GetBoard()[3, 0] == player) { return player; }
            if (_layers[3].GetBoard()[0, 1] == player && _layers[2].GetBoard()[1, 1] == player && _layers[1].GetBoard()[2, 1] == player && _layers[0].GetBoard()[3, 1] == player) { return player; }
            if (_layers[3].GetBoard()[0, 2] == player && _layers[2].GetBoard()[1, 2] == player && _layers[1].GetBoard()[2, 2] == player && _layers[0].GetBoard()[3, 2] == player) { return player; }
            if (_layers[3].GetBoard()[0, 3] == player && _layers[2].GetBoard()[1, 3] == player && _layers[1].GetBoard()[2, 3] == player && _layers[0].GetBoard()[3, 3] == player) { return player; }


            return -1;
        }
    }
}
