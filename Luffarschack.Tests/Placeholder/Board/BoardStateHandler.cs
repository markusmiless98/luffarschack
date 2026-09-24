using System;
using System.Collections.Generic;
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
                    break;
                }
            }

            // Make that checks top-bottom

            return final;
        }
        private bool CheckVictoryRelative()
        {

            return false;
        }
    }
}
