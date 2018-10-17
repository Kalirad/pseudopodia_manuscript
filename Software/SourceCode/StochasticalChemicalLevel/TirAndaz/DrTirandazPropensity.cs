using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StochasticalChemicalLevel
{
    public static class DrTirandazPropensity
    {
        public static double Propensity1(DrTirandazVoxel voxel)
        {
            return DrTirandazVoxel.K_a * voxel.M1_Ras;
        }

        public static double Propensity2(DrTirandazVoxel voxel)
        {
            return DrTirandazVoxel.K_I * voxel.M1_Ras;
        }

        public static double Propensity3(DrTirandazVoxel voxel)
        {
            return DrTirandazVoxel.K_1 * voxel.M2_PI3K * voxel.M6_P2;
        }

        public static double Propensity4(DrTirandazVoxel voxel)
        {
            return DrTirandazVoxel.K_n1 * voxel.M26_PI3K_P2;
        }

        public static double Propensity5(DrTirandazVoxel voxel)
        {
            return DrTirandazVoxel.K_11 * voxel.M26_PI3K_P2;
        }

        public static double Propensity6(DrTirandazVoxel voxel)
        {
            return DrTirandazVoxel.K_2 * voxel.M3_PTEN * voxel.M7_P3;
        }

        public static double Propensity7(DrTirandazVoxel voxel)
        {
            return DrTirandazVoxel.K_n2 * voxel.M37_PTEN_P3;
        }

        public static double Propensity8(DrTirandazVoxel voxel)
        {
            return DrTirandazVoxel.K_22 * voxel.M37_PTEN_P3;
        }

        public static double Propensity9(DrTirandazVoxel voxel)
        {
            return DrTirandazVoxel.K_3 * voxel.M7_P3;
        }

        public static double Propensity10(DrTirandazVoxel voxel)
        {
            return DrTirandazVoxel.K_4 * voxel.PIP;
        }

        public static double Propensity11(DrTirandazVoxel voxel)
        {
            return DrTirandazVoxel.K_n4 * voxel.M6_P2;
        }

        public static double Propensity12(DrTirandazVoxel voxel)
        {
            return DrTirandazVoxel.K_5 * voxel.M7_P3;
        }

        public static double Propensity13(DrTirandazVoxel voxel)
        {
            return DrTirandazVoxel.K_6 * voxel.M3_PTEN;
        }

        public static double Propensity14(DrTirandazVoxel voxel)
        {
            return DrTirandazVoxel.K_222 * voxel.M3_PTEN;
        }

        public static double Propensity15(DrTirandazVoxel voxel)
        {
            return DrTirandazVoxel.K_333 * voxel.M2_PI3K;
        }

        //public static double Propensity14_1(Voxel voxel)
        //{
        //    return Voxel.K_222 * voxel.M3_PTEN * voxel.M8_Xactin;
        //}

        //public static double Propensity15_1(Voxel voxel)
        //{
        //    return Voxel.K_333 * voxel.M2_PI3K * voxel.M9_Xmyosin;
        //}
        public const  double D_PTEN = 5E-12;//0.0000005;//5 microMeter^2/s
        public static double PropensityDifUp(DrTirandazVoxel voxel)
        {
            if (voxel.IsTopBoundry) return 0;
            double tt = voxel.M3_PTEN * (D_PTEN / DrTirandazVoxel.Area);
            if(tt>0)
            {


            }
            return tt;
        }
        public static double PropensityDifDown(DrTirandazVoxel voxel)
        {
            if (voxel.IsBottonBoundry) return 0;
            return voxel.M3_PTEN * (D_PTEN / DrTirandazVoxel.Area);
        }
        public static double PropensityDifRight(DrTirandazVoxel voxel)
        {
            if (voxel.IsRightBoundry) return 0;
            return voxel.M3_PTEN * (D_PTEN / DrTirandazVoxel.Area);
        }
        public static double PropensityDifLeft(DrTirandazVoxel voxel)
        {
            if (voxel.IsLeftBoundry) return 0;
            double d = 2 * voxel.M3_PTEN * (D_PTEN / DrTirandazVoxel.Area);
            return d;
        }

        public static double Propensity21(DrTirandazVoxel voxel)
        {
            return 1;
        }
    }
}
