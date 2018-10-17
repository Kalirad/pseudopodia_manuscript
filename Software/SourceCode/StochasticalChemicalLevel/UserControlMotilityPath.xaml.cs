using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    
    public partial class UserControlMotilityPath : UserControl
    {
        System.Timers.Timer mTimet;
        Point ecoliPos;
        public UserControlMotilityPath()
        {
            InitializeComponent();
            mTimet = new System.Timers.Timer(2800);
            mTimet.Elapsed += MTimet_Elapsed;
        }

        Random rnd = new Random(DateTime.Now.Millisecond);
        private void MTimet_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Action act = () =>
                {
                    double stepLength,angle;
                    this.cellBody.GetStepLengthAndDirection(out stepLength, out angle);
                    double len = Math.Min(Math.Max(0.01, stepLength), 2);
                    len *= 50;
                    double stepX = Math.Cos(angle) * len;
                    double stepY = Math.Sin(angle) * len;
                    ecoliPos.X += stepX;
                    ecoliPos.Y += stepY;

                    Draw(ecoliPos, 2, Brushes.Black, System.Windows.Media.Brushes.GreenYellow);
                };

                this.Dispatcher.BeginInvoke(act);
                
            }
            catch (Exception ex)
            {
                mTimet.Stop();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnStart.IsEnabled = false;
                double cnetrX = MainCanvas.ActualWidth / 2 ;
                double cnetrY = MainCanvas.ActualHeight / 2;
                double radius = Math.Min( 300, 0.8* cnetrX);
                double posX, posY;
                GetRandomDistanceFromThisPoint(cnetrX, cnetrY, radius, out posX, out posY);
                ecoliPos = new Point(posX, posY);
                // TestInitialPoint();
                this.CreatCellBody();
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    this.cellBody.UpdateVoxels();
                }).Start();
                mTimet.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void TestInitialPoint()
        {

            Action act = () =>
            {
                Draw(ecoliPos, 4, Brushes.Black, System.Windows.Media.Brushes.GreenYellow);
            };
            this.Dispatcher.BeginInvoke(act);
        }

        private void GetRandomDistanceFromThisPoint(double centerX, double centerY, double distance, out double X, out double Y)
        {
            int maxX = (int)(centerX + distance);
            int minX = (int)(centerX - distance);
            X = rnd.Next(minX, maxX);
            double defX = centerX - X;
            double defY = Math.Sqrt(Math.Pow(distance, 2) - Math.Pow(defX, 2));
            int sign = 1;
            if (rnd.Next() % 2 == 0)
                sign = -1;

            Y = centerY + (sign * defY);

        }
        double oldXN=-1, oldYN = -1;
        Ellipse elLastNormal;
        int historyPointSie = 3;
        private void Draw(Point m, double size, Brush color, Brush lineColor)
        {
            try
            {

                Ellipse el = new Ellipse();
                el.Width = size;
                el.Height = size;
                el.SetValue(Canvas.LeftProperty, m.X);
                el.SetValue(Canvas.TopProperty, m.Y);
                el.Fill = color;
                    //if (oldYN>0)//avvali resource
                    if (elLastNormal != null)
                    {
                        var p = elLastNormal;// (MainCanvas.Children[MainCanvas.Children.Count - 1] as Ellipse);
                        p.Fill = Brushes.Gray;
                        p.Width = p.Height = historyPointSie;
                        this.DrawLine(oldXN, oldYN, m.X, m.Y, lineColor);
                    }
                    oldXN = m.X;
                    oldYN = m.Y;
                    elLastNormal = el;
                MainCanvas.Children.Add(el);

            }
            catch (Exception ex)
            {
            }
        }



        private void DrawLine(double startX, double startY, double endX, double endY, Brush lineColor)
        {
            Line myLine = new Line();
            myLine.Stroke = lineColor;// System.Windows.Media.Brushes.LightSteelBlue;
            myLine.X1 = startX;
            myLine.X2 = endX;
            myLine.Y1 = startY;
            myLine.Y2 = endY;
            //myLine.HorizontalAlignment = HorizontalAlignment.Left;
            //myLine.VerticalAlignment = VerticalAlignment.Center;
            myLine.StrokeThickness = 1;
            MainCanvas.Children.Add(myLine);
        }


        #region  Cell Body
        private ICellBody cellBody;
        public TextBox txtBoxCellHeight { get; set; }
        public TextBox txtBoxCellWidth { get; set; }
        public TextBox txtBoxVoxelSize { get; set; }
        public RadioButton rdBtnTirAndaz { get; set; }
        private void CreatCellBody()
        {
            double cellH = double.Parse(txtBoxCellHeight.Text);
            double cellW = double.Parse(txtBoxCellWidth.Text);

            double voxelSize = double.Parse(txtBoxVoxelSize.Text);
            int numberOfRowVoxels = (int)(cellH / voxelSize);
            int numberOfColVoxels = (int)(cellW / voxelSize);

            if (this.rdBtnTirAndaz.IsChecked.Value)
                this.cellBody = new DrTirandazCellBody(numberOfRowVoxels, numberOfColVoxels, voxelSize);
            else
                this.cellBody = new DrKaliradCellBody(numberOfRowVoxels, numberOfColVoxels, voxelSize);

            this.cellBody.InitiateMolecularNumbers();
        }
        #endregion
    }
}
