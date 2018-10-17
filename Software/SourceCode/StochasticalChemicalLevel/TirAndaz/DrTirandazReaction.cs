using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StochasticalChemicalLevel
{
    static public class DrTirandazReaction
    {
        public static void R1(DrTirandazVoxel voxel)
        {
            voxel.M1_Ras--;
            voxel.M1_Ras = Math.Max(0, voxel.M1_Ras);
            voxel.M2_PI3K++;
        }

        public static void R2(DrTirandazVoxel voxel)
        {
            voxel.M1_Ras--;
            voxel.M1_Ras = Math.Max(0, voxel.M1_Ras);

            voxel.M3_PTEN++;
        }

        public static void R3(DrTirandazVoxel voxel)
        {
            voxel.M2_PI3K--;
            voxel.M2_PI3K = Math.Max(0, voxel.M2_PI3K);
            voxel.M6_P2--;
            voxel.M6_P2 = Math.Max(0, voxel.M6_P2);

            voxel.M26_PI3K_P2++;
        }

        public static void R4(DrTirandazVoxel voxel)
        {
            voxel.M26_PI3K_P2--;
            voxel.M26_PI3K_P2 = Math.Max(0, voxel.M26_PI3K_P2);

            voxel.M2_PI3K++;
            voxel.M6_P2++;
        }

        public static void R5(DrTirandazVoxel voxel)
        {
            voxel.M26_PI3K_P2--;
            voxel.M26_PI3K_P2 = Math.Max(0, voxel.M26_PI3K_P2);
            voxel.M7_P3++;
        }

        public static void R6(DrTirandazVoxel voxel)
        {
            voxel.M3_PTEN--;
            voxel.M3_PTEN = Math.Max(0, voxel.M3_PTEN);

            voxel.M7_P3--;
            voxel.M7_P3 = Math.Max(0, voxel.M7_P3);

            voxel.M37_PTEN_P3++;
        }

        public static void R7(DrTirandazVoxel voxel)
        {
            voxel.M37_PTEN_P3--;
            voxel.M37_PTEN_P3 = Math.Max(0, voxel.M37_PTEN_P3);

            voxel.M3_PTEN++;
            voxel.M7_P3++;
        }

        public static void R8(DrTirandazVoxel voxel)
        {
            voxel.M37_PTEN_P3--;
            voxel.M37_PTEN_P3 = Math.Max(0, voxel.M37_PTEN_P3);

            voxel.M6_P2++;
        }

        public static void R9(DrTirandazVoxel voxel)
        {
            voxel.M7_P3--;
            voxel.M7_P3 = Math.Max(0, voxel.M7_P3);

            voxel.PIP++;
        }

        public static void R10(DrTirandazVoxel voxel)
        {
            voxel.PIP--;
            voxel.PIP = Math.Max(0, voxel.PIP);

            voxel.M6_P2++;
        }

        public static void R11(DrTirandazVoxel voxel)
        {
            voxel.M6_P2--;
            voxel.M6_P2 = Math.Max(0, voxel.M6_P2);

            voxel.PIP++;
        }

        public static void R12(DrTirandazVoxel voxel)
        {
            voxel.M7_P3--;
            voxel.M7_P3 = Math.Max(0, voxel.M7_P3);

            voxel.M8_Xactin++;
        }
        public static void R13(DrTirandazVoxel voxel)
        {
            voxel.M3_PTEN--;
            voxel.M3_PTEN = Math.Max(0, voxel.M3_PTEN);

            voxel.M9_Xmyosin++;
        }
        public static void R14(DrTirandazVoxel voxel)
        {
            //سوال؟
            //اگه M3_PTEN وجود داشت
            //ولی اکتین نداشت، چه اتفاقی برای این بازدارندگی میوفته؟
            if (voxel.M8_Xactin <= 0) return;
            voxel.M8_Xactin--;//خاصیت بازدارندگی
        }
        public static void R15(DrTirandazVoxel voxel)
        {
            //عین سوال بالا
            if (voxel.M9_Xmyosin <= 0) return;
            voxel.M9_Xmyosin--;//خاصیت بازدارندگی
        }

        public static void DiffuseUp(DrTirandazVoxel src, DrTirandazVoxel dst)
        {
            if(src.M3_PTEN<=0)
            {
                throw new Exception(string.Format("DiffuseUp:src.PTEN={0}  des.PTEN={1}",src.M3_PTEN, dst.M3_PTEN));
            }
            src.M3_PTEN--;
            dst.M3_PTEN++;
        }

        public static void DiffuseDown(DrTirandazVoxel src, DrTirandazVoxel dst)
        {
            if (src.M3_PTEN <= 0)
            {
                throw new Exception(string.Format("DiffuseUp:src.PTEN={0}  des.PTEN={1}", src.M3_PTEN, dst.M3_PTEN));
            }
            src.M3_PTEN--;
            dst.M3_PTEN++;
        }

        public static void DiffuseRight(DrTirandazVoxel src, DrTirandazVoxel dst)
        {
            if (src.M3_PTEN <= 0)
            {
                throw new Exception(string.Format("DiffuseUp:src.PTEN={0}  des.PTEN={1}", src.M3_PTEN, dst.M3_PTEN));
            }
            src.M3_PTEN--;
            dst.M3_PTEN++;
        }

        public static void DiffuseLeft(DrTirandazVoxel src, DrTirandazVoxel dst)
        {
            if (src.M3_PTEN <= 0)
            {
                throw new Exception(string.Format("DiffuseUp:src.PTEN={0}  des.PTEN={1}", src.M3_PTEN, dst.M3_PTEN));
            }

            src.M3_PTEN--;
            dst.M3_PTEN++;
        }

        //تولید به خود به خود P2
        public static void R21(DrTirandazVoxel voxel)
        {
            voxel.M6_P2++;//خاصیت بازدارندگی
        }
        //public static void R22(Voxel voxel)
        //{
        //    voxel.M7_P3++;//خاصیت بازدارندگی
        //}
    }
}
