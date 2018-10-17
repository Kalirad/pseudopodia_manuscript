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
using System.Windows.Shapes;

namespace StochasticalChemicalLevel
{
    /// <summary>
    /// Interaction logic for Window3D.xaml
    /// </summary>
    public partial class Window3D : Window
    {
        private DrTirandazCellBody cellBody;
        System.Timers.Timer Timer;
        public Window3D()
        {
            InitializeComponent();
            Timer = new System.Timers.Timer();
            Timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Timer.Stop();
                this.NextStep();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { Timer.Start(); }
        }

        bool firstTime = true;
        int DisplayMolecule = 0;
        private void btnNestStep_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //btnStartTimer.IsEnabled = false;
                this.NextStep();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        int stepCounter = 0;
        public void NextStep()
        {
            stepCounter++;
            if (firstTime)
            {
                firstTime = false;
                this.CreatCellBody();
                btnStartTimer.IsEnabled = true;
            }
            else
            {
                int time = 100;//ms badan bayad az tozi biad
                this.cellBody.UpdateVoxels();
                ucMoleculesHeatMap.RefereshGUI(this.cellBody, DisplayMolecule);
            }
            Action m = () => { this.Title = stepCounter.ToString("N0"); };
            this.Dispatcher.BeginInvoke(m);
        }

        private void CreatCellBody()
        {
            //int cellH = int.Parse(txtBoxCellHeight.Text);
            //int cellW = int.Parse(txtBoxCellWidth.Text);
            //Random rnd = new Random(DateTime.Now.Millisecond);
            //int voxelSize = int.Parse(txtBoxVoxelSize.Text);
            //this.cellBody = new CellBody(cellH / voxelSize, cellW / voxelSize, voxelSize);
            //for (int i = 0; i < cellH / voxelSize; i++)
            //    for (int j = 0; j < cellW / voxelSize; j++)
            //    {
            //        int tt = int.Parse(ucMoleculesInitiate.txtBoxM1.Text);
            //        this.cellBody.SubVolumes[i, j].M1_Ras = rnd.Next(1, tt);
            //        this.cellBody.SubVolumes[i, j].M2_PI3K =  int.Parse(ucMoleculesInitiate.txtBoxM2.Text);
            //        this.cellBody.SubVolumes[i, j].M3_PTEN = int.Parse(ucMoleculesInitiate.txtBoxM3.Text);
            //        //this.cellBody.SubVolumes[i, j].M4_SGP = int.Parse(ucMoleculesInitiate.txtBoxM4.Text);
            //        //this.cellBody.SubVolumes[i, j].M5_PIP = int.Parse(ucMoleculesInitiate.txtBoxM5.Text);
            //        this.cellBody.SubVolumes[i, j].M6_P2 = int.Parse(ucMoleculesInitiate.txtBoxM6.Text);
            //        this.cellBody.SubVolumes[i, j].M7_P3 = int.Parse(ucMoleculesInitiate.txtBoxM7.Text);
            //        this.cellBody.SubVolumes[i, j].M8_Xactin = int.Parse(ucMoleculesInitiate.txtBoxM8.Text);
            //        this.cellBody.SubVolumes[i, j].M9_Xmyosin = int.Parse(ucMoleculesInitiate.txtBoxM9.Text);
            //    }
            //ucMoleculesHeatMap.Initiate(cellH, cellW, voxelSize);

            //DisplayMolecule = ucMoleculesInitiate.GetCheckedMolecule();
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnNestStep.IsEnabled = false;
                btnStartTimer.IsEnabled = false;
                int timerInterval = int.Parse(txtBoxTimerInterval.Text);
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
            DisplayMolecule = ucMoleculesInitiate.GetCheckedMolecule();
        }
    }
}
