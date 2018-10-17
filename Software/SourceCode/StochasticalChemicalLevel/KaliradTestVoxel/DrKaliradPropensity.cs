using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StochasticalChemicalLevel
{
    public static class DrKaliradPropensity
    {
        private static double n = 3;
        private static double s = 1;
        public static double Propensity1(DrKaliradVoxel voxel)
        {
            return (Math.Pow(voxel.B,n) * voxel.B * voxel.A)/(s+ Math.Pow(voxel.B, n));
        }
        public static double Propensity2(DrKaliradVoxel voxel)
        {
            return (Math.Pow(voxel.D, n) * voxel.B ) / (s + Math.Pow(voxel.D, n));
        }
        public static double Propensity3(DrKaliradVoxel voxel)
        {
            return (Math.Pow(voxel.D, n) * voxel.C * voxel.D) / (s + Math.Pow(voxel.D, n));
        }
        public static double Propensity4(DrKaliradVoxel voxel)
        {
            return (Math.Pow(voxel.B, n) * voxel.D) / (s + Math.Pow(voxel.B, n));
        }
    }
}
