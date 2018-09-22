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
        Button[,] grid = new Button[Conn4Board.NUM_ROWS, Conn4Board.NUM_COLS];

        public MainWindow()
        {
            InitializeComponent();

            // TO DO: Setup grid
        }

        private void colBtnClicked(int col_idx)
        {
            System.Console.WriteLine(col_idx + " pressed ");
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
