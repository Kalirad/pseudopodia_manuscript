using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StochasticalChemicalLevel
{
    static public class DrKaliradReaction
    {
       static Random rnd = new Random(DateTime.Now.Millisecond);
        public static void R1(DrKaliradVoxel voxel)
        {
            voxel.A--;
            voxel.B++;
            
            int r = rnd.Next(1, 10);
            if (r % 3 == 0 && voxel.B>1) voxel.B--;
        }
        public static void R2(DrKaliradVoxel voxel)
        {
            voxel.B--;
            voxel.A++;
           
        }
        public static void R3(DrKaliradVoxel voxel)
        {
            voxel.C--;
            voxel.D++;
            int r = rnd.Next(1, 10);
            if (r % 3 == 0 && voxel.D>1) voxel.D--;
        }
        public static void R4(DrKaliradVoxel voxel)
        {
            voxel.D--;
            voxel.C++;
            int r = rnd.Next(1, 10);
            if (r % 3 == 0 && voxel.C>1) voxel.C--;
        }
    }
}
