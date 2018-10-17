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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Timers.Timer movingTimer;
        private IWorld ucWorld;
        public MainWindow()
        {
            InitializeComponent();
            // ucWorld1.Initiate(50,10,5);
            movingTimer = new System.Timers.Timer(1000);
            movingTimer.Elapsed += MovingTimer_Elapsed;
           
        }

        private void MovingTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                movingTimer.Stop();
                this.NextStep();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Timer Exception!" + ex.Message);
            }
            finally { movingTimer.Start(); }
        }

        int stepCounter = 0;
        private void NextStep()
        {
            stepCounter++;
            ucWorld.FillOneEmptyOtherOne();
            Action m = () => { btnStartTimer.Content = stepCounter.ToString("0,0"); };
            Dispatcher.BeginInvoke(m);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnNestStep.IsEnabled = false;
                btnStartTimer.IsEnabled = false;
                this.CreatWorld();
                int r = int.Parse(txtBoxRows.Text);
                int c = int.Parse(txtBoxCols.Text);

                int dRows = int.Parse(txtBoxDictyDictyosteliumRows.Text);
                int dCols = int.Parse(txtBoxDictyosteliumCols.Text);

                ucWorld.Initiate(r, c, dRows, dCols);
                double timr = double.Parse(txtBoxTimerInterval.Text);
                movingTimer.Interval = 1000 * timr;
                movingTimer.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CreatWorld()
        {
            if (rdBtnRandom.IsChecked.Value)
            {
                ucWorld = ucWorld1;
                ucWorld2.Visibility = Visibility.Collapsed;
                ucWorldDrSadeghi.Visibility = Visibility.Collapsed;
            }
            else if(rdBtnDrSadeghi.IsChecked.Value)
            {
                ucWorld = ucWorldDrSadeghi;
                ucWorld1.Visibility = Visibility.Collapsed;
                ucWorld2.Visibility = Visibility.Collapsed;
            }
            else
            {
                ucWorld = ucWorld2;
                ucWorld1.Visibility = Visibility.Collapsed;
                ucWorldDrSadeghi.Visibility = Visibility.Collapsed;
            }
            ////if (rdBtnRandom.IsChecked.Value)
            ////{
            ////    ucWorld = new UserControlWorld1();
            ////    mainGrid.Children.Add(ucWorld as UserControlWorld1);
            ////    Grid.SetColumnSpan(ucWorld as UserControlWorld1, 20);
            ////    Grid.SetRow(ucWorld as UserControlWorld1, 1);
            ////}
            ////else
            ////{
            ////  //  ucWorld = new UserControlWorld2();
            ////}
            //ucWorld = new UserControlWorld1();
            //mainGrid.Children.Add(ucWorld);
            //Grid.SetColumnSpan(ucWorld, 20);
            //Grid.SetRow(ucWorld, 1);
        }

        bool firstTime = true;
        private void btnNestStep_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (firstTime)
                {
                    firstTime = false;
                    btnStartTimer.IsEnabled = false;
                    this.CreatWorld();
                    int r = int.Parse(txtBoxRows.Text);
                    int c = int.Parse(txtBoxCols.Text);

                    int dR = int.Parse(txtBoxDictyDictyosteliumRows.Text);
                    int dC = int.Parse(txtBoxDictyosteliumCols.Text);

                    ucWorld.Initiate(r, c, dR, dC);
                }
                else
                {
                    ucWorld.FillOneEmptyOtherOne();
                }

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
                if (string.IsNullOrEmpty(txtBoxTimerInterval.Text)) return;
                Action m = () =>
                {
                    double timr = double.Parse(txtBoxTimerInterval.Text);
                    timr = Math.Max(0.01, timr);
                    movingTimer.Interval = 1000 * timr;
                };
                Dispatcher.BeginInvoke(m);
            }
            finally
            {

            }
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                txtBoxRows.Text = "250";
                txtBoxCols.Text = "300";
                txtBoxDictyDictyosteliumRows.Text = "40";
                txtBoxDictyosteliumCols.Text = "20";
                txtBoxTimerInterval.Text = "0.01";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCheckRules_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowCheckRules win = new WindowCheckRules();
                win.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
