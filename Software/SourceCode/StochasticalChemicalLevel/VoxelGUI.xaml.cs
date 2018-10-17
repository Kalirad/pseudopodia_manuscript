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
    /// Interaction logic for VoxelGUI.xaml
    /// </summary>
    public partial class VoxelGUI : UserControl
    {
        public static double MaxValue=1;
        public VoxelGUI()
        {
            InitializeComponent();
        }

        byte Avalue = 1, rValue = 50, bValue = 50;

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
           // this.txtBlock.Background = Brushes.Red;// Color.FromArgb(Avalue, 100, 200, bValue));
        }

        Random rnd = new Random(DateTime.Now.Millisecond);
        internal void SetVoxelValue(DrTirandazVoxel voxel, int displayMolecule)
        {
            Action m = () =>
            {
                int gValue = 0;

                switch (displayMolecule)
                {
                    case 1: gValue = voxel.M1_Ras; break;
                    case 2: gValue = voxel.M2_PI3K; break;
                    case 3: gValue = voxel.M3_PTEN; break;
                    //case 4: gValue = voxel.M4_SGP; break;
                    //case 5: gValue = voxel.M5_PIP; break;
                    case 6: gValue = voxel.M6_P2; break;
                    case 7: gValue = voxel.M7_P3; break;
                    case 8: gValue = voxel.M8_Xactin; break;
                    case 9: gValue = voxel.M9_Xmyosin; break;
                }
                
                txtBlock.Text = gValue.ToString();
                /*
                if (gValue > 190)      this.txtBlock.Background = Brushes.MediumSeaGreen;
                else if (gValue > 180)  this.txtBlock.Background = Brushes.MediumSpringGreen;
                else if (gValue > 150)   this.txtBlock.Background = Brushes.GreenYellow;
                else if (gValue > 120) this.txtBlock.Background = Brushes.Yellow;
                else if (gValue > 90) this.txtBlock.Background = Brushes.Gold;
                else if (gValue > 60) this.txtBlock.Background = Brushes.Orange;
                else if (gValue > 20) this.txtBlock.Background = Brushes.DarkOrange;
                else if (gValue > 0) this.txtBlock.Background = Brushes.Red;
                else  this.txtBlock.Background = Brushes.Brown;
                */

                /*
                byte R = (byte)Math.Min(255, voxel.M8_Xactin);
                byte G = (byte)Math.Min(255, voxel.M9_Xmyosin);
                byte B = 160;// (byte)gValue;
                this.txtBlock.Background =  new SolidColorBrush(Color.FromArgb(255, R, G, B));
                */
                //  gValue = rnd.Next(2, 244);
                txtBlock.Background = HeatMapColor(gValue, 0, 255);
                // txtBlock.Background = new SolidColorBrush(Color.FromRgb((byte)(gValue/2), 50, 50));

                //this.txtBlock.Background =  new SolidColorBrush(Color.FromArgb(Avalue, rValue, gValue, bValue));
                //this.cellBody.SubVolumes[i, j].M2_PI3K
                //this.cellBody.SubVolumes[i, j].M3_PTEN
                //this.cellBody.SubVolumes[i, j].M4_SGP 
                //this.cellBody.SubVolumes[i, j].M5_PIP 
                //this.cellBody.SubVolumes[i, j].M6_P2 
                //this.cellBody.SubVolumes[i, j].M7_P3 
                //this.cellBody.SubVolumes[i, j].M8_Xa 
                //this.cellBody.SubVolumes[i, j].M9_Xm 
            };
            Dispatcher.BeginInvoke(m);
        }

        internal void SetVoxelValue(object p, int displayMolecule)
        {
            throw new NotImplementedException();
        }

        Dictionary<int, SolidColorBrush> heatDic = new Dictionary<int, SolidColorBrush>();
        Color firstColour = Brushes.RoyalBlue.Color;
        Color secondColour = Brushes.LightSkyBlue.Color;
        private SolidColorBrush HeatMapColor(int value, double min, double max)
        {

            if (heatDic.ContainsKey(value)) return heatDic[value];

            // Example: Take the RGB
            //135-206-250 // Light Sky Blue
            // 65-105-225 // Royal Blue
            // 70-101-25 // Delta

            int rOffset = Math.Max(firstColour.R, secondColour.R);
            int gOffset = Math.Max(firstColour.G, secondColour.G);
            int bOffset = Math.Max(firstColour.B, secondColour.B);

            int deltaR = Math.Abs(firstColour.R - secondColour.R);
            int deltaG = Math.Abs(firstColour.G - secondColour.G);
            int deltaB = Math.Abs(firstColour.B - secondColour.B);

            double val = (value - min) / (max - min);
            int r = rOffset - Convert.ToByte(Math.Max(255, deltaR * (1 - val)));
            int g = gOffset - Convert.ToByte(Math.Max(255, deltaG * (1 - val)));
            int b = bOffset - Convert.ToByte(Math.Max(255, deltaB * (1 - val)));
            var clr = new SolidColorBrush(Color.FromArgb(255, (byte)r, (byte)g, (byte)b));
            heatDic[value] = clr;
            return clr;
        }
    }
}
