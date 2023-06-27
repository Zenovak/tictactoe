using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Media;

namespace Tiktattoe
{
    public class Tictactoe
    {
        public string[,] grid = new string[3,3];

        public delegate void WinEventHandler(string win, string winline);
        public event WinEventHandler Win;

        public delegate void DrawEventHandler();
        public event DrawEventHandler Draw;

        private string _winning_line;

        
        // ctor
        public Tictactoe()
        {
            //init a random player at the turn.
            var random = new Random();
            _turn = random.Next(0, 2) == 1;
        }

        public string winning_line
        {
            ///Returns the wining line data if any: row0 - row2, col0 - col 2, diagonal1 & diagonal2
            get
            {
                return _winning_line;
            }
        }



        public void OnWin(string e, string winline)
        {
            if (Win != null)
            {
                Win(e, winline);
            }
        }

        public void OnDraw()
        {
            if (Draw != null)
            {
                Draw();
            }
        }


        // Subsribes to the MouseClick event and updates its own gird. 
        public void Update(int row, int col)
        {
            Move(row, col);
        }


        // Wining and Losing Checkings
        private string _Winner(out string _winning_line)
        {
            for (int i = 0; i < 3; i++)
            {
                if (grid[i, 0] == grid[i, 1] && grid[i, 0] == grid[i, 2])
                {
                    _winning_line = "row" + i.ToString();
                    return grid[i, 0];
                }
                if (grid[0, i] == grid[1, i] && grid[0, i] == grid[2, i])
                {   
                    _winning_line = "col" + i.ToString();
                    return grid[0, i];
                }
            }
            if (grid[0, 0] == grid[1, 1] && grid[0,0] == grid[2, 2])
            {
                _winning_line = "diagonal1";
                return grid[0, 0];
            }
            else if (grid[0, 2] == grid[1, 1] && grid[0, 2] == grid[2, 0])
            {
                _winning_line = "diagonal2";
                return grid[0, 2];
            }
            else
            {
                _winning_line = "";
                return "";
            }
        }

        public string GetWinner()
        {
            if (CheckWinner())
            {
                return _Winner(out _winning_line);
            }
            else if (!grid.Cast<string>().Any(s => s == null))
            {
                return "Draw";
            }
            else
            {
                return "";
            }
        }

        private bool CheckWinner()
        {
            return !string.IsNullOrEmpty(_Winner(out _winning_line));
        }

        // Defines whose turn
        private bool _turn;
        
        public void Move(int row, int col)
        {

            if (_turn)
            {
                grid[row, col] = "X";
                _turn = false;
            }
            else
            {
                grid[row, col] = "O";
                _turn = true;
            }
        }
    }
}
