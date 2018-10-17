using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StochasticalChemicalLevel
{
    public class Cell4Part
    {
        public CenterOfActinMyosin part11, part12, part21, part22, partTotal;
        public CenterOfActinMyosin maxPartActin, maxPartMyosin;
        private int rows,cols;
        private DrTirandazVoxel[,] SubVolumes;
        public Cell4Part(DrTirandazVoxel[,] parent, int numberOfRows, int numberOfCols)
        {
            this.rows = numberOfRows;
            this.cols = numberOfCols;
            SubVolumes = parent;
            part11 = new CenterOfActinMyosin();
            part12 = new CenterOfActinMyosin();
            part21 = new CenterOfActinMyosin();
            part22 = new CenterOfActinMyosin();
            partTotal = new CenterOfActinMyosin();
        }

        public void Refresh()
        {
            for (int i = 0; i < rows / 2; i++)
                for (int j = 0; j < cols / 2; j++)
                {
                    part11.actin_W += SubVolumes[i, j].M8_Xactin;
                    part11.acmulatedOfActin_X += (i + 1) * SubVolumes[i, j].M8_Xactin;
                    part11.acmulatedOfActin_Y += (j + 1) * SubVolumes[i, j].M8_Xactin;

                    part11.myosin_W += SubVolumes[i, j].M9_Xmyosin;
                    part11.acmulatedOfMyosin_X += (i + 1) * SubVolumes[i, j].M9_Xmyosin;
                    part11.acmulatedOfMyosin_Y += (j + 1) * SubVolumes[i, j].M9_Xmyosin;
                }
            this.AddLocalPartInfoInTotalPart(part11, partTotal);

            for (int i = 0; i < rows / 2; i++)
                for (int j = cols / 2; j < cols; j++)
                {
                    part12.actin_W += SubVolumes[i, j].M8_Xactin;
                    part12.acmulatedOfActin_X += (i + 1) * SubVolumes[i, j].M8_Xactin;
                    part12.acmulatedOfActin_Y += (j + 1) * SubVolumes[i, j].M8_Xactin;
                    part12.myosin_W += SubVolumes[i, j].M9_Xmyosin;
                    part12.acmulatedOfMyosin_X += (i + 1) * SubVolumes[i, j].M9_Xmyosin;
                    part12.acmulatedOfMyosin_Y += (j + 1) * SubVolumes[i, j].M9_Xmyosin;
                }
            this.AddLocalPartInfoInTotalPart(part12, partTotal);

            for (int i = rows / 2; i < rows; i++)
                for (int j = 0; j < cols / 2; j++)
                {
                    part21.actin_W += SubVolumes[i, j].M8_Xactin;
                    part21.acmulatedOfActin_X += (i + 1) * SubVolumes[i, j].M8_Xactin;
                    part21.acmulatedOfActin_Y += (j + 1) * SubVolumes[i, j].M8_Xactin;
                    part21.myosin_W += SubVolumes[i, j].M9_Xmyosin;
                    part21.acmulatedOfMyosin_X += (i + 1) * SubVolumes[i, j].M9_Xmyosin;
                    part21.acmulatedOfMyosin_Y += (j + 1) * SubVolumes[i, j].M9_Xmyosin;
                }

            this.AddLocalPartInfoInTotalPart(part21, partTotal);

            for (int i = rows / 2; i < rows; i++)
                for (int j = cols / 2; j < cols; j++)
                {
                    part22.actin_W += SubVolumes[i, j].M8_Xactin;
                    part22.acmulatedOfActin_X += (i + 1) * SubVolumes[i, j].M8_Xactin;
                    part22.acmulatedOfActin_Y += (j + 1) * SubVolumes[i, j].M8_Xactin;
                    part22.myosin_W += SubVolumes[i, j].M9_Xmyosin;
                    part22.acmulatedOfMyosin_X += (i + 1) * SubVolumes[i, j].M9_Xmyosin;
                    part22.acmulatedOfMyosin_Y += (j + 1) * SubVolumes[i, j].M9_Xmyosin;
                }
            this.AddLocalPartInfoInTotalPart(part22, partTotal);

            maxPartActin = this.MaxActin(part11, part12, part21, part22);
            maxPartMyosin = this.MaxMyosin(part11, part12, part21, part22);

        }

        private void AddLocalPartInfoInTotalPart(CenterOfActinMyosin partLocal, CenterOfActinMyosin partTotal)
        {
            partTotal.actin_W += partLocal.actin_W;
            partTotal.acmulatedOfActin_X += partLocal.acmulatedOfActin_X;
            partTotal.acmulatedOfActin_Y += partLocal.acmulatedOfActin_Y;

            partTotal.myosin_W += partLocal.myosin_W;
            partTotal.acmulatedOfMyosin_X += partLocal.acmulatedOfMyosin_X;
            partTotal.acmulatedOfMyosin_Y += partLocal.acmulatedOfMyosin_Y;
        }

        private CenterOfActinMyosin MaxMyosin(CenterOfActinMyosin part11, CenterOfActinMyosin part12, CenterOfActinMyosin part21, CenterOfActinMyosin part22)
        {
            var t1 = part11.myosin_W > part12.myosin_W ? part11 : part12;
            var t2 = part21.myosin_W > part22.myosin_W ? part21 : part22;

            var t3 = t1.myosin_W > t2.myosin_W ? t1 : t2;

            return t3;
        }


        private CenterOfActinMyosin MaxActin(CenterOfActinMyosin part11, CenterOfActinMyosin part12, CenterOfActinMyosin part21, CenterOfActinMyosin part22)
        {
            var t1 = part11.actin_W > part12.actin_W ? part11 : part12;
            var t2 = part21.actin_W > part22.actin_W ? part21 : part22;

            var t3 = t1.actin_W > t2.actin_W ? t1 : t2;

            return t3;
        }
    }
}
