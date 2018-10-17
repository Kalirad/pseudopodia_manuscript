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

namespace StochasticalChemicalLevel
{
    /// <summary>
    /// Interaction logic for UserControlMoleculesHeatMap.xaml
    /// </summary>
    public partial class UserControlMoleculesHeatMap : UserControl
    {
        public UserControlMoleculesHeatMap()
        {
            InitializeComponent();
        }
        private int rows, cols;
        private VoxelGUI[,] VoxelButtons;



        internal void RefereshGUI(ICellBody cellBody, int displayMolecule)
        {
            //try
            //{
            //    for (int i = 0; i < rows; i++)
            //        for (int j = 0; j < cols; j++)
            //        { 

            //            VoxelButtons[i, j].SetVoxelValue(cellBody.SubVolumes[i, j], displayMolecule);
            //        }

               
            //        Action m = () =>
            //        {
            //            if(cellBody.Cell4Parts!=null && cellBody.Cell4Parts.maxPartActin != null)
            //            txtCenterOfActin.Text = string.Format("X={0} , Y={1}", cellBody.Cell4Parts.maxPartActin.CenterOfActin_x.ToString(".00"), cellBody.Cell4Parts.maxPartActin.CenterOfActin_x.ToString(".00"));
            //            if (cellBody.Cell4Parts.maxPartMyosin != null)
            //                txtCenterOfMyosin.Text = string.Format("X={0} , Y={1}", cellBody.Cell4Parts.maxPartMyosin.CenterOfMyosin_x.ToString(".00"), cellBody.Cell4Parts.maxPartMyosin.CenterOfMyosin_x.ToString(".00"));
            //        };
            //        this.Dispatcher.BeginInvoke(m);
               
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

     
        internal void Initiate(double cellH, double cellW, double voxelSize)
        {
            try
            {
                //double h = MainCanvas.ActualHeight;
                //double w = MainCanvas.ActualWidth;
                rows = (int)(cellH / voxelSize);
                cols = (int)(cellW / voxelSize);

                //for (int i = 0; i < rows; i++)
                //{
                //    this.DrawHorizantalLine(i * (h / rows));
                //}
                //for (int j = 0; j < cols; j++)
                //{
                //    this.DrawVerticalLine(j * (w / cols));
                //}
                uniformGrid.Rows = rows;
                uniformGrid.Columns = cols;
                
                VoxelButtons = new VoxelGUI[rows,cols] ;
                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < cols; j++)
                    {
                        VoxelButtons[i, j] = new VoxelGUI();// { Margin = uniformGrid.Margin } ;
                        uniformGrid.Children.Add(VoxelButtons[i, j]);
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

      
        Brush lineColor = Brushes.Yellow;
        private int centerOfActin_Y;

        private void DrawHorizantalLine(double y)
        {
            //Line myLine = new Line();
            //myLine.Stroke = lineColor;// System.Windows.Media.Brushes.LightSteelBlue;
            //myLine.X1 = 1;
            //myLine.X2 = MainCanvas.ActualWidth;
            //myLine.Y1 = y;
            //myLine.Y2 = y;
            //myLine.StrokeThickness = 1;
            //MainCanvas.Children.Add(myLine);
        }

        private void DrawVerticalLine(double x)
        {
            //Line myLine = new Line();
            //myLine.Stroke = lineColor;// System.Windows.Media.Brushes.LightSteelBlue;
            //myLine.X1 = x;
            //myLine.X2 = x;
            //myLine.Y1 = 1;
            //myLine.Y2 = MainCanvas.ActualHeight;
            //myLine.StrokeThickness = 1;
            //MainCanvas.Children.Add(myLine);
        }
    }
}
