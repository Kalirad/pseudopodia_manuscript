using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StochasticalChemicalLevel
{
   public class Cell3Dbody
    {
        public DrTirandazVoxel[,,] SubVolumes { get; private set; }
        public int VoxelSize { get; private set; }
        public int NumberOfRowVoxels { get; private set; }

        public int NumberOfColVoxels { get; private set; }
        public int NumberOfDepthVoxels { get; private set; }
        

        public Cell3Dbody(int numberOfRowVoxels, int numberOfColVoxels, int numberOfDepthVoxels, int voxelSize)
        {
            NumberOfColVoxels = numberOfColVoxels;
            NumberOfRowVoxels = numberOfRowVoxels;
            NumberOfDepthVoxels = numberOfDepthVoxels;
            this.VoxelSize = voxelSize;
            SubVolumes = new DrTirandazVoxel[numberOfRowVoxels, numberOfColVoxels, numberOfDepthVoxels];
            for (int i = 0; i < numberOfRowVoxels; i++)
                for (int j = 0; j < numberOfColVoxels; j++)
                    for (int k = 0; k < numberOfDepthVoxels; k++)
                    {
                    SubVolumes[i, j,k] = new DrTirandazVoxel();
                }
        }

        internal void UpdateVoxels(int time)
        {
            //for (int i = 0; i < NumberOfRowVoxels; i++)
            //    for (int j = 0; j < NumberOfColVoxels; j++)
            //        for (int k = 0; k < NumberOfDepthVoxels; k++)
            //        {
            //        SubVolumes[i, j,k].UpdateNumberOfMoleculesByReactions();
            //        DiffuseOutToNeighbors(i, j,k, SubVolumes);
            //    }
        }

        private void DiffuseOutToNeighbors(int i, int j, int k, DrTirandazVoxel[,,] subVolumes)
        {
            try
            {
                if (i > 0)
                    this.MoveMoleculesFromFirstToSecond(subVolumes[i, j, k], subVolumes[i - 1, j, k]);
                if (j > 0)
                    this.MoveMoleculesFromFirstToSecond(subVolumes[i, j, k], subVolumes[i, j - 1, k]);
                if(k>0)
                    this.MoveMoleculesFromFirstToSecond(subVolumes[i, j, k], subVolumes[i, j , k -1 ]);

                if (i < NumberOfRowVoxels - 1)
                    this.MoveMoleculesFromFirstToSecond(subVolumes[i, j, k], subVolumes[i + 1, j, k]);
                if (j < NumberOfColVoxels - 1)
                    this.MoveMoleculesFromFirstToSecond(subVolumes[i, j, k], subVolumes[i, j + 1, k]);
                if (k < NumberOfColVoxels - 1)
                    this.MoveMoleculesFromFirstToSecond(subVolumes[i, j, k], subVolumes[i, j , k+1]);
            }
            catch (Exception ex)
            {
                var a = 100 * i + j;
                throw ex;
            }
        }

        Random rnd = new Random(DateTime.Now.Millisecond);
        private void MoveMoleculesFromFirstToSecond(DrTirandazVoxel voxelSourc, DrTirandazVoxel voxelDestination)
        {
            int difRate = rnd.Next(2, 10);//2;//برای هر ملکولی باید فرق کنه
            //voxelSourc.M1_Ras -= difRate;
            //voxelSourc.M2_PI3K -= difRate;
            voxelSourc.M3_PTEN -= difRate;
            //voxelSourc.M4_SGP -= difRate;
            //voxelSourc.M5_PIP -= difRate;
            //voxelSourc.M6_P2 -= difRate;
            //voxelSourc.M7_P3 -= difRate;
            //voxelSourc.M8_Xa -= difRate;
            //voxelSourc.M9_Xm -= difRate;
            ////__________________
            //voxelDestination.M1_Ras += difRate;
            //voxelDestination.M2_PI3K += difRate;
            voxelDestination.M3_PTEN += difRate;
            //voxelDestination.M4_SGP += difRate;
            //voxelDestination.M5_PIP += difRate;
            //voxelDestination.M6_P2 += difRate;
            //voxelDestination.M7_P3 += difRate;
            //voxelDestination.M8_Xa += difRate;
            //voxelDestination.M9_Xm += difRate;
        }
    }
}
    
