using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Luffarschack.Tests.Placeholder.Board
{
    public class BoardLayer
    {
        int[,] board;
        public BoardLayer()
        {
            board = new int[,]
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            };
        }

        public int[,] GetBoard()
        {
            return board;
        }

        public void SetBoard(int[,] _board)
        {
            board = _board;
        }

        public void UpdateBoard(int _x, int _y, int _player)
        {
            if (board.GetLength(0) < _x)
            {
                // Failed
                return;
            }
            if (board.GetLength(1) < _y)
            {
                // Failed
                return;
            }
            board[_x, _y] = _player;
        }

        public int CheckForLayerWinner()
        {
            List<int> _playerNums = new List<int>();

            foreach (var item in board)
            {
                if (!_playerNums.Contains(item) && item != 0)
                {
                    if (CheckWinner(item))
                    {
                        return item;
                    }
                }
            }


            return -1;
        }

        // Source - https://stackoverflow.com/a/21370106
        // Posted by Jay Wick, modified by community. See post 'Timeline' for change history
        // Retrieved 2026-09-23, License - CC BY-SA 3.0

        private bool CheckWinner(int player)
        {
            // check rows
            if (board[0, 0] == player && board[0, 1] == player && board[0, 2] == player && board[0, 3] == player) { return true; }
            if (board[1, 0] == player && board[1, 1] == player && board[1, 2] == player && board[1, 3] == player) { return true; }
            if (board[2, 0] == player && board[2, 1] == player && board[2, 2] == player && board[2, 3] == player) { return true; }
            if (board[3, 0] == player && board[3, 1] == player && board[3, 2] == player && board[3, 3] == player) { return true; }

            // check columns
            if (board[0, 0] == player && board[1, 0] == player && board[2, 0] == player && board[3, 0] == player) { return true; }
            if (board[0, 1] == player && board[1, 1] == player && board[2, 1] == player && board[3, 1] == player) { return true; }
            if (board[0, 2] == player && board[1, 2] == player && board[2, 2] == player && board[3, 2] == player) { return true; }
            if (board[0, 3] == player && board[1, 3] == player && board[2, 3] == player && board[3, 3] == player) { return true; }

            // check diags
            if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player && board[3, 3] == player) { return true; }
            if (board[0, 3] == player && board[1, 2] == player && board[2, 1] == player && board[1, 0] == player) { return true; }

            return false;
        }

    }
}
