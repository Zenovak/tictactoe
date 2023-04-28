using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tiktattoe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TextBlock[,] _textBlocks;
        private TicTactoeController tictactoeController;

        public MainWindow()
        {
            InitializeComponent();
            Start();
        }


        // This section is solely responsible for initializing the grid and starting the game fresh.
        public void Start()
        {
            var myBoard = (Grid)FindName("Board");
            this.Content = myBoard;


            var tictactoe = new Tictactoe();
            tictactoeController = new TicTactoeController(tictactoe, this);

            // for reseting the game:
            if (_textBlocks != null)
            {
                // Resseting the board
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        myBoard.Children.Remove(_textBlocks[i, j]);
                    }
                }

                // Resetting the lines are placed on the OnWin();
                

            }

            // for starting a new game

            // Adding all the textboxes to the grid, and adding them to event handler.
            //row = i, col = j
            _textBlocks = new TextBlock[3, 3];
            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _textBlocks[i, j] = new TextBlock();
                    Grid.SetColumn(_textBlocks[i, j], j);
                    Grid.SetRow(_textBlocks[i, j], i);
                    myBoard.Children.Add(_textBlocks[i, j]);
                    _textBlocks[i, j].PreviewMouseLeftButtonDown += Button_Click;
                    _textBlocks[i, j].FontSize = 50;
                }
            }
            
        }            


        // Sends information to the controller when button is clicked
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is TextBlock textBlock)
            {
                var col = Grid.GetColumn(textBlock);
                var rol = Grid.GetRow(textBlock);
                tictactoeController.Button_Click(rol, col);

                textBlock.PreviewMouseLeftButtonDown -= Button_Click;
            }
        }


        // When the controller raised the diff events,
        public void OnWin(string win, string winingline)
        {
            var line = (Line)FindName(winingline);
            line.Visibility = Visibility.Visible;
            MessageBox.Show(win + " has won!");

            line.Visibility = Visibility.Hidden;
            Start();
        }
        public void OnDraw()
        {
            MessageBox.Show("Its a draw!");
            Start();
        }


        // Updates the display. Displays are updated based on information from the controller. 
        public void Update(string[,] grid)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!string.IsNullOrEmpty(grid[i, j]))
                    {
                        _textBlocks[i, j].Text = grid[i, j];
                        _textBlocks[i, j].HorizontalAlignment = HorizontalAlignment.Center;
                        _textBlocks[i, j].VerticalAlignment = VerticalAlignment.Center;
                    }
                }
            }
        }
    }
}
