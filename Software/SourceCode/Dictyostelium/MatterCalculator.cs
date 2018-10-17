using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vafadar_GOL
{
   static public class MatterCalculator
    {
        static public int GetNumberOfNeighbors(int r, int c, Matter[,] matterTable, int rows, int cols)
        {
            int sum = 0;

            if (r > 0)
            {
                if (c > 0 && matterTable[r - 1, c - 1]!=null)
                    sum ++ ;
                if(matterTable[r - 1, c] != null)
                    sum ++ ;
                if (c < cols - 1 && matterTable[r - 1, c + 1] != null)
                    sum ++ ;
            }

            if (c > 0 && matterTable[r, c - 1] != null)
                sum ++ ;
            if(matterTable[r, c] != null)
                sum ++ ;
            if (c < cols - 1 && matterTable[r, c + 1] != null)
                sum ++ ;

            if (r < rows - 1)
            {
                if (c > 0 && matterTable[r + 1, c - 1] != null)
                    sum ++ ;
                if(matterTable[r + 1, c] != null)
                    sum ++ ;
                if (c < cols - 1 && matterTable[r + 1, c + 1] != null)
                    sum ++;
            }

            return sum;
        }

        static public double GetAverageProbability(int r, int c, double[,] ProbabilityTable, int rows, int cols)
        {
            double sum = 0;

            if (r > 0)
            {
                if (c > 0) sum += ProbabilityTable[r - 1, c - 1];
                sum += ProbabilityTable[r - 1, c];
                if (c < cols - 1) sum += ProbabilityTable[r - 1, c + 1];
            }

            if (c > 0) sum += ProbabilityTable[r, c - 1];
            sum += ProbabilityTable[r, c];
            if (c < cols - 1) sum += ProbabilityTable[r, c + 1];

            if (r < rows - 1)
            {
                if (c > 0) sum += ProbabilityTable[r + 1, c - 1];
                sum += ProbabilityTable[r + 1, c];
                if (c < cols - 1) sum += ProbabilityTable[r + 1, c + 1];
            }
            return sum / 8;
        }

        internal static bool IsBoundary(int row, int col, Matter[,] matterTable, int rows, int cols)
        {
            if (row < 1 || row >= rows - 1 || col < 1 || col >= cols - 1)
                return false;//World Edge

            if (col<cols-1 && ISEmptyCol3Cell(row, col+1, matterTable))//Right
                return true;
            if (col >1 && ISEmptyCol3Cell(row, col - 1, matterTable))//Left
                return true;

            if (row > 1 && ISEmptyRow3Cell(row-1, col, matterTable))//Top
                return true;

            if (row < rows - 1 && ISEmptyRow3Cell(row + 1, col, matterTable))//Bottom
                return true;

            return false;
        }

        private static bool ISEmptyCol3Cell(int row, int col, Matter[,] matterTable)
        {
            //شرایط مرزی چک نشده
            //فقط گوشه ها  
            //وگرنه موقع فراخوانی این وسطچک شده

            if (matterTable[row - 1, col] == null
                &&
                matterTable[row, col] == null
                &&
                matterTable[row + 1, col] == null
                )
                return true;
            return false;
        }
        private static bool ISEmptyRow3Cell(int row, int col, Matter[,] matterTable)
        {
            //شرایط مرزی چک نشده
            if (matterTable[row, col-1] == null
                &&
                matterTable[row, col] == null
                &&
                matterTable[row, col+1] == null
                )
                return true;
            return false;
        }

        internal static bool HasMatterNeighbor(int r, int c, Matter[,] matterTable, int rows, int cols)
        {
            if (r > 0 && matterTable[r - 1, c] != null) return true;
            if (r < rows && matterTable[r + 1, c] != null) return true;

            if (c > 0 && matterTable[r , c - 1] != null) return true;
            if (c < cols && matterTable[r, c + 1] != null) return true;

            return false;
        }
    }
}
