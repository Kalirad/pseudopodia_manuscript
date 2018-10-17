using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace StochasticalChemicalLevel
{
    public class CellBodyLogger
    {
        private static string filePath = string.Format("stepsVoxelInfo_1_{0}.csv", DateTime.Now.ToString("yyyy_MM_dd__HH-mm-ss"));
        private static string filePath2 = string.Format("stepsVoxelInfo_2_{0}.csv", DateTime.Now.ToString("yyyy_MM_dd__HH-mm-ss"));
        private static string filePathStepsQuantomic = string.Format("QuantomicSteps_{0}.csv", DateTime.Now.ToString("yyyy_MM_dd__HH-mm-ss"));
        string headerLine = "step,voxel, reaction, stepTime, totalTime," +
        " CenterOfActin_11_X,CenterOfActin_11_Y,SumOfActin_11, CenterOfMyosin_11_X,CenterOfMyosin_11_Y,SumOfMyosin_11," +
        " CenterOfActin_12_X,CenterOfActin_12_Y,SumOfActin_12, CenterOfMyosin_12_X,CenterOfMyosin_12_Y,SumOfMyosin_12," +
        " CenterOfActin_21_X,CenterOfActin_21_Y,SumOfActin_21, CenterOfMyosin_21_X,CenterOfMyosin_21_Y,SumOfMyosin_21," +
        " CenterOfActin_22_X,CenterOfActin_22_Y,SumOfActin_22, CenterOfMyosin_22_X,CenterOfMyosin_22_Y,SumOfMyosin_22 \n";

        string quantomicHeaderLine = "step,voxelID,voxel_Row,voxel_Col,reaction,OldTime_clock, stepTime, time_clock, centOfTotalActin_X, centOfTotalActin_Y, centOfTotalMyosin_X, centOfTotalMyosin_Y,  centerOfActin_X, centerOfActin_Y, centerOfMyosin_X, centerOfMyosin_Y,actinMyosinLocationID";
        string numberFormat = "0.00000";

        public CellBodyLogger()
        {
            //File.AppendAllText(filePath, headerLine);
            //File.AppendAllText(filePath2, headerLine);
            //File.AppendAllText(filePathStepsQuantomic, quantomicHeaderLine);
            File.AppendAllText(filePathForDrKalirad, drKaliradOutputHeader);
        }

        private void LogToFile(int voxelI, int voxelJ, int action, double lastStepTime, double totalTime, DrTirandazVoxel[,] voxels)
        {
            //try
            //{
            //    StringBuilder sb = new StringBuilder();
            //    sb.Append(stepCount + ",");
            //    sb.Append(voxelI + "_" + voxelJ + ",");
            //    sb.Append(action + ",");
            //    sb.Append(lastStepTime.ToString(numberFormat) + ",");
            //    sb.Append(totalTime.ToString(numberFormat) + ",");
            //    //------------------------------
            //    string str11 = CalcCenterOfActinMyosin(voxels, 11);
            //    string str12 = CalcCenterOfActinMyosin(voxels, 12);
            //    string str21 = CalcCenterOfActinMyosin(voxels, 21);
            //    string str22 = CalcCenterOfActinMyosin(voxels, 22);

            //    sb.Append(str11 + ", ");
            //    sb.Append(str12 + ", ");
            //    sb.Append(str21 + ", ");
            //    sb.Append(str22);
            //    File.AppendAllText(filePath, sb.ToString() + "\n");
            //    if (stepCount % 5000 == 0)
            //        File.AppendAllText(filePath2, sb.ToString() + "\n");

            //}
            //catch (Exception ex)
            //{

            //}
        }

        string dblFormat = "0.000000";
        internal void Log(int stepCount, DrTirandazVoxel vox, int reaction, double voxelOldClock, double stepTime, Cell4Part cell4Parts)
        {
            string centOfActin = "null";
            string centOfMyosin = "null";
            if (cell4Parts != null && cell4Parts.maxPartActin != null)
                // centOfActin= string.Format("X={0} _ Y={1}",cell4Parts.maxPartActin.CenterOfActin_x.ToString(".00"),cell4Parts.maxPartActin.CenterOfActin_x.ToString(".00"));
                centOfActin = string.Format("{0},{1}", cell4Parts.maxPartActin.CenterOfActin_x.ToString(".00"), cell4Parts.maxPartActin.CenterOfActin_Y.ToString(".00"));
            if (cell4Parts != null && cell4Parts.maxPartMyosin != null)
                // centOfMyosin = string.Format("X={0} _ Y={1}",cell4Parts.maxPartMyosin.CenterOfMyosin_x.ToString(".00"),cell4Parts.maxPartMyosin.CenterOfMyosin_x.ToString(".00"));
                centOfMyosin = string.Format("{0},{1}", cell4Parts.maxPartMyosin.CenterOfMyosin_x.ToString(".00"), cell4Parts.maxPartMyosin.CenterOfMyosin_Y.ToString(".00"));


            string centOfTotalActin = string.Format("{0},{1}", cell4Parts.partTotal.CenterOfActin_x.ToString(".00"), cell4Parts.partTotal.CenterOfActin_Y.ToString(".00"));
            string centOfTotalMyosin = string.Format("{0},{1}", cell4Parts.partTotal.CenterOfMyosin_x.ToString(".00"), cell4Parts.partTotal.CenterOfMyosin_Y.ToString(".00"));


            string line = string.Format("\n {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}", stepCount, vox.Row * 10 + vox.Col, vox.Row, vox.Col, reaction, voxelOldClock.ToString(dblFormat), stepTime.ToString(dblFormat), vox.QuantomixClock.ToString(dblFormat), centOfTotalActin, centOfTotalMyosin, centOfActin, centOfMyosin, cell4Parts.maxPartActin.actinMyosinLocationID);
            File.AppendAllText(filePathStepsQuantomic, line);
        }

        string drKaliradOutputHeader = "stepCount, stepTime,maxA,meanA,varA,meanB,varB,meanC,varC,meanD,varD,reaction \n";
        private static string filePathForDrKalirad = string.Format("oupt4DrKalirad_{0}.csv", DateTime.Now.ToString("yyyy_MM_dd__HH-mm-ss"));
        internal void Log(int stepCount, DrKaliradVoxel vox, int reaction, double voxelOldClock, double stepTime, DrKaliradVoxel[,] allVoxels, int row, int col)
        {

            double meanA, varA, meanB, varB, meanC, varC, meanD, varD,maxA;
            this.CalcMeanVar(allVoxels, out meanA, out varA, out meanB, out varB, out meanC, out varC, out meanD, out varD, row, col, out maxA);
            string line = string.Format("{0},{1},{11},{2},{3},{4},{5},{6},{7},{8},{9},{10} \n", stepCount, stepTime, meanA, varA, meanB, varB, meanC, varC, meanD, varD, reaction,maxA);

            File.AppendAllText(filePathForDrKalirad, line);
        }

        private void CalcMeanVar(DrKaliradVoxel[,] allVoxels, out double meanA, out double varA,
            out double meanB, out double varB, out double meanC, out double varC, out double meanD, out double varD, int row, int col, out double maxA)
        {
            double n = row * col;
            maxA = 0;
            meanA = varA = meanB = varB = meanC = varC = meanD = varD = 0;
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                {
                    meanA += allVoxels[i, j].A;
                    if (allVoxels[i, j].A > maxA) maxA = allVoxels[i, j].A;
                    meanB += allVoxels[i, j].B;
                    meanC += allVoxels[i, j].C;
                    meanD += allVoxels[i, j].D;
                }
            meanA = meanA / n;
            meanB = meanB / n;
            meanC = meanC / n;
            meanD = meanD / n;
            //--------------------------------------
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                {
                    varA += Math.Pow(meanA -allVoxels[i, j].A, 2);
                    varB += Math.Pow(meanB - allVoxels[i, j].B, 2);
                    varC += Math.Pow(meanC - allVoxels[i, j].C, 2);
                    varD += Math.Pow(meanD - allVoxels[i, j].D, 2);
                }

            varA = varA / n;
            varB = varB / n;
            varC = varC / n;
            varD = varD / n;
        }
    }
}
