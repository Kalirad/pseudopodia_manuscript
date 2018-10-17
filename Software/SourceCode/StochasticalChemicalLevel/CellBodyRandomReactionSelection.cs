using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StochasticalChemicalLevel
{
  static public  class CellBodyRandomReactionSelection
    {
       static Random rnd = new Random(DateTime.Now.Millisecond);
        static public double[] roletWeelOneVoxel;
        public static int NumberOfRowVoxels;
        public static int NumberOfColVoxels;
        public static int numberOfReactions;
        public static void GetRandomReactionInVoxelByPropensityFunction(DrTirandazVoxel vox, out int action)
        {

            double sum = 0;

            roletWeelOneVoxel[0] = DrTirandazPropensity.Propensity1(vox);
            roletWeelOneVoxel[1] = DrTirandazPropensity.Propensity2(vox);
            roletWeelOneVoxel[2] = DrTirandazPropensity.Propensity3(vox);
            roletWeelOneVoxel[3] = DrTirandazPropensity.Propensity4(vox);
            roletWeelOneVoxel[4] = DrTirandazPropensity.Propensity5(vox);
            roletWeelOneVoxel[5] = DrTirandazPropensity.Propensity6(vox);
            roletWeelOneVoxel[6] = DrTirandazPropensity.Propensity7(vox);
            roletWeelOneVoxel[7] = DrTirandazPropensity.Propensity8(vox);
            roletWeelOneVoxel[8] = DrTirandazPropensity.Propensity9(vox);
            roletWeelOneVoxel[9] = DrTirandazPropensity.Propensity10(vox);
            roletWeelOneVoxel[10] = DrTirandazPropensity.Propensity11(vox);
            roletWeelOneVoxel[11] = DrTirandazPropensity.Propensity12(vox);
            roletWeelOneVoxel[12] = DrTirandazPropensity.Propensity13(vox);
            roletWeelOneVoxel[13] = DrTirandazPropensity.Propensity14(vox);
            roletWeelOneVoxel[14] = DrTirandazPropensity.Propensity15(vox);

            roletWeelOneVoxel[15] = DrTirandazPropensity.PropensityDifUp(vox);
            roletWeelOneVoxel[16] = DrTirandazPropensity.PropensityDifDown(vox);
            roletWeelOneVoxel[17] = DrTirandazPropensity.PropensityDifRight(vox);
            roletWeelOneVoxel[18] = DrTirandazPropensity.PropensityDifLeft(vox);
            roletWeelOneVoxel[19] = DrTirandazPropensity.Propensity21(vox);
            for (int k = 0; k < numberOfReactions; k++)
                sum += roletWeelOneVoxel[k];

            GodOfReactions.sumPropensity = sum;
            double ss = 0;
            double r = rnd.NextDouble() * sum;
            for (int k = 0; k < numberOfReactions; k++)
            {
                if (roletWeelOneVoxel[k] == 0)
                    continue;

                ss += roletWeelOneVoxel[k];
                if (r < ss)
                {
                    action = (k + 1);
                    return;
                }

            }

            action = -1000;
        }

        public static void GetRandomReactionInVoxelByPropensityFunction(DrKaliradVoxel vox, out int action)
        {

            double sum = 0;

            roletWeelOneVoxel[0] = DrKaliradPropensity.Propensity1(vox);
            roletWeelOneVoxel[1] = DrKaliradPropensity.Propensity2(vox);
            roletWeelOneVoxel[2] = DrKaliradPropensity.Propensity3(vox);
            roletWeelOneVoxel[3] = DrKaliradPropensity.Propensity4(vox);
            for (int k = 0; k < numberOfReactions; k++)
                sum += roletWeelOneVoxel[k];

            GodOfReactions.sumPropensity = sum;
            double ss = 0;
            double r = rnd.NextDouble() * sum;
            for (int k = 0; k < numberOfReactions; k++)
            {
                if (roletWeelOneVoxel[k] == 0)
                    continue;

                ss += roletWeelOneVoxel[k];
                if (r < ss)
                {
                    action = (k + 1);
                    return;
                }

            }

            action = -1000;
        }

        /*
       public static void GetRandomVoxelAndItsReactionByPropensityFunction(Voxel vox, out int row, out int col, out int action)
       {

           double sum = 0;
           for (int i = 0; i < NumberOfRowVoxels; i++)
               for (int j = 0; j < NumberOfColVoxels; j++)
               {


                   roletWeel[i, j, 0] = Propensity.Propensity1(vox);
                   roletWeel[i, j, 1] = Propensity.Propensity2(vox);
                   roletWeel[i, j, 2] = Propensity.Propensity3(vox);
                   roletWeel[i, j, 3] = Propensity.Propensity4(vox);
                   roletWeel[i, j, 4] = Propensity.Propensity5(vox);
                   roletWeel[i, j, 5] = Propensity.Propensity6(vox);
                   roletWeel[i, j, 6] = Propensity.Propensity7(vox);
                   roletWeel[i, j, 7] = Propensity.Propensity8(vox);
                   roletWeel[i, j, 8] = Propensity.Propensity9(vox);
                   roletWeel[i, j, 9] = Propensity.Propensity10(vox);
                   roletWeel[i, j, 10] = Propensity.Propensity11(vox);
                   roletWeel[i, j, 11] = Propensity.Propensity12(vox);
                   roletWeel[i, j, 12] = Propensity.Propensity13(vox);
                   roletWeel[i, j, 13] = Propensity.Propensity14(vox);
                   roletWeel[i, j, 14] = Propensity.Propensity15(vox);

                   roletWeel[i, j, 15] = Propensity.PropensityDifUp(vox);
                   roletWeel[i, j, 16] = Propensity.PropensityDifDown(vox);
                   roletWeel[i, j, 17] = Propensity.PropensityDifRight(vox);
                   roletWeel[i, j, 18] = Propensity.PropensityDifLeft(vox);
                   roletWeel[i, j, 19] = Propensity.Propensity21(vox);
                   for (int k = 0; k < numberOfReactions; k++)
                       sum += roletWeel[i, j, k];
               }

           //for (int i = 0; i < numberOfReactions; i++)
           //    if (roletWeel[i] < 0)
           //    {
           //    }


           double ss = 0;
           double r = rnd.NextDouble() * sum;
           for (int i = 0; i < NumberOfRowVoxels; i++)
               for (int j = 0; j < NumberOfColVoxels; j++)
                   for (int k = 0; k < numberOfReactions; k++)
                   {
                       if (roletWeel[i, j, k] == 0)
                           continue;

                       ss += roletWeel[i, j, k];
                       if (r < ss)
                       {

                           row = i;
                           col = j;
                           action = (k + 1);
                           return;
                       }

                   }

           row = NumberOfRowVoxels - 1;
           col = NumberOfColVoxels - 1;
           action = numberOfReactions - 1;
          
    }
     */
    }
}
