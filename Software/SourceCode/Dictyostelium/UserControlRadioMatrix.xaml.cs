using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vafadar_GOL
{
    /// <summary>
    /// Interaction logic for UserControlRadioMatrix.xaml
    /// </summary>
    public partial class UserControlRadioMatrix : UserControl
    {
        public UserControlRadioMatrix()
        {
            InitializeComponent();
        }

        CheckBox[,] checkBoxMatrix;
        Label[,] numberOfNeighborsMatrix;
        int[,] numberOfNeighbors;
        Label[,] numberOfNeighborsMatterMatrix;
        int Rows, Cols;
        public void InitializeMatrix(int rows, int cols)
        {
            //mainGrid.Children.Clear();

            Rows = rows;
            Cols = cols;
            checkBoxMatrix = new CheckBox[rows, cols];
            numberOfNeighborsMatrix = new Label[rows, cols];
            numberOfNeighbors = new int[rows, cols];
            numberOfNeighborsMatterMatrix = new Label[rows, cols];

            UniformGrid ug = new UniformGrid() { Rows = rows, Columns = cols };
            mainGrid.Children.Add(ug);
            for(int r=0;r<rows;r++)
                for(int c=0; c<cols;c++)
                {
                    checkBoxMatrix[r, c] = new CheckBox() { IsChecked = false ,HorizontalAlignment=HorizontalAlignment.Center};
                    checkBoxMatrix[r, c].Checked += UserControlRadioMatrix_Checked;
                    checkBoxMatrix[r, c].Unchecked += UserControlRadioMatrix_Checked;
                    ug.Children.Add(checkBoxMatrix[r, c]);
                }

            //*************************************8
            //UniformGrid ugNeigbors = new UniformGrid() { Rows = rows, Columns = cols };
            ugNeigbors.Rows = Rows;
            ugNeigbors.Columns = Cols;

            ugNeigborsMatter.Rows = Rows;
            ugNeigborsMatter.Columns = Cols;
            
           // mainGrid.Children.Add(ugNeigbors);

            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                {
                    numberOfNeighborsMatrix[r, c] = new Label() { Content = "*" , HorizontalContentAlignment=HorizontalAlignment.Center};
                    numberOfNeighborsMatrix[r, c].SizeChanged += UserControlRadioMatrix_SizeChanged;
                    ugNeigbors.Children.Add(numberOfNeighborsMatrix[r, c]);

                    numberOfNeighborsMatterMatrix[r, c] = new Label() { Content = "#", HorizontalContentAlignment = HorizontalAlignment.Center };
                    numberOfNeighborsMatterMatrix[r, c].SizeChanged += UserControlRadioMatrix_SizeChanged;
                    ugNeigborsMatter.Children.Add(numberOfNeighborsMatterMatrix[r, c]);
                }
        }

        private void UserControlRadioMatrix_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var lab = sender as Label;
            lab.FontSize = Math.Max(9, Math.Min(16, e.NewSize.Height/2));
        }

        private void UserControlRadioMatrix_Checked(object sender, RoutedEventArgs e)
        {
            GetStringForNumberOfNeigbors(this.checkBoxMatrix);
            GetStringForNumberOfNeigborsMatter(this.checkBoxMatrix);
        }

        private void GetStringForNumberOfNeigbors(CheckBox[,] checkMtrx)
        {

            for (int r = 1; r < Rows - 1; r++)
            {
                for (int c = 1; c < Cols - 1; c++)
                {
                    int n = 0;
                    if (checkBoxMatrix[r - 1, c - 1].IsChecked.Value) n++;
                    if (checkBoxMatrix[r - 1, c].IsChecked.Value) n++;
                    if (checkBoxMatrix[r - 1, c + 1].IsChecked.Value) n++;

                    if (checkBoxMatrix[r + 1, c - 1].IsChecked.Value) n++;
                    if (checkBoxMatrix[r + 1, c].IsChecked.Value) n++;
                    if (checkBoxMatrix[r + 1, c + 1].IsChecked.Value) n++;

                    if (checkBoxMatrix[r, c - 1].IsChecked.Value) n++;
                    if (checkBoxMatrix[r, c + 1].IsChecked.Value) n++;

                    numberOfNeighborsMatrix[r, c].Content = numberOfNeighbors[r, c] = n;
                    if (checkBoxMatrix[r, c].IsChecked.Value)
                        numberOfNeighborsMatrix[r, c].Background = Brushes.Orange;
                    else
                        numberOfNeighborsMatrix[r, c].Background = Brushes.Transparent;

                }
            }
        }
        private void GetStringForNumberOfNeigborsMatter(CheckBox[,] checkMtrx)
        {

            for (int r = 1; r < Rows - 1; r++)
            {
                for (int c = 1; c < Cols - 1; c++)
                {
                    int n = 0;
                    if (checkBoxMatrix[r - 1, c - 1].IsChecked.Value) n+=numberOfNeighbors[r - 1, c - 1];
                    if (checkBoxMatrix[r - 1, c].IsChecked.Value) n+=numberOfNeighbors[r - 1, c];
                    if (checkBoxMatrix[r - 1, c + 1].IsChecked.Value) n+=numberOfNeighbors[r - 1, c + 1];

                    if (checkBoxMatrix[r + 1, c - 1].IsChecked.Value) n+=numberOfNeighbors[r + 1, c - 1];
                    if (checkBoxMatrix[r + 1, c].IsChecked.Value) n+=numberOfNeighbors[r + 1, c ];
                    if (checkBoxMatrix[r + 1, c + 1].IsChecked.Value) n+=numberOfNeighbors[r + 1, c + 1];

                    if (checkBoxMatrix[r, c + 1].IsChecked.Value) n+=numberOfNeighbors[r, c + 1];
                    if (checkBoxMatrix[r, c - 1].IsChecked.Value) n+=numberOfNeighbors[r, c - 1];

                    numberOfNeighborsMatterMatrix[r, c].Content = n - numberOfNeighbors[r , c];
                    if (checkBoxMatrix[r, c].IsChecked.Value)
                        numberOfNeighborsMatterMatrix[r, c].Background = Brushes.PaleVioletRed;
                    else
                        numberOfNeighborsMatterMatrix[r, c].Background = Brushes.Transparent;

                }
            }
        }
    }
}
