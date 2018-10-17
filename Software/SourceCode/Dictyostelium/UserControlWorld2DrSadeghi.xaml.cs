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
    public partial class UserControlWorld2DrSadeghi :  UserControl, IWorld
    {
        int cols, rows, cellRows, cellCols;
        Random rnd = new Random(DateTime.Now.Millisecond);
        double[,] ProbabilityTable;
        Matter[,] MatterTable;
        public UserControlWorld2DrSadeghi()
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
            try
            {
                this.RefereshProbabiltyTable();
                int tailRow=-1, tailCol=-1;
                int podHeadRow=-1, podHeadCol=-1;
                char Dir='*';
                bool notValidTogrowth = true;
                while (notValidTogrowth)
                {
                    GetRandomPodHeadToGrowth(out podHeadRow, out podHeadCol);
                    List<char> directions = GetDirectionCanGrowth(podHeadRow, podHeadCol);
                    
                    if (directions.Count < 1)
                    {
                        // throw new Exception("197 Cant Find Correct Direction!");
                    }
                    int rD = rnd.Next(0, directions.Count - 1);
                    Dir = directions[rD];
                    GetTailByDrSadeghiMethod(podHeadRow, podHeadCol, Dir, out tailRow, out tailCol);

                    if (IsValidCellToGrowth(podHeadRow, podHeadCol, Dir, tailRow, tailCol)
                        &&
                        IsValidValidLengthToGrowth(podHeadRow, podHeadCol, Dir, tailRow, tailCol))
                        notValidTogrowth = false;
                }
                
               
                int newR = podHeadRow;
                int newC = podHeadCol;
                switch (Dir)
                {
                    case 'U': newR = podHeadRow - 1; break;
                    case 'R': newC = podHeadCol + 1; break;
                    case 'D': newR = podHeadRow + 1; break;
                    case 'L': newC = podHeadCol - 1; break;
                }
                #region ValidCell
                //****************************************************************************
                if (tailCol < 0 || tailCol >= cols) { MessageBox.Show("tailCol=" + tailCol); return; }
                if (tailRow < 0 || tailRow >= rows) { MessageBox.Show("tailRow=" + tailRow); return; }

                if (newC < 1 || newC >= cols - 1) { MessageBox.Show("newC=" + newC); return; }
                if (newR < 1 || newR >= rows - 1) { MessageBox.Show("newR=" + newR); return; }
                #endregion
                //****************************************************************************
                var part = MatterTable[tailRow, tailCol];

                Action m = () =>
                {
                    try
                    {
                        part.GoTo(newR, newC);

                        ProbabilityTable[tailRow, tailCol] = 0;
                        ProbabilityTable[newR, newC] = 1024;
                        MatterTable[tailRow, tailCol] = null;
                        MatterTable[newR, newC] = part;

                        Absorb(newR, newC, Dir);
                        
                    }
                    catch (Exception ex1)
                    {  }
                };

                this.MainCanvas.Dispatcher.BeginInvoke(m);
            }
            catch(Exception ex)
            { throw ex; }
        }

        private void Absorb(int newR, int newC, char dir)
        {
            switch (dir)
            {
                case 'U':
                    for (int gap = 1; gap < 5; gap++)
                        for (int r = newR + 1; r < rows - gap; r++)
                        {
                            if (MatterTable[r, newC] == null && MatterTable[r + gap, newC] != null)
                            {
                                var part = MatterTable[r + gap, newC];
                                part.GoTo(r, newC);

                                ProbabilityTable[r, newC] = ProbabilityTable[r + gap, newC];
                                ProbabilityTable[r + gap, newC] = 0;
                                MatterTable[r + gap, newC] = null;
                                MatterTable[r, newC] = part;
                            }
                        }
                    break;
                case 'R':
                    for (int gap = 1; gap < 5; gap++)
                        for (int c = newC - 1; c > gap; c--)
                        {
                            if (MatterTable[newR, c] == null && MatterTable[newR, c - gap] != null)
                            {
                                var part = MatterTable[newR, c - gap];
                                part.GoTo(newR, c);

                                ProbabilityTable[newR, c] = ProbabilityTable[newR, c - gap];
                                ProbabilityTable[newR, c - gap] = 0;
                                MatterTable[newR, c - gap] = null;
                                MatterTable[newR, c] = part;
                            }
                        }
                    break;
                case 'D':
                    for (int gap = 1; gap < 5; gap++)
                        for (int r = newR - 1; r > gap; r--)
                        {
                            if (MatterTable[r, newC] == null && MatterTable[r - gap, newC] != null)
                            {

                                var part = MatterTable[r - gap, newC];
                                part.GoTo(r, newC);

                                ProbabilityTable[r, newC] = ProbabilityTable[r -+ gap, newC];
                                ProbabilityTable[r - gap, newC] = 0;
                                MatterTable[r - gap, newC] = null;
                                MatterTable[r, newC] = part;
                            }
                        }
                    break;
                case 'L':
                    for (int gap = 1; gap < 5; gap++)
                        for (int c = newC + 1; c <cols-gap; c++)
                        {
                            if (MatterTable[newR, c] == null && MatterTable[newR, c + gap] != null)
                            {
                                var part = MatterTable[newR, c + gap];
                                part.GoTo(newR, c);

                                ProbabilityTable[newR, c] = ProbabilityTable[newR, c + gap];
                                ProbabilityTable[newR, c +gap] = 0;
                                MatterTable[newR, c + gap] = null;
                                MatterTable[newR, c] = part;
                            }
                        }
                    break;
            }


        }

        private bool IsValidValidLengthToGrowth(int r, int c, char Dir, int tailRow, int tailCol)
        {
            try
            {
                //بر اساس r c طول پاها رو میشه محدود کرد
                int maxSudoPodLength = 4;
                switch (Dir)
                {
                    case 'U':
                        for (int x = 0; x < maxSudoPodLength; x++)
                            if (MatterTable[r + x, c + 1] != null || MatterTable[r + x, c - 1] != null)
                                return true;
                        return false;
                    case 'R':
                        for (int x = 0; x < maxSudoPodLength; x++)
                            if (MatterTable[r + 1, c - x] != null || MatterTable[r - 1, c - x] != null)
                                return true;
                        return false;
                    case 'D':
                        for (int x = 0; x < maxSudoPodLength; x++)
                            if (MatterTable[r - x, c + 1] != null || MatterTable[r - x, c - 1] != null)
                                return true;
                        return false;
                    case 'L':
                        for (int x = 0; x < maxSudoPodLength; x++)
                            if (MatterTable[r + 1, c + x] != null || MatterTable[r - 1, c + x] != null)
                                return true;
                        return false;
                }
            }
            catch(Exception ex)
            {

            }
            return false;
        }

        private bool IsValidCellToGrowth(int r, int c, char Dir, int tailRow, int tailCol)
        {
            try
            {
                //شرط پیوستگی
                switch (Dir)
                {
                    case 'U':
                        if (MatterTable[tailRow - 1, tailCol - 1] != null && MatterTable[tailRow - 1, tailCol + 1] != null) return true;
                        return false;
                    case 'R':
                        if (MatterTable[tailRow - 1, tailCol + 1] != null && MatterTable[tailRow + 1, tailCol + 1] != null) return true;
                        return false;
                    case 'D':
                        if (MatterTable[tailRow + 1, tailCol - 1] != null && MatterTable[tailRow + 1, tailCol + 1] != null) return true;
                        return false;
                    case 'L':
                        if (MatterTable[tailRow - 1, tailCol - 1] != null && MatterTable[tailRow + 1, tailCol - 1] != null) return true;
                        return false;
                }

            }
            catch (Exception ex)
            {

            }
            return false;
        }

        private List<char> GetDirectionCanGrowth(int r, int c)
        {
            List<char> dir = new List<char>();
            if (MatterTable[r, c] == null)
                return dir;

            if (r > 0 && MatterTable[r - 1, c] == null && MatterTable[r + 1, c] != null) dir.Add('U');
            if (c < cols - 1 && MatterTable[r, c + 1] == null && MatterTable[r, c - 1] != null) dir.Add('R');
            if (r < rows - 1 && MatterTable[r + 1, c] == null && MatterTable[r - 1, c] != null) dir.Add('D');
            if (c > 0 && MatterTable[r , c - 1] == null && MatterTable[r, c + 1] != null) dir.Add('L');


            //var oU = MatterTable[r - 1, c];
            //var oR = MatterTable[r, c+1];
            //var oD = MatterTable[r + 1, c];
            //var oL = MatterTable[r , c-1];
            return dir;
        }

        private void GetTailByDrSadeghiMethod(int podHeadRow, int podHeadCol, char Direct, out int tailRow, out int tailCol)
        {
            tailRow = podHeadRow;
            tailCol = podHeadCol;
            //وقتی جهت حرکت رو به بالاست، باید انقدر پایین بره تا انتها و پیدا کنه
            switch (Direct)
            {
                case 'U': while (MatterTable[tailRow + 1, tailCol] != null) tailRow++; break;
                case 'R': while (MatterTable[tailRow , tailCol-1] != null) tailCol--; break;
                case 'D': while (MatterTable[tailRow - 1, tailCol] != null) tailRow--; break;
                case 'L': while (MatterTable[tailRow , tailCol+1] != null) tailCol++; break;
            }

            //double rouletteSum = 0;
            //List<Box> BoundarySpace = GetBoundarySpace(out rouletteSum);
            //List<Box> BoundarySpaceSameRowOrColumn = new List<Box>();
            //foreach(Box b in BoundarySpace)
            //{
            //    if ((b.Col == oldCol && Math.Abs(b.Row - oldRow)>1 )
            //      || b.Row == oldRow && Math.Abs(b.Col - oldCol) > 1)
            //        BoundarySpaceSameRowOrColumn.Add(b);
            //}
            //Box slcRandombox=null;
            //if (BoundarySpaceSameRowOrColumn.Count == 1)
            //    slcRandombox = BoundarySpaceSameRowOrColumn[0];
            //else
            //{
            //    int r = rnd.Next(0, BoundarySpaceSameRowOrColumn.Count - 1);
            //    slcRandombox = BoundarySpaceSameRowOrColumn[r];
            //}

            //newRow = slcRandombox.Row;
            //newCol = slcRandombox.Col;
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

        // private void GetRandomOldBoxToEmpty(out int oldRow, out int oldCol)
        private void GetRandomPodHeadToGrowth(out int oldRow, out int oldCol)
        {
            try
            {
                double rouletteSum = 0;
                List<Box> BoundaryMatters = GetBoundaryMatter(out rouletteSum);
                double s = 0;
                double r = rnd.NextDouble();
                r = r * rouletteSum;
                int i = 0;
                while (s < r && i < BoundaryMatters.Count-1)
                {
                    s += BoundaryMatters[i].Probability;
                    i++;
                }
                if (BoundaryMatters.Count < 2)
                {
                    throw new Exception("Can not find Boundary Matter");
                }
                //int i = rnd.Next(0, BoundaryMatters.Count - 1);
                i = Math.Min(i, BoundaryMatters.Count-1);
                oldRow = BoundaryMatters[i].Row;
                oldCol = BoundaryMatters[i].Col;
            }
            catch (Exception ex)
            {
                //throw ex;
                oldRow = 0;
                oldCol = 0;
            }
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
            for (int i = 2; i < rows-2; i++)
            {
                for (int j = 2; j < cols-2; j++)
                {
                    if (MatterTable[i, j] != null)
                    {
                        /*
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
                        */
                        if(GetDirectionCanGrowth(i, j).Count>0)
                        {
                             rouletteSum += ProbabilityTable[i, j];
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

           // var newProb = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (ProbabilityTable[i, j] > 2)
                    {
                        double bef = ProbabilityTable[i, j];
                        ProbabilityTable[i, j] =(ProbabilityTable[i, j] + 0.3* MatterCalculator.GetAverageProbability(i, j, ProbabilityTable, rows, cols)) /2;
                        double aft = ProbabilityTable[i, j];
                    }
                    else
                        ProbabilityTable[i, j] = 1;
                    //if (ProbabilityTable[i, j] == 1)
                    //    newProb[i, j] = 1;
                    //else
                    //    newProb[i, j] = MatterCalculator.GetAverageProbability(i, j, ProbabilityTable, rows, cols);
                }
            }

            //ProbabilityTable = newProb;
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
