using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaction logic for UserControlWorld1.xaml
    /// </summary>
    public partial class UserControlWorld2 :  UserControl, IWorld
    {
        int cols, rows, cellRows, cellCols;
        Random rnd = new Random(DateTime.Now.Millisecond);
        double[,] ProbabilityTable;
        Matter[,] MatterTable;
        public UserControlWorld2()
        {
            InitializeComponent();
        }

        public void Initiate(int numOfRows, int numOfCols, int dHeight, int dWidth)
        {
            this.MainCanvas.Children.Clear();
            // cellBodyParts.Clear();

            cols = numOfCols;
            rows = numOfRows;
            cellRows = dHeight;
            cellCols = dWidth;
            ProbabilityTable = new double[rows, cols];
            MatterTable = new Matter[rows, cols];

            this.Draw();
            this.CreatCellBody(cellRows, cellCols);
        }

        public void FillOneEmptyOtherOne()
        {
            this.RefereshProbabiltyTable();

            int oldRow, oldCol;
            GetRandomOldBoxToEmpty(out oldRow, out oldCol);
            int newRow, newCol;
            GetRandomNewBoxToFill(out newRow, out newCol);

            #region ValidCell
            //****************************************************************************
            if (newCol < 0 || newCol >= cols) { MessageBox.Show("newCol=" + newCol); return; }
            if (newRow < 0 || newRow >= rows) { MessageBox.Show("newRow=" + newRow); return; }

            if (oldCol < 0 || oldCol >= cols) { MessageBox.Show("oldCol=" + oldCol); return; }
            if (oldRow < 0 || oldRow >= rows) { MessageBox.Show("oldRow=" + oldRow); return; }
            #endregion
            //****************************************************************************
            var part = MatterTable[oldRow, oldCol];

            Action m = () =>
            {
                part.GoTo(newRow, newCol);

                ProbabilityTable[newRow, newCol] = 1;
                ProbabilityTable[oldRow, oldCol] = 0;
                MatterTable[newRow, newCol] = part;
                MatterTable[oldRow, oldCol] = null;
            };

            this.MainCanvas.Dispatcher.BeginInvoke(m);

        }

        private void GetRandomNewBoxToFill(out int newRow, out int newCol)
        {
            double rouletteSum = 0;
            List<Box> BoundarySpace = GetBoundarySpace(out rouletteSum);
            //double s = 0;
            //double r = rnd.NextDouble();
            //r = r * rouletteSum;
            //int i = 0;
            //while (s < r && i < BoundarySpace.Count)
            //{
            //    s += BoundarySpace[i].Probability;
            //    i++;
            //}

            if (BoundarySpace.Count < 2)
            {
                throw new Exception("Can not find Boundary Space");
            }
            int indx = rnd.Next(0, BoundarySpace.Count - 1);// Math.Max(BoundarySpace.Count - 1, i);
            newRow = BoundarySpace[indx].Row;
            newCol = BoundarySpace[indx].Col;
        }

        private void GetRandomOldBoxToEmpty(out int oldRow, out int oldCol)
        {
            double rouletteSum = 0;
            List<Box> BoundaryMatters = GetBoundaryMatter(out rouletteSum);
            //double s = 0;
            //double r = rnd.NextDouble();
            //r = r * rouletteSum;
            //int i = 0;
            //while (s < r && i < BoundaryMatters.Count)
            //{
            //    s += BoundaryMatters[i].Probability;
            //    i++;
            //}
            if (BoundaryMatters.Count < 2)
            {
                throw new Exception("Can not find Boundary Matter");
            }
            int i = rnd.Next(0, BoundaryMatters.Count - 1);
            oldRow = BoundaryMatters[i].Row;
            oldCol = BoundaryMatters[i].Col;
        }

        private List<Box> GetBoundarySpace(out double rouletteSum)
        {
            rouletteSum = 0;
            List<Box> Boundary = new List<Box>();
            for (int i = 1; i < rows - 1; i++)
            {
                for (int j = 1; j < cols - 1; j++)
                {
                    if (MatterTable[i, j] == null && MatterCalculator.HasMatterNeighbor(i,j, MatterTable, rows, cols))
                    {
                        rouletteSum += ProbabilityTable[i, j];
                        Boundary.Add(new Box() { Row = i, Col = j, Probability = ProbabilityTable[i, j] });
                    }
                }
            }
            return Boundary;
        }
        private List<Box> GetBoundaryMatter(out double rouletteSum)
        {
            rouletteSum = 0;
            List<Box> Boundary = new List<Box>();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (MatterTable[i, j] != null)
                    {
                        //int n = MatterCalculator.GetNumberOfNeighbors(i, j, MatterTable, rows, cols);
                        //if (n > 0 && n < 8)
                        //{
                        //    // rouletteSum += ProbabilityTable[i, j];
                        //    Boundary.Add(new Box() { Row = i, Col = j, Probability = ProbabilityTable[i, j] });
                        //}
                        bool b = MatterCalculator.IsBoundary(i, j, MatterTable, rows, cols);
                        if (b)
                        {
                            // rouletteSum += ProbabilityTable[i, j];
                            Boundary.Add(new Box() { Row = i, Col = j, Probability = ProbabilityTable[i, j] });
                        }
                    }
                }
            }
            return Boundary;
        }


        private void RefereshProbabiltyTable()
        {
            //باید یه مقداری بذاریم که عین مونود فانکشن کنترل کنه مقادیر هر خونه رو
            //نذاره کل سلول بیش از حد کشیده بشه

            var newProb = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {

                    if (ProbabilityTable[i, j] == 1)
                        newProb[i, j] = 1;
                    else
                        newProb[i, j] = MatterCalculator.GetAverageProbability(i, j, ProbabilityTable, rows, cols);
                }
            }

            ProbabilityTable = newProb;
        }



        private void CreatCellBody(int cRows, int cColls)
        {
            int baseC = ((cols - cColls) / 2);// -1;
            int baseR = ((rows - cRows) / 2);//-1;

            for (int r = 0; r < cRows; r++)
            {
                for (int c = 0; c < cColls; c++)
                {
                    var part = this.CreatePartOfBody(baseR + r, baseC + c);
                }
            }



        }

        private Matter CreatePartOfBody(int row, int col)
        {
            //double wRowsScale = (MainCanvas.ActualHeight / rows);
            //double wColsScale = (MainCanvas.ActualHeight / cols);
            Matter mtr = new Matter(rows, cols, MainCanvas);
            // Matter mtr = new Matter(wRowsScale, wColsScale);
            mtr.Row = row;
            mtr.Col = col;
            mtr.Rec.Width = MainCanvas.ActualWidth / cols;
            mtr.Rec.Height = MainCanvas.ActualHeight / rows;
            mtr.Rec.SetValue(Canvas.LeftProperty, col * (MainCanvas.ActualWidth / cols));
            mtr.Rec.SetValue(Canvas.TopProperty, row * (MainCanvas.ActualHeight / rows));
            mtr.Rec.Fill = Brushes.Green;
            ProbabilityTable[row, col] = 1;
            MatterTable[row, col] = mtr;
            MainCanvas.Children.Add(mtr.Rec);
            return mtr;
        }

        private void Draw()
        {
            double h = MainCanvas.ActualHeight;
            double w = MainCanvas.ActualWidth;
            for (int i = 0; i < rows; i++)
            {
                this.DrawHorizantalLine(i * (h / rows));
            }
            for (int j = 0; j < cols; j++)
            {
                this.DrawVerticalLine(j * (w / cols));
            }
        }

        Brush lineColor = Brushes.Yellow;
        private void DrawHorizantalLine(double y)
        {
            Line myLine = new Line();
            myLine.Stroke = lineColor;// System.Windows.Media.Brushes.LightSteelBlue;
            myLine.X1 = 1;
            myLine.X2 = MainCanvas.ActualWidth;
            myLine.Y1 = y;
            myLine.Y2 = y;
            myLine.StrokeThickness = 1;
            MainCanvas.Children.Add(myLine);
        }

        private void DrawVerticalLine(double x)
        {
            Line myLine = new Line();
            myLine.Stroke = lineColor;// System.Windows.Media.Brushes.LightSteelBlue;
            myLine.X1 = x;
            myLine.X2 = x;
            myLine.Y1 = 1;
            myLine.Y2 = MainCanvas.ActualHeight;
            myLine.StrokeThickness = 1;
            MainCanvas.Children.Add(myLine);
        }
    }
}
