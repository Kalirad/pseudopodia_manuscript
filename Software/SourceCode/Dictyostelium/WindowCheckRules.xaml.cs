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

namespace Vafadar_GOL
{
    /// <summary>
    /// Interaction logic for WindowCheckRules.xaml
    /// </summary>
    public partial class WindowCheckRules : Window
    {
        public WindowCheckRules()
        {
            InitializeComponent();
        }

        private void CreateMatrix_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int rows = int.Parse(txtBoxRows.Text);
                int cols = int.Parse(txtBoxCols.Text);
                ucRadioMatrix.InitializeMatrix(rows, cols);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
