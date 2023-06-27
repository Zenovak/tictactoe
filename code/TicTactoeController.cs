using Microsoft.VisualBasic;
using System.Windows;
using System.Windows.Controls;

namespace Tiktattoe
{
    public class TicTactoeController
    {
        private Tictactoe _model;
        private MainWindow _view;

        public TicTactoeController(Tictactoe model, MainWindow view)
        {
            _model = model;
            _view = view;

            // Subscribing the view to Model Events
            _model.Win += _view.OnWin;
            _model.Draw += _view.OnDraw;
        }

        // Pass the information from view to game, then call
        public void Button_Click(int row, int col)
        {
            _model.Update(row, col);
            _view.Update(_model.grid);

            if (!string.IsNullOrEmpty(_model.GetWinner()))
            {
                if (_model.GetWinner() != "Draw")
                    _model.OnWin(_model.GetWinner(), _model.winning_line);
                else
                    _model.OnDraw();
            }
        }
    }
}
