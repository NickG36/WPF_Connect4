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

namespace Conn4_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button[,] grid = new Button[C4Board.NUM_ROWS, C4Board.NUM_COLS];

        Button[] colBtns = new Button[C4Board.NUM_COLS];

        C4Board board = new C4Board();

        public MainWindow()
        {
            InitializeComponent();

            setupGrid();

            setupColBtns();

            board.setTestPosn();

            updateBoard();
        }

        private void setupGrid()
        {
            grid[0, 0] = btn00;
            grid[0, 1] = btn10;
            grid[0, 2] = btn20;
            grid[0, 3] = btn30;
            grid[0, 4] = btn40;
            grid[0, 5] = btn50;
            grid[0, 6] = btn60;

            grid[1, 0] = btn01;
            grid[1, 1] = btn11;
            grid[1, 2] = btn21;
            grid[1, 3] = btn31;
            grid[1, 4] = btn41;
            grid[1, 5] = btn51;
            grid[1, 6] = btn61;

            grid[2, 0] = btn02;
            grid[2, 1] = btn12;
            grid[2, 2] = btn22;
            grid[2, 3] = btn32;
            grid[2, 4] = btn42;
            grid[2, 5] = btn52;
            grid[2, 6] = btn62;

            grid[3, 0] = btn03;
            grid[3, 1] = btn13;
            grid[3, 2] = btn23;
            grid[3, 3] = btn33;
            grid[3, 4] = btn43;
            grid[3, 5] = btn53;
            grid[3, 6] = btn63;

            grid[4, 0] = btn04;
            grid[4, 1] = btn14;
            grid[4, 2] = btn24;
            grid[4, 3] = btn34;
            grid[4, 4] = btn44;
            grid[4, 5] = btn54;
            grid[4, 6] = btn64;

            grid[5, 0] = btn05;
            grid[5, 1] = btn15;
            grid[5, 2] = btn25;
            grid[5, 3] = btn35;
            grid[5, 4] = btn45;
            grid[5, 5] = btn55;
            grid[5, 6] = btn65;
        }

        private void updateBoard()
        {
            for (int row_idx = 0; row_idx < C4Board.NUM_ROWS; ++row_idx)
            {
                for (int col_idx = 0; col_idx < C4Board.NUM_COLS; ++col_idx)
                {
                    Button curr_btn = grid[row_idx, col_idx];
                    if (board.squares[row_idx, col_idx] == C4Board.PieceType.BLACK)
                    {
                        //curr_btn.Content = "B";
                        curr_btn.Background = new SolidColorBrush(Colors.Red);
                        curr_btn.IsEnabled = true;
                    }
                    else if (board.squares[row_idx, col_idx] == C4Board.PieceType.WHITE)
                    {
                        //curr_btn.Content = "W";
                        curr_btn.Background = new SolidColorBrush(Colors.Blue);
                        curr_btn.IsEnabled = true;
                    }
                    else
                    {
                        curr_btn.Content = " ";
                       // grid[row_idx, col_idx].Background = "#FF040FF7";
                    }

                    //grid[row_idx, col_idx].back
                }
            }

            // TO DO: Enable just the col btns which allow a valid move
            for (int col_idx = 0; col_idx < C4Board.NUM_COLS; ++col_idx)
            {
                colBtns[col_idx].IsEnabled = true;
            }

        }

        private void setupColBtns()
        {
            colBtns[0] = btnCol1;
            colBtns[1] = btnCol2;
            colBtns[2] = btnCol3;
            colBtns[3] = btnCol4;
            colBtns[4] = btnCol5;
            colBtns[5] = btnCol6;
            colBtns[6] = btnCol7;
        }

        private void colBtnClicked(int col_idx)
        {
            System.Console.WriteLine(col_idx + " pressed ");

            disableColBtns();

            // Make move on board
            var curr_move = new AbstractBoard.CommonMove();
            curr_move.move_idx = col_idx - 1;
            board.makeMove(curr_move);

            // Update board (including col btns)
            updateBoard();

            disableColBtns();

            // Use an async move to calculate computer's move. If it takes a long time then
            // GUI has a chance to update first due to player's move

            makeBestMoveAsync();

            // Find best move
            //board.makeBestMove();
        }

        private void makeBestMoveAsync()
        {
            var move_finder = new BestMoveFinder(board);

            var best_move = new AbstractBoard.CommonMove();
            move_finder.findBestMove(best_move);

            board.makeMove(best_move);
            updateBoard();
        }

        private void disableColBtns()
        {
            for(int col_idx = 0; col_idx < C4Board.NUM_COLS; ++col_idx)
            {
                colBtns[col_idx].IsEnabled = false;
            }
        }

        private void btnCol1_Click(object sender, RoutedEventArgs e)
        {
            colBtnClicked(1);
        }

        private void btnCol2_Click(object sender, RoutedEventArgs e)
        {
            colBtnClicked(2);
        }

        private void btnCol3_Click(object sender, RoutedEventArgs e)
        {
            colBtnClicked(3);
        }

        private void btnCol4_Click(object sender, RoutedEventArgs e)
        {
            colBtnClicked(4);
        }

        private void btnCol5_Click(object sender, RoutedEventArgs e)
        {
            colBtnClicked(5);
        }

        private void btnCol6_Click(object sender, RoutedEventArgs e)
        {
            colBtnClicked(6);
        }

        private void btnCol7_Click(object sender, RoutedEventArgs e)
        {
            colBtnClicked(7);
        }
    }
}
