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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICellBody cellBody;
        System.Timers.Timer Timer;
        public MainWindow()
        {
            InitializeComponent();

            this.ucMotilityPath.txtBoxCellHeight = this.txtBoxCellHeight;
            this.ucMotilityPath.txtBoxCellWidth = this.txtBoxCellWidth;
            this.ucMotilityPath.txtBoxVoxelSize = this.txtBoxVoxelSize;
            this.ucMotilityPath.rdBtnTirAndaz = this.rdBtnTirAndaz;
        double N_Avogadro = 6.0221415 * Math.Pow(10, 23);//1/mol

          //  MessageBox.Show(double.MinValue.ToString() + "\n "+ double.MaxValue.ToString());
            //double voxSize = 0.0000001;
            //double Volume = voxSize * voxSize * voxSize;
            //string s = "\n N_Avogadro=" + N_Avogadro.ToString();
            //s += "\n voxSize=" + voxSize.ToString();
            //s += "\n Volume=" + Volume.ToString();
            //s += "\n N_Avogadro * Volume=" + (N_Avogadro * Volume).ToString();
            //double k1 =0.5 * Math.Pow(10, -6) / (N_Avogadro * Volume);
            //s += "\n k1=" + k1.ToString();

            //MessageBox.Show(s);
            Timer = new System.Timers.Timer();
            Timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Timer.Stop();
                ucMoleculesHeatMap.RefereshGUI(this.cellBody, DisplayMolecule);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { Timer.Start(); }
        }

        int DisplayMolecule = 1;
        private void btnNestStep_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //btnStartTimer.IsEnabled = false;
                btnNestStep.IsEnabled = false;
                this.CreatCellBody();
                btnStartTimer.IsEnabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

     

        private void CreatCellBody()
        {
            double cellH = double.Parse(txtBoxCellHeight.Text);
            double cellW = double.Parse(txtBoxCellWidth.Text);

            double voxelSize = double.Parse(txtBoxVoxelSize.Text);
            int numberOfRowVoxels = (int)(cellH / voxelSize);
            int numberOfColVoxels = (int)(cellW / voxelSize);
           
            if(this.rdBtnTirAndaz.IsChecked.Value)
                this.cellBody = new DrTirandazCellBody(numberOfRowVoxels, numberOfColVoxels, voxelSize);
            else
                this.cellBody = new DrKaliradCellBody(numberOfRowVoxels, numberOfColVoxels, voxelSize);

            this.cellBody.InitiateMolecularNumbers();
            ucMoleculesHeatMap.Initiate(cellH, cellW, voxelSize);
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnNestStep.IsEnabled = false;
                btnStartTimer.IsEnabled = false;
                int timerInterval = int.Parse(txtBoxTimerInterval.Text);
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    this.cellBody.UpdateVoxels();
                }).Start();
               
                Timer.Interval = timerInterval;
                Timer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtBoxTimerInterval_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Timer != null)
                {
                    int timerInterval = int.Parse(txtBoxTimerInterval.Text);
                    Timer.Interval = timerInterval;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnUpdateDisplayData_Click(object sender, RoutedEventArgs e)
        {
           // DisplayMolecule = ucMoleculesInitiate.GetCheckedMolecule();
        }

        private void btn3DWin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window3D win = new Window3D();
                win.Show();
            }
            finally
            {

            }
        }
    }
}
