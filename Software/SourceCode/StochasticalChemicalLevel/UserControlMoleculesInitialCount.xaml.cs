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
    /// Interaction logic for UserControlMoleculesInitialCount.xaml
    /// </summary>
    public partial class UserControlMoleculesInitialCount : UserControl
    {
        public UserControlMoleculesInitialCount()
        {
            InitializeComponent();
        }

        internal int GetCheckedMolecule()
        {
            if (rd1.IsChecked.Value) return 1;
            if (rd2.IsChecked.Value) return 2;
            if (rd3.IsChecked.Value) return 3;
            if (rd4.IsChecked.Value) return 4;
            if (rd5.IsChecked.Value) return 5;
            if (rd6.IsChecked.Value) return 6;
            if (rd7.IsChecked.Value) return 7;
            if (rd8.IsChecked.Value) return 8;
            if (rd9.IsChecked.Value) return 9;

            return -1;
        }
    }
}
