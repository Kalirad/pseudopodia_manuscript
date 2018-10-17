using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StochasticalChemicalLevel
{
    public class DrKaliradCellBody : ICellBody
    {
        public int stepCount = 0;
       
        public DrKaliradVoxel[,] SubVolumes { get; private set; }
        public double VoxelSize { get; private set; }
        public int NumberOfRowVoxels { get; private set; }

        public int NumberOfColVoxels { get; private set; }

        private static int numberOfReactions = 4;
        private static int numberOfVoxels;
        private double[,,] roletWeel;
      
        private CellBodyLogger cellBodyLogger;
        public DrKaliradCellBody(int numberOfRowVoxels, int numberOfColVoxels, double voxelSize)
        {
            NumberOfRowVoxels = numberOfRowVoxels;
            NumberOfColVoxels = numberOfColVoxels;
           
            cellBodyLogger = new CellBodyLogger();
            numberOfVoxels = NumberOfColVoxels * NumberOfRowVoxels;
            roletWeel = new double[NumberOfRowVoxels, NumberOfColVoxels, numberOfReactions];
            CellBodyRandomReactionSelection.NumberOfRowVoxels = numberOfRowVoxels;
            CellBodyRandomReactionSelection.NumberOfColVoxels = numberOfColVoxels;
            CellBodyRandomReactionSelection.numberOfReactions = numberOfReactions;
            CellBodyRandomReactionSelection.roletWeelOneVoxel = new double[numberOfReactions];
            this.VoxelSize = voxelSize;
            DrKaliradVoxel.Size = voxelSize ;
            DrKaliradVoxel.Area = voxelSize * voxelSize;
            DrKaliradVoxel.Volume = voxelSize * voxelSize * voxelSize;
            //Voxel.K_1 = 0.5E-6 / (Voxel.N_Avogadro * Voxel.Volume);
            //Voxel.K_2 = 0.5E-6 / (Voxel.N_Avogadro * Voxel.Volume);
            //Voxel.K_111 = 20E-6 / (Voxel.N_Avogadro * Voxel.Volume);
            //Voxel.K_222 = 20E-6 / (Voxel.N_Avogadro * Voxel.Volume);
            //Voxel.K_333 = 20E-6 / (Voxel.N_Avogadro * Voxel.Volume);
            //چون احتمالها ده بتوان 15 برابر فرق داشت
            //فعلا دستی نزدیک میکنیم تا پیش بره
            //بعدا پیدا میکنیم

            
            SubVolumes = new DrKaliradVoxel[numberOfRowVoxels, numberOfColVoxels];
            
            for (int i = 0; i < numberOfRowVoxels; i++)
                for (int j = 0; j < numberOfColVoxels; j++)
                {
                    SubVolumes[i, j] = new DrKaliradVoxel();
                    SubVolumes[i, j].Row = i;
                    SubVolumes[i, j].Col = j;
                }

            for (int i = 0; i < numberOfRowVoxels; i++)
            {
                SubVolumes[i, 0].IsLeftBoundry = true;
                SubVolumes[i, numberOfRowVoxels - 1].IsRightBoundry = true;
            }
            for (int i = 0; i < numberOfColVoxels; i++)
            {
                SubVolumes[0, i].IsTopBoundry = true;
                SubVolumes[numberOfColVoxels - 1, i].IsBottonBoundry = true;
            }

           // testFile();
        }

      

        
        public void UpdateVoxels()
        {
            int a;
            this.SensEnvironnement();
            while (true)
            {
                DrKaliradVoxel vox = this.GetTheBackmost();
                CellBodyRandomReactionSelection.GetRandomReactionInVoxelByPropensityFunction(vox, out a);
                double voxelOldClock = vox.QuantomixClock;
                GodOfReactions.ExecuteThisReactionOnThisVoxel(vox, a, SubVolumes);
                //نیمه عمر پروتئین هایی مثل اکتین چی میشه؟

                var stepTime= GodOfReactions.GetNeededQuantomsForThisReaction(a);
                vox.QuantomixClock += stepTime;
                stepCount++;
                cellBodyLogger.Log(stepCount, vox, a, voxelOldClock, stepTime, SubVolumes,NumberOfRowVoxels, NumberOfColVoxels);
                System.Threading.Thread.Sleep(30);
            }
            // GodOfReactions.ExecuteReactionsInBoard();
        }

        private DrKaliradVoxel GetTheBackmost()
        {
            int min_I = 0;
            int min_J = 0;
            double minClocktime = SubVolumes[0,0].QuantomixClock;
            for (int i = 0; i < NumberOfRowVoxels; i++)
                for (int j = 0; j < NumberOfColVoxels; j++)
                    if(SubVolumes[i, j].QuantomixClock< minClocktime)
                {
                        minClocktime = SubVolumes[i, j].QuantomixClock;
                        min_I = i;
                        min_J = j;
                }

            return SubVolumes[min_I, min_J];
        }

        private string CalcCenterOfActinMyosin(DrTirandazVoxel[,] voxels, int region)
        {
            int rStart = 0, rEnd = 0, cStart = 0, cEnd = 0;
            double centerOfActin_X;
            double centerOfActin_Y;
            double sumOfActin;
            double centerOfMyosin_X;
            double centerOfMyosin_Y;
            double sumOfMyosin;
            sumOfActin = 0;
            sumOfMyosin = 0;

            switch (region)
            {
                case 11:
                    rStart = 0; rEnd = NumberOfRowVoxels / 2; cStart = 0; cEnd = NumberOfRowVoxels / 2;
                    break;
                case 12:
                    rStart = 0; rEnd = NumberOfRowVoxels / 2; cStart = NumberOfRowVoxels / 2; cEnd = NumberOfRowVoxels;
                    break;
                case 21:
                    rStart = NumberOfRowVoxels / 2; rEnd = NumberOfRowVoxels; cStart = 0; cEnd = NumberOfRowVoxels / 2;
                    break;
                case 22:
                    rStart = NumberOfRowVoxels / 2; rEnd = NumberOfRowVoxels; cStart = NumberOfRowVoxels / 2; cEnd = NumberOfRowVoxels;
                    break;
            }
            double Xactin = 0;
            double Yactin = 0;
            sumOfActin = 0;
            double Xmyosin = 0;
            double Ymyosin = 0;
            sumOfMyosin = 0;

            for (int i = rStart; i < rEnd; i++)
                for (int j = cStart; j < cEnd; j++)
                {
                    sumOfActin += voxels[i, j].M8_Xactin;
                    Xactin += (i + 1) * voxels[i, j].M8_Xactin;
                    Yactin += (j + 1) * voxels[i, j].M8_Xactin;

                    sumOfMyosin += voxels[i, j].M9_Xmyosin;
                    Xmyosin += (i + 1) * voxels[i, j].M9_Xmyosin;
                    Ymyosin += (j + 1) * voxels[i, j].M9_Xmyosin;
                }
            centerOfActin_X = Xactin / sumOfActin;
            centerOfActin_Y = Yactin / sumOfActin;

            centerOfMyosin_X = Xmyosin / sumOfMyosin;
            centerOfMyosin_Y = Ymyosin / sumOfMyosin;

            StringBuilder sb = new StringBuilder();
            sb.Append(Math.Floor(centerOfActin_X) + ",");
            sb.Append(Math.Floor(centerOfActin_Y) + ",");
            sb.Append(sumOfActin + ",");

            sb.Append(Math.Floor(centerOfMyosin_X) + ",");
            sb.Append(Math.Floor(centerOfMyosin_Y) + ",");
            sb.Append(sumOfMyosin );
            return sb.ToString();
        }

        private void SensEnvironnement()
        {
            ////Right:
            //for (int i = 0; i < NumberOfRowVoxels; i++)
            //   // for (int j = 0; j < NumberOfColVoxels; j++)
            //    {
            //    if(rnd.Next(1,100)==3) //1%
            //         SubVolumes[i, NumberOfColVoxels-1].M1_Ras++;
            //    }
        }

        public void InitiateMolecularNumbers()
        {
            int min = 1;
            int max = 10;
            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < NumberOfRowVoxels; i++)
                for (int j = 0; j < NumberOfColVoxels; j++)
                {
                    this.SubVolumes[i, j].A = rnd.Next(min, max);
                    this.SubVolumes[i, j].B = rnd.Next(min, max);
                    this.SubVolumes[i, j].C = rnd.Next(min, max);
                    this.SubVolumes[i, j].D = rnd.Next(min, max);
                }
        }

        public void GetStepLengthAndDirection(out double stepLength, out double angle)
        {
            try
            {
                double centerOfActinX = 0;
                double centerOfActinY = 0;
                double sumA = 0;

                double centerOfMyosinX = 0;
                double centerOfMyosinY = 0;
                double sumM = 0;

                for (int i = 0; i < NumberOfRowVoxels; i++)
                    for (int j = 0; j < NumberOfColVoxels; j++)
                    {
                        sumA += SubVolumes[i, j].A;
                        centerOfActinX += (i + 1) * SubVolumes[i, j].A;
                        centerOfActinY += (j + 1) * SubVolumes[i, j].A;

                        sumM += SubVolumes[i, j].D;
                        centerOfMyosinX += (i + 1) * SubVolumes[i, j].D;
                        centerOfMyosinY += (j + 1) * SubVolumes[i, j].D;
                    }

                centerOfActinX = centerOfActinX / sumA;
                centerOfActinY = centerOfActinY / sumA;

                centerOfMyosinX = centerOfMyosinX / sumM;
                centerOfMyosinY = centerOfMyosinY / sumM;

                double deltaX = centerOfMyosinX - centerOfActinX;
                double deltaY = centerOfMyosinY - centerOfActinY;
                angle = Math.Atan(deltaY/ deltaX)* 180;
                stepLength = Math.Sqrt(Math.Pow(deltaX,2)+ Math.Pow(deltaY, 2));
            }
            catch (Exception ex)
            {
                stepLength = -123;
                angle = -360;
            }
        }
    }
}
